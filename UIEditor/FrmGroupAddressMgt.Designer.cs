namespace UIEditor
{
    partial class FrmGroupAddressMgt
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvGroupAddress = new System.Windows.Forms.DataGridView();
            this.palTop = new System.Windows.Forms.Panel();
            this.flpToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnImportETSProject = new System.Windows.Forms.Button();
            this.btnImportETSAddressXML = new System.Windows.Forms.Button();
            this.palContent = new System.Windows.Forms.Panel();
            this.flpSearch = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmsDgvGroupAddress = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupAddress)).BeginInit();
            this.palTop.SuspendLayout();
            this.flpToolbar.SuspendLayout();
            this.palContent.SuspendLayout();
            this.flpSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvGroupAddress
            // 
            this.dgvGroupAddress.AllowUserToAddRows = false;
            this.dgvGroupAddress.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvGroupAddress.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGroupAddress.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGroupAddress.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGroupAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGroupAddress.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGroupAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGroupAddress.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvGroupAddress.Location = new System.Drawing.Point(0, 0);
            this.dgvGroupAddress.MultiSelect = false;
            this.dgvGroupAddress.Name = "dgvGroupAddress";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGroupAddress.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvGroupAddress.RowHeadersWidth = 30;
            this.dgvGroupAddress.RowTemplate.Height = 23;
            this.dgvGroupAddress.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGroupAddress.Size = new System.Drawing.Size(1284, 508);
            this.dgvGroupAddress.StandardTab = true;
            this.dgvGroupAddress.TabIndex = 0;
            this.dgvGroupAddress.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGroupAddress_CellContentClick);
            this.dgvGroupAddress.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGroupAddress_CellDoubleClick);
            this.dgvGroupAddress.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvGroupAddress_CellMouseDown);
            this.dgvGroupAddress.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvGroupAddress_ColumnHeaderMouseClick);
            this.dgvGroupAddress.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvGroupAddress_DataBindingComplete);
            this.dgvGroupAddress.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvGroupAddress_RowPostPaint);
            this.dgvGroupAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvGroupAddress_KeyDown);
            // 
            // palTop
            // 
            this.palTop.Controls.Add(this.flpToolbar);
            this.palTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.palTop.Location = new System.Drawing.Point(0, 0);
            this.palTop.Name = "palTop";
            this.palTop.Size = new System.Drawing.Size(1284, 40);
            this.palTop.TabIndex = 1;
            // 
            // flpToolbar
            // 
            this.flpToolbar.AutoSize = true;
            this.flpToolbar.Controls.Add(this.btnAdd);
            this.flpToolbar.Controls.Add(this.btnModify);
            this.flpToolbar.Controls.Add(this.btnSave);
            this.flpToolbar.Controls.Add(this.btnDelete);
            this.flpToolbar.Controls.Add(this.btnImportETSProject);
            this.flpToolbar.Controls.Add(this.btnImportETSAddressXML);
            this.flpToolbar.Location = new System.Drawing.Point(0, 0);
            this.flpToolbar.Name = "flpToolbar";
            this.flpToolbar.Padding = new System.Windows.Forms.Padding(3);
            this.flpToolbar.Size = new System.Drawing.Size(537, 38);
            this.flpToolbar.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 32);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = " 添加";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(83, 3);
            this.btnModify.Margin = new System.Windows.Forms.Padding(0);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(80, 32);
            this.btnModify.TabIndex = 3;
            this.btnModify.Text = " 修改";
            this.btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(163, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 32);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = " 保存";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(243, 3);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 32);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = " 删除";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnImportETSProject
            // 
            this.btnImportETSProject.Location = new System.Drawing.Point(323, 3);
            this.btnImportETSProject.Margin = new System.Windows.Forms.Padding(0);
            this.btnImportETSProject.Name = "btnImportETSProject";
            this.btnImportETSProject.Size = new System.Drawing.Size(105, 32);
            this.btnImportETSProject.TabIndex = 6;
            this.btnImportETSProject.Text = " 导入ETS项目";
            this.btnImportETSProject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImportETSProject.UseVisualStyleBackColor = true;
            this.btnImportETSProject.Click += new System.EventHandler(this.btnImportETSProject_Click);
            // 
            // btnImportETSAddressXML
            // 
            this.btnImportETSAddressXML.Location = new System.Drawing.Point(428, 3);
            this.btnImportETSAddressXML.Margin = new System.Windows.Forms.Padding(0);
            this.btnImportETSAddressXML.Name = "btnImportETSAddressXML";
            this.btnImportETSAddressXML.Size = new System.Drawing.Size(105, 32);
            this.btnImportETSAddressXML.TabIndex = 7;
            this.btnImportETSAddressXML.Text = " 导入ETS地址";
            this.btnImportETSAddressXML.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImportETSAddressXML.UseVisualStyleBackColor = true;
            this.btnImportETSAddressXML.Click += new System.EventHandler(this.btnImportETSAddressXML_Click);
            // 
            // palContent
            // 
            this.palContent.Controls.Add(this.dgvGroupAddress);
            this.palContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palContent.Location = new System.Drawing.Point(0, 40);
            this.palContent.Name = "palContent";
            this.palContent.Size = new System.Drawing.Size(1284, 508);
            this.palContent.TabIndex = 2;
            // 
            // flpSearch
            // 
            this.flpSearch.Controls.Add(this.txtSearch);
            this.flpSearch.Controls.Add(this.btnFilter);
            this.flpSearch.Controls.Add(this.btnClear);
            this.flpSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpSearch.Location = new System.Drawing.Point(0, 548);
            this.flpSearch.Name = "flpSearch";
            this.flpSearch.Padding = new System.Windows.Forms.Padding(0, 1, 2, 0);
            this.flpSearch.Size = new System.Drawing.Size(1284, 32);
            this.flpSearch.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(3, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(150, 21);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFilter.Location = new System.Drawing.Point(156, 4);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(60, 23);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "过滤";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnClear.Location = new System.Drawing.Point(219, 4);
            this.btnClear.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(60, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmsDgvGroupAddress
            // 
            this.cmsDgvGroupAddress.Name = "contextMenuStrip1";
            this.cmsDgvGroupAddress.ShowImageMargin = false;
            this.cmsDgvGroupAddress.Size = new System.Drawing.Size(36, 4);
            // 
            // FrmGroupAddressMgt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 580);
            this.Controls.Add(this.palContent);
            this.Controls.Add(this.palTop);
            this.Controls.Add(this.flpSearch);
            this.Name = "FrmGroupAddressMgt";
            this.Text = "KNX 组地址管理界面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMgrGroupAddresses_FormClosing);
            this.Load += new System.EventHandler(this.FrmGroupAddressMgt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupAddress)).EndInit();
            this.palTop.ResumeLayout(false);
            this.palTop.PerformLayout();
            this.flpToolbar.ResumeLayout(false);
            this.palContent.ResumeLayout(false);
            this.flpSearch.ResumeLayout(false);
            this.flpSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGroupAddress;
        private System.Windows.Forms.Panel palTop;
        private System.Windows.Forms.FlowLayoutPanel flpToolbar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel palContent;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnImportETSProject;
        private System.Windows.Forms.Button btnImportETSAddressXML;
        private System.Windows.Forms.FlowLayoutPanel flpSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ContextMenuStrip cmsDgvGroupAddress;

    }
}