using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using UIEditor.CommandManager;
using UIEditor.CommandManager.CommandNodeProperty;
using UIEditor.CommandManager.CommandTreeNode;
using UIEditor.Component;
using UIEditor.Entity;
using UIEditor.Entity.Control;

namespace UIEditor.Drawing
{
    public class LayerControls : Panel
    {
        #region 常量
        private const int DIS_BETWEEN_PARENT_MAX = 25;
        private const int DIS_BETWEEN_PARENT_MID = 20;
        private const int DIS_BETWEEN_PARENT_MIN = 5;
        private const int DIS_BETWEEN_CONTROL_MAX = 25;
        private const int DIS_BETWEEN_CONTROL_MID = 20;
        private const int DIS_BETWEEN_CONTROL_MIN = 5;
        private const int SIDE_ALIGN_BETWEEN_CONTROL = 5;
        #endregion

        #region 变量定义
        /// <summary>
        /// 当前页面的 PageNode
        /// </summary>
        private PageNode mPageNode { get; set; }
        private List<ViewNode> SelectedNodes { get; set; }
        /// <summary>
        /// 当前选中的控件
        /// </summary>
        private ViewNode CurrentSelectedControl { get; set; }
        private List<ViewNode> ContainsPointNodes { get; set; }
        public static Type ToAddControl { get; set; }
        /// <summary>
        /// 前一个鼠标位置。未缩放的原始图
        /// </summary>
        private Point PrePoint { get; set; }
        /// <summary>
        /// 鼠标的实际位置，不考虑缩放因素
        /// </summary>
        private Point FactPoint { get; set; }
        private CommandQuene mCommandQueue { get; set; }
        private bool CtrlDown { get; set; }
        private bool ShiftDown { get; set; }
        private bool Changed { get; set; }
        private float Ratio { get; set; }
        private int DisBetweenParentMax { get; set; }
        private int DisBetweenParentMid { get; set; }
        private int DisBetweenParentMin { get; set; }
        private int DisBetweenControlMax { get; set; }
        private int DisBetweenControlMid { get; set; }
        private int DisBetweenControlMin { get; set; }
        private int SideAlignBetweenControl { get; set; }
        #region 框选控件
        private Point StartPoint { get; set; }
        /// <summary>
        /// 准备画虚线方框，用以框选控件
        /// </summary>
        private bool IsBoxControl { get; set; }

        private LayerLines mLayerLines;

        private List<ViewNode> ListCopyControls { get; set; }
        #endregion
        #endregion

        #region 通知定义
        public delegate void ControlSelectedEventDelegate(object sender, ControlSelectedEventArgs e);
        public event ControlSelectedEventDelegate ControlSelectedEvent;

        public delegate void PageChangedEventDelegate(object sender, EventArgs e);
        public event PageChangedEventDelegate PageChangedEvent;

        public delegate void SelectedControlsIsBrotherhoodEventDelegate(object sender, BrothershipEventArgs e);
        public event SelectedControlsIsBrotherhoodEventDelegate SelectedControlsIsBrotherhoodEvent;

        public delegate void SelectedControlsMoveEventDelegate(object sender, ControlSelectedEventArgs e);
        public event SelectedControlsMoveEventDelegate SelectedControlsMoveEvent;
        #endregion

        #region 构造函数
        public LayerControls(PageNode node, float ratio)
        {
            this.mPageNode = node;
            this.Ratio = ratio;

            this.BackColor = Color.Transparent;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.AllPaintingInWmPaint, true);

            this.mLayerLines = new LayerLines();
            this.mLayerLines.Visible = false;
            this.Controls.Add(this.mLayerLines);

            this.PagePropertyChanged(ratio);

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseUp);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDoubleClick);

            this.SelectedNodes = new List<ViewNode>();
            this.ContainsPointNodes = new List<ViewNode>();
            this.CtrlDown = false;
            this.ShiftDown = false;
            this.Changed = false;
            this.mCommandQueue = new CommandQuene();
            this.ListCopyControls = new List<ViewNode>();
        }
        #endregion

        #region override 方法
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (ViewNode node in this.mPageNode.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType != node.Name)
                {
                    node.DrawAt(g, this.Ratio, false);
                }
            }

            foreach (ViewNode node in this.mPageNode.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType == node.Name)
                {
                    node.DrawAt(g, this.Ratio, false);
                }
            }
        }
        #endregion

        #region 事件通知
        /// <summary>
        /// 控件选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSelectedEventNotify(ViewNode node)
        {
            if (null != ControlSelectedEvent)
            {
                ControlSelectedEventArgs arg = new ControlSelectedEventArgs();
                arg.mPageNode = this.mPageNode;
                arg.mNodes = this.SelectedNodes; //mPageNode;

                ControlSelectedEvent(node, arg);
            }

            if (null != SelectedControlsIsBrotherhoodEvent)
            {
                BrothershipEventArgs arg = new BrothershipEventArgs();
                if (this.SelectedNodes.Count < 2)
                {
                    arg.IsBrothership = false;
                }
                else
                {
                    arg.IsBrothership = SelectedControlsIsBrotherhood();
                }
                SelectedControlsIsBrotherhoodEvent(node, arg);
            }
        }

        /// <summary>
        /// 页面已发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PageChangedEventNotify(object sender, EventArgs e)
        {
            if (!this.Changed)
            {
                this.Changed = true;

                if (null != PageChangedEvent)
                {
                    PageChangedEvent(sender, e);
                }
            }
        }

        public void SelectedControlsMoveEventNotify(object sender, ControlSelectedEventArgs e)
        {
            if (null != this.SelectedControlsMoveEvent)
            {
                SelectedControlsMoveEvent(sender, e);
            }
        }
        #endregion

        #region STPage公共方法
        public CommandQuene GetCommandQueue()
        {
            return this.mCommandQueue;
        }

        public void SetSelectedControl(ViewNode node)
        {
            ClearSelectedNodes();

            if (MyConst.View.KnxPageType != node.Name)
            {
                AddSelectedNode(node);
            }
        }

        public void ControlPropertyChanged(ViewNode node)
        {
            RefreshThisPage();
        }

        public void AddControl(ViewNode node)
        {
            RefreshControl(node);
        }

        public void RemoveControl(ViewNode node)
        {
            RefreshThisPage();
        }

        public void PagePropertyChanged(float ratio)
        {
            this.Ratio = ratio;

            int pWidth = this.mPageNode.DrawWidth;
            int pHeight = this.mPageNode.DrawHeight;

            this.DisBetweenParentMax = ViewNode.ConvertToDraw(DIS_BETWEEN_PARENT_MAX, ratio);
            this.DisBetweenParentMid = ViewNode.ConvertToDraw(DIS_BETWEEN_PARENT_MID, ratio);
            this.DisBetweenParentMin = ViewNode.ConvertToDraw(DIS_BETWEEN_PARENT_MIN, ratio);
            this.DisBetweenControlMax = ViewNode.ConvertToDraw(DIS_BETWEEN_CONTROL_MAX, ratio);
            this.DisBetweenControlMid = ViewNode.ConvertToDraw(DIS_BETWEEN_CONTROL_MID, ratio);
            this.DisBetweenControlMin = ViewNode.ConvertToDraw(DIS_BETWEEN_CONTROL_MIN, ratio);
            this.SideAlignBetweenControl = ViewNode.ConvertToDraw(SIDE_ALIGN_BETWEEN_CONTROL, ratio);

            this.Size = this.MaximumSize = this.MinimumSize = new Size(pWidth, pHeight);
            this.mLayerLines.SetNewSize(pWidth, pHeight);

            RefreshThisPage();
        }

        public void LayerControlsKeyDowns(KeyEventArgs e)
        {
            if ((Keys.LControlKey == e.KeyCode) || (Keys.RControlKey == e.KeyCode))
            {
                this.CtrlDown = true;
            }
            else if ((Keys.LShiftKey == e.KeyCode) || (Keys.RShiftKey == e.KeyCode))
            {
                this.ShiftDown = true;
            }
        }

        public void LayerControlsKeyUps(KeyEventArgs e)
        {
            if ((Keys.LControlKey == e.KeyCode) || (Keys.RControlKey == e.KeyCode))
            {
                this.CtrlDown = false;
            }
            else if ((Keys.LShiftKey == e.KeyCode) || (Keys.RShiftKey == e.KeyCode))
            {
                this.ShiftDown = false;
            }
            else if (Keys.Delete == e.KeyCode)
            {
                foreach (ViewNode node in this.SelectedNodes)
                {
                    this.mCommandQueue.ExecuteCommand(new TreeNodeRemove(node.TreeView, node));

                    PageChangedEventNotify(node, EventArgs.Empty);
                }

                ClearSelectedNodes();

                RefreshThisPage();
            }
            else if (Keys.Up == e.KeyCode)
            {
                foreach (ViewNode node in this.SelectedNodes)
                {
                    //this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Y, node.Y-1));
                    this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Location.Y, node.Location.Y - 1));
                    PageChangedEventNotify(node, EventArgs.Empty);
                }

                RefreshThisPage();
            }
            else if (Keys.Down == e.KeyCode)
            {
                foreach (ViewNode node in this.SelectedNodes)
                {
                    //this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Y, node.Y + 1));
                    this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Location.Y, node.Location.Y + 1));
                    PageChangedEventNotify(node, EventArgs.Empty);
                }

                RefreshThisPage();
            }
            else if (Keys.Left == e.KeyCode)
            {
                foreach (ViewNode node in this.SelectedNodes)
                {
                    //this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.X, node.X - 1));
                    this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.Location.X, node.Location.X - 1));
                    PageChangedEventNotify(node, EventArgs.Empty);
                }

                RefreshThisPage();
            }
            else if (Keys.Right == e.KeyCode)
            {
                foreach (ViewNode node in this.SelectedNodes)
                {
                    //this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.X, node.X + 1));
                    this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.Location.X, node.Location.X + 1));
                    PageChangedEventNotify(node, EventArgs.Empty);
                }

                RefreshThisPage();
            }
        }

        public void Saved()
        {
            this.Changed = false;
        }

        #region 控件布局
        /// <summary>
        /// 左对齐
        /// </summary>
        public void AlignLeft()
        {
            if (this.SelectedNodes.Count < 2)
            {
                return;
            }

            //int left = this.SelectedNodes[0].X;
            int left = this.SelectedNodes[0].Location.X;
            foreach (ViewNode node in this.SelectedNodes)
            {
                //this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.X, left));
                this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.Location.X, left));
                PageChangedEventNotify(node, EventArgs.Empty);
            }

            RefreshThisPage();
        }

        /// <summary>
        /// 右对齐
        /// </summary>
        public void AlignRight()
        {
            if (this.SelectedNodes.Count < 2)
            {
                return;
            }

            //int right = this.SelectedNodes[0].X + this.SelectedNodes[0].Width;
            int right = this.SelectedNodes[0].Location.X + this.SelectedNodes[0].Size.Width;
            foreach (ViewNode node in this.SelectedNodes)
            {
                //this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.X, right - node.Width));
                this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.Location.X, right - node.Size.Width));
                PageChangedEventNotify(node, EventArgs.Empty);
            }

            RefreshThisPage();
        }

        /// <summary>
        /// 上对齐 
        /// </summary>
        public void AlignTop()
        {
            if (this.SelectedNodes.Count < 2)
            {
                return;
            }

            //int top = this.SelectedNodes[0].Y;
            int top = this.SelectedNodes[0].Location.Y;
            foreach (ViewNode node in this.SelectedNodes)
            {
                //this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Y, top));
                this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Location.Y, top));
                PageChangedEventNotify(node, EventArgs.Empty);
            }

            RefreshThisPage();
        }

        /// <summary>
        /// 下对齐
        /// </summary>
        public void AlignBottom()
        {
            if (this.SelectedNodes.Count < 2)
            {
                return;
            }

            //int bottom = this.SelectedNodes[0].Y + this.SelectedNodes[0].Height;
            int bottom = this.SelectedNodes[0].Location.Y + this.SelectedNodes[0].Size.Height;
            foreach (ViewNode node in this.SelectedNodes)
            {
                //this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Y, bottom - node.Height));
                this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Location.Y, bottom - node.Size.Height));
                PageChangedEventNotify(node, EventArgs.Empty);
            }

            RefreshThisPage();
        }

        /// <summary>
        /// 水平居中对齐
        /// </summary>
        public void AlignHorizontalCenter()
        {
            if (this.SelectedNodes.Count < 2)
            {
                return;
            }

            //int hCenter = this.SelectedNodes[0].Y + this.SelectedNodes[0].Height / 2;
            int hCenter = this.SelectedNodes[0].Location.Y + this.SelectedNodes[0].Size.Height / 2;
            foreach (ViewNode node in this.SelectedNodes)
            {
                //this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Y, hCenter - node.Height / 2));
                this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Location.Y, hCenter - node.Size.Height / 2));
                PageChangedEventNotify(node, EventArgs.Empty);
            }

            RefreshThisPage();
        }

        /// <summary>
        /// 垂直居中对齐
        /// </summary>
        public void AlignVerticalCenter()
        {
            if (this.SelectedNodes.Count < 2)
            {
                return;
            }

            //int vCenter = this.SelectedNodes[0].X + this.SelectedNodes[0].Width / 2;
            int vCenter = this.SelectedNodes[0].Location.X + this.SelectedNodes[0].Size.Width / 2;
            foreach (ViewNode node in this.SelectedNodes)
            {
                //this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.X, vCenter - node.Width / 2));
                this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.Location.X, vCenter - node.Size.Width / 2));
                PageChangedEventNotify(node, EventArgs.Empty);
            }

            RefreshThisPage();
        }

        /// <summary>
        /// 水平等间距
        /// </summary>
        public void HorizontalEquidistanceAlignment()
        {
            if (this.SelectedNodes.Count < 3)
            {
                return;
            }

            //var sortData = (from i in this.SelectedNodes orderby i.X ascending, i.X select i).ToList();
            var sortData = (from i in this.SelectedNodes orderby i.Location.X ascending, i.Location.X select i).ToList();
            int sum = 0;
            for (int i = 0; i < sortData.Count - 1; i++)
            {
                //sum += (sortData[i + 1].X - (sortData[i].X + sortData[i].Width));
                sum += (sortData[i + 1].Location.X - (sortData[i].Location.X + sortData[i].Size.Width));
            }

            int average = (sum / (sortData.Count - 1));

            for (int i = 1; i < sortData.Count - 1; i++)
            {
                //this.mCommandQueue.ExecuteCommand(new NodePropertyX(sortData[i], sortData[i].X, sortData[i - 1].X + sortData[i - 1].Width + average));
                this.mCommandQueue.ExecuteCommand(new NodePropertyX(sortData[i], sortData[i].Location.X, sortData[i - 1].Location.X + sortData[i - 1].Size.Width + average));
                PageChangedEventNotify(sortData[i], EventArgs.Empty);
            }

            RefreshThisPage();
        }

        /// <summary>
        /// 垂直等间距
        /// </summary>
        public void VerticalEquidistanceAlignment()
        {
            if (this.SelectedNodes.Count < 3)
            {
                return;
            }

            //var sortData = (from i in this.SelectedNodes orderby i.Y ascending, i.Y select i).ToList();
            var sortData = (from i in this.SelectedNodes orderby i.Location.Y ascending, i.Location.Y select i).ToList();
            int sum = 0;
            for (int i = 0; i < sortData.Count - 1; i++)
            {
                //sum += (sortData[i + 1].Y - (sortData[i].Y + sortData[i].Height));
                sum += (sortData[i + 1].Location.Y - (sortData[i].Location.Y + sortData[i].Size.Height));
            }

            int average = (sum / (sortData.Count - 1));

            for (int i = 1; i < sortData.Count - 1; i++)
            {
                //this.mCommandQueue.ExecuteCommand(new NodePropertyY(sortData[i], sortData[i].Y, sortData[i - 1].Y + sortData[i - 1].Height + average));
                this.mCommandQueue.ExecuteCommand(new NodePropertyY(sortData[i], sortData[i].Location.Y, sortData[i - 1].Location.Y + sortData[i - 1].Size.Height + average));
                PageChangedEventNotify(sortData[i], EventArgs.Empty);
            }

            RefreshThisPage();
        }

        /// <summary>
        /// 等宽
        /// </summary>
        public void WidthAlignment()
        {
            if (this.SelectedNodes.Count < 2)
            {
                return;
            }

            //int width = this.SelectedNodes[0].Width;
            int width = this.SelectedNodes[0].Size.Width;
            foreach (ViewNode node in this.SelectedNodes)
            {
                //this.mCommandQueue.ExecuteCommand(new NodePropertyWidth(node, node.Width, width));
                this.mCommandQueue.ExecuteCommand(new NodePropertyWidth(node, node.Size.Width, width));
                PageChangedEventNotify(node, EventArgs.Empty);
            }

            RefreshThisPage();
        }

        /// <summary>
        /// 等高
        /// </summary>
        public void HeightAlignment()
        {
            if (this.SelectedNodes.Count < 2)
            {
                return;
            }

            //int height = this.SelectedNodes[0].Height;
            int height = this.SelectedNodes[0].Size.Height;
            foreach (ViewNode node in this.SelectedNodes)
            {
                //this.mCommandQueue.ExecuteCommand(new NodePropertyHeight(node, node.Height, height));
                this.mCommandQueue.ExecuteCommand(new NodePropertyHeight(node, node.Size.Height, height));
                PageChangedEventNotify(node, EventArgs.Empty);
            }

            RefreshThisPage();
        }

        /// <summary>
        /// 水平居中
        /// </summary>
        public void CenterHorizontal()
        {
            Rectangle rect = ViewNode.GetMinimumCommonRectangleInParent(this.SelectedNodes);
            if (!rect.IsEmpty)
            {
                Rectangle parRect = (this.SelectedNodes[0].Parent as ViewNode).RectInPageFact;
                int ch = parRect.Width / 2;
                int nx = ch - rect.Width / 2;
                int offset = nx - rect.X;

                foreach (ViewNode node in this.SelectedNodes)
                {
                    this.mCommandQueue.ExecuteCommand(new NodePropertyX(node, node.Location.X, node.Location.X + offset));
                    PageChangedEventNotify(node, EventArgs.Empty);
                }
            }

            RefreshThisPage();

            ControlSelectedEventNotify(this.CurrentSelectedControl);
        }

        /// <summary>
        /// 垂直居中
        /// </summary>
        public void CenterVertical()
        {
            Rectangle rect = ViewNode.GetMinimumCommonRectangleInParent(this.SelectedNodes);
            if (!rect.IsEmpty)
            {
                Rectangle parRect = (this.SelectedNodes[0].Parent as ViewNode).RectInPageFact;
                int cv = parRect.Height / 2;
                int ny = cv - rect.Height / 2;
                int offset = ny - rect.Y;

                foreach (ViewNode node in this.SelectedNodes)
                {
                    this.mCommandQueue.ExecuteCommand(new NodePropertyY(node, node.Location.Y, node.Location.Y + offset));
                    PageChangedEventNotify(node, EventArgs.Empty);
                }
            }

            RefreshThisPage();

            ControlSelectedEventNotify(this.CurrentSelectedControl);
        }
        #endregion

        #region 控件操作 - 剪切、复制、粘贴
        public void CutControls()
        {
            this.ListCopyControls.Clear();

            foreach (ViewNode node in this.SelectedNodes)
            {
                this.ListCopyControls.Add(node.Copy() as ViewNode);

                this.mCommandQueue.ExecuteCommand(new TreeNodeRemove(this.mPageNode.TreeView, node));
            }

            RefreshThisPage();
        }

        public void CopyControls()
        {
            this.ListCopyControls.Clear();

            foreach (ViewNode node in this.SelectedNodes)
            {
                this.ListCopyControls.Add(node);
            }
        }

        public void PasteControls()
        {
            ClearSelectedNodes();

            foreach (ViewNode node in this.ListCopyControls)
            {
                ViewNode n = node.Copy() as ViewNode;
                this.mCommandQueue.ExecuteCommand(new TreeNodeAdd(this.mPageNode.TreeView, this.mPageNode, n, -1));

                AddSelectedNode(n);
            }

            CopyControls();
        }
        #endregion
        #endregion

        #region 私有方法
        #region 获取鼠标位置的顶层控件
        /// <summary>
        /// 获取该鼠标位置的所有控件，并存储在 ContainsPointNodes 中
        /// </summary>
        /// <param name="e"></param>
        private void GetContainsPointNodes(Point p)
        {
            this.ContainsPointNodes.Clear();

            foreach (ViewNode node in this.mPageNode.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType != node.Name) // 先判断非GroupBox控件
                {
                    node.ContainsPoint(p, this.ContainsPointNodes);
                }
            }

            foreach (ViewNode node in this.mPageNode.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType == node.Name) // GroupBox控件
                {
                    node.ContainsPoint(p, this.ContainsPointNodes);
                }
            }
        }

        /// <summary>
        /// 获取当前鼠标位置最顶层的控件
        /// </summary>
        /// <returns></returns>
        private ViewNode GetTopNode()
        {
            int count = this.ContainsPointNodes.Count;
            if (count > 0)
            {
                return this.ContainsPointNodes[count - 1];
            }
            else
            {
                return this.mPageNode;
            }
        }

        /// <summary>
        /// 获取当前鼠标位置最顶层的GroupBox
        /// </summary>
        /// <returns></returns>
        private GroupBoxNode GetTopGroupBoxNode()
        {
            int count = this.ContainsPointNodes.Count;
            if (count > 0)
            {
                for (int i = count - 1; i >= 0; i--)
                {
                    GroupBoxNode node = this.ContainsPointNodes[i] as GroupBoxNode;
                    if (null != node)
                    {
                        return node;
                    }
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取当前鼠标位置除了gbNode以外的最顶层的GroupBox
        /// </summary>
        /// <param name="gbNode"></param>
        /// <returns></returns>
        private GroupBoxNode GetTopGroupBoxNodeExceptNode(ViewNode gbNode)
        {
            int count = this.ContainsPointNodes.Count;
            if (count > 0)
            {
                /* 在Z向上从上至下搜寻GroupBox */
                bool start = false;
                for (int i = count - 1; i >= 0; i--)
                {
                    GroupBoxNode node = this.ContainsPointNodes[i] as GroupBoxNode;
                    if (null != node) // 确认node为GroupBox
                    {
                        if (node.Id == gbNode.Id)
                        {
                            start = true;
                            continue;
                        }
                        else if (start) // 从gbNode之下开始找GroupNode
                        {
                            return node;
                        }
                        else // node在gbNode之上
                        {
                            if (!ContainsChildNode(gbNode, node))
                            {
                                return node;
                            }
                        }

                    }

                }

                return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// parentNode的子孙控件中是否有childNode
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="childNode"></param>
        /// <returns></returns>
        private bool ContainsChildNode(ViewNode parentNode, ViewNode childNode)
        {
            bool contains = false;
            TreeNode ppNode = parentNode.Parent;
            TreeNode pcNode = childNode.Parent;
            while (true) // 遍历ChildNode的父控件、祖控件...
            {
                if (null != pcNode)
                {
                    if (pcNode == parentNode) // 子控件的父控件与parenNode一致，则包含
                    {
                        contains = true;
                        break;
                    }
                    else if (pcNode == ppNode) // 子控件的父控件与parentNode的父控件一致，则不包含
                    {
                        contains = false;
                        break;
                    }
                }
                else
                {
                    break;
                }

                pcNode = pcNode.Parent;

            }

            return contains;
        }
        #endregion

        private void InitBeforeMoveControl(ViewNode node)
        {
            node.CompensateReset();
            foreach (ViewNode n in node.Nodes)
            {
                InitBeforeMoveControl(n);
            }
        }

        private void SetGroupBoxInnerControlToStateMove(ViewNode gb)
        {
            foreach (ViewNode node in gb.Nodes)
            {
                node.State = UIEditor.Entity.ViewNode.ControlState.Move;
                SetGroupBoxInnerControlToStateMove(node);
            }
        }

        private void ResumeGroupBoxInnerControlToStateNormal(ViewNode gb)
        {
            foreach (ViewNode node in gb.Nodes)
            {
                node.State = UIEditor.Entity.ViewNode.ControlState.Normal;
                ResumeGroupBoxInnerControlToStateNormal(node);
            }
        }

        private Boolean SelectedControlsIsBrotherhood()
        {
            //if (this.SelectedNodes.Count < 2)
            //{
            //    return false;
            //}

            ViewNode pNode = this.SelectedNodes[0].Parent as ViewNode;
            if (null == pNode)
            {
                return false;
            }

            foreach (ViewNode node in this.SelectedNodes)
            {
                ViewNode n = node.Parent as ViewNode;
                if (null == n)
                {
                    return false;
                }

                if (n.Id != pNode.Id)
                {
                    return false;
                }
            }

            return true;
        }

        #region 框选控件
        /// <summary>
        /// 用鼠标拖出的矩形框框选控件
        /// </summary>
        /// <param name="node"></param>
        private void BoxControl(ViewNode node, Rectangle rect)
        {
            foreach (ViewNode cNode in node.Nodes)
            {
                if (rect.IntersectsWith(cNode.VisibleRectInPage))
                {
                    AddSelectedNode(cNode);
                }
            }
        }
        #endregion

        private void AddControlAtPoint(ViewNode node, Point p)
        {
            GroupBoxNode gbNode = GetTopGroupBoxNode();
            if (null != gbNode)
            {
                /* GroupBox中添加控件 */
                this.mCommandQueue.ExecuteCommand(new TreeNodeAdd(gbNode.TreeView, gbNode, node, -1));
                SetChildNodeX(node, p.X - gbNode.LocationInPage.X);
                SetChildNodeY(node, p.Y - gbNode.LocationInPage.Y);
            }
            else
            {
                /* Page中添加控件 */
                this.mCommandQueue.ExecuteCommand(new TreeNodeAdd(this.mPageNode.TreeView, this.mPageNode, node, -1));
                SetChildNodeX(node, p.X);
                SetChildNodeY(node, p.Y);
            }

            ControlSelectedEventNotify(node); // 选中控件 事件通知
            PageChangedEventNotify(node, EventArgs.Empty); // 页面更改 事件通知
        }

        #region Panel 鼠标按下后的任务
        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="node">要添加的控件</param>
        /// <param name="p">控件添加的位置</param>
        private void MouseDownWork_AddControl(ViewNode node, Point p)
        {
            AddControlAtPoint(node, p);

            ClearSelectedNodes();
            AddSelectedNode(node);

            this.CurrentSelectedControl = node;

            node.State = UIEditor.Entity.ViewNode.ControlState.Normal;

            //ControlSelectedEventArgs arg = new ControlSelectedEventArgs();
            //arg.mPageNode = this.mPageNode;
            //arg.mNodes = this.SelectedNodes; //node;
            //ControlSelectedEventNotify(node, arg); // 选中控件 事件通知
            //PageChangedEventNotify(node, EventArgs.Empty); // 页面更改 事件通知

            LayerControls.ToAddControl = null;
            SetCursor(null);
        }

        /// <summary>
        /// 是否点击了新的控件【已选控件列表】外的控件
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool MouseDownWork_IsClickOnNewControl(Point p)
        {
            bool ClickOnNewControl = true; // 鼠标选中的是否为除【已选控件列表】中控件外的新控件
            bool RemoveOthers = false;
            bool SetAllControlStateMove = false;
            bool RemoveCurrentNode = false;

            foreach (ViewNode node in this.SelectedNodes) // 遍历【已选中控件列表】
            {
                if (node.FrameBoundContainsPoint(p)) // 点击了已选控件的调节大小的小方框（8个中的一个）
                {
                    ClickOnNewControl = false;

                    node.PreBound = node.FactRect;
                    node.State = UIEditor.Entity.ViewNode.ControlState.ChangeSize;
                    node.PPoint = p;

                    this.CurrentSelectedControl = node;
                    RemoveOthers = true; // 同一时刻只能对一个控件调节大小

                    break;
                }
                else if (node.MobileRect.Contains(p)) // 点击了已选控件的可移动框（现仅用于GroupBox）
                {
                    ClickOnNewControl = false;

                    this.CurrentSelectedControl = node;
                    SetAllControlStateMove = true;

                    break;
                }
                else if (node.VisibleRectInPage.Contains(p)) // 点击了已选中的控件的本体
                {
                    ViewNode topestNode = GetTopNode();
                    if (node == topestNode)
                    {
                        ClickOnNewControl = false;

                        if (MyConst.Controls.KnxGroupBoxType != node.Name) // 选中的控件不是GroupBox
                        {
                            if (CtrlDown) // Ctrl键被按下
                            {
                                this.CurrentSelectedControl = node;
                                //RemoveCurrentNode = true;
                                this.CurrentSelectedControl.State = UIEditor.Entity.ViewNode.ControlState.SelectedAgain;
                            }
                            else
                            {
                                this.CurrentSelectedControl = node;
                                SetAllControlStateMove = true;
                            }
                        }
                        else if (MyConst.Controls.KnxGroupBoxType == node.Name) // 选中的控件是GroupBox
                        {
                            if (CtrlDown) // Ctrl键被按下
                            {
                                this.StartPoint = p;
                                this.IsBoxControl = true;
                                //this.ParentControl = topestNode;
                                this.CurrentSelectedControl = topestNode;
                                this.CurrentSelectedControl.State = UIEditor.Entity.ViewNode.ControlState.SelectedAgain;
                            }
                            else // Ctrl没被按下
                            {
                                // 啥也不做
                            }
                        }
                    }
                    else  // 点击的是内部的子控件，点击了新的控件
                    {
                    }

                    break;
                }
            }

            if (RemoveOthers)
            {
                RemoveOthersNode(this.CurrentSelectedControl);
            }
            else if (SetAllControlStateMove)
            {
                foreach (ViewNode n in this.SelectedNodes)
                {
                    n.State = UIEditor.Entity.ViewNode.ControlState.Selected;
                    n.PPoint = p;
                }
            }
            else if (RemoveCurrentNode)
            {
                RemoveSelectedNode(this.CurrentSelectedControl); // 将选中的控件从【已选中控件列表移除】
                this.CurrentSelectedControl = null;
            }

            return ClickOnNewControl;
        }

        /// <summary>
        /// 鼠标按下时的任务
        /// </summary>
        /// <param name="obj"></param>
        private void MouseLeftButtonDown_Work(Point p)
        {
            this.CurrentSelectedControl = null;
            GetContainsPointNodes(p);

            Type type = LayerControls.ToAddControl;
            if (null != type) // 添加控件
            {
                object objType = type.Assembly.CreateInstance(type.FullName);
                ViewNode node = objType as ViewNode; // 要添加的控件

                MouseDownWork_AddControl(node, p);
            }
            else // 点选控件
            {
                if (MouseDownWork_IsClickOnNewControl(p)) // 选中了新的控件
                {
                    if (!this.CtrlDown) // Ctrl键没有被按下
                    {
                        ClearSelectedNodes();
                    }

                    ViewNode topestNode = GetTopNode();
                    if (this.mPageNode != topestNode) // 选中的不是当前页面本身
                    {
                        /* 添加到选中的控件列表中 */
                        AddSelectedNode(topestNode);
                        topestNode.PPoint = p;
                        topestNode.State = UIEditor.Entity.ViewNode.ControlState.Selected;
                        this.CurrentSelectedControl = topestNode;

                        if (MyConst.Controls.KnxGroupBoxType == topestNode.Name)
                        {
                            this.StartPoint = p; // e.Location;
                            this.IsBoxControl = true;
                            //this.ParentControl = topestNode;
                            this.CurrentSelectedControl = topestNode;

                            topestNode.State = UIEditor.Entity.ViewNode.ControlState.Normal;
                        }
                    }
                    else // 根据鼠标轨迹绘制矩形框---多选
                    {
                        this.StartPoint = p; // e.Location;
                        this.IsBoxControl = true;
                        //this.ParentControl = this.node;
                        this.CurrentSelectedControl = this.mPageNode;
                    }
                }
            }
        }
        #endregion

        #region Panel 鼠标移动后的任务
        /// <summary>
        /// 复制控件，将选中的控件复制一份，并替换为选中的控件
        /// </summary>
        private void MouseMoveWork_CopyControl()
        {
            List<ViewNode> copyNodes = new List<ViewNode>(); // 复制的控件列表
            foreach (ViewNode node in this.SelectedNodes) // 遍历【已选控件列表】
            {
                node.State = UIEditor.Entity.ViewNode.ControlState.Normal;

                ViewNode copyNode = node.Copy() as ViewNode;
                this.mCommandQueue.ExecuteCommand(new TreeNodeAdd(node.TreeView, node.Parent, copyNode, node.Index + 1));
                copyNodes.Add(copyNode);

                if (this.CurrentSelectedControl == node)
                {
                    this.CurrentSelectedControl = copyNode;
                }
            }

            /* 替换为复制后的控件 */
            ClearSelectedNodes();
            foreach (ViewNode node in copyNodes)
            {
                node.State = UIEditor.Entity.ViewNode.ControlState.Move;
                //node.PreLocation = new Point(node.X, node.Y);
                node.PreLocation = node.Location;
                //node.PreLocation = node.DrawLocation;
                AddSelectedNode(node);
            }
        }

        /// <summary>
        /// 在父控件内移动或从父控件移出、移到新的父控件
        /// </summary>
        /// <param name="cNode"></param>
        /// <param name="e"></param>
        private void MouseMoveWork_MoveOutIn(ViewNode cNode, Point p)
        {
            GetContainsPointNodes(p);
            ViewNode pNode = cNode.Parent as ViewNode;

            if (pNode.VisibleRectInPage.Contains(p)) // 拖动子控件的鼠标位置还在子控件所属的父控件中
            {
                ViewNode topGroupBoxNode = null;
                if (MyConst.Controls.KnxGroupBoxType == cNode.Name) // 拖动的子控件为GroupBox类型
                {
                    topGroupBoxNode = GetTopGroupBoxNodeExceptNode(cNode);
                }
                else
                {
                    topGroupBoxNode = GetTopGroupBoxNode();
                }

                if (null != topGroupBoxNode && topGroupBoxNode != cNode && topGroupBoxNode != pNode)
                {
                    Point locationInPage = cNode.LocationInPage;
                    this.mCommandQueue.ExecuteCommand(new TreeNodeRemove(cNode.TreeView, cNode));

                    this.mCommandQueue.ExecuteCommand(new TreeNodeAdd(topGroupBoxNode.TreeView, topGroupBoxNode, cNode, -1));
                    Point loc = ViewNode.GetLocationInParent(topGroupBoxNode.LocationInPage, locationInPage);
                    //cNode.X = loc.X;
                    //cNode.Y = loc.Y;
                    SetChildNodeX(cNode, loc.X);
                    SetChildNodeY(cNode, loc.Y);
                }
            }
            else  // 鼠标已经不在原父控件中
            {
                ViewNode topGroupBoxNode = null;
                if (MyConst.Controls.KnxGroupBoxType == cNode.Name)
                {
                    topGroupBoxNode = GetTopGroupBoxNodeExceptNode(cNode);
                }
                else
                {
                    topGroupBoxNode = GetTopGroupBoxNode();
                }

                if (null != topGroupBoxNode)
                {
                    if (topGroupBoxNode != cNode)
                    {
                        Point locationInPage = cNode.LocationInPage;
                        this.mCommandQueue.ExecuteCommand(new TreeNodeRemove(cNode.TreeView, cNode));

                        this.mCommandQueue.ExecuteCommand(new TreeNodeAdd(topGroupBoxNode.TreeView, topGroupBoxNode, cNode, -1));
                        Point loc = ViewNode.GetLocationInParent(topGroupBoxNode.LocationInPage, locationInPage);
                        //cNode.X = loc.X;
                        //cNode.Y = loc.Y;
                        SetChildNodeX(cNode, loc.X);
                        SetChildNodeY(cNode, loc.Y);
                    }
                }
                else
                {
                    Point locationInPage = cNode.LocationInPage;
                    this.mCommandQueue.ExecuteCommand(new TreeNodeRemove(cNode.TreeView, cNode));

                    this.mCommandQueue.ExecuteCommand(new TreeNodeAdd(this.mPageNode.TreeView, this.mPageNode, cNode, -1));
                    //Point loc = ViewNode.GetLocationInParent(new Point(this.mPageNode.X, this.mPageNode.Y), locationInPage);
                    Point loc = ViewNode.GetLocationInParent(this.mPageNode.Location, locationInPage);
                    //cNode.X = loc.X;
                    //cNode.Y = loc.Y;
                    SetChildNodeX(cNode, loc.X);
                    SetChildNodeY(cNode, loc.Y);
                }
            }
        }

        /// <summary>
        /// 与父控件在水平方向进行距离检查
        /// </summary>
        /// <param name="nodeRect"></param>
        /// <param name="pNode"></param>
        /// <param name="Lines"></param>
        private void MouseMoveWork_CheckMarginHorizontal_BetweenParent(ViewNode node, ViewNode pNode, List<Line> lines)
        {
            Rectangle nodeRect = node.RectInPage;
            Rectangle pNodeRect = pNode.RectInPage;

            int disLeft = nodeRect.Left - pNodeRect.Left; // 与父视图左边距
            int compensatedLeft = disLeft + pNode.ParCompX;
            int disRight = nodeRect.Right - pNodeRect.Right; // 与父视图右边距
            int compensatedRight = disRight + pNode.ParCompX;
            if (compensatedLeft >= this.DisBetweenParentMid && compensatedLeft <= this.DisBetweenParentMax) // 与父视图左侧距离[mid, max]
            {
                if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                {
                    SetChildNodeX(node, node.GetLocationXFromLocationXInPage(pNodeRect.Left + this.DisBetweenParentMid));
                    pNode.ParCompX += disLeft - this.DisBetweenParentMid;
                }

                int y = nodeRect.Top + nodeRect.Height / 2;
                AddLine(lines, new Line(new Point(pNodeRect.Left, y), new Point(node.RectInPage.Left, y)));
            }
            else if (compensatedLeft <= this.DisBetweenParentMin) // 与父视图左侧距离在(-∞, min]之间时
            {
                if ((MyConst.View.KnxPageType == pNode.Name) ||  // 父视图为KNXPage
                    ((MyConst.Controls.KnxGroupBoxType == pNode.Name) && (compensatedLeft >= 0))) // 父视图为KNXGroupBox
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        SetChildNodeX(node, node.GetLocationXFromLocationXInPage(pNodeRect.Left));
                        pNode.ParCompX += disLeft;
                    }

                    AddLine(lines, new Line(pNodeRect.Location, new Point(pNodeRect.Left, pNodeRect.Bottom)));
                }
            }
            else if (compensatedRight >= -this.DisBetweenParentMax && compensatedRight <= -this.DisBetweenParentMid) // 与父视图右侧距离在[-max, -mid]之间时
            {
                if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                {
                    SetChildNodeX(node, node.GetLocationXFromLocationRightInPage(pNodeRect.Right - this.DisBetweenParentMid));
                    pNode.ParCompX += disRight + this.DisBetweenParentMid;
                }

                int y = nodeRect.Top + nodeRect.Height / 2;
                AddLine(lines, new Line(new Point(node.RectInPage.Right, y), new Point(pNodeRect.Right, y)));
            }
            else if (compensatedRight >= -this.DisBetweenParentMin) // 与父视图右侧距离在(-∞, -min]之间时
            {
                if ((MyConst.View.KnxPageType == pNode.Name) ||  // 父视图为KNXPage
                    ((MyConst.Controls.KnxGroupBoxType == pNode.Name) && (compensatedRight <= 0))) // 父视图为KNXGroupBox
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                    {
                        SetChildNodeX(node, node.GetLocationXFromLocationRightInPage(pNodeRect.Right));
                        pNode.ParCompX += disRight;
                    }

                    AddLine(lines, new Line(new Point(pNodeRect.Right, pNodeRect.Top), new Point(pNodeRect.Right, pNodeRect.Bottom)));
                }
            }
            else
            {
                SetChildNodeXOffset(node, pNode.ParCompX);
                pNode.ParCompX = 0;
            }
        }

        /// <summary>
        /// 与父控件在垂直方向进行距离检查
        /// </summary>
        /// <param name="nodeRect"></param>
        /// <param name="pNode"></param>
        /// <param name="Lines"></param>
        private void MouseMoveWork_CheckMarginVertical_BetweenParent(ViewNode node, ViewNode pNode, List<Line> lines)
        {
            Rectangle nodeRect = node.RectInPage;
            Rectangle pNodeRect = pNode.RectInPage;

            int disTop = nodeRect.Top - pNodeRect.Top; // 与父视图上边距
            int compensatedTop = disTop + pNode.ParCompY;
            int disBottom = nodeRect.Bottom - pNodeRect.Bottom; // 与父视图下边距
            int compensatedBottom = disBottom + pNode.ParCompY;
            if (compensatedTop >= this.DisBetweenParentMid && compensatedTop <= this.DisBetweenParentMid) // 与父视图顶部距离在[mid, max]之间时
            {
                if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                {
                    SetChildNodeY(node, node.GetLocationYFromLocationYInPage(pNodeRect.Top + this.DisBetweenParentMid));
                    pNode.ParCompY += disTop - this.DisBetweenParentMid;
                }

                int x = nodeRect.Left + nodeRect.Width / 2;
                AddLine(lines, new Line(new Point(x, pNodeRect.Top), new Point(x, node.RectInPage.Top)));
            }
            else if (compensatedTop <= this.DisBetweenParentMin) // 与父视图顶部距离在(-∞, min]之间时
            {
                if ((MyConst.View.KnxPageType == pNode.Name) ||  // 父视图为KNXPage，不能超出父视图的边沿
                    ((MyConst.Controls.KnxGroupBoxType == pNode.Name) && (compensatedTop >= 0))) // 父视图为KNXGroupBox
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                    {
                        SetChildNodeY(node, node.GetLocationYFromLocationYInPage(pNodeRect.Top));
                        pNode.ParCompY += disTop;
                    }

                    AddLine(lines, new Line(pNodeRect.Location, new Point(pNodeRect.Right, pNodeRect.Top)));
                }
            }
            else if (compensatedBottom >= -this.DisBetweenParentMax && compensatedBottom <= -this.DisBetweenParentMid) // 与父视图底部距离在[-max，-mid]之间时
            {
                if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                {
                    SetChildNodeY(node, node.GetLocationYFromLocationBottomInPage(pNodeRect.Bottom - this.DisBetweenParentMid));
                    pNode.ParCompY += disBottom + this.DisBetweenParentMid;
                }

                int x = nodeRect.Left + nodeRect.Width / 2;
                AddLine(lines, new Line(new Point(x, node.RectInPage.Bottom), new Point(x, pNodeRect.Bottom)));
            }
            else if (compensatedBottom >= -this.DisBetweenParentMin) // 与父视图底部距离在(-∞, -min]之间时
            {
                if ((MyConst.View.KnxPageType == pNode.Name) ||  // 父视图为KNXPage，不能超出父视图的边沿
                    ((MyConst.Controls.KnxGroupBoxType == pNode.Name) && (compensatedBottom <= 0))) // 父视图为KNXGroupBox
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                    {
                        SetChildNodeY(node, node.GetLocationYFromLocationBottomInPage(pNodeRect.Bottom));
                        pNode.ParCompY += disBottom;
                    }

                    AddLine(lines, new Line(new Point(pNodeRect.Left, pNodeRect.Bottom), new Point(pNodeRect.Right, pNodeRect.Bottom)));
                }
            }
            else
            {
                SetChildNodeYOffset(node, pNode.ParCompY);
                pNode.ParCompY = 0;
            }
        }

        /// <summary>
        /// 与兄弟控件在水平方向进行距离检查
        /// </summary>
        /// <param name="nodeRect"></param>
        /// <param name="oNode"></param>
        /// <param name="Lines"></param>
        /// <returns></returns>
        private bool MouseMoveWork_ChekMarginHorizontal_BetweenNode(ViewNode node, ViewNode oNode, List<Line> lines)
        {
            Rectangle nodeRect = node.RectInPage;
            Rectangle vNodeRect = oNode.RectInPage;
            bool alreadyMargin = false;

            int disLeft = nodeRect.Left - vNodeRect.Right; // 左边距
            int compensatedLeft = disLeft + oNode.GapCompX;
            int disRight = nodeRect.Right - vNodeRect.Left; // 右边距
            int compensatedRight = disRight + oNode.GapCompX;

            /* 计算水平投影重合部分的高度 */
            int height = 0;
            bool isTop = false;
            if ((nodeRect.Top >= vNodeRect.Top) && (nodeRect.Top <= vNodeRect.Bottom)) // 两个控件的水平投影有重合，其已选控件靠上
            {
                if (vNodeRect.Bottom >= nodeRect.Bottom)
                {
                    height = nodeRect.Height;
                }
                else
                {
                    height = vNodeRect.Bottom - nodeRect.Top;
                }

                isTop = true;
            }
            else if ((vNodeRect.Top > nodeRect.Top) && (vNodeRect.Top <= nodeRect.Bottom)) // 两个控件的水平投影有重合，其已选控件靠下
            {
                if (nodeRect.Bottom >= vNodeRect.Bottom)
                {
                    height = vNodeRect.Height;
                }
                else
                {
                    height = nodeRect.Bottom - vNodeRect.Top;
                }

                isTop = false;
            }
            if ((compensatedLeft >= this.DisBetweenControlMid) && (compensatedLeft <= this.DisBetweenControlMax)) // 两控件的水平距离[mid, max]
            {
                if (height > 0) // 两控件的水平投影有重合
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        SetChildNodeX(node, node.GetLocationXFromLocationXInPage(vNodeRect.Right + this.DisBetweenControlMid));
                        oNode.GapCompX += disLeft - this.DisBetweenControlMid;
                        //node.GapNode = oNode;
                    }

                    int y;
                    if (isTop)
                    {
                        y = nodeRect.Top + height / 2;
                    }
                    else
                    {
                        y = vNodeRect.Top + height / 2;
                    }

                    AddLine(lines, new Line(new Point(vNodeRect.Right, y), new Point(node.RectInPage.Left, y)));
                    alreadyMargin = true;
                    //break;
                }
            }
            else if ((compensatedLeft >= 0) && (compensatedLeft <= this.DisBetweenControlMin)) // 两控件的水平距离[0, min]
            {
                if (height > 0) // 两控件的水平投影有重合
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        SetChildNodeX(node, node.GetLocationXFromLocationXInPage(vNodeRect.Right));
                        oNode.GapCompX += disLeft;
                    }

                    AddLine(lines, new Line(new Point(vNodeRect.Right, vNodeRect.Top), new Point(vNodeRect.Right, vNodeRect.Bottom)));
                    AddLine(lines, new Line(node.RectInPage.Location, new Point(node.RectInPage.Left, nodeRect.Bottom)));
                    alreadyMargin = true;
                }
            }
            else if ((compensatedRight >= -this.DisBetweenControlMax) && (compensatedRight <= -this.DisBetweenControlMid)) // 两控件的水平距离[-max, -min]
            {
                if (height > 0) // 两控件的水平投影有重合
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        SetChildNodeX(node, node.GetLocationXFromLocationRightInPage(vNodeRect.Left - this.DisBetweenControlMid));
                        oNode.GapCompX += disRight + this.DisBetweenControlMid;
                    }

                    int y;
                    if (isTop)
                    {
                        y = nodeRect.Top + height / 2;
                    }
                    else
                    {
                        y = vNodeRect.Top + height / 2;
                    }
                    AddLine(lines, new Line(new Point(node.RectInPage.Right, y), new Point(vNodeRect.Left, y)));
                    alreadyMargin = true;
                }
            }
            else if ((compensatedRight >= -this.DisBetweenControlMin) && (compensatedRight < 0)) // 两控件的水平距离[-min, 0)
            {
                if (height > 0) // 两控件的水平投影有重合
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        SetChildNodeX(node, node.GetLocationXFromLocationRightInPage(vNodeRect.Left));
                        oNode.GapCompX += disRight;
                    }

                    AddLine(lines, new Line(vNodeRect.Location, new Point(vNodeRect.Left, vNodeRect.Bottom)));
                    AddLine(lines, new Line(new Point(node.RectInPage.Right, nodeRect.Top), new Point(node.RectInPage.Right, nodeRect.Bottom)));
                    alreadyMargin = true;
                }
            }
            else
            {
                SetChildNodeXOffset(node, oNode.GapCompX);
                oNode.GapCompX = 0;
                alreadyMargin = true;
            }

            return alreadyMargin;
        }

        /// <summary>
        /// 与兄弟控件在垂直方向进行距离检查
        /// </summary>
        /// <param name="nodeRect"></param>
        /// <param name="oNode"></param>
        /// <param name="Lines"></param>
        /// <returns></returns>
        private bool MouseMoveWork_ChekMarginVertical_BetweenNode(ViewNode node, ViewNode oNode, List<Line> lines)
        {
            Rectangle nodeRect = node.RectInPage;
            Rectangle vNodeRect = oNode.RectInPage;
            bool alreadyMargin = false;

            int disTop = nodeRect.Top - vNodeRect.Bottom; // 上边距
            int compensatedTop = disTop + oNode.GapCompY;
            int disBottom = nodeRect.Bottom - vNodeRect.Top; // 下边距
            int compensatedBottom = disBottom + oNode.GapCompY;

            /* 计算垂直投影重合部分的宽度 */
            int width = 0;
            bool isLeft = false;
            if ((nodeRect.Left >= vNodeRect.Left) && (nodeRect.Left <= vNodeRect.Right)) // 两个控件的垂直投影有重合，其已选控件靠左
            {
                if (vNodeRect.Right >= nodeRect.Right)
                {
                    width = nodeRect.Width;
                }
                else
                {
                    width = vNodeRect.Right - nodeRect.Left;
                }

                isLeft = true;
            }
            else if ((vNodeRect.Left > nodeRect.Left) && (vNodeRect.Left <= nodeRect.Right)) // 两个控件的垂直投影有重合，其已选控件靠右
            {
                if (nodeRect.Right >= vNodeRect.Right)
                {
                    width = vNodeRect.Width;
                }
                else
                {
                    width = nodeRect.Right - vNodeRect.Left;
                }

                isLeft = false;
            }
            if ((compensatedTop >= this.DisBetweenControlMid) && (compensatedTop <= this.DisBetweenControlMax)) // 两控件的垂直距离[mid, max]
            {
                if (width > 0) // 两控件垂直投影有重合
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 已选控件处于移动状态时才产生吸附效应
                    {
                        SetChildNodeY(node, node.GetLocationYFromLocationYInPage(vNodeRect.Bottom + this.DisBetweenControlMid));
                        oNode.GapCompY += disTop - this.DisBetweenControlMid;
                    }

                    int x;
                    if (isLeft)
                    {
                        x = nodeRect.Left + width / 2;
                    }
                    else
                    {
                        x = vNodeRect.Left + width / 2;
                    }
                    AddLine(lines, new Line(new Point(x, vNodeRect.Bottom), new Point(x, node.RectInPage.Top)));
                    alreadyMargin = true;
                }
            }
            else if ((compensatedTop >= 0) && (compensatedTop <= this.DisBetweenControlMin)) // 两控件的垂直距离[0, 5]
            {
                if (width > 0) // 两控件垂直投影有重合
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 已选控件处于移动状态时才产生吸附效应
                    {
                        SetChildNodeY(node, node.GetLocationYFromLocationYInPage(vNodeRect.Bottom));
                        oNode.GapCompY += disTop;
                    }

                    AddLine(lines, new Line(new Point(vNodeRect.Left, vNodeRect.Bottom), new Point(vNodeRect.Right, vNodeRect.Bottom)));
                    AddLine(lines, new Line(node.RectInPage.Location, new Point(nodeRect.Right, node.RectInPage.Top)));
                    alreadyMargin = true;
                }
            }
            else if ((compensatedBottom >= -this.DisBetweenControlMax) && (compensatedBottom <= -this.DisBetweenControlMid)) // 两控件的垂直距离[-max, -mid]
            {
                if (width > 0) // 两控件垂直投影有重合
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        SetChildNodeY(node, node.GetLocationYFromLocationBottomInPage(vNodeRect.Top - this.DisBetweenControlMid));
                        oNode.GapCompY += disBottom + this.DisBetweenControlMid;
                    }

                    int x;
                    if (isLeft)
                    {
                        x = nodeRect.Left + width / 2;
                    }
                    else
                    {
                        x = vNodeRect.Left + width / 2;
                    }
                    AddLine(lines, new Line(new Point(x, node.RectInPage.Bottom), new Point(x, vNodeRect.Top)));
                    alreadyMargin = true;
                }
            }
            else if ((compensatedBottom >= -this.DisBetweenControlMin) && (compensatedBottom <= 0)) // 两控件的垂直距离
            {
                if (width > 0) // 两控件垂直投影有重合
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        SetChildNodeY(node, node.GetLocationYFromLocationBottomInPage(vNodeRect.Top));
                        oNode.GapCompY += disBottom;
                    }

                    AddLine(lines, new Line(vNodeRect.Location, new Point(vNodeRect.Right, vNodeRect.Top)));
                    AddLine(lines, new Line(new Point(nodeRect.Left, node.RectInPage.Bottom), new Point(nodeRect.Right, node.RectInPage.Bottom)));
                    alreadyMargin = true;
                }
            }
            else
            {
                SetChildNodeYOffset(node, oNode.GapCompY);
                oNode.GapCompY = 0;
                alreadyMargin = true;
            }

            return alreadyMargin;
        }

        /// <summary>
        /// 外边距比对。与父控件进行外边距比对；与处于同一父控件下的兄弟控件进行外边距比对
        /// </summary>
        private void MouseMoveWork_CheckMargin(ViewNode node, List<Line> Lines)
        {
            Rectangle nodeRect = node.RectInPage;
            ViewNode pNode = node.Parent as ViewNode;
            Rectangle pNodeRect = pNode.RectInPage;

            MouseMoveWork_CheckMarginHorizontal_BetweenParent(node, pNode, Lines);
            MouseMoveWork_CheckMarginVertical_BetweenParent(node, pNode, Lines);

            ///* 与同一父视图下兄弟控件的边距 */
            foreach (ViewNode oNode in pNode.Nodes)
            {
                if (oNode.IsThisSelected)
                {
                    continue;
                }

                MouseMoveWork_ChekMarginHorizontal_BetweenNode(node, oNode, Lines);

                MouseMoveWork_ChekMarginVertical_BetweenNode(node, oNode, Lines);
            }
        }

        /// <summary>
        /// 与其他控件在垂直方向进行边沿对齐检查
        /// </summary>
        /// <param name="node"></param>
        /// <param name="oNode"></param>
        /// <param name="Lines"></param>
        /// <returns></returns>
        private bool MouseMoveWork_ControlAlignmentVertical(ViewNode node, ViewNode oNode, List<Line> lines)
        {
            Rectangle sNodeRect = node.RectInPage;
            Rectangle vNodeRect = oNode.RectInPage;
            bool alreadyAlign = false;

            int sideLeft = sNodeRect.Left - vNodeRect.Left; // 左边沿比对
            int compensateLeft = sideLeft + oNode.AliCompX;
            int sideRight = sNodeRect.Right - vNodeRect.Right; // 右边沿比对
            int compensateRight = sideRight + oNode.AliCompX;
            if ((compensateLeft >= 0) && (compensateLeft <= this.SideAlignBetweenControl)) // 比对控件与被比对控件的左侧距离差[0, align]
            {
                if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 仅控件处于移动状态时才产生吸附效应
                {
                    SetChildNodeX(node, node.GetLocationXFromLocationXInPage(vNodeRect.Left));
                    oNode.AliCompX += sideLeft;
                }

                if (sNodeRect.Top > vNodeRect.Top) // 比对控件在被比对控件的上侧时
                {
                    AddLine(lines, new Line(new Point(vNodeRect.Left - 1, vNodeRect.Top), new Point(node.RectInPage.Left - 1, sNodeRect.Bottom)));
                }
                else
                {
                    AddLine(lines, new Line(new Point(node.RectInPage.Left - 1, sNodeRect.Top), new Point(vNodeRect.Left - 1, vNodeRect.Bottom)));
                }

                alreadyAlign = true;
            }
            else if ((compensateRight >= -this.SideAlignBetweenControl) && (compensateRight <= 0)) // 比对控件与被比对控件的右侧距离差[-align, 0]
            {
                if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 仅控件处于移动状态时才产生吸附效应
                {
                    SetChildNodeX(node, node.GetLocationXFromLocationRightInPage(vNodeRect.Right));
                    oNode.AliCompX += sideRight;
                }

                if (sNodeRect.Top > vNodeRect.Top) // 比对控件在被比对控件的上侧时
                {
                    AddLine(lines, new Line(new Point(vNodeRect.Right, vNodeRect.Top), new Point(node.RectInPage.Right, sNodeRect.Bottom)));
                }
                else
                {
                    AddLine(lines, new Line(new Point(node.RectInPage.Right, sNodeRect.Top), new Point(vNodeRect.Right, vNodeRect.Bottom)));
                }

                alreadyAlign = true;
            }
            else
            {
                SetChildNodeXOffset(node, oNode.AliCompX);
                oNode.AliCompX = 0;

                alreadyAlign = true;
            }

            return alreadyAlign;
        }

        /// <summary>
        /// 与其他控件在水平方向进行边沿对齐检查
        /// </summary>
        /// <param name="node"></param>
        /// <param name="oNode"></param>
        /// <param name="Lines"></param>
        /// <returns></returns>
        private bool MouseMoveWork_ControlAlignmentHorizontal(ViewNode node, ViewNode oNode, List<Line> lines)
        {
            Rectangle sNodeRect = node.RectInPage;
            Rectangle vNodeRect = oNode.RectInPage;
            bool alreadyAlign = false;

            int sideTop = sNodeRect.Top - vNodeRect.Top; // 上边沿比对
            int compensateTop = sideTop + oNode.AliCompY;
            int sideBottom = sNodeRect.Bottom - vNodeRect.Bottom; // 下边沿比对
            int compensateBottom = sideBottom + oNode.AliCompY;
            if ((compensateTop >= 0) && (compensateTop <= this.SideAlignBetweenControl))   // 比对控件与被比对控件的顶部距离差在[0, align]之间时
            {
                if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 仅控件处于移动状态时才产生吸附效应
                {
                    SetChildNodeY(node, node.GetLocationYFromLocationYInPage(vNodeRect.Top));
                    oNode.AliCompY += sideTop;
                }

                if (sNodeRect.Left > vNodeRect.Left) // 比对控件在被比对控件的左侧
                {
                    AddLine(lines, new Line(new Point(vNodeRect.Left, vNodeRect.Top - 1), new Point(sNodeRect.Right, node.RectInPage.Top - 1)));
                }
                else // 比对控件在被比对控件的右侧或两者的起始点重合时
                {
                    AddLine(lines, new Line(new Point(sNodeRect.Left, node.RectInPage.Top - 1), new Point(vNodeRect.Right, vNodeRect.Top - 1)));
                }

                alreadyAlign = true;
                //break;
            }
            else if ((compensateBottom >= -this.SideAlignBetweenControl) && (compensateBottom <= 0)) // 比对控件与被比对控件的底部距离差在[-align, 0]之间时
            {
                if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)  // 仅控件处于移动状态时才产生吸附效应
                {
                    SetChildNodeY(node, node.GetLocationYFromLocationBottomInPage(vNodeRect.Bottom));
                    oNode.AliCompY += sideBottom;
                }

                if (sNodeRect.Left > vNodeRect.Left) // 比对控件在被比对控件的左侧
                {
                    AddLine(lines, new Line(new Point(vNodeRect.Left, vNodeRect.Bottom), new Point(sNodeRect.Right, node.RectInPage.Bottom)));
                }
                else
                {
                    AddLine(lines, new Line(new Point(sNodeRect.Left, node.RectInPage.Bottom), new Point(vNodeRect.Right, vNodeRect.Bottom)));
                }

                alreadyAlign = true;
            }
            else
            {
                SetChildNodeYOffset(node, oNode.AliCompY);
                oNode.AliCompY = 0;

                alreadyAlign = true;
            }

            return alreadyAlign;
        }

        /// <summary>
        /// 与其他控件进行对齐检查
        /// </summary>
        /// <param name="pNode"></param>
        /// <param name="node"></param>
        /// <param name="Lines"></param>
        /// <returns></returns>
        private bool MouseMoveWork_ControlsAlignment(ViewNode pNode, ViewNode node, List<Line> Lines)
        {
            if ((null == pNode) || (null == node) || (null == Lines))
            {
                return true;
            }

            Rectangle sNodeRect = node.RectInPage;
            bool alreadyAlign = false;

            foreach (ViewNode oNode in pNode.Nodes) // 遍历node下的子控件
            {
                Rectangle vNodeRect = oNode.RectInPage;

                /* 不与自身、父控件进行边沿比对。与父控件边沿比对请看 ↑CheckMargin() */
                if (oNode.IsThisSelected)
                {
                    continue;
                }

                if (oNode == node.Parent)
                {
                    alreadyAlign = MouseMoveWork_ControlsAlignment(node.Parent as ViewNode, node, Lines);
                }

                MouseMoveWork_ControlAlignmentHorizontal(node, oNode, Lines);

                MouseMoveWork_ControlAlignmentVertical(node, oNode, Lines);

                /* 若被比对控件还包含子控件，则继续与其子控件进行边沿比对 */
                alreadyAlign = MouseMoveWork_ControlsAlignment(oNode, node, Lines);
                if (alreadyAlign)
                {
                    break;
                }
            }

            return alreadyAlign;
        }

        /// <summary>
        /// 边沿对齐。遍历node下所有子孙控件，让已选控件与他们进行边沿比对
        /// </summary>
        /// <param name="node"></param>
        private void CheckAlignment(ViewNode node, List<Line> Lines)
        {
            MouseMoveWork_ControlsAlignment(this.mPageNode, node, Lines);
        }

        private void MouseMoveWork_MoveController(ViewNode node, int dx, int dy, Point p)
        {
            EraseLines();
            RefreshControl(node);
            //node.X += dx;
            //node.Y += dy;
            node.Location = new Point(node.Location.X + dx, node.Location.Y + dy);

            MouseMoveWork_MoveOutIn(node, p);

            if (node == this.CurrentSelectedControl)
            {
                List<Line> lines = new List<Line>();
                MouseMoveWork_CheckMargin(node, lines);
                CheckAlignment(node, lines);

                DrawControl(node);
                DrawLines(lines);
            }
            else
            {
                DrawControl(node);
            }

            PageChangedEventNotify(node, EventArgs.Empty);
        }

        private void MouseMoveWork_ChangeSize(ViewNode node, int dx, int dy)
        {
            EraseLines();
            RefreshControl(node);

            Rectangle rect = node.FrameChanged(dx, dy, this.ShiftDown);
            //node.X = rect.X;
            //node.Y = rect.Y;
            //node.Width = rect.Width;
            //node.Height = rect.Height;
            node.Location = rect.Location;
            node.Size = rect.Size;

            List<Line> lines = new List<Line>();

            MouseMoveWork_CheckMargin(node, lines);
            CheckAlignment(node, lines);

            DrawControl(node);
            DrawLines(lines);

            PageChangedEventNotify(node, EventArgs.Empty);
        }

        private void MouseMoveWork_BoxControl(Point p)
        {
            ClearSelectedNodes();
            EraseRectangleFrame();

            Rectangle BoxRect;

            /* 以起始点为坐标原点 */
            if (p.X - this.StartPoint.X >= 0)
            {
                if (p.Y - this.StartPoint.Y >= 0) // 第一象限
                {
                    BoxRect = new Rectangle(this.StartPoint, new Size(p.X - this.StartPoint.X, p.Y - this.StartPoint.Y));
                }
                else // 第四象限
                {
                    BoxRect = new Rectangle(new Point(this.StartPoint.X, p.Y), new Size(p.X - this.StartPoint.X, this.StartPoint.Y - p.Y));
                }
            }
            else
            {
                if (p.Y - this.StartPoint.Y >= 0) // 第二象限
                {
                    BoxRect = new Rectangle(new Point(p.X, this.StartPoint.Y), new Size(this.StartPoint.X - p.X, p.Y - this.StartPoint.Y));
                }
                else // 第三象限
                {
                    BoxRect = new Rectangle(p, new Size(this.StartPoint.X - p.X, this.StartPoint.Y - p.Y));
                }
            }

            BoxControl(this.CurrentSelectedControl, BoxRect);
            DrawRectangleFrame(BoxRect);
        }

        /// <summary>
        /// 鼠标移动时的任务
        /// </summary>
        /// <param name="obj"></param>
        private void MouseMove_Work(MouseEventArgs e/*object obj*/)
        {
            //GetContainsPointNodes(e.Location);

            //MouseEventArgs e = obj as MouseEventArgs;
            //Point p = new Point((int)Math.Round(e.X / this.Ratio, 0), (int)Math.Round(e.Y / this.Ratio, 0));
            Point p = ConvertToFactLocation(e.Location); // 当前鼠标的位置。未缩放的原始图

            if (p.Equals(this.PrePoint))
            {
                return;
            }

            if (MouseButtons.Left == e.Button) // 鼠标左键按下
            {
                if ((null != this.CurrentSelectedControl) &&
                    (this.SelectedNodes.Contains(this.CurrentSelectedControl))) // 当前有选中控件
                {
                    int dx = p.X - this.PrePoint.X;
                    int dy = p.Y - this.PrePoint.Y;

                    if ((UIEditor.Entity.ViewNode.ControlState.Selected == this.CurrentSelectedControl.State) ||
                        (UIEditor.Entity.ViewNode.ControlState.SelectedAgain == this.CurrentSelectedControl.State)) // 当前鼠标点击控件状态为Down(第一次拖动)
                    {
                        if (this.CtrlDown) // Ctrl键被按下，表示复制控件
                        {
                            MouseMoveWork_CopyControl();
                        }
                        else // 否则，将各控件的状态置为【移动状态】
                        {
                            foreach (ViewNode node in this.SelectedNodes)
                            {
                                node.State = UIEditor.Entity.ViewNode.ControlState.Move;
                                //node.PreLocation = new Point(node.X, node.Y);
                                node.PreLocation = node.Location;
                                //node.PreLocation = node.DrawLocation;

                                SetGroupBoxInnerControlToStateMove(node);
                            }
                        }

                        InitBeforeMoveControl(this.mPageNode);
                    }

                    if (UIEditor.Entity.ViewNode.ControlState.Move == this.CurrentSelectedControl.State)
                    {
                        foreach (ViewNode node in this.SelectedNodes)
                        {
                            MouseMoveWork_MoveController(node, dx, dy, e.Location);
                        }
                    }
                    else if (UIEditor.Entity.ViewNode.ControlState.ChangeSize == this.CurrentSelectedControl.State) // 控件处于调节大小状态
                    {
                        MouseMoveWork_ChangeSize(this.CurrentSelectedControl, dx, dy);
                    }
                }

                if (this.IsBoxControl) // 根据鼠标运动轨迹绘制矩形边框---多选
                {
                    MouseMoveWork_BoxControl(e.Location);
                }

            }
            else if (MouseButtons.Right == e.Button) // 鼠标右键按下
            {
                // 什么也不做
            }
            else // 鼠标无键按下，仅移动鼠标
            {
                foreach (ViewNode node in this.SelectedNodes)
                {
                    if (node.FrameIsVisible)
                    {
                        node.MouseMove(e);
                    }
                }
            }

            this.PrePoint = ConvertToFactLocation(e.Location);
        }
        #endregion

        #region Panel 鼠标释放后的任务
        private void MouseUp_Work(MouseEventArgs e)
        {
            if ((null != this.CurrentSelectedControl) &&
                (UIEditor.Entity.ViewNode.ControlState.SelectedAgain == this.CurrentSelectedControl.State)) // 点击了已选中的控件的本体
            {
                RemoveSelectedNode(this.CurrentSelectedControl);
            }

            if (this.SelectedNodes.Count > 0)
            {
                foreach (ViewNode node in this.SelectedNodes)
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        //this.mCommandQueue.ExecuteCommand(new NodePropertyLocation(node, node.PreLocation, new Point(node.X, node.Y)));
                        this.mCommandQueue.ExecuteCommand(new NodePropertyLocation(node, node.PreLocation, node.Location));
                        ResumeGroupBoxInnerControlToStateNormal(node);

                        RefreshControl(node);
                    }
                    else if (UIEditor.Entity.ViewNode.ControlState.ChangeSize == node.State)
                    {
                        this.mCommandQueue.ExecuteCommand(new NodePropertyBound(node, node.PreBound, node.FactRect));
                    }

                    node.State = UIEditor.Entity.ViewNode.ControlState.Up;

                    ControlSelectedEventNotify(node);
                }
            }
            else
            {
                ControlSelectedEventNotify(this.mPageNode);
            }
        }
        #endregion

        #region Panel事件
        /// <summary>
        /// 鼠标点击Panel事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            this.PrePoint = ConvertToFactLocation(e.Location);
            this.IsBoxControl = false;

            if (MouseButtons.Left == e.Button) // 鼠标左键
            {
                MouseLeftButtonDown_Work(e.Location);
            }
            else // 鼠标右键
            {
                this.FactPoint = e.Location;

                ContextMenuStrip cms = new ContextMenuStrip();
                this.ContextMenuStrip = cms;

                //ToolStripMenuItem addControlItem = CreateAddControlMenuItem();
                ToolStripMenuItem collectionItem = CreateCollectionMenuItem();
                //ToolStripMenuItem templateItem = CreateTemplateMenuItem();
                ToolStripMenuItem copyItem = CreateCopyMenuItem();
                ToolStripMenuItem cutItem = CreateCutMenuItem();
                ToolStripMenuItem pasteItem = CreatePasteMenuItem();
                ToolStripMenuItem deleteItem = CreateDeleteMenuItem();
                ToolStripMenuItem addToCollectionsItem = CreateAddToCollectionMenuItem();
                //ToolStripMenuItem addToTemplatesItem = CreateAddToTemplateMenuItem(); 
                ToolStripMenuItem refreshItem = CreateRefreshMenuItem();

                //cms.Items.Add(addControlItem);
                cms.Items.Add(collectionItem);
                //cms.Items.Add(templateItem);
                cms.Items.Add(new ToolStripSeparator());
                cms.Items.Add(copyItem);
                cms.Items.Add(cutItem);
                cms.Items.Add(pasteItem);
                cms.Items.Add(deleteItem);
                cms.Items.Add(new ToolStripSeparator());
                cms.Items.Add(addToCollectionsItem);
                //cms.Items.Add(addToTemplatesItem);
                cms.Items.Add(new ToolStripSeparator());
                cms.Items.Add(refreshItem);

                if (null == MyCache.ClipBoard as List<ViewNode>)
                {
                    pasteItem.Enabled = false;
                }

                if (this.SelectedNodes.Count > 0)
                {
                    copyItem.Enabled = cutItem.Enabled = addToCollectionsItem.Enabled = SelectedControlsIsBrotherhood();
                }
                else
                {
                    copyItem.Enabled = false;
                    cutItem.Enabled = false;
                    deleteItem.Enabled = false;
                    addToCollectionsItem.Enabled = false;
                }
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove_Work(e);

            ControlSelectedEventArgs arg = new ControlSelectedEventArgs();
            arg.mPageNode = this.mPageNode;
            arg.mNodes = this.SelectedNodes;
            SelectedControlsMoveEventNotify(this.mPageNode, arg);

            SetCursor(null);
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            EraseLines();

            if (this.IsBoxControl)
            {
                EraseRectangleFrame();
            }

            MouseUp_Work(e);

            this.IsBoxControl = false;
        }

        private void Panel_KeyDown(object sender, KeyEventArgs e)
        {
            LayerControlsKeyDowns(e);
        }

        private void Panel_KeyUp(object sender, KeyEventArgs e)
        {
            LayerControlsKeyUps(e);
        }

        private void Panel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point p = e.Location;

            if (MouseButtons.Left == e.Button)
            {
                GetContainsPointNodes(p);
                ViewNode topestNode = GetTopNode();
                if (this.mPageNode != topestNode) // 选中的不是当前页面本身
                {
                    if (null != (topestNode as ShutterNode))
                    {
                        FrmShutter frm = new FrmShutter(topestNode as ShutterNode);
                        if (DialogResult.OK == frm.ShowDialog())
                        {
                            PageChangedEventNotify(topestNode, EventArgs.Empty);
                        }
                    }

                    if (null != (topestNode as DimmerNode))
                    {
                        FrmDimmer frm = new FrmDimmer(topestNode as DimmerNode);
                        if (DialogResult.OK == frm.ShowDialog())
                        {
                            PageChangedEventNotify(topestNode, EventArgs.Empty);
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 擦除矩形方框
        /// </summary>
        /// <param name="obj"></param>
        private void EraseRectangleFrame()
        {
            this.mLayerLines.Visible = false;
            //this.mLayerLines.SendToBack();
        }

        /// <summary>
        /// 用虚线绘制矩形方框
        /// </summary>
        /// <param name="obj"></param>
        private void DrawRectangleFrame(Rectangle rect)
        {
            this.mLayerLines.Visible = true;
            this.mLayerLines.BringToFront();
            this.mLayerLines.DrawRectangleLightGrayDot(rect);
        }

        private void EraseLines()
        {
            this.mLayerLines.Visible = false;
        }

        /// <summary>
        /// 绘制线条
        /// </summary>
        /// <param name="Lines"></param>
        private void DrawLines(List<Line> Lines)
        {
            this.mLayerLines.Visible = true;
            this.mLayerLines.BringToFront();
            this.mLayerLines.DrawLines(Lines);
        }

        private void AddLine(List<Line> lines, Line l)
        {
            lines.Add(l);
        }

        /// <summary>
        /// 刷新矩形区域
        /// </summary>
        /// <param name="rect">需要刷新的矩形</param>
        private void RefreshRectangle(Rectangle rect)
        {
            this.Invalidate(rect, false);
        }

        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="node"></param>
        private void DrawControl(ViewNode node)
        {
            Graphics g = this.CreateGraphics();

            node.DrawAt(g, this.Ratio, false);
        }

        private void RefreshControl(ViewNode node)
        {
            RefreshRectangle(node.FrameBound);
        }

        private void DeselectNode(ViewNode node)
        {
            node.Deselected();
            RefreshControl(node);
        }

        private void RemoveSelectedNode(ViewNode node)
        {
            DeselectNode(node);

            this.SelectedNodes.Remove(node);
        }

        private void RemoveOthersNode(ViewNode node)
        {
            List<ViewNode> list = new List<ViewNode>();
            foreach (ViewNode n in this.SelectedNodes)
            {
                if (n != node)
                {
                    list.Add(n);
                }
            }

            foreach (ViewNode n in list)
            {
                RemoveSelectedNode(n);
            }
        }

        /// <summary>
        /// 清空【已选控件列表】
        /// </summary>
        private void ClearSelectedNodes()
        {
            foreach (ViewNode node in this.SelectedNodes)
            {
                DeselectNode(node);
            }

            this.SelectedNodes.Clear();
        }

        /// <summary>
        /// 添加控件到【已选控件列表】
        /// </summary>
        /// <param name="node"></param>
        private void AddSelectedNode(ViewNode node)
        {
            node.Selected();

            this.SelectedNodes.Add(node);

            DrawControl(node);
        }

        /// <summary>
        /// 设置鼠标的形状
        /// </summary>
        private void SetCursor(object obj)
        {
            if (null != LayerControls.ToAddControl)
            {
                this.Cursor = Cursors.Cross;
            }
            else if (this.SelectedNodes.Count > 0)
            {
                foreach (ViewNode node in this.SelectedNodes)
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        //this.Cursor = Cursors.SizeAll;
                    }
                    else if (UIEditor.Entity.ViewNode.MousePosOnCtrl.TOPLEFT == node.MPOC || UIEditor.Entity.ViewNode.MousePosOnCtrl.BOTTOMRIGHT == node.MPOC)
                    {
                        this.Cursor = Cursors.SizeNWSE;
                    }
                    else if (UIEditor.Entity.ViewNode.MousePosOnCtrl.TOP == node.MPOC || UIEditor.Entity.ViewNode.MousePosOnCtrl.BOTTOM == node.MPOC)
                    {
                        this.Cursor = Cursors.SizeNS;
                    }
                    else if (UIEditor.Entity.ViewNode.MousePosOnCtrl.TOPRIGHT == node.MPOC || UIEditor.Entity.ViewNode.MousePosOnCtrl.BOTTOMLEFT == node.MPOC)
                    {
                        this.Cursor = Cursors.SizeNESW;
                    }
                    else if (UIEditor.Entity.ViewNode.MousePosOnCtrl.RIGHT == node.MPOC || UIEditor.Entity.ViewNode.MousePosOnCtrl.LEFT == node.MPOC)
                    {
                        this.Cursor = Cursors.SizeWE;
                    }
                    else if (UIEditor.Entity.ViewNode.MousePosOnCtrl.SizeAll == node.MPOC)
                    {
                        this.Cursor = Cursors.SizeAll;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void RefreshThisPage()
        {
            this.Refresh();
        }
        #endregion

        #region ViewNode方法
        private void SetChildNodeX(ViewNode node, int x)
        {
            //node.X = (int)Math.Round((float)x / this.Ratio, 0);
            node.Location = new Point((int)Math.Round((float)x / this.Ratio, 0), node.Location.Y);
        }

        private void SetChildNodeXOffset(ViewNode node, int offset)
        {
            //node.X += (int)Math.Round((float)offset / this.Ratio, 0);
            node.Location.Offset((int)Math.Round((float)offset / this.Ratio, 0), 0);
        }

        private void SetChildNodeY(ViewNode node, int y)
        {
            //node.Y = (int)Math.Round((float)y / this.Ratio, 0);
            node.Location = new Point(node.Location.X, (int)Math.Round((float)y / this.Ratio, 0));
        }

        private void SetChildNodeYOffset(ViewNode node, int offset)
        {
            //node.Y += (int)Math.Round((float)offset / this.Ratio, 0);
            node.Location.Offset(0, (int)Math.Round((float)offset / this.Ratio, 0));
        }

        private void SetChildNodeWidth(ViewNode node, int width)
        {
            //node.Width = (int)Math.Round((float)width / this.Ratio, 0);
            node.Size = new Size((int)Math.Round((float)width / this.Ratio, 0), node.Size.Height);
        }

        private void SetChildNodeHeight(ViewNode node, int height)
        {
            //node.Height = (int)Math.Round((float)height / this.Ratio, 0);
            node.Size = new Size(node.Size.Width, (int)Math.Round((float)height / this.Ratio, 0));
        }
        #endregion

        private Point ConvertToFactLocation(Point p)
        {
            return new Point((int)Math.Round(p.X / this.Ratio, 0), (int)Math.Round(p.Y / this.Ratio, 0));
        }

        #region 右键菜单
        #region 创建右键菜单选项
        private ToolStripMenuItem CreateAddControlMenuItem()
        {
            ToolStripMenuItem tsmiAddControl = new ToolStripMenuItem();
            tsmiAddControl.Name = "tsmiAddControl";
            tsmiAddControl.Size = new Size(152, 22);
            tsmiAddControl.Text = UIResMang.GetString("TextAddControl");
            //tsmiAddControl.Click += AddControl_Click;

            ToolStripMenuItem groupBoxItem = CreateGroupBoxMenuItem();
            ToolStripMenuItem blindsItem = CreateBlindsMenuItem();
            ToolStripMenuItem labelItem = CreateLabelMenuItem();
            ToolStripMenuItem sceneItem = CreateSceneMenuItem();
            ToolStripMenuItem sliderSwitchItem = CreateSliderSwitchMenuItem();
            ToolStripMenuItem switchItem = CreateSwitchMenuItem();
            ToolStripMenuItem valueDisplayItem = CreateValueDisplayMenuItem();
            ToolStripMenuItem timerItem = CreateTimerMenuItem();
            ToolStripMenuItem digitalAdjustmentItem = CreateDigitalAdjustmentMenuItem();
            ToolStripMenuItem imageButtonItem = CreateImageButtonMenuItem();

            tsmiAddControl.DropDownItems.Add(groupBoxItem);
            tsmiAddControl.DropDownItems.Add(blindsItem);
            tsmiAddControl.DropDownItems.Add(labelItem);
            tsmiAddControl.DropDownItems.Add(sceneItem);
            tsmiAddControl.DropDownItems.Add(sliderSwitchItem);
            tsmiAddControl.DropDownItems.Add(switchItem);
            tsmiAddControl.DropDownItems.Add(valueDisplayItem);
            tsmiAddControl.DropDownItems.Add(timerItem);
            tsmiAddControl.DropDownItems.Add(digitalAdjustmentItem);
            tsmiAddControl.DropDownItems.Add(imageButtonItem);

            return tsmiAddControl;
        }

        private ToolStripMenuItem CreateCollectionMenuItem()
        {
            ToolStripMenuItem tsmiCollection = new ToolStripMenuItem();
            tsmiCollection.Name = "tsmiCollection";
            tsmiCollection.Size = new Size(152, 22);
            tsmiCollection.Text = UIResMang.GetString("TextCollection");
            tsmiCollection.Click += CollectionControl_Click;

            return tsmiCollection;
        }

        /// <summary>
        /// 创建右键复制选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreateCopyMenuItem()
        {
            ToolStripMenuItem tsmiCopy = new ToolStripMenuItem();
            tsmiCopy.Image = UIResMang.GetImage("Copy_16x16");
            tsmiCopy.Name = "tsmiCopyNode";
            tsmiCopy.Size = new System.Drawing.Size(152, 22);
            tsmiCopy.Text = UIResMang.GetString("Copy");
            tsmiCopy.Click += CopyControl_Click;

            return tsmiCopy;
        }

        /// <summary>
        /// 创建右键剪切选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreateCutMenuItem()
        {
            ToolStripMenuItem tsmiCut = new ToolStripMenuItem();
            tsmiCut.Image = UIResMang.GetImage("Cut_16x16");
            tsmiCut.Name = "tsmiCutNode";
            tsmiCut.Size = new System.Drawing.Size(152, 22);
            tsmiCut.Text = UIResMang.GetString("Cut");
            tsmiCut.Click += CutControl_Click;

            return tsmiCut;
        }

        /// <summary>
        /// 创建右键粘贴选项
        /// </summary>
        /// <returns></returns>
        private ToolStripMenuItem CreatePasteMenuItem()
        {
            ToolStripMenuItem tsmiPaste = new ToolStripMenuItem();
            tsmiPaste.Image = UIResMang.GetImage("Paste_16x16");
            tsmiPaste.Name = "tsmiPasteNode";
            tsmiPaste.Size = new System.Drawing.Size(152, 22);
            tsmiPaste.Text = UIResMang.GetString("Paste");
            tsmiPaste.Click += PasteControl_Click;

            return tsmiPaste;
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
            tsmiDeleteItem.Size = new Size(100, 22);
            tsmiDeleteItem.Text = UIResMang.GetString("Delete");
            tsmiDeleteItem.Click += DeleteSelectedNode_Click;

            return tsmiDeleteItem;
        }

        private ToolStripMenuItem CreateAddToCollectionMenuItem()
        {
            ToolStripMenuItem tsmiAddToCollection = new ToolStripMenuItem();
            tsmiAddToCollection.Name = "tsmiAddToCollection";
            tsmiAddToCollection.Size = new Size(152, 22);
            tsmiAddToCollection.Text = UIResMang.GetString("TextAddToCollections");
            tsmiAddToCollection.Click += AddToCollection_Click;

            return tsmiAddToCollection;
        }

        private ToolStripMenuItem CreateRefreshMenuItem()
        {
            ToolStripMenuItem tsmiRefreshPage = new ToolStripMenuItem();
            tsmiRefreshPage.Name = "tsmiRefreshPage";
            tsmiRefreshPage.Size = new Size(152, 22);
            tsmiRefreshPage.Text = UIResMang.GetString("TextRefreshPage");
            tsmiRefreshPage.Click += RefreshPage_Click;

            return tsmiRefreshPage;
        }

        #region 添加控件
        private ToolStripMenuItem CreateGroupBoxMenuItem()
        {
            ToolStripMenuItem tsmiGroupBox = new ToolStripMenuItem();
            tsmiGroupBox.Name = "tsmiGroupBox";
            tsmiGroupBox.Image = UIResMang.GetImage("GroupBox_16x16");
            tsmiGroupBox.Size = new Size(152, 22);
            tsmiGroupBox.Text = UIResMang.GetString("TextGroupBox");
            tsmiGroupBox.Click += AddControlGroupBox_Click;

            return tsmiGroupBox;
        }

        private ToolStripMenuItem CreateBlindsMenuItem()
        {
            ToolStripMenuItem tsmiBlinds = new ToolStripMenuItem();
            tsmiBlinds.Name = "tsmiBlinds";
            tsmiBlinds.Image = UIResMang.GetImage("Blinds_16x16");
            tsmiBlinds.Size = new Size(152, 22);
            tsmiBlinds.Text = UIResMang.GetString("TextBlinds");
            tsmiBlinds.Click += AddControlBlinds_Click;

            return tsmiBlinds;
        }

        private ToolStripMenuItem CreateLabelMenuItem()
        {
            ToolStripMenuItem tsmiLabel = new ToolStripMenuItem();
            tsmiLabel.Name = "tsmiLabel";
            tsmiLabel.Image = UIResMang.GetImage("Label_16x16");
            tsmiLabel.Size = new Size(152, 22);
            tsmiLabel.Text = UIResMang.GetString("TextLabel");
            tsmiLabel.Click += AddControlLabel_Click;

            return tsmiLabel;
        }

        private ToolStripMenuItem CreateSceneMenuItem()
        {
            ToolStripMenuItem tsmiScene = new ToolStripMenuItem();
            tsmiScene.Name = "tsmiScene";
            tsmiScene.Image = UIResMang.GetImage("Scene_16x16");
            tsmiScene.Size = new Size(152, 22);
            tsmiScene.Text = UIResMang.GetString("TextSceneButton");
            tsmiScene.Click += AddControlScene_Click;

            return tsmiScene;
        }

        private ToolStripMenuItem CreateSliderSwitchMenuItem()
        {
            ToolStripMenuItem tsmiSliderSwitch = new ToolStripMenuItem();
            tsmiSliderSwitch.Name = "tsmiSliderSwitch";
            tsmiSliderSwitch.Image = UIResMang.GetImage("SliderSwitch_16x16");
            tsmiSliderSwitch.Size = new Size(152, 22);
            tsmiSliderSwitch.Text = UIResMang.GetString("TextSliderSwitch");
            tsmiSliderSwitch.Click += AddControlSliderSwitch_Click;

            return tsmiSliderSwitch;
        }

        private ToolStripMenuItem CreateSwitchMenuItem()
        {
            ToolStripMenuItem tsmiSwitch = new ToolStripMenuItem();
            tsmiSwitch.Name = "tsmiSwitch";
            tsmiSwitch.Image = UIResMang.GetImage("Switch_16x16");
            tsmiSwitch.Size = new Size(152, 22);
            tsmiSwitch.Text = UIResMang.GetString("TextSwitch");
            tsmiSwitch.Click += AddControlSwitch_Click;

            return tsmiSwitch;
        }

        private ToolStripMenuItem CreateValueDisplayMenuItem()
        {
            ToolStripMenuItem tsmiValueDisplay = new ToolStripMenuItem();
            tsmiValueDisplay.Name = "tsmiValueDisplay";
            tsmiValueDisplay.Image = UIResMang.GetImage("ValueDisplay_16x16");
            tsmiValueDisplay.Size = new Size(152, 22);
            tsmiValueDisplay.Text = UIResMang.GetString("TextValueDisplay");
            tsmiValueDisplay.Click += AddControlValueDisplay_Click;

            return tsmiValueDisplay;
        }

        private ToolStripMenuItem CreateTimerMenuItem()
        {
            ToolStripMenuItem tsmiTimer = new ToolStripMenuItem();
            tsmiTimer.Name = "tsmiTimer";
            tsmiTimer.Image = UIResMang.GetImage("Timer_16x16");
            tsmiTimer.Size = new Size(152, 22);
            tsmiTimer.Text = UIResMang.GetString("TextTimer");
            tsmiTimer.Click += AddControlTimer_Click;

            return tsmiTimer;
        }

        private ToolStripMenuItem CreateDigitalAdjustmentMenuItem()
        {
            ToolStripMenuItem tsmiDigitalAdjustment = new ToolStripMenuItem();
            tsmiDigitalAdjustment.Name = "tsmiDigitalAdjustment";
            tsmiDigitalAdjustment.Image = UIResMang.GetImage("DigitalAdjustment_16x16");
            tsmiDigitalAdjustment.Size = new Size(152, 22);
            tsmiDigitalAdjustment.Text = UIResMang.GetString("TextDigitalAdjustment");
            tsmiDigitalAdjustment.Click += AddControlDigitalAdjustment_Click;

            return tsmiDigitalAdjustment;
        }

        private ToolStripMenuItem CreateImageButtonMenuItem()
        {
            ToolStripMenuItem tsmiImageButton = new ToolStripMenuItem();
            tsmiImageButton.Name = "tsmiImageButton";
            tsmiImageButton.Image = UIResMang.GetImage("imageButton");
            tsmiImageButton.Size = new Size(152, 22);
            tsmiImageButton.Text = UIResMang.GetString("TextImageButton");
            tsmiImageButton.Click += AddControlImageButton_Click;

            return tsmiImageButton;
        }
        #endregion
        #endregion

        #region 右键菜单事件
        private void CollectionControl_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCollections frmClc = new FrmCollections();
                frmClc.ShowDialog(this);

                if (DialogResult.OK == frmClc.DialogResult)
                {
                    List<ViewNode> nodes = frmClc.GetSelectedNodes();

                    ClearSelectedNodes();

                    foreach (ViewNode node in nodes)
                    {
                        AddControlAtPoint(node, new Point(this.FactPoint.X + ViewNode.ConvertToDraw(node.Location.X, this.Ratio), this.FactPoint.Y + ViewNode.ConvertToDraw(node.Location.Y, this.Ratio)));
                        AddSelectedNode(node);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 右键复制点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyControl_Click(object sender, EventArgs e)
        {
            try
            {
                //List<ViewNode> list = new List<ViewNode>();
                //foreach (ViewNode node in this.SelectedNodes)
                //{
                //    list.Add(node.Clone() as ViewNode);
                //}

                //MyCache.ClipBoard = list;

                MyCache.ClipBoard = EntityHelper.CopyControls(this.SelectedNodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 右键剪切点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutControl_Click(object sender, EventArgs e)
        {
            try
            {
                //List<ViewNode> list = new List<ViewNode>();
                //foreach (ViewNode node in this.SelectedNodes)
                //{
                //    list.Add(node.Copy() as ViewNode);
                //}

                //MyCache.ClipBoard = list;

                MyCache.ClipBoard = EntityHelper.CopyControls(this.SelectedNodes);

                foreach (ViewNode node in this.SelectedNodes)
                {
                    this.mCommandQueue.ExecuteCommand(new TreeNodeRemove(node.TreeView, node));
                }

                RefreshThisPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 右键粘贴点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteControl_Click(object sender, EventArgs e)
        {
            try
            {
                ClearSelectedNodes();

                List<ViewNode> list = MyCache.ClipBoard as List<ViewNode>;
                foreach (ViewNode node in list)
                {
                    //ViewNode cNode = node.Copy() as ViewNode;

                    AddControlAtPoint(node, new Point(this.FactPoint.X + ViewNode.ConvertToDraw(node.Location.X, this.Ratio),
                        this.FactPoint.Y + ViewNode.ConvertToDraw(node.Location.Y, this.Ratio)));
                    AddSelectedNode(node);
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
                foreach (ViewNode node in this.SelectedNodes)
                {
                    this.mCommandQueue.ExecuteCommand(new TreeNodeRemove(node.TreeView, node));
                }

                RefreshThisPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddToCollection_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedNodes.Count > 0)
                {
                    using (var cfd = new SaveFileDialog())
                    {
                        cfd.Title = UIResMang.GetString("StoreIn");
                        cfd.InitialDirectory = MyCache.DefatultKnxCollectionFolder;
                        cfd.Filter = CollectionHelper.TemplateFilter;
                        cfd.FileName = CollectionHelper.GetDefaultCollectionName();

                        if (cfd.ShowDialog(this) == DialogResult.OK)
                        {
                            string name = cfd.FileName;
                            CollectionHelper.ExportToCollections(this.SelectedNodes, name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RefreshPage_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshThisPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region 添加控件事件
        private void AddControlGroupBox_Click(object sender, EventArgs e)
        {
            try
            {
                MouseDownWork_AddControl(new GroupBoxNode(), this.FactPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddControlBlinds_Click(object sender, EventArgs e)
        {
            try
            {
                MouseDownWork_AddControl(new BlindsNode(), this.FactPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddControlLabel_Click(object sender, EventArgs e)
        {
            try
            {
                MouseDownWork_AddControl(new LabelNode(), this.FactPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddControlScene_Click(object sender, EventArgs e)
        {
            try
            {
                MouseDownWork_AddControl(new SceneButtonNode(), this.FactPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddControlSliderSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                MouseDownWork_AddControl(new SliderSwitchNode(), this.FactPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddControlSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                MouseDownWork_AddControl(new SwitchNode(), this.FactPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddControlValueDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                MouseDownWork_AddControl(new ValueDisplayNode(), this.FactPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddControlTimer_Click(object sender, EventArgs e)
        {
            try
            {
                MouseDownWork_AddControl(new TimerButtonNode(), this.FactPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddControlDigitalAdjustment_Click(object sender, EventArgs e)
        {
            try
            {
                MouseDownWork_AddControl(new DigitalAdjustmentNode(), this.FactPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddControlImageButton_Click(object sender, EventArgs e)
        {
            try
            {
                MouseDownWork_AddControl(new ImageButtonNode(), this.FactPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #endregion
        #endregion
    }

    public class BrothershipEventArgs : EventArgs
    {
        public bool IsBrothership;
    }

    public class ControlSelectedEventArgs : EventArgs
    {
        public PageNode mPageNode;
        public List<ViewNode> mNodes;
    }
}
