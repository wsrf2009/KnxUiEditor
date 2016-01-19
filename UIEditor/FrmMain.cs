using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using NLog;
using SourceGrid;
using SourceGrid.Cells.Views;
using UIEditor.Component;
using UIEditor.Entity;
using UIEditor.Entity.Control;
using ColumnHeader = SourceGrid.Cells.ColumnHeader;

namespace UIEditor
{
    struct ToolBarStatus
    {
        public bool expand;
        public bool collapse;
        public bool moveup;
        public bool movedown;
        public bool area;
        public bool room;
        public bool page;
        public bool grid;
        public bool control;
        public bool searchBox;
        public bool priview;
        public bool importKnx;
        public bool btnPriview;
    }

    public partial class FrmMain : Form
    {
        // 日志
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region 常量

        private const string KnxFilter = "KNX UI metadata files (*.knxuie)|*.knxuie|All files (*.*)|*.*";

        #endregion

        #region 模块变量

        // 文件是否保存
        private bool _saved = false;

        // 保存文件名
        private string _projectFile = "";

        // 显示属性的网格
        private readonly Grid _gridProperty = new Grid();

        private ViewNode _tempCacheNode;

        #endregion

        #region 构造函数

        public FrmMain()
        {
            InitializeComponent();

            InitMenuItem();

            InitGrid();
        }

        private void InitMenuItem()
        {
            this.cmsAddArea.Items.Add(CreatePasteMenuItem());
            this.cmsAddArea.Items.Add(CreateDeleteMenuItem());


            this.cmsAddRoom.Items.AddRange(CreateEditMenu());

            this.cmsAddPage.Items.AddRange(CreateEditMenu());

            this.cmsAddGrid.Items.AddRange(CreateEditMenu(true));
            this.cmsAddControl.Items.AddRange(CreateEditMenu(true));

            this.cmsDeleteControl.Items.AddRange(CreateEditMenu());

            // 
            this.cmsAddControl.Items.AddRange(CreateSubToolStripItem());


            //
            this.tsrdpbtnAddContol.DropDownItems.AddRange(CreateSubToolStripItem());

            //
            this.cmsAddGrid.Items.AddRange(CreateSubToolStripItem());


        }

        /// <summary>
        /// 创建删除右键菜单
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreateDeleteMenuItem()
        {
            // 
            // tsmiDeleteItem
            // 
            ToolStripMenuItem tsmiDeleteItem = new ToolStripMenuItem();
            tsmiDeleteItem.Image = Properties.Resources.Delete_Control_16x16;
            tsmiDeleteItem.Name = "tsmiDeleteItem";
            tsmiDeleteItem.Size = new System.Drawing.Size(100, 22);
            tsmiDeleteItem.Text = "删除";
            tsmiDeleteItem.Click += this.DeleteSelectNode_Click;

            return tsmiDeleteItem;
        }

        private ToolStripMenuItem CreatePasteMenuItem()
        {
            // tsmiPasteNode
            // 
            ToolStripMenuItem tsmiPasteNode = new ToolStripMenuItem();
            tsmiPasteNode.Image = Properties.Resources.Paste_16x16;
            tsmiPasteNode.Name = "tsmiPasteNode";
            tsmiPasteNode.Size = new System.Drawing.Size(152, 22);
            tsmiPasteNode.Text = "粘贴";
            tsmiPasteNode.Click += PasteNode_Click;

            return tsmiPasteNode;
        }

        private ToolStripMenuItem CreateCopyMenuItem()
        {
            // 
            // tsmiCopyNode
            // 
            ToolStripMenuItem tsmiCopyNode = new ToolStripMenuItem();
            tsmiCopyNode.Image = Properties.Resources.Copy_16x16;
            tsmiCopyNode.Name = "tsmiCopyNode";
            tsmiCopyNode.Size = new System.Drawing.Size(152, 22);
            tsmiCopyNode.Text = "复制";
            tsmiCopyNode.Click += CopyNode_Click;

            return tsmiCopyNode;
        }

        private ToolStripMenuItem CreateCutMenuItem()
        {
            // 
            // tsmiCutNode
            // 
            ToolStripMenuItem tsmiCutNode = new ToolStripMenuItem();
            tsmiCutNode.Image = Properties.Resources.Cut_16x16;
            tsmiCutNode.Name = "tsmiCutNode";
            tsmiCutNode.Size = new System.Drawing.Size(152, 22);
            tsmiCutNode.Text = "剪切";
            tsmiCutNode.Click += CutNode_Click;

            return tsmiCutNode;
        }

        /// <summary>
        ///  创建编辑的右键菜单， 复制剪切粘贴
        /// </summary>
        /// <param name="includeSplite"></param>
        /// <returns></returns>
        private ToolStripItem[] CreateEditMenu(bool includeSplite = false)
        {
            var separator = new ToolStripSeparator();
            separator.Name = "toolStripSeparator1";
            separator.Size = new Size(121, 6);

            List<ToolStripItem> list = new List<ToolStripItem>();
            list.Add(CreateCopyMenuItem());
            list.Add(CreateCutMenuItem());
            list.Add(CreatePasteMenuItem());
            list.Add(CreateDeleteMenuItem());

            // 是否添加删除菜单
            if (includeSplite)
            {
                list.Add(separator);
            }

            return list.ToArray();
        }

        private void PasteNode_Click(object sender, EventArgs e)
        {
            if (_tempCacheNode == null)
            {
                return;
            }

            var selectedNode = this.tvwAppdata.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }

            var cloneNode = _tempCacheNode;

            switch (cloneNode.Name)
            {
                case MyConst.View.KnxAppType:
                    break;

                case MyConst.View.KnxAreaType:
                    {
                        if (selectedNode.Name == MyConst.View.KnxAppType)
                        {
                            selectedNode.Nodes.Add(cloneNode);
                        }
                    }
                    break;

                case MyConst.View.KnxRoomType:
                    {
                        if (selectedNode.Name == MyConst.View.KnxAreaType)
                        {
                            selectedNode.Nodes.Add(cloneNode);
                        }
                    }
                    break;

                case MyConst.View.KnxPageType:
                    {
                        if (selectedNode.Name == MyConst.View.KnxRoomType)
                        {
                            selectedNode.Nodes.Add(cloneNode);
                        }
                    }
                    break;

                case MyConst.View.KnxGridType:
                    {
                        if (selectedNode.Name == MyConst.View.KnxPageType)
                        {
                            selectedNode.Nodes.Add(cloneNode);
                        }
                    }
                    break;
                case MyConst.Controls.KnxBlindsType:
                case MyConst.Controls.KnxColorLightType:
                case MyConst.Controls.KnxImageButtonType:
                case MyConst.Controls.KnxLabelType:
                case MyConst.Controls.KnxMediaButtonType:
                case MyConst.Controls.KnxSceneButtonType:
                case MyConst.Controls.KnxSipCallType:
                case MyConst.Controls.KnxSliderSwitchType:
                case MyConst.Controls.KnxSliderType:
                case MyConst.Controls.KnxSnapperSwitchType:
                case MyConst.Controls.KnxSnapperType:
                case MyConst.Controls.KnxSwitchType:
                case MyConst.Controls.KnxValueDisplayType:
                case MyConst.Controls.KnxWebCamViewerType:
                case MyConst.Controls.KnxTimerButtonType:
                case MyConst.Controls.KnxTimerTaskListViewType:
                    {
                        if (selectedNode.Name == MyConst.View.KnxPageType || selectedNode.Name == MyConst.View.KnxGridType)
                        {
                            selectedNode.Nodes.Add(cloneNode);
                        }
                    }
                    break;
                default:
                    MessageBox.Show("类型不兼容，不能粘贴到当前的节点！");
                    break;
            }
        }

        private void CutNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            _tempCacheNode = (ViewNode)selectedNode;
            selectedNode.Remove();
        }

        private void CopyNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;

            _tempCacheNode = (ViewNode)selectedNode.Clone();
        }


        private ToolStripItem[] CreateSubToolStripItem()
        {
            // 
            // tsmiAddBlinds
            // 
            ToolStripMenuItem tsmiAddBlinds = new ToolStripMenuItem();
            tsmiAddBlinds.Image = Properties.Resources.blinds;
            tsmiAddBlinds.Name = "tsmiAddBlinds";
            tsmiAddBlinds.Size = new System.Drawing.Size(152, 22);
            tsmiAddBlinds.Text = "窗帘开关";
            tsmiAddBlinds.Click += new System.EventHandler(AddBlindsNode_Click);
            // 
            // tsmiAddImageButton
            // 
            ToolStripMenuItem tsmiAddImageButton = new ToolStripMenuItem();
            tsmiAddImageButton.Image = Properties.Resources.imageButton;
            tsmiAddImageButton.Name = "tsmiAddImageButton";
            tsmiAddImageButton.Size = new System.Drawing.Size(152, 22);
            tsmiAddImageButton.Text = "图像按钮";
            tsmiAddImageButton.Click += new System.EventHandler(tsmiAddImageButton_Click);
            // 
            // tsmiAddLabel
            // 
            ToolStripMenuItem tsmiAddLabel = new ToolStripMenuItem();
            tsmiAddLabel.Image = Properties.Resources.label;
            tsmiAddLabel.Name = "tsmiAddLabel";
            tsmiAddLabel.Size = new System.Drawing.Size(152, 22);
            tsmiAddLabel.Text = "标签";
            tsmiAddLabel.Click += new System.EventHandler(AddLabelNode_Click);
            // 
            // tsmiAddMediaButton
            // 
            ToolStripMenuItem tsmiAddMediaButton = new ToolStripMenuItem();
            tsmiAddMediaButton.Image = Properties.Resources.media_button;
            tsmiAddMediaButton.Name = "tsmiAddMediaButton";
            tsmiAddMediaButton.Size = new System.Drawing.Size(152, 22);
            tsmiAddMediaButton.Text = "多媒体开关";
            tsmiAddMediaButton.Click += new System.EventHandler(AddMediaButtonNode_Click);
            // 
            // tsmiAddColorLight
            // 
            ToolStripMenuItem tsmiAddColorLight = new ToolStripMenuItem();
            tsmiAddColorLight.Image = Properties.Resources.rgb;
            tsmiAddColorLight.Name = "tsmiAddColorLight";
            tsmiAddColorLight.Size = new System.Drawing.Size(152, 22);
            tsmiAddColorLight.Text = "彩灯开关";
            tsmiAddColorLight.Click += new System.EventHandler(AddColorLightNode_Click);
            // 
            // tsmiAddSceneButton
            // 
            ToolStripMenuItem tsmiAddSceneButton = new ToolStripMenuItem();
            tsmiAddSceneButton.Image = Properties.Resources.scene_button;
            tsmiAddSceneButton.Name = "tsmiAddSceneButton";
            tsmiAddSceneButton.Size = new System.Drawing.Size(152, 22);
            tsmiAddSceneButton.Text = "场景开关";
            tsmiAddSceneButton.Click += new System.EventHandler(AddSceneButtonNode_Click);
            // 
            // tsmiAddSIPCall
            // 
            ToolStripMenuItem tsmiAddSipCall = new ToolStripMenuItem();
            tsmiAddSipCall.Image = Properties.Resources.sip2;
            tsmiAddSipCall.Name = "tsmiAddSIPCall";
            tsmiAddSipCall.Size = new System.Drawing.Size(152, 22);
            tsmiAddSipCall.Text = "可视门铃";
            tsmiAddSipCall.Click += new System.EventHandler(AddSIPCallNode_Click);
            // 
            // tsmiAddSlider
            // 
            ToolStripMenuItem tsmiAddSlider = new ToolStripMenuItem();
            tsmiAddSlider.Image = Properties.Resources.slider;
            tsmiAddSlider.Name = "tsmiAddSlider";
            tsmiAddSlider.Size = new System.Drawing.Size(152, 22);
            tsmiAddSlider.Text = "滑动条";
            tsmiAddSlider.Click += new System.EventHandler(AddSliderNode_Click);
            // 
            // tsmiAddSliderSwitch
            // 
            ToolStripMenuItem tsmiAddSliderSwitch = new ToolStripMenuItem();
            tsmiAddSliderSwitch.Image = Properties.Resources.slider_switch;
            tsmiAddSliderSwitch.Name = "tsmiAddSliderSwitch";
            tsmiAddSliderSwitch.Size = new System.Drawing.Size(152, 22);
            tsmiAddSliderSwitch.Text = "滑动开关";
            tsmiAddSliderSwitch.Click += new System.EventHandler(AddSliderSwitchNode_Click);
            // 
            // tsmiAddSnapper
            // 
            ToolStripMenuItem tsmiAddSnapper = new ToolStripMenuItem();
            tsmiAddSnapper.Image = Properties.Resources.snapper;
            tsmiAddSnapper.Name = "tsmiAddSnapper";
            tsmiAddSnapper.Size = new System.Drawing.Size(152, 22);
            tsmiAddSnapper.Text = "步进条";
            tsmiAddSnapper.Click += new System.EventHandler(AddSnapperNode_Click);
            // 
            // tsmiAddSnapperSwitch
            // 
            ToolStripMenuItem tsmiAddSnapperSwitch = new ToolStripMenuItem();
            tsmiAddSnapperSwitch.Image = Properties.Resources.snapper_switch;
            tsmiAddSnapperSwitch.Name = "tsmiAddSnapperSwitch";
            tsmiAddSnapperSwitch.Size = new System.Drawing.Size(152, 22);
            tsmiAddSnapperSwitch.Text = "步进开关";
            tsmiAddSnapperSwitch.Click += new System.EventHandler(AddSnapperSwitchNode_Click);
            // 
            // tsmiAddSwitch
            // 
            ToolStripMenuItem tsmiAddSwitch = new ToolStripMenuItem();
            tsmiAddSwitch.Image = Properties.Resources.switch_icon;
            tsmiAddSwitch.Name = "tsmiAddSwitch";
            tsmiAddSwitch.Size = new System.Drawing.Size(152, 22);
            tsmiAddSwitch.Text = "开关";
            tsmiAddSwitch.Click += new System.EventHandler(AddSwitchNode_Click);
            // 
            // tsmiAddValueDisplay
            // 
            ToolStripMenuItem tsmiAddValueDisplay = new ToolStripMenuItem();
            tsmiAddValueDisplay.Image = Properties.Resources.value_display;
            tsmiAddValueDisplay.Name = "tsmiAddValueDisplay";
            tsmiAddValueDisplay.Size = new System.Drawing.Size(152, 22);
            tsmiAddValueDisplay.Text = "数码显示";
            tsmiAddValueDisplay.Click += new System.EventHandler(AddValueDisplayNode_Click);
            // 
            // tsmiAddWebcam
            // 
            ToolStripMenuItem tsmiAddWebcam = new ToolStripMenuItem();
            tsmiAddWebcam.Image = Properties.Resources.webcam_viewer;
            tsmiAddWebcam.Name = "tsmiAddWebcam";
            tsmiAddWebcam.Size = new System.Drawing.Size(152, 22);
            tsmiAddWebcam.Text = "网络摄像头";
            tsmiAddWebcam.Click += new System.EventHandler(AddWebcamViewerNode_Click);

            ToolStripMenuItem tsmiAddTimerButton = new ToolStripMenuItem();
            tsmiAddTimerButton.Image = Properties.Resources.timer_task;
            tsmiAddTimerButton.Name = "tsmiAddTimerButton";
            tsmiAddTimerButton.Size = new System.Drawing.Size(152, 22);
            tsmiAddTimerButton.Text = "定时器";
            tsmiAddTimerButton.Click += new System.EventHandler(AddTimerButtonNode_Click);

            ToolStripMenuItem tsmiAddTimerTaskListView = new ToolStripMenuItem();
            tsmiAddTimerTaskListView.Image = Properties.Resources.timer_task_list;
            tsmiAddTimerTaskListView.Name = "tsmiAddTimerTaskListView";
            tsmiAddTimerTaskListView.Size = new System.Drawing.Size(152, 22);
            tsmiAddTimerTaskListView.Text = "定时任务列表";
            tsmiAddTimerTaskListView.Click += new System.EventHandler(AddTimerTaskListViewNode_Click);

            List<ToolStripItem> list = new List<ToolStripItem>();
            list.Add(tsmiAddBlinds);
            list.Add(tsmiAddImageButton);
            list.Add(tsmiAddLabel);
            list.Add(tsmiAddMediaButton);
            list.Add(tsmiAddColorLight);
            list.Add(tsmiAddSceneButton);
            list.Add(tsmiAddSipCall);
            list.Add(tsmiAddSlider);
            list.Add(tsmiAddSliderSwitch);
            list.Add(tsmiAddSnapper);
            list.Add(tsmiAddSnapperSwitch);
            list.Add(tsmiAddSwitch);
            list.Add(tsmiAddValueDisplay);
            list.Add(tsmiAddWebcam);
            list.Add(tsmiAddTimerButton);
            list.Add(tsmiAddTimerTaskListView);

            return list.ToArray();
        }

        public bool Saved
        {
            get { return _saved; }
            set { _saved = value; }
        }

        public string ProjectFile
        {
            get { return _projectFile; }
            set { _projectFile = value; }
        }

        /// <summary>
        /// 用户控件初始化
        /// </summary>
        private void InitGrid()
        {
            // grid 初始化            
            this._gridProperty.Redim(0, 0);
            this._gridProperty.Dock = DockStyle.Fill;
            this._gridProperty.EnableSort = false;
            this._gridProperty.Location = new Point(0, 0);
            this._gridProperty.Name = "GridProperty";
            this._gridProperty.OptimizeMode = CellOptimizeMode.ForRows;
            this._gridProperty.SelectionMode = GridSelectionMode.Cell;
            this._gridProperty.TabIndex = 0;
            this._gridProperty.TabStop = true;

            SetGridHeader();

            // 添加到 panel 中显示
            this.palProperty.Controls.Add(this._gridProperty);
        }


        /// <summary>
        /// 设置属性表格的样式
        /// </summary>
        private void SetGridHeader()
        {
            _gridProperty.ColumnsCount = 3;

            var titleModel = new Cell();
            titleModel.BackColor = Color.SteelBlue;
            titleModel.ForeColor = Color.White;
            titleModel.Font = new Font(_gridProperty.Font, FontStyle.Bold);

            _gridProperty.FixedRows = 1;
            _gridProperty.Rows.Insert(0);
            _gridProperty.Rows[0].Height = 28;


            _gridProperty[0, 0] = new ColumnHeader("属性名称");
            _gridProperty[0, 0].View = titleModel;
            _gridProperty[0, 1] = new ColumnHeader("属性值");
            _gridProperty[0, 1].View = titleModel;
            _gridProperty[0, 2] = new ColumnHeader("");
            _gridProperty[0, 2].View = titleModel;

            _gridProperty.Columns[0].Width = 150;
            _gridProperty.Columns[1].MinimalWidth = 150;
            _gridProperty.Columns[2].Width = 36;
            //gridProperty.Columns[2].MaximalWidth = 35;

            _gridProperty.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            _gridProperty.Columns[1].AutoSizeMode = SourceGrid.AutoSizeMode.Default;
            _gridProperty.Columns[2].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            _gridProperty.AutoStretchColumnsToFitWidth = true;
            _gridProperty.Columns.StretchToFit();
        }

        #endregion

        #region 窗体事件

        private void FormMain_Load(object sender, EventArgs e)
        {
            ToolBarStatus status = new ToolBarStatus()
            {
                moveup = false,
            };

            SetButtonStatus(status);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Saved == false && tvwAppdata.Nodes.Count > 0)
            {
                var result = MessageBox.Show("文件已经修改，是否要保存？", "文件保存提醒", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    var save = SaveKnxUiProject(ProjectFile);
                    if (save == false)
                    {
                        e.Cancel = true;
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion

        #region treeview 事件

        /// <summary>
        /// 选中树上的节点，显示其属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewApp_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedNode = e.Node;

            if (selectedNode != null)
            {
                var node = selectedNode as ViewNode;
                // 1. 设置界面 button
                CheckBarStatus(selectedNode);

                // 2. 显示当前节点的属性信息            
                _gridProperty.Rows.Clear();
                SetGridHeader();
                node.DisplayProperties(_gridProperty);


                #region 给节点绑定上下文菜单
                switch (selectedNode.Name)
                {
                    case MyConst.View.KnxAppType:
                        selectedNode.ContextMenuStrip = cmsAddArea;
                        break;

                    case MyConst.View.KnxAreaType:
                        selectedNode.ContextMenuStrip = cmsAddRoom;
                        break;

                    case MyConst.View.KnxRoomType:
                        selectedNode.ContextMenuStrip = cmsAddPage;
                        break;

                    case MyConst.View.KnxPageType:
                        selectedNode.ContextMenuStrip = cmsAddGrid;
                        break;

                    case MyConst.View.KnxGridType:
                        selectedNode.ContextMenuStrip = cmsAddControl;
                        break;

                    default:
                        selectedNode.ContextMenuStrip = cmsDeleteControl;
                        break;
                }
                #endregion
            }
        }

        private void tvwAppdata_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (tvwAppdata.GetNodeAt(e.X, e.Y) != null)
                {
                    this.tvwAppdata.SelectedNode = tvwAppdata.GetNodeAt(e.X, e.Y);
                }
            }
        }

        #endregion

        #region 主菜单事件

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            NewKnxUiProject();
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            OpenKnxUiPrject();
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            SaveKnxUiProject(ProjectFile);
        }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            SaveKnxUiProject(MyConst.DefaultKnxUiProjectName);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        private void tsmiOpenHelp_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["FrmHelp"] != null)
            {
                var frmHelp = Application.OpenForms["FrmHelp"];
                frmHelp.WindowState = FormWindowState.Normal;
                frmHelp.Activate();
            }
            else
            {
                var frmHelp = new FrmHelp();
                frmHelp.Show(this);
            }
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            new FrmAboutBox().ShowDialog(this);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 保存当前的数据
            SaveKnxUiProject(ProjectFile);

            // 清除所有节点
            this.tvwAppdata.Nodes.Clear();

            // 清楚表格中的行
            this._gridProperty.Rows.Clear();
            // 显示表头
            this.SetGridHeader();
        }

        private void tsmiKnxGroupAddress_Click(object sender, EventArgs e)
        {
            OpenGroupAddressMgr();
        }

        /// <summary>
        /// 发布程到web服务器，提供给用户下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiPublish_Click(object sender, EventArgs e)
        {
            PublishKnxUiProject();
        }

        #endregion

        #region 主工具条事件

        /// <summary>
        /// 添加APP，整个应用的根
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewApp_Click(object sender, EventArgs e)
        {
            NewKnxUiProject();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        private void buttonSaveApp_Click(object sender, EventArgs e)
        {
            SaveKnxUiProject(ProjectFile);
        }

        private void buttonOpenApp_Click(object sender, EventArgs e)
        {
            OpenKnxUiPrject();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            ShowPreview();
        }

        private void tsmiPreview_Click(object sender, EventArgs e)
        {
            ShowPreview();
        }

        private void ShowPreview()
        {
            var frm = new FrmPreview();
            var appNode = this.tvwAppdata.Nodes[0] as AppNode;
            if (appNode != null)
            {
                frm.Size = new Size(appNode.ScreenWidth, appNode.ScreenHeight);
            }

            var pageNode = this.tvwAppdata.SelectedNode as PageNode;
            if (pageNode != null)
            {
                frm.SelectedNode = pageNode;
            }
            frm.Show(this);
        }

        private void btnImportKNXFile_Click(object sender, EventArgs e)
        {
            OpenGroupAddressMgr();
        }

        private void OpenGroupAddressMgr()
        {
            try
            {
                var formName = typeof(FrmGroupAddressMgt).Name;
                if (Application.OpenForms[formName] != null)
                {
                    var frm = Application.OpenForms[formName];
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                }
                else
                {
                    var frm = new FrmGroupAddressMgt();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "群组加载错误！";
                MessageBox.Show(errorMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
        }

        #endregion

        #region treeview 工具条事件

        private void tsrBtnExpandAll_Click(object sender, EventArgs e)
        {
            // 展开树树上的所有节点
            this.tvwAppdata.ExpandAll();
        }

        private void tsrBtnCollapseAll_Click(object sender, EventArgs e)
        {
            // 合并节点
            this.tvwAppdata.CollapseAll();
        }

        private void tsrBtnMoveUp_Click(object sender, EventArgs e)
        {
            //节点上移一个
            var selectedNode = this.tvwAppdata.SelectedNode;
            NodeMoveUp(selectedNode);
        }

        private void tsrBtnMoveDown_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            NodeMoveDown(selectedNode);
        }

        private void AddAreaNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddAreaNode(selectedNode);
        }

        /// <summary>
        /// 添加 Room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRoomNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddRoomNode(selectedNode);
        }

        private void AddPageNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddPageNode(selectedNode);
        }

        private void AddGridNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddGridNode(selectedNode);
        }

        private void AddBlindsNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddBlindsNode(selectedNode);
        }

        private void DeleteSelectNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            DeleteSelectedNode(selectedNode);
        }

        private void AddLabelNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddLabelNode(selectedNode);
        }

        private void AddMediaButtonNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddMediaButtonNode(selectedNode);
        }

        private void AddColorLightNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddColorLightNode(selectedNode);
        }

        private void AddSceneButtonNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddSceneButtonNode(selectedNode);
        }

        private void AddSIPCallNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddSIPCallNode(selectedNode);
        }

        private void AddSliderNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddSlideNode(selectedNode);
        }

        private void AddSliderSwitchNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddSliderSwitchNode(selectedNode);
        }

        private void AddSnapperNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddSnapperNode(selectedNode);
        }

        private void AddSnapperSwitchNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddSnapperSwitchNode(selectedNode);
        }

        private void AddSwitchNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddSwitchNode(selectedNode);
        }

        private void AddValueDisplayNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddValueDisplayNode(selectedNode);
        }

        private void AddWebcamViewerNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddWebcamViewerNode(selectedNode);
        }

        private void AddTimerButtonNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddTimerButtonNode(selectedNode);
        }

        private void AddTimerTaskListViewNode_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddTimerTaskListViewNode(selectedNode);
        }

        #endregion

        #region 打开，保存，退出方法

        private static void CreateProjectFolder()
        {
            // 新建项目文件
            var appFolder = "knxuieditor_" + DateTime.Now.Ticks;
            MyCache.ProjectFolder = Path.Combine(MyCache.DefaultKnxCacheFolder, appFolder);
            MyCache.ProjTempFolder = Path.Combine(MyCache.ProjectFolder, MyConst.TempFolder);
            MyCache.ProjImagePath = Path.Combine(MyCache.ProjectFolder, MyConst.ImageFolder);
            Directory.CreateDirectory(MyCache.ProjTempFolder);
            Directory.CreateDirectory(MyCache.ProjImagePath);
        }

        private bool SaveKnxUiProject(string fileName)
        {
            Cursor = Cursors.WaitCursor;
            bool result = false;

            try
            {
                // 当前的项目不为空
                if (tvwAppdata.Nodes.Count > 0)
                {
                    // 保存项目文件的版本信息。
                    VersionStorage.Save();

                    // 保存当前树上的节点为 json 文件
                    var app = FrmMainHelp.ExportTreeView(tvwAppdata);

                    // 保存 KNXApp 对象到文件
                    AppStorage.Save(app);

                    // 是否指定了项目文件名
                    if (fileName == MyConst.DefaultKnxUiProjectName)
                    {
                        var myDialog = new SaveFileDialog();
                        myDialog.InitialDirectory = MyCache.DefaultKnxProjectFolder;
                        myDialog.OverwritePrompt = true;
                        myDialog.FileName = MyConst.DefaultKnxUiProjectName;
                        myDialog.DefaultExt = MyConst.KnxUiEditorFileExt;
                        myDialog.Filter = KnxFilter;
                        myDialog.FilterIndex = 1;
                        myDialog.RestoreDirectory = true;

                        var myResult = myDialog.ShowDialog(this);

                        if (DialogResult.OK == myResult)
                        {
                            ProjectFile = myDialog.FileName;
                            ZipProject();
                            result = true;
                        }
                    }
                    else
                    {
                        ZipProject();
                        result = true;
                    }
                }
                else
                {
                    string errorMsg = "没有数据!";
                    MessageBox.Show(errorMsg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "系统异常, exception message: " + ex.Message;
                MessageBox.Show(errorMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            return result;
        }

        private void ZipProject()
        {
            // 删除临时目录
            FileHelper.DeleteFolder(MyCache.ProjTempFolder);

            //保存项目文件为 knxuie 类型
            // ZipHelper.ZipDir(MyCache.ProjectFolder, ProjectFile.Replace(MyConst.KnxUiEditorFileExt, "v" + MyCache.ProjectVersion.Version + "." + MyConst.KnxUiEditorFileExt), MyConst.MyKey);
            ZipHelper.ZipDir(MyCache.ProjectFolder, ProjectFile, MyConst.MyKey);

            //保存状态
            Saved = true;
            ShowProjectFile(ProjectFile);
        }

        private void ShowProjectFile(string projectFile)
        {
            tsslblProjectName.Text = string.Format("Project File: {0}", projectFile);
        }

        private void NewKnxUiProject()
        {
            if (Saved == false && tvwAppdata.Nodes.Count > 0)
            {
                MessageBox.Show("当前项目未保存，请先保存当前的项目!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AddAppNode(this.tvwAppdata);
            }
        }

        /// <summary>
        /// 打开应用程序
        /// </summary>
        private void OpenKnxUiPrject()
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                if (Saved == false && tvwAppdata.Nodes.Count > 0)
                {
                    MessageBox.Show("当前项目未保存，请先保存当前的项目!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (var ofd = new OpenFileDialog())
                    {
                        ofd.InitialDirectory = MyCache.DefaultKnxProjectFolder;
                        ofd.Filter = KnxFilter;
                        ofd.FilterIndex = 1;
                        ofd.DefaultExt = MyConst.KnxUiEditorFileExt;
                        ofd.RestoreDirectory = true;

                        if (ofd.ShowDialog(this) == DialogResult.OK)
                        {
                            // 新建项目文件夹
                            CreateProjectFolder();
                            var projectFile = ofd.FileName;
                            Debug.WriteLine(projectFile);
                            Debug.WriteLine(MyCache.ProjectFolder);

                            ZipHelper.UnZipDir(projectFile, MyCache.ProjectFolder, MyConst.MyKey);

                            var app = AppStorage.Load();

                            if (app != null)
                            {
                                // 导入所有节点
                                FrmMainHelp.ImportNode(app, tvwAppdata);

                                ProjectFile = ofd.FileName;
                                ShowProjectFile(ProjectFile);

                                // 
                                ToolBarStatus status = new ToolBarStatus { collapse = true, expand = true, searchBox = true, importKnx = true };
                                SetButtonStatus(status);

                                Saved = true;
                            }
                            else
                            {
                                throw new ApplicationException("项目文件不兼容");
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string errorMsg = "项目文件不兼容！";
                MessageBox.Show(errorMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 项目发布
        /// </summary>
        private void PublishKnxUiProject()
        {
            try
            {
                if (ProjectFile != null)
                {
                    #region 第一步： 保存knx项目文件
                    SaveKnxUiProject(ProjectFile);
                    #endregion

                    #region 第二步： 复制项目文件到发布目录
                    var publishProjectFile = Path.Combine(MyCache.DefaultKnxPublishFolder, Path.GetFileName(ProjectFile));
                    File.Copy(ProjectFile, publishProjectFile, true);
                    #endregion

                    #region 第三步： 启动Web服务器，提供给用户下载
                    var argsString = new StringBuilder(213);
                    argsString.AppendFormat(@" /a:{0}", MyCache.DefaultKnxPublishFolder);
                    argsString.Append(@" /pm:Specific /p:80");

                    var cassiniDevExe = Path.Combine(Path.Combine(Application.StartupPath, "Cassini"), @"CassiniDev.exe");

                    // 
                    if (File.Exists(cassiniDevExe) == true)
                    {
                        if (File.Exists(publishProjectFile) == true)
                        {
                            // 启动当前的程序
                            Process proc = new Process();
                            proc.StartInfo.CreateNoWindow = true;
                            proc.StartInfo.UseShellExecute = true;
                            proc.StartInfo.FileName = cassiniDevExe;
                            proc.StartInfo.Arguments = argsString.ToString();
                            proc.Start();
                        }
                        else
                        {
                            string errorMsg = "找不到项目文件！ 项目文件： " + publishProjectFile;
                            MessageBox.Show(errorMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Log.Error(errorMsg);
                        }
                    }
                    else
                    {
                        string errorMsg = "找不到启动程序！ 启动程序： " + cassiniDevExe;
                        MessageBox.Show(errorMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Log.Error(errorMsg);

                    }
                    #endregion

                    #region 显示下载地址
                    //var ipAddress = GetAddressIP();
                    //var downloadUrl = string.Format("http://{0}/", ipAddress);
                    //var downloadForm = new FrmDownloadUrl { DownloadUri = downloadUrl };
                    //downloadForm.ShowDialog(this);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "项目发布不成功！";
                MessageBox.Show(errorMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
        }


        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        public string GetAddressIp()
        {
            string addressIp = string.Empty;
            foreach (IPAddress ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    addressIp = ipAddress.ToString();
                }
            }

            return addressIp;
        }

        private void ExitApp()
        {
            //// 删除临时目录
            //if (Directory.Exists(MyCache.DefaultKnxCacheFolder))
            //{
            //    FileHelper.DeleteFolder(MyCache.DefaultKnxCacheFolder);
            //}

            Application.Exit();
        }

        /// <summary>
        /// 设置 Treeview 工具栏按钮的状态
        /// </summary>
        /// <param name="selectedNode"></param>
        private void CheckBarStatus(TreeNode selectedNode)
        {
            ToolBarStatus status = new ToolBarStatus();

            if (selectedNode.PrevNode != null)
            {
                status.moveup = true;
            }
            if (selectedNode.NextNode != null)
            {
                status.movedown = true;
            }

            if (selectedNode.Name == MyConst.View.KnxAppType)
            {
                status.area = true;
            }

            if (selectedNode.Name == MyConst.View.KnxAreaType)
            {
                status.room = true;
            }

            if (selectedNode.Name == MyConst.View.KnxRoomType)
            {
                status.page = true;
            }

            if (selectedNode.Name == MyConst.View.KnxPageType)
            {
                status.grid = true;
                status.control = true;
                status.priview = true;
                status.btnPriview = true;
            }

            if (selectedNode.Name == MyConst.View.KnxGridType)
            {
                status.control = true;
            }

            if (tvwAppdata.Nodes.Count > 0)
            {
                status.collapse = true;
                status.expand = true;
                status.searchBox = true;
                status.importKnx = true;
            }

            SetButtonStatus(status);
        }

        private void SetButtonStatus(ToolBarStatus status)
        {
            this.tsrBtnExpandAll.Enabled = status.expand;
            this.tsrBtnCollapseAll.Enabled = status.collapse;
            this.tsrBtnMoveUp.Enabled = status.moveup;
            this.tsrBtnMoveDown.Enabled = status.movedown;
            this.tsrBtnAddArea.Enabled = status.area;
            this.tsrBtnAddRoom.Enabled = status.room;
            this.tsrBtnAddPage.Enabled = status.page;
            this.tsrBtnAddGrid.Enabled = status.grid;
            this.tsrdpbtnAddContol.Enabled = status.control;
            this.btnImportKNXFile.Enabled = status.importKnx;
            this.btnPreview.Enabled = status.btnPriview;
        }

        /// <summary>
        /// 节点向下移动
        /// </summary>
        /// <param name="selectedNode"></param>
        private void NodeMoveDown(TreeNode selectedNode)
        {
            //向下移动一个树节点
            if (selectedNode.NextNode != null && selectedNode.Parent != null)
            {
                var pNode = selectedNode.Parent;
                var index = selectedNode.NextNode.Index;
                pNode.Nodes.RemoveAt(selectedNode.Index);
                pNode.Nodes.Insert(index, selectedNode);
                this.tvwAppdata.SelectedNode = selectedNode;
            }

            Saved = false;
        }

        private void NodeMoveUp(TreeNode selectedNode)
        {
            // 向上移动一个数节点
            if (selectedNode.PrevNode != null && selectedNode.Parent != null)
            {
                var pNode = selectedNode.Parent;
                var index = selectedNode.PrevNode.Index;
                pNode.Nodes.RemoveAt(selectedNode.Index);
                pNode.Nodes.Insert(index, selectedNode);
                this.tvwAppdata.SelectedNode = selectedNode;
            }

            Saved = false;
        }

        #endregion

        private void tstbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TreeNode tnRet = null;
                var name = "test";
                foreach (TreeNode tn in tvwAppdata.Nodes)
                {
                    tnRet = FindNode(tn, name);
                    if (tnRet != null) { break; }
                }

                if (tnRet != null)
                {
                    tvwAppdata.SelectedNode = tnRet;
                }

                tvwAppdata.Focus();
            }
        }

        /// <summary>
        /// 在树上搜索节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private TreeNode FindNode(TreeNode parentNode, string strValue)
        {
            // 如果当前节点为空，返回
            if (parentNode == null) { return null; }

            //
            if (parentNode.Text.Contains(strValue)) { return parentNode; }

            //
            if (parentNode.Nodes.Count == 0) { return null; }

            TreeNode currentNode, currentPartentNode;

            //Init node
            currentPartentNode = parentNode;
            currentNode = currentPartentNode.FirstNode;

            while (currentNode != null && currentNode != parentNode)
            {
                while (currentNode != null)
                {
                    if (currentNode.Text.Contains(strValue)) { return currentNode; }
                    if (currentNode.Nodes.Count > 0)
                    {
                        currentPartentNode = currentNode;
                        currentNode = currentNode.FirstNode;
                    }

                    else if (currentNode != currentPartentNode.LastNode)
                    {
                        currentNode = currentNode.NextNode;
                    }
                    else { break; }
                }

                while (currentNode != parentNode && currentNode == currentPartentNode.LastNode)
                {
                    currentNode = currentPartentNode;
                    currentPartentNode = currentPartentNode.Parent;
                }

                if (currentNode != parentNode) { currentNode = currentNode.NextNode; }
            }

            return null;
        }

        private void tsmiAddImageButton_Click(object sender, EventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            AddImageButtonNode(selectedNode);
        }

        #region "控制树节点移动,向左右下上"
        /// <summary>
        /// 通过Ctrl+键盘移动选定的树节点
        /// </summary>
        /// <param name="TreeView1">要编辑的TreeView控件</param>
        /// <param name="eKeys">The <see cref="System.Windows.Forms.KeyEventArgs"/>KeyEventArgs为按键事件提供数据</param>
        public void MoveSelectNode(TreeView TreeView1, KeyEventArgs eKeys)
        {
            try
            {
                if (TreeView1.SelectedNode == null)
                {
                    return;
                }

                try
                {
                    if (eKeys.KeyCode == Keys.Up && eKeys.Control == true)
                    {
                        TreeNode TN = new TreeNode();
                        TN = TreeView1.SelectedNode;
                        TreeNode TempNode = (TreeNode)TreeView1.SelectedNode.Clone();
                        if (TreeView1.SelectedNode.PrevNode == null)
                        {
                            return;

                        }
                        if (TN.Parent == null)
                        {
                            TreeView1.Nodes.Insert(TN.Index - 1, TempNode);
                        }
                        else
                        {
                            TN.Parent.Nodes.Insert(TN.Index - 1, TempNode);
                        }

                        TreeView1.SelectedNode.Remove();
                        TreeView1.SelectedNode = TempNode;
                    }

                    else if (eKeys.KeyCode == Keys.Down && eKeys.Control == true)
                    {
                        TreeNode TN = new TreeNode();
                        TN = TreeView1.SelectedNode;
                        TreeNode TempNode = (TreeNode)TreeView1.SelectedNode.Clone();
                        if (TreeView1.SelectedNode.NextNode == null)
                        {
                            return;
                        }
                        if (TN.Parent == null)
                        {
                            TreeView1.Nodes.Insert(TN.Index + 2, TempNode);
                        }
                        else
                        {
                            TN.Parent.Nodes.Insert(TN.Index + 2, TempNode);
                        }
                        TreeView1.SelectedNode.Remove();
                        TreeView1.SelectedNode = TempNode;
                    }
                    else if (eKeys.KeyCode == Keys.Left && eKeys.Control == true)
                    {
                        TreeNode TN = new TreeNode();
                        TN = TreeView1.SelectedNode;
                        TreeNode TempNode = (TreeNode)TreeView1.SelectedNode.Clone();
                        if (TreeView1.SelectedNode.Parent == null)
                        {
                            return;
                        }
                        else
                        {
                            if (TreeView1.SelectedNode.Parent.Parent == null)
                            {
                                TreeView1.Nodes.Add(TempNode);
                            }

                            else
                            {
                                TN.Parent.Parent.Nodes.Add(TempNode);
                            }
                        }
                        TN.Remove();
                        TreeView1.SelectedNode = TempNode;
                    }
                    else if (eKeys.KeyCode == Keys.Right && eKeys.Control == true)
                    {
                        TreeNode TN = new TreeNode();
                        TN = TreeView1.SelectedNode;
                        TreeNode TempNode = (TreeNode)TreeView1.SelectedNode.Clone();
                        if (TreeView1.SelectedNode.NextNode == null)
                        {
                            return;
                        }
                        TN.NextNode.Nodes.Insert(0, TempNode);
                        TN.Remove();
                        TreeView1.SelectedNode = TempNode;
                    }
                }
                catch
                {

                }

            }
            catch
            { }

        }

        #endregion


        private void tvwAppdata_KeyDown(object sender, KeyEventArgs e)
        {
            var selectedNode = tvwAppdata.SelectedNode;
            if (selectedNode != null)
            {
                switch (e.KeyCode)
                {
                    // 删除当前节点
                    case Keys.Delete:
                        DeleteSelectedNode(selectedNode);
                        break;
                    // Counts the ENTER keys.
                    case Keys.Insert:
                        MessageBox.Show("new item");
                        break;
                }
            }
        }

        private void tsrdpbtnAddContol_Click(object sender, EventArgs e)
        {

        }
    }
}



