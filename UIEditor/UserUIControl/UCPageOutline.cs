using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIEditor.Entity;
using UIEditor.Component;
using UIEditor.CommandManager.CommandTreeNode;
using UIEditor.CommandManager;
using UIEditor.Entity.Control;

namespace UIEditor.UserUIControl
{
    public partial class UCPageOutline : UserControl
    {
        #region 变量定义
        private ViewNode CurSelectedNode { get; set; }
        private ViewNode cacheNode { get; set; }
        private CommandQuene cqpo { get; set; }
        //private bool Changed { get; set; }
        private PageNode CurPageNode { get; set; }
        #endregion

        #region 事件通知定义
        public delegate void AddNodeEventDelegate(object sender, NodeAddEventArgs e);
        public event AddNodeEventDelegate AddNode;

        public delegate void RemoveNodeEventDelegate(object sender, NodeRemoveEventArgs e);
        public event RemoveNodeEventDelegate RemoveNode;

        public delegate void TreeViewChangedEventDelegate(object sender, EventArgs e);
        public event TreeViewChangedEventDelegate TreeViewChangedEvent;

        public delegate void DisplayNodePropertyEventDelegate(object sender, EventArgs e);
        public event DisplayNodePropertyEventDelegate DisplayNodeProperty;

        public delegate void NodeClickEventDelegate(object sender, EventArgs e);
        public event NodeClickEventDelegate NodeClickEvent;
        #endregion

        #region 构造函数
        public UCPageOutline()
        {
            InitializeComponent();

            ToolStripButtonInitialize(false);
        }
        #endregion

        #region 公共方法
        public void SetPageNode(PageNode node)
        {
            this.tvPage.Nodes.Clear();
            this.CurPageNode = node;
            if (null != node)
            {
                this.tvPage.Nodes.Add(node);
                this.tvPage.ExpandAll();

                this.lblTitle.Text = UIResMang.GetString("PageManager") + " - " + node.Text;
                //this.lblTitle.Text = UIResMang.GetString("PageManager") + " - " + node.Title;
            }
            else
            {
                this.lblTitle.Text = "";
                ToolStripButtonInitialize(false);
            }
        }

        #region 命令队列 - 撤销、删除
        public void SetCommandQueue(CommandQuene cq)
        {
            this.cqpo = cq;
        }
        #endregion

        public void SetSelectedNode(ViewNode node)
        {
            if (null != node)
            {
                this.CurSelectedNode = node;
                this.tvPage.SelectedNode = this.CurSelectedNode;
            }
        }

        public void RefreshPageOutline()
        {
            this.tvPage.Refresh();
        }
        #endregion

        #region 私有方法
        #region 事件通知
        private void AddNodeNotify(object sender, NodeAddEventArgs e)
        {
            if (null != AddNode)
            {
                AddNode(sender, e);
            }
        }

        private void RemoveNodeNotify(object sender, NodeRemoveEventArgs e)
        {
            if (null != RemoveNode)
            {
                RemoveNode(sender, e);
            }
        }

        private void NodeClickNotity(object sender, EventArgs e)
        {
            if (null != this.NodeClickEvent)
            {
                NodeClickEvent(sender, e);
            }
        }

        private void TreeViewChangedEventNotify(object sender, EventArgs e)
        {
            //if (!this.Changed)
            //{
            //    this.Changed = true;
            if (null != this.TreeViewChangedEvent)
            {
                TreeViewChangedEvent(sender, e);
            }
            //}
        }

        private void DisplayNodePropertyNotify(object sender, EventArgs e)
        {
            if (null != this.DisplayNodeProperty)
            {
                this.DisplayNodeProperty(this.CurSelectedNode, EventArgs.Empty);
            }
        }
        #endregion

        #region 工具条按钮状态
        private void ToolStripButtonInitialize(bool enable)
        {
            InitialTSBExpandCollapseStatus(enable);

            InitialTSBMoveUpDownStatus(enable);

            InitialTSBControlStatus(enable);
        }

        private void InitialTSBExpandCollapseStatus(bool enable)
        {
            this.tsbExpand.Enabled = enable;
            this.tsbCollapse.Enabled = enable;
        }

        private void InitialTSBMoveUpDownStatus(bool enable)
        {
            this.tsbMoveUp.Enabled = enable;
            this.tsbMoveDown.Enabled = enable;
        }

        private void InitialTSBControlStatus(bool enable)
        {
            this.tsbAddGroupBox.Enabled = enable;
            this.tsbAddBlinds.Enabled = enable;
            this.tsbAddLabel.Enabled = enable;
            this.tsbAddSceneButton.Enabled = enable;
            this.tsbAddSliderSwitch.Enabled = enable;
            this.tsbAddSwitch.Enabled = enable;
            this.tsbAddValueDisplay.Enabled = enable;
            this.tsbAddTimer.Enabled = enable;
            this.tsbAddDigitalAdjustment.Enabled = enable;
        }

        #region 根据所选Node设置工具条状态
        private void SetToolStripButtonStatus(ViewNode node)
        {
            SetTSBExpandCollapseStatus(node);

            SetTSBMoveUpDownStatus(node);

            SetTSBControlStatus(node);
        }

        private void SetTSBExpandCollapseStatus(ViewNode node)
        {
            InitialTSBExpandCollapseStatus(false);

            if (node.Nodes.Count > 0)
            {
                if (node.IsExpanded)
                {
                    this.tsbCollapse.Enabled = true;
                }
                else
                {
                    this.tsbExpand.Enabled = true;
                }
            }
        }

        private void SetTSBMoveUpDownStatus(ViewNode node)
        {
            InitialTSBMoveUpDownStatus(false);

            if ((null != node) && (node.Name != MyConst.View.KnxPageType))
            {
                if ((null != node.PrevNode) && (MyConst.View.KnxPageType != node.PrevNode.Name))
                {
                    this.tsbMoveUp.Enabled = true;
                }

                if ((null != node.NextNode) && (MyConst.View.KnxPageType != node.NextNode.Name))
                {
                    this.tsbMoveDown.Enabled = true;
                }
            }
        }

        private void SetTSBControlStatus(ViewNode node)
        {
            if (null != node)
            {
                switch (node.Name)
                {
                    case MyConst.View.KnxPageType:
                    case MyConst.Controls.KnxGroupBoxType:
                        InitialTSBControlStatus(true);
                        break;

                    default:
                        InitialTSBControlStatus(false);
                        break;
                }
            }
        }
        #endregion
        #endregion

        #region 工具条按钮点击事件
        #region 点击展开、折叠事件
        private void tsbExpand_Click(object sender, EventArgs e)
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

        private void tsbCollapse_Click(object sender, EventArgs e)
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
        #endregion

        #region 点击上移、下移事件
        private void tsbMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                //节点上移一个
                //var selectedNode = this.tvPage.SelectedNode as ViewNode;
                NodeMoveUp(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                //var selectedNode = this.tvPage.SelectedNode;
                NodeMoveDown(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 点击控件事件
        private void tsbAddGroupBox_Click(object sender, EventArgs e)
        {
            try
            {
                AddNodeGroupBox(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAddBlinds_Click(object sender, EventArgs e)
        {
            try
            {
                AddNodeBlinds(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAddSceneButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddNodeSceneButton(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAddLabel_Click(object sender, EventArgs e)
        {
            try
            {
                AddNodeLabel(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAddSliderSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                AddNodeSliderSwitch(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAddSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                AddNodeSwitch(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAddValueDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                AddNodeValueDisplay(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAddTimer_Click(object sender, EventArgs e)
        {
            try
            {
                AddNodeTimer(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tsbAddDigitalAdjustment_Click(object sender, EventArgs e)
        {
            try
            {
                AddNodeDigitalAdjustment(this.CurSelectedNode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
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
                this.tvPage.SelectedNode = selectedNode;

                //this.cqpo.ExecuteCommand(new TreeNodeMoveDown(this.tvPage, selectedNode, index));

                TreeViewChangedEventNotify(selectedNode, EventArgs.Empty);
            }

            SetTSBMoveUpDownStatus(selectedNode);
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
                this.tvPage.SelectedNode = selectedNode;

                //this.cqpo.ExecuteCommand(new TreeNodeMoveUp(this.tvPage, selectedNode, index));

                TreeViewChangedEventNotify(selectedNode, EventArgs.Empty);
            }

            SetTSBMoveUpDownStatus(selectedNode);
        }
        #endregion

        #region 节点复制缓存
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

        #region 右键菜单选项点击事件
        /// <summary>
        /// 右键复制节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != this.CurSelectedNode)
                {
                    var SelectedNode = this.CurSelectedNode.Copy() as ViewNode;
                    SelectedNode.Location = new Point(SelectedNode.Location.X + 10, SelectedNode.Location.Y + 10);
                    SaveCacheNode(SelectedNode);
                }
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
                if (null != this.CurSelectedNode)
                {
                    SaveCacheNode(this.CurSelectedNode);
                    RemoveSelectedNode(this.CurSelectedNode);

                    NodeRemoveEventArgs arg = new NodeRemoveEventArgs();
                    arg.mPageNode = this.CurPageNode;
                    arg.mParentNode = this.CurSelectedNode.Parent as ViewNode;
                    arg.mNode = this.CurSelectedNode;
                    RemoveNodeNotify(this.CurSelectedNode, arg);
                }
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
                if (null != this.CurSelectedNode)
                {
                    ViewNode node = GetCacheNode();
                    if (null != node)
                    {
                        bool AddFlag = true;
                        if ((MyConst.View.KnxPageType == this.CurSelectedNode.Name) || (MyConst.Controls.KnxGroupBoxType == this.CurSelectedNode.Name))
                        {
                            if (!EntityHelper.IsControlNode(this.CurSelectedNode.Name))
                            {
                                AddFlag = false;
                            }
                        }
                        else if (EntityHelper.IsControlNodeAndNotChildNode(this.CurSelectedNode.Name))
                        {
                            AddFlag = false;
                        }

                        if (AddFlag)
                        {
                            this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, this.CurSelectedNode, node, -1));

                            NodeAddEventArgs arg = new NodeAddEventArgs();
                            arg.mPageNode = this.CurPageNode;
                            arg.mParentNode = this.CurSelectedNode;
                            arg.mNode = node;
                            AddNodeNotify(node, arg);

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
                //ViewNode SelectedNode = this.tvPage.SelectedNode as ViewNode;
                if (null != this.CurSelectedNode)
                {
                    DeleteSelectedNode(this.CurSelectedNode);
                }
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
                if (null != this.CurSelectedNode)
                {
                    int CurIndex = this.CurSelectedNode.Index;
                    ViewNode node = GetCacheNode();
                    if (null != node)
                    {
                        bool InsertFlag = true;
                        if ((MyConst.View.KnxPageType == this.CurSelectedNode.Name) || (MyConst.Controls.KnxGroupBoxType == this.CurSelectedNode.Name))
                        {
                            if (!EntityHelper.IsControlNode(this.CurSelectedNode.Parent.Name))
                            {
                                InsertFlag = false;
                            }
                        }
                        else if (EntityHelper.IsControlNodeAndNotChildNode(this.CurSelectedNode.Parent.Name))
                        {
                            InsertFlag = false;
                        }

                        if (InsertFlag)
                        {
                            this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, this.CurSelectedNode.Parent, node, CurIndex));

                            NodeAddEventArgs arg = new NodeAddEventArgs();
                            arg.mPageNode = this.CurPageNode;
                            arg.mParentNode = this.CurSelectedNode.Parent as ViewNode;
                            arg.mNode = node;
                            AddNodeNotify(node, arg);

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
        #endregion

        #region 右键菜单选项
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
                    itemInsert.Enabled = false;
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

        /// <summary>
        /// 右键【属性】选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreatePropertyMenuItem()
        {
            ToolStripMenuItem tsmiPropertyItem = new ToolStripMenuItem();
            tsmiPropertyItem.Name = "tsmiPropertyItem";
            tsmiPropertyItem.Size = new System.Drawing.Size(100, 22);
            tsmiPropertyItem.Text = UIResMang.GetString("Property");
            tsmiPropertyItem.Click += this.DisplaySelectedNodeProperty;

            return tsmiPropertyItem;
        }
        #endregion

        #region 节点操作
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="SelectedNode"></param>
        private void DeleteSelectedNode(ViewNode SelectedNode)
        {
            if (SelectedNode != null)
            {
                //if (DialogResult.OK == MessageBox.Show(ResourceMng.GetString("Message14"), ResourceMng.GetString("Message15"), MessageBoxButtons.OKCancel,
                //    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                //{
                NodeRemoveEventArgs arg = new NodeRemoveEventArgs();
                arg.mPageNode = this.CurPageNode;
                arg.mParentNode = SelectedNode.Parent as ViewNode;
                arg.mNode = SelectedNode;

                RemoveSelectedNode(SelectedNode);


                RemoveNodeNotify(SelectedNode, arg);
                //}
            }
        }

        private void RemoveSelectedNode(TreeNode node)
        {
            if (null != node)
            {
                //node.Remove();
                this.cqpo.ExecuteCommand(new TreeNodeRemove(this.tvPage, node));

                TreeViewChangedEventNotify(node, EventArgs.Empty);
            }
        }

        #region 添加节点
        private void AddNodeGroupBox(ViewNode pNode)
        {
            if (null != pNode)
            {
                if ((MyConst.View.KnxPageType == pNode.Name) || (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    GroupBoxNode node = new GroupBoxNode();
                    //pNode.Nodes.Add(node);
                    this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, pNode, node, -1));

                    NodeAddEventArgs arg = new NodeAddEventArgs();
                    arg.mPageNode = this.CurPageNode;
                    arg.mParentNode = pNode;
                    arg.mNode = node;

                    AddNodeNotify(node, arg);
                }
            }
        }

        private void AddNodeBlinds(ViewNode pNode)
        {
            if (null != pNode)
            {
                if ((MyConst.View.KnxPageType == pNode.Name) || (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    BlindsNode node = new BlindsNode();
                    //pNode.Nodes.Add(node);
                    this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, pNode, node, -1));

                    NodeAddEventArgs arg = new NodeAddEventArgs();
                    arg.mPageNode = this.CurPageNode;
                    arg.mParentNode = pNode;
                    arg.mNode = node;
                    AddNodeNotify(node, arg);
                }
            }
        }

        private void AddNodeSceneButton(ViewNode pNode)
        {
            if (null != pNode)
            {
                if ((MyConst.View.KnxPageType == pNode.Name) || (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    SceneButtonNode node = new SceneButtonNode();
                    //pNode.Nodes.Add(node);
                    this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, pNode, node, -1));

                    NodeAddEventArgs arg = new NodeAddEventArgs();
                    arg.mPageNode = this.CurPageNode;
                    arg.mParentNode = pNode;
                    arg.mNode = node;
                    AddNodeNotify(node, arg);
                }
            }
        }

        private void AddNodeLabel(ViewNode pNode)
        {
            if (null != pNode)
            {
                if ((MyConst.View.KnxPageType == pNode.Name) || (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    LabelNode node = new LabelNode();
                    //pNode.Nodes.Add(node);
                    this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, pNode, node, -1));

                    NodeAddEventArgs arg = new NodeAddEventArgs();
                    arg.mPageNode = this.CurPageNode;
                    arg.mParentNode = pNode;
                    arg.mNode = node;
                    AddNodeNotify(node, arg);
                }
            }
        }

        private void AddNodeSliderSwitch(ViewNode pNode)
        {
            if (null != pNode)
            {
                if ((MyConst.View.KnxPageType == pNode.Name) || (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    SliderSwitchNode node = new SliderSwitchNode();
                    //pNode.Nodes.Add(node);
                    this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, pNode, node, -1));

                    NodeAddEventArgs arg = new NodeAddEventArgs();
                    arg.mPageNode = this.CurPageNode;
                    arg.mParentNode = pNode;
                    arg.mNode = node;
                    AddNodeNotify(node, arg);
                }
            }
        }

        private void AddNodeSwitch(ViewNode pNode)
        {
            if (null != pNode)
            {
                if ((MyConst.View.KnxPageType == pNode.Name) || (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    SwitchNode node = new SwitchNode();
                    //pNode.Nodes.Add(node);
                    this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, pNode, node, -1));

                    NodeAddEventArgs arg = new NodeAddEventArgs();
                    arg.mPageNode = this.CurPageNode;
                    arg.mParentNode = pNode;
                    arg.mNode = node;
                    AddNodeNotify(node, arg);
                }
            }
        }

        private void AddNodeValueDisplay(ViewNode pNode)
        {
            if (null != pNode)
            {
                if ((MyConst.View.KnxPageType == pNode.Name) || (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    ValueDisplayNode node = new ValueDisplayNode();
                    //pNode.Nodes.Add(node);
                    this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, pNode, node, -1));

                    NodeAddEventArgs arg = new NodeAddEventArgs();
                    arg.mPageNode = this.CurPageNode;
                    arg.mParentNode = pNode;
                    arg.mNode = node;
                    AddNodeNotify(node, arg);
                }
            }
        }

        private void AddNodeTimer(ViewNode pNode)
        {
            if (null != pNode)
            {
                if ((MyConst.View.KnxPageType == pNode.Name) || (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    TimerButtonNode node = new TimerButtonNode();
                    //pNode.Nodes.Add(node);
                    this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, pNode, node, -1));

                    NodeAddEventArgs arg = new NodeAddEventArgs();
                    arg.mPageNode = this.CurPageNode;
                    arg.mParentNode = pNode;
                    arg.mNode = node;
                    AddNodeNotify(node, arg);
                }
            }
        }

        private void AddNodeDigitalAdjustment(ViewNode pNode)
        {
            if (null != pNode)
            {
                if ((MyConst.View.KnxPageType == pNode.Name) || (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    DigitalAdjustmentNode node = new DigitalAdjustmentNode();
                    //pNode.Nodes.Add(node);
                    this.cqpo.ExecuteCommand(new TreeNodeAdd(this.tvPage, pNode, node, -1));

                    NodeAddEventArgs arg = new NodeAddEventArgs();
                    arg.mPageNode = this.CurPageNode;
                    arg.mParentNode = pNode;
                    arg.mNode = node;
                    AddNodeNotify(node, arg);
                }
            }
        }
        #endregion
        #endregion

        #region TreeView事件
        private void tvPage_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                var v = this.tvPage.GetNodeAt(e.X, e.Y);
                if (null != v)
                {
                    ViewNode vNode = v as ViewNode;

                    this.CurSelectedNode = vNode;

                    SetToolStripButtonStatus(vNode);

                    if (MouseButtons.Left == e.Button)
                    {
                        NodeClickNotity(vNode, EventArgs.Empty);
                    }
                    else if (MouseButtons.Right == e.Button)
                    {
                        ContextMenuStrip cms = new ContextMenuStrip();
                        this.CurSelectedNode.ContextMenuStrip = cms;
                        switch (this.CurSelectedNode.Name)
                        {
                            case MyConst.View.KnxPageType:
                                ToolStripMenuItem itemPaste = CreatePasteMenuItem();
                                if (null == GetCacheNode())
                                {
                                    itemPaste.Enabled = false;
                                }
                                cms.Items.Add(itemPaste);
                                cms.Items.Add(new ToolStripSeparator());
                                cms.Items.Add(CreatePropertyMenuItem());
                                break;

                            default:
                                cms.Items.AddRange(GetEditContextMenuStrip(vNode));
                                cms.Items.Add(new ToolStripSeparator());
                                cms.Items.Add(CreatePropertyMenuItem());
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #endregion
    }

    public class NodeRemoveEventArgs : EventArgs
    {
        public PageNode mPageNode { get; set; }
        public ViewNode mParentNode { get; set; }
        public ViewNode mNode { get; set; }
    }

    public class NodeAddEventArgs : EventArgs
    {
        public PageNode mPageNode { get; set; }
        public ViewNode mParentNode { get; set; }
        public ViewNode mNode { get; set; }
    }
}
