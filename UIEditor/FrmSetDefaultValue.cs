﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor
{
    public partial class FrmSetDefaultValue : Form
    {
        public int value;

        public FrmSetDefaultValue()
        {
            InitializeComponent();

            this.CenterToParent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (0 < this.txtboxValue.Text.Length)
            {
                try
                {
                    value = int.Parse(this.txtboxValue.Text);

                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("输入的只能是数字", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Console.Write(ex.Message);
                }
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