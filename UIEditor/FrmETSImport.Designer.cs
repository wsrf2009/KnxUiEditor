namespace UIEditor
{
    partial class FrmETSImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmETSImport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFilter = new System.Windows.Forms.Button();
            this.tbFilterText = new System.Windows.Forms.TextBox();
            this.cbbFilterType = new System.Windows.Forms.ComboBox();
            this.buttonImportOPC = new System.Windows.Forms.Button();
            this.buttonImportETSProject = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.backWorkerImportEtsProject = new System.ComponentModel.BackgroundWorker();
            this.backWorkerSave = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerImportOPC = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFilter);
            this.panel1.Controls.Add(this.tbFilterText);
            this.panel1.Controls.Add(this.cbbFilterType);
            this.panel1.Controls.Add(this.buttonImportOPC);
            this.panel1.Controls.Add(this.buttonImportETSProject);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonFinish);
            this.panel1.Controls.Add(this.buttonBack);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnFilter
            // 
            resources.ApplyResources(this.btnFilter, "btnFilter");
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // tbFilterText
            // 
            resources.ApplyResources(this.tbFilterText, "tbFilterText");
            this.tbFilterText.Name = "tbFilterText";
            this.tbFilterText.TextChanged += new System.EventHandler(this.tbFilterText_TextChanged);
            // 
            // cbbFilterType
            // 
            this.cbbFilterType.FormattingEnabled = true;
            resources.ApplyResources(this.cbbFilterType, "cbbFilterType");
            this.cbbFilterType.Name = "cbbFilterType";
            this.cbbFilterType.SelectedIndexChanged += new System.EventHandler(this.cbbFilterType_SelectedIndexChanged);
            // 
            // buttonImportOPC
            // 
            resources.ApplyResources(this.buttonImportOPC, "buttonImportOPC");
            this.buttonImportOPC.Name = "buttonImportOPC";
            this.buttonImportOPC.UseVisualStyleBackColor = true;
            this.buttonImportOPC.Click += new System.EventHandler(this.buttonImportOPC_Click);
            // 
            // buttonImportETSProject
            // 
            resources.ApplyResources(this.buttonImportETSProject, "buttonImportETSProject");
            this.buttonImportETSProject.Name = "buttonImportETSProject";
            this.buttonImportETSProject.UseVisualStyleBackColor = true;
            this.buttonImportETSProject.Click += new System.EventHandler(this.buttonImportETSProject_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonFinish
            // 
            resources.ApplyResources(this.buttonFinish, "buttonFinish");
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // buttonBack
            // 
            resources.ApplyResources(this.buttonBack, "buttonBack");
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            resources.ApplyResources(this.dataGridView, "dataGridView");
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.StandardTab = true;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting);
            this.dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_ColumnHeaderMouseClick);
            this.dataGridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView_ColumnWidthChanged);
            this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            this.dataGridView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView_Scroll);
            this.dataGridView.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseWheel);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label);
            this.panel3.Controls.Add(this.pictureBox);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // label
            // 
            resources.ApplyResources(this.label, "label");
            this.label.Name = "label";
            // 
            // pictureBox
            // 
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // backWorkerImportEtsProject
            // 
            this.backWorkerImportEtsProject.WorkerReportsProgress = true;
            this.backWorkerImportEtsProject.WorkerSupportsCancellation = true;
            this.backWorkerImportEtsProject.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backWorkerImport_DoWork);
            this.backWorkerImportEtsProject.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backWorkerImport_RunWorkerCompleted);
            // 
            // backWorkerSave
            // 
            this.backWorkerSave.WorkerReportsProgress = true;
            this.backWorkerSave.WorkerSupportsCancellation = true;
            this.backWorkerSave.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backWorkerSave_DoWork);
            this.backWorkerSave.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backWorkerSave_RunWorkerCompleted);
            // 
            // backgroundWorkerImportOPC
            // 
            this.backgroundWorkerImportOPC.WorkerReportsProgress = true;
            this.backgroundWorkerImportOPC.WorkerSupportsCancellation = true;
            this.backgroundWorkerImportOPC.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerImportOPC_DoWork);
            this.backgroundWorkerImportOPC.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerImportOPC_RunWorkerCompleted);
            // 
            // FrmETSImport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmETSImport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmETSImport_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.ComponentModel.BackgroundWorker backWorkerImportEtsProject;
        private System.Windows.Forms.Button buttonImportOPC;
        private System.Windows.Forms.Button buttonImportETSProject;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.ComponentModel.BackgroundWorker backWorkerSave;
        private System.ComponentModel.BackgroundWorker backgroundWorkerImportOPC;
        private System.Windows.Forms.TextBox tbFilterText;
        private System.Windows.Forms.ComboBox cbbFilterType;
        private System.Windows.Forms.Button btnFilter;
    }
}