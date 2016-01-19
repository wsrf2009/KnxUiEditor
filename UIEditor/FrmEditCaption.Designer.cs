namespace UIEditor
{
    partial class FrmEditCaption
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
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.lblChinese = new System.Windows.Forms.Label();
            this.lblEnglish = new System.Windows.Forms.Label();
            this.txtChinese = new System.Windows.Forms.TextBox();
            this.txtEnglish = new System.Windows.Forms.TextBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlButtonBar = new System.Windows.Forms.Panel();
            this.cmbLangurage = new System.Windows.Forms.ComboBox();
            this.btnAddLangurage = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tlpTop.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlButtonBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpTop
            // 
            this.tlpTop.AutoScroll = true;
            this.tlpTop.ColumnCount = 2;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tlpTop.Controls.Add(this.lblChinese, 0, 0);
            this.tlpTop.Controls.Add(this.lblEnglish, 0, 1);
            this.tlpTop.Controls.Add(this.txtChinese, 1, 0);
            this.tlpTop.Controls.Add(this.txtEnglish, 1, 1);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTop.Location = new System.Drawing.Point(0, 0);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 8;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Size = new System.Drawing.Size(436, 206);
            this.tlpTop.TabIndex = 0;
            // 
            // lblChinese
            // 
            this.lblChinese.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblChinese.AutoSize = true;
            this.lblChinese.Location = new System.Drawing.Point(48, 6);
            this.lblChinese.Name = "lblChinese";
            this.lblChinese.Size = new System.Drawing.Size(29, 12);
            this.lblChinese.TabIndex = 0;
            this.lblChinese.Text = "中文";
            // 
            // lblEnglish
            // 
            this.lblEnglish.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEnglish.AutoSize = true;
            this.lblEnglish.Location = new System.Drawing.Point(48, 31);
            this.lblEnglish.Name = "lblEnglish";
            this.lblEnglish.Size = new System.Drawing.Size(29, 12);
            this.lblEnglish.TabIndex = 0;
            this.lblEnglish.Text = "英语";
            // 
            // txtChinese
            // 
            this.txtChinese.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChinese.Location = new System.Drawing.Point(83, 3);
            this.txtChinese.Name = "txtChinese";
            this.txtChinese.Size = new System.Drawing.Size(350, 21);
            this.txtChinese.TabIndex = 1;
            // 
            // txtEnglish
            // 
            this.txtEnglish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnglish.Location = new System.Drawing.Point(83, 28);
            this.txtEnglish.Name = "txtEnglish";
            this.txtEnglish.Size = new System.Drawing.Size(350, 21);
            this.txtEnglish.TabIndex = 1;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.tlpTop);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(436, 206);
            this.pnlTop.TabIndex = 3;
            // 
            // pnlButtonBar
            // 
            this.pnlButtonBar.Controls.Add(this.cmbLangurage);
            this.pnlButtonBar.Controls.Add(this.btnAddLangurage);
            this.pnlButtonBar.Controls.Add(this.btnOK);
            this.pnlButtonBar.Controls.Add(this.btnCancel);
            this.pnlButtonBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtonBar.Location = new System.Drawing.Point(3, 209);
            this.pnlButtonBar.Name = "pnlButtonBar";
            this.pnlButtonBar.Size = new System.Drawing.Size(436, 50);
            this.pnlButtonBar.TabIndex = 2;
            // 
            // cmbLangurage
            // 
            this.cmbLangurage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLangurage.FormattingEnabled = true;
            this.cmbLangurage.Items.AddRange(new object[] {
            "French",
            "German",
            "Polish",
            "Russian",
            "Spanish"});
            this.cmbLangurage.Location = new System.Drawing.Point(9, 16);
            this.cmbLangurage.Name = "cmbLangurage";
            this.cmbLangurage.Size = new System.Drawing.Size(74, 20);
            this.cmbLangurage.TabIndex = 2;
            // 
            // btnAddLangurage
            // 
            this.btnAddLangurage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLangurage.Location = new System.Drawing.Point(89, 15);
            this.btnAddLangurage.Name = "btnAddLangurage";
            this.btnAddLangurage.Size = new System.Drawing.Size(80, 23);
            this.btnAddLangurage.TabIndex = 3;
            this.btnAddLangurage.Text = "添加语言";
            this.btnAddLangurage.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(273, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(353, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmEditCaption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 262);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlButtonBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditCaption";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.Text = "添加多语言";
            this.tlpTop.ResumeLayout(false);
            this.tlpTop.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlButtonBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.Label lblChinese;
        private System.Windows.Forms.Label lblEnglish;
        private System.Windows.Forms.TextBox txtChinese;
        private System.Windows.Forms.TextBox txtEnglish;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlButtonBar;
        private System.Windows.Forms.ComboBox cmbLangurage;
        private System.Windows.Forms.Button btnAddLangurage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}