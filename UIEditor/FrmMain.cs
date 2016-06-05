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
        public bool groupbox;
        //public bool radioGroup;
        public bool control;
        public bool searchBox;
        public bool priview;
        public bool importKnx;
        //public bool btnPriview;
    }

    public partial class FrmMain : Form
    {
        #region 常量
        private const string KnxFilter = "KNX UI metadata files (*.knxuie)|*.knxuie|All files (*.*)|*.*";
        #endregion

        #region 变量
        // 文件是否保存
        private bool _saved = false;
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
        // 显示属性的网格
        private readonly Grid _gridProperty = new Grid();

        private ViewNode _tempCacheNode;
        // 日志
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private Point pPoint; //上个鼠标坐标
        private Point cPoint; //当前鼠标坐标
        private STPanel curSTPanel = null; //传入的控件
        private FrameControl curFrameControl = null;//边框控件
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

            InitMenuItem();

            InitGrid();
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

            this.curSTPanel = null;
            this.curFrameControl = null;

            MyCache.ResetVariable();
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
            if (Saved == false && this.tvwAppdata.Nodes.Count > 0)
            {
                var result = MessageBox.Show(ResourceMng.GetString("Message1"), ResourceMng.GetString("Message2"), MessageBoxButtons.YesNoCancel,
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

            ResetParameter();
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
                if (this.tvwAppdata.Nodes.Count > 0)
                {
                    // 保存项目文件的版本信息。
                    VersionStorage.Save();

                    // 保存当前树上的节点为 json 文件
                    var app = FrmMainHelp.ExportTreeView(this.tvwAppdata);

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
                    string errorMsg = ResourceMng.GetString("Message3");
                    MessageBox.Show(errorMsg, ResourceMng.GetString("Message4"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            if (Saved == false && this.tvwAppdata.Nodes.Count > 0)
            {
                //MessageBox.Show("当前项目未保存，请先保存当前的项目!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var result = MessageBox.Show(ResourceMng.GetString("Message7"), ResourceMng.GetString("Message4"), MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    SaveKnxUiProject(ProjectFile);
                }
                else if (DialogResult.No == result)
                {
                    AddAppNode(this.tvwAppdata);
                }
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

            if (Saved == false && this.tvwAppdata.Nodes.Count > 0)
            {
                //MessageBox.Show("当前项目未保存，请先保存当前的项目!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                //else
                //{
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
                            FrmMainHelp.ImportNode(app, this.tvwAppdata, Frm_ControlPropertiesChangedEvent);

                            ProjectFile = ofd.FileName;
                            ShowProjectFile(ProjectFile);

                            // 
                            ToolBarStatus status = new ToolBarStatus { collapse = true, expand = true, searchBox = true, importKnx = true };
                            SetButtonStatus(status);

                            ResetParameter();

                            Saved = true;
                        }
                        else
                        {
                            throw new ApplicationException(ResourceMng.GetString("Message8"));
                        }

                    }
                }
                //}

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
                            string errorMsg = ResourceMng.GetString("Message9") + " " + publishProjectFile;
                            MessageBox.Show(errorMsg, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Log.Error(errorMsg);
                        }
                    }
                    else
                    {
                        string errorMsg = ResourceMng.GetString("Message10") + " " + cassiniDevExe;
                        MessageBox.Show(errorMsg, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string errorMsg = ResourceMng.GetString("Message11");
                MessageBox.Show(errorMsg, ResourceMng.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                status.groupbox = true;
                status.control = true;
                status.priview = true;
                //status.btnPriview = true;

                var page = selectedNode as PageNode;
                if (page != this.curPageNode)
                {
                    this.curPageNode = page;
                    LoadPreview();
                }
                else
                {
                    //RefreshPreview();
                }
            }

            if (selectedNode.Name == MyConst.Controls.KnxGroupBoxType)
            {
                if (MyConst.Controls.KnxGroupBoxType == selectedNode.Parent.Name)
                {
                    status.groupbox = false;
                }
                else
                {
                    status.groupbox = true;
                }
                status.control = true;
            }

            //if (selectedNode.Name == MyConst.Controls.KnxRadioGroup)
            //{
            //    status.radioGroup = true;
            //}

            if (this.tvwAppdata.Nodes.Count > 0)
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
            this.tsrBtnGroupBox.Enabled = /*status.groupbox*/status.control;
            //this.tsrdpbtnAddContol.Enabled = status.control;
            this.tsrBtnAddBlinds.Enabled = status.control;
            //this.tsrBtnAddImageButton.Enabled = status.control;
            this.tsrBtnAddLabel.Enabled = status.control;
            //this.tsrBtnAddMediaButton.Enabled = status.control;
            //this.tsrBtnAddColorLight.Enabled = status.control;
            this.tsrBtnAddSceneButton.Enabled = status.control/* | status.radioGroup*/;
            //this.tsrBtnAddSIPCall.Enabled = status.control;
            //this.tsrBtnAddSlider.Enabled = status.control;
            this.tsrBtnAddSliderSwitch.Enabled = status.control;
            //this.tsrBtnAddSnapper.Enabled = status.control;
            //this.tsrBtnAddSnapperSwitch.Enabled = status.control;
            this.tsrBtnAddSwitch.Enabled = status.control;
            this.tsrBtnAddValueDisplay.Enabled = status.control;
            //this.tsrBtnAddWebcam.Enabled = status.control;
            this.tsrBtnAddTimerButton.Enabled = status.control;
            //this.tsrBtnAddTimerTaskListView.Enabled = status.control;
            this.tsrBtnDigitalAdjustment.Enabled = status.control;
            //this.tsrBtnRadioGroup.Enabled = status.control;

            //this.btnImportKNXFile.Enabled = status.importKnx;
            this.tsbKNXAddr.Enabled = status.importKnx;

            //this.btnPreview.Enabled = status.btnPriview;
            //LoadPreview();
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
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddAreaNode(selectedNode);
        }

        /// <summary>
        /// 添加 Room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRoomNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddRoomNode(selectedNode);
        }

        private void AddPageNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddPageNode(selectedNode);
        }

        private void AddGridNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddGroupBoxNode(selectedNode);
        }

        private void AddBlindsNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddBlindsNode(selectedNode);

            //LoadPreview();
        }

        private void AddLabelNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddLabelNode(selectedNode);

            //LoadPreview();
        }

        private void AddSceneButtonNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddSceneButtonNode(selectedNode);

            //LoadPreview();
        }

        private void AddSliderSwitchNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddSliderSwitchNode(selectedNode);

            //LoadPreview();
        }

        private void AddSwitchNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddSwitchNode(selectedNode);

            //LoadPreview();
        }

        private void AddValueDisplayNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddValueDisplayNode(selectedNode);

            //LoadPreview();
        }

        private void AddTimerButtonNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddTimerButtonNode(selectedNode);
        }

        private void AddDigitalAdjustmentNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            AddDigitalAdjustmenNode(selectedNode);
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
            if (selectedNode == curSelectedNode)
            {
                return;
            }

            if (selectedNode != null)
            {
                selectedNodeChanged(selectedNode);

                //if (null != controltable)
                //{
                //    var key = getHashTableKey(controltable, selectedNode);
                //    FrameControl fControl = key as FrameControl;
                //    if (null != fControl)
                //    {
                //        selectControl(fControl);
                //    }
                //}


            }
        }

        private void tvwAppdata_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.tvwAppdata.GetNodeAt(e.X, e.Y) != null)
                {
                    //this.tvwAppdata.SelectedNode = tvwAppdata.GetNodeAt(e.X, e.Y);
                    var selectedNode = this.tvwAppdata.GetNodeAt(e.X, e.Y);
                    selectedNodeChanged(selectedNode);
                }
            }
        }

        private void selectedNodeChanged(TreeNode newNode)
        {
            if (null == newNode)
            {
                return;
            }

            var node = newNode as ViewNode;
            if (null != node)
            {
                if (null != curSelectedNode)
                {
                    curSelectedNode.BackColor = this.tvwAppdata.BackColor;
                }
                this.curSelectedNode = node;
                this.tvwAppdata.SelectedNode = curSelectedNode;
                //curSelectedNode.Parent.Expand();
                this.tvwAppdata.SelectedNode.BackColor = System.Drawing.ColorTranslator.FromHtml("#3399FF");

                // 1. 设置界面 button
                CheckBarStatus(newNode);

                // 2. 显示当前节点的属性信息            
                _gridProperty.Rows.Clear();
                SetGridHeader();
                node.DisplayProperties(_gridProperty);

                #region 给节点绑定上下文菜单
                switch (curSelectedNode.Name)
                {
                    case MyConst.View.KnxAppType:
                        curSelectedNode.ContextMenuStrip = cmsAddArea;
                        break;

                    case MyConst.View.KnxAreaType:
                        curSelectedNode.ContextMenuStrip = cmsAddRoom;
                        break;

                    case MyConst.View.KnxRoomType:
                        curSelectedNode.ContextMenuStrip = cmsAddPage;
                        break;

                    case MyConst.View.KnxPageType:
                        curSelectedNode.ContextMenuStrip = cmsAddGroupBox;
                        break;

                    case MyConst.Controls.KnxGroupBoxType:
                        curSelectedNode.ContextMenuStrip = /*cmsAddControl*/cmsAddGroupBox;
                        break;

                    default:
                        curSelectedNode.ContextMenuStrip = cmsDeleteControl;
                        break;
                }
                #endregion
            }

            if (null != controltable)
            {
                var key = getHashTableKey(controltable, newNode);
                FrameControl fControl = key as FrameControl;
                if (null != fControl)
                {
                    selectControl(fControl);
                }
            }
        }

        private void tvwAppdata_KeyDown(object sender, KeyEventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
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
        #endregion

        #region 鼠标右键菜单和事件
        private void InitMenuItem()
        {
            this.cmsAddArea.Items.Add(CreatePasteMenuItem());
            this.cmsAddArea.Items.Add(CreateDeleteMenuItem());
            this.cmsAddRoom.Items.AddRange(CreateEditMenu());
            this.cmsAddPage.Items.AddRange(CreateEditMenu());
            this.cmsAddGroupBox.Items.AddRange(CreateEditMenu(true));
            this.cmsAddControl.Items.AddRange(CreateEditMenu(true));
            this.cmsDeleteControl.Items.AddRange(CreateEditMenu());
            this.cmsAddControl.Items.AddRange(CreateSubToolStripItem());
            //this.tsrdpbtnAddContol.DropDownItems.AddRange(CreateSubToolStripItem());
            this.cmsAddGroupBox.Items.AddRange(CreateSubToolStripItem());
        }

        /// <summary>
        /// 创建右键删除选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreateDeleteMenuItem()
        {
            ToolStripMenuItem tsmiDeleteItem = new ToolStripMenuItem();
            tsmiDeleteItem.Image = ResourceMng.GetImage("Delete_Control_16x16");
            tsmiDeleteItem.Name = "tsmiDeleteItem";
            tsmiDeleteItem.Size = new System.Drawing.Size(100, 22);
            tsmiDeleteItem.Text = ResourceMng.GetString("Delete");
            tsmiDeleteItem.Click += this.DeleteSelectNode_Click;

            return tsmiDeleteItem;
        }

        /// <summary>
        /// 创建右键粘贴选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreatePasteMenuItem()
        {
            // tsmiPasteNode
            // 
            ToolStripMenuItem tsmiPasteNode = new ToolStripMenuItem();
            tsmiPasteNode.Image = ResourceMng.GetImage("Paste_16x16");
            tsmiPasteNode.Name = "tsmiPasteNode";
            tsmiPasteNode.Size = new System.Drawing.Size(152, 22);
            tsmiPasteNode.Text = ResourceMng.GetString("Paste");
            tsmiPasteNode.Click += PasteNode_Click;

            return tsmiPasteNode;
        }

        /// <summary>
        /// 创建右键复制选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreateCopyMenuItem()
        {
            // 
            // tsmiCopyNode
            // 
            ToolStripMenuItem tsmiCopyNode = new ToolStripMenuItem();
            tsmiCopyNode.Image = ResourceMng.GetImage("Copy_16x16");
            tsmiCopyNode.Name = "tsmiCopyNode";
            tsmiCopyNode.Size = new System.Drawing.Size(152, 22);
            tsmiCopyNode.Text = ResourceMng.GetString("Copy");
            tsmiCopyNode.Click += CopyNode_Click;

            return tsmiCopyNode;
        }

        /// <summary>
        /// 创建右键剪切选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreateCutMenuItem()
        {
            // 
            // tsmiCutNode
            // 
            ToolStripMenuItem tsmiCutNode = new ToolStripMenuItem();
            tsmiCutNode.Image = ResourceMng.GetImage("Cut_16x16");
            tsmiCutNode.Name = "tsmiCutNode";
            tsmiCutNode.Size = new System.Drawing.Size(152, 22);
            tsmiCutNode.Text = ResourceMng.GetString("Cut");
            tsmiCutNode.Click += CutNode_Click;

            return tsmiCutNode;
        }

        /// <summary>
        /// 右键删除节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSelectNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            DeleteSelectedNode(selectedNode);
        }

        /// <summary>
        /// 右键粘贴节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                case MyConst.Controls.KnxGroupBoxType:
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
                case MyConst.Controls.KnxDigitalAdjustment:
                    {
                        if (selectedNode.Name == MyConst.View.KnxPageType || selectedNode.Name == MyConst.Controls.KnxGroupBoxType)
                        {
                            selectedNode.Nodes.Add(cloneNode);
                        }
                    }
                    break;
                default:
                    MessageBox.Show(ResourceMng.GetString("Message13"));
                    return;
                //break;
            }

            LoadPreview();
        }

        /// <summary>
        /// 右键剪切节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;
            _tempCacheNode = (ViewNode)selectedNode;
            selectedNode.Remove();

            LoadPreview();
        }

        /// <summary>
        /// 右键复制节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyNode_Click(object sender, EventArgs e)
        {
            var selectedNode = this.tvwAppdata.SelectedNode;

            _tempCacheNode = (ViewNode)selectedNode.Clone();

            LoadPreview();
        }

        /// <summary>
        ///  创建编辑的右键菜单， 复制、剪切、粘贴、删除
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

        /// <summary>
        /// 创建右键创建控件选项
        /// </summary>
        /// <returns></returns>
        private ToolStripItem[] CreateSubToolStripItem()
        {
            // 
            // tsmiAddBlinds
            // 
            ToolStripMenuItem tsmiAddBlinds = new ToolStripMenuItem();
            tsmiAddBlinds.Image = ResourceMng.GetImage("blinds");
            tsmiAddBlinds.Name = "tsmiAddBlinds";
            tsmiAddBlinds.Size = new System.Drawing.Size(152, 22);
            tsmiAddBlinds.Text = ResourceMng.GetString("TextBlinds");
            tsmiAddBlinds.Click += new System.EventHandler(AddBlindsNode_Click);
            //// 
            //// tsmiAddImageButton
            //// 
            //ToolStripMenuItem tsmiAddImageButton = new ToolStripMenuItem();
            //tsmiAddImageButton.Image = Properties.Resources.imageButton;
            //tsmiAddImageButton.Name = "tsmiAddImageButton";
            //tsmiAddImageButton.Size = new System.Drawing.Size(152, 22);
            //tsmiAddImageButton.Text = "图像按钮";
            //tsmiAddImageButton.Click += new System.EventHandler(tsmiAddImageButton_Click);
            // 
            // tsmiAddLabel
            // 
            ToolStripMenuItem tsmiAddLabel = new ToolStripMenuItem();
            tsmiAddLabel.Image = ResourceMng.GetImage("label");
            tsmiAddLabel.Name = "tsmiAddLabel";
            tsmiAddLabel.Size = new System.Drawing.Size(152, 22);
            tsmiAddLabel.Text = ResourceMng.GetString("TextLabel");
            tsmiAddLabel.Click += new System.EventHandler(AddLabelNode_Click);
            //// 
            //// tsmiAddMediaButton
            //// 
            //ToolStripMenuItem tsmiAddMediaButton = new ToolStripMenuItem();
            //tsmiAddMediaButton.Image = Properties.Resources.media_button;
            //tsmiAddMediaButton.Name = "tsmiAddMediaButton";
            //tsmiAddMediaButton.Size = new System.Drawing.Size(152, 22);
            //tsmiAddMediaButton.Text = "多媒体开关";
            //tsmiAddMediaButton.Click += new System.EventHandler(AddMediaButtonNode_Click);
            //// 
            //// tsmiAddColorLight
            //// 
            //ToolStripMenuItem tsmiAddColorLight = new ToolStripMenuItem();
            //tsmiAddColorLight.Image = Properties.Resources.rgb;
            //tsmiAddColorLight.Name = "tsmiAddColorLight";
            //tsmiAddColorLight.Size = new System.Drawing.Size(152, 22);
            //tsmiAddColorLight.Text = "彩灯开关";
            //tsmiAddColorLight.Click += new System.EventHandler(AddColorLightNode_Click);
            // 
            // tsmiAddSceneButton
            // 
            ToolStripMenuItem tsmiAddSceneButton = new ToolStripMenuItem();
            tsmiAddSceneButton.Image = ResourceMng.GetImage("scene_button");
            tsmiAddSceneButton.Name = "tsmiAddSceneButton";
            tsmiAddSceneButton.Size = new System.Drawing.Size(152, 22);
            tsmiAddSceneButton.Text = ResourceMng.GetString("TextSceneButton");
            tsmiAddSceneButton.Click += new System.EventHandler(AddSceneButtonNode_Click);
            //// 
            //// tsmiAddSIPCall
            //// 
            //ToolStripMenuItem tsmiAddSipCall = new ToolStripMenuItem();
            //tsmiAddSipCall.Image = Properties.Resources.sip2;
            //tsmiAddSipCall.Name = "tsmiAddSIPCall";
            //tsmiAddSipCall.Size = new System.Drawing.Size(152, 22);
            //tsmiAddSipCall.Text = "可视门铃";
            //tsmiAddSipCall.Click += new System.EventHandler(AddSIPCallNode_Click);
            //// 
            //// tsmiAddSlider
            //// 
            //ToolStripMenuItem tsmiAddSlider = new ToolStripMenuItem();
            //tsmiAddSlider.Image = Properties.Resources.slider;
            //tsmiAddSlider.Name = "tsmiAddSlider";
            //tsmiAddSlider.Size = new System.Drawing.Size(152, 22);
            //tsmiAddSlider.Text = "滑动条";
            //tsmiAddSlider.Click += new System.EventHandler(AddSliderNode_Click);
            // 
            // tsmiAddSliderSwitch
            // 
            ToolStripMenuItem tsmiAddSliderSwitch = new ToolStripMenuItem();
            tsmiAddSliderSwitch.Image = ResourceMng.GetImage("slider_switch");
            tsmiAddSliderSwitch.Name = "tsmiAddSliderSwitch";
            tsmiAddSliderSwitch.Size = new System.Drawing.Size(152, 22);
            tsmiAddSliderSwitch.Text = ResourceMng.GetString("TextSliderSwitch");
            tsmiAddSliderSwitch.Click += new System.EventHandler(AddSliderSwitchNode_Click);
            //// 
            //// tsmiAddSnapper
            //// 
            //ToolStripMenuItem tsmiAddSnapper = new ToolStripMenuItem();
            //tsmiAddSnapper.Image = Properties.Resources.snapper;
            //tsmiAddSnapper.Name = "tsmiAddSnapper";
            //tsmiAddSnapper.Size = new System.Drawing.Size(152, 22);
            //tsmiAddSnapper.Text = "步进条";
            //tsmiAddSnapper.Click += new System.EventHandler(AddSnapperNode_Click);
            //// 
            //// tsmiAddSnapperSwitch
            //// 
            //ToolStripMenuItem tsmiAddSnapperSwitch = new ToolStripMenuItem();
            //tsmiAddSnapperSwitch.Image = Properties.Resources.snapper_switch;
            //tsmiAddSnapperSwitch.Name = "tsmiAddSnapperSwitch";
            //tsmiAddSnapperSwitch.Size = new System.Drawing.Size(152, 22);
            //tsmiAddSnapperSwitch.Text = "步进开关";
            //tsmiAddSnapperSwitch.Click += new System.EventHandler(AddSnapperSwitchNode_Click);
            // 
            // tsmiAddSwitch
            // 
            ToolStripMenuItem tsmiAddSwitch = new ToolStripMenuItem();
            tsmiAddSwitch.Image = ResourceMng.GetImage("switch_16x16");
            tsmiAddSwitch.Name = "tsmiAddSwitch";
            tsmiAddSwitch.Size = new System.Drawing.Size(152, 22);
            tsmiAddSwitch.Text = ResourceMng.GetString("TextSwitch");
            tsmiAddSwitch.Click += new System.EventHandler(AddSwitchNode_Click);
            // 
            // tsmiAddValueDisplay
            // 
            ToolStripMenuItem tsmiAddValueDisplay = new ToolStripMenuItem();
            tsmiAddValueDisplay.Image = ResourceMng.GetImage("value_display");
            tsmiAddValueDisplay.Name = "tsmiAddValueDisplay";
            tsmiAddValueDisplay.Size = new System.Drawing.Size(152, 22);
            tsmiAddValueDisplay.Text = ResourceMng.GetString("TextValueDisplay");
            tsmiAddValueDisplay.Click += new System.EventHandler(AddValueDisplayNode_Click);
            //// 
            //// tsmiAddWebcam
            //// 
            //ToolStripMenuItem tsmiAddWebcam = new ToolStripMenuItem();
            //tsmiAddWebcam.Image = Properties.Resources.webcam_viewer;
            //tsmiAddWebcam.Name = "tsmiAddWebcam";
            //tsmiAddWebcam.Size = new System.Drawing.Size(152, 22);
            //tsmiAddWebcam.Text = "网络摄像头";
            //tsmiAddWebcam.Click += new System.EventHandler(AddWebcamViewerNode_Click);

            ToolStripMenuItem tsmiAddTimerButton = new ToolStripMenuItem();
            tsmiAddTimerButton.Image = ResourceMng.GetImage("timer_512x512");
            tsmiAddTimerButton.Name = "tsmiAddTimerButton";
            tsmiAddTimerButton.Size = new System.Drawing.Size(152, 22);
            tsmiAddTimerButton.Text = ResourceMng.GetString("TextTimer");
            tsmiAddTimerButton.Click += new System.EventHandler(AddTimerButtonNode_Click);

            //ToolStripMenuItem tsmiAddTimerTaskListView = new ToolStripMenuItem();
            //tsmiAddTimerTaskListView.Image = Properties.Resources.timer_task_list;
            //tsmiAddTimerTaskListView.Name = "tsmiAddTimerTaskListView";
            //tsmiAddTimerTaskListView.Size = new System.Drawing.Size(152, 22);
            //tsmiAddTimerTaskListView.Text = "定时任务列表";
            //tsmiAddTimerTaskListView.Click += new System.EventHandler(AddTimerTaskListViewNode_Click);

            ToolStripMenuItem tsmiDigitalAdjustment = new ToolStripMenuItem();
            tsmiDigitalAdjustment.Image = ResourceMng.GetImage("DigitalAdjustment_500x500");
            tsmiDigitalAdjustment.Name = "tsmiDigitalAdjustment";
            tsmiDigitalAdjustment.Size = new System.Drawing.Size(152, 22);
            tsmiDigitalAdjustment.Text = ResourceMng.GetString("TextDigitalAdjustment");
            tsmiDigitalAdjustment.Click += new System.EventHandler(AddDigitalAdjustmentNode_Click);

            //ToolStripMenuItem tsmiRadioGroup = new ToolStripMenuItem();
            //tsmiRadioGroup.Image = Properties.Resources.RadioGroup_220x220;
            //tsmiRadioGroup.Name = "tsmiRadioGroup";
            //tsmiRadioGroup.Size = new System.Drawing.Size(152, 22);
            //tsmiRadioGroup.Text = MyConst.TextRadioGroup;
            //tsmiRadioGroup.Click += new System.EventHandler(AddRadioGroupNode_Click);

            List<ToolStripItem> list = new List<ToolStripItem>();
            list.Add(tsmiAddBlinds);
            //list.Add(tsmiAddImageButton);
            list.Add(tsmiAddLabel);
            //list.Add(tsmiAddMediaButton);
            //list.Add(tsmiAddColorLight);
            list.Add(tsmiAddSceneButton);
            //list.Add(tsmiAddSipCall);
            //list.Add(tsmiAddSlider);
            list.Add(tsmiAddSliderSwitch);
            //list.Add(tsmiAddSnapper);
            //list.Add(tsmiAddSnapperSwitch);
            list.Add(tsmiAddSwitch);
            list.Add(tsmiAddValueDisplay);
            //list.Add(tsmiAddWebcam);
            list.Add(tsmiAddTimerButton);
            //list.Add(tsmiAddTimerTaskListView);
            list.Add(tsmiDigitalAdjustment);
            //list.Add(tsmiRadioGroup);

            return list.ToArray();
        }
        #endregion

        #region 添加TreeView节点
        /// <summary>
        /// 创建新项目
        /// </summary>
        private void AddAppNode(TreeView treeView)
        {
            if (treeView != null)
            {
                ProjectFile = MyConst.DefaultKnxUiProjectName;
                tsslblProjectName.Text = string.Format("Project Name: {0}", ProjectFile);

                var root = new AppNode();

                // 清除所有节点，添加根
                treeView.Nodes.Clear();
                treeView.Nodes.Add(root);

                CreateProjectFolder();

                // 复制默认图像到项目资源路径
                //if (File.Exists(MyConst.DefaultIcon))
                //{
                //    File.Copy(MyConst.DefaultIcon, Path.Combine(MyCache.ProjImagePath, MyConst.DefaultIcon));
                //    File.Copy(MyConst.SwitchIcon, Path.Combine(MyCache.ProjImagePath, Path.GetFileName(MyConst.SwitchIcon)), true);
                //}

                // 
                ToolBarStatus status = new ToolBarStatus { collapse = true, expand = true, searchBox = true, importKnx = true };
                SetButtonStatus(status);

                ResetParameter();

                Saved = false;
            }
        }

        private void AddAreaNode(TreeNode appNode)
        {
            // 添加楼层
            if (appNode != null && appNode.Name == MyConst.View.KnxAppType)
            {
                var areaNode = new AreaNode();
                // 
                appNode.Nodes.Add(areaNode);
                appNode.Expand();

                Saved = false;
            }
        }

        private void AddRoomNode(TreeNode areaNode)
        {
            // 添加
            if (areaNode != null && areaNode.Name == MyConst.View.KnxAreaType)
            {
                var roomNode = new RoomNode();
                roomNode.ContextMenuStrip = cmsAddPage;
                areaNode.Nodes.Add(roomNode);
                areaNode.Expand();

                Saved = false;
            }
        }

        private void AddPageNode(TreeNode roomNode)
        {
            if (roomNode != null && roomNode.Name == MyConst.View.KnxRoomType)
            {
                var pageNode = new PageNode();
                pageNode.PropertiesChangedEvent += new UIEditor.Entity.ViewNode.PropertiesChangedDelegate(Frm_ControlPropertiesChangedEvent);
                //AppNode aNode = null;
                //TreeNode pNode = roomNode.Parent;
                //while (true)
                //{
                //    aNode = pNode as AppNode;
                //    if (null != aNode)
                //    {
                //        break;
                //    }
                //    else
                //    {
                //        if (null != pNode.Parent)
                //        {
                //            pNode = pNode.Parent;
                //        }
                //        else
                //        {
                //            break;
                //        }
                //    }
                //}
                //if (null != pNode)
                //{
                //    pageNode.Width = aNode.ScreenWidth;
                //    pageNode.Height = aNode.ScreenHeight;
                //}
                pageNode.ContextMenuStrip = cmsAddGroupBox;
                roomNode.Nodes.Add(pageNode);
                roomNode.Expand();

                Saved = false;
            }
        }

        /// <summary>
        /// 创建界面布局控件
        /// </summary>
        /// <param name="pageNode"></param>
        private void AddGroupBoxNode(TreeNode pageNode)
        {
            if (pageNode != null && (pageNode.Name == MyConst.View.KnxPageType || pageNode.Name == MyConst.Controls.KnxGroupBoxType))
            {
                var groupBoxNode = new GroupBoxNode();
                groupBoxNode.PropertiesChangedEvent += new UIEditor.Entity.ViewNode.PropertiesChangedDelegate(Frm_ControlPropertiesChangedEvent);
                groupBoxNode.ContextMenuStrip = cmsAddControl;
                pageNode.Nodes.Add(groupBoxNode);
                pageNode.Expand();

                AddControlToPagePreview(pageNode, groupBoxNode);

                Saved = false;
            }
        }

        private void AddBlindsNode(TreeNode parentNode)
        {
            if (parentNode != null && (parentNode.Name == MyConst.Controls.KnxGroupBoxType || parentNode.Name == MyConst.View.KnxPageType))
            {
                var node = new BlindsNode();
                node.PropertiesChangedEvent += new UIEditor.Entity.ViewNode.PropertiesChangedDelegate(Frm_ControlPropertiesChangedEvent);
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                AddControlToPagePreview(parentNode, node);

                Saved = false;
            }
        }

        private void AddLabelNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.Controls.KnxGroupBoxType))
            {
                var node = new LabelNode();
                node.PropertiesChangedEvent += new UIEditor.Entity.ViewNode.PropertiesChangedDelegate(Frm_ControlPropertiesChangedEvent);
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                AddControlToPagePreview(parentNode, node);

                Saved = false;
            }
        }

        private void AddSceneButtonNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.Controls.KnxGroupBoxType/* || parentNode.Name == MyConst.Controls.KnxRadioGroup*/))
            {
                var node = new SceneButtonNode();
                node.PropertiesChangedEvent += new UIEditor.Entity.ViewNode.PropertiesChangedDelegate(Frm_ControlPropertiesChangedEvent);
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                AddControlToPagePreview(parentNode, node);

                Saved = false;
            }
        }

        private void AddSliderSwitchNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.Controls.KnxGroupBoxType))
            {
                var node = new SliderSwitchNode();
                node.PropertiesChangedEvent += new UIEditor.Entity.ViewNode.PropertiesChangedDelegate(Frm_ControlPropertiesChangedEvent);
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                AddControlToPagePreview(parentNode, node);

                Saved = false;
            }
        }

        private void AddSwitchNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.Controls.KnxGroupBoxType))
            {
                var node = new SwitchNode();
                node.PropertiesChangedEvent += new UIEditor.Entity.ViewNode.PropertiesChangedDelegate(Frm_ControlPropertiesChangedEvent);
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                AddControlToPagePreview(parentNode, node);

                Saved = false;
            }
        }

        private void AddValueDisplayNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.Controls.KnxGroupBoxType))
            {
                var node = new ValueDisplayNode();
                node.PropertiesChangedEvent += new UIEditor.Entity.ViewNode.PropertiesChangedDelegate(Frm_ControlPropertiesChangedEvent);
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                AddControlToPagePreview(parentNode, node);

                Saved = false;
            }
        }

        private void AddTimerButtonNode(TreeNode parentNode)
        {
            if (parentNode != null &&
                (parentNode.Name == MyConst.View.KnxPageType || parentNode.Name == MyConst.Controls.KnxGroupBoxType))
            {
                var node = new TimerButtonNode();
                node.PropertiesChangedEvent += new UIEditor.Entity.ViewNode.PropertiesChangedDelegate(Frm_ControlPropertiesChangedEvent);
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                AddControlToPagePreview(parentNode, node);

                Saved = false;
            }
        }

        private void AddDigitalAdjustmenNode(TreeNode parentNode)
        {
            if ((null != parentNode) && ((MyConst.View.KnxPageType == parentNode.Name) || (MyConst.Controls.KnxGroupBoxType == parentNode.Name)))
            {
                var node = new DigitalAdjustmentNode();
                node.PropertiesChangedEvent += new ViewNode.PropertiesChangedDelegate(Frm_ControlPropertiesChangedEvent);
                parentNode.Nodes.Add(node);
                parentNode.Expand();

                AddControlToPagePreview(parentNode, node);

                Saved = false;
            }
        }

        #endregion

        #region 预览界面

        private Hashtable controltable = new Hashtable();

        private object getHashTableKey(Hashtable table, object value)
        {
            if ((null == table) || (null == value))
            {
                return null;
            }

            foreach (DictionaryEntry dic in table)
            {
                if (dic.Value == value)
                {
                    return dic.Key;
                }
            }

            return null;
        }

        private void selectControl(FrameControl fctrl)
        //private void selectControl(STPanel stPanel)
        {
            if (null == fctrl)
            {
                return;
            }
            //if (null == stPanel)
            //{
            //    return;
            //}

            if (this.curFrameControl != fctrl)
            {
                if (null != this.curFrameControl)
                {
                    this.curFrameControl.Visible = false;
                    if (null != this.curFrameControl.Parent)
                    {
                        this.curFrameControl.Parent.Controls.Remove(this.curFrameControl);
                    }
                }

                //this.currentControl = fctrl.baseControl;
                //this.fc = fctrl;

                //this.currentControl.BringToFront();
                //this.fc.BackColor = Color.Transparent;
                //this.currentControl.Parent.Controls.Add(this.fc);
                //this.fc.CreateBounds();
                //this.fc.Visible = true;
                //this.fc.Draw();
            }

            this.curSTPanel = fctrl.baseControl;
            this.curFrameControl = fctrl;

            this.curSTPanel.BringToFront();
            this.curFrameControl.BackColor = Color.Transparent;
            this.curSTPanel.Parent.Controls.Add(this.curFrameControl);
            this.curFrameControl.CreateBounds();
            this.curFrameControl.Visible = true;
            this.curFrameControl.Draw();

            //if (this.fc != stPanel)
            //{
            //    if (null != this.fc)
            //    {
            //        this.fc.Visible = false;
            //        if (null != this.fc.Parent)
            //        {
            //            this.fc.Parent.Controls.Remove(this.fc);
            //        }
            //    }

            //    this.currentControl = fctrl.baseControl;
            //    this.fc = fctrl;

            //    this.currentControl.BringToFront();
            //    this.fc.BackColor = Color.Transparent;
            //    this.currentControl.Parent.Controls.Add(this.fc);
            //    this.fc.CreateBounds();
            //    this.fc.Visible = true;
            //    this.fc.Draw();

            //    //FrameControl mFrameControl = new FrameControl(fctrl.baseControl);
            //    //this.currentControl = fctrl.baseControl;
            //    //this.fc = mFrameControl;
            //    //this.currentControl.BringToFront();
            //    //this.fc.BackColor = Color.Transparent;
            //    //this.currentControl.Parent.Controls.Add(this.fc);
            //    //this.fc.CreateBounds();
            //    //this.fc.Visible = true;
            //    //this.fc.Draw();
            //}
        }

        private void Frm_ControlPropertiesChangedEvent(object sender, EventArgs e)
        {
            _saved = false;

            ViewNode node = sender as ViewNode;
            var key = getHashTableKey(controltable, sender);
            FrameControl fControl = key as FrameControl;
            if (null != fControl)
            {
                STPanel panel = (STPanel)fControl.baseControl;

                fControl.Visible = false;

                panel.RefreshUI();
                fControl.CreateBounds();
                fControl.Visible = true;
                fControl.Draw();
            }
            else if (sender == curPageNode)
            {
                STPage pagePanel = key as STPage;
                pagePanel.MaximumSize = pagePanel.MinimumSize = new Size(curPageNode.Width, curPageNode.Height);
                pagePanel.RefreshUI();
            }
        }

        private void Frm_ControlMouseUpEvent(object sender, EventArgs e)
        {
            //STPanel panel = sender as STPanel;
            FrameControl fControl = sender as FrameControl;
            ViewNode vNode = (ViewNode)controltable[sender];
            //foreach (DictionaryEntry dic in controltable)
            //{
            //    //FrameControl fControl = dic.Key as FrameControl;
            //    if ((null != fControl) && (fControl.baseControl == panel))
            //    {
            //        tNode = (TreeNode)dic.Value;
            //        break;
            //    }
            //}

            //if (null != tNode)
            //{
            //ViewNode vNode = tNode as ViewNode;

            if (null != vNode)
            {
                if (null != fControl)
                {
                    vNode.Left = /*fControl.baseControlLeft*/fControl.baseControl.Left;
                    vNode.Top = /*fControl.baseControlTop*/fControl.baseControl.Top;
                    vNode.Width = /*fControl.baseControlWidth*/fControl.baseControl.Width;
                    vNode.Height = /*fControl.baseControlHeight*/fControl.baseControl.Height;

                    _saved = false;
                }

                selectedNodeChanged(vNode);
            }

            fControl.baseControl.Refresh();

            //Frm_ControlPropertiesChangedEvent(vNode, EventArgs.Empty);
            //}
        }

        private void LoadPreview()
        {
            this.panLayout.Controls.Clear();
            BuilderControles();
        }

        private void AddControlToPagePreview(TreeNode parentNode, TreeNode childNode)
        {
            if ((null == parentNode) || (null == childNode) || (null == curSTPage))
            {
                return;
            }

            if (null != (parentNode as PageNode))
            {
                PageNode pageNode = parentNode as PageNode;
                if (pageNode == curPageNode)
                {
                    ViewNode viewNode = childNode as ViewNode;
                    if (null != viewNode)
                    {
                        STPanel controlPanel = CreateControl(viewNode);
                        //    controlPanel.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                        //System.Reflection.BindingFlags.NonPublic).SetValue(controlPanel, true, null);    // 双缓存，减少重绘控件时的闪烁
                        AddControlToParentPage(curSTPage, controlPanel);
                        FrameControl fctrl = CreateAndAddFrameControlToParentPage(controlPanel, curSTPage);
                        controltable.Add(fctrl, viewNode);
                        //controlPanel.Invalidate();
                        //controlPanel.Refresh();
                    }
                }
            }
            else if (null != (parentNode as GroupBoxNode))
            {
                TreeNode node = parentNode.Parent;
                while (null != node)
                {
                    if (curPageNode == node)
                    {
                        var key = getHashTableKey(controltable, parentNode);
                        FrameControl fControl = key as FrameControl;
                        STGroupBox gridPanel = null;
                        if ((null != fControl) && (null != fControl.baseControl))
                        {
                            gridPanel = fControl.baseControl as STGroupBox;
                        }
                        ViewNode viewNode = childNode as ViewNode;
                        if ((null != gridPanel) && (null != viewNode))
                        {
                            STPanel controlPanel = CreateControl(viewNode);
                            AddControlToParentPage(gridPanel, controlPanel);
                            FrameControl fctrl = CreateAndAddFrameControlToParentPage(controlPanel, gridPanel);
                            controltable.Add(fctrl, viewNode);
                        }
                    }

                    node = node.Parent;
                }
            }
            //else if (null != (parentNode as RadioGroupNode))
            //{
            //    TreeNode node = parentNode.Parent;
            //    if (null != node)
            //    {
            //        PageNode pageNode = node as PageNode;
            //        if (null != pageNode)
            //        {
            //            if ((pageNode == curPageNode) && (null != curPage))
            //            {
            //                var key = getHashTableKey(controltable, parentNode);
            //                FrameControl fControl = key as FrameControl;
            //                STRadioGroup radioGroupPanel = null;
            //                if ((null != fControl) && (null != fControl.baseControl))
            //                {
            //                    radioGroupPanel = fControl.baseControl as STRadioGroup;
            //                }
            //                ViewNode viewNode = childNode as ViewNode;
            //                if ((null != radioGroupPanel) && (null != viewNode))
            //                {
            //                    STPanel controlPanel = CreateControl(viewNode);
            //                    AddControlToParentPage(radioGroupPanel, controlPanel);
            //                    FrameControl fctrl = CreateAndAddFrameControlToParentPage(controlPanel, radioGroupPanel);
            //                    controltable.Add(fctrl, viewNode);
            //                }
            //            }
            //        }
            //        else if (null != (node as GroupBoxNode))
            //        {
            //            PageNode parent = node.Parent as PageNode;
            //            if (null != parent)
            //            {
            //                if ((parent == curPageNode) && (null != curPage))
            //                {
            //                    var key = getHashTableKey(controltable, parentNode);
            //                    FrameControl fControl = key as FrameControl;
            //                    STRadioGroup radioGroupPanel = null;
            //                    if ((null != fControl) && (null != fControl.baseControl))
            //                    {
            //                        radioGroupPanel = fControl.baseControl as STRadioGroup;
            //                    }
            //                    ViewNode viewNode = childNode as ViewNode;
            //                    if ((null != radioGroupPanel) && (null != viewNode))
            //                    {
            //                        STPanel controlPanel = CreateControl(viewNode);
            //                        AddControlToParentPage(radioGroupPanel, controlPanel);
            //                        FrameControl fctrl = CreateAndAddFrameControlToParentPage(controlPanel, radioGroupPanel);
            //                        controltable.Add(fctrl, viewNode);
            //                    }
            //                }
            //            }

            //        }
            //    }
            //}
        }

        private void DeleteControlFromPagePreview(TreeNode treeNode)
        {
            if (null != (treeNode as AppNode))
            {
                /* 删除的是AppNode */
                curPageNode = null;
                curSelectedNode = null;
                if (null != curSTPage)
                {
                    curSTPage.Parent.Controls.Clear();
                }
                curSTPage = null;
            }
            else if (null != (treeNode as AreaNode))
            {
                /* 删除的是AreaNode */
                foreach (TreeNode roomNode in treeNode.Nodes)
                {
                    foreach (TreeNode node in roomNode.Nodes)
                    {
                        PageNode pageNode = node as PageNode;
                        if (pageNode == curPageNode)
                        {
                            curPageNode = null;
                            if (null != curSTPage)
                            {
                                curSTPage.Parent.Controls.Clear();
                            }
                            curSTPage = null;
                        }
                    }
                }
            }
            else if (null != (treeNode as RoomNode))
            {
                /* 删除的是RoomNode */
                foreach (TreeNode node in treeNode.Nodes)
                {
                    PageNode pageNode = node as PageNode;
                    if (pageNode == curPageNode)
                    {
                        curPageNode = null;
                        if (null != curSTPage)
                        {
                            curSTPage.Parent.Controls.Clear();
                        }
                        curSTPage = null;
                    }
                }
            }
            else if (treeNode == curPageNode)
            {
                /* 删除的是当前正在显示的PageNode */
                curPageNode = null;
                if (null != curSTPage)
                {
                    curSTPage.Parent.Controls.Clear();
                }
                curSTPage = null;
            }
            else
            {
                /* 删除的是页面下的控件 */
                TreeNode node = treeNode.Parent;
                while (null != node)
                {
                    if (curPageNode == node)
                    {
                        var key = getHashTableKey(controltable, treeNode);
                        FrameControl fControl = key as FrameControl;
                        if (null != fControl)
                        {
                            Control parent = fControl.baseControl.Parent;
                            if (null != parent)
                            {
                                parent.Controls.Remove(fControl.baseControl);
                                parent.Controls.Remove(fControl);
                                parent.Refresh();
                            }

                            controltable.Remove(fControl);
                        }

                        break;
                    }
                    else
                    {
                        node = node.Parent;
                    }
                }

                //TreeNode parentNode = treeNode.Parent;
                //if (null != parentNode)
                //{
                //    /* 删除的是当前页面下的控件 */
                //    PageNode pageNode = parentNode as PageNode;
                //    if (parentNode == curPageNode)
                //    {
                //        var key = getHashTableKey(controltable, treeNode);
                //        FrameControl fControl = key as FrameControl;
                //        if (null != fControl)
                //        {
                //            Control parent = fControl.baseControl.Parent;
                //            if (null != parent)
                //            {
                //                parent.Controls.Remove(fControl.baseControl);
                //                parent.Controls.Remove(fControl);
                //                parent.Refresh();
                //            }

                //            controltable.Remove(fControl);
                //        }
                //    }
                //    else
                //    {
                //        GroupBoxNode gridNode = parentNode as GroupBoxNode;
                //        if (null != gridNode)
                //        {
                //            if (gridNode.Parent == curPageNode)
                //            {
                //                var key = getHashTableKey(controltable, treeNode);
                //                FrameControl fControl = key as FrameControl;
                //                if (null != fControl)
                //                {
                //                    Control parent = fControl.baseControl.Parent;
                //                    if (null != parent)
                //                    {
                //                        fControl.baseControl.Visible = false;
                //                        fControl.Visible = false;
                //                        //parent.Controls.Remove(fControl.baseControl);
                //                        //parent.Controls.Remove(fControl);
                //                        parent.Refresh();
                //                    }

                //                    //this.fc = null;
                //                    //this.currentControl = null;

                //                    controltable.Remove(fControl);
                //                }
                //            }
                //        }
                //    }
                //}
            }

            curSelectedNode = null;
        }

        private void BuilderControles()
        {
            if (curPageNode != null && curPageNode.Name == MyConst.View.KnxPageType)
            {
                controltable.Clear();
                this.panLayout.SuspendLayout();

                curSTPage = (STPage)CreateControl(curPageNode);
                //Panel curPage = new Panel();
                //curPage.Location = new Point(0, 0);
                //curPage.Size = new Size(curPageNode.Width, curPageNode.Height);
                //curPage.BorderStyle = BorderStyle.FixedSingle;
                //curPage.BackgroundImageLayout = ImageLayout.Stretch;
                //curPage.AutoScroll = true;
                //setControlProperties(curPage, curPageNode);
                curSTPage.MouseDown += new System.Windows.Forms.MouseEventHandler(Page_MouseDown);
                //curPage.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                //    System.Reflection.BindingFlags.NonPublic).SetValue(curPage, true, null);    // 双缓存，减少重绘控件时的闪烁
                controltable.Add(curSTPage, curPageNode);

                CreatePanelAndAddToParentPanel(curSTPage, curPageNode);

                //var page = curPageNode.ToKnx();
                //STPanel parentPanel = curPage;
                //ViewNode parentNode = curPageNode;
                //while (parentNode.Nodes.Count > 0)
                //{
                //    // 添加 grid
                //    foreach (TreeNode item1 in parentNode.Nodes)
                //    {
                //        // 添加控件
                //        ViewNode vNode = item1 as ViewNode;
                //        STPanel controlPanel = CreateControl(vNode);
                //        AddControlToParentPage(curPage, controlPanel);
                //        FrameControl fctrl = CreateAndAddFrameControlToParentPage(controlPanel, curPage);
                //        controltable.Add(fctrl, vNode);

                //        if (MyConst.View.KnxGroupBoxType == item1.Name)
                //        {
                //            var gridNode = item1 as GroupBoxNode;
                //            if (gridNode != null)
                //            {
                //                STGroupBox gridPanel = (STGroupBox)CreateControl(gridNode);
                //                AddControlToParentPage(curPage, gridPanel);
                //                FrameControl fctrl = CreateAndAddFrameControlToParentPage(gridPanel, curPage);
                //                //            gridPanel.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                //                //System.Reflection.BindingFlags.NonPublic).SetValue(gridPanel, true, null);    // 双缓存，减少重绘控件时的闪烁
                //                controltable.Add(fctrl, gridNode);

                //                if (item1.Nodes.Count > 0)
                //                {
                //                    // 添加控件
                //                    foreach (TreeNode item2 in item1.Nodes)
                //                    {
                //                        ViewNode vNode = item2 as ViewNode;
                //                        STPanel controlPanel = CreateControl(vNode);
                //                        AddControlToParentPage(gridPanel, controlPanel);
                //                        FrameControl frameControl = CreateAndAddFrameControlToParentPage(controlPanel, gridPanel);
                //                        controltable.Add(frameControl, vNode);
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}

                this.panLayout.Controls.Add(curSTPage);
                this.panLayout.ResumeLayout();
            }
        }

        private void CreatePanelAndAddToParentPanel(STPanel parentPanel, ViewNode parentNode)
        {
            foreach (TreeNode tNode in parentNode.Nodes)
            {
                ViewNode vNode = tNode as ViewNode;
                if (null != vNode)
                {
                    // 添加控件
                    STPanel stPanel = CreateControl(vNode);
                    AddControlToParentPage(parentPanel, stPanel);
                    FrameControl fctrl = CreateAndAddFrameControlToParentPage(stPanel, parentPanel);
                    controltable.Add(fctrl, vNode);
                    //controltable.Add(stPanel, vNode);

                    if (MyConst.Controls.KnxGroupBoxType == vNode.Name)
                    {
                        CreatePanelAndAddToParentPanel(stPanel, vNode);
                    }
                }
            }
        }

        private bool AddControlToParentPage(STPanel parent, STPanel child)
        {
            if ((null == parent) || (null == child))
            {
                return false;
            }

            if (null != child)
            {
                child.MouseDown += new System.Windows.Forms.MouseEventHandler(Control_MouseDown);
                child.MouseMove += new System.Windows.Forms.MouseEventHandler(Control_MouseMove);
                child.MouseUp += new System.Windows.Forms.MouseEventHandler(Control_MouseUp);

                parent.Controls.Add(child);
            }
            else
            {
                return false;
            }

            return true;
        }

        private FrameControl CreateAndAddFrameControlToParentPage(STPanel child, STPanel parent)
        {
            if ((null == parent) || (null == child))
            {
                return null;
            }

            FrameControl fctrl = new FrameControl(child);
            //fctrl.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
            //    System.Reflection.BindingFlags.NonPublic).SetValue(fctrl, true, null);    // 双缓存，减少重绘控件时的闪烁
            fctrl.Visible = false;
            fctrl.ControlMouseUpEvent += new UIEditor.FrameControl.ControlMouseUpEventDelegate(Frm_ControlMouseUpEvent);

            //parent.Controls.Add(fctrl);

            return fctrl;
        }

        /// <summary>
        /// 添加控件到 Panel
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="node"></param>
        private STPanel CreateControl(ViewNode node)
        {
            STPanel panel = null;
            if (null == node)
            {
                return null;
            }

            if (null != (node as PageNode))
            {
                panel = new STPage(node as PageNode);
            }
            else if (null != (node as GroupBoxNode))
            {
                panel = new STGroupBox(node as GroupBoxNode);
            }
            else if (null != (node as SwitchNode))
            {
                panel = new STSwitch(node as SwitchNode);
            }
            else if (null != node as LabelNode)
            {
                panel = new STLable(node as LabelNode);
            }
            else if (null != (node as SceneButtonNode))
            {
                panel = new STSceneButton(node as SceneButtonNode);
            }
            else if (null != (node as SliderSwitchNode))
            {
                panel = new STSliderSwitch(node as SliderSwitchNode);
            }
            else if (null != (node as ValueDisplayNode))
            {
                panel = new STValueDisplay(node as ValueDisplayNode);
            }
            else if (null != (node as TimerButtonNode))
            {
                panel = new STTimerButton(node as TimerButtonNode);
            }
            else if (null != (node as DigitalAdjustmentNode))
            {
                panel = new STDigitalAdjustment(node as DigitalAdjustmentNode);
            }
            else if (null != (node as BlindsNode))
            {
                panel = new STBlinds(node as BlindsNode);
            }
            else
            {
                return null;
            }

            if (null != panel)
            {
                panel.RefreshUI();
                //setControlProperties(panel, node);
            }

            return panel;
        }

        #endregion

        #region 鼠标动作



        /// <summary>
        /// 鼠标左键按下时动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            pPoint = Cursor.Position;
            STPanel panel = sender as STPanel;
            TreeNode node = null;
            FrameControl fControl = null;
            foreach (DictionaryEntry dic in controltable)
            {
                fControl = dic.Key as FrameControl;
                if ((null != fControl) && (fControl.baseControl == panel))
                {
                    node = (TreeNode)dic.Value;
                    break;
                }
            }

            /* 去掉之前已选中控件的边框 */
            if (null != this.curFrameControl)
            {
                this.curFrameControl.Visible = false;
                if (null != this.curFrameControl.Parent)
                {
                    this.curFrameControl.Parent.Controls.Remove(this.curFrameControl);
                }
            }

            /* 赋值新的控件 */
            this.curSTPanel = fControl.baseControl;
            this.curFrameControl = fControl;

            /* 绘制实线边框 */
            this.curSTPanel.BringToFront();
            this.curSTPanel.Parent.Controls.Add(this.curFrameControl);
            this.curFrameControl.BackColor = Color.Orange;
            this.curFrameControl.CreateBounds();
            this.curFrameControl.Visible = true;
            this.curFrameControl.DrawBoundsLine();
        }

        /// <summary>
        /// 鼠标左键按下并移动时的动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeAll; //当鼠标处于控件内部时，显示光标样式为SizeAll
            cPoint = Cursor.Position; //获得当前鼠标位置
            if ((cPoint.X == pPoint.X) && (cPoint.Y == pPoint.Y))
            {
                return;
            }

            //当鼠标左键按下时才触发
            if (e.Button == MouseButtons.Left)
            {
                int x = cPoint.X - pPoint.X;
                int y = cPoint.Y - pPoint.Y;
                if (null != this.curFrameControl)
                {
                    /* 控件的实线边框跟随鼠标移动 */
                    this.curFrameControl.Location = new Point(this.curFrameControl.Left + x, this.curFrameControl.Top + y);
                }
                pPoint = cPoint;
            }
        }

        /// <summary>
        /// 鼠标左键释放时动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            FrameControl fControl = null;
            STPanel panel = sender as STPanel;
            TreeNode node = null;
            foreach (DictionaryEntry dic in controltable)
            {
                fControl = dic.Key as FrameControl;
                if ((null != fControl) && (fControl.baseControl == panel))
                {
                    node = (TreeNode)dic.Value;
                    break;
                }
            }

            /* 绘制控件的可变边框 */
            this.curFrameControl.BackColor = Color.Transparent;
            this.curFrameControl.baseControl.Location = this.curFrameControl.getControlLocation();
            this.curFrameControl.CreateBounds();
            this.curFrameControl.Visible = true;
            this.curFrameControl.Draw();

            Frm_ControlMouseUpEvent(this.curFrameControl, EventArgs.Empty);
        }

        private void Page_MouseDown(object sender, MouseEventArgs e)
        {
            selectedNodeChanged(curPageNode);

            this.curSTPanel = null;
            if (null != this.curFrameControl)
            {
                this.curFrameControl.Visible = false;
                this.curFrameControl = null;
            }
        }

        #endregion

        #region 属性表格
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


            //_gridProperty[0, 0] = new ColumnHeader("属性名称");
            //_gridProperty[0, 0].View = titleModel;
            //_gridProperty[0, 1] = new ColumnHeader("属性值");
            //_gridProperty[0, 1].View = titleModel;
            //_gridProperty[0, 2] = new ColumnHeader("");
            //_gridProperty[0, 2].View = titleModel;

            _gridProperty.Columns[0].Width = 80;
            _gridProperty.Columns[1].Width = 120;
            _gridProperty.Columns[2].Width = 36;
            //gridProperty.Columns[2].MaximalWidth = 35;

            _gridProperty.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            _gridProperty.Columns[1].AutoSizeMode = SourceGrid.AutoSizeMode.Default;
            _gridProperty.Columns[2].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            _gridProperty.AutoStretchColumnsToFitWidth = true;
            _gridProperty.Columns.StretchToFit();
        }
        #endregion

        /// <summary>
        /// 删除当前选中的节点
        /// </summary>
        private void DeleteSelectedNode(TreeNode selectedNode)
        {
            if (selectedNode != null)
            {
                if (DialogResult.OK == MessageBox.Show(ResourceMng.GetString("Message14"), ResourceMng.GetString("Message15"), MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                {
                    DeleteControlFromPagePreview(selectedNode);

                    selectedNode.Remove();

                    Saved = false;
                }
            }

            if (this.tvwAppdata.Nodes.Count == 0)
            {
                ToolBarStatus status = new ToolBarStatus();
                SetButtonStatus(status);
                tsslblProjectName.Text = "";

                InitGrid();
            }
        }

        void FrmMain_ResizeBegin(object sender, System.EventArgs e)
        {
            Console.WriteLine("FrmMain_Resize");

            //this.fc.Visible = false;
            //this.fc.Parent.Controls.Remove(this.fc);
        }

        private void tsm_en_US_Click(object sender, EventArgs e)
        {
            //CultureInfo ci = CultureInfo.GetCultureInfo("en-US");
            //Thread.CurrentThread.CurrentCulture = ci;
            //Thread.CurrentThread.CurrentUICulture = ci;

            //ApplyResource(ci);

            if (DialogResult.OK == MessageBox.Show(ResourceMng.GetString("Message45"), ResourceMng.GetString("Message4"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
            {
                AppConfigHelper.UpdateAppConfig(MyConst.XmlTagAppLanguange, "en-US");
                Application.Restart();
            }
        }

        private void tsm_zh_CN_Click(object sender, EventArgs e)
        {
            //CultureInfo ci = CultureInfo.GetCultureInfo("zh-CN");
            //Thread.CurrentThread.CurrentCulture = ci;
            //Thread.CurrentThread.CurrentUICulture = ci;

            //ApplyResource(ci);

            if (DialogResult.OK == MessageBox.Show(ResourceMng.GetString("Message45"), ResourceMng.GetString("Message4"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
            {
                AppConfigHelper.UpdateAppConfig(MyConst.XmlTagAppLanguange, "zh-CN");
                Application.Restart();
            }
        }

        /// <summary>
        /// 应用资源
        /// ApplyResources 的第一个参数为要设置的控件
        ///                  第二个参数为在资源文件中的ID，默认为控件的名称
        /// </summary>
        private void ApplyResource(CultureInfo ci)
        {
            int width = this.tlpMain.Width;
            //    int height = this.tableLayoutPanel1.Height;
            Rectangle rect = this.tlpMain.Bounds;

            ComponentResourceManager res = new ComponentResourceManager(typeof(FrmMain));
            //res.ApplyResources(FrmMain, FrmMain.Name, ci);
            foreach (Control ctl in Controls)
            {
                res.ApplyResources(ctl, ctl.Name, ci);
            }

            //菜单
            res.ApplyResources(this.tsmiFile, this.tsmiFile.Name, ci);
            foreach (ToolStripItem item in this.tsmiFile.DropDownItems)
            {
                if (null != (item as ToolStripMenuItem))
                {
                    res.ApplyResources(item, item.Name, ci);
                }
            }
            res.ApplyResources(this.tsmiLanguange, this.tsmiLanguange.Name, ci);
            foreach (ToolStripItem item in this.tsmiLanguange.DropDownItems)
            {
                if (null != (item as ToolStripMenuItem))
                {
                    res.ApplyResources(item, item.Name, ci);
                }
            }
            res.ApplyResources(this.tsmiHelp, this.tsmiHelp.Name, ci);
            foreach (ToolStripItem item in this.tsmiHelp.DropDownItems)
            {
                if (null != (item as ToolStripMenuItem))
                {
                    res.ApplyResources(item, item.Name, ci);
                }
            }

            foreach (ToolStripItem item in this.tsrProject.Items)
            {
                if (null != (item as ToolStripButton))
                {
                    res.ApplyResources(item, item.Name, ci);
                }
            }
            foreach (ToolStripItem item in this.tsrAddControlToolBar.Items)
            {
                if (null != (item as ToolStripButton))
                {
                    res.ApplyResources(item, item.Name, ci);
                }
            }

            //Caption
            res.ApplyResources(this, "$this", ci);
            //int c = this.tableLayoutPanel1.ColumnCount;
            //this.tlpMain.Width = width;
            //this.tableLayoutPanel1.Height = height;
            //this.tlpMain.Bounds = rect;
        }

        private void FrmMain_ResizeBegin_1(object sender, EventArgs e)
        {
            Console.WriteLine("FrmMain_ResizeBegin_1");
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            Console.WriteLine("FrmMain_Resize");
        }

        private void FrmMain_ResizeEnd(object sender, EventArgs e)
        {
            Console.WriteLine("FrmMain_ResizeEnd");
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            Console.WriteLine("FrmMain_SizeChanged");
        }

        private void tlpWorkArea_MouseHover(object sender, EventArgs e)
        {
            TableLayoutPanel tlp = (TableLayoutPanel)sender;

        }

        private void tlpWorkArea_MouseEnter(object sender, EventArgs e)
        {

        }

        private void tlpWorkArea_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void tlpWorkArea_MouseMove(object sender, MouseEventArgs e)
        {
            int x=e.X;
            TableLayoutPanel tlp = (TableLayoutPanel)sender;
            float posCol1Border = tlp.ColumnStyles[0].Width;
            float posCol2Border = tlp.ColumnStyles[1].Width + posCol1Border;
            if((Math.Abs(x-posCol1Border) < 10) || (Math.Abs(x-posCol2Border)<10)) {
                tlp.Cursor = Cursors.SizeWE;
            } else {
                tlp.Cursor = Cursors.Default;
            }
        }
    }
}