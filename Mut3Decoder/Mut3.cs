using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;

//http://go.microsoft.com/fwlink/?LinkID=188438&clcid=0%C3%97409

namespace Mut3Decoder
{
    public class Mut3
    {
        private Microsoft.SqlServer.Management.Smo.Server server;
        private SqlConnection sqlConnection;
        private int skey;

        public Mut3()
        {
            
            //sqlConnection = new SqlConnection(@"Integrated Security=true; server=(10.30.31.13)\caesarcompdb; database=CAESARCOMPDB2");
            sqlConnection = new SqlConnection(@"user id=sa2;password=1q2w3e;Integrated Security=false; server=10.30.23.250\CAESARCOMPDB; database=CAESARCOMPDB2");

            Microsoft.SqlServer.Management.Common.ServerConnection serverConnection =
            new Microsoft.SqlServer.Management.Common.ServerConnection(sqlConnection);
            serverConnection.Connect();
            server = new Server(serverConnection);
             
        }

        public List<String> loadDiagVers(String year, String type, String kind, bool etacs)
        {
            int vkey = findVkey(year, type, kind);
            int etacsOrMotorId = etacs ? findEtacsId(vkey) : findMotorId(vkey);
            int syskey = findSyskey(etacsOrMotorId);
            int appFlag = 0;
            int mutClass = findMutClass(vkey, syskey, ref appFlag);
            int skeyId = findSkeyId(mutClass);
            String ecuName = findEcuName(skeyId);

            List<String> res = new List<String>();
            String s = String.Format("SELECT DISTINCT SKEY.DIAGVERSION FROM SKEY_EXT SKEY WHERE SKEY.ID = {0} AND SKEY.ECUNAME = '{1}' AND SKEY.DRIVERTYPE = {2}",
                skeyId, ecuName, 4);

            SqlCommand sql = new SqlCommand(s, sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                res.Add(reader["DIAGVERSION"].ToString());
            }
            reader.Close();
            return res;

        }

        public void loadEtacs(String year, String type, String kind, int diagVer)
        {
            int vkey = findVkey(year, type, kind);
            int etacsId = findEtacsId(vkey);
            loadCar(vkey, etacsId, diagVer);
        }

        public void loadMotor(String year, String type, String kind, int diagVer)
        {
            int vkey = findVkey(year, type, kind);
            int motorId = findMotorId(vkey);
            loadCar(vkey, motorId, diagVer);
        }

        private void loadCar(int vkey, int etacsOrMotorId, int diagVer)
        {
            int syskey = findSyskey(etacsOrMotorId);
            int appFlag = 0;
            int mutClass = findMutClass(vkey, syskey, ref appFlag);
            appFlag = 0;
            int skeyId = findSkeyId(mutClass);
            String ecuName = findEcuName(skeyId);
            skey = findSkey(skeyId, ecuName, 4, diagVer);
            //skey = 220016501;
            //fillTables(skey);
        }

        private int findVkey(String year, String type, String kind)
        {
            String s = String.Format("SELECT DISTINCT VEHI.VKEY FROM VEHICLE_EXT VEHI JOIN MODELYEAR_MAS MY ON VEHI.MODELYEAR_ID = MY.ID JOIN TYPE_MAS TYPE ON VEHI.TYPE_ID = TYPE.ID JOIN KIND_MAS KIND ON VEHI.KIND_ID = KIND.ID WHERE MY.MODELYEAR = '{0}' AND TYPE.TYPE = '{1}' AND KIND.KIND = '{2}'",
                year, type, kind);
            SqlCommand sql = new SqlCommand(s, sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            int vkey = 0;
            while (reader.Read())
            {
                vkey = (int)reader["VKEY"];
            }
            reader.Close();
            if (vkey == 0)
                throw new Exception("Can't find this car");
            return vkey;
        }

        private int findSkey(int skeyId, String ecuName, int driverType, int diagVer)
        {
            int res = 0;
            String s = String.Format("SELECT DISTINCT SKEY.SKEY FROM SKEY_EXT SKEY WHERE SKEY.ID = {0} AND SKEY.ECUNAME = '{1}' AND SKEY.DRIVERTYPE = {2} AND SKEY.DIAGVERSION = {3}",
                skeyId, ecuName, driverType, diagVer);

            SqlCommand sql = new SqlCommand(s, sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                res = (int)reader["SKEY"];
            }
            reader.Close();
            return res;
        }

        private int findVariantCodingId(int skey)
        {
            int res = 0;
            SqlCommand sql = new SqlCommand("SELECT DISTINCT GRPMA.ID, GRPMA.NAME_E, GRPRE.NO FROM CC_GROUP_REF GRPRE JOIN GROUP_MAS GRPMA ON GRPRE.GROUP_ID = GRPMA.ID WHERE GRPRE.USERLEVEL <= 3 AND NAME_E='Variant Coding' AND GRPRE.SKEY = " + skey.ToString(), sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                res = (int)reader["ID"];
            }
            reader.Close();
            return res;
        }

        private int findEtacsId(int vkey)
        {
            int res = 0;
            SqlCommand sql = new SqlCommand("SELECT DISTINCT SYS.ID, SYS.NO, SYS.NAME_E FROM MUTCLASS_EXT MUTCL JOIN SYS_OPT_EXT SYSOP ON MUTCL.ID = SYSOP.SYSKEY JOIN SYSTEM_MAS SYS ON SYSOP.BLOCK_ID = SYS.ID WHERE NAME_E='ETACS' and MUTCL.BLOCK_ID = " + vkey.ToString(), sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                res = (int)reader["ID"];
            }
            reader.Close();
            return res;
        }

        private int findMotorId(int vkey)
        {
            int res = 0;
            SqlCommand sql = new SqlCommand("SELECT DISTINCT SYS.ID, SYS.NO, SYS.NAME_E FROM MUTCLASS_EXT MUTCL JOIN SYS_OPT_EXT SYSOP ON MUTCL.ID = SYSOP.SYSKEY JOIN SYSTEM_MAS SYS ON SYSOP.BLOCK_ID = SYS.ID WHERE NAME_E='MPI/GDI/DIESEL' and MUTCL.BLOCK_ID = " + vkey.ToString(), sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                res = (int)reader["ID"];
            }
            reader.Close();
            return res;
        }

        private String findEcuName(int skeyId)
        {
            String res = String.Empty;
            SqlCommand sql = new SqlCommand("SELECT DISTINCT CBF.FILENAME FROM SKEY_EXT SKEY JOIN CBF_MAS CBF ON SKEY.CBF_ID = CBF.ID WHERE SKEY.ID = " + skeyId.ToString(), sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                res = reader["FILENAME"].ToString();
            }
            reader.Close();
            return res;
        }

        private int findSkeyId(int mutClass)
        {
            int res = 0;
            SqlCommand sql = new SqlCommand("SELECT DISTINCT CAESARCL.SKEY_ID, CAESARCL.CAL_ID, CAESARCL.MULT_CANID, CAESARCL.DTCMASTER, CAESARCL.DTC_COMMETHOD, CAESARCL.ADDRESSING_AGAINST, CAESARCL.EXT_FFDTYPE FROM CAESAR_CLASS_PRO CAESARCL WHERE CAESARCL.MUTCLASS =  " + mutClass.ToString(), sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                res = (int)reader["SKEY_ID"];
            }
            reader.Close();
            return res;
        }

        private int findSyskey(int etacksId)
        {
            int res = 0;
            SqlCommand sql = new SqlCommand("SELECT DISTINCT SYSOP.SYSKEY FROM SYS_OPT_EXT SYSOP WHERE SYSOP.ID = 0 AND SYSOP.BLOCK_ID = " + etacksId.ToString(), sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                res = (int)reader["SYSKEY"];
            }
            reader.Close();
            return res;
        }

        private int findMutClass(int vkey, int syskey, ref int appFlag)
        {
            int res = 0;
            String s = String.Format("SELECT DISTINCT MUTCL.MUTCLASS, MUTCL.APPFLAG FROM MUTCLASS_EXT MUTCL WHERE MUTCL.BLOCK_ID = {0} AND MUTCL.ID = {1}", vkey, syskey);
            SqlCommand sql = new SqlCommand(s, sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                res = (int)reader["MUTCLASS"];
                appFlag = (int)reader["APPFLAG"];
            }
            reader.Close();
            return res;
        }

        public String getMeaning(int id)
        {
            String res = String.Empty;
            SqlCommand sql = new SqlCommand("select NAME_E from meaning_mas where id=" + id.ToString(), sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                res = reader["NAME_E"].ToString();
            }
            reader.Close();
            return res;
        }

        public void getFragProperties(int id, ref int pos, ref int bit, ref int length)
        {
            SqlCommand sql = new SqlCommand("SELECT * from CC_VC_FRAGMENT_MAS where block_id=" + id.ToString() + "and skey=" + skey.ToString(), sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            length = -1;
            while (reader.Read())
            {
                if (length != -1)
                    throw new Exception("More than a single fragment encountered");
                pos = (int)reader["BYTEPOSITION"];
                bit = (int)reader["BITPOSITION"];
                length = (int)reader["LENGTH"];
            }
            reader.Close();
        }

        public Dictionary<String, String> getFragValues(int id)
        {
            Dictionary<String, String> values = new Dictionary<String, String>();
            SqlCommand sql = new SqlCommand("select VALUE, MEANING from cc_vc_fragvalue_mas LEFT JOIN meaning_mas on meaning_mas.id = MEANING_ID where block_id=" + id.ToString() + " and skey=" + skey.ToString(), sqlConnection);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                String val = reader["VALUE"].ToString().Replace(" ", String.Empty);
                while (val.Length > 2 && val[0] == '0' && val[1] == '0')
                    val = val.Remove(0, 2);
                values[val] = reader["MEANING"].ToString();
            }
            reader.Close();
            return values;
        }

        public SqlDataReader readEtacs()
        {
            int vc = findVariantCodingId(skey);
            String s = String.Format("SELECT DISTINCT ITEMMA.NAME_E, VCITEPR.QUAL_ID, VCITEPR.SUQUAL_ID, VCITEPR.DEFAULTVALUE, VCITEPR.USEFLAG, VCITERE.NO, VCFRMEMA.NAME FROM CC_VC_ITEM_REF VCITERE JOIN CC_VC_ITEM_PRO VCITEPR ON VCITERE.ITEM_ID = VCITEPR.ID JOIN ITEM_MAS ITEMMA ON VCITEPR.ITEM_LANG_ID = ITEMMA.ID JOIN CC_VC_FRAGMENT_MAS VCFRMEMA ON VCITEPR.QUAL_ID = VCFRMEMA.BLOCK_ID WHERE VCITERE.SKEY = {0} AND VCITEPR.SKEY = {0} AND VCFRMEMA.SKEY = {0} AND VCITERE.BLOCK_ID = {1} ORDER BY VCITERE.NO", skey, vc);
            SqlCommand sql = new SqlCommand(s, sqlConnection);
            return sql.ExecuteReader();
        }
    }
}
