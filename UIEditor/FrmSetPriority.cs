using KNX;
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
    public partial class FrmSetPriority : Form
    {
        public KNXPriority priority;

        public FrmSetPriority()
        {
            InitializeComponent();

            this.CenterToParent();

            foreach (var it in Enum.GetNames(typeof(KNXPriority)))
            {
                this.cmboxPriority.Items.Add(it);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var selectedText = this.cmboxPriority.SelectedItem;
            if (selectedText != null)
            {
                KNXPriority dataPriority = KNXPriority.Low;
                Enum.TryParse(selectedText.ToString(), out dataPriority);
                priority = dataPriority;

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
