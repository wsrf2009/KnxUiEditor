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
    public partial class FrmSetWireNumber : Form
    {
        public string typeName;
        public string value;
        public enum CodingMode {Decrease = 1, Constant = 2, Increment = 3 };

        public CodingMode mode = 0;

        public FrmSetWireNumber()
        {
            InitializeComponent();

            this.CenterToParent();
        }

        private void FrmSetWireNumber_Load(object sender, EventArgs e)
        {
            this.lblTypeName.Text = typeName;
            this.rbConstant.Checked = true;
            mode = CodingMode.Constant;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (0 < this.txtboxNumber.Text.Trim().Length)
            {
                value = txtboxNumber.Text;
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

        private void rbDec_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbDec.Checked) 
            {
                mode = CodingMode.Decrease;
                this.rbConstant.Checked = false;
                this.rbInc.Checked = false;
            }
        }

        private void rbConstant_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbConstant.Checked)
            {
                mode = CodingMode.Constant;
                this.rbDec.Checked = false;
                this.rbInc.Checked = false;
            }
        }

        private void rbInc_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbInc.Checked)
            {
                mode = CodingMode.Increment;
                this.rbDec.Checked = false;
                this.rbConstant.Checked = false;
            }
        }
    }
}
