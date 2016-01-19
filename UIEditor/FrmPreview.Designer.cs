namespace UIEditor
{
    partial class FrmPreview
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
            this.flpMainTools = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefersh = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.palToolbar = new System.Windows.Forms.Panel();
            this.panLayout = new System.Windows.Forms.Panel();
            this.flpMainTools.SuspendLayout();
            this.palToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMainTools
            // 
            this.flpMainTools.AutoSize = true;
            this.flpMainTools.Controls.Add(this.btnRefersh);
            this.flpMainTools.Controls.Add(this.buttonClose);
            this.flpMainTools.Location = new System.Drawing.Point(2, 2);
            this.flpMainTools.Margin = new System.Windows.Forms.Padding(0);
            this.flpMainTools.Name = "flpMainTools";
            this.flpMainTools.Size = new System.Drawing.Size(572, 44);
            this.flpMainTools.TabIndex = 0;
            // 
            // btnRefersh
            // 
            this.btnRefersh.Image = global::UIEditor.Properties.Resources.Refresh_24;
            this.btnRefersh.Location = new System.Drawing.Point(1, 1);
            this.btnRefersh.Margin = new System.Windows.Forms.Padding(1);
            this.btnRefersh.Name = "btnRefersh";
            this.btnRefersh.Size = new System.Drawing.Size(100, 42);
            this.btnRefersh.TabIndex = 3;
            this.btnRefersh.Text = " 刷新";
            this.btnRefersh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefersh.UseVisualStyleBackColor = true;
            this.btnRefersh.Click += new System.EventHandler(this.btnRefersh_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Image = global::UIEditor.Properties.Resources.Close_32;
            this.buttonClose.Location = new System.Drawing.Point(103, 1);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(1);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(90, 42);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "关闭";
            this.buttonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // palToolbar
            // 
            this.palToolbar.Controls.Add(this.flpMainTools);
            this.palToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.palToolbar.Location = new System.Drawing.Point(0, 0);
            this.palToolbar.Name = "palToolbar";
            this.palToolbar.Padding = new System.Windows.Forms.Padding(2);
            this.palToolbar.Size = new System.Drawing.Size(784, 48);
            this.palToolbar.TabIndex = 1;
            // 
            // panLayout
            // 
            this.panLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panLayout.Location = new System.Drawing.Point(0, 48);
            this.panLayout.Name = "panLayout";
            this.panLayout.Size = new System.Drawing.Size(784, 514);
            this.panLayout.TabIndex = 2;
            // 
            // FrmPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panLayout);
            this.Controls.Add(this.palToolbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmPreview";
            this.Text = "界面预览";
            this.Activated += new System.EventHandler(this.FrmPreview_Activated);
            this.Load += new System.EventHandler(this.FrmPreview_Load);
            this.flpMainTools.ResumeLayout(false);
            this.palToolbar.ResumeLayout(false);
            this.palToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button btnRefersh;
        private System.Windows.Forms.FlowLayoutPanel flpMainTools;
        private System.Windows.Forms.Panel palToolbar;
        private System.Windows.Forms.Panel panLayout;


    }
}