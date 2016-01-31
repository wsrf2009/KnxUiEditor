namespace UIEditor
{
    partial class FrmSetWireNumber
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.txtboxNumber = new System.Windows.Forms.TextBox();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.rbDec = new System.Windows.Forms.RadioButton();
            this.rbConstant = new System.Windows.Forms.RadioButton();
            this.rbInc = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(14, 86);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(161, 86);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtboxNumber
            // 
            this.txtboxNumber.Location = new System.Drawing.Point(14, 28);
            this.txtboxNumber.Name = "txtboxNumber";
            this.txtboxNumber.Size = new System.Drawing.Size(222, 21);
            this.txtboxNumber.TabIndex = 2;
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.Location = new System.Drawing.Point(12, 9);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(53, 12);
            this.lblTypeName.TabIndex = 3;
            this.lblTypeName.Text = "线缆编号";
            // 
            // rbDec
            // 
            this.rbDec.AutoSize = true;
            this.rbDec.Location = new System.Drawing.Point(14, 57);
            this.rbDec.Name = "rbDec";
            this.rbDec.Size = new System.Drawing.Size(47, 16);
            this.rbDec.TabIndex = 4;
            this.rbDec.TabStop = true;
            this.rbDec.Text = "递减";
            this.rbDec.UseVisualStyleBackColor = true;
            this.rbDec.CheckedChanged += new System.EventHandler(this.rbDec_CheckedChanged);
            // 
            // rbConstant
            // 
            this.rbConstant.AutoSize = true;
            this.rbConstant.Location = new System.Drawing.Point(102, 58);
            this.rbConstant.Name = "rbConstant";
            this.rbConstant.Size = new System.Drawing.Size(47, 16);
            this.rbConstant.TabIndex = 5;
            this.rbConstant.TabStop = true;
            this.rbConstant.Text = "恒值";
            this.rbConstant.UseVisualStyleBackColor = true;
            this.rbConstant.CheckedChanged += new System.EventHandler(this.rbConstant_CheckedChanged);
            // 
            // rbInc
            // 
            this.rbInc.AutoSize = true;
            this.rbInc.Location = new System.Drawing.Point(189, 58);
            this.rbInc.Name = "rbInc";
            this.rbInc.Size = new System.Drawing.Size(47, 16);
            this.rbInc.TabIndex = 6;
            this.rbInc.TabStop = true;
            this.rbInc.Text = "递增";
            this.rbInc.UseVisualStyleBackColor = true;
            this.rbInc.CheckedChanged += new System.EventHandler(this.rbInc_CheckedChanged);
            // 
            // FrmSetWireNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 118);
            this.Controls.Add(this.rbInc);
            this.Controls.Add(this.rbConstant);
            this.Controls.Add(this.rbDec);
            this.Controls.Add(this.lblTypeName);
            this.Controls.Add(this.txtboxNumber);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Name = "FrmSetWireNumber";
            this.Text = "批量设置";
            this.Load += new System.EventHandler(this.FrmSetWireNumber_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox txtboxNumber;
        private System.Windows.Forms.Label lblTypeName;
        private System.Windows.Forms.RadioButton rbDec;
        private System.Windows.Forms.RadioButton rbConstant;
        private System.Windows.Forms.RadioButton rbInc;
    }
}