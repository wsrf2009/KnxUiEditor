using System;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor
{
    partial class FrmAboutBox : Form
    {
        static readonly string FormTitle = ResourceMng.GetString("AppName");
        static readonly string MyProductName = ResourceMng.GetString("ProductName") +": "+ ResourceMng.GetString("AppName");
        static readonly string MyVersion = ResourceMng.GetString("Message16");
        static readonly string MyCopyright = ResourceMng.GetString("Copyright");
        static readonly string MyCompany = ResourceMng.GetString("Company");
        static readonly string Description = ResourceMng.GetString("AppDescription");
        public FrmAboutBox()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", FormTitle);
            this.labelProductName.Text = MyProductName;
            this.labelVersion.Text = string.Format(MyVersion, Application.ProductVersion);
            this.labelCopyright.Text = MyCopyright;
            this.labelCompanyName.Text = MyCompany;
            this.textBoxDescription.Text = Description;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
