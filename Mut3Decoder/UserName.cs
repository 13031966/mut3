using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mut3Decoder
{
    public partial class UserName : Form
    {
        public UserName()
        {
            InitializeComponent();
        }

        public String getName()
        {
            return name.Text;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
