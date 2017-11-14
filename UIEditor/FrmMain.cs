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
using UIEditor.Component;
using UIEditor.Entity;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Threading;
using System.ComponentModel;
using UIEditor.Drawing;
using UIEditor.CommandManager;
using UIEditor.Entity.Control;
using Ionic.Zip;
using UIEditor.UserUIControl;
using System.Runtime.InteropServices;
using Utils;
using Structure;
using Upgrade;
using System.Reflection;

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
        private CommandQuene cqdo { get; set; }
        private KeyboardHook k_hook { get; set; }
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private FrmProgress ProgressOpenProject;
        #endregion

        #region 公共方法
        public void ProjectChanged()
        {
            ProjectChanged(null);
        }
        #endregion

        #region 窗体构造函数
        public FrmMain(string arg)
        {
            try
            {
                string localize = AppConfigHelper.GetAppConfig(MyConst.XmlTagAppLanguange);
                if (!string.IsNullOrWhiteSpace(localize))
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(localize);
                }

                InitializeComponent();

                this.Text = UIResMang.GetString("AppName");
                this.tsmiAbout.Text = UIResMang.GetString("About") + " " + UIResMang.GetString("AppName");

                CheckTabControl();

                this.ucpm.RemoveNode += new UIEditor.UserUIControl.UCProjectManager.RemoveNodeEventDelegate(this.ucpm_RemoveNode);
                this.ucpm.TreeViewChangedEvent += new UIEditor.UserUIControl.UCProjectManager.TreeViewChangedEventDelegate(this.ucpm_TreeViewChanged);
                this.ucpm.DisplayNodeProperty += new UIEditor.UserUIControl.UCProjectManager.DisplayNodePropertyEventDelegate(this.ucpm_DisplayNodeProperty);
                this.ucpm.DisplayPage += new UIEditor.UserUIControl.UCProjectManager.DisplayPageEventDelegate(this.ucpm_DisplayPage);
                this.ucpm.NodeDoubleClickEvent += new UIEditor.UserUIControl.UCProjectManager.NodeDoubleClickEventDelegate(this.ucpm_NodeDoubleClick);

                this.ucpo.AddNode += new UIEditor.UserUIControl.UCPageOutline.AddNodeEventDelegate(this.ucpo_AddNode);
                this.ucpo.RemoveNode += new UIEditor.UserUIControl.UCPageOutline.RemoveNodeEventDelegate(this.ucpo_RemoveNode);
                this.ucpo.TreeViewChangedEvent += new UIEditor.UserUIControl.UCPageOutline.TreeViewChangedEventDelegate(this.ucpo_TreeViewChange);
                this.ucpo.DisplayNodeProperty += new UIEditor.UserUIControl.UCPageOutline.DisplayNodePropertyEventDelegate(this.ucpo_DisplayNodeProperty);
                this.ucpo.NodeClickEvent += new UIEditor.UserUIControl.UCPageOutline.NodeClickEventDelegate(this.ucpo_NodeClick);

                this.ucp.NodePropertyChange += new UIEditor.UserUIControl.UCProperty.NodePropertyChangeEvent(this.ucp_NodePropertyChanged);

                this.k_hook = new KeyboardHook();
                this.k_hook.KeyDownEvent += new KeyEventHandler(tabControl_KeyDown);//钩住键按下
                this.k_hook.KeyUpEvent += new KeyEventHandler(tabControl_KeyUp);
                this.k_hook.Start();//安装键盘钩子 

                if ((null != arg) && (arg.Length > 0))
                {
                    this.BGWOpenProject.RunWorkerAsync(arg);
                    this.ProgressOpenProject = new FrmProgress(this.BGWOpenProject);
                    this.ProgressOpenProject.Text = string.Format(UIResMang.GetString("OpeningProject"), arg);
                    this.ProgressOpenProject.ShowDialog(this);
                    this.ProgressOpenProject.Close();
                }

                // 获取是否显示标尺的标志
                string disRuler = AppConfigHelper.GetAppConfig(MyConst.XmlTagRuler);
                if ("yes".Equals(disRuler))
                {
                    MyCache.DisplayRuler = true;
                    this.tsmiRuler.Image = UIResMang.GetImage("CheckMark_128");
                }
                else
                {
                    MyCache.DisplayRuler = false;
                    this.tsmiRuler.Image = null;
                }

                tsmiCheckUpdate.Enabled = false;
                CheckUpdate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            tsmiCheckUpdate.Enabled = true;
        }
        #endregion

        #region 窗体事件
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            this.k_hook.Stop();

            if (!this.Saved)
            {
                var result = MessageBox.Show(UIResMang.GetString("Message7"), UIResMang.GetString("Message4"), MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (DialogResult.Cancel == result)
                {

                }
                else if (DialogResult.Yes == result)
                {
                    SaveKnxUiProject(ProjectFile);
                }
            }

            Cursor = Cursors.Default;
        }

        private void tsMain_Paint(object sender, PaintEventArgs e)
        {
            ToolStrip ts = sender as ToolStrip;
            if (ts.RenderMode == ToolStripRenderMode.System)
            {
                Rectangle rect = new Rectangle(0, 0, ts.Width, ts.Height - 2);
                e.Graphics.SetClip(rect);
            }
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            TabControlKeyDown(e);
        }

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
        {
            TabControlKeyUp(e);
        }
        #endregion

        #region 私有方法
        #region 事件通知回调
        #region ucpm - 工程管理器的事件通知
        private void ucpm_RemoveNode(object sender, EventArgs e)
        {
            ViewNode node = sender as ViewNode;
            switch (node.Name)
            {
                case MyConst.View.KnxAppType:
                    break;

                default:
                    UndoRedo();
                    CheckTabControl();
                    break;
            }
        }

        private void ucpm_TreeViewChanged(object sender, EventArgs e)
        {
            if (null != sender)
            {
                ProjectChanged(sender as ViewNode);
            }
        }

        private void ucpm_DisplayNodeProperty(object sender, EventArgs e)
        {
            if (null != sender)
            {
                ViewNode node = sender as ViewNode;
                if (null != node)
                {
                    PageNode pageNode = node as PageNode;
                    if (null != pageNode)
                    {
                        DisplayNodeProperty(pageNode.GetTwinsPageNode());
                    }
                    else
                    {
                        DisplayNodeProperty(sender as ViewNode);
                    }
                }
            }
        }

        private void ucpm_DisplayPage(object sender, EventArgs e)
        {
            if (null != sender)
            {
                PageNode node = sender as PageNode;
                if (null != node)
                {
                    DisplayPageNode(node.GetTwinsPageNode());
                }
            }
        }

        private void ucpm_NodeDoubleClick(object sender, EventArgs e)
        {
            if (null != sender)
            {
                ViewNode node = sender as ViewNode;
                if (null != node)
                {
                    switch (node.Name)
                    {
                        case MyConst.View.KnxAppType:
                        case MyConst.View.KnxAreaType:
                        case MyConst.View.KnxRoomType:
                            DisplayNodeProperty(node);
                            break;

                        case MyConst.View.KnxPageType:
                            PageNode pageNode = node as PageNode;
                            if (null != pageNode)
                            {
                                DisplayPageNode(pageNode.GetTwinsPageNode());
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        #region ucpo - 页面管理器的事件通知
        private void ucpo_AddNode(object sender, NodeAddEventArgs e)
        {
            ViewNode node = sender as ViewNode;
            if ((MyConst.View.KnxAppType == node.Name) || (MyConst.View.KnxAreaType == node.Name) ||
                (MyConst.View.KnxRoomType == node.Name) || (MyConst.View.KnxPageType == node.Name))
            {
                return;
            }

            AddControl(e.mPageNode, e.mNode);
        }

        private void ucpo_RemoveNode(object sender, NodeRemoveEventArgs e)
        {
            RemoveControl(e.mPageNode, e.mNode);
        }

        private void ucpo_TreeViewChange(object sender, EventArgs e)
        {
            if (null != sender)
            {
                ProjectChanged(sender as ViewNode);
            }
        }

        private void ucpo_DisplayNodeProperty(object sender, EventArgs e)
        {
            if (null != sender)
            {
                DisplayNodeProperty(sender as ViewNode);
            }
        }

        private void ucpo_NodeClick(object sender, EventArgs e)
        {
            if (null != sender)
            {
                ViewNode node = sender as ViewNode;
                if (null != node)
                {
                    if (MyConst.View.KnxPageType == node.Name)
                    {
                        PageNode pageNode = node as PageNode;
                        SetSelectedControl(pageNode, node);

                        DisplayNodeProperty(node);
                    }
                    else if (EntityHelper.IsControlNode(node.Name))
                    {
                        PageNode pageNode = ViewNode.GetPageNodeFromParent(node);
                        if (null != pageNode)
                        {
                            SetSelectedControl(pageNode, node);

                            DisplayNodeProperty(node);

                            List<ViewNode> nodes = new List<ViewNode>();
                            nodes.Add(node);
                            SetSelectedControlProjection(pageNode, nodes);
                        }
                    }
                }
            }
        }
        #endregion

        #region ucp - Node属性管理器的事件通知
        private void ucp_NodePropertyChanged(object sender, UCPropertyChangedEventArgs e)
        {
            if (null != sender)
            {
                ViewNode node = sender as ViewNode;

                if (null != node)
                {
                    PageNode pageNode = node as PageNode;
                    if (null != pageNode)
                    {
                        RefreshSTTabPage(pageNode);
                    }
                    else
                    {
                        pageNode = ViewNode.GetPageNodeFromParent(node);
                        if (null != pageNode)
                        {
                            RefreshSTTabPageContent(pageNode, node);
                        }
                    }

                    if (MyConst.View.KnxAppType == node.Name)
                    {
                        if (("Width" == e.pi.Name) || ("Height" == e.pi.Name))
                        {
                            MyCache.AppSize = node.Size;

                            SetPageNodeNewSize(node, node.Size);

                            RefreshTabPages();
                        }
                    }

                    RefreshProjectOutline();
                    ProjectChanged(node);
                }
            }
        }
        #endregion

        #region 撤销、重做队列更新通知
        private void CommandManager_UndoStateChanged(object sender, CommandEventArgs e)
        {
            SetUndoState(e.Valid);
        }

        private void CommandManager_ReverseStateChanged(object sender, CommandEventArgs e)
        {
            SetRedoState(e.Valid);
        }
        #endregion
        #endregion

        #region 下拉菜单点击事件
        #region 文件
        private void tsmiNew_Click(object sender, EventArgs e)
        {
            try
            {
                NewKnxUiProject();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenKnxUiPrject();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveKnxUiProject(ProjectFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = KnxFilter;
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        this.ProjectFile = saveFileDialog.FileName;
                        SaveProject(this.ProjectFile);
                    }
                    catch (Exception ex)
                    {
                        string errorMsg = UIResMang.GetString("Message5") + " " + "exception message: " + ex.Message;
                        MessageBox.Show(errorMsg, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Log.Error(errorMsg + LogHelper.Format(ex));
                    }

                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            try
            {
                CloseProject();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 编辑
        private void tsmiUndo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Undo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiRedo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Redo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiCut_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tabControl.TabCount > 0)
                {
                    CutControls();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tabControl.TabCount > 0)
                {
                    CopyControls();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tabControl.TabCount > 0)
                {
                    PasteControls();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region KNX
        private void tsmiKNXAddress_Click(object sender, EventArgs e)
        {
            try
            {
                OpenGroupAddressMgr();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 布局
        private void tsmiAlignLeft_Click(object sender, EventArgs e)
        {
            try
            {
                AlignLeft();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiAlignRight_Click(object sender, EventArgs e)
        {
            try
            {
                AlignRight();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiAlignTop_Click(object sender, EventArgs e)
        {
            try
            {
                AlignTop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiAlignBottom_Click(object sender, EventArgs e)
        {
            try
            {
                AlignBottom();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiAlignHorizontalCenter_Click(object sender, EventArgs e)
        {
            try
            {
                AlignHorizontalCenter();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiAlignVerticalCenter_Click(object sender, EventArgs e)
        {
            try
            {
                AlignVerticalCenter();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiHorizontalEquidistanceAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                HorizontalEquidistanceAlignment();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiVerticalEquidistanceAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                VerticalEquidistanceAlignment();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiWidthAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                WidthAlignment();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiHeightAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                HeightAlignment();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiCenterHorizontal_Click(object sender, EventArgs e)
        {
            try
            {
                CenterHorizontal();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiCenterVertical_Click(object sender, EventArgs e)
        {
            try
            {
                CenterVertical();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 组件
        private void tsmiArea_Click(object sender, EventArgs e)
        {

        }

        private void tsmiRoom_Click(object sender, EventArgs e)
        {

        }

        private void tsmiPage_Click(object sender, EventArgs e)
        {

        }

        private void tsmiGroupBox_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(GroupBoxNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiBlinds_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(BlindsNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiLabel_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(LabelNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiScene_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(SceneButtonNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiSliderSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(SliderSwitchNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(SwitchNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiValueDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(ValueDisplayNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiTimer_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(TimerButtonNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiDigitalAdjustment_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(DigitalAdjustmentNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiImageButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(ImageButtonNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 视图
        private void tsmiZoomIn_Click(object sender, EventArgs e)
        {
            try
            {
                ZoomCurrentTabPage(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
                ZoomCurrentTabPage(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiRuler_Click(object sender, EventArgs e)
        {
            try
            {
                if (MyCache.DisplayRuler)
                {
                    AppConfigHelper.UpdateAppConfig(MyConst.XmlTagRuler, "no");
                    MyCache.DisplayRuler = false;
                    this.tsmiRuler.Image = null;
                }
                else
                {
                    AppConfigHelper.UpdateAppConfig(MyConst.XmlTagRuler, "yes");
                    MyCache.DisplayRuler = true;
                    this.tsmiRuler.Image = UIResMang.GetImage("CheckMark_128");
                }

                ChangeRulerStatus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiAdaptiveScreen_Click(object sender, EventArgs e)
        {
            try
            {
                AdaptiveScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 语言
        private void tsmi_en_US_Click(object sender, EventArgs e)
        {
            try
            {
                AppConfigHelper.UpdateAppConfig(MyConst.XmlTagAppLanguange, "en-US");

                Application.Restart();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmi_zh_CN_Click(object sender, EventArgs e)
        {
            try
            {
                AppConfigHelper.UpdateAppConfig(MyConst.XmlTagAppLanguange, "zh-CN");

                Application.Restart();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 帮助
        private void tsmiOpenHelp_Click(object sender, EventArgs e)
        {
            try
            {
                new FrmHelp().ShowDialog(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsmiCheckUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                tsmiCheckUpdate.Enabled = false;
                if (!CheckUpdate())
                {
                    MessageBox.Show(UIResMang.GetString("UpdatedTips"), UIResMang.GetString("Upgrade"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                MessageBox.Show(ex.Message, UIResMang.GetString("Upgrade"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            tsmiCheckUpdate.Enabled = true;
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            try
            {
                new FrmAboutBox().ShowDialog(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #endregion

        #region 工具条点击事件
        #region 工程 - 新建、打开、关闭
        private void tsbNewProj_Click(object sender, EventArgs e)
        {
            try
            {
                NewKnxUiProject();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbOpenProj_Click(object sender, EventArgs e)
        {
            try
            {
                OpenKnxUiPrject();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbSaveProj_Click(object sender, EventArgs e)
        {
            try
            {
                SaveKnxUiProject(ProjectFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region KNX地址
        private void tsbKNXAddr_Click(object sender, EventArgs e)
        {
            try
            {
                OpenGroupAddressMgr();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 撤销、重做
        private void tsbUndo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Undo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbRedo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Redo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 控件
        private void tsrBtnGroupBox_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(GroupBoxNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnAddBlinds_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(BlindsNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnAddLabel_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(LabelNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnAddSceneButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(SceneButtonNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnAddSliderSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(SliderSwitchNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnAddSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(SwitchNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnAddValueDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(ValueDisplayNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnAddTimerButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(TimerButtonNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnDigitalAdjustment_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(DigitalAdjustmentNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnImageButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(ImageButtonNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbShutter_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(ShutterNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbDimmer_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(DimmerNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbWebCamer_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(WebCamerNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbMediaButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(MediaButtonNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAirCondition_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(AirConditionNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbHVAC_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewControl(typeof(HVACNode));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 对齐
        private void tsbAlignLeft_Click(object sender, EventArgs e)
        {
            try
            {
                AlignLeft();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAlignRight_Click(object sender, EventArgs e)
        {
            try
            {
                AlignRight();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAlignTop_Click(object sender, EventArgs e)
        {
            try
            {
                AlignTop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAlignBottom_Click(object sender, EventArgs e)
        {
            try
            {
                AlignBottom();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAlignHorizontalCenter_Click(object sender, EventArgs e)
        {
            try
            {
                AlignHorizontalCenter();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAlignVerticalCenter_Click(object sender, EventArgs e)
        {
            try
            {
                AlignVerticalCenter();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbHorizontalEquidistanceAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                HorizontalEquidistanceAlignment();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbVerticalEquidistanceAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                VerticalEquidistanceAlignment();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbWidthAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                WidthAlignment();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbHeightAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                HeightAlignment();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbCenterHorizontalInParent_Click(object sender, EventArgs e)
        {
            try
            {
                CenterHorizontal();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbCenterVerticalInParent_Click(object sender, EventArgs e)
        {
            try
            {
                CenterVertical();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 视图
        private void tscbViewScale_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                ToolStripComboBox cbb = sender as ToolStripComboBox;
                string nv = cbb.Text;
                int v = int.Parse(nv);
                float ratio = (float)v / 100;

                ViewScale(ratio);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tscbViewScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ToolStripComboBox cbb = sender as ToolStripComboBox;
                string nv = cbb.Text;
                nv = nv.Substring(0, nv.IndexOf("%"));
                int v = int.Parse(nv);
                float ratio = (float)v / 100;

                ViewScale(ratio);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbZoomIn_Click(object sender, EventArgs e)
        {
            try
            {
                ZoomCurrentTabPage(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
                ZoomCurrentTabPage(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAdaptiveScreen_Click(object sender, EventArgs e)
        {
            try
            {
                AdaptiveScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #endregion

        #region 下拉菜单选项 工具条快捷按钮 状态
        private void SetCloseProject(bool enable)
        {
            try
            {
                this.tsmiClose.Enabled = enable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SetSaveAsState(bool enable)
        {
            this.tsmiSaveAs.Enabled = enable;
        }

        private void SetSaveState(bool enable)
        {
            this.tsbSaveProj.Enabled = enable;

            this.tsmiSave.Enabled = enable;
        }

        private void SetKNXAddrState(bool enable)
        {
            this.tsbKNXAddr.Enabled = enable;

            this.tsmiKNXAddress.Enabled = enable;
        }

        private void SetUndoState(bool enable)
        {
            this.tsmiUndo.Enabled = enable;
            this.tsbUndo.Enabled = enable;
        }

        private void SetRedoState(bool enable)
        {
            this.tsmiRedo.Enabled = enable;
            this.tsbRedo.Enabled = enable;
        }

        private void SetControlState(bool enable)
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
            this.tsrBtnImageButton.Enabled = enable;
            this.tsbShutter.Enabled = enable;
            this.tsbDimmer.Enabled = enable;
            this.tsbWebCamer.Enabled = enable;
            this.tsbMediaButton.Enabled = enable;
            this.tsbAirCondition.Enabled = enable;
            this.tsbHVAC.Enabled = enable;

            this.tsmiGroupBox.Enabled = enable;
            this.tsmiBlinds.Enabled = enable;
            this.tsmiLabel.Enabled = enable;
            this.tsmiScene.Enabled = enable;
            this.tsmiSliderSwitch.Enabled = enable;
            this.tsmiSwitch.Enabled = enable;
            this.tsmiValueDisplay.Enabled = enable;
            this.tsmiTimer.Enabled = enable;
            this.tsmiDigitalAdjustment.Enabled = enable;
            this.tsmiImageButton.Enabled = enable;
        }

        private void SetLayoutAlignmentState(bool enable)
        {
            this.tsbAlignLeft.Enabled = enable;
            this.tsbAlignRight.Enabled = enable;
            this.tsbAlignTop.Enabled = enable;
            this.tsbAlignBottom.Enabled = enable;
            this.tsbAlignHorizontalCenter.Enabled = enable;
            this.tsbAlignVerticalCenter.Enabled = enable;
            this.tsbHorizontalEquidistanceAlignment.Enabled = enable;
            this.tsbVerticalEquidistanceAlignment.Enabled = enable;
            this.tsbWidthAlignment.Enabled = enable;
            this.tsbHeightAlignment.Enabled = enable;

            this.tsmiAlignLeft.Enabled = enable;
            this.tsmiAlignRight.Enabled = enable;
            this.tsmiAlignTop.Enabled = enable;
            this.tsmiAlignBottom.Enabled = enable;
            this.tsmiAlignHorizontalCenter.Enabled = enable;
            this.tsmiAlignVerticalCenter.Enabled = enable;
            this.tsmiHorizontalEquidistanceAlignment.Enabled = enable;
            this.tsmiVerticalEquidistanceAlignment.Enabled = enable;
            this.tsmiWidthAlignment.Enabled = enable;
            this.tsmiHeightAlignment.Enabled = enable;
        }

        private void SetCenterParentState(bool enable)
        {
            this.tsmiCenterHorizontalInParent.Enabled = enable;
            this.tsmiCenterVerticalInParent.Enabled = enable;

            this.tsbCenterHorizontalInParent.Enabled = enable;
            this.tsbCenterVerticalInParent.Enabled = enable;
        }

        private void SetViewItemState(bool enable)
        {
            this.tsmiZoomIn.Enabled = enable;
            this.tsmiZoomOut.Enabled = enable;
            this.tsmiAdaptiveScreen.Enabled = enable;
            this.tsmiRuler.Enabled = enable;

            this.tscbViewScale.Enabled = enable;
            this.tsbZoomIn.Enabled = enable;
            this.tsbZoomOut.Enabled = enable;
            this.tsbAdaptiveScreen.Enabled = enable;
        }
        #endregion

        #region 工程操作相关附属
        private static void CreateProjectFolder()
        {
            // 新建项目文件
            var workFolder = "knxuieditor_" + DateTime.Now.Ticks;
            MyCache.WorkFolder = Path.Combine(MyCache.DefaultKnxCacheFolder, workFolder);

            MyCache.ProjectFolder = Path.Combine(MyCache.WorkFolder, MyConst.ProjFolder);
            MyCache.ProjResfolder = Path.Combine(MyCache.ProjectFolder, MyConst.ResFolder);
            MyCache.ProjImgPath = Path.Combine(MyCache.ProjResfolder, MyConst.ImgFolder);

            MyCache.ProjTempFolder = Path.Combine(MyCache.WorkFolder, MyConst.TempFolder);
            MyCache.ProjTempImgFolder = Path.Combine(MyCache.ProjTempFolder, MyConst.ImgFolder);
            MyCache.ProjTempCollFolder = Path.Combine(MyCache.ProjTempFolder, MyConst.CollFolder);

            Directory.CreateDirectory(MyCache.ProjImgPath);

            Directory.CreateDirectory(MyCache.ProjTempFolder);
            Directory.CreateDirectory(MyCache.ProjTempImgFolder);
        }
        #endregion

        #region 工程操作
        #region 新建工程
        private void NewKnxUiProject()
        {
            if (Saved == false)
            {
                var result = MessageBox.Show(UIResMang.GetString("Message7"), UIResMang.GetString("Message4"), MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    SaveKnxUiProject(ProjectFile);
                }
                else if (DialogResult.No == result)
                {
                    NewProject();
                }
            }
            else
            {
                NewProject();
            }
        }

        private void NewProject()
        {
            CreateProjectFolder();

            var myDialog = new SaveFileDialog();
            myDialog.Title = UIResMang.GetString("StoreIn");
            myDialog.InitialDirectory = MyCache.DefaultKnxProjectFolder;
            myDialog.FileName = MyConst.DefaultKnxUiProjectName;
            myDialog.DefaultExt = MyConst.KnxUiEditorFileExt;
            myDialog.Filter = KnxFilter;

            var myResult = myDialog.ShowDialog(this);

            if (DialogResult.OK == myResult)
            {
                this.ProjectFile = myDialog.FileName;

                //ResetParameter();
                CloseAllTabPages();

                AppNode appNode = new AppNode();
                AreaNode areaNode = new AreaNode();
                RoomNode roomNode = new RoomNode();
                PageNode pageNode = new PageNode();
                roomNode.Nodes.Add(pageNode);
                areaNode.Nodes.Add(roomNode);
                appNode.Nodes.Add(areaNode);
                //SetProjectOutline(appNode);
                //SetPageOutline(null);
                //DisplayNodeProperty(appNode);
                MyCache.Project = appNode;

                SetWindowStateProjectPresent(appNode, this.ProjectFile);

                pageNode.CreateTwinsPageNode();

                SetKNXAddrState(true);
                SetSaveAsState(true);
                SetCloseProject(true);

                //ShowProjectFile(this.ProjectFile);
            }
        }
        #endregion

        #region 打开工程
        private void OpenKnxUiPrject()
        {
            Cursor = Cursors.WaitCursor;

            if (Saved == false)
            {
                var result = MessageBox.Show(UIResMang.GetString("Message7"), UIResMang.GetString("Message4"), MessageBoxButtons.YesNoCancel,
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

            SetWindowStateNoProject();

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
                        this.BGWOpenProject.RunWorkerAsync(ofd.FileName);
                        this.ProgressOpenProject = new FrmProgress(this.BGWOpenProject);
                        this.ProgressOpenProject.Text = string.Format(UIResMang.GetString("OpeningProject"), ofd.FileName);
                        this.ProgressOpenProject.ShowDialog(this);
                        this.ProgressOpenProject.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(ex.Message + LogHelper.Format(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BGWOpenProject_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            this.ProjectFile = e.Argument as string;

            try
            {
                worker.ReportProgress(0, UIResMang.GetString("InitializeEnviroment"));
                CreateProjectFolder();

                worker.ReportProgress(0, UIResMang.GetString("TextIsUnziping") + this.ProjectFile);
                ZipHelper.UnZipDir(this.ProjectFile, MyCache.ProjectFolder, MyConst.MyKey);

                KNXVersion projectVersion = VersionStorage.Load();
                MyCache.VersionOfImportedFile = projectVersion;

                MyCache.GroupAddressTable = GroupAddressStorage.Load();

                worker.ReportProgress(0, UIResMang.GetString("ConvertToJSON"));
                var app = AppStorage.Load();

                if (app != null)
                {
                    // 导入所有节点
                    AppNode appNode = EntityHelper.ImportNode(app, worker);
                    MyCache.Project = appNode;

                    worker.ReportProgress(100, "");

                    this.BeginInvoke(
                        new MethodInvoker(
                            delegate
                            {
                                SetWindowStateProjectPresent(appNode, this.ProjectFile);
                            }));

                    Saved = true;
                }
                else
                {
                    throw new ApplicationException(UIResMang.GetString("Message8"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(ex.Message + LogHelper.Format(ex));
            }
        }

        private void BGWOpenProject_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.ProgressOpenProject.Close();
        }
        #endregion

        #region 保存工程
        private bool SaveKnxUiProject(string fileName)
        {
            bool result = false;

            try
            {
                // 是否指定了项目文件名
                if (fileName == MyConst.DefaultKnxUiProjectName)
                {
                    var myDialog = new SaveFileDialog();
                    myDialog.Title = UIResMang.GetString("StoreIn");
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
                        this.ProjectFile = myDialog.FileName;
                        SaveProject(this.ProjectFile);
                    }
                }
                else
                {
                    SaveProject(this.ProjectFile);
                }
            }
            catch (Exception ex)
            {
                string errorMsg = UIResMang.GetString("Message5") + " " + "exception message: " + ex.Message;
                MessageBox.Show(errorMsg, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
            }

            return result;
        }

        private void BGWSaveProject_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string fileName = e.Argument as string;

            try
            {
                MyCache.ValidResImgNames.Clear();

                worker.ReportProgress(0, UIResMang.GetString("SavingVersionFile"));
                VersionStorage.Save(); // 保存项目文件的版本信息。

                this.SaveNode(worker); // 保存界面到JSON文件

                worker.ReportProgress(0, UIResMang.GetString("SavingGroupAddressFile"));
                GroupAddressStorage.Save(); // 保存组地址到JSON文件

                worker.ReportProgress(0, UIResMang.GetString("TextIsZiping"));
                ZipProject(fileName);

                this.BeginInvoke(
                        new MethodInvoker(
                            delegate
                            {
                                SavePage();

                                ProjectSaved();

                                ClearCommandQueue();
                            }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BGWSaveProject_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.ProgressOpenProject.Close();
        }

        private void SaveNode(BackgroundWorker worker)
        {
            // 保存当前树上的节点为 json 文件
            var app = EntityHelper.ExportAppNodeAndResources(MyCache.Project, worker);

            worker.ReportProgress(0, UIResMang.GetString("SavingUIMetaFile"));
            // 保存 KNXApp 对象到文件
            AppStorage.Save(app);
        }

        private void SaveProject(string fullName)
        {
            this.BGWSaveProject.RunWorkerAsync(fullName);
            this.ProgressOpenProject = new FrmProgress(this.BGWSaveProject);
            this.ProgressOpenProject.Text = string.Format(UIResMang.GetString("SavingProject"), fullName);
            this.ProgressOpenProject.ShowDialog(this);
            this.ProgressOpenProject.Close();
        }

        private void ZipProject(string desFileName)
        {
            // 删除无用的图片
            foreach (string file in Directory.GetFiles(MyCache.ProjImgPath))
            {
                string name = Path.GetFileName(file);
                if (!MyCache.ValidResImgNames.Contains(name))
                {
                    File.Delete(file);
                }
            }

            // 删除所有的目录
            foreach (string dir in Directory.GetDirectories(MyCache.ProjImgPath))
            {
                Directory.Delete(dir, true);
            }

            // 压缩文件
            ZipHelper.ZipDir(MyCache.ProjectFolder, desFileName, MyConst.MyKey);

            //保存状态
            Saved = true;
            //ShowProjectFile(ProjectFile);
        }

        private void ProjectChanged(ViewNode node)
        {
            if (null != node)
            {
                this.Saved = false;

                switch (node.Name)
                {
                    case MyConst.View.KnxAppType:
                    case MyConst.View.KnxAreaType:
                    case MyConst.View.KnxRoomType:
                        break;

                    case MyConst.View.KnxPageType:
                        SetTabPageTitle(node as PageNode, node.Text);
                        break;

                    default:
                        PageNode pageNode = ViewNode.GetPageNodeFromParent(node);
                        break;
                }
            }

            SetSaveState(true);
        }

        private void ProjectSaved()
        {
            this.Saved = true;

            SetSaveState(false);
        }
        #endregion

        #region 关闭工程
        private void CloseProject()
        {
            if (Saved == false)
            {
                // 提示：当前项目未保存，是否要保存？
                var result = MessageBox.Show(UIResMang.GetString("Message7"), UIResMang.GetString("Message4"), MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == result)
                {
                    SaveKnxUiProject(ProjectFile);
                    Cursor = Cursors.Default;
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

            SetWindowStateNoProject();
        }
        #endregion
        #endregion

        #region KNX地址
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
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                string errorMsg = UIResMang.GetString("Message12");
                MessageBox.Show(errorMsg, UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(errorMsg + LogHelper.Format(ex));
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region TabControl - 页面显示
        #region TabControl 右键菜单
        private ToolStripMenuItem CreateCloseMenuItem()
        {
            ToolStripMenuItem tsmiCloseItem = new ToolStripMenuItem();
            tsmiCloseItem.Name = "tsmiCloseItem";
            tsmiCloseItem.Text = UIResMang.GetString("Close");
            tsmiCloseItem.Click += new System.EventHandler(CloseTabPage_Click);

            return tsmiCloseItem;
        }

        private ToolStripMenuItem CreateCloseAllMenuItem()
        {
            ToolStripMenuItem tsmiCloseAllItem = new ToolStripMenuItem();
            tsmiCloseAllItem.Name = "tsmiCloseAllItem";
            tsmiCloseAllItem.Text = UIResMang.GetString("CloseAll");
            tsmiCloseAllItem.Click += new System.EventHandler(CloseAllTabPage_Click);

            return tsmiCloseAllItem;
        }

        private ToolStripMenuItem CreateCloseOthersMenuItem()
        {
            ToolStripMenuItem tsmiCloseOthersItem = new ToolStripMenuItem();
            tsmiCloseOthersItem.Name = "tsmiCloseOthersItem";
            tsmiCloseOthersItem.Text = UIResMang.GetString("CloseOthers");
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
            STTabPage page = this.tabControl.SelectedTab as STTabPage;
            foreach (STTabPage tp in this.tabControl.TabPages)
            {
                if (tp != page)
                {
                    this.tabControl.TabPages.Remove(tp);
                }
            }
        }
        #endregion

        #region TabControl方法
        private void CloseAllTabPages()
        {
            this.tabControl.TabPages.Clear();

            CheckTabControl();
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

                SetPageAbsentStatus();
            }
        }

        private bool IsTheCurrentTabpage(PageNode node)
        {
            STTabPage tabPage = GetExistsTabPage(node);
            if (null != tabPage)
            {
                if (this.tabControl.SelectedTab == tabPage)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private STTabPage GetExistsTabPage(PageNode node)
        {
            foreach (STTabPage tabPage in this.tabControl.TabPages)
            {
                //PageNode pageNode = tabPage.Tag as PageNode;
                PageNode pageNode = GetPageNode(tabPage);
                if ((null != pageNode) && (pageNode.Id == node.Id))
                {
                    return tabPage;
                }
            }

            return null;
        }

        private STTabPage CreateTabPage(PageNode node)
        {
            STTabPage tabPage = new STTabPage(node);
            this.tabControl.TabPages.Add(tabPage);

            ContextMenuStrip cms = new ContextMenuStrip();
            tabPage.ContextMenuStrip = cms;

            tabPage.STTabPageControlSelectedEvent += new UIEditor.Drawing.LayerControls.ControlSelectedEventDelegate(this.STTabPage_ControlChangedEvent);
            tabPage.STTabPagePageChangedEvent += new UIEditor.Drawing.LayerControls.PageChangedEventDelegate(this.STTabPage_PageChangedEvent);
            tabPage.STTabPageSelectedControlsIsBrotherhoodEvent += new UIEditor.Drawing.LayerControls.SelectedControlsIsBrotherhoodEventDelegate(this.STTabPage_SelectedControlsIsBrotherhood);
            tabPage.STTabPageSelectedControlsMoveEvent += new UIEditor.Drawing.LayerControls.SelectedControlsMoveEventDelegate(STTabPage_SelectedControlsMove);

            return tabPage;
        }

        private STTabPage GetTabPage(PageNode node)
        {
            STTabPage tabPage = GetExistsTabPage(node);
            if (null != tabPage)
            {
                return tabPage;
            }
            else
            {
                return CreateTabPage(node);
            }
        }

        private STTabPage GetCurrentTabPage()
        {
            if (null != this.tabControl.SelectedTab)
            {
                return this.tabControl.SelectedTab as STTabPage;
            }
            else
            {
                return null;
            }
        }

        private void TabControlKeyDown(KeyEventArgs e)
        {
            TabPageKeyDowns(e);
        }

        private void TabControlKeyUp(KeyEventArgs e)
        {
            TabPageKeyUps(e);
        }
        #endregion

        #region TabControl 事件
        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < this.tabControl.TabPages.Count; i++)
                {
                    STTabPage tabPage = this.tabControl.TabPages[i] as STTabPage;
                    if (this.tabControl.GetTabRect(i).Contains(new Point(e.X, e.Y)))
                    {
                        this.tabControl.SelectedTab = tabPage;
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
                STTabPage tabPage = this.tabControl.TabPages[i] as STTabPage;
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
            if (!DLLHelp.ApplicationIsActive(this.Handle))
            {
                return;
            }

            if ((int)Keys.LControlKey == e.KeyValue)
            {
            }

            if (this.tabControl.TabCount > 0)
            {
                TabPageKeyDowns(e);
            }
        }

        private void tabControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (!DLLHelp.ApplicationIsActive(this.Handle))
            {
                return;
            }

            if ((int)Keys.LControlKey == e.KeyValue)
            {
            }

            if (this.tabControl.TabCount > 0)
            {
                TabPageKeyUps(e);
            }
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            STTabPage tabPage = e.TabPage as STTabPage;
            if (null != tabPage)
            {
                PageNode node = GetPageNode(tabPage);

                SetProjectOutlineSelectedNode(node.GetTwinsPageNode());

                DisplayPageStatus(node, tabPage);

                int v = (int)(tabPage.GetViewScale() * 100);
                this.tscbViewScale.Text = v.ToString() + "%";
            }
            else
            {
                SetProjectOutlineSelectedNode(null);

                SetPageOutline(null);

                DisplayNodeProperty(null);

                SetCommandQueue(null);
            }
        }
        #endregion

        #region STPage - 页面显示的事件通知
        private void STTabPage_ControlChangedEvent(object sender, ControlSelectedEventArgs e)
        {
            ViewNode node = sender as ViewNode;
            if (null != node)
            {
                PageNode pageNode = node as PageNode;
                if (null != pageNode)
                {
                    SetProjectOutlineSelectedNode(pageNode.GetTwinsPageNode());

                    //SetCenterParentState(false);
                }
                else
                {
                    SetProjectOutlineSelectedNode(e.mPageNode.GetTwinsPageNode());

                    //SetCenterParentState(true);
                }

                SetPageOutlineSelectedNode(node);
                DisplayNodeProperty(node);
                SetSelectedControlProjection(e.mPageNode, e.mNodes);
            }
        }

        private void STTabPage_PageChangedEvent(object sender, EventArgs e)
        {
            ViewNode node = sender as ViewNode;
            ProjectChanged(node);
        }

        private void STTabPage_SelectedControlsIsBrotherhood(object sender, BrothershipEventArgs e)
        {
            SetLayoutAlignmentState(e.IsBrothership);
        }

        private void STTabPage_SelectedControlsMove(object sender, ControlSelectedEventArgs e)
        {
            if ((null != e.mPageNode) && (null != e.mNodes))
            {
<<<<<<< HEAD
                VersionStorage.Save(); // 保存项目文件的版本信息。
                this.ucdo.SaveNode(); // 保存界面到JSON文件
                GroupAddressStorage.Save(); // 保存组地址到JSON文件
=======
                SetSelectedControlProjection(e.mPageNode, e.mNodes);
            }
        }
        #endregion
        #endregion

        #region FrmMain方法
        private void ShowProjectFile(string projectFile)
        {
            this.Text = UIResMang.GetString("AppName") + " - " + projectFile;
        }
>>>>>>> SationKNXUIEditor-Modify

        private void SetWindowStateNoProject()
        {
            SetSaveState(false);
            SetSaveAsState(false);
            SetCloseProject(false);

            SetUndoState(false);
            SetRedoState(false);

            SetKNXAddrState(false);
            MyCache.GroupAddressTable.Clear();

            SetControlState(false);
            SetCenterParentState(false);
            SetViewItemState(false);

            SetProjectOutline(null);
            SetPageOutline(null);
            DisplayNodeProperty(null);

            CloseAllTabPages();

            ShowProjectFile("");

            ProjectSaved();
        }

        private void SetWindowStateProjectPresent(AppNode appNode, string projectFile)
        {
            SetProjectOutline(appNode);
            SetPageOutline(null);
            DisplayNodeProperty(appNode);

            ShowProjectFile(projectFile);

            //SetControlState(false);
            SetKNXAddrState(true);
            SetSaveAsState(true);
            SetCloseProject(true);
        }
        #endregion

        #region CommandQueue - 撤销、重做
        private void SetCommandQueue(CommandQuene cq)
        {
            if (null != cq)
            {
                this.cqdo = cq;
                this.cqdo.UndoStateChanged += new CommandQuene.CommandQueueChangedEvent(CommandManager_UndoStateChanged);
                this.cqdo.ReverseUndoStateChanged += new CommandQuene.CommandQueueChangedEvent(CommandManager_ReverseStateChanged);

                SetUndoState(this.cqdo.UndoIsValid());
                SetRedoState(this.cqdo.ReverseIsValid());
            }
            else
            {
                SetUndoState(false);
                SetRedoState(false);
            }
        }

        private void Undo()
        {
            this.cqdo.Undo();

            RefreshCurrentTabPage();

            RefreshNodeProperty();
        }

        private void Redo()
        {
            this.cqdo.ReverseUndo();

            RefreshCurrentTabPage();

            RefreshNodeProperty();
        }

        private void UndoRedo()
        {
            TreeView tv = this.ucpm.GetTreeView();
            if (tv.Nodes.Count > 0)
            {
                foreach (STTabPage tabPage in this.tabControl.TabPages)
                {
                    bool b = false;
                    foreach (TreeNode node in tv.Nodes)
                    {
                        b = ErgodicPageNode(node, GetPageNode(tabPage)/*tabPage.Tag as PageNode*/);
                        if (b)
                        {
                            break;
                        }
                    }

                    if (b)
                    {
                        STTabPage _tabPage = GetCurrentTabPage();
                        if (_tabPage == tabPage)
                        {
                            PageNode pageNode = GetPageNode(_tabPage);/*_tabPage.Tag as PageNode;*/
                            //STPage panel = pageNode.mSTPage as STPage;
                            //panel.PagePropertyChanged();
                            RefreshSTTabPage(pageNode);
                        }
                    }
                    else
                    {
                        this.tabControl.TabPages.Remove(tabPage);
                    }
                }
            }
        }

        /// <summary>
        /// 遍历TreeNode p及其子Node，找到与PageNode相同的Node
        /// </summary>
        /// <param name="p">需要遍历的Node</param>
        /// <param name="pageNode">需要匹配的Node</param>
        /// <returns></returns>
        private bool ErgodicPageNode(TreeNode p, PageNode pageNode)
        {
            foreach (TreeNode c in p.Nodes)
            {
                ViewNode cNode = c as ViewNode;
                if (MyConst.View.KnxPageType == cNode.Name)
                {
                    if (cNode.Id == pageNode.Id)
                    {
<<<<<<< HEAD
                        ProjectFile = myDialog.FileName;
                        ZipProject(this.ProjectFile);
                        result = true;
=======
                        return true;
>>>>>>> SationKNXUIEditor-Modify
                    }
                }
                else if ((MyConst.View.KnxAppType == cNode.Name)
                    || (MyConst.View.KnxAreaType == cNode.Name)
                    || (MyConst.View.KnxRoomType == cNode.Name))
                {
<<<<<<< HEAD
                    ZipProject(this.ProjectFile);
                    result = true;
                }
            }
            catch (Exception ex)
=======
                    if (ErgodicPageNode(cNode, pageNode))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ClearCommandQueue()
        {
            if (null != this.cqdo)
>>>>>>> SationKNXUIEditor-Modify
            {
                this.cqdo.Clear();
            }
        }
        #endregion

        #region 工程管理器方法
        /// <summary>
        /// 设置工程管理器中的工程
        /// </summary>
        /// <param name="node"></param>
        private void SetProjectOutline(AppNode node)
        {
            this.ucpm.SetOutlineNode(node);
        }

        private void RefreshProjectOutline()
        {
            this.ucpm.RefreshProjectTree();
        }

        /// <summary>
        /// 在工程管理器的树结构中选中节点
        /// </summary>
        /// <param name="node">被选中的节点</param>
        private void SetProjectOutlineSelectedNode(ViewNode node)
        {
            this.ucpm.SetSelectedNode(node);
        }
        #endregion

        #region 页面管理器方法
        private void SetPageOutline(PageNode node)
        {
            this.ucpo.SetPageNode(node);
        }

        private void SetPageOutlineCommandQueue(CommandQuene cq)
        {
            this.ucpo.SetCommandQueue(cq);
        }

        /// <summary>
        /// 在页面管理器的树结构中选中节点
        /// </summary>
        /// <param name="node">被选中的节点</param>
        private void SetPageOutlineSelectedNode(ViewNode node)
        {
            this.ucpo.SetSelectedNode(node);
        }
        #endregion

        #region 属性管理器方法
        /// <summary>
        /// 在属性框中显示节点的属性
        /// </summary>
        /// <param name="node">要显示属性的节点</param>
        private void DisplayNodeProperty(ViewNode node)
        {
            this.ucp.DisplayNode(node);

            if (null != node)
            {
                switch (node.Name)
                {
                    case MyConst.View.KnxAppType:
                    case MyConst.View.KnxAreaType:
                    case MyConst.View.KnxRoomType:
                    case MyConst.View.KnxPageType:
                        SetPropertyCommandQueue(null);
                        break;
                }
            }
            else
            {
                SetPropertyCommandQueue(null);
            }
        }

        private void SetPropertyCommandQueue(CommandQuene cq)
        {
            this.ucp.SetCommandQueue(cq);
        }

        private void RefreshNodeProperty()
        {
            this.ucp.RefreshNodeProperty();
        }
        #endregion

        #region STTabPage
        private PageNode GetPageNode(STTabPage tabPage)
        {
            return tabPage.GetPageNode();
        }

        private void RefreshTabPages()
        {
            foreach (STTabPage tabPage in this.tabControl.TabPages)
            {
                if (null != tabPage)
                {
                    PageNode curPageNode = GetPageNode(tabPage);
                    RefreshSTTabPage(curPageNode);
                }
            }
        }

<<<<<<< HEAD
        private void ZipProject(string desFileName)
=======
        private void RefreshCurrentTabPage()
>>>>>>> SationKNXUIEditor-Modify
        {
            STTabPage tabPage = GetCurrentTabPage();
            PageNode curPageNode = GetPageNode(tabPage);

<<<<<<< HEAD
            //保存项目文件为 knxuie 类型
            ZipHelper.ZipDir(MyCache.ProjectFolder, desFileName, MyConst.MyKey);

            //保存状态
            Saved = true;
            ShowProjectFile(ProjectFile);
=======
            RefreshSTTabPage(curPageNode);
>>>>>>> SationKNXUIEditor-Modify
        }

        private void SetTabPageTitle(PageNode pNode, string title)
        {
            STTabPage tabPage = GetExistsTabPage(pNode);
            if (null != tabPage)
            {
                tabPage.Invoke(
                    new MethodInvoker(
                        delegate
                        {
                            tabPage.Text = title;
                        }
                    )
                );
            }
        }

        private void SavePage()
        {
            foreach (STTabPage tabPage in this.tabControl.TabPages)
            {
                tabPage.Save();
            }
        }

        /// <summary>
        /// 缩放当前的TabPage
        /// </summary>
        /// <param name="zoomIn">true, 放大；false，缩小</param>
        private void ZoomCurrentTabPage(bool zoomIn)
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                if (zoomIn)
                {
                    tabPage.ZoomIn();
                }
                else
                {
                    tabPage.ZoomOut();
                }

                int v = (int)(tabPage.GetViewScale() * 100);
                this.tscbViewScale.Text = v.ToString() + "%";
            }
        }

        private void AdaptiveScreen()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.SelfAdapter();

                int v = (int)(tabPage.GetViewScale() * 100);
                this.tscbViewScale.Text = v.ToString() + "%";
            }
        }

        private void ViewScale(float ratio)
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.SetViewScale(ratio);
            }
        }

        private void SetSelectedControlProjection(PageNode pageNode, List<ViewNode> cNode)
        {
            STTabPage tabPage = GetExistsTabPage(pageNode);
            STTabPage curTabPage = GetCurrentTabPage();
            if (tabPage == curTabPage)
            {
                tabPage.SetSelectedControlProjection(cNode);
            }
        }

        private void ChangeRulerStatus()
        {
            RefreshTabPages();
        }

        private void SetSelectedControl(PageNode pNode, ViewNode node)
        {
            STTabPage tabPage = GetExistsTabPage(pNode);
            if (null != tabPage)
            {
                tabPage.SelectControl(node);
            }
        }

        private void AddControl(PageNode pNode, ViewNode node)
        {
            STTabPage tabPage = GetExistsTabPage(pNode);
            if (null != tabPage)
            {
                tabPage.AddControl(node);
            }
        }

        private void RemoveControl(PageNode pNode, ViewNode node)
        {
            STTabPage tabPage = GetExistsTabPage(pNode);
            if (null != tabPage)
            {
                tabPage.RemoveControl(node);
            }
        }

        private void RefreshSTTabPage(PageNode pNode)
        {
            STTabPage tabPage = GetExistsTabPage(pNode);
            if (null != tabPage)
            {
                tabPage.PropertyChanged();
            }
        }

        private void RefreshSTTabPageContent(PageNode pNode, ViewNode node)
        {
            STTabPage tabPage = GetExistsTabPage(pNode);
            if (null != tabPage)
            {
                tabPage.ControlPropertyChanged(node);
            }
        }

        private void AddNewControl(Type type)
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.AddNewControl(type);
            }
        }

        private void CutControls()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.CutControls();
            }
        }

        private void CopyControls()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.CopyControls();
            }
        }

        private void PasteControls()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.PasteControls();
            }
        }

        private void TabPageKeyDowns(KeyEventArgs e)
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.KeyDowns(e);
            }
        }

        private void TabPageKeyUps(KeyEventArgs e)
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.KeyUps(e);
            }
        }

        #region 布局
        private void AlignLeft()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.AlignLeft();
            }
        }

        private void AlignRight()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.AlignRight();
            }
        }

        private void AlignTop()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.AlignTop();
            }
        }

        private void AlignBottom()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.AlignBottom();
            }
        }

        private void AlignHorizontalCenter()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.AlignHorizontalCenter();
            }
        }

        private void AlignVerticalCenter()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.AlignVerticalCenter();
            }
        }

        private void HorizontalEquidistanceAlignment()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.HorizontalEquidistanceAlignment();
            }
        }

        private void VerticalEquidistanceAlignment()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.VerticalEquidistanceAlignment();
            }
        }

        private void WidthAlignment()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.WidthAlignment();
            }
        }

        private void HeightAlignment()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.HeightAlignment();
            }
        }

        private void CenterHorizontal()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.CenterHorizontal();
            }
        }

        private void CenterVertical()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                tabPage.CenterVertical();
            }
        }
        #endregion

        private CommandQuene GetCommandQueue()
        {
            STTabPage tabPage = GetCurrentTabPage();
            if (null != tabPage)
            {
                return tabPage.GetCommandQueue();
            }
            else
            {
                return null;
            }
        }
        #endregion

        private void DisplayPageNode(PageNode node)
        {
            if (null != node)
            {
                this.tabControl.Enabled = true;
                STTabPage tabPage = GetTabPage(node);
                this.tabControl.SelectedTab = tabPage;
                CheckTabControl();

                SetControlState(true);
                SetViewItemState(true);

                DisplayPageStatus(node, tabPage);
            }
        }

        private void DisplayPageStatus(PageNode node, STTabPage tabPage)
        {
            SetPageOutline(node);
            SetPageOutlineCommandQueue(GetCommandQueue());

            DisplayNodeProperty(node);
            SetPropertyCommandQueue(GetCommandQueue());

            SetCommandQueue(GetCommandQueue());

            SetCenterParentState(true);

            int v = (int)(tabPage.GetViewScale() * 100);
            this.tscbViewScale.Text = v.ToString() + "%";
        }

        private void SetPageAbsentStatus()
        {
            SetControlState(false);
            SetViewItemState(false);
            SetLayoutAlignmentState(false);
            SetCenterParentState(false);
        }

        /// <summary>
        /// 根据Node Id查找对应的PageNode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private PageNode GetPageNode(ViewNode pNode, int id)
        {
            if (id == pNode.Id)
            {
                return pNode as PageNode;
            }
            else if ((MyConst.View.KnxAppType == pNode.Name)
              || (MyConst.View.KnxAreaType == pNode.Name)
              || (MyConst.View.KnxRoomType == pNode.Name))
            {
                foreach (ViewNode cNode in pNode.Nodes)
                {
                    PageNode node = GetPageNode(cNode, id);
                    if (null != node)
                    {
                        return node;
                    }
                }
            }

            return null;
        }

        private void SetPageNodeNewSize(ViewNode pNode, Size size)
        {
            PageNode pageNode = pNode as PageNode;
            if (null != pageNode)
            {
                pageNode.SetNewSize(size);
            }
            else if ((MyConst.View.KnxAppType == pNode.Name)
              || (MyConst.View.KnxAreaType == pNode.Name)
              || (MyConst.View.KnxRoomType == pNode.Name))
            {
                foreach (ViewNode cNode in pNode.Nodes)
                {
                    SetPageNodeNewSize(cNode, size);
                }
            }
        }

        #region 更新程序
        private bool CheckUpdate()
        {
            Attribute guid_attr = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute));
            string guid = ((GuidAttribute)guid_attr).Value;

            string version = Application.ProductVersion;
            string[] str = version.Split('.');
            if (str.Length > 3)
            {
                version = str[0] + "." + str[1] + "." + str[2];
            }

<<<<<<< HEAD
        private void SetSelectedNode(ViewNode node)
        {
            this.curSelectedNode = node;
            this.ucdo.SetSelectedNode(node);
            this.ucProperty.DisplayNode(node);
        }
=======
            CheckUpdate up = new CheckUpdate();
            var info = up.Check(Application.ProductName, guid, version);

            if (info.Update)
            {
                string file = Path.Combine(MyCache.DefaultDownloadFolder, info.Name);
                if (!File.Exists(file))
                {
                    tsslUpdate.Visible = true;
                    tsslUpdate.Text = UIResMang.GetString("Download") + info.Name;
>>>>>>> SationKNXUIEditor-Modify

                    tspbUpdate.Visible = true;
                    tspbUpdate.PerformStep();

                    up.DownloadProgressChanged += DownloadProgressChanged;
                    up.DownloadFileCompleted += DownloadFileCompleted;

                    up.Download(MyCache.DefaultDownloadFolder, info);
                }
                else
                {
                    UpgradeApplication(info);
                }
            }
            else
            {
            }

            return info.Update;
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke(
                       new MethodInvoker(
                           delegate
                           {
                               tspbUpdate.Value = e.ProgressPercentage;
                           }));
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke(
                        new MethodInvoker(
                            delegate
                            {
                                tsslUpdate.Visible = false;
                                tspbUpdate.Visible = false;

                                UpgradeApplication(e.UserState as UpdateInfo);
                            }));
        }

        private void UpgradeApplication(UpdateInfo info)
        {
            try
            {
                tsmiCheckUpdate.Enabled = true;

                DialogResult result = MessageBox.Show(UIResMang.GetString("FoundNewVersion") + info.Version + "\r\n" + info.Tips + "\r\n" + UIResMang.GetString("UpgradeTips"),
                                UIResMang.GetString("Upgrade"),
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
                if (DialogResult.OK == result)
                {
                    Process.Start(Path.Combine(MyCache.DefaultDownloadFolder, info.Name));
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
<<<<<<< HEAD

        #region 下拉菜单点击事件
        private void tsmiNew_Click(object sender, EventArgs e)
        {
            NewKnxUiProject();
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            OpenKnxUiPrject();
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            SetToolStripButtonStatus(false);
            SetToolStripButtonKNXAddrStatus(false);
            SetToolStripButtonSaveStatus(false);
            ResetParameter();
            this.ucdo.RemoveAllAppNode();
            CloseAllTabPages();
            this.ucProperty.NotDisplay();
            tsslblProjectName.Text = "";
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            SaveKnxUiProject(ProjectFile);
        }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = KnxFilter;
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        VersionStorage.Save(); // 保存项目文件的版本信息。
                        this.ucdo.SaveNode(); // 保存界面到JSON文件
                        GroupAddressStorage.Save(); // 保存组地址到JSON文件

                        ZipProject(saveFileDialog.FileName);

                        ProjectSaved();
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
                }
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmi_en_US_Click(object sender, EventArgs e)
        {
            AppConfigHelper.UpdateAppConfig(MyConst.XmlTagAppLanguange, "en-US");

            Application.Restart();
        }

        private void tsmi_zh_CN_Click(object sender, EventArgs e)
        {
            AppConfigHelper.UpdateAppConfig(MyConst.XmlTagAppLanguange, "zh-CN");

            Application.Restart();
        }

        private void tsmiOpenHelp_Click(object sender, EventArgs e)
        {
            new FrmHelp().ShowDialog(this);
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            new FrmAboutBox().ShowDialog(this);
        }
=======
        #endregion
>>>>>>> SationKNXUIEditor-Modify
        #endregion
    }
}