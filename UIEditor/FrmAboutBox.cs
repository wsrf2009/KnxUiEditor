using System;
using System.Reflection;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor
{
    partial class FrmAboutBox : Form
    {
        static readonly string About = UIResMang.GetString("About");
        static readonly string FormTitle = UIResMang.GetString("AppName");
        static readonly string MyProductName = UIResMang.GetString("ProductName") +": "+ UIResMang.GetString("AppName");
        static readonly string MyVersion = UIResMang.GetString("Message16");
        static readonly string MyCopyright = UIResMang.GetString("Copyright");
        static readonly string MyCompany = UIResMang.GetString("Company");
        static readonly string Description = UIResMang.GetString("AppDescription");
        public FrmAboutBox()
        {
            InitializeComponent();

            Assembly asm = Assembly.GetExecutingAssembly();

            this.Text = String.Format("{0} {1}", About, FormTitle);

            this.labelProductName.Text = Application.ProductName;

            this.labelVersion.Text = Application.ProductVersion;

            AssemblyCopyrightAttribute asmcpr = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCopyrightAttribute));
            this.labelCopyright.Text = asmcpr.Copyright;  //MyCopyright;

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
