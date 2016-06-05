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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonImportETSAddressXML = new System.Windows.Forms.Button();
            this.buttonImportETSProject = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.backWorkerImport = new System.ComponentModel.BackgroundWorker();
            this.backWorkerSave = new System.ComponentModel.BackgroundWorker();
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
            this.panel1.Controls.Add(this.buttonImportETSAddressXML);
            this.panel1.Controls.Add(this.buttonImportETSProject);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonFinish);
            this.panel1.Controls.Add(this.buttonBack);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // buttonImportETSAddressXML
            // 
            resources.ApplyResources(this.buttonImportETSAddressXML, "buttonImportETSAddressXML");
            this.buttonImportETSAddressXML.Name = "buttonImportETSAddressXML";
            this.buttonImportETSAddressXML.UseVisualStyleBackColor = true;
            this.buttonImportETSAddressXML.Click += new System.EventHandler(this.buttonImportETSAddressXML_Click);
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
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
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
            // backWorkerImport
            // 
            this.backWorkerImport.WorkerReportsProgress = true;
            this.backWorkerImport.WorkerSupportsCancellation = true;
            this.backWorkerImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backWorkerImport_DoWork);
            this.backWorkerImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backWorkerImport_RunWorkerCompleted);
            // 
            // backWorkerSave
            // 
            this.backWorkerSave.WorkerReportsProgress = true;
            this.backWorkerSave.WorkerSupportsCancellation = true;
            this.backWorkerSave.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backWorkerSave_DoWork);
            this.backWorkerSave.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backWorkerSave_RunWorkerCompleted);
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
        private System.ComponentModel.BackgroundWorker backWorkerImport;
        private System.Windows.Forms.Button buttonImportETSAddressXML;
        private System.Windows.Forms.Button buttonImportETSProject;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.ComponentModel.BackgroundWorker backWorkerSave;
    }
}