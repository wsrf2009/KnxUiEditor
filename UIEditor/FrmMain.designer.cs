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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.tvwAppdata = new System.Windows.Forms.TreeView();
            this.imgToolBar = new System.Windows.Forms.ImageList(this.components);
            this.tsrAddControlToolBar = new System.Windows.Forms.ToolStrip();
            this.tsrBtnExpandAll = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsrBtnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsrBtnAddArea = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddRoom = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddPage = new System.Windows.Forms.ToolStripButton();
            this.tsrBtnAddGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsrdpbtnAddContol = new System.Windows.Forms.ToolStripDropDownButton();
            this.palProperty = new System.Windows.Forms.Panel();
            this.cmsAddArea = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddArea = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.flpMainTools = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonNewApp = new System.Windows.Forms.Button();
            this.buttonOpenApp = new System.Windows.Forms.Button();
            this.buttonSaveApp = new System.Windows.Forms.Button();
            this.btnImportKNXFile = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.mnsMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPublish = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.tsslblProjectName = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmsAddRoom = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsAddPage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddPage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsAddGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsAddControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsDeleteControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            this.tsrAddControlToolBar.SuspendLayout();
            this.cmsAddArea.SuspendLayout();
            this.flpMainTools.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.mnsMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.cmsAddRoom.SuspendLayout();
            this.cmsAddPage.SuspendLayout();
            this.cmsAddGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // splMain
            // 
            this.splMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpMain.SetColumnSpan(this.splMain, 2);
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.Location = new System.Drawing.Point(3, 45);
            this.splMain.Name = "splMain";
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.Controls.Add(this.tvwAppdata);
            this.splMain.Panel1.Controls.Add(this.tsrAddControlToolBar);
            this.splMain.Panel1MinSize = 300;
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.Controls.Add(this.palProperty);
            this.splMain.Size = new System.Drawing.Size(938, 507);
            this.splMain.SplitterDistance = 441;
            this.splMain.SplitterWidth = 5;
            this.splMain.TabIndex = 1;
            // 
            // tvwAppdata
            // 
            this.tvwAppdata.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvwAppdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwAppdata.ImageKey = "App_16x16.png";
            this.tvwAppdata.ImageList = this.imgToolBar;
            this.tvwAppdata.Indent = 20;
            this.tvwAppdata.ItemHeight = 18;
            this.tvwAppdata.Location = new System.Drawing.Point(0, 25);
            this.tvwAppdata.Margin = new System.Windows.Forms.Padding(0);
            this.tvwAppdata.Name = "tvwAppdata";
            this.tvwAppdata.SelectedImageIndex = 0;
            this.tvwAppdata.Size = new System.Drawing.Size(439, 480);
            this.tvwAppdata.TabIndex = 1;
            this.tvwAppdata.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewApp_AfterSelect);
            this.tvwAppdata.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvwAppdata_KeyDown);
            this.tvwAppdata.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwAppdata_MouseDown);
            // 
            // imgToolBar
            // 
            this.imgToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolBar.ImageStream")));
            this.imgToolBar.TransparentColor = System.Drawing.Color.Transparent;
            this.imgToolBar.Images.SetKeyName(0, "Control");
            this.imgToolBar.Images.SetKeyName(1, "KNXApp");
            this.imgToolBar.Images.SetKeyName(2, "KNXArea");
            this.imgToolBar.Images.SetKeyName(3, "KNXRoom");
            this.imgToolBar.Images.SetKeyName(4, "KNXPage");
            this.imgToolBar.Images.SetKeyName(5, "KNXGrid");
            this.imgToolBar.Images.SetKeyName(6, "KNXSlide");
            this.imgToolBar.Images.SetKeyName(7, "KNXBlinds");
            this.imgToolBar.Images.SetKeyName(8, "KNXButton");
            this.imgToolBar.Images.SetKeyName(9, "KNXLabel");
            this.imgToolBar.Images.SetKeyName(10, "KNXMediaButton");
            this.imgToolBar.Images.SetKeyName(11, "KNXColorLight");
            this.imgToolBar.Images.SetKeyName(12, "KNXSceneButton");
            this.imgToolBar.Images.SetKeyName(13, "KNXSIPCall");
            this.imgToolBar.Images.SetKeyName(14, "KNXSlider");
            this.imgToolBar.Images.SetKeyName(15, "KNXSliderSwitch");
            this.imgToolBar.Images.SetKeyName(16, "KNXSnapper");
            this.imgToolBar.Images.SetKeyName(17, "KNXSnapperSwitch");
            this.imgToolBar.Images.SetKeyName(18, "KNXSwitch");
            this.imgToolBar.Images.SetKeyName(19, "KNXValueDisplay");
            this.imgToolBar.Images.SetKeyName(20, "KNXWebcamViewer");
            this.imgToolBar.Images.SetKeyName(21, "KNXImageButton");
            this.imgToolBar.Images.SetKeyName(22, "KNXIconSwitch");
            this.imgToolBar.Images.SetKeyName(23, "KNXTimerButton");
            this.imgToolBar.Images.SetKeyName(24, "KNXTimerTaskListView");
            // 
            // tsrAddControlToolBar
            // 
            this.tsrAddControlToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsrAddControlToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsrBtnExpandAll,
            this.tsrBtnCollapseAll,
            this.toolStripSeparator2,
            this.tsrBtnMoveUp,
            this.tsrBtnMoveDown,
            this.toolStripSeparator3,
            this.tsrBtnAddArea,
            this.tsrBtnAddRoom,
            this.tsrBtnAddPage,
            this.tsrBtnAddGrid,
            this.toolStripSeparator4,
            this.tsrdpbtnAddContol});
            this.tsrAddControlToolBar.Location = new System.Drawing.Point(0, 0);
            this.tsrAddControlToolBar.Name = "tsrAddControlToolBar";
            this.tsrAddControlToolBar.Padding = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.tsrAddControlToolBar.Size = new System.Drawing.Size(439, 25);
            this.tsrAddControlToolBar.TabIndex = 2;
            this.tsrAddControlToolBar.Text = "工具条";
            // 
            // tsrBtnExpandAll
            // 
            this.tsrBtnExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnExpandAll.Image = global::UIEditor.Properties.Resources.Expand_16x16;
            this.tsrBtnExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsrBtnExpandAll.Name = "tsrBtnExpandAll";
            this.tsrBtnExpandAll.Size = new System.Drawing.Size(23, 22);
            this.tsrBtnExpandAll.Text = "展开全部";
            this.tsrBtnExpandAll.Click += new System.EventHandler(this.tsrBtnExpandAll_Click);
            // 
            // tsrBtnCollapseAll
            // 
            this.tsrBtnCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnCollapseAll.Image = global::UIEditor.Properties.Resources.Collapse_16x16;
            this.tsrBtnCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsrBtnCollapseAll.Name = "tsrBtnCollapseAll";
            this.tsrBtnCollapseAll.Size = new System.Drawing.Size(23, 22);
            this.tsrBtnCollapseAll.Text = "合并全部";
            this.tsrBtnCollapseAll.Click += new System.EventHandler(this.tsrBtnCollapseAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsrBtnMoveUp
            // 
            this.tsrBtnMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("tsrBtnMoveUp.Image")));
            this.tsrBtnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsrBtnMoveUp.Name = "tsrBtnMoveUp";
            this.tsrBtnMoveUp.Size = new System.Drawing.Size(23, 22);
            this.tsrBtnMoveUp.Text = "上移";
            this.tsrBtnMoveUp.Click += new System.EventHandler(this.tsrBtnMoveUp_Click);
            // 
            // tsrBtnMoveDown
            // 
            this.tsrBtnMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("tsrBtnMoveDown.Image")));
            this.tsrBtnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsrBtnMoveDown.Name = "tsrBtnMoveDown";
            this.tsrBtnMoveDown.Size = new System.Drawing.Size(23, 22);
            this.tsrBtnMoveDown.Text = "下移";
            this.tsrBtnMoveDown.Click += new System.EventHandler(this.tsrBtnMoveDown_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsrBtnAddArea
            // 
            this.tsrBtnAddArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnAddArea.Image = global::UIEditor.Properties.Resources.Area_16x16;
            this.tsrBtnAddArea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsrBtnAddArea.Name = "tsrBtnAddArea";
            this.tsrBtnAddArea.Size = new System.Drawing.Size(23, 22);
            this.tsrBtnAddArea.Text = "添加楼层";
            this.tsrBtnAddArea.Click += new System.EventHandler(this.AddAreaNode_Click);
            // 
            // tsrBtnAddRoom
            // 
            this.tsrBtnAddRoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnAddRoom.Image = ((System.Drawing.Image)(resources.GetObject("tsrBtnAddRoom.Image")));
            this.tsrBtnAddRoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsrBtnAddRoom.Name = "tsrBtnAddRoom";
            this.tsrBtnAddRoom.Size = new System.Drawing.Size(23, 22);
            this.tsrBtnAddRoom.Text = "添加房间";
            this.tsrBtnAddRoom.Click += new System.EventHandler(this.AddRoomNode_Click);
            // 
            // tsrBtnAddPage
            // 
            this.tsrBtnAddPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnAddPage.Image = ((System.Drawing.Image)(resources.GetObject("tsrBtnAddPage.Image")));
            this.tsrBtnAddPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsrBtnAddPage.Name = "tsrBtnAddPage";
            this.tsrBtnAddPage.Size = new System.Drawing.Size(23, 22);
            this.tsrBtnAddPage.Text = "添加页面";
            this.tsrBtnAddPage.Click += new System.EventHandler(this.AddPageNode_Click);
            // 
            // tsrBtnAddGrid
            // 
            this.tsrBtnAddGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrBtnAddGrid.Image = ((System.Drawing.Image)(resources.GetObject("tsrBtnAddGrid.Image")));
            this.tsrBtnAddGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsrBtnAddGrid.Name = "tsrBtnAddGrid";
            this.tsrBtnAddGrid.Size = new System.Drawing.Size(23, 22);
            this.tsrBtnAddGrid.Text = "添加布局网格";
            this.tsrBtnAddGrid.Click += new System.EventHandler(this.AddGridNode_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsrdpbtnAddContol
            // 
            this.tsrdpbtnAddContol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsrdpbtnAddContol.Image = global::UIEditor.Properties.Resources.Controls_16x16;
            this.tsrdpbtnAddContol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsrdpbtnAddContol.Name = "tsrdpbtnAddContol";
            this.tsrdpbtnAddContol.Size = new System.Drawing.Size(29, 22);
            this.tsrdpbtnAddContol.Text = "添加控件";
            this.tsrdpbtnAddContol.Click += new System.EventHandler(this.tsrdpbtnAddContol_Click);
            // 
            // palProperty
            // 
            this.palProperty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.palProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palProperty.Location = new System.Drawing.Point(0, 0);
            this.palProperty.Name = "palProperty";
            this.palProperty.Size = new System.Drawing.Size(490, 505);
            this.palProperty.TabIndex = 4;
            // 
            // cmsAddArea
            // 
            this.cmsAddArea.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddArea,
            this.toolStripSeparator6});
            this.cmsAddArea.Name = "contextMenuStrip1";
            this.cmsAddArea.Size = new System.Drawing.Size(125, 32);
            // 
            // tsmiAddArea
            // 
            this.tsmiAddArea.Image = global::UIEditor.Properties.Resources.Area_16x16;
            this.tsmiAddArea.Name = "tsmiAddArea";
            this.tsmiAddArea.Size = new System.Drawing.Size(124, 22);
            this.tsmiAddArea.Text = "添加楼层";
            this.tsmiAddArea.Click += new System.EventHandler(this.AddAreaNode_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(121, 6);
            // 
            // flpMainTools
            // 
            this.flpMainTools.AutoSize = true;
            this.flpMainTools.Controls.Add(this.buttonNewApp);
            this.flpMainTools.Controls.Add(this.buttonOpenApp);
            this.flpMainTools.Controls.Add(this.buttonSaveApp);
            this.flpMainTools.Controls.Add(this.btnImportKNXFile);
            this.flpMainTools.Controls.Add(this.btnPreview);
            this.flpMainTools.Controls.Add(this.buttonClose);
            this.flpMainTools.Location = new System.Drawing.Point(0, 0);
            this.flpMainTools.Margin = new System.Windows.Forms.Padding(0);
            this.flpMainTools.Name = "flpMainTools";
            this.flpMainTools.Size = new System.Drawing.Size(572, 42);
            this.flpMainTools.TabIndex = 0;
            // 
            // buttonNewApp
            // 
            this.buttonNewApp.Image = global::UIEditor.Properties.Resources.App_32x32;
            this.buttonNewApp.Location = new System.Drawing.Point(1, 1);
            this.buttonNewApp.Margin = new System.Windows.Forms.Padding(1);
            this.buttonNewApp.Name = "buttonNewApp";
            this.buttonNewApp.Size = new System.Drawing.Size(90, 42);
            this.buttonNewApp.TabIndex = 0;
            this.buttonNewApp.Text = " 新建";
            this.buttonNewApp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNewApp.UseVisualStyleBackColor = true;
            this.buttonNewApp.Click += new System.EventHandler(this.buttonNewApp_Click);
            // 
            // buttonOpenApp
            // 
            this.buttonOpenApp.Image = global::UIEditor.Properties.Resources.Open_32x32;
            this.buttonOpenApp.Location = new System.Drawing.Point(93, 1);
            this.buttonOpenApp.Margin = new System.Windows.Forms.Padding(1);
            this.buttonOpenApp.Name = "buttonOpenApp";
            this.buttonOpenApp.Size = new System.Drawing.Size(90, 42);
            this.buttonOpenApp.TabIndex = 1;
            this.buttonOpenApp.Text = " 打开";
            this.buttonOpenApp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOpenApp.UseVisualStyleBackColor = true;
            this.buttonOpenApp.Click += new System.EventHandler(this.buttonOpenApp_Click);
            // 
            // buttonSaveApp
            // 
            this.buttonSaveApp.Image = global::UIEditor.Properties.Resources.Save_32x32;
            this.buttonSaveApp.Location = new System.Drawing.Point(185, 1);
            this.buttonSaveApp.Margin = new System.Windows.Forms.Padding(1);
            this.buttonSaveApp.Name = "buttonSaveApp";
            this.buttonSaveApp.Size = new System.Drawing.Size(90, 42);
            this.buttonSaveApp.TabIndex = 2;
            this.buttonSaveApp.Text = "保存";
            this.buttonSaveApp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSaveApp.UseVisualStyleBackColor = true;
            this.buttonSaveApp.Click += new System.EventHandler(this.buttonSaveApp_Click);
            // 
            // btnImportKNXFile
            // 
            this.btnImportKNXFile.Image = ((System.Drawing.Image)(resources.GetObject("btnImportKNXFile.Image")));
            this.btnImportKNXFile.Location = new System.Drawing.Point(277, 1);
            this.btnImportKNXFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnImportKNXFile.Name = "btnImportKNXFile";
            this.btnImportKNXFile.Size = new System.Drawing.Size(100, 42);
            this.btnImportKNXFile.TabIndex = 4;
            this.btnImportKNXFile.Text = "KNX地址";
            this.btnImportKNXFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImportKNXFile.UseVisualStyleBackColor = true;
            this.btnImportKNXFile.Click += new System.EventHandler(this.btnImportKNXFile_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.Location = new System.Drawing.Point(379, 1);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(1);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(100, 42);
            this.btnPreview.TabIndex = 3;
            this.btnPreview.Text = "页面预览";
            this.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Image = global::UIEditor.Properties.Resources.Close_32;
            this.buttonClose.Location = new System.Drawing.Point(481, 1);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(1);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(90, 42);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "退出";
            this.buttonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 729F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.splMain, 0, 1);
            this.tlpMain.Controls.Add(this.flpMainTools, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 25);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(944, 555);
            this.tlpMain.TabIndex = 5;
            // 
            // mnsMain
            // 
            this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiHelp});
            this.mnsMain.Location = new System.Drawing.Point(0, 0);
            this.mnsMain.Name = "mnsMain";
            this.mnsMain.Size = new System.Drawing.Size(944, 25);
            this.mnsMain.TabIndex = 3;
            this.mnsMain.Text = "menuStrip1";
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
            this.tsmiPublish,
            this.toolStripSeparator8,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(44, 21);
            this.tsmiFile.Text = "文件";
            // 
            // tsmiNew
            // 
            this.tsmiNew.Image = global::UIEditor.Properties.Resources.App_16x16;
            this.tsmiNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiNew.Size = new System.Drawing.Size(174, 22);
            this.tsmiNew.Text = "新建项目";
            this.tsmiNew.Click += new System.EventHandler(this.tsmiNew_Click);
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Image = global::UIEditor.Properties.Resources.Open_32x32;
            this.tsmiOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiOpen.Size = new System.Drawing.Size(174, 22);
            this.tsmiOpen.Text = "打开项目";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Image = global::UIEditor.Properties.Resources.CloseProj_16;
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.tsmiClose.Size = new System.Drawing.Size(174, 22);
            this.tsmiClose.Text = "关闭项目";
            this.tsmiClose.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(171, 6);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSave.Image")));
            this.tsmiSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiSave.Size = new System.Drawing.Size(174, 22);
            this.tsmiSave.Text = "保存";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiSaveAs
            // 
            this.tsmiSaveAs.Name = "tsmiSaveAs";
            this.tsmiSaveAs.Size = new System.Drawing.Size(174, 22);
            this.tsmiSaveAs.Text = "另存为 ...";
            this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
            // 
            // tsmiPublish
            // 
            this.tsmiPublish.Image = global::UIEditor.Properties.Resources.Publish_16;
            this.tsmiPublish.Name = "tsmiPublish";
            this.tsmiPublish.Size = new System.Drawing.Size(174, 22);
            this.tsmiPublish.Text = "发布";
            this.tsmiPublish.Click += new System.EventHandler(this.tsmiPublish_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(171, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Image = global::UIEditor.Properties.Resources.Close_16;
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.tsmiExit.Size = new System.Drawing.Size(174, 22);
            this.tsmiExit.Text = "退出";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenHelp,
            this.toolStripSeparator5,
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(44, 21);
            this.tsmiHelp.Text = "帮助";
            // 
            // tsmiOpenHelp
            // 
            this.tsmiOpenHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenHelp.Image")));
            this.tsmiOpenHelp.Name = "tsmiOpenHelp";
            this.tsmiOpenHelp.Size = new System.Drawing.Size(148, 22);
            this.tsmiOpenHelp.Text = "打开帮助文档";
            this.tsmiOpenHelp.Click += new System.EventHandler(this.tsmiOpenHelp_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(145, 6);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(148, 22);
            this.tsmiAbout.Text = "关于 ...";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblProjectName});
            this.statusStripMain.Location = new System.Drawing.Point(0, 580);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(944, 22);
            this.statusStripMain.TabIndex = 6;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // tsslblProjectName
            // 
            this.tsslblProjectName.Name = "tsslblProjectName";
            this.tsslblProjectName.Size = new System.Drawing.Size(0, 17);
            // 
            // cmsAddRoom
            // 
            this.cmsAddRoom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddRoom,
            this.toolStripSeparator9});
            this.cmsAddRoom.Name = "contextMenuStrip1";
            this.cmsAddRoom.Size = new System.Drawing.Size(125, 32);
            // 
            // tsmiAddRoom
            // 
            this.tsmiAddRoom.Image = global::UIEditor.Properties.Resources.Room_16x16;
            this.tsmiAddRoom.Name = "tsmiAddRoom";
            this.tsmiAddRoom.Size = new System.Drawing.Size(124, 22);
            this.tsmiAddRoom.Text = "添加房间";
            this.tsmiAddRoom.Click += new System.EventHandler(this.AddRoomNode_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(121, 6);
            // 
            // cmsAddPage
            // 
            this.cmsAddPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddPage,
            this.toolStripSeparator10});
            this.cmsAddPage.Name = "contextMenuStrip1";
            this.cmsAddPage.Size = new System.Drawing.Size(125, 32);
            // 
            // tsmiAddPage
            // 
            this.tsmiAddPage.Image = global::UIEditor.Properties.Resources.Page_16x16;
            this.tsmiAddPage.Name = "tsmiAddPage";
            this.tsmiAddPage.Size = new System.Drawing.Size(124, 22);
            this.tsmiAddPage.Text = "添加页面";
            this.tsmiAddPage.Click += new System.EventHandler(this.AddPageNode_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(121, 6);
            // 
            // cmsAddGrid
            // 
            this.cmsAddGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddGrid,
            this.tsmiPreview,
            this.toolStripSeparator7});
            this.cmsAddGrid.Name = "contextMenuStrip1";
            this.cmsAddGrid.Size = new System.Drawing.Size(125, 54);
            // 
            // tsmiAddGrid
            // 
            this.tsmiAddGrid.Image = global::UIEditor.Properties.Resources.Grid_16x16;
            this.tsmiAddGrid.Name = "tsmiAddGrid";
            this.tsmiAddGrid.Size = new System.Drawing.Size(124, 22);
            this.tsmiAddGrid.Text = "添加表格";
            this.tsmiAddGrid.Click += new System.EventHandler(this.AddGridNode_Click);
            // 
            // tsmiPreview
            // 
            this.tsmiPreview.Image = global::UIEditor.Properties.Resources.Check_32x32;
            this.tsmiPreview.Name = "tsmiPreview";
            this.tsmiPreview.Size = new System.Drawing.Size(124, 22);
            this.tsmiPreview.Text = "预览";
            this.tsmiPreview.Click += new System.EventHandler(this.tsmiPreview_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(121, 6);
            // 
            // cmsAddControl
            // 
            this.cmsAddControl.Name = "contextMenuStrip1";
            this.cmsAddControl.Size = new System.Drawing.Size(61, 4);
            // 
            // cmsDeleteControl
            // 
            this.cmsDeleteControl.Name = "contextMenuStrip1";
            this.cmsDeleteControl.Size = new System.Drawing.Size(61, 4);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 602);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.mnsMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnsMain;
            this.Name = "FrmMain";
            this.Text = "界面编辑器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel1.PerformLayout();
            this.splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            this.tsrAddControlToolBar.ResumeLayout(false);
            this.tsrAddControlToolBar.PerformLayout();
            this.cmsAddArea.ResumeLayout(false);
            this.flpMainTools.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.mnsMain.ResumeLayout(false);
            this.mnsMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.cmsAddRoom.ResumeLayout(false);
            this.cmsAddPage.ResumeLayout(false);
            this.cmsAddGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splMain;
        private System.Windows.Forms.TreeView tvwAppdata;
        private System.Windows.Forms.FlowLayoutPanel flpMainTools;
        private System.Windows.Forms.Button buttonNewApp;
        private System.Windows.Forms.Button buttonOpenApp;
        private System.Windows.Forms.Button buttonSaveApp;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ToolStrip tsrAddControlToolBar;
        private System.Windows.Forms.ToolStripButton tsrBtnExpandAll;
        private System.Windows.Forms.ToolStripButton tsrBtnCollapseAll;
        private System.Windows.Forms.ToolStripButton tsrBtnMoveUp;
        private System.Windows.Forms.ToolStripButton tsrBtnMoveDown;
        private System.Windows.Forms.ToolStripButton tsrBtnAddArea;
        private System.Windows.Forms.ToolStripButton tsrBtnAddRoom;
        private System.Windows.Forms.ToolStripButton tsrBtnAddPage;
        private System.Windows.Forms.ToolStripDropDownButton tsrdpbtnAddContol;
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
        private System.Windows.Forms.ToolStripButton tsrBtnAddGrid;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ImageList imgToolBar;
        private System.Windows.Forms.Button btnImportKNXFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip cmsAddArea;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddArea;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.ContextMenuStrip cmsAddRoom;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddRoom;
        private System.Windows.Forms.ContextMenuStrip cmsAddPage;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddPage;
        private System.Windows.Forms.ContextMenuStrip cmsAddGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddGrid;
        private System.Windows.Forms.ContextMenuStrip cmsAddControl;
        private System.Windows.Forms.Panel palProperty;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreview;
        private System.Windows.Forms.ToolStripMenuItem tsmiPublish;
        private System.Windows.Forms.ToolStripStatusLabel tsslblProjectName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ContextMenuStrip cmsDeleteControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;

    }
}