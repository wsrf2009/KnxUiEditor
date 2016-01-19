using System;
using System.Windows.Forms;

namespace UIEditor
{
    partial class FrmAboutBox : Form
    {
        static readonly string FormTitle = "KNX UI Editor";
        static readonly string MyProductName = "产品名称： KNX UI Editor";
        static readonly string MyVersion = "版本： {0}";
        static readonly string MyCopyright = "版权： Peter Guo 2014 - 2015";
        static readonly string MyCompany = "作者： Peter";
        static readonly string Description = "KNX UI Editor 是一款用户界面定制工具，本工具定义界面的元素和位置，通过解析引擎动态的生成控制界面";
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
