using UIEditor.Component;
namespace UIEditor
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ucdo = new UIEditor.UserUIControl.UCDocumentOutline();
            this.ucProperty = new UIEditor.UserUIControl.UCProperty();
            this.mnsMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLanguange = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_en_US = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_zh_CN = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.tsslblProjectName = new System.Windows.Forms.ToolStripStatusLabel();
            this.flpMainTools = new System.Windows.Forms.FlowLayoutPanel();
            this.tsrProject = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNewProj = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenProj = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveProj = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbKNXAddr = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.tsrAddControlToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsrBtnGroupBox = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddBlinds = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddLabel = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddSceneButton = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddSliderSwitch = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddSwitch = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddValueDisplay = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddTimerButton = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnDigitalAdjustment = new System.Windows.Forms.ToolStripButton();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.mnsMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.flpMainTools.SuspendLayout();
            this.tsrProject.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tsrAddControlToolBar.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BackColor = System.Drawing.Color.Coral;
            this.splitContainer1.ForeColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            // 
            // tabControl
            // 
            this.tabControl.AllowDrop = true;
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.HotTrack = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            this.tabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl_MouseDown);
            this.tabControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabControl_MouseMove);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ucdo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucProperty);
            // 
            // ucdo
            // 
            this.ucdo.cqdo = null;
            resources.ApplyResources(this.ucdo, "ucdo");
            this.ucdo.Name = "ucdo";
            this.ucdo.Title = "";
            // 
            // ucProperty
            // 
            this.ucProperty.cqp = null;
            resources.ApplyResources(this.ucProperty, "ucProperty");
            this.ucProperty.Name = "ucProperty";
            // 
            // mnsMain
            // 
            this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiLanguange,
            this.tsmiHelp});
            resources.ApplyResources(this.mnsMain, "mnsMain");
            this.mnsMain.Name = "mnsMain";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.tsmiOpen,
            this.tsmiClose,
            this.toolStripSeparator,
            this.tsmiSave,
            this.tsmiSaveAs,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            resources.ApplyResources(this.tsmiFile, "tsmiFile");
            // 
            // tsmiNew
            // 
            resources.ApplyResources(this.tsmiNew, "tsmiNew");
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.Click += new System.EventHandler(this.tsmiNew_Click);
            // 
            // tsmiOpen
            // 
            resources.ApplyResources(this.tsmiOpen, "tsmiOpen");
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Name = "tsmiClose";
            resources.ApplyResources(this.tsmiClose, "tsmiClose");
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            // 
            // tsmiSave
            // 
            resources.ApplyResources(this.tsmiSave, "tsmiSave");
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiSaveAs
            // 
            this.tsmiSaveAs.Name = "tsmiSaveAs";
            resources.ApplyResources(this.tsmiSaveAs, "tsmiSaveAs");
            this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            resources.ApplyResources(this.tsmiExit, "tsmiExit");
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiLanguange
            // 
            this.tsmiLanguange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_en_US,
            this.tsmi_zh_CN});
            this.tsmiLanguange.Name = "tsmiLanguange";
            resources.ApplyResources(this.tsmiLanguange, "tsmiLanguange");
            // 
            // tsmi_en_US
            // 
            this.tsmi_en_US.Image = global::UIEditor.Properties.Resources.America_flag_16x16;
            resources.ApplyResources(this.tsmi_en_US, "tsmi_en_US");
            this.tsmi_en_US.Name = "tsmi_en_US";
            this.tsmi_en_US.Click += new System.EventHandler(this.tsmi_en_US_Click);
            // 
            // tsmi_zh_CN
            // 
            this.tsmi_zh_CN.Image = global::UIEditor.Properties.Resources.Chinese_flag_16x16;
            resources.ApplyResources(this.tsmi_zh_CN, "tsmi_zh_CN");
            this.tsmi_zh_CN.Name = "tsmi_zh_CN";
            this.tsmi_zh_CN.Click += new System.EventHandler(this.tsmi_zh_CN_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenHelp,
            this.toolStripSeparator5,
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            resources.ApplyResources(this.tsmiHelp, "tsmiHelp");
            // 
            // tsmiOpenHelp
            // 
            resources.ApplyResources(this.tsmiOpenHelp, "tsmiOpenHelp");
            this.tsmiOpenHelp.Name = "tsmiOpenHelp";
            this.tsmiOpenHelp.Click += new System.EventHandler(this.tsmiOpenHelp_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            resources.ApplyResources(this.tsmiAbout, "tsmiAbout");
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblProjectName});
            resources.ApplyResources(this.statusStripMain, "statusStripMain");
            this.statusStripMain.Name = "statusStripMain";
            // 
            // tsslblProjectName
            // 
            this.tsslblProjectName.Name = "tsslblProjectName";
            resources.ApplyResources(this.tsslblProjectName, "tsslblProjectName");
            // 
            // flpMainTools
            // 
            resources.ApplyResources(this.flpMainTools, "flpMainTools");
            this.flpMainTools.Controls.Add(this.tsrProject);
            this.flpMainTools.Controls.Add(this.toolStrip1);
            this.flpMainTools.Controls.Add(this.tsrAddControlToolBar);
            this.flpMainTools.Name = "flpMainTools";
            // 
            // tsrProject
            // 
            this.tsrProject.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tsrProject, "tsrProject");
            this.tsrProject.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsrProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator12,
            this.tsbNewProj,
            this.tsbOpenProj,
            this.tsbSaveProj,
            this.toolStripSeparator11,
            this.tsbKNXAddr});
            this.tsrProject.Name = "tsrProject";
            this.tsrProject.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsrProject.Stretch = true;
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            resources.ApplyResources(this.toolStripSeparator12, "toolStripSeparator12");
            // 
            // tsbNewProj
            // 
            this.tsbNewProj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNewProj.Image = global::UIEditor.Properties.Resources.New_16x16;
            resources.ApplyResources(this.tsbNewProj, "tsbNewProj");
            this.tsbNewProj.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsbNewProj.Name = "tsbNewProj";
            this.tsbNewProj.Click += new System.EventHandler(this.tsbNewProj_Click);
            // 
            // tsbOpenProj
            // 
            this.tsbOpenProj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenProj.Image = global::UIEditor.Properties.Resources.Open_16x16;
            resources.ApplyResources(this.tsbOpenProj, "tsbOpenProj");
            this.tsbOpenProj.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsbOpenProj.Name = "tsbOpenProj";
            this.tsbOpenProj.Click += new System.EventHandler(this.tsbOpenProj_Click);
            // 
            // tsbSaveProj
            // 
            this.tsbSaveProj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbSaveProj, "tsbSaveProj");
            this.tsbSaveProj.Image = global::UIEditor.Properties.Resources.Save_16x16;
            this.tsbSaveProj.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsbSaveProj.Name = "tsbSaveProj";
            this.tsbSaveProj.Click += new System.EventHandler(this.tsbSaveProj_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
            // 
            // tsbKNXAddr
            // 
            this.tsbKNXAddr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbKNXAddr, "tsbKNXAddr");
            this.tsbKNXAddr.Image = global::UIEditor.Properties.Resources.ImportKNX_16x16;
            this.tsbKNXAddr.Margin = new System.Windows.Forms.Padding(0);
            this.tsbKNXAddr.Name = "tsbKNXAddr";
            this.tsbKNXAddr.Click += new System.EventHandler(this.tsbKNXAddr_Click);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator14,
            this.tsbUndo,
            this.tsbRedo});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            resources.ApplyResources(this.toolStripSeparator14, "toolStripSeparator14");
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbUndo, "tsbUndo");
            this.tsbUndo.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbRedo, "tsbRedo");
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // tsrAddControlToolBar
            // 
            resources.ApplyResources(this.tsrAddControlToolBar, "tsrAddControlToolBar");
            this.tsrAddControlToolBar.BackColor = System.Drawing.Color.Transparent;
            this.tsrAddControlToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsrAddControlToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.tsrBtnGroupBox,
            this.tsrBtnAddBlinds,
            this.tsrBtnAddLabel,
            this.tsrBtnAddSceneButton,
            this.tsrBtnAddSliderSwitch,
            this.tsrBtnAddSwitch,
            this.tsrBtnAddValueDisplay,
            this.tsrBtnAddTimerButton,
            this.tsrBtnDigitalAdjustment});
            this.tsrAddControlToolBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsrAddControlToolBar.Name = "tsrAddControlToolBar";
            this.tsrAddControlToolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // tsrBtnGroupBox
            // 
            this.tsrBtnGroupBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnGroupBox, "tsrBtnGroupBox");
            this.tsrBtnGroupBox.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsrBtnGroupBox.Name = "tsrBtnGroupBox";
            this.tsrBtnGroupBox.Click += new System.EventHandler(this.tsrBtnGroupBox_Click);
            // 
            // tsrBtnAddBlinds
            // 
            this.tsrBtnAddBlinds.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddBlinds, "tsrBtnAddBlinds");
            this.tsrBtnAddBlinds.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsrBtnAddBlinds.Name = "tsrBtnAddBlinds";
            this.tsrBtnAddBlinds.Click += new System.EventHandler(this.tsrBtnAddBlinds_Click);
            // 
            // tsrBtnAddLabel
            // 
            this.tsrBtnAddLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddLabel, "tsrBtnAddLabel");
            this.tsrBtnAddLabel.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsrBtnAddLabel.Name = "tsrBtnAddLabel";
            this.tsrBtnAddLabel.Click += new System.EventHandler(this.tsrBtnAddLabel_Click);
            // 
            // tsrBtnAddSceneButton
            // 
            this.tsrBtnAddSceneButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddSceneButton, "tsrBtnAddSceneButton");
            this.tsrBtnAddSceneButton.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsrBtnAddSceneButton.Name = "tsrBtnAddSceneButton";
            this.tsrBtnAddSceneButton.Click += new System.EventHandler(this.tsrBtnAddSceneButton_Click);
            // 
            // tsrBtnAddSliderSwitch
            // 
            this.tsrBtnAddSliderSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddSliderSwitch, "tsrBtnAddSliderSwitch");
            this.tsrBtnAddSliderSwitch.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsrBtnAddSliderSwitch.Name = "tsrBtnAddSliderSwitch";
            this.tsrBtnAddSliderSwitch.Click += new System.EventHandler(this.tsrBtnAddSliderSwitch_Click);
            // 
            // tsrBtnAddSwitch
            // 
            this.tsrBtnAddSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddSwitch, "tsrBtnAddSwitch");
            this.tsrBtnAddSwitch.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsrBtnAddSwitch.Name = "tsrBtnAddSwitch";
            this.tsrBtnAddSwitch.Click += new System.EventHandler(this.tsrBtnAddSwitch_Click);
            // 
            // tsrBtnAddValueDisplay
            // 
            this.tsrBtnAddValueDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddValueDisplay, "tsrBtnAddValueDisplay");
            this.tsrBtnAddValueDisplay.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsrBtnAddValueDisplay.Name = "tsrBtnAddValueDisplay";
            this.tsrBtnAddValueDisplay.Click += new System.EventHandler(this.tsrBtnAddValueDisplay_Click);
            // 
            // tsrBtnAddTimerButton
            // 
            this.tsrBtnAddTimerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddTimerButton, "tsrBtnAddTimerButton");
            this.tsrBtnAddTimerButton.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tsrBtnAddTimerButton.Name = "tsrBtnAddTimerButton";
            this.tsrBtnAddTimerButton.Click += new System.EventHandler(this.tsrBtnAddTimerButton_Click);
            // 
            // tsrBtnDigitalAdjustment
            // 
            this.tsrBtnDigitalAdjustment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnDigitalAdjustment, "tsrBtnDigitalAdjustment");
            this.tsrBtnDigitalAdjustment.Margin = new System.Windows.Forms.Padding(0);
            this.tsrBtnDigitalAdjustment.Name = "tsrBtnDigitalAdjustment";
            this.tsrBtnDigitalAdjustment.Click += new System.EventHandler(this.tsrBtnDigitalAdjustment_Click);
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.BackColor = System.Drawing.SystemColors.Control;
            this.tlpMain.Controls.Add(this.flpMainTools, 0, 0);
            this.tlpMain.Controls.Add(this.splitContainer1, 0, 1);
            this.tlpMain.Name = "tlpMain";
            // 
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.mnsMain);
            this.MainMenuStrip = this.mnsMain;
            this.Name = "FrmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.mnsMain.ResumeLayout(false);
            this.mnsMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.flpMainTools.ResumeLayout(false);
            this.flpMainTools.PerformLayout();
            this.tsrProject.ResumeLayout(false);
            this.tsrProject.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tsrAddControlToolBar.ResumeLayout(false);
            this.tsrAddControlToolBar.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.MenuStrip mnsMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiNew;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.ToolStripStatusLabel tsslblProjectName;
        //private System.Windows.Forms.ToolStripButton tsrBtnAddImageButton;
        //private System.Windows.Forms.ToolStripButton tsrBtnAddMediaButton;
        //private System.Windows.Forms.ToolStripButton tsrBtnAddColorLight;
        //private System.Windows.Forms.ToolStripButton tsrBtnAddSIPCall;
        //private System.Windows.Forms.ToolStripButton tsrBtnAddSlider;
        //private System.Windows.Forms.ToolStripButton tsrBtnAddSnapper;
        //private System.Windows.Forms.ToolStripButton tsrBtnAddSnapperSwitch;
        //private System.Windows.Forms.ToolStripButton tsrBtnAddWebcam;
        //private System.Windows.Forms.ToolStripButton tsrBtnAddTimerTaskListView;
        private System.Windows.Forms.FlowLayoutPanel flpMainTools;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ToolStrip tsrProject;
        private System.Windows.Forms.ToolStripButton tsbNewProj;
        private System.Windows.Forms.ToolStripButton tsbOpenProj;
        private System.Windows.Forms.ToolStripButton tsbSaveProj;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton tsbKNXAddr;
        private System.Windows.Forms.ToolStrip tsrAddControlToolBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsrBtnGroupBox;
        private System.Windows.Forms.ToolStripButton tsrBtnAddBlinds;
        private System.Windows.Forms.ToolStripButton tsrBtnAddLabel;
        private System.Windows.Forms.ToolStripButton tsrBtnAddSceneButton;
        private System.Windows.Forms.ToolStripButton tsrBtnAddSliderSwitch;
        private System.Windows.Forms.ToolStripButton tsrBtnAddSwitch;
        private System.Windows.Forms.ToolStripButton tsrBtnAddValueDisplay;
        private System.Windows.Forms.ToolStripButton tsrBtnAddTimerButton;
        private System.Windows.Forms.ToolStripButton tsrBtnDigitalAdjustment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem tsmiLanguange;
        private System.Windows.Forms.ToolStripMenuItem tsmi_en_US;
        private System.Windows.Forms.ToolStripMenuItem tsmi_zh_CN;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private UserUIControl.UCDocumentOutline ucdo;
        private UserUIControl.UCProperty ucProperty;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.TabControl tabControl;
        //private System.Windows.Forms.ToolStripButton tsrBtnRadioGroup;

    }
}