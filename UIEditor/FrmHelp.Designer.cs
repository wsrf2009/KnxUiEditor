namespace UIEditor
{
    partial class FrmHelp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHelp));
            this.wbsHelp = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbsHelp
            // 
            resources.ApplyResources(this.wbsHelp, "wbsHelp");
            this.wbsHelp.Name = "wbsHelp";
            this.wbsHelp.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // FrmHelp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wbsHelp);
            this.Name = "FrmHelp";
            this.Load += new System.EventHandler(this.FrmHelp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbsHelp;
    }
}