namespace UIEditor
{
    partial class FrmDownloadUrl
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
            this.grbBottom = new System.Windows.Forms.GroupBox();
            this.flpCommands = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lklDownload = new System.Windows.Forms.LinkLabel();
            this.grbBottom.SuspendLayout();
            this.flpCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbBottom
            // 
            this.grbBottom.Controls.Add(this.flpCommands);
            this.grbBottom.Location = new System.Drawing.Point(-15, 98);
            this.grbBottom.Name = "grbBottom";
            this.grbBottom.Size = new System.Drawing.Size(372, 75);
            this.grbBottom.TabIndex = 4;
            this.grbBottom.TabStop = false;
            // 
            // flpCommands
            // 
            this.flpCommands.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flpCommands.Controls.Add(this.btnOK);
            this.flpCommands.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpCommands.Location = new System.Drawing.Point(155, 20);
            this.flpCommands.Name = "flpCommands";
            this.flpCommands.Size = new System.Drawing.Size(189, 30);
            this.flpCommands.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(109, 3);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(22, 28);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(113, 12);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Text = "项目文件下载地址：";
            // 
            // lklDownload
            // 
            this.lklDownload.AutoSize = true;
            this.lklDownload.Location = new System.Drawing.Point(25, 59);
            this.lklDownload.Name = "lklDownload";
            this.lklDownload.Size = new System.Drawing.Size(107, 12);
            this.lklDownload.TabIndex = 6;
            this.lklDownload.TabStop = true;
            this.lklDownload.Text = "http://localhost/";
            this.lklDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklDownload_LinkClicked);
            // 
            // FrmDownloadUrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 162);
            this.Controls.Add(this.lklDownload);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.grbBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDownloadUrl";
            this.ShowIcon = false;
            this.Text = "项目下载地址";
            this.Load += new System.EventHandler(this.FrmDownloadUrl_Load);
            this.grbBottom.ResumeLayout(false);
            this.flpCommands.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbBottom;
        private System.Windows.Forms.FlowLayoutPanel flpCommands;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.LinkLabel lklDownload;
    }
}