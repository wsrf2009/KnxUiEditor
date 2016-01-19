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
            this.wbsHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbsHelp.Location = new System.Drawing.Point(0, 0);
            this.wbsHelp.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbsHelp.Name = "wbsHelp";
            this.wbsHelp.Size = new System.Drawing.Size(784, 562);
            this.wbsHelp.TabIndex = 0;
            this.wbsHelp.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // FrmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.wbsHelp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmHelp";
            this.Text = "用户帮助";
            this.Load += new System.EventHandler(this.FrmHelp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbsHelp;
    }
}