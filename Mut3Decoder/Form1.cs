using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mut3Decoder
{
    public partial class Form1 : Form
    {
        Mut3 mut;
        Progress progress;
        string coding;

        public Form1(Mut3 mut)
        {
            progress = new Progress();
            this.mut = mut;
            InitializeComponent();
/*
            this.carYear.Text = "2013";
            this.carType.Text = "GF2W";
            this.carKind.Text = "XTSHZL6Z";
            this.codingHex.Text = "001A3380AE0504001C001A66240000A8B7A9B38D180000000845014802083716009B0E1C6D086B792C5035030C28193591041C24953100800016000000000000000000000000";
*/
            grid.Columns.Add("Name", "Name");
            grid.Columns[0].Width = 340;
            grid.Columns.Add("Value", "Value");
            grid.Columns[1].Width = 100;
            grid.Columns.Add("NewValue", "New Value");
            grid.Columns[2].Width = 100;
            grid.Columns.Add("NewValueHex", "hex");
            grid.Columns[3].Width = 30;
            int c = grid.Columns.Add("id", null);
            grid.Columns[c].Width = 30;
            grid.Columns[c].Visible = false;

            itemValues.Columns.Add("Value", "Value");
            itemValues.Columns[0].Width = 200;
            itemValues.Columns.Add("hex", null);
            itemValues.Columns[1].Width = 30;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            showCoding();
        }

        private void loadCar()
        {
            if (diagVer.Items.Count == 0)
                throw new Exception("Select Diagversion");
            if (radioEtacs.Checked)
                mut.loadEtacs(carYear.Text, carType.Text, carKind.Text, int.Parse(diagVer.Text));
            else if (radioMotor.Checked)
                mut.loadMotor(carYear.Text, carType.Text, carKind.Text, int.Parse(diagVer.Text));
        }

        private void showCoding()
        {
            grid.Rows.Clear();
            SqlDataReader reader = mut.readEtacs();
            while (reader.Read())
            {
                int r = grid.Rows.Add();
                grid.Rows[r].Cells[0].Value = reader["NAME"].ToString() + " /// " + reader["NAME_E"].ToString();
                grid.Rows[r].Cells["id"].Value = reader["QUAL_ID"];
            }
            reader.Close();
        }

        private void decode(bool compare, String hex)
        {
            progress.progressBar.Maximum = grid.Rows.Count;
            progress.progressBar.Value = 0;

            for (int i = 0; i < grid.Rows.Count; ++i)
            {
                DataGridViewCellCollection cells = grid.Rows[i].Cells;
                int id = (int)grid.Rows[i].Cells["id"].Value;

                Dictionary<String, String> values = mut.getFragValues(id);

                if (i > 0 && !cells["id"].Value.Equals((int)grid.Rows[i - 1].Cells["id"].Value + 1)) {
                    cells["id"].Style.BackColor = Color.Yellow;
                }

                int pos = 0, bit = 0, length = 0;
                mut.getFragProperties(id, ref pos, ref bit, ref length);
                if (length == 16)
                    length = 8;
                String s = hex.Substring(pos * 2, length <= 8 ? 2 : 4);
                UInt16 val = Convert.ToUInt16(s, 16);
                if (bit != 255)
                {
                    int mask = ((1 << length) - 1) << bit;
                    val = (UInt16)(val & mask);
                }
                String sval = val.ToString("X2");
                try
                {
                    cells[2].Value = values[sval];
                }
                catch (Exception)
                {
                    MessageBox.Show("Can't understand value for parameter " + cells[0].Value);
                    cells[2].Value = "N/A";
                    cells[0].Style.BackColor = Color.Red;
                }
                cells[2].Style.BackColor = SystemColors.Window;
                if (!compare)
                {
                    cells[1].Value = cells[2].Value;
                }
                else
                {
                    if (!cells[2].Value.Equals(cells[1].Value))
                    {
                        cells[2].Style.BackColor = Color.Yellow;
                    }
                }
                cells["NewValueHex"].Value = sval;
                progress.progressBar.Increment(1);
                progress.progressBar.Update();
            }
        }

        private bool validateParams()
        {
            carType.Text = carType.Text.Trim();
            carYear.Text = carYear.Text.Trim();
            carKind.Text = carKind.Text.Trim();

            if (carType.Text.Length == 0)
            {
                MessageBox.Show("Car type is empty");
                carType.Focus();
                return false;
            }
            if (carYear.Text.Length == 0)
            {
                MessageBox.Show("Car year is empty");
                carYear.Focus();
                return false;
            }
            if (carKind.Text.Length == 0)
            {
                MessageBox.Show("Car kind is empty");
                carKind.Focus();
                return false;
            }
            if (!radioEtacs.Checked && !radioMotor.Checked)
            {
                MessageBox.Show("Select ECU type");
                groupBox1.Focus();
                return false;
            }
            return true;
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            if (!validateParams())
                return;
            codingHex.Text = codingHex.Text.Trim();
            if (codingHex.Text.Length == 0)
            {
                MessageBox.Show("Specify coding");
                return;
            }
            try
            {
                loadCar();
                showCoding();
                if (grid.Rows.Count > 1)
                {
                    itemSelected(grid.SelectedRows[0].Index);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            progress.StartPosition = FormStartPosition.Manual;
            progress.Location = new Point(Location.X + Width / 2 - progress.Width / 2, Location.Y + Height / 2 - progress.Height / 2);
            progress.Show();
            decode(false, codingHex.Text);
            progress.Hide();
        }

        private void decode2_Click(object sender, EventArgs e)
        {
            if (!validateParams())
                return;
            if (codingHexNewRich.Text.Length == 0)
            {
                MessageBox.Show("Specify coding");
                return;
            }

            if (codingHexNewRich.Text.Length != codingHex.Text.Length)
            {
                MessageBox.Show("Different length of codings");
                return;
            }
            progress.StartPosition = FormStartPosition.Manual;
            progress.Location = new Point(Location.X + Width / 2 - progress.Width / 2, Location.Y + Height / 2 - progress.Height / 2);
            progress.Show();
            decode(true, codingHexNewRich.Text);
            progress.Hide();
        }

        private void itemSelected(int row)
        {
            itemValues.Rows.Clear();
            DataGridViewCellCollection cells = grid.Rows[row].Cells;
            Object v = cells["id"].Value;
            if (v == null)
                return;
            int id = (int)v;
            Dictionary<String, String> values = mut.getFragValues(id);

            foreach (KeyValuePair<String, String> pair in values)
            {
                int r = itemValues.Rows.Add();
                itemValues.Rows[r].Cells[0].Value = pair.Value;
                itemValues.Rows[r].Cells[1].Value = pair.Key;
                if (pair.Value.Equals(cells[1].Value))
                {
                    itemValues.CurrentCell = itemValues.Rows[r].Cells[0];
                }
            }
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
                return;
            itemSelected(grid.SelectedRows[0].Index);
        }

        private String makeCoding()
        {
            progress.progressBar.Maximum = grid.Rows.Count;
            progress.progressBar.Value = 0;
            String newHex = String.Empty;
            byte[] hex = new byte[codingHex.Text.Length / 2];
            for (int i = 0; i < hex.Length; ++i)
            {
                String s = codingHex.Text.Substring(i * 2, 2);
                hex[i] = Convert.ToByte(s, 16);
            }

            for (int i = 0; i < grid.Rows.Count - 1; ++i)
            {
                DataGridViewCellCollection cells = grid.Rows[i].Cells;
                int id = (int)cells["id"].Value;
                Dictionary<String, String> values = mut.getFragValues(id);

                int pos = 0, bit = 0, length = 0;
                mut.getFragProperties(id, ref pos, ref bit, ref length);
                if (length == 16)
                    length = 8;
                String q = cells["NewValueHex"].Value.ToString();
                byte b = Convert.ToByte(q, 16);
                if (bit != 255)
                {
                    int mask = ((1 << length) - 1) << bit;
                    hex[pos] &= (byte)~mask;
                    hex[pos] |= (byte)(b & mask);
                }
                else
                    hex[pos] |= (byte)(b);
                progress.progressBar.Increment(1);
                progress.progressBar.Update();
            }

            for (int i = 0; i < hex.Length; ++i)
            {
                newHex += hex[i].ToString("X2");
            }
            return newHex;
        }

        static private void appendRichText(RichTextBox box, Color color, String text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
            }
            box.SelectionLength = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!validateParams())
                return;

            progress.StartPosition = FormStartPosition.Manual;
            progress.Location = new Point(Location.X + Width / 2 - progress.Width / 2, Location.Y + Height / 2 - progress.Height / 2);
            progress.Show();
            String coding = makeCoding();
            progress.Hide();
            if (codingHex.Text.Length != coding.Length)
                return;
            codingHexNewRich.Clear();
            for (int i = 0; i < coding.Length; ++i)
            {
                Color color = coding[i] != codingHex.Text[i] ? Color.Red : SystemColors.WindowText;
                appendRichText(codingHexNewRich, color, coding[i].ToString());
            }
        }

        private void itemValues_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellCollection cells = grid.SelectedRows[0].Cells;
            cells["NewValueHex"].Value = itemValues.Rows[e.RowIndex].Cells["hex"].Value;
            cells[2].Value = itemValues.Rows[e.RowIndex].Cells["Value"].Value;
            if (!cells[2].Value.Equals(cells[1].Value))
                cells[2].Style.BackColor = Color.Yellow;
            else
                cells[2].Style.BackColor = SystemColors.Window;
        }

        private void diagVer_Enter(object sender, EventArgs e)
        {
            if (!validateParams())
                return;
            try
            {
                List<String> diags = mut.loadDiagVers(carYear.Text, carType.Text, carKind.Text, radioEtacs.Checked);
                diagVer.Items.Clear();
                diagVer.Items.AddRange(diags.ToArray());
                if (diagVer.Items.Count > 0)
                    diagVer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                carType.Focus();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "D:\\Doc\\!out\\Cdgdata";
            dlg.Filter = "KON files (*.kon)|*.kon|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                coding = System.IO.File.ReadAllText(dlg.FileName);
                string config = coding.Substring(49, 12);
                this.carType.Text = config.Substring(0, 4);
                this.carKind.Text = config.Substring(4);
                this.carYear.Text = "20" + coding.Substring(65, 2);
                this.codingHex.Text = coding.Substring(200, 140);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.codingHexNewRich.Text.Length == 0)
            {
                MessageBox.Show("Specify coding");
                return;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = "D:\\Doc\\!out\\Cdgdata";
            dlg.Filter = "KON files (*.kon)|*.kon|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string newCodingStr = coding.Replace(this.codingHex.Text, this.codingHexNewRich.Text);
                char[] newCoding = newCodingStr.ToCharArray();
                string crcStr = crc(this.codingHexNewRich.Text);
                for (int i = 0; i < 4; ++i) newCoding[1224 + i] = crcStr[i];
                System.IO.File.WriteAllText(dlg.FileName, new string(newCoding));
            }
        }

        static private string crc(string inputStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inputStr);
            for (int i = 0; i <= 0x200; i++)
            {
                if (sb.Length < i)
                    sb.Append(" ");
            }
            inputStr = sb.ToString();
            byte[] str = Encoding.ASCII.GetBytes(inputStr);
            int result = 65535;
            int length = str.Length;
            int CurrentByte = 0;
            int StepByte = 0;

            for (int i = 0; i < length; i++)
            {
                CurrentByte = (int)str[i];
                result ^= CurrentByte;
                StepByte = result;
                for (int j = 0; j < 8; j++)
                {
                    if ((StepByte & 1) == 1)
                    {
                        result = (StepByte >> 1) & 0xFFFF;
                        result ^= 0x8408;
                        StepByte = result;
                    }
                    else
                    {
                        StepByte = (StepByte >> 1) & 0xFFFF;
                        result = StepByte;
                    }
                }
            }
            ushort res = ((ushort)~(short)result);
            return res.ToString("X");
        }
    }
}
