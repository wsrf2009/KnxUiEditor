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
<<<<<<< HEAD
            this.ucdo = new UIEditor.UserUIControl.UCDocumentOutline();
            this.ucProperty = new UIEditor.UserUIControl.UCProperty();
=======
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucpm = new UIEditor.UserUIControl.UCProjectManager();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucpo = new UIEditor.UserUIControl.UCPageOutline();
            this.ucp = new UIEditor.UserUIControl.UCProperty();
>>>>>>> SationKNXUIEditor-Modify
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
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKNX = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKNXAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAlignLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAlignRight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAlignTop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAlignBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAlignHorizontalCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAlignVerticalCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiHorizontalEquidistanceAlignment = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVerticalEquidistanceAlignment = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiWidthAlignment = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHeightAlignment = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCenterHorizontalInParent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCenterVerticalInParent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiComponet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiArea = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiGroupBox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBlinds = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiScene = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSliderSwitch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSwitch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiValueDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTimer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDigitalAdjustment = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImageButton = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdaptiveScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRuler = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLanguange = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_en_US = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_zh_CN = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCheckUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.flpMainTools = new System.Windows.Forms.FlowLayoutPanel();
            this.tsFile = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNewProj = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenProj = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveProj = new System.Windows.Forms.ToolStripButton();
            this.tsEdit = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.tsKNX = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbKNXAddr = new System.Windows.Forms.ToolStripButton();
            this.tsComponents = new System.Windows.Forms.ToolStrip();
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
            this.tsrBtnImageButton = new System.Windows.Forms.ToolStripButton();
            this.tsbShutter = new System.Windows.Forms.ToolStripButton();
            this.tsbDimmer = new System.Windows.Forms.ToolStripButton();
            this.tsbWebCamer = new System.Windows.Forms.ToolStripButton();
            this.tsbMediaButton = new System.Windows.Forms.ToolStripButton();
            this.tsbAirCondition = new System.Windows.Forms.ToolStripButton();
            this.tsbHVAC = new System.Windows.Forms.ToolStripButton();
            this.tsLayout = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAlignLeft = new System.Windows.Forms.ToolStripButton();
            this.tsbAlignRight = new System.Windows.Forms.ToolStripButton();
            this.tsbAlignTop = new System.Windows.Forms.ToolStripButton();
            this.tsbAlignBottom = new System.Windows.Forms.ToolStripButton();
            this.tsbAlignHorizontalCenter = new System.Windows.Forms.ToolStripButton();
            this.tsbAlignVerticalCenter = new System.Windows.Forms.ToolStripButton();
            this.tsbHorizontalEquidistanceAlignment = new System.Windows.Forms.ToolStripButton();
            this.tsbVerticalEquidistanceAlignment = new System.Windows.Forms.ToolStripButton();
            this.tsbWidthAlignment = new System.Windows.Forms.ToolStripButton();
            this.tsbHeightAlignment = new System.Windows.Forms.ToolStripButton();
            this.tsbCenterHorizontalInParent = new System.Windows.Forms.ToolStripButton();
            this.tsbCenterVerticalInParent = new System.Windows.Forms.ToolStripButton();
            this.tsView = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tscbViewScale = new System.Windows.Forms.ToolStripComboBox();
            this.tsbAdaptiveScreen = new System.Windows.Forms.ToolStripButton();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
<<<<<<< HEAD
=======
            this.BGWOpenProject = new System.ComponentModel.BackgroundWorker();
            this.BGWSaveProject = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslUpdate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbUpdate = new System.Windows.Forms.ToolStripProgressBar();
>>>>>>> SationKNXUIEditor-Modify
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.mnsMain.SuspendLayout();
            this.flpMainTools.SuspendLayout();
            this.tsFile.SuspendLayout();
            this.tsEdit.SuspendLayout();
            this.tsKNX.SuspendLayout();
            this.tsComponents.SuspendLayout();
            this.tsLayout.SuspendLayout();
            this.tsView.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.LightSlateGray;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.ForeColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
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
            this.splitContainer2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer2.Panel1.ForeColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucp);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.ucpm);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // ucpm
            // 
            this.ucpm.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.ucpm, "ucpm");
            this.ucpm.Name = "ucpm";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucpo);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucpo
            // 
            resources.ApplyResources(this.ucpo, "ucpo");
            this.ucpo.BackColor = System.Drawing.Color.Transparent;
            this.ucpo.Name = "ucpo";
            // 
            // ucp
            // 
            resources.ApplyResources(this.ucp, "ucp");
            this.ucp.Name = "ucp";
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
            this.mnsMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            resources.ApplyResources(this.mnsMain, "mnsMain");
            this.mnsMain.GripMargin = new System.Windows.Forms.Padding(2, 1, 0, 0);
            this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiEdit,
            this.tsmiKNX,
            this.tsmiLayout,
            this.tsmiComponet,
            this.tsmiView,
            this.tsmiLanguange,
            this.tsmiHelp});
            this.mnsMain.Name = "mnsMain";
            this.mnsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
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
            resources.ApplyResources(this.tsmiFile, "tsmiFile");
            this.tsmiFile.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Padding = new System.Windows.Forms.Padding(0);
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
            resources.ApplyResources(this.tsmiClose, "tsmiClose");
<<<<<<< HEAD
=======
            this.tsmiClose.Name = "tsmiClose";
>>>>>>> SationKNXUIEditor-Modify
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
            resources.ApplyResources(this.tsmiSaveAs, "tsmiSaveAs");
<<<<<<< HEAD
=======
            this.tsmiSaveAs.Name = "tsmiSaveAs";
>>>>>>> SationKNXUIEditor-Modify
            this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsmiExit
<<<<<<< HEAD
            // 
            this.tsmiExit.Name = "tsmiExit";
            resources.ApplyResources(this.tsmiExit, "tsmiExit");
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
=======
            // 
            resources.ApplyResources(this.tsmiExit, "tsmiExit");
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUndo,
            this.tsmiRedo,
            this.toolStripSeparator3,
            this.tsmiCut,
            this.tsmiCopy,
            this.tsmiPaste});
            this.tsmiEdit.Name = "tsmiEdit";
            resources.ApplyResources(this.tsmiEdit, "tsmiEdit");
            // 
            // tsmiUndo
            // 
            resources.ApplyResources(this.tsmiUndo, "tsmiUndo");
            this.tsmiUndo.Name = "tsmiUndo";
            this.tsmiUndo.Click += new System.EventHandler(this.tsmiUndo_Click);
            // 
            // tsmiRedo
            // 
            resources.ApplyResources(this.tsmiRedo, "tsmiRedo");
            this.tsmiRedo.Name = "tsmiRedo";
            this.tsmiRedo.Click += new System.EventHandler(this.tsmiRedo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tsmiCut
            // 
            resources.ApplyResources(this.tsmiCut, "tsmiCut");
            this.tsmiCut.Name = "tsmiCut";
            this.tsmiCut.Click += new System.EventHandler(this.tsmiCut_Click);
            // 
            // tsmiCopy
            // 
            resources.ApplyResources(this.tsmiCopy, "tsmiCopy");
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiPaste
            // 
            resources.ApplyResources(this.tsmiPaste, "tsmiPaste");
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // tsmiKNX
            // 
            this.tsmiKNX.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKNXAddress});
            this.tsmiKNX.Name = "tsmiKNX";
            resources.ApplyResources(this.tsmiKNX, "tsmiKNX");
            // 
            // tsmiKNXAddress
            // 
            resources.ApplyResources(this.tsmiKNXAddress, "tsmiKNXAddress");
            this.tsmiKNXAddress.Name = "tsmiKNXAddress";
            this.tsmiKNXAddress.Click += new System.EventHandler(this.tsmiKNXAddress_Click);
            // 
            // tsmiLayout
            // 
            this.tsmiLayout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAlignLeft,
            this.tsmiAlignRight,
            this.tsmiAlignTop,
            this.tsmiAlignBottom,
            this.toolStripSeparator6,
            this.tsmiAlignHorizontalCenter,
            this.tsmiAlignVerticalCenter,
            this.toolStripSeparator7,
            this.tsmiHorizontalEquidistanceAlignment,
            this.tsmiVerticalEquidistanceAlignment,
            this.toolStripSeparator8,
            this.tsmiWidthAlignment,
            this.tsmiHeightAlignment,
            this.toolStripSeparator15,
            this.tsmiCenterHorizontalInParent,
            this.tsmiCenterVerticalInParent});
            this.tsmiLayout.Name = "tsmiLayout";
            resources.ApplyResources(this.tsmiLayout, "tsmiLayout");
            // 
            // tsmiAlignLeft
            // 
            resources.ApplyResources(this.tsmiAlignLeft, "tsmiAlignLeft");
            this.tsmiAlignLeft.Name = "tsmiAlignLeft";
            this.tsmiAlignLeft.Click += new System.EventHandler(this.tsmiAlignLeft_Click);
            // 
            // tsmiAlignRight
            // 
            resources.ApplyResources(this.tsmiAlignRight, "tsmiAlignRight");
            this.tsmiAlignRight.Name = "tsmiAlignRight";
            this.tsmiAlignRight.Click += new System.EventHandler(this.tsmiAlignRight_Click);
            // 
            // tsmiAlignTop
            // 
            resources.ApplyResources(this.tsmiAlignTop, "tsmiAlignTop");
            this.tsmiAlignTop.Name = "tsmiAlignTop";
            this.tsmiAlignTop.Click += new System.EventHandler(this.tsmiAlignTop_Click);
            // 
            // tsmiAlignBottom
            // 
            resources.ApplyResources(this.tsmiAlignBottom, "tsmiAlignBottom");
            this.tsmiAlignBottom.Name = "tsmiAlignBottom";
            this.tsmiAlignBottom.Click += new System.EventHandler(this.tsmiAlignBottom_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // tsmiAlignHorizontalCenter
            // 
            resources.ApplyResources(this.tsmiAlignHorizontalCenter, "tsmiAlignHorizontalCenter");
            this.tsmiAlignHorizontalCenter.Name = "tsmiAlignHorizontalCenter";
            this.tsmiAlignHorizontalCenter.Click += new System.EventHandler(this.tsmiAlignHorizontalCenter_Click);
            // 
            // tsmiAlignVerticalCenter
            // 
            resources.ApplyResources(this.tsmiAlignVerticalCenter, "tsmiAlignVerticalCenter");
            this.tsmiAlignVerticalCenter.Name = "tsmiAlignVerticalCenter";
            this.tsmiAlignVerticalCenter.Click += new System.EventHandler(this.tsmiAlignVerticalCenter_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // tsmiHorizontalEquidistanceAlignment
            // 
            resources.ApplyResources(this.tsmiHorizontalEquidistanceAlignment, "tsmiHorizontalEquidistanceAlignment");
            this.tsmiHorizontalEquidistanceAlignment.Name = "tsmiHorizontalEquidistanceAlignment";
            this.tsmiHorizontalEquidistanceAlignment.Click += new System.EventHandler(this.tsmiHorizontalEquidistanceAlignment_Click);
            // 
            // tsmiVerticalEquidistanceAlignment
            // 
            resources.ApplyResources(this.tsmiVerticalEquidistanceAlignment, "tsmiVerticalEquidistanceAlignment");
            this.tsmiVerticalEquidistanceAlignment.Name = "tsmiVerticalEquidistanceAlignment";
            this.tsmiVerticalEquidistanceAlignment.Click += new System.EventHandler(this.tsmiVerticalEquidistanceAlignment_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // tsmiWidthAlignment
            // 
            resources.ApplyResources(this.tsmiWidthAlignment, "tsmiWidthAlignment");
            this.tsmiWidthAlignment.Image = global::UIEditor.Properties.Resources.WidthAlignment;
            this.tsmiWidthAlignment.Name = "tsmiWidthAlignment";
            this.tsmiWidthAlignment.Click += new System.EventHandler(this.tsmiWidthAlignment_Click);
            // 
            // tsmiHeightAlignment
            // 
            resources.ApplyResources(this.tsmiHeightAlignment, "tsmiHeightAlignment");
            this.tsmiHeightAlignment.Image = global::UIEditor.Properties.Resources.HeightAlignment;
            this.tsmiHeightAlignment.Name = "tsmiHeightAlignment";
            this.tsmiHeightAlignment.Click += new System.EventHandler(this.tsmiHeightAlignment_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            resources.ApplyResources(this.toolStripSeparator15, "toolStripSeparator15");
            // 
            // tsmiCenterHorizontalInParent
            // 
            resources.ApplyResources(this.tsmiCenterHorizontalInParent, "tsmiCenterHorizontalInParent");
            this.tsmiCenterHorizontalInParent.Image = global::UIEditor.Properties.Resources.CenterHorizontalInParent;
            this.tsmiCenterHorizontalInParent.Name = "tsmiCenterHorizontalInParent";
            this.tsmiCenterHorizontalInParent.Click += new System.EventHandler(this.tsmiCenterHorizontal_Click);
            // 
            // tsmiCenterVerticalInParent
            // 
            resources.ApplyResources(this.tsmiCenterVerticalInParent, "tsmiCenterVerticalInParent");
            this.tsmiCenterVerticalInParent.Image = global::UIEditor.Properties.Resources.CenterVerticalInParent;
            this.tsmiCenterVerticalInParent.Name = "tsmiCenterVerticalInParent";
            this.tsmiCenterVerticalInParent.Click += new System.EventHandler(this.tsmiCenterVertical_Click);
            // 
            // tsmiComponet
            // 
            this.tsmiComponet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiArea,
            this.toolStripSeparator9,
            this.tsmiRoom,
            this.toolStripSeparator10,
            this.tsmiPage,
            this.toolStripSeparator11,
            this.tsmiGroupBox,
            this.tsmiBlinds,
            this.tsmiLabel,
            this.tsmiScene,
            this.tsmiSliderSwitch,
            this.tsmiSwitch,
            this.tsmiValueDisplay,
            this.tsmiTimer,
            this.tsmiDigitalAdjustment,
            this.tsmiImageButton});
            this.tsmiComponet.Name = "tsmiComponet";
            resources.ApplyResources(this.tsmiComponet, "tsmiComponet");
            // 
            // tsmiArea
            // 
            resources.ApplyResources(this.tsmiArea, "tsmiArea");
            this.tsmiArea.Name = "tsmiArea";
            this.tsmiArea.Click += new System.EventHandler(this.tsmiArea_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            // 
            // tsmiRoom
            // 
            resources.ApplyResources(this.tsmiRoom, "tsmiRoom");
            this.tsmiRoom.Name = "tsmiRoom";
            this.tsmiRoom.Click += new System.EventHandler(this.tsmiRoom_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            // 
            // tsmiPage
            // 
            resources.ApplyResources(this.tsmiPage, "tsmiPage");
            this.tsmiPage.Name = "tsmiPage";
            this.tsmiPage.Click += new System.EventHandler(this.tsmiPage_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
            // 
            // tsmiGroupBox
            // 
            resources.ApplyResources(this.tsmiGroupBox, "tsmiGroupBox");
            this.tsmiGroupBox.Name = "tsmiGroupBox";
            this.tsmiGroupBox.Click += new System.EventHandler(this.tsmiGroupBox_Click);
            // 
            // tsmiBlinds
            // 
            resources.ApplyResources(this.tsmiBlinds, "tsmiBlinds");
            this.tsmiBlinds.Name = "tsmiBlinds";
            this.tsmiBlinds.Click += new System.EventHandler(this.tsmiBlinds_Click);
            // 
            // tsmiLabel
            // 
            resources.ApplyResources(this.tsmiLabel, "tsmiLabel");
            this.tsmiLabel.Name = "tsmiLabel";
            this.tsmiLabel.Click += new System.EventHandler(this.tsmiLabel_Click);
            // 
            // tsmiScene
            // 
            resources.ApplyResources(this.tsmiScene, "tsmiScene");
            this.tsmiScene.Name = "tsmiScene";
            this.tsmiScene.Click += new System.EventHandler(this.tsmiScene_Click);
            // 
            // tsmiSliderSwitch
            // 
            resources.ApplyResources(this.tsmiSliderSwitch, "tsmiSliderSwitch");
            this.tsmiSliderSwitch.Name = "tsmiSliderSwitch";
            this.tsmiSliderSwitch.Click += new System.EventHandler(this.tsmiSliderSwitch_Click);
            // 
            // tsmiSwitch
            // 
            resources.ApplyResources(this.tsmiSwitch, "tsmiSwitch");
            this.tsmiSwitch.Name = "tsmiSwitch";
            this.tsmiSwitch.Click += new System.EventHandler(this.tsmiSwitch_Click);
            // 
            // tsmiValueDisplay
            // 
            resources.ApplyResources(this.tsmiValueDisplay, "tsmiValueDisplay");
            this.tsmiValueDisplay.Name = "tsmiValueDisplay";
            this.tsmiValueDisplay.Click += new System.EventHandler(this.tsmiValueDisplay_Click);
            // 
            // tsmiTimer
            // 
            resources.ApplyResources(this.tsmiTimer, "tsmiTimer");
            this.tsmiTimer.Name = "tsmiTimer";
            this.tsmiTimer.Click += new System.EventHandler(this.tsmiTimer_Click);
            // 
            // tsmiDigitalAdjustment
            // 
            resources.ApplyResources(this.tsmiDigitalAdjustment, "tsmiDigitalAdjustment");
            this.tsmiDigitalAdjustment.Name = "tsmiDigitalAdjustment";
            this.tsmiDigitalAdjustment.Click += new System.EventHandler(this.tsmiDigitalAdjustment_Click);
            // 
            // tsmiImageButton
            // 
            resources.ApplyResources(this.tsmiImageButton, "tsmiImageButton");
            this.tsmiImageButton.Name = "tsmiImageButton";
            this.tsmiImageButton.Click += new System.EventHandler(this.tsmiImageButton_Click);
            // 
            // tsmiView
            // 
            this.tsmiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiZoomIn,
            this.tsmiZoomOut,
            this.tsmiAdaptiveScreen,
            this.toolStripSeparator13,
            this.tsmiRuler});
            this.tsmiView.Name = "tsmiView";
            resources.ApplyResources(this.tsmiView, "tsmiView");
            // 
            // tsmiZoomIn
            // 
            resources.ApplyResources(this.tsmiZoomIn, "tsmiZoomIn");
            this.tsmiZoomIn.Name = "tsmiZoomIn";
            this.tsmiZoomIn.Click += new System.EventHandler(this.tsmiZoomIn_Click);
            // 
            // tsmiZoomOut
            // 
            resources.ApplyResources(this.tsmiZoomOut, "tsmiZoomOut");
            this.tsmiZoomOut.Name = "tsmiZoomOut";
            this.tsmiZoomOut.Click += new System.EventHandler(this.tsmiZoomOut_Click);
            // 
            // tsmiAdaptiveScreen
            // 
            resources.ApplyResources(this.tsmiAdaptiveScreen, "tsmiAdaptiveScreen");
            this.tsmiAdaptiveScreen.Name = "tsmiAdaptiveScreen";
            this.tsmiAdaptiveScreen.Click += new System.EventHandler(this.tsmiAdaptiveScreen_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            resources.ApplyResources(this.toolStripSeparator13, "toolStripSeparator13");
            // 
            // tsmiRuler
            // 
            this.tsmiRuler.Name = "tsmiRuler";
            resources.ApplyResources(this.tsmiRuler, "tsmiRuler");
            this.tsmiRuler.Click += new System.EventHandler(this.tsmiRuler_Click);
>>>>>>> SationKNXUIEditor-Modify
            // 
            // tsmiLanguange
            // 
            this.tsmiLanguange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_en_US,
            this.tsmi_zh_CN});
            this.tsmiLanguange.ForeColor = System.Drawing.Color.Black;
            this.tsmiLanguange.Name = "tsmiLanguange";
            resources.ApplyResources(this.tsmiLanguange, "tsmiLanguange");
            // 
            // tsmi_en_US
            // 
            resources.ApplyResources(this.tsmi_en_US, "tsmi_en_US");
            this.tsmi_en_US.Name = "tsmi_en_US";
            this.tsmi_en_US.Click += new System.EventHandler(this.tsmi_en_US_Click);
            // 
            // tsmi_zh_CN
            // 
            resources.ApplyResources(this.tsmi_zh_CN, "tsmi_zh_CN");
            this.tsmi_zh_CN.Name = "tsmi_zh_CN";
            this.tsmi_zh_CN.Click += new System.EventHandler(this.tsmi_zh_CN_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenHelp,
            this.toolStripSeparator5,
            this.tsmiCheckUpdate,
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
            // tsmiCheckUpdate
            // 
            this.tsmiCheckUpdate.Name = "tsmiCheckUpdate";
            resources.ApplyResources(this.tsmiCheckUpdate, "tsmiCheckUpdate");
            this.tsmiCheckUpdate.Click += new System.EventHandler(this.tsmiCheckUpdate_Click);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            resources.ApplyResources(this.tsmiAbout, "tsmiAbout");
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
<<<<<<< HEAD
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
=======
>>>>>>> SationKNXUIEditor-Modify
            // 
            // flpMainTools
            // 
            resources.ApplyResources(this.flpMainTools, "flpMainTools");
            this.flpMainTools.BackColor = System.Drawing.Color.White;
            this.flpMainTools.Controls.Add(this.tsFile);
            this.flpMainTools.Controls.Add(this.tsEdit);
            this.flpMainTools.Controls.Add(this.tsKNX);
            this.flpMainTools.Controls.Add(this.tsComponents);
            this.flpMainTools.Controls.Add(this.tsLayout);
            this.flpMainTools.Controls.Add(this.tsView);
            this.flpMainTools.Name = "flpMainTools";
            // 
            // tsFile
            // 
            this.tsFile.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tsFile, "tsFile");
            this.tsFile.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsFile.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsFile.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.tsFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator12,
            this.tsbNewProj,
            this.tsbOpenProj,
            this.tsbSaveProj});
            this.tsFile.Name = "tsFile";
            this.tsFile.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsFile.Stretch = true;
            this.tsFile.Paint += new System.Windows.Forms.PaintEventHandler(this.tsMain_Paint);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            resources.ApplyResources(this.toolStripSeparator12, "toolStripSeparator12");
            // 
            // tsbNewProj
            // 
            this.tsbNewProj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbNewProj, "tsbNewProj");
            this.tsbNewProj.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.tsbNewProj.Name = "tsbNewProj";
            this.tsbNewProj.Click += new System.EventHandler(this.tsbNewProj_Click);
            // 
            // tsbOpenProj
            // 
            this.tsbOpenProj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbOpenProj, "tsbOpenProj");
            this.tsbOpenProj.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbOpenProj.Name = "tsbOpenProj";
            this.tsbOpenProj.Click += new System.EventHandler(this.tsbOpenProj_Click);
            // 
            // tsbSaveProj
            // 
            this.tsbSaveProj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbSaveProj, "tsbSaveProj");
            this.tsbSaveProj.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbSaveProj.Name = "tsbSaveProj";
            this.tsbSaveProj.Click += new System.EventHandler(this.tsbSaveProj_Click);
            // 
            // tsEdit
            // 
            this.tsEdit.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tsEdit, "tsEdit");
            this.tsEdit.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsEdit.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.tsEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator14,
            this.tsbUndo,
            this.tsbRedo});
            this.tsEdit.Name = "tsEdit";
            this.tsEdit.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsEdit.Paint += new System.Windows.Forms.PaintEventHandler(this.tsMain_Paint);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            resources.ApplyResources(this.toolStripSeparator14, "toolStripSeparator14");
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbUndo, "tsbUndo");
            this.tsbUndo.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbRedo, "tsbRedo");
            this.tsbRedo.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // tsKNX
            // 
            resources.ApplyResources(this.tsKNX, "tsKNX");
            this.tsKNX.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsKNX.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.tsKNX.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator16,
            this.tsbKNXAddr});
            this.tsKNX.Name = "tsKNX";
            this.tsKNX.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            resources.ApplyResources(this.toolStripSeparator16, "toolStripSeparator16");
            // 
            // tsbKNXAddr
            // 
            this.tsbKNXAddr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbKNXAddr, "tsbKNXAddr");
            this.tsbKNXAddr.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.tsbKNXAddr.Name = "tsbKNXAddr";
            this.tsbKNXAddr.Click += new System.EventHandler(this.tsbKNXAddr_Click);
            // 
            // tsComponents
            // 
            this.tsComponents.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tsComponents, "tsComponents");
            this.tsComponents.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsComponents.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.tsComponents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.tsrBtnGroupBox,
            this.tsrBtnAddBlinds,
            this.tsrBtnAddLabel,
            this.tsrBtnAddSceneButton,
            this.tsrBtnAddSliderSwitch,
            this.tsrBtnAddSwitch,
            this.tsrBtnAddValueDisplay,
            this.tsrBtnAddTimerButton,
            this.tsrBtnDigitalAdjustment,
            this.tsrBtnImageButton,
            this.tsbShutter,
            this.tsbDimmer,
            this.tsbWebCamer,
            this.tsbMediaButton,
            this.tsbAirCondition,
            this.tsbHVAC});
            this.tsComponents.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsComponents.Name = "tsComponents";
            this.tsComponents.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsComponents.Paint += new System.Windows.Forms.PaintEventHandler(this.tsMain_Paint);
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
            this.tsrBtnGroupBox.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.tsrBtnGroupBox.Name = "tsrBtnGroupBox";
            this.tsrBtnGroupBox.Click += new System.EventHandler(this.tsrBtnGroupBox_Click);
            // 
            // tsrBtnAddBlinds
            // 
            this.tsrBtnAddBlinds.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddBlinds, "tsrBtnAddBlinds");
            this.tsrBtnAddBlinds.Image = global::UIEditor.Properties.Resources.Blinds_16x16;
            this.tsrBtnAddBlinds.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsrBtnAddBlinds.Name = "tsrBtnAddBlinds";
            this.tsrBtnAddBlinds.Click += new System.EventHandler(this.tsrBtnAddBlinds_Click);
            // 
            // tsrBtnAddLabel
            // 
            this.tsrBtnAddLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddLabel, "tsrBtnAddLabel");
            this.tsrBtnAddLabel.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsrBtnAddLabel.Name = "tsrBtnAddLabel";
            this.tsrBtnAddLabel.Click += new System.EventHandler(this.tsrBtnAddLabel_Click);
            // 
            // tsrBtnAddSceneButton
            // 
            this.tsrBtnAddSceneButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddSceneButton, "tsrBtnAddSceneButton");
            this.tsrBtnAddSceneButton.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsrBtnAddSceneButton.Name = "tsrBtnAddSceneButton";
            this.tsrBtnAddSceneButton.Click += new System.EventHandler(this.tsrBtnAddSceneButton_Click);
            // 
            // tsrBtnAddSliderSwitch
            // 
            this.tsrBtnAddSliderSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddSliderSwitch, "tsrBtnAddSliderSwitch");
            this.tsrBtnAddSliderSwitch.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsrBtnAddSliderSwitch.Name = "tsrBtnAddSliderSwitch";
            this.tsrBtnAddSliderSwitch.Click += new System.EventHandler(this.tsrBtnAddSliderSwitch_Click);
            // 
            // tsrBtnAddSwitch
            // 
            this.tsrBtnAddSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddSwitch, "tsrBtnAddSwitch");
            this.tsrBtnAddSwitch.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsrBtnAddSwitch.Name = "tsrBtnAddSwitch";
            this.tsrBtnAddSwitch.Click += new System.EventHandler(this.tsrBtnAddSwitch_Click);
            // 
            // tsrBtnAddValueDisplay
            // 
            this.tsrBtnAddValueDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddValueDisplay, "tsrBtnAddValueDisplay");
            this.tsrBtnAddValueDisplay.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsrBtnAddValueDisplay.Name = "tsrBtnAddValueDisplay";
            this.tsrBtnAddValueDisplay.Click += new System.EventHandler(this.tsrBtnAddValueDisplay_Click);
            // 
            // tsrBtnAddTimerButton
            // 
            this.tsrBtnAddTimerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnAddTimerButton, "tsrBtnAddTimerButton");
            this.tsrBtnAddTimerButton.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsrBtnAddTimerButton.Name = "tsrBtnAddTimerButton";
            this.tsrBtnAddTimerButton.Click += new System.EventHandler(this.tsrBtnAddTimerButton_Click);
            // 
            // tsrBtnDigitalAdjustment
            // 
            this.tsrBtnDigitalAdjustment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnDigitalAdjustment, "tsrBtnDigitalAdjustment");
            this.tsrBtnDigitalAdjustment.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsrBtnDigitalAdjustment.Name = "tsrBtnDigitalAdjustment";
            this.tsrBtnDigitalAdjustment.Click += new System.EventHandler(this.tsrBtnDigitalAdjustment_Click);
            // 
            // tsrBtnImageButton
            // 
            this.tsrBtnImageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsrBtnImageButton, "tsrBtnImageButton");
            this.tsrBtnImageButton.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsrBtnImageButton.Name = "tsrBtnImageButton";
            this.tsrBtnImageButton.Click += new System.EventHandler(this.tsrBtnImageButton_Click);
            // 
            // tsbShutter
            // 
            this.tsbShutter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbShutter, "tsbShutter");
            this.tsbShutter.Image = global::UIEditor.Properties.Resources.Shutter;
            this.tsbShutter.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbShutter.Name = "tsbShutter";
            this.tsbShutter.Click += new System.EventHandler(this.tsbShutter_Click);
            // 
            // tsbDimmer
            // 
            this.tsbDimmer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbDimmer, "tsbDimmer");
            this.tsbDimmer.Image = global::UIEditor.Properties.Resources.Dimmer_16x16;
            this.tsbDimmer.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbDimmer.Name = "tsbDimmer";
            this.tsbDimmer.Click += new System.EventHandler(this.tsbDimmer_Click);
            // 
            // tsbWebCamer
            // 
            this.tsbWebCamer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbWebCamer, "tsbWebCamer");
            this.tsbWebCamer.Image = global::UIEditor.Properties.Resources.webcam_viewer;
            this.tsbWebCamer.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbWebCamer.Name = "tsbWebCamer";
            this.tsbWebCamer.Click += new System.EventHandler(this.tsbWebCamer_Click);
            // 
            // tsbMediaButton
            // 
            this.tsbMediaButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbMediaButton, "tsbMediaButton");
            this.tsbMediaButton.Image = global::UIEditor.Properties.Resources.media_button;
            this.tsbMediaButton.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbMediaButton.Name = "tsbMediaButton";
            this.tsbMediaButton.Click += new System.EventHandler(this.tsbMediaButton_Click);
            // 
            // tsbAirCondition
            // 
            this.tsbAirCondition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbAirCondition, "tsbAirCondition");
            this.tsbAirCondition.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbAirCondition.Name = "tsbAirCondition";
            this.tsbAirCondition.Click += new System.EventHandler(this.tsbAirCondition_Click);
            // 
            // tsbHVAC
            // 
            this.tsbHVAC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbHVAC, "tsbHVAC");
            this.tsbHVAC.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbHVAC.Name = "tsbHVAC";
            this.tsbHVAC.Click += new System.EventHandler(this.tsbHVAC_Click);
            // 
            // tsLayout
            // 
            this.tsLayout.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.tsLayout, "tsLayout");
            this.tsLayout.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsLayout.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.tsLayout.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.tsbAlignLeft,
            this.tsbAlignRight,
            this.tsbAlignTop,
            this.tsbAlignBottom,
            this.tsbAlignHorizontalCenter,
            this.tsbAlignVerticalCenter,
            this.tsbHorizontalEquidistanceAlignment,
            this.tsbVerticalEquidistanceAlignment,
            this.tsbWidthAlignment,
            this.tsbHeightAlignment,
            this.tsbCenterHorizontalInParent,
            this.tsbCenterVerticalInParent});
            this.tsLayout.Name = "tsLayout";
            this.tsLayout.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsLayout.Stretch = true;
            this.tsLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.tsMain_Paint);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsbAlignLeft
            // 
            this.tsbAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbAlignLeft, "tsbAlignLeft");
            this.tsbAlignLeft.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.tsbAlignLeft.Name = "tsbAlignLeft";
            this.tsbAlignLeft.Click += new System.EventHandler(this.tsbAlignLeft_Click);
            // 
            // tsbAlignRight
            // 
            this.tsbAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbAlignRight, "tsbAlignRight");
            this.tsbAlignRight.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbAlignRight.Name = "tsbAlignRight";
            this.tsbAlignRight.Click += new System.EventHandler(this.tsbAlignRight_Click);
            // 
            // tsbAlignTop
            // 
            this.tsbAlignTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbAlignTop, "tsbAlignTop");
            this.tsbAlignTop.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbAlignTop.Name = "tsbAlignTop";
            this.tsbAlignTop.Click += new System.EventHandler(this.tsbAlignTop_Click);
            // 
            // tsbAlignBottom
            // 
            this.tsbAlignBottom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbAlignBottom, "tsbAlignBottom");
            this.tsbAlignBottom.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbAlignBottom.Name = "tsbAlignBottom";
            this.tsbAlignBottom.Click += new System.EventHandler(this.tsbAlignBottom_Click);
            // 
            // tsbAlignHorizontalCenter
            // 
            this.tsbAlignHorizontalCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbAlignHorizontalCenter, "tsbAlignHorizontalCenter");
            this.tsbAlignHorizontalCenter.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbAlignHorizontalCenter.Name = "tsbAlignHorizontalCenter";
            this.tsbAlignHorizontalCenter.Click += new System.EventHandler(this.tsbAlignHorizontalCenter_Click);
            // 
            // tsbAlignVerticalCenter
            // 
            this.tsbAlignVerticalCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbAlignVerticalCenter, "tsbAlignVerticalCenter");
            this.tsbAlignVerticalCenter.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbAlignVerticalCenter.Name = "tsbAlignVerticalCenter";
            this.tsbAlignVerticalCenter.Click += new System.EventHandler(this.tsbAlignVerticalCenter_Click);
            // 
            // tsbHorizontalEquidistanceAlignment
            // 
            this.tsbHorizontalEquidistanceAlignment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbHorizontalEquidistanceAlignment, "tsbHorizontalEquidistanceAlignment");
            this.tsbHorizontalEquidistanceAlignment.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbHorizontalEquidistanceAlignment.Name = "tsbHorizontalEquidistanceAlignment";
            this.tsbHorizontalEquidistanceAlignment.Click += new System.EventHandler(this.tsbHorizontalEquidistanceAlignment_Click);
            // 
            // tsbVerticalEquidistanceAlignment
            // 
            this.tsbVerticalEquidistanceAlignment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbVerticalEquidistanceAlignment, "tsbVerticalEquidistanceAlignment");
            this.tsbVerticalEquidistanceAlignment.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbVerticalEquidistanceAlignment.Name = "tsbVerticalEquidistanceAlignment";
            this.tsbVerticalEquidistanceAlignment.Click += new System.EventHandler(this.tsbVerticalEquidistanceAlignment_Click);
            // 
            // tsbWidthAlignment
            // 
            this.tsbWidthAlignment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbWidthAlignment, "tsbWidthAlignment");
            this.tsbWidthAlignment.Image = global::UIEditor.Properties.Resources.WidthAlignment;
            this.tsbWidthAlignment.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbWidthAlignment.Name = "tsbWidthAlignment";
            this.tsbWidthAlignment.Click += new System.EventHandler(this.tsbWidthAlignment_Click);
            // 
            // tsbHeightAlignment
            // 
            this.tsbHeightAlignment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbHeightAlignment, "tsbHeightAlignment");
            this.tsbHeightAlignment.Image = global::UIEditor.Properties.Resources.HeightAlignment;
            this.tsbHeightAlignment.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbHeightAlignment.Name = "tsbHeightAlignment";
            this.tsbHeightAlignment.Click += new System.EventHandler(this.tsbHeightAlignment_Click);
            // 
            // tsbCenterHorizontalInParent
            // 
            this.tsbCenterHorizontalInParent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbCenterHorizontalInParent, "tsbCenterHorizontalInParent");
            this.tsbCenterHorizontalInParent.Image = global::UIEditor.Properties.Resources.CenterHorizontalInParent;
            this.tsbCenterHorizontalInParent.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbCenterHorizontalInParent.Name = "tsbCenterHorizontalInParent";
            this.tsbCenterHorizontalInParent.Click += new System.EventHandler(this.tsbCenterHorizontalInParent_Click);
            // 
            // tsbCenterVerticalInParent
            // 
            this.tsbCenterVerticalInParent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbCenterVerticalInParent, "tsbCenterVerticalInParent");
            this.tsbCenterVerticalInParent.Image = global::UIEditor.Properties.Resources.CenterVerticalInParent;
            this.tsbCenterVerticalInParent.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbCenterVerticalInParent.Name = "tsbCenterVerticalInParent";
            this.tsbCenterVerticalInParent.Click += new System.EventHandler(this.tsbCenterVerticalInParent_Click);
            // 
            // tsView
            // 
            resources.ApplyResources(this.tsView, "tsView");
            this.tsView.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsView.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.tsView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator17,
            this.tsbZoomIn,
            this.tsbZoomOut,
            this.tscbViewScale,
            this.tsbAdaptiveScreen});
            this.tsView.Name = "tsView";
            this.tsView.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            resources.ApplyResources(this.toolStripSeparator17, "toolStripSeparator17");
            // 
            // tsbZoomIn
            // 
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbZoomIn, "tsbZoomIn");
            this.tsbZoomIn.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Click += new System.EventHandler(this.tsbZoomIn_Click);
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbZoomOut, "tsbZoomOut");
            this.tsbZoomOut.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Click += new System.EventHandler(this.tsbZoomOut_Click);
            // 
            // tscbViewScale
            // 
            this.tscbViewScale.Items.AddRange(new object[] {
            resources.GetString("tscbViewScale.Items"),
            resources.GetString("tscbViewScale.Items1"),
            resources.GetString("tscbViewScale.Items2"),
            resources.GetString("tscbViewScale.Items3"),
            resources.GetString("tscbViewScale.Items4"),
            resources.GetString("tscbViewScale.Items5"),
            resources.GetString("tscbViewScale.Items6"),
            resources.GetString("tscbViewScale.Items7")});
            this.tscbViewScale.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tscbViewScale.Name = "tscbViewScale";
            resources.ApplyResources(this.tscbViewScale, "tscbViewScale");
            this.tscbViewScale.SelectedIndexChanged += new System.EventHandler(this.tscbViewScale_SelectedIndexChanged);
            this.tscbViewScale.TextUpdate += new System.EventHandler(this.tscbViewScale_TextUpdate);
            // 
            // tsbAdaptiveScreen
            // 
            this.tsbAdaptiveScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbAdaptiveScreen, "tsbAdaptiveScreen");
            this.tsbAdaptiveScreen.Margin = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.tsbAdaptiveScreen.Name = "tsbAdaptiveScreen";
            this.tsbAdaptiveScreen.Click += new System.EventHandler(this.tsbAdaptiveScreen_Click);
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.Controls.Add(this.mnsMain, 0, 0);
            this.tlpMain.Controls.Add(this.flpMainTools, 0, 1);
            this.tlpMain.Controls.Add(this.splitContainer1, 0, 2);
            this.tlpMain.Name = "tlpMain";
            // 
<<<<<<< HEAD
=======
            // BGWOpenProject
            // 
            this.BGWOpenProject.WorkerReportsProgress = true;
            this.BGWOpenProject.WorkerSupportsCancellation = true;
            this.BGWOpenProject.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGWOpenProject_DoWork);
            this.BGWOpenProject.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGWOpenProject_RunWorkerCompleted);
            // 
            // BGWSaveProject
            // 
            this.BGWSaveProject.WorkerReportsProgress = true;
            this.BGWSaveProject.WorkerSupportsCancellation = true;
            this.BGWSaveProject.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGWSaveProject_DoWork);
            this.BGWSaveProject.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGWSaveProject_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslUpdate,
            this.tspbUpdate});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // tsslUpdate
            // 
            this.tsslUpdate.Name = "tsslUpdate";
            resources.ApplyResources(this.tsslUpdate, "tsslUpdate");
            // 
            // tspbUpdate
            // 
            this.tspbUpdate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tspbUpdate.Name = "tspbUpdate";
            resources.ApplyResources(this.tspbUpdate, "tspbUpdate");
            this.tspbUpdate.Step = 1;
            this.tspbUpdate.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
>>>>>>> SationKNXUIEditor-Modify
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tlpMain);
            this.MainMenuStrip = this.mnsMain;
            this.Name = "FrmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.mnsMain.ResumeLayout(false);
            this.mnsMain.PerformLayout();
            this.flpMainTools.ResumeLayout(false);
            this.flpMainTools.PerformLayout();
            this.tsFile.ResumeLayout(false);
            this.tsFile.PerformLayout();
            this.tsEdit.ResumeLayout(false);
            this.tsEdit.PerformLayout();
            this.tsKNX.ResumeLayout(false);
            this.tsKNX.PerformLayout();
            this.tsComponents.ResumeLayout(false);
            this.tsComponents.PerformLayout();
            this.tsLayout.ResumeLayout(false);
            this.tsLayout.PerformLayout();
            this.tsView.ResumeLayout(false);
            this.tsView.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
<<<<<<< HEAD
        private System.Windows.Forms.ToolStripStatusLabel tsslblProjectName;
=======
>>>>>>> SationKNXUIEditor-Modify
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
        private System.Windows.Forms.ToolStrip tsFile;
        private System.Windows.Forms.ToolStripButton tsbNewProj;
        private System.Windows.Forms.ToolStripButton tsbOpenProj;
        private System.Windows.Forms.ToolStripButton tsbSaveProj;
        private System.Windows.Forms.ToolStrip tsComponents;
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
        private System.Windows.Forms.ToolStrip tsEdit;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.ComponentModel.BackgroundWorker BGWOpenProject;
        private System.Windows.Forms.ToolStrip tsLayout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbAlignLeft;
        private System.Windows.Forms.ToolStripButton tsbAlignRight;
        private System.Windows.Forms.ToolStripButton tsbAlignTop;
        private System.Windows.Forms.ToolStripButton tsbAlignBottom;
        private System.Windows.Forms.ToolStripButton tsbAlignHorizontalCenter;
        private System.Windows.Forms.ToolStripButton tsbAlignVerticalCenter;
        private System.Windows.Forms.ToolStripButton tsbHorizontalEquidistanceAlignment;
        private System.Windows.Forms.ToolStripButton tsbVerticalEquidistanceAlignment;
        private System.Windows.Forms.ToolStripButton tsbWidthAlignment;
        private System.Windows.Forms.ToolStripButton tsbHeightAlignment;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmiRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiCut;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmiKNX;
        private System.Windows.Forms.ToolStripMenuItem tsmiKNXAddress;
        private System.Windows.Forms.ToolStripMenuItem tsmiLayout;
        private System.Windows.Forms.ToolStripMenuItem tsmiAlignLeft;
        private System.Windows.Forms.ToolStripMenuItem tsmiAlignRight;
        private System.Windows.Forms.ToolStripMenuItem tsmiAlignTop;
        private System.Windows.Forms.ToolStripMenuItem tsmiAlignBottom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiAlignHorizontalCenter;
        private System.Windows.Forms.ToolStripMenuItem tsmiAlignVerticalCenter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiHorizontalEquidistanceAlignment;
        private System.Windows.Forms.ToolStripMenuItem tsmiVerticalEquidistanceAlignment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem tsmiWidthAlignment;
        private System.Windows.Forms.ToolStripMenuItem tsmiHeightAlignment;
        private System.Windows.Forms.ToolStripMenuItem tsmiComponet;
        private System.Windows.Forms.ToolStripMenuItem tsmiArea;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem tsmiRoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem tsmiPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem tsmiGroupBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiBlinds;
        private System.Windows.Forms.ToolStripMenuItem tsmiLabel;
        private System.Windows.Forms.ToolStripMenuItem tsmiScene;
        private System.Windows.Forms.ToolStripMenuItem tsmiSliderSwitch;
        private System.Windows.Forms.ToolStripMenuItem tsmiSwitch;
        private System.Windows.Forms.ToolStripMenuItem tsmiValueDisplay;
        private System.Windows.Forms.ToolStripMenuItem tsmiTimer;
        private System.Windows.Forms.ToolStripMenuItem tsmiDigitalAdjustment;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ToolStripMenuItem tsmiView;
        private System.Windows.Forms.ToolStripMenuItem tsmiRuler;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoomIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoomOut;
        private System.Windows.Forms.ToolStripMenuItem tsmiImageButton;
        private System.Windows.Forms.ToolStripButton tsrBtnImageButton;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdaptiveScreen;
        private System.ComponentModel.BackgroundWorker BGWSaveProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem tsmiCenterHorizontalInParent;
        private System.Windows.Forms.ToolStripMenuItem tsmiCenterVerticalInParent;
        private System.Windows.Forms.ToolStrip tsKNX;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripButton tsbKNXAddr;
        private System.Windows.Forms.ToolStrip tsView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripComboBox tscbViewScale;
        private System.Windows.Forms.ToolStripButton tsbZoomIn;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ToolStripButton tsbAdaptiveScreen;
        private System.Windows.Forms.ToolStripButton tsbCenterHorizontalInParent;
        private System.Windows.Forms.ToolStripButton tsbCenterVerticalInParent;
        private UserUIControl.UCProjectManager ucpm;
        private UserUIControl.UCPageOutline ucpo;
        private UserUIControl.UCProperty ucp;
        private System.Windows.Forms.ToolStripButton tsbShutter;
        private System.Windows.Forms.ToolStripButton tsbDimmer;
        private System.Windows.Forms.ToolStripMenuItem tsmiCheckUpdate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tspbUpdate;
        private System.Windows.Forms.ToolStripStatusLabel tsslUpdate;
        private System.Windows.Forms.ToolStripButton tsbWebCamer;
        private System.Windows.Forms.ToolStripButton tsbMediaButton;
        private System.Windows.Forms.ToolStripButton tsbAirCondition;
        private System.Windows.Forms.ToolStripButton tsbHVAC;
        //private System.Windows.Forms.ToolStripButton tsrBtnRadioGroup;

    }
}