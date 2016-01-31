using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor
{
    public partial class FrmSetOperationTips : Form
    {
        public string tip;

        public FrmSetOperationTips()
        {
            InitializeComponent();

            this.CenterToParent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (0 < txtboxTip.Text.Trim().Length)
            {
                tip = txtboxTip.Text.Trim();

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Ignore;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
