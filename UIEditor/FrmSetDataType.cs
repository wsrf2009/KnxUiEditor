using Structure;
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
    public partial class FrmSetDataType : Form
    {
        public KNXDataType type;

        public FrmSetDataType()
        {
            InitializeComponent();

            foreach (var it in Enum.GetNames(typeof(KNXDataType)))
            {
                this.comboxType.Items.Add(it);
            }

            this.CenterToParent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var selectedText = this.comboxType.SelectedItem;
            if (selectedText != null)
            {
                KNXDataType dataType = KNXDataType.Bit1;
                Enum.TryParse(selectedText.ToString(), out dataType);
                type = dataType;

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
