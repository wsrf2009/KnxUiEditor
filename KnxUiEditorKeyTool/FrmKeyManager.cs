using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KnxUiEditorKeyTool
{
    public partial class FrmKeyManager : Form
    {
        public FrmKeyManager()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmSerialNumber frm = new FrmSerialNumber();
            frm.ShowDialog(this);
        }
    }
}
