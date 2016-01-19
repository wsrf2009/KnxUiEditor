using System;
using System.Windows.Forms;

namespace UIEditor
{
    public partial class FrmEditCaption : Form
    {
        public FrmEditCaption()
        {
            InitializeComponent();
        }

        private void btnAddLangurage_Click(object sender, EventArgs e)
        {
            Label l1 = new Label();
            l1.Text = "French";
            l1.Anchor = AnchorStyles.Right;
            l1.AutoSize = true;

            TextBox t1 = new TextBox();
            t1.Dock = DockStyle.Fill;

            this.tlpTop.Controls.Add(l1);
            this.tlpTop.Controls.Add(t1);

        }
    }
}
