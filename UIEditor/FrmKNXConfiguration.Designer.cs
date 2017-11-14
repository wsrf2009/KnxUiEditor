namespace UIEditor
{
    partial class FrmKNXConfiguration
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.ckbWrite = new System.Windows.Forms.CheckBox();
            this.tlpWrite = new System.Windows.Forms.TableLayoutPanel();
            this.btnWrite = new System.Windows.Forms.Button();
            this.txbxWrite = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.ckbRead = new System.Windows.Forms.CheckBox();
            this.tlpRead = new System.Windows.Forms.TableLayoutPanel();
            this.btnRead = new System.Windows.Forms.Button();
            this.txbxRead = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tlpWrite.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tlpRead.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(436, 224);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoScroll = true;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(436, 164);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.ckbWrite, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tlpWrite, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(10, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(416, 70);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // ckbWrite
            // 
            this.ckbWrite.AutoSize = true;
            this.ckbWrite.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ckbWrite.Location = new System.Drawing.Point(4, 17);
            this.ckbWrite.Name = "ckbWrite";
            this.ckbWrite.Size = new System.Drawing.Size(408, 16);
            this.ckbWrite.TabIndex = 1;
            this.ckbWrite.Text = "Write Address";
            this.ckbWrite.UseVisualStyleBackColor = true;
            this.ckbWrite.CheckedChanged += new System.EventHandler(this.ckbWrite_CheckedChanged);
            // 
            // tlpWrite
            // 
            this.tlpWrite.ColumnCount = 4;
            this.tlpWrite.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpWrite.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpWrite.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpWrite.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tlpWrite.Controls.Add(this.btnWrite, 3, 0);
            this.tlpWrite.Controls.Add(this.txbxWrite, 2, 0);
            this.tlpWrite.Controls.Add(this.label1, 1, 0);
            this.tlpWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpWrite.Location = new System.Drawing.Point(1, 37);
            this.tlpWrite.Margin = new System.Windows.Forms.Padding(0);
            this.tlpWrite.Name = "tlpWrite";
            this.tlpWrite.RowCount = 1;
            this.tlpWrite.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWrite.Size = new System.Drawing.Size(414, 32);
            this.tlpWrite.TabIndex = 0;
            // 
            // btnWrite
            // 
            this.btnWrite.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWrite.Location = new System.Drawing.Point(330, 6);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(3, 6, 3, 5);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(42, 21);
            this.btnWrite.TabIndex = 0;
            this.btnWrite.Text = "...";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // txbxWrite
            // 
            this.txbxWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbxWrite.Location = new System.Drawing.Point(115, 6);
            this.txbxWrite.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.txbxWrite.Name = "txbxWrite";
            this.txbxWrite.Size = new System.Drawing.Size(209, 21);
            this.txbxWrite.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(23, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "Group Address";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.ckbRead, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tlpRead, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(10, 70);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(416, 70);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // ckbRead
            // 
            this.ckbRead.AutoSize = true;
            this.ckbRead.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ckbRead.Location = new System.Drawing.Point(4, 17);
            this.ckbRead.Name = "ckbRead";
            this.ckbRead.Size = new System.Drawing.Size(408, 16);
            this.ckbRead.TabIndex = 1;
            this.ckbRead.Text = "Read Address";
            this.ckbRead.UseVisualStyleBackColor = true;
            this.ckbRead.CheckedChanged += new System.EventHandler(this.ckbRead_CheckedChanged);
            // 
            // tlpRead
            // 
            this.tlpRead.ColumnCount = 4;
            this.tlpRead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpRead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpRead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tlpRead.Controls.Add(this.btnRead, 3, 0);
            this.tlpRead.Controls.Add(this.txbxRead, 2, 0);
            this.tlpRead.Controls.Add(this.label2, 1, 0);
            this.tlpRead.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpRead.Location = new System.Drawing.Point(1, 37);
            this.tlpRead.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRead.Name = "tlpRead";
            this.tlpRead.RowCount = 1;
            this.tlpRead.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRead.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpRead.Size = new System.Drawing.Size(414, 32);
            this.tlpRead.TabIndex = 0;
            // 
            // btnRead
            // 
            this.btnRead.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRead.Location = new System.Drawing.Point(330, 6);
            this.btnRead.Margin = new System.Windows.Forms.Padding(3, 6, 3, 5);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(42, 21);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "...";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txbxRead
            // 
            this.txbxRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbxRead.Location = new System.Drawing.Point(115, 6);
            this.txbxRead.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.txbxRead.Name = "txbxRead";
            this.txbxRead.Size = new System.Drawing.Size(209, 21);
            this.txbxRead.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(23, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Group Address";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnCancel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnOk, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 184);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(436, 30);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(143, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.Location = new System.Drawing.Point(351, 0);
            this.btnOk.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 30);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FrmKNXConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 224);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 240);
            this.Name = "FrmKNXConfiguration";
            this.ShowIcon = false;
            this.Text = "FrmKNXConfiguration";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tlpWrite.ResumeLayout(false);
            this.tlpWrite.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tlpRead.ResumeLayout(false);
            this.tlpRead.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TableLayoutPanel tlpWrite;
        private System.Windows.Forms.TableLayoutPanel tlpRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.TextBox txbxWrite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox txbxRead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbRead;
    }
}