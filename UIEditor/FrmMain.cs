using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using NLog;
using SourceGrid;
using SourceGrid.Cells.Views;
using UIEditor.Component;
using UIEditor.Entity;
using UIEditor.Entity.Control;
using ColumnHeader = SourceGrid.Cells.ColumnHeader;
using Structure;
using System.Drawing.Drawing2D;
using UIEditor.SationUIControl;
using System.Globalization;
using System.Threading;
using System.ComponentModel;
using UIEditor.CommandManager;

namespace UIEditor
{

    public partial class FrmMain : Form
    {
        #region 常量
        private const string KnxFilter = "KNX UI metadata files (*.knxuie)|*.knxuie|All files (*.*)|*.*";
        #endregion

        #region 变量
        // 文件是否保存
        private bool _saved = true;
        public bool Saved
        {
            get { return _saved; }
            set { _saved = value; }
        }

        // 保存文件名
        private string _projectFile = "";
        public string ProjectFile
        {
            get { return _projectFile; }
            set { _projectFile = value; }
        }

        private PageNode curPageNode; // 选中的页面节点
        private TreeNode curSelectedNode = null;    // 选中的节点
        private STPage curSTPage;  // 当前的页面Panel，与选中的页面节点对应

        private CommandQuene cqdo;
        private KeyboardHook k_hook;
        private bool CtrlDown = false;
        // 日志
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region 窗体构造函数
        public FrmMain()
        {
            string localize = AppConfigHelper.GetAppConfig(MyConst.XmlTagAppLanguange);
            if (!string.IsNullOrWhiteSpace(localize))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(localize);
            }

            InitializeComponent();

            CheckTabControl();

            this.ucdo.SelectedNodeChange += new UIEditor.UserUIControl.UCDocumentOutline.SelectedNodeChangeEvent(this.ucdo_SelectedNodeChanged);
            this.ucdo.AddNode += new UIEditor.UserUIControl.UCDocumentOutline.AddNodeEvent(this.ucdo_AddNode);
            this.ucdo.RemoveNode += new UIEditor.UserUIControl.UCDocumentOutline.RemoveNodeEvent(this.ucdo_RemoveNode);
            this.ucdo.TreeViewChangedEvent += new UIEditor.UserUIControl.UCDocumentOutline.TreeViewChangedEventDelegate(ucdo_TreeViewChangedEvent);
            this.ucProperty.NodePropertyChange += new UIEditor.UserUIControl.UCProperty.NodePropertyChangeEvent(this.ucProperty_NodePropertyChanged);

            this.k_hook = new KeyboardHook();
            this.k_hook.KeyDownEvent += new KeyEventHandler(tabControl_KeyDown);//钩住键按下
            this.k_hook.KeyUpEvent += new KeyEventHandler(tabControl_KeyUp);
            this.k_hook.Start();//安装键盘钩子 
        }
        #endregion

        #region 窗体事件
        private void FrmMain_Load(object sender, EventArgs e)
        {
            SetToolStripButtonStatus(false);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            this.k_hook.Stop();

            if (!this.Saved)
            {
                var result = MessageBox.Show(ResourceMng.GetString("Message7"), ResourceMng.GetString("Message4"), MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == result)
                {
                    SaveKnxUiProject(ProjectFile);
                }
            }

            Cursor = Cursors.Default;
        }
        #endregion

        #region
        private void SetToolStripButtonStatus(bool enable)
        {
            this.tsrBtnGroupBox.Enabled = enable;
            this.tsrBtnAddBlinds.Enabled = enable;
            this.tsrBtnAddLabel.Enabled = enable;
            this.tsrBtnAddSceneButton.Enabled = enable;
            this.tsrBtnAddSliderSwitch.Enabled = enable;
            this.tsrBtnAddSwitch.Enabled = enable;
            this.tsrBtnAddValueDisplay.Enabled = enable;
            this.tsrBtnAddTimerButton.Enabled = enable;
            this.tsrBtnDigitalAdjustment.Enabled = enable;
        }

        private void SetToolStripButtonKNXAddrStatus(bool enable)
        {
            this.tsbKNXAddr.Enabled = enable;
        }

        private void SetToolStripButtonSaveStatus(bool enable)
        {
            this.tsbSaveProj.Enabled = enable;
        }

        private void ResetParameter()
        {
            this.curPageNode = null;
            this.curSelectedNode = null;
            if (null != this.curSTPage)
            {
                this.curSTPage.Parent.Controls.Remove(this.curSTPage);
                this.curSTPage = null;
            }

            //this.curSTPanel = null;
            //this.curFrameControl = null;

            MyCache.ResetVariable();
        }
        #endregion

        #region
        private void SetProjectOutline(AppNode node)
        {
            this.ucdo.SetOutlineNode(node);
            this.ucdo.Title = ResourceMng.GetString("DocumentOutline") + "-" + node.Text;
        }

        private void CreateCommandManager()
        {
            this.cqdo = new CommandQuene();
            this.cqdo.UndoStateChanged += new CommandQuene.CommandQueueChangedEvent(CommandManager_UndoStateChanged);
            this.cqdo.ReverseUndoStateChanged += new CommandQuene.CommandQueueChangedEvent(CommandManager_ReverseStateChanged);
            this.ucdo.cqdo = this.cqdo;
            this.ucProperty.cqp = this.cqdo;
        }

        private bool ErgodicPageNode(TreeNode p, PageNode pageNode)
        {
            foreach (TreeNode c in p.Nodes)
            {
                ViewNode cNode = c as ViewNode;
                if (MyConst.View.KnxPageType == cNode.Name)
                {
                    if (cNode.Id == pageNode.Id)
                    {
                        return true;
                    }
                }
                else if ((MyConst.View.KnxAppType == cNode.Name) || (MyConst.View.KnxAreaType == cNode.Name) || (MyConst.View.KnxRoomType == cNode.Name))
                {
                    if (ErgodicPageNode(cNode, pageNode))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void UndoRedo()
        {
            TreeView tv = this.ucdo.GetTreeView();
            if (tv.Nodes.Count > 0)
            {
                foreach (TabPage tabPage in this.tabControl.TabPages)
                {
                    bool b = false;
                    foreach (TreeNode node in tv.Nodes)
                    {
                        b = ErgodicPageNode(node, tabPage.Tag as PageNode);
                        if (b)
                        {
                            break;
                        }
                    }

                    if (b)
                    {
                        TabPage page = this.tabControl.SelectedTab;
                        if (page == tabPage)
                        {
                            PageNode pageNode = page.Tag as PageNode;
                            STPage panel = pageNode.panel as STPage;
                            panel.Refresh();
                        }
                    }
                    else
                    {
                        this.tabControl.TabPages.Remove(tabPage);
                    }
                }
            }
        }
        #endregion

        #region 事件通知
        private void ucdo_SelectedNodeChanged(object sender, EventArgs e)
        {
            ViewNode node = sender as ViewNode;
            if (this.curSelectedNode == node)
            {
                return;
            }

            this.curSelectedNode = node;

            this.ucProperty.DisplayNode(node);

            switch (node.Name)
            {
                case MyConst.View.KnxAppType:
                case MyConst.View.KnxAreaType:
                case MyConst.View.KnxRoomType:
                    //SetToolStripButtonStatus(false);
                    //this.tabControl.Enabled = false;
                    break;
                case MyConst.View.KnxPageType:
                    SetToolStripButtonStatus(true);
                    this.tabControl.Enabled = true;
                    CreateTabPage(node as PageNode);
                    break;

                default:
                    PageNode pageNode = GetPageNodeFromParent(node);
                    if (null != pageNode)
                    {
                        SetToolStripButtonStatus(true);
                        this.tabControl.Enabled = true;

                        CreateTabPage(pageNode);

                        STPage pagePanel = pageNode.panel as STPage;
                        pagePanel.SetSelectedControl(node);
                    }
                    break;
            }
        }

        private void ucdo_AddNode(object sender, EventArgs e)
        {
            ViewNode node = sender as ViewNode;
            if ((MyConst.View.KnxAppType == node.Name) || (MyConst.View.KnxAreaType == node.Name) ||
                (MyConst.View.KnxRoomType == node.Name) || (MyConst.View.KnxPageType == node.Name))
            {
                return;
            }

            PageNode pageNode = GetPageNodeFromParent(node);
            STPage pagePanel = pageNode.panel as STPage;

            pagePanel.AddControl(node);
        }

        private void ucdo_RemoveNode(object sender, EventArgs e)
        {
            ViewNode node = sender as ViewNode;
            switch (node.Name)
            {
                case MyConst.View.KnxAppType:
                    //SetToolStripButtonKNXAddrStatus(false);
                    //SetToolStripButtonStatus(false);
                    break;

                //case MyConst.View.KnxAreaType:
                //case MyConst.View.KnxRoomType:
                //case MyConst.View.KnxPageType:
                default:
                    UndoRedo();
                    CheckTabControl();
                    break; 
            }
        }

        private void ucdo_TreeViewChangedEvent(object sender, EventArgs e)
        {
            ViewNode node = sender as ViewNode;
            ProjectChanged(node);
        }

        private void ucProperty_NodePropertyChanged(object sender, EventArgs e)
        {
            this.Saved = false;

            ViewNode node = sender as ViewNode;

            if (null != (node as PageNode))
            {
                STPage pagePanel = node.panel as STPage;
                pagePanel.ChangeSize();
            }
            else
            {
                PageNode pageNode = GetPageNodeFromParent(node);
                if (null != pageNode)
                {
                    STPage pagePanel = pageNode.panel as STPage;
                    pagePanel.ControlPropertyChanged(node);
                }
            }

            ProjectChanged(node);
        }

        private void STPage_ControlChangedEvent(object sender, EventArgs e)
        {
            ViewNode node = sender as ViewNode;
            if (null != node)
            {
                SetSelectedNode(node);
            }
        }

        private void STPage_PageChangedEvent(object sender, EventArgs e)
        {
            ViewNode node = sender as ViewNode;
            ProjectChanged(node);
        }

        private PageNode GetPageNodeFromParent(TreeNode node)
        {
            TreeNode parentNode = node.Parent;
            if (null != parentNode)
            {
                if (MyConst.View.KnxPageType == parentNode.Name)
                {
                    return parentNode as PageNode;
                }
                else
                {
                    return GetPageNodeFromParent(parentNode);
                }
            }
            else
            {
                return null;
            }
        }

        private void CommandManager_UndoStateChanged(object sender, CommandEventArgs e)
        {
            this.tsbUndo.Enabled = e.Valid;
        }

        private void CommandManager_ReverseStateChanged(object sender, CommandEventArgs e)
        {
            this.tsbRedo.Enabled = e.Valid;
        }

        private void ProjectChanged(ViewNode node)
        {
            this.Saved = false;

            switch (node.Name)
            {
                case MyConst.View.KnxAppType:
                case MyConst.View.KnxAreaType:
                case MyConst.View.KnxRoomType:
                    break;

                case MyConst.View.KnxPageType:
                    SetTabPageTitle(node.Id.ToString(), node.Text + " " + "*");
                    break;

                default:
                    PageNode pageNode = GetPageNodeFromParent(node);
                    if (null != pageNode)
                    {
                        SetTabPageTitle(pageNode.Id.ToString(), pageNode.Text + " " + "*");
                    }
                    break;
            }
        }
        #endregion

        #region
        private void NewKnxUiProject()
        {
            if (Saved == false/* && this.tvwAppdata.Nodes.Count > 0*/)
            {
                var result = MessageBox.Show(ResourceMng.GetString("Message7"), ResourceMng.GetString("Message4"), MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    SaveKnxUiProject(ProjectFile);
                }
                else if (DialogResult.No == result)
                {
                    AddAppNode();
                }
            }
            else
            {
                AddAppNode();
            }
        }

        private void OpenKnxUiPrject()
        {
            Cursor = Cursors.WaitCursor;

            if (Saved == false)
            {
                var result = MessageBox.Show(ResourceMng.GetString("Message7"), ResourceMng.GetString("Message4"), MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == result)
                {
                    SaveKnxUiProject(ProjectFile);
                    Cursor = Cursors.Default;
                    return;
                }
                else if (DialogResult.No == result)
                {

                }
                else if (DialogResult.Cancel == result)
                {
                    Cursor = Cursors.Default;
                    return;
                }
            }

            try
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
                            AppNode appNode = FrmMainHelp.ImportNode(app);
                            SetProjectOutline(appNode);

                            ProjectFile = ofd.FileName;
                            ShowProjectFile(ProjectFile);

                            // 
                            //ToolBarStatus status = new ToolBarStatus { collapse = true, expand = true, searchBox = true, importKnx = true };
                            //SetButtonStatus(status);
                            SetToolStripButtonStatus(false);
                            SetToolStripButtonKNXAddrStatus(true);
                            SetToolStripButtonSaveStatus(true);

                            ResetParameter();
                            CreateCommandManager();
                            CloseAllTabPages();

                            Saved = true;
                        }
                        else
                        {
                            throw new ApplicationException(ResourceMng.GetString("Message8"));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ResourceMng.GetString("Message8");
                MessageBox.Show(errorMsg, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool SaveKnxUiProject(string fileName)
        {
            Cursor = Cursors.WaitCursor;
            bool result = false;

            try
            {
                // 当前的项目不为空
                //if (this.tvwAppdata.Nodes.Count > 0)
                //{

                VersionStorage.Save(); // 保存项目文件的版本信息。
                this.ucdo.SaveNode(); // 保存界面到JSON文件
                GroupAddressStorage.Save(); // 保存组地址到JSON文件

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
            //    else
            //    {
            //        string errorMsg = ResourceMng.GetString("Message3");
            //        MessageBox.Show(errorMsg, ResourceMng.GetString("Message4"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            catch (Exception ex)
            {
                string errorMsg = ResourceMng.GetString("Message5") + " " + "exception message: " + ex.Message;
                MessageBox.Show(errorMsg, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            if (result) // 保存成功
            {
                ProjectSaved();
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
                    //frm.Show();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ResourceMng.GetString("Message12");
                MessageBox.Show(errorMsg, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }
        }
        #endregion

        #region 新建 AppNode
        private void AddAppNode()
        {
            ProjectFile = MyConst.DefaultKnxUiProjectName;
            tsslblProjectName.Text = string.Format("Project Name: {0}", ProjectFile);

            CreateProjectFolder();

            ResetParameter();
            CreateCommandManager();
            CloseAllTabPages();

            this.ucdo.AddAppNode();

            //ToolBarStatus status = new ToolBarStatus { collapse = true, expand = true, searchBox = true, importKnx = true };
            //SetButtonStatus(status);
            SetToolStripButtonStatus(false);
            SetToolStripButtonKNXAddrStatus(true);
            SetToolStripButtonSaveStatus(true);
        }
        #endregion

        #region ToolStripButton点击事件 工程
        private void tsbNewProj_Click(object sender, EventArgs e)
        {
            NewKnxUiProject();
        }

        private void tsbOpenProj_Click(object sender, EventArgs e)
        {
            OpenKnxUiPrject();
        }

        private void tsbSaveProj_Click(object sender, EventArgs e)
        {
            SaveKnxUiProject(ProjectFile);
        }
        #endregion

        #region ToolStripButton点击事件 撤销、重做
        private void tsbUndo_Click(object sender, EventArgs e)
        {
            this.Undo();
        }

        private void tsbRedo_Click(object sender, EventArgs e)
        {
            this.Redo();
        }
        #endregion

        #region ToolStripButton点击事件 添加控件
        private void tsrBtnGroupBox_Click(object sender, EventArgs e)
        {
            STPage.ToAddControl = typeof(GroupBoxNode);
        }

        private void tsrBtnAddBlinds_Click(object sender, EventArgs e)
        {
            STPage.ToAddControl = typeof(BlindsNode);
        }

        private void tsrBtnAddLabel_Click(object sender, EventArgs e)
        {
            STPage.ToAddControl = typeof(LabelNode);
        }

        private void tsrBtnAddSceneButton_Click(object sender, EventArgs e)
        {
            STPage.ToAddControl = typeof(SceneButtonNode);
        }

        private void tsrBtnAddSliderSwitch_Click(object sender, EventArgs e)
        {
            STPage.ToAddControl = typeof(SliderSwitchNode);
        }

        private void tsrBtnAddSwitch_Click(object sender, EventArgs e)
        {
            STPage.ToAddControl = typeof(SwitchNode);
        }

        private void tsrBtnAddValueDisplay_Click(object sender, EventArgs e)
        {
            STPage.ToAddControl = typeof(ValueDisplayNode);
        }

        private void tsrBtnAddTimerButton_Click(object sender, EventArgs e)
        {
            STPage.ToAddControl = typeof(TimerButtonNode);
        }

        private void tsrBtnDigitalAdjustment_Click(object sender, EventArgs e)
        {
            STPage.ToAddControl = typeof(DigitalAdjustmentNode);
        }
        #endregion

        #region ToolStripButton点击事件 KNX地址
        private void tsbKNXAddr_Click(object sender, EventArgs e)
        {
            OpenGroupAddressMgr();
        }
        #endregion

        #region TabControl
        #region TabControl 右键菜单
        private ToolStripMenuItem CreateCloseMenuItem()
        {
            ToolStripMenuItem tsmiCloseItem = new ToolStripMenuItem();
            tsmiCloseItem.Name = "tsmiCloseItem";
            //tsmiCloseItem.Size = new System.Drawing.Size(100, 22);
            tsmiCloseItem.Text = ResourceMng.GetString("Close");
            tsmiCloseItem.Click += new System.EventHandler(CloseTabPage_Click);

            return tsmiCloseItem;
        }

        private ToolStripMenuItem CreateCloseAllMenuItem()
        {
            ToolStripMenuItem tsmiCloseAllItem = new ToolStripMenuItem();
            tsmiCloseAllItem.Name = "tsmiCloseAllItem";
            //tsmiCloseAllItem.Size = new System.Drawing.Size(100, 22);
            tsmiCloseAllItem.Text = ResourceMng.GetString("CloseAll");
            tsmiCloseAllItem.Click += new System.EventHandler(CloseAllTabPage_Click);

            return tsmiCloseAllItem;
        }

        private ToolStripMenuItem CreateCloseOthersMenuItem()
        {
            ToolStripMenuItem tsmiCloseOthersItem = new ToolStripMenuItem();
            tsmiCloseOthersItem.Name = "tsmiCloseOthersItem";
            //tsmiCloseOthersItem.Size = new System.Drawing.Size(100, 22);
            tsmiCloseOthersItem.Text = ResourceMng.GetString("CloseOthers");
            tsmiCloseOthersItem.Click += new System.EventHandler(CloseOthersTabPage_Click);

            return tsmiCloseOthersItem;
        }
        #endregion

        #region TabControl 右键菜单事件
        private void CloseTabPage_Click(object sender, EventArgs e)
        {
            this.tabControl.SelectedTab.Dispose();

            CheckTabControl();
        }

        private void CloseAllTabPage_Click(object sender, EventArgs e)
        {
            CloseAllTabPages();
        }

        private void CloseOthersTabPage_Click(object sender, EventArgs e)
        {
            TabPage page = this.tabControl.SelectedTab;
            foreach (TabPage tp in this.tabControl.TabPages)
            {
                if (tp != page)
                {
                    this.tabControl.TabPages.Remove(tp);
                }
            }
        }
        #endregion

        private void CloseAllTabPages()
        {
            this.tabControl.TabPages.Clear();

            CheckTabControl();
        }

        private void CloseTabPage(string pageId)
        {
            TabPage page = this.tabControl.TabPages[pageId];
            if (null != page)
            {
                this.tabControl.TabPages.Remove(page);
            }
        }

        private void CheckTabControl()
        {
            if (this.tabControl.TabPages.Count > 0)
            {
                this.tabControl.Visible = true;
            }
            else
            {
                this.tabControl.Visible = false;
            }
        }

        private void CreateTabPage(PageNode node)
        {
            TabPage page = this.tabControl.TabPages[node.Id.ToString()];
            if (null != page)
            {
                this.tabControl.SelectedTab = page;
            }
            else
            {
                this.tabControl.TabPages.Add(node.Id.ToString(), node.Text);
                page = this.tabControl.TabPages[node.Id.ToString()];
                ContextMenuStrip cms = new ContextMenuStrip();
                page.ContextMenuStrip = cms;
                page.AutoScroll = true;
                
                STPage panel = new STPage(node);
                panel.cqp = this.cqdo;
                panel.ControlSelectedEvent += new UIEditor.SationUIControl.STPage.ControlSelectedEventDelegate(this.STPage_ControlChangedEvent);
                //panel.ControlDeleteEvent += new UIEditor.SationUIControl.STPage.ControlDeleteEventDelegate(this.STPage_ControlDeleteEvent);
                panel.PageChangedEvent += new UIEditor.SationUIControl.STPage.PageChangedEventDelegate(this.STPage_PageChangedEvent);
                page.Controls.Add(panel);
            }

            page.Tag = node;
            this.tabControl.SelectedTab = page;
            this.curPageNode = node;
            this.curSTPage = node.panel as STPage;

            CheckTabControl();
        }

        private void SetTabPageTitle(string pageId, string title)
        {
            TabPage page = this.tabControl.TabPages[pageId];
            if (null != page)
            {
                page.Text = title;
            }
        }

        private void PageSaved()
        {
            foreach (TabPage page in this.tabControl.TabPages)
            {
                ViewNode node = page.Tag as ViewNode;
                page.Text = node.Text;

                STPage panel = node.panel as STPage;
                panel.Saved();
            }
        }

        #region TabControl 事件
        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < this.tabControl.TabPages.Count; i++)
                {
                    TabPage tp = this.tabControl.TabPages[i];
                    if (this.tabControl.GetTabRect(i).Contains(new Point(e.X, e.Y)))
                    {
                        this.tabControl.SelectedTab = tp;
                        break;
                    }
                }

                ContextMenuStrip cms = new ContextMenuStrip();
                cms.Items.Add(CreateCloseMenuItem());
                cms.Items.Add(CreateCloseAllMenuItem());
                cms.Items.Add(CreateCloseOthersMenuItem());
                this.tabControl.ContextMenuStrip = cms;  //弹出菜单
            }
        }

        private void tabControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool contains = false;

            for (int i = 0; i < this.tabControl.TabPages.Count; i++)
            {
                TabPage tp = this.tabControl.TabPages[i];
                if (this.tabControl.GetTabRect(i).Contains(new Point(e.X, e.Y)))
                {
                    contains = true;
                    break;
                }
            }

            if (!contains)
            {
                this.tabControl.ContextMenuStrip = null;  //离开选项卡后 取消菜单
            }
        }

        private void tabControl_KeyDown(object sender, KeyEventArgs e)
        {

            if ((int)Keys.LControlKey == e.KeyValue)
            {
                this.CtrlDown = true;
            }

            if (this.CtrlDown && ((int)Keys.Z == e.KeyValue)) // 撤销
            {
                this.Undo();
            }
            else if (this.CtrlDown && ((int)Keys.Y == e.KeyValue)) // 重做
            {
                this.Redo();
            }
            else if (this.CtrlDown && ((int)Keys.S == e.KeyValue)) // 保存
            {
                this.SaveKnxUiProject(ProjectFile);
            }
            else
            {
                if (this.tabControl.TabCount > 0)
                {
                    TabPage tabPage = this.tabControl.SelectedTab;
                    PageNode node = tabPage.Tag as PageNode;
                    STPage panel = node.panel as STPage;
                    panel.KeyDowns(e);
                }
            }
        }

        private void tabControl_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)Keys.LControlKey == e.KeyValue)
            {
                this.CtrlDown = false;
            }

            if (this.tabControl.TabCount > 0)
            {
                TabPage tabPage = this.tabControl.SelectedTab;
                PageNode node = tabPage.Tag as PageNode;
                STPage panel = node.panel as STPage;
                panel.KeyUps(e);
            }
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            TabPage page = e.TabPage;
            if (null != page)
            {
                PageNode node = page.Tag as PageNode;
                this.curSTPage = node.panel as STPage;
                SetSelectedNode(node);
            }
        }
        #endregion
        #endregion

        private void SetSelectedNode(ViewNode node)
        {
            //if (MyConst.View.KnxPageType == node.Name)
            //{
            //    SetToolStripButtonStatus(true);
            //}

            this.curSelectedNode = node;
            this.ucdo.SetSelectedNode(node);
            this.ucProperty.DisplayNode(node);
        }

        private void Undo()
        {
            this.ucdo.Undo();

            UndoRedo();

            CheckTabControl();
        }

        private void Redo()
        {
            this.ucdo.Redo();

            UndoRedo();

            CheckTabControl();
        }

        private void ProjectSaved()
        {
            PageSaved();

            this.Saved = true;
        }
    }
}