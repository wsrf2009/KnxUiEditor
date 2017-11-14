using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.Entity;
using System.Collections;
using UIEditor.CommandManager;
using UIEditor.CommandManager.CommandTreeNode;
using System.Threading;

namespace UIEditor.UserUIControl
{
    public partial class UCProjectManager : UserControl
    {
        #region 变量定义
        private ViewNode CurSelectedNode { get; set; }
        private ViewNode cacheNode { get; set; }
        private SynchronizationContext mSyncContext { get; set; }
        #endregion

        #region 事件通知定义
        public delegate void AddNodeEventDelegate(object sender, EventArgs e);
        public event AddNodeEventDelegate AddNode;

        public delegate void RemoveNodeEventDelegate(object sender, EventArgs e);
        public event RemoveNodeEventDelegate RemoveNode;

        public delegate void TreeViewChangedEventDelegate(object sender, EventArgs e);
        public event TreeViewChangedEventDelegate TreeViewChangedEvent;

        public delegate void DisplayNodePropertyEventDelegate(object sender, EventArgs e);
        public event DisplayNodePropertyEventDelegate DisplayNodeProperty;

        public delegate void DisplayPageEventDelegate(object sender, EventArgs e);
        public event DisplayPageEventDelegate DisplayPage;

        public delegate void NodeDoubleClickEventDelegate(object sender, EventArgs e);
        public event NodeDoubleClickEventDelegate NodeDoubleClickEvent;

        //public delegate void NodeLabelEditEventDelegate(object sender, EventArgs e);
        //public event NodeLabelEditEventDelegate NodeLabelEditEvent;
        #endregion

        #region 构造函数
        public UCProjectManager()
        {
            InitializeComponent();

            ToolStripButtonInitialize(false);

            this.mSyncContext = SynchronizationContext.Current;
        }
        #endregion

        #region 事件通知
        private void AddNodeNotify(object sender, EventArgs e)
        {
            if (null != AddNode)
            {
                AddNode(sender, e);
            }
        }

        private void RemoveNodeNotify(object sender, EventArgs e)
        {
            if (null != RemoveNode)
            {
                RemoveNode(sender, e);
            }
        }

        private void TreeViewChangedEventNotify(object sender, EventArgs e)
        {
            if (null != this.TreeViewChangedEvent)
            {
                TreeViewChangedEvent(sender, e);
            }
        }

        private void NodeDoubleClickNotify(object sender, EventArgs e)
        {
            if (null != this.NodeDoubleClickEvent)
            {
                NodeDoubleClickEvent(sender, e);
            }
        }

        private void DisplayNodePropertyNotify(object sender, EventArgs e)
        {
            if (null != this.DisplayNodeProperty)
            {
                this.DisplayNodeProperty(sender, EventArgs.Empty);
            }
        }

        //private void NodeLabelEditNotify(object sender, EventArgs e)
        //{
        //    if (null != this.NodeLabelEditEvent)
        //    {
        //        this.NodeLabelEditEvent(sender, e);
        //    }
        //}
        #endregion

        #region 公共方法
        //public string Title
        //{
        //    get
        //    {
        //        return this.lblTitle.Text;
        //    }
        //    set
        //    {
        //        this.lblTitle.Text = value;
        //    }
        //}

        public void SetOutlineNode(ViewNode node)
        {
            this.tvProject.Nodes.Clear();

            if (null != node)
            {
                SetOutlineTitle(UIResMang.GetString("ProjectManager") + " - " + node.Text);
                //SetOutlineTitle(UIResMang.GetString("ProjectManager") + " - " + node.Title);

                this.tvProject.Nodes.Add(node);
                this.tvProject.ExpandAll();
            }
            else
            {
                SetOutlineTitle("");
                ToolStripButtonInitialize(false);
            }
        }

        public void RefreshProjectTree()
        {
            this.tvProject.Refresh();
        }

        public void SetSelectedNode(ViewNode node)
        {
            if (null != node)
            {
                if (MyConst.View.KnxPageType == node.Name)
                {
                    foreach (ViewNode pNode in this.tvProject.Nodes)
                    {
                        PageNode pageNode = GetPageNode(pNode, node.Id);
                        if (null != pageNode)
                        {
                            this.CurSelectedNode = pageNode;
                            this.tvProject.SelectedNode = this.CurSelectedNode;
                            break;
                        }
                    }
                }
            }
            else
            {
                this.tvProject.SelectedNode = null;
            }
        }

        public TreeView GetTreeView()
        {
            return this.tvProject;
        }

        //public void AddAppNode()
        //{
        //    this.tvProject.Nodes.Clear();

        //    AppNode appNode = new AppNode();
        //    this.cqdo.ExecuteCommand(new TreeNodeAdd(this.tvProject, null, appNode, -1));
        //    AreaNode areaNode = new AreaNode();
        //    this.cqdo.ExecuteCommand(new TreeNodeAdd(this.tvProject, appNode, areaNode, -1));
        //    RoomNode roomNode = new RoomNode();
        //    this.cqdo.ExecuteCommand(new TreeNodeAdd(this.tvProject, areaNode, roomNode, -1));
        //    PageNode pageNode = new PageNode();
        //    this.cqdo.ExecuteCommand(new TreeNodeAdd(this.tvProject, roomNode, pageNode, -1));

        //    TreeViewChangedEventNotify(pageNode, EventArgs.Empty);

        //    SetOutlineTitle(UIResMang.GetString("DocumentOutline") + " - " + appNode.Text);

        //    //SelectedNodeChangeNotify(pageNode, EventArgs.Empty);
        //}

        //public void RemoveAllAppNode()
        //{
        //    //this.tvProject.Nodes.Clear();
        //    foreach (TreeNode node in this.tvProject.Nodes)
        //    {
        //        this.cqdo.ExecuteCommand(new TreeNodeRemove(this.tvProject, node));
        //    }

        //    SetOutlineTitle("");
        //}
        #endregion

        #region 私有方法
        #region 设置工具条按钮状态
        private void SetEntityStatus(bool enable)
        {
            this.tsrBtnAddArea.Enabled = enable;
            this.tsrBtnAddRoom.Enabled = enable;
            this.tsrBtnAddPage.Enabled = enable;
        }

        private void ToolStripButtonInitialize(bool enable)
        {
            this.tsrBtnExpandAll.Enabled = enable;
            this.tsrBtnCollapseAll.Enabled = enable;
            this.tsrBtnMoveUp.Enabled = enable;
            this.tsrBtnMoveDown.Enabled = enable;
            SetEntityStatus(false);
        }

        private void SetToolStripButtonStatus(ViewNode node)
        {
            SetTSBExpandCollapseStatus(node);
            SetTSBMoveUpDown(node);
            SetEntityStatus(false);

            switch (node.Name)
            {
                case MyConst.View.KnxAppType:
                    this.tsrBtnAddArea.Enabled = true;
                    break;

                case MyConst.View.KnxAreaType:
                    this.tsrBtnAddRoom.Enabled = true;
                    break;

                case MyConst.View.KnxRoomType:
                    this.tsrBtnAddPage.Enabled = true;
                    break;

                default:
                    break;
            }
        }

        private void SetTSBExpandCollapseStatus(ViewNode node)
        {
            this.tsrBtnCollapseAll.Enabled = false;
            this.tsrBtnExpandAll.Enabled = false;

            if (node.Nodes.Count > 0)
            {
                if (node.IsExpanded)
                {
                    this.tsrBtnCollapseAll.Enabled = true;
                }
                else
                {
                    this.tsrBtnExpandAll.Enabled = true;
                }
            }
        }

        private void SetTSBMoveUpDown(ViewNode node)
        {
            this.tsrBtnMoveUp.Enabled = false;
            this.tsrBtnMoveDown.Enabled = false;

            if (null != node)
            {
                switch (node.Name)
                {
                    case MyConst.View.KnxAppType:
                        if ((null != node.PrevNode) && (MyConst.View.KnxAppType == node.PrevNode.Name))
                        {
                            this.tsrBtnMoveUp.Enabled = true;
                        }

                        if ((null != node.NextNode) && (MyConst.View.KnxAppType == node.NextNode.Name))
                        {
                            this.tsrBtnMoveDown.Enabled = true;
                        }
                        break;

                    case MyConst.View.KnxAreaType:
                        if ((null != node.PrevNode) && (MyConst.View.KnxAreaType == node.PrevNode.Name))
                        {
                            this.tsrBtnMoveUp.Enabled = true;
                        }

                        if ((null != node.NextNode) && (MyConst.View.KnxAreaType == node.NextNode.Name))
                        {
                            this.tsrBtnMoveDown.Enabled = true;
                        }
                        break;

                    case MyConst.View.KnxRoomType:
                        if ((null != node.PrevNode) && (MyConst.View.KnxRoomType == node.PrevNode.Name))
                        {
                            this.tsrBtnMoveUp.Enabled = true;
                        }

                        if ((null != node.NextNode) && (MyConst.View.KnxRoomType == node.NextNode.Name))
                        {
                            this.tsrBtnMoveDown.Enabled = true;
                        }
                        break;

                    case MyConst.View.KnxPageType:
                        if ((null != node.PrevNode) && (MyConst.View.KnxPageType == node.PrevNode.Name))
                        {
                            this.tsrBtnMoveUp.Enabled = true;
                        }

                        if ((null != node.NextNode) && (MyConst.View.KnxPageType == node.NextNode.Name))
                        {
                            this.tsrBtnMoveDown.Enabled = true;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion

        #region 添加树节点
        private void AddAreaNode(TreeNode appNode)
        {
            if (appNode != null && appNode.Name == MyConst.View.KnxAppType)
            {
                AreaNode node = new AreaNode();
                appNode.Nodes.Add(node);

                TreeViewChangedEventNotify(node, EventArgs.Empty);
            }
        }

        private void AddRoomNode(TreeNode areaNode)
        {
            if (areaNode != null && areaNode.Name == MyConst.View.KnxAreaType)
            {
                RoomNode node = new RoomNode();
                areaNode.Nodes.Add(node);

                TreeViewChangedEventNotify(node, EventArgs.Empty);
            }
        }

        private void AddPageNode(TreeNode roomNode)
        {
            if (roomNode != null && roomNode.Name == MyConst.View.KnxRoomType)
            {
                PageNode node = new PageNode();
                roomNode.Nodes.Add(node);

                node.CreateTwinsPageNode();

                TreeViewChangedEventNotify(node, EventArgs.Empty);
            }
        }
        #endregion

        #region 工具条事件
        private void tsrBtnExpandAll_Click(object sender, EventArgs e)
        {
            try
            {
                // 展开当前所选节点
                this.CurSelectedNode.Expand();

                SetTSBExpandCollapseStatus(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnCollapseAll_Click(object sender, EventArgs e)
        {
            try
            {
                // 折叠当前节点
                this.CurSelectedNode.Collapse();

                SetTSBExpandCollapseStatus(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                //节点上移一个
                //var selectedNode = this.tvProject.SelectedNode;
                NodeMoveUp(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                //var selectedNode = this.tvProject.SelectedNode;
                NodeMoveDown(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnAddArea_Click(object sender, EventArgs e)
        {
            try
            {
                AddAreaNode(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnAddRoom_Click(object sender, EventArgs e)
        {
            try
            {
                AddRoomNode(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsrBtnAddPage_Click(object sender, EventArgs e)
        {
            try
            {
                AddPageNode(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 移动节点
        /// <summary>
        /// 节点向下移动
        /// </summary>
        /// <param name="selectedNode"></param>
        private void NodeMoveDown(ViewNode selectedNode)
        {
            //向下移动一个树节点
            if ((null != selectedNode.NextNode) && (null != selectedNode.Parent))
            {
                var index = selectedNode.NextNode.Index;

                var pNode = selectedNode.Parent;
                selectedNode.Remove();
                pNode.Nodes.Insert(index, selectedNode);
                this.tvProject.SelectedNode = selectedNode;

                TreeViewChangedEventNotify(selectedNode, EventArgs.Empty);
            }

            SetTSBMoveUpDown(selectedNode);
        }

        /// <summary>
        /// 向上移动节点
        /// </summary>
        /// <param name="selectedNode"></param>
        private void NodeMoveUp(ViewNode selectedNode)
        {
            // 向上移动一个数节点
            if (selectedNode.PrevNode != null && selectedNode.Parent != null)
            {
                var index = selectedNode.PrevNode.Index;

                var pNode = selectedNode.Parent;
                selectedNode.Remove();
                pNode.Nodes.Insert(index, selectedNode);
                this.tvProject.SelectedNode = selectedNode;

                TreeViewChangedEventNotify(selectedNode, EventArgs.Empty);
            }

            SetTSBMoveUpDown(selectedNode);
        }
        #endregion

        #region 右键菜单
        private ToolStripMenuItem CreateAddAreaMenuItem()
        {
            ToolStripMenuItem tsmiAddAreaItem = new ToolStripMenuItem();
            tsmiAddAreaItem.Image = UIResMang.GetImage("Area_16x16");
            tsmiAddAreaItem.Name = "tsmiAddAreaItem";
            tsmiAddAreaItem.Size = new System.Drawing.Size(152, 22);
            tsmiAddAreaItem.Text = UIResMang.GetString("AddArea");
            tsmiAddAreaItem.Click += new System.EventHandler(AddAreaNode_Click);

            return tsmiAddAreaItem;
        }

        private ToolStripMenuItem CreateAddRoomMenuItem()
        {
            ToolStripMenuItem tsmiAddRoomItem = new ToolStripMenuItem();
            tsmiAddRoomItem.Image = UIResMang.GetImage("Room_16x16");
            tsmiAddRoomItem.Name = "tsmiAddRoomItem";
            tsmiAddRoomItem.Size = new System.Drawing.Size(152, 22);
            tsmiAddRoomItem.Text = UIResMang.GetString("AddRoom");
            tsmiAddRoomItem.Click += new System.EventHandler(AddRoomNode_Click);

            return tsmiAddRoomItem;
        }

        private ToolStripMenuItem CreateAddPageMenuItem()
        {
            ToolStripMenuItem tsmiAddPageItem = new ToolStripMenuItem();
            tsmiAddPageItem.Image = UIResMang.GetImage("Page_16x16");
            tsmiAddPageItem.Name = "tsmiAddPageItem";
            tsmiAddPageItem.Size = new System.Drawing.Size(152, 22);
            tsmiAddPageItem.Text = UIResMang.GetString("AddPage");
            tsmiAddPageItem.Click += new System.EventHandler(AddPageNode_Click);

            return tsmiAddPageItem;
        }

        /// <summary>
        /// 创建右键复制选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreateCopyMenuItem()
        {
            ToolStripMenuItem tsmiCopyNode = new ToolStripMenuItem();
            tsmiCopyNode.Image = UIResMang.GetImage("Copy_16x16");
            tsmiCopyNode.Name = "tsmiCopyNode";
            tsmiCopyNode.Size = new System.Drawing.Size(152, 22);
            tsmiCopyNode.Text = UIResMang.GetString("Copy");
            tsmiCopyNode.Click += CopyNode_Click;

            return tsmiCopyNode;
        }

        /// <summary>
        /// 创建右键剪切选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreateCutMenuItem()
        {
            ToolStripMenuItem tsmiCutNode = new ToolStripMenuItem();
            tsmiCutNode.Image = UIResMang.GetImage("Cut_16x16");
            tsmiCutNode.Name = "tsmiCutNode";
            tsmiCutNode.Size = new System.Drawing.Size(152, 22);
            tsmiCutNode.Text = UIResMang.GetString("Cut");
            tsmiCutNode.Click += CutNode_Click;

            return tsmiCutNode;
        }

        /// <summary>
        /// 创建右键粘贴选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreatePasteMenuItem()
        {
            ToolStripMenuItem tsmiPasteNode = new ToolStripMenuItem();
            tsmiPasteNode.Image = UIResMang.GetImage("Paste_16x16");
            tsmiPasteNode.Name = "tsmiPasteNode";
            tsmiPasteNode.Size = new System.Drawing.Size(152, 22);
            tsmiPasteNode.Text = UIResMang.GetString("Paste");
            tsmiPasteNode.Click += PasteNode_Click;

            return tsmiPasteNode;
        }

        /// <summary>
        /// 创建右键删除选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreateDeleteMenuItem()
        {
            ToolStripMenuItem tsmiDeleteItem = new ToolStripMenuItem();
            tsmiDeleteItem.Image = UIResMang.GetImage("Delete_Control_16x16");
            tsmiDeleteItem.Name = "tsmiDeleteItem";
            tsmiDeleteItem.Size = new System.Drawing.Size(100, 22);
            tsmiDeleteItem.Text = UIResMang.GetString("Delete");
            tsmiDeleteItem.Click += this.DeleteSelectedNode_Click;

            return tsmiDeleteItem;
        }

        private ToolStripMenuItem CreateInsertMenuItem()
        {
            ToolStripMenuItem tsmiInsertItem = new ToolStripMenuItem();
            //tsmiInsertItem.Image = ResourceMng.GetImage("Delete_Control_16x16");
            tsmiInsertItem.Name = "tsmiInsertItem";
            tsmiInsertItem.Size = new System.Drawing.Size(100, 22);
            tsmiInsertItem.Text = UIResMang.GetString("Insert");
            tsmiInsertItem.Click += this.InsertAtSelectedNode;

            return tsmiInsertItem;
        }

        private ToolStripMenuItem CreatePropertyMenuItem()
        {
            ToolStripMenuItem tsmiPropertyItem = new ToolStripMenuItem();
            tsmiPropertyItem.Name = "tsmiPropertyItem";
            tsmiPropertyItem.Size = new System.Drawing.Size(100, 22);
            tsmiPropertyItem.Text = UIResMang.GetString("Property");
            tsmiPropertyItem.Click += this.DisplaySelectedNodeProperty;

            return tsmiPropertyItem;
        }

        private ToolStripMenuItem CreateDisplayMenuItem()
        {
            ToolStripMenuItem tsmiDisplayItem = new ToolStripMenuItem();
            tsmiDisplayItem.Name = "tsmiDisplayItem";
            tsmiDisplayItem.Size = new Size(100, 22);
            tsmiDisplayItem.Text = UIResMang.GetString("Display");
            tsmiDisplayItem.Click += this.DisplaySelectedNode;

            return tsmiDisplayItem;
        }

        private ToolStripMenuItem CreateChangeNameMenuItem()
        {
            ToolStripMenuItem tsmiChangeNameItem = new ToolStripMenuItem();
            tsmiChangeNameItem.Name = "tsmiChangeNameItem";
            tsmiChangeNameItem.Size = new Size(100, 22);
            tsmiChangeNameItem.Text = UIResMang.GetString("ChangeName");
            tsmiChangeNameItem.Click += this.ChangeNodeName;

            return tsmiChangeNameItem;
        }
        #endregion

        #region 节点复制
        private void SaveCacheNode(ViewNode node)
        {
            this.cacheNode = node;
        }

        private ViewNode GetCacheNode()
        {
            ViewNode node = this.cacheNode;

            return node;
        }
        #endregion

        #region 右键菜单点击事件
        private void AddAreaNode_Click(object sender, EventArgs e)
        {
            try
            {
                AddAreaNode(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddRoomNode_Click(object sender, EventArgs e)
        {
            try
            {
                AddRoomNode(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddPageNode_Click(object sender, EventArgs e)
        {
            try
            {
                AddPageNode(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 右键复制节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyNode_Click(object sender, EventArgs e)
        {
            try
            {
                ViewNode node = this.tvProject.SelectedNode as ViewNode;
                var nodeCopy = node.Copy() as ViewNode;
                CopyNode(nodeCopy);
                SaveCacheNode(nodeCopy);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 右键剪切节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutNode_Click(object sender, EventArgs e)
        {
            try
            {
                var SelectedNode = this.tvProject.SelectedNode;
                SaveCacheNode(SelectedNode as ViewNode);
                RemoveSelectedNode(SelectedNode);
                RemoveNodeNotify(SelectedNode, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 右键粘贴节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteNode_Click(object sender, EventArgs e)
        {
            try
            {
                var SelectedNode = this.tvProject.SelectedNode;
                if (null == SelectedNode)
                {
                    return;
                }
                else
                {
                    ViewNode node = GetCacheNode();
                    if (null != node)
                    {
                        bool AddFlag = true;
                        switch (this.CurSelectedNode.Name)
                        {
                            case MyConst.View.KnxAppType:
                                if (MyConst.View.KnxAreaType != node.Name)
                                {
                                    AddFlag = false;
                                }
                                break;

                            case MyConst.View.KnxAreaType:
                                if (MyConst.View.KnxRoomType != node.Name)
                                {
                                    AddFlag = false;
                                }
                                break;

                            case MyConst.View.KnxRoomType:
                                if (MyConst.View.KnxPageType != node.Name)
                                {
                                    AddFlag = false;
                                }
                                break;

                            case MyConst.View.KnxPageType:
                            case MyConst.Controls.KnxGroupBoxType:
                                if ((MyConst.Controls.KnxGroupBoxType != node.Name) && (MyConst.Controls.KnxBlindsType != node.Name) &&
                                    (MyConst.Controls.KnxDigitalAdjustmentType != node.Name) && (MyConst.Controls.KnxLabelType != node.Name) &&
                                    (MyConst.Controls.KnxSceneButtonType != node.Name) && (MyConst.Controls.KnxSliderSwitchType != node.Name) &&
                                    (MyConst.Controls.KnxSwitchType != node.Name) && (MyConst.Controls.KnxTimerButtonType != node.Name) &&
                                    (MyConst.Controls.KnxValueDisplayType != node.Name))
                                {
                                    AddFlag = false;
                                }
                                break;

                            case MyConst.Controls.KnxBlindsType:
                            case MyConst.Controls.KnxDigitalAdjustmentType:
                            case MyConst.Controls.KnxLabelType:
                            case MyConst.Controls.KnxSceneButtonType:
                            case MyConst.Controls.KnxSliderSwitchType:
                            case MyConst.Controls.KnxSwitchType:
                            case MyConst.Controls.KnxTimerButtonType:
                            case MyConst.Controls.KnxValueDisplayType:
                                AddFlag = false;
                                break;

                            default:
                                break;
                        }

                        if (AddFlag)
                        {
                            SelectedNode.Nodes.Add(node);
                            SelectedNode.Expand();
                            this.tvProject.SelectedNode = node;
                            //this.cqdo.ExecuteCommand(new TreeNodeAdd(this.tvProject, SelectedNode, node, -1));
                            //AddNodeNotify(node, EventArgs.Empty);
                            TreeViewChangedEventNotify(node, EventArgs.Empty);
                        }
                        else
                        {
                            MessageBox.Show(string.Format(UIResMang.GetString("Message47"), node.Name,
                                        this.CurSelectedNode.Name), UIResMang.GetString("Message6"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    this.cacheNode = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 右键删除节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSelectedNode_Click(object sender, EventArgs e)
        {
            try
            {
                var SelectedNode = this.tvProject.SelectedNode;
                DeleteSelectedNode(SelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void InsertAtSelectedNode(object sender, EventArgs e)
        {
            try
            {
                var SelectedNode = this.tvProject.SelectedNode;
                int CurIndex = SelectedNode.Index;
                ViewNode node = GetCacheNode();
                if (null != node)
                {
                    bool InsertFlag = true;
                    switch (this.CurSelectedNode.Parent.Name)
                    {
                        case MyConst.View.KnxAppType:
                            if (MyConst.View.KnxAreaType != node.Name)
                            {
                                InsertFlag = false;
                            }
                            break;

                        case MyConst.View.KnxAreaType:
                            if (MyConst.View.KnxRoomType != node.Name)
                            {
                                InsertFlag = false;
                            }
                            break;

                        case MyConst.View.KnxRoomType:
                            if (MyConst.View.KnxPageType != node.Name)
                            {
                                InsertFlag = false;
                            }
                            break;

                        case MyConst.View.KnxPageType:
                        case MyConst.Controls.KnxGroupBoxType:
                            if ((MyConst.Controls.KnxGroupBoxType != node.Name) && (MyConst.Controls.KnxBlindsType != node.Name) &&
                                (MyConst.Controls.KnxDigitalAdjustmentType != node.Name) && (MyConst.Controls.KnxLabelType != node.Name) &&
                                (MyConst.Controls.KnxSceneButtonType != node.Name) && (MyConst.Controls.KnxSliderSwitchType != node.Name) &&
                                (MyConst.Controls.KnxSwitchType != node.Name) && (MyConst.Controls.KnxTimerButtonType != node.Name) &&
                                (MyConst.Controls.KnxValueDisplayType != node.Name))
                            {
                                InsertFlag = false;
                            }
                            break;

                        case MyConst.Controls.KnxBlindsType:
                        case MyConst.Controls.KnxDigitalAdjustmentType:
                        case MyConst.Controls.KnxLabelType:
                        case MyConst.Controls.KnxSceneButtonType:
                        case MyConst.Controls.KnxSliderSwitchType:
                        case MyConst.Controls.KnxSwitchType:
                        case MyConst.Controls.KnxTimerButtonType:
                        case MyConst.Controls.KnxValueDisplayType:
                            InsertFlag = false;
                            break;

                        default:
                            break;
                    }

                    if (InsertFlag)
                    {
                        SelectedNode.Parent.Nodes.Insert(CurIndex, node);
                        SelectedNode.Parent.Expand();
                        this.tvProject.SelectedNode = node;
                        //this.cqdo.ExecuteCommand(new TreeNodeAdd(this.tvProject, SelectedNode.Parent, node, CurIndex));
                        //AddNodeNotify(node, EventArgs.Empty);
                        TreeViewChangedEventNotify(node, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show(string.Format(UIResMang.GetString("Message47"), node.Name,
                                    this.CurSelectedNode.Parent.Name), UIResMang.GetString("Message6"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                this.cacheNode = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DisplaySelectedNodeProperty(object sender, EventArgs e)
        {
            try
            {
                DisplayNodePropertyNotify(this.CurSelectedNode, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DisplaySelectedNode(object sender, EventArgs e)
        {
            try
            {
                if (null != this.DisplayPage)
                {
                    this.DisplayPage(this.CurSelectedNode, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ChangeNodeName(object sender, EventArgs e)
        {
            try
            {
                ChangeName();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region TreeView事件
        private void tvProject_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                var SelectedNode = this.tvProject.GetNodeAt(e.X, e.Y);
                if (null != SelectedNode)
                {
                    SelectedNodeChanged(SelectedNode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tvProject_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var SNode = this.tvProject.GetNodeAt(e.X, e.Y);
                if (null != SNode)
                {
                    NodeDoubleClickNotify(SNode, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tvProject_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((Keys.Delete == e.KeyCode) || (Keys.Decimal == e.KeyCode)) // 删除Node
                {
                    //DeleteSelectedNode(this.CurSelectedNode);
                }
                else if (Keys.F2 == e.KeyCode) // 重命名
                {
                    if (null != this.CurSelectedNode)
                    {
                        //ChangeName();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //private void tvProject_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        //{
        //    try
        //    {
        //        TreeView tv = sender as TreeView;
        //        TreeNode tn = e.Node;
        //        if ("" != e.Label.Trim())
        //        {
        //            tn.Text = e.Label;
        //            tv.SelectedNode.EndEdit(false);
        //            tv.LabelEdit = false;

        //            if (MyConst.View.KnxPageType == tn.Name)
        //            {
        //                PageNode pNode = tn.Tag as PageNode;
        //                pNode.Text = e.Label;
        //                //pNode.Title = e.Label;
        //            }

        //            NodeLabelEditNotify(tn, EventArgs.Empty);
        //            TreeViewChangedEventNotify(tn, EventArgs.Empty);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        #endregion

        #region 私有方法
        public ToolStripItem[] GetEditContextMenuStrip(ViewNode node)
        {
            ToolStripMenuItem itemCopy = CreateCopyMenuItem();
            ToolStripMenuItem itemCut = CreateCutMenuItem();
            ToolStripMenuItem itemPaste = CreatePasteMenuItem();
            ToolStripMenuItem itemInsert = CreateInsertMenuItem();
            ToolStripMenuItem itemDelete = CreateDeleteMenuItem();
            if (null != node)
            {
                if (MyConst.View.KnxAppType == this.CurSelectedNode.Name)
                {
                    itemCopy.Enabled = false;
                    itemCut.Enabled = false;
                    itemInsert.Enabled = false;
                    itemDelete.Enabled = false;
                }
                else if (MyConst.View.KnxAreaType == this.CurSelectedNode.Name)
                {

                }

                if (null == GetCacheNode())
                {
                    itemPaste.Enabled = false;
                }
            }
            else
            {
                itemCopy.Enabled = false;
                itemCut.Enabled = false;
                itemPaste.Enabled = false;
                itemDelete.Enabled = false;
            }

            List<ToolStripItem> list = new List<ToolStripItem>();
            list.Add(itemCopy);
            list.Add(itemCut);
            list.Add(itemPaste);
            list.Add(itemInsert);
            list.Add(itemDelete);

            return list.ToArray();
        }

        private void SelectedNodeChanged(TreeNode newNode)
        {
            if (null == newNode)
            {
                return;
            }

            var node = newNode as ViewNode;
            if (null != node)
            {
                //if (null != this.CurSelectedNode)
                //{
                //    this.CurSelectedNode.Deselected();
                //    this.tvProject.Refresh();
                //}
                //node.Selected();
                this.CurSelectedNode = node;

                //this.tvProject.SelectedNode = this.CurSelectedNode;

                SetToolStripButtonStatus(node);

                ContextMenuStrip cms = new ContextMenuStrip();
                switch (this.CurSelectedNode.Name)
                {
                    case MyConst.View.KnxAppType:
                        cms.Items.Add(CreateAddAreaMenuItem());
                        cms.Items.Add(new ToolStripSeparator());
                        cms.Items.AddRange(GetEditContextMenuStrip(node));
                        cms.Items.Add(new ToolStripSeparator());
                        //cms.Items.Add(CreateChangeNameMenuItem());
                        //cms.Items.Add(new ToolStripSeparator());
                        cms.Items.Add(CreatePropertyMenuItem());
                        this.CurSelectedNode.ContextMenuStrip = cms;
                        break;

                    case MyConst.View.KnxAreaType:
                        cms.Items.Add(CreateAddRoomMenuItem());
                        cms.Items.Add(new ToolStripSeparator());
                        cms.Items.AddRange(GetEditContextMenuStrip(node));
                        cms.Items.Add(new ToolStripSeparator());
                        //cms.Items.Add(CreateChangeNameMenuItem());
                        //cms.Items.Add(new ToolStripSeparator());
                        cms.Items.Add(CreatePropertyMenuItem());
                        this.CurSelectedNode.ContextMenuStrip = cms;
                        break;

                    case MyConst.View.KnxRoomType:
                        cms.Items.Add(CreateAddPageMenuItem());
                        cms.Items.Add(new ToolStripSeparator());
                        cms.Items.AddRange(GetEditContextMenuStrip(node));
                        cms.Items.Add(new ToolStripSeparator());
                        //cms.Items.Add(CreateChangeNameMenuItem());
                        //cms.Items.Add(new ToolStripSeparator());
                        cms.Items.Add(CreatePropertyMenuItem());
                        this.CurSelectedNode.ContextMenuStrip = cms;
                        break;

                    case MyConst.View.KnxPageType:
                        cms.Items.Add(CreateDisplayMenuItem());
                        cms.Items.Add(new ToolStripSeparator());
                        cms.Items.AddRange(GetEditContextMenuStrip(node));
                        //cms.Items.Add(new ToolStripSeparator());
                        ////cms.Items.Add(CreateChangeNameMenuItem());
                        ////cms.Items.Add(new ToolStripSeparator());
                        //cms.Items.Add(CreatePropertyMenuItem());
                        this.CurSelectedNode.ContextMenuStrip = cms;
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="SelectedNode"></param>
        private void DeleteSelectedNode(TreeNode SelectedNode)
        {
            if (SelectedNode != null)
            {
                if (DialogResult.OK == MessageBox.Show(string.Format(UIResMang.GetString("Message14"), SelectedNode.Text), UIResMang.GetString("Message4"), MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                {
                    RemoveSelectedNode(SelectedNode);
                    RemoveNodeNotify(SelectedNode, EventArgs.Empty);
                }
            }

            if (this.tvProject.Nodes.Count == 0)
            {
            }
        }

        private void RemoveSelectedNode(TreeNode node)
        {
            if (null != node)
            {
                node.Remove();
                //this.cqdo.ExecuteCommand(new TreeNodeRemove(this.tvProject, node));

                TreeViewChangedEventNotify(node, EventArgs.Empty);
            }
        }

        private void ExpandUntilKNXPageNode(ViewNode pNode)
        {
            switch (pNode.Name)
            {
                case MyConst.View.KnxAppType:
                case MyConst.View.KnxAreaType:
                case MyConst.View.KnxRoomType:
                    //case MyConst.View.KnxPageType:
                    pNode.Expand();
                    foreach (ViewNode cNode in pNode.Nodes)
                    {
                        ExpandUntilKNXPageNode(cNode);
                    }
                    break;

                default:
                    pNode.Collapse();
                    break;
            }
        }

        private PageNode GetPageNode(ViewNode pNode, int id)
        {
            switch (pNode.Name)
            {
                case MyConst.View.KnxAppType:
                case MyConst.View.KnxAreaType:
                case MyConst.View.KnxRoomType:
                    foreach (ViewNode cNode in pNode.Nodes)
                    {
                        PageNode node = GetPageNode(cNode, id);
                        if (null != node)
                        {
                            return node;
                        }
                    }
                    break;

                case MyConst.View.KnxPageType:
                    if (pNode.Id == id)
                    {
                        return pNode as PageNode;
                    }
                    break;
            }

            return null;
        }

        private void SetOutlineTitle(string title)
        {
            this.lblTitle.Text = title;
        }

        /// <summary>
        /// 判断pNode及其子孙node，如为PageNode则Clone
        /// </summary>
        /// <param name="pNode"></param>
        private void CopyNode(ViewNode pNode)
        {
            if (null != pNode)
            {
                switch (pNode.Name)
                {
                    case MyConst.View.KnxAppType:
                    case MyConst.View.KnxAreaType:
                    case MyConst.View.KnxRoomType:
                        foreach (ViewNode cNode in pNode.Nodes)
                        {
                            CopyNode(cNode);
                        }
                        break;
                    case MyConst.View.KnxPageType:
                        PageNode pageNode = pNode as PageNode;
                        if (null != pageNode)
                        {
                            //PageNode pageNodeClone = pNode.Tag as PageNode;
                            //if (null != pageNodeClone)
                            //{
                            //    PageNode pageNodeCopy = pageNodeClone.Copy() as PageNode;
                            //    pNode.Text = pageNodeCopy.Text;
                            //    //pNode.Title = pageNodeCopy.Title;
                            //    pNode.Tag = pageNodeCopy;
                            //    pageNodeCopy.Tag = pNode;
                            //}
                            pageNode.CopyPageNode();
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        private void ChangeName()
        {
            if (null != this.CurSelectedNode)
            {
                this.tvProject.LabelEdit = true;
                this.tvProject.SelectedNode.BeginEdit();
            }
        }
        #endregion
        #endregion
    }
}
