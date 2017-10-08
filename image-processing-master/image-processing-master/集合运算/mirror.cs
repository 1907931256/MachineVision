using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 集合运算
{
    public partial class mirror : Form
    {
        public mirror()
        {
            InitializeComponent();
        }

        private void startMirror_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool GetMirror
        {
            get
            {
                return horMirror.Checked;
            }
        }

        

    }
}
