using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor
{
    public partial class FrmSetReadTimeSpan : Form
    {
        public int time;

        public FrmSetReadTimeSpan()
        {
            InitializeComponent();

            this.CenterToParent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (0 < this.txtboxTime.Text.Length)
            {
                try
                {
                    time = int.Parse(this.txtboxTime.Text);

                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(UIResMang.GetString("Message24"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);

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
