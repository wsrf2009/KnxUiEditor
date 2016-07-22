using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

namespace UIEditor.SationUIControl
{
    class STPage : STContainer
    {
        #region 变量
        private List<ViewNode> SelectedNodes { get; set; }
        private ViewNode CurrentSelectedControl { get; set; }
        private List<ViewNode> ContainsPointNodes { get; set; }
        public static Type ToAddControl { get; set; }
        private Point PrePoint { get; set; }
        public CommandQuene cqp { get; set; }
        private bool CtrlDown { get; set; }
        private bool Changed { get; set; }

        #region 框选控件
        private Rectangle RectFrame { get; set; }
        private Point StartPoint { get; set; }
        private bool IsBoxControl { get; set; }
        private ViewNode ParentControl { get; set; }
        #endregion

        public delegate void ControlSelectedEventDelegate(object sender, EventArgs e);
        public event ControlSelectedEventDelegate ControlSelectedEvent;

        public delegate void ControlDeleteEventDelegate(object sender, EventArgs e);
        public event ControlDeleteEventDelegate ControlDeleteEvent;

        public delegate void PageChangedEventDelegate(object sender, EventArgs e);
        public event PageChangedEventDelegate PageChangedEvent;
        #endregion

        public STPage() { }

        public STPage(PageNode node)
            : base(node)
        {
            this.Location = new Point(0, 0);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.AutoScroll = true;
            this.ChangeSize();

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseUp);

            this.SelectedNodes = new List<ViewNode>();
            this.ContainsPointNodes = new List<ViewNode>();
            this.CtrlDown = false;
            this.Changed = false;

            this.node.panel = this;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (ViewNode node in this.node.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType != node.Name)
                {
                    node.DrawAt(Point.Empty, g);
                }
            }

            foreach (ViewNode node in this.node.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType == node.Name)
                {
                    node.DrawAt(Point.Empty, g);
                }
            }
        }

        #region 获取鼠标位置的顶层控件
        /// <summary>
        /// 获取该鼠标位置的所有控件，并存储在 ContainsPointNodes 中
        /// </summary>
        /// <param name="e"></param>
        private void GetContainsPointNodes(Point location)
        {
            this.ContainsPointNodes.Clear();

            foreach (ViewNode node in this.node.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType != node.Name) // 先判断非GroupBox控件
                {
                    node.ContainsPoint(location, this.ContainsPointNodes);
                }
            }

            foreach (ViewNode node in this.node.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType == node.Name) // GroupBox控件
                {
                    node.ContainsPoint(location, this.ContainsPointNodes);
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
                return this.node;
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

        #region
        /// <summary>
        /// 用鼠标拖出的矩形框框选控件
        /// </summary>
        /// <param name="node"></param>
        private void BoxControl(ViewNode node)
        {
            foreach (ViewNode cNode in node.Nodes)
            {
                if (this.RectFrame.IntersectsWith(cNode.VisibleRectInPage))
                {
                    AddSelectedNode(cNode);
                }
            }
        }

        /// <summary>
        /// 在已选控件列表中选择包含位置p的控件
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private ViewNode GetContainsPointControl(Point p)
        {
            foreach (ViewNode node in this.SelectedNodes)
            {
                if (node.RectInPage.Contains(p))
                {
                    return node;
                }
            }

            return null;
        }
        #endregion

        #region STPage公共方法
        public void SetSelectedControl(ViewNode node)
        {
            ClearSelectedNodes();
            //node.IsSelected = true;
            //this.SelectedNodes.Add(node);
            AddSelectedNode(node);

            this.Refresh();
        }

        public void ControlPropertyChanged(ViewNode node)
        {
            this.Refresh();
        }

        public void AddControl(ViewNode node)
        {
            //this.Refresh();
            RefreshControls(node);
        }

        public void RefreshControls(ViewNode node)
        {
            this.Invalidate(node.FrameBound, false);
        }

        public void DeleteControl(ViewNode node)
        {
            this.Controls.Remove(node.panel);
            this.Controls.Remove(node.fc);
            this.Refresh();
        }

        public void ChangeSize()
        {
            this.MaximumSize = this.MinimumSize = new Size(this.node.Width, this.node.Height);

            this.RefreshUI();
        }

        public void KeyDowns(KeyEventArgs e)
        {
            if ((int)Keys.LControlKey == e.KeyValue)
            {
                this.CtrlDown = true;
            }

            if ((int)Keys.Delete == e.KeyValue)
            {
                foreach (ViewNode node in this.SelectedNodes)
                {
                    this.cqp.ExecuteCommand(new TreeNodeRemove(node.TreeView, node));

                    PageChangedEventNotify(node, EventArgs.Empty);
                }

                this.SelectedNodes.Clear();

                this.Refresh();
            }
        }

        public void KeyUps(KeyEventArgs e)
        {
            if ((int)Keys.LControlKey == e.KeyValue)
            {
                this.CtrlDown = false;
            }
        }

        public void Saved()
        {
            this.Changed = false;
        }
        #endregion

        #region 事件通知
        private void ControlSelectedEventNotify(object sender, EventArgs e)
        {
            if (null != ControlSelectedEvent)
            {
                ControlSelectedEvent(sender, e);
            }
        }

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
        #endregion

        #region Panel事件
        /// <summary>
        /// 鼠标点击Panel事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            STPanel pagePanel = sender as STPanel;
            this.PrePoint = e.Location;
            this.Focus();
            this.IsBoxControl = false;

            GetContainsPointNodes(e.Location);

            Type type = STPage.ToAddControl;
            if (null != type) // 添加控件
            {
                object obj = type.Assembly.CreateInstance(type.FullName);
                ViewNode node = obj as ViewNode; // 要添加的控件

                GroupBoxNode gbNode = GetTopGroupBoxNode();
                if (null != gbNode)
                {
                    /* GroupBox中添加控件 */
                    this.cqp.ExecuteCommand(new TreeNodeAdd(gbNode.TreeView, gbNode, node, -1));
                    node.X = e.Location.X - gbNode.LocationInPage.X;
                    node.Y = e.Location.Y - gbNode.LocationInPage.Y;
                    gbNode.Expand();
                }
                else
                {
                    /* Page中添加控件 */
                    this.cqp.ExecuteCommand(new TreeNodeAdd(this.node.TreeView, this.node, node, -1));
                    node.X = e.Location.X;
                    node.Y = e.Location.Y;
                    this.node.Expand();
                }

                /* 添加到选中的控件列表中 */
                ClearSelectedNodes();
                AddSelectedNode(node);
                this.CurrentSelectedControl = node;
                node.State = UIEditor.Entity.ViewNode.ControlState.Normal;

                ControlSelectedEventNotify(node, EventArgs.Empty);
                PageChangedEventNotify(node, EventArgs.Empty);

                STPage.ToAddControl = null;
                this.Cursor = Cursors.Default;
            }
            else
            {
                bool SelectNode = true;
                foreach (ViewNode node in this.SelectedNodes)
                {
                    if (node.FrameBoundContainsPoint(e.Location)) // 调节大小
                    {
                        SelectNode = false;

                        node.PreBound = node.FactRect;
                        node.State = UIEditor.Entity.ViewNode.ControlState.ChangeSize;

                        node.MouseDown(e);

                        this.CurrentSelectedControl = node;

                        break;
                    }
                    else if (node.MobileRect.Contains(e.Location)) // 移动控件
                    {
                        SelectNode = false;

                        foreach (ViewNode n in this.SelectedNodes)
                        {
                            n.State = UIEditor.Entity.ViewNode.ControlState.Down;

                            n.MouseDown(e);
                        }

                        this.CurrentSelectedControl = node;
                        break;
                    }

                    if (node.VisibleRectInPage.Contains(e.Location)) // 点击了已选中的控件
                    {
                        ViewNode topestNode = GetTopNode();
                        if (node != topestNode) // 点击的是内部的子控件
                        {
                            if (!CtrlDown)
                            {
                                ClearSelectedNodes();
                            }

                            /* 添加到选中的控件列表中 */
                            AddSelectedNode(topestNode);
                            topestNode.MouseDown(e);

                            if (MyConst.Controls.KnxGroupBoxType == topestNode.Name)
                            {
                                topestNode.State = UIEditor.Entity.ViewNode.ControlState.Normal;
                            }
                            else
                            {
                                topestNode.State = UIEditor.Entity.ViewNode.ControlState.Down;
                            }

                            this.CurrentSelectedControl = topestNode;
                        }
                        else if (MyConst.Controls.KnxGroupBoxType != node.Name)
                        {
                            SelectNode = false;

                            if (CtrlDown)
                            {
                                node.Deselected();
                                this.SelectedNodes.Remove(node);
                            }
                            else
                            {
                                //SelectNode = false;

                                foreach (ViewNode n in this.SelectedNodes)
                                {
                                    n.State = UIEditor.Entity.ViewNode.ControlState.Down;

                                    n.MouseDown(e);
                                }

                                this.CurrentSelectedControl = node;
                            }
                        }
                        else if (MyConst.Controls.KnxGroupBoxType == node.Name)
                        {
                            if (CtrlDown)
                            {
                                SelectNode = false;

                                node.Deselected();
                                this.SelectedNodes.Remove(node);
                            }
                        }

                        break;
                    }
                }

                if (SelectNode)
                {
                    if (!this.CtrlDown)
                    {
                        ClearSelectedNodes();
                    }

                    this.Refresh();

                    ViewNode topestNode = GetTopNode();
                    if (this.node != topestNode) // 选中当前页面下的控件
                    {
                        /* 添加到选中的控件列表中 */
                        AddSelectedNode(topestNode);
                        topestNode.MouseDown(e);
                        topestNode.State = UIEditor.Entity.ViewNode.ControlState.Down;
                        this.CurrentSelectedControl = topestNode;

                        if (MyConst.Controls.KnxGroupBoxType == topestNode.Name)
                        {
                            this.StartPoint = e.Location;
                            this.IsBoxControl = true;
                            this.ParentControl = topestNode;

                            topestNode.State = UIEditor.Entity.ViewNode.ControlState.Normal;
                        }
                    }
                    else // 根据鼠标轨迹绘制矩形框---多选
                    {
                        this.StartPoint = e.Location;
                        this.IsBoxControl = true;
                        this.ParentControl = this.node;
                    }
                }
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.Equals(this.PrePoint))
            {
                return;
            }

            if (MouseButtons.Left == e.Button) // 鼠标左键按下
            {
                int dx = e.Location.X - this.PrePoint.X;
                int dy = e.Location.Y - this.PrePoint.Y;
                List<ViewNode> newNodes = new List<ViewNode>(); // 复制的控件列表
                bool MoveControl = false;
                bool ChangeSize = false;

                foreach (ViewNode node in this.SelectedNodes)
                {
                    ViewNode n = node;
                    if (UIEditor.Entity.ViewNode.ControlState.Down == node.State)
                    {
                        if (this.CtrlDown) // 复制控件。按下左侧Ctrl，移动鼠标
                        {
                            node.State = UIEditor.Entity.ViewNode.ControlState.Normal;

                            ViewNode copyNode = node.Clone() as ViewNode;
                            this.cqp.ExecuteCommand(new TreeNodeAdd(node.TreeView, node.Parent, copyNode, node.Index + 1));
                            n = copyNode;
                            newNodes.Add(copyNode);
                        }

                        n.State = UIEditor.Entity.ViewNode.ControlState.Move;
                        n.PreLocation = new Point(n.X, n.Y);
                        n.PrePoint = e.Location;
                    }

                    if (UIEditor.Entity.ViewNode.ControlState.Move == n.State) // 控件处于移动状态
                    {
                        n.X += dx;
                        n.Y += dy;

                        MoveControl = true;
                    }
                    else if (UIEditor.Entity.ViewNode.ControlState.ChangeSize == n.State) // 控件处于调节大小状态
                    {
                        Rectangle rect = n.FrameChanged(dx, dy);
                        n.X = rect.X;
                        n.Y = rect.Y;
                        n.Width = rect.Width;
                        n.Height = rect.Height;

                        ChangeSize = true;
                    }
                }

                if (newNodes.Count > 0) // 替换选中的控件为复制生成的控件
                {
                    ClearSelectedNodes();
                    foreach (ViewNode node in newNodes)
                    {
                        AddSelectedNode(node);
                    }
                }

                if (MoveControl)
                {
                    MoveOutIn(this.CurrentSelectedControl, e.Location);

                    List<Line> lines = new List<Line>();
                    CheckMargin(lines);
                    CheckAlignment(lines);
                    this.Refresh();
                    DrawLines(lines);

                    PageChangedEventNotify(this.CurrentSelectedControl, EventArgs.Empty);
                }
                else if (ChangeSize)
                {
                    List<Line> lines = new List<Line>();
                    CheckMargin(lines);
                    CheckAlignment(lines);
                    this.Refresh();
                    DrawLines(lines);

                    PageChangedEventNotify(this.CurrentSelectedControl, EventArgs.Empty);
                }

                if (this.IsBoxControl) // 根据鼠标运动轨迹绘制矩形边框---多选
                {
                    if (e.Location.X - this.StartPoint.X >= 0)
                    {
                        if (e.Location.Y - this.StartPoint.Y >= 0) // 第一象限
                        {
                            this.RectFrame = new Rectangle(this.StartPoint, new Size(e.Location.X - this.StartPoint.X, e.Location.Y - this.StartPoint.Y));
                        }
                        else // 第四象限
                        {
                            this.RectFrame = new Rectangle(new Point(this.StartPoint.X, e.Location.Y), new Size(e.Location.X - this.StartPoint.X, this.StartPoint.Y - e.Location.Y));
                        }
                    }
                    else
                    {
                        if (e.Location.Y - this.RectFrame.Y >= 0) // 第二象限
                        {
                            this.RectFrame = new Rectangle(new Point(e.Location.X, this.StartPoint.Y), new Size(this.StartPoint.X - e.Location.X, e.Location.Y - this.StartPoint.Y));
                        }
                        else // 第三象限
                        {
                            this.RectFrame = new Rectangle(e.Location, new Size(this.StartPoint.X - e.Location.X, this.StartPoint.Y - e.Location.Y));
                        }
                    }

                    ClearSelectedNodes();
                    BoxControl(this.ParentControl);
                    this.Refresh();
                    DrawRect(this.RectFrame);
                }
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

            SetCursor();
            this.PrePoint = e.Location;
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.SelectedNodes.Count > 0)
            {
                foreach (ViewNode node in this.SelectedNodes)
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        this.cqp.ExecuteCommand(new NodePropertyLocation(node, node.PreLocation, new Point(node.X, node.Y)));
                    }
                    else if (UIEditor.Entity.ViewNode.ControlState.ChangeSize == node.State)
                    {
                        this.cqp.ExecuteCommand(new NodePropertyBound(node, node.PreBound, node.FactRect));
                    }
                    node.State = UIEditor.Entity.ViewNode.ControlState.Up;

                    ControlSelectedEventNotify(node, EventArgs.Empty);
                }
            }
            else
            {
                ControlSelectedEventNotify(this.node, EventArgs.Empty);
            }

            this.Refresh();
        }
        #endregion

        private void ClearSelectedNodes()
        {
            foreach (ViewNode node in this.SelectedNodes)
            {
                node.Deselected();
            }

            this.SelectedNodes.Clear();
        }

        private void AddSelectedNode(ViewNode node)
        {
            node.IsSelected = true;
            node.ParCompX = 0;
            node.ParCompY = 0;
            node.ParNode = null;
            node.GapCompX = 0;
            node.GapCompY = 0;
            node.GapNode = null;
            node.AliCompX = 0;
            node.AliCompY = 0;
            node.AliNode = null;
            this.SelectedNodes.Add(node);
        }

        /// <summary>
        /// 设置鼠标的形状
        /// </summary>
        private void SetCursor()
        {
            if (null != STPage.ToAddControl)
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

        /// <summary>
        /// 从父控件移出、移到新的父控件
        /// </summary>
        /// <param name="cNode"></param>
        /// <param name="e"></param>
        private void MoveOutIn(ViewNode cNode, Point p)
        {
            GetContainsPointNodes(p);

            //Point locationInPage = cNode.LocationInPage;
            ViewNode pNode = cNode.Parent as ViewNode;
            if (pNode.VisibleRectInPage.Contains(p)) // 拖动子控件的鼠标位置还在子控件的父控件中
            {
                Console.WriteLine("MoveOutIn:" + pNode.VisibleRectInPage + " " + p);
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
                    foreach (ViewNode node in this.SelectedNodes)
                    {
                        Point locationInPage = node.LocationInPage;
                        this.cqp.ExecuteCommand(new TreeNodeRemove(node.TreeView, node));

                        this.cqp.ExecuteCommand(new TreeNodeAdd(topGroupBoxNode.TreeView, topGroupBoxNode, node, -1));
                        Point loc = ViewNode.GetLocationInParent(topGroupBoxNode.LocationInPage, locationInPage);
                        node.X = loc.X;
                        node.Y = loc.Y;
                    }
                    topGroupBoxNode.Expand();
                }
            }
            else
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
                        foreach (ViewNode node in this.SelectedNodes)
                        {
                            Point locationInPage = node.LocationInPage;
                            this.cqp.ExecuteCommand(new TreeNodeRemove(node.TreeView, node));

                            this.cqp.ExecuteCommand(new TreeNodeAdd(topGroupBoxNode.TreeView, topGroupBoxNode, node, -1));
                            Point loc = ViewNode.GetLocationInParent(topGroupBoxNode.LocationInPage, locationInPage);
                            node.X = loc.X;
                            node.Y = loc.Y;
                        }
                        topGroupBoxNode.Expand();
                    }
                }
                else
                {
                    foreach (ViewNode node in this.SelectedNodes)
                    {
                        Point locationInPage = node.LocationInPage;
                        this.cqp.ExecuteCommand(new TreeNodeRemove(node.TreeView, node));

                        this.cqp.ExecuteCommand(new TreeNodeAdd(this.node.TreeView, this.node, node, -1));
                        Point loc = ViewNode.GetLocationInParent(new Point(this.node.X, this.node.Y), locationInPage);
                        node.X = loc.X;
                        node.Y = loc.Y;
                    }
                    this.node.Expand();
                }
            }
        }

        /// <summary>
        /// 外边距比对。与父控件进行外边距比对；与处于同一父控件下的兄弟控件进行外边距比对
        /// </summary>
        private void CheckMargin(List<Line> Lines)
        {
            foreach (ViewNode node in this.SelectedNodes) // 遍历已选子控件列表
            {
                Rectangle nodeRect = node.RectInPage;
                ViewNode pNode = node.Parent as ViewNode;
                Rectangle pNodeRect = pNode.RectInPage;

                int disLeft = nodeRect.Left - pNodeRect.Left; // 与父视图左边距
                int compensatedLeft = disLeft + node.ParCompX;
                int disRight = nodeRect.Right - pNodeRect.Right; // 与父视图右边距
                int compensatedRight = disRight + node.ParCompX;
                if (compensatedLeft >= 20 && compensatedLeft <= 25) // 与父视图左侧距离在[18,22]之间时
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                    {
                        node.X = node.GetLocationXFromLocationXInPage(pNodeRect.Left + 20);
                        node.ParCompX += disLeft - 20;
                    }

                    int y = nodeRect.Top + nodeRect.Height / 2;
                    Lines.Add(new Line(new Point(node.RectInPage.Left, y), new Point(pNodeRect.Left, y)));
                }
                else if (compensatedLeft <= 5) // 与父视图左侧距离在(-∞, 5)之间时
                {
                    if ((MyConst.View.KnxPageType == pNode.Name) ||  // 父视图为KNXPage
                        ((MyConst.Controls.KnxGroupBoxType == pNode.Name) && (compensatedLeft >= 0))) // 父视图为KNXGroupBox
                    {
                        if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                        {
                            node.X = node.GetLocationXFromLocationXInPage(pNodeRect.Left);
                            node.ParCompX += disLeft;
                        }

                        Lines.Add(new Line(new Point(pNodeRect.Left, pNodeRect.Bottom), pNodeRect.Location));
                    }
                }
                else if (compensatedRight >= -25 && compensatedRight <= -20) // 与父视图右侧距离在[18,22]之间时
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                    {
                        node.X = node.GetLocationXFromLocationRightInPage(pNodeRect.Right - 20);
                        node.ParCompX += disRight + 20;
                    }

                    int y = nodeRect.Top + nodeRect.Height / 2;
                    Lines.Add(new Line(new Point(node.RectInPage.Right, y), new Point(pNodeRect.Right, y)));
                }
                else if (compensatedRight >= -5) // 与父视图右侧距离在(-∞, 5)之间时
                {
                    if ((MyConst.View.KnxPageType == pNode.Name) ||  // 父视图为KNXPage
                        ((MyConst.Controls.KnxGroupBoxType == pNode.Name) && (compensatedRight <= 0))) // 父视图为KNXGroupBox
                    {
                        if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                        {
                            node.X = node.GetLocationXFromLocationRightInPage(pNodeRect.Right);
                            node.ParCompX += disRight;
                        }

                        Lines.Add(new Line(new Point(pNodeRect.Right, pNodeRect.Top), new Point(pNodeRect.Right, pNodeRect.Bottom)));
                    }
                }
                else if (0 != node.ParCompX)
                {
                    node.X += node.ParCompX;
                    node.ParCompX = 0;
                }

                int disTop = nodeRect.Top - pNodeRect.Top; // 与父视图上边距
                int compensatedTop = disTop + node.ParCompY;
                int disBottom = nodeRect.Bottom - pNodeRect.Bottom; // 与父视图下边距
                int compensatedBottom = disBottom + node.ParCompY;
                if (compensatedTop >= 20 && compensatedTop <= 25) // 与父视图顶部距离在[18,22]之间时
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                    {
                        node.Y = node.GetLocationYFromLocationYInPage(pNodeRect.Top + 20);
                        node.ParCompY += disTop - 20;
                    }

                    int x = nodeRect.Left + nodeRect.Width / 2;
                    Lines.Add(new Line(new Point(x, node.RectInPage.Top), new Point(x, pNodeRect.Top)));
                }
                else if (compensatedTop <= 5) // 与父视图顶部距离在(-∞, 5)之间时
                {
                    if ((MyConst.View.KnxPageType == pNode.Name) ||  // 父视图为KNXPage
                        ((MyConst.Controls.KnxGroupBoxType == pNode.Name) && (compensatedTop >= 0))) // 父视图为KNXGroupBox
                    {
                        if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                        {
                            node.Y = node.GetLocationYFromLocationYInPage(pNodeRect.Top);
                            node.ParCompY += disTop;
                        }

                        Lines.Add(new Line(pNodeRect.Location, new Point(pNodeRect.Right, pNodeRect.Top)));
                    }
                }
                else if (compensatedBottom >= -25 && compensatedBottom <= -20) // 与父视图底部距离在[18,22]之间时
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                    {
                        node.Y = node.GetLocationYFromLocationBottomInPage(pNodeRect.Bottom - 20);
                        node.ParCompY += disBottom + 20;
                    }

                    int x = nodeRect.Left + nodeRect.Width / 2;
                    Lines.Add(new Line(new Point(x, node.RectInPage.Bottom), new Point(x, pNodeRect.Bottom)));
                }
                else if (compensatedBottom >= -5) // 与父视图底部距离在(-∞, 5)之间时
                {
                    if ((MyConst.View.KnxPageType == pNode.Name) ||  // 父视图为KNXPage
                        ((MyConst.Controls.KnxGroupBoxType == pNode.Name) && (compensatedBottom <= 0))) // 父视图为KNXGroupBox
                    {
                        if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 比对控件处于移动状态时，才产生吸附效应
                        {
                            node.Y = node.GetLocationYFromLocationBottomInPage(pNodeRect.Bottom);
                            node.ParCompY += disBottom;
                        }

                        Lines.Add(new Line(new Point(pNodeRect.Right, pNodeRect.Bottom), new Point(pNodeRect.Left, pNodeRect.Bottom)));
                    }
                }
                else if (0 != node.ParCompY)
                {
                    node.Y += node.ParCompY;
                    node.ParCompY = 0;
                }

                /* 与同一父视图下兄弟控件的边距 */
                foreach (ViewNode vNode in pNode.Nodes)
                {
                    if (/*vNode == node || */vNode.IsSelected)
                    {
                        continue;
                    }

                    Rectangle vNodeRect = vNode.RectInPage;

                    disLeft = nodeRect.Left - vNodeRect.Right; // 左边距
                    compensatedLeft = disLeft + node.GapCompX;
                    disRight = nodeRect.Right - vNodeRect.Left; // 右边距
                    compensatedRight = disRight + node.GapCompX;

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
                    if ((compensatedLeft >= 20) && (compensatedLeft <= 25)) // 两控件的水平距离
                    {
                        if (height > 0) // 两控件的水平投影有重合
                        {
                            if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                            {
                                node.X = node.GetLocationXFromLocationXInPage(vNodeRect.Right + 20);
                                node.GapCompX += disLeft - 20;
                                node.GapNode = vNode;
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

                            Lines.Add(new Line(new Point(node.RectInPage.Left, y), new Point(vNodeRect.Right, y)));

                            break;
                        }
                    }
                    else if ((compensatedLeft >= 0) && (compensatedLeft <= 5)) // 两控件的水平距离
                    {
                        if (height > 0) // 两控件的水平投影有重合
                        {
                            if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                            {
                                node.X = node.GetLocationXFromLocationXInPage(vNodeRect.Right);
                                node.GapCompX += disLeft;
                                node.GapNode = vNode;
                            }

                            Lines.Add(new Line(new Point(vNodeRect.Right, vNodeRect.Top), new Point(vNodeRect.Right, vNodeRect.Bottom)));
                            Lines.Add(new Line(node.RectInPage.Location, new Point(node.RectInPage.Left, nodeRect.Bottom)));

                            break;
                        }
                    }
                    else if ((compensatedRight >= -25) && (compensatedRight <= -20)) // 两控件的水平距离
                    {
                        if (height > 0) // 两控件的水平投影有重合
                        {
                            if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                            {
                                node.X = node.GetLocationXFromLocationRightInPage(vNodeRect.Left - 20);
                                node.GapCompX += disRight + 20;
                                node.GapNode = vNode;
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
                            Lines.Add(new Line(new Point(node.RectInPage.Right, y), new Point(vNodeRect.Left, y)));

                            break;
                        }
                    }
                    else if ((compensatedRight >= -5) && (compensatedRight < 0)) // 两控件的水平距离
                    {
                        if (height > 0) // 两控件的水平投影有重合
                        {
                            if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                            {
                                node.X = node.GetLocationXFromLocationRightInPage(vNodeRect.Left);
                                node.GapCompX += disRight;
                                node.GapNode = vNode;
                            }

                            Lines.Add(new Line(vNodeRect.Location, new Point(vNodeRect.Left, vNodeRect.Bottom)));
                            Lines.Add(new Line(new Point(node.RectInPage.Right, nodeRect.Top), new Point(node.RectInPage.Right, nodeRect.Bottom)));

                            break;
                        }
                    }
                    else if ((0 != node.GapCompX) && (node.GapNode.Id == vNode.Id))
                    {
                        node.X += node.GapCompX;
                        node.GapCompX = 0;

                        break;
                    }

                    disTop = nodeRect.Top - vNodeRect.Bottom; // 上边距
                    compensatedTop = disTop + node.GapCompY;
                    disBottom = nodeRect.Bottom - vNodeRect.Top; // 下边距
                    compensatedBottom = disBottom + node.GapCompY;

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
                    if ((compensatedTop >= 20) && (compensatedTop <= 25)) // 两控件的垂直距离
                    {
                        if (width > 0) // 两控件垂直投影有重合
                        {
                            if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 已选控件处于移动状态时才产生吸附效应
                            {
                                node.Y = node.GetLocationYFromLocationYInPage(vNodeRect.Bottom + 20);
                                node.GapCompY += disTop - 20;
                                node.GapNode = vNode;
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
                            Lines.Add(new Line(new Point(x, node.RectInPage.Top), new Point(x, vNodeRect.Bottom)));

                            break;
                        }
                    }
                    else if ((compensatedTop >= 0) && (compensatedTop <= 5)) // 两控件的垂直距离
                    {
                        if (width > 0) // 两控件垂直投影有重合
                        {
                            if (UIEditor.Entity.ViewNode.ControlState.Move == node.State) // 已选控件处于移动状态时才产生吸附效应
                            {
                                node.Y = node.GetLocationYFromLocationYInPage(vNodeRect.Bottom);
                                node.GapCompY += disTop;
                                node.GapNode = vNode;
                            }

                            Lines.Add(new Line(new Point(vNodeRect.Left, vNodeRect.Bottom), new Point(vNodeRect.Right, vNodeRect.Bottom)));
                            Lines.Add(new Line(node.RectInPage.Location, new Point(nodeRect.Right, node.RectInPage.Top)));

                            break;
                        }
                    }
                    else if ((compensatedBottom >= -25) && (compensatedBottom <= -20)) // 两控件的垂直距离
                    {
                        if (width > 0) // 两控件垂直投影有重合
                        {
                            if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                            {
                                node.Y = node.GetLocationYFromLocationBottomInPage(vNodeRect.Top - 20);
                                node.GapCompY += disBottom + 20;
                                node.GapNode = vNode;
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
                            Lines.Add(new Line(new Point(x, node.RectInPage.Bottom), new Point(x, vNodeRect.Top)));

                            break;
                        }
                    }
                    else if ((compensatedBottom >= -5) && (compensatedBottom <= 0)) // 两控件的垂直距离
                    {
                        if (width > 0) // 两控件垂直投影有重合
                        {
                            if (UIEditor.Entity.ViewNode.ControlState.Move == node.State)
                            {
                                node.Y = node.GetLocationYFromLocationBottomInPage(vNodeRect.Top);
                                node.GapCompY += disBottom;
                                node.GapNode = vNode;
                            }

                            Lines.Add(new Line(vNodeRect.Location, new Point(vNodeRect.Right, vNodeRect.Top)));
                            Lines.Add(new Line(new Point(nodeRect.Left, node.RectInPage.Bottom), new Point(nodeRect.Right, node.RectInPage.Bottom)));

                            break;
                        }
                    }
                    else if ((0 != node.GapCompY) && (node.GapNode.Id == vNode.Id))
                    {
                        node.Y += node.GapCompY;
                        node.GapCompY = 0;

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 边沿对齐。遍历node下所有子孙控件，让已选控件与他们进行边沿比对
        /// </summary>
        /// <param name="node"></param>
        private void CheckAlignment(List<Line> Lines)
        {
            foreach (ViewNode sNode in this.SelectedNodes) // 遍历已选控件
            {
                ControlsAlignment(this.node, sNode, Lines);
            }
        }

        private bool ControlsAlignment(ViewNode node, ViewNode sNode, List<Line> Lines)
        {
            Rectangle sNodeRect = sNode.RectInPage;
            bool alreadyAlign = false;

            foreach (ViewNode vNode in node.Nodes) // 遍历node下的子控件
            {
                Rectangle vNodeRect = vNode.RectInPage;

                /* 不与自身、父控件进行边沿比对。与父控件边沿比对请看 ↑CheckMargin() */
                if (/*vNode == sNode*/vNode.IsSelected || vNode == sNode.Parent)
                {
                    continue;
                }

                int sideLeft = sNodeRect.Left - vNodeRect.Left; // 左边沿比对
                int compensateLeft = sideLeft + sNode.AliCompX;
                int sideRight = sNodeRect.Right - vNodeRect.Right; // 右边沿比对
                int compensateRight = sideRight + sNode.AliCompX;
                if ((compensateLeft >= 0) && (compensateLeft <= 5)) // 比对控件与被比对控件的左侧距离差
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == sNode.State) // 仅控件处于移动状态时才产生吸附效应
                    {
                        sNode.X = sNode.GetLocationXFromLocationXInPage(vNodeRect.Left);
                        sNode.AliCompX += sideLeft;
                        sNode.AliNode = vNode;
                    }

                    if (sNodeRect.Top > vNodeRect.Top) // 比对控件在被比对控件的上侧时
                    {
                        Lines.Add(new Line(new Point(vNodeRect.Left - 1, vNodeRect.Top), new Point(sNode.RectInPage.Left - 1, sNodeRect.Bottom)));
                    }
                    else
                    {
                        Lines.Add(new Line(new Point(sNode.RectInPage.Left - 1, sNodeRect.Top), new Point(vNodeRect.Left - 1, vNodeRect.Bottom)));
                    }

                    alreadyAlign = true;
                    break;
                }
                else if ((compensateRight >= -5) && (compensateRight <= 0)) // 比对控件与被比对控件的右侧距离差
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == sNode.State) // 仅控件处于移动状态时才产生吸附效应
                    {
                        sNode.X = sNode.GetLocationXFromLocationRightInPage(vNodeRect.Right);
                        sNode.AliCompX += sideRight;
                        sNode.AliNode = vNode;
                    }

                    if (sNodeRect.Top > vNodeRect.Top) // 比对控件在被比对控件的上侧时
                    {
                        Lines.Add(new Line(new Point(vNodeRect.Right, vNodeRect.Top), new Point(sNode.RectInPage.Right, sNodeRect.Bottom)));
                    }
                    else
                    {
                        Lines.Add(new Line(new Point(sNode.RectInPage.Right, sNodeRect.Top), new Point(vNodeRect.Right, vNodeRect.Bottom)));
                    }

                    alreadyAlign = true;
                    break;
                }
                else if ((0 != sNode.AliCompX) && (sNode.AliNode.Id == vNode.Id))
                {
                    sNode.X += sNode.AliCompX;
                    sNode.AliCompX = 0;

                    alreadyAlign = true;
                    break;
                }

                int sideTop = sNodeRect.Top - vNodeRect.Top; // 上边沿比对
                int compensateTop = sideTop + sNode.AliCompY;
                int sideBottom = sNodeRect.Bottom - vNodeRect.Bottom; // 下边沿比对
                int compensateBottom = sideBottom + sNode.AliCompY;
                if ((compensateTop >= 0) && (compensateTop <= 5))   // 比对控件与被比对控件的顶部距离差在[0,5)之间时
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == sNode.State) // 仅控件处于移动状态时才产生吸附效应
                    {
                        sNode.Y = sNode.GetLocationYFromLocationYInPage(vNodeRect.Top);
                        sNode.AliCompY += sideTop;
                        sNode.AliNode = vNode;
                    }

                    if (sNodeRect.Left > vNodeRect.Left) // 比对控件在被比对控件的左侧
                    {
                        Lines.Add(new Line(new Point(vNodeRect.Left, vNodeRect.Top - 1), new Point(sNodeRect.Right, sNode.RectInPage.Top - 1)));
                    }
                    else // 比对控件在被比对控件的右侧或两者的起始点重合时
                    {
                        Lines.Add(new Line(new Point(sNodeRect.Left, sNode.RectInPage.Top - 1), new Point(vNodeRect.Right, vNodeRect.Top - 1)));
                    }

                    alreadyAlign = true;
                    break;
                }
                else if ((compensateBottom >= -5) && (compensateBottom <= 0)) // 比对控件与被比对控件的底部距离差在[0,5)之间时
                {
                    if (UIEditor.Entity.ViewNode.ControlState.Move == sNode.State)  // 仅控件处于移动状态时才产生吸附效应
                    {
                        sNode.Y = sNode.GetLocationYFromLocationBottomInPage(vNodeRect.Bottom);
                        sNode.AliCompY += sideBottom;
                        sNode.AliNode = vNode;
                    }

                    if (sNodeRect.Left > vNodeRect.Left) // 比对控件在被比对控件的左侧
                    {
                        Lines.Add(new Line(new Point(vNodeRect.Left, vNodeRect.Bottom), new Point(sNodeRect.Right, sNode.RectInPage.Bottom)));
                    }
                    else
                    {
                        Lines.Add(new Line(new Point(sNodeRect.Left, sNode.RectInPage.Bottom), new Point(vNodeRect.Right, vNodeRect.Bottom)));
                    }

                    alreadyAlign = true;
                    break;
                }
                else if ((0 != sNode.AliCompY) && (sNode.AliNode.Id == vNode.Id))
                {
                    sNode.Y += sNode.AliCompY;
                    sNode.AliCompY = 0;

                    alreadyAlign = true;
                    break;
                }

                /* 若被比对控件还包含子控件，则继续与其子控件进行边沿比对 */
                if (vNode.Nodes.Count > 0)
                {
                    alreadyAlign = ControlsAlignment(vNode, sNode, Lines);
                    if (alreadyAlign)
                    {
                        break;
                    }
                }
            }

            return alreadyAlign;
        }

        private void DrawLines(List<Line> Lines)
        {
            if ((null != Lines) && (Lines.Count > 0))
            {
                Graphics g = this.CreateGraphics();
                Pen pen = new Pen(Color.DodgerBlue, 1.8f);

                foreach (Line line in Lines)
                {
                    g.DrawLine(pen, line.Begin, line.End);
                }
            }
        }

        /// <summary>
        /// 绘制从p1到p2的线段
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        private void DrawLine(Point p1, Point p2)
        {
            Graphics g = this.CreateGraphics();

            Pen pen = new Pen(Color.DodgerBlue, 1.8f);
            g.DrawLine(pen, p1, p2);
        }

        /// <summary>
        /// 用虚线绘制矩形rect的边框
        /// </summary>
        /// <param name="rect"></param>
        private void DrawRect(Rectangle rect)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.LightGray, 1.0f);
            pen.DashStyle = DashStyle.Dot;
            g.DrawRectangle(pen, rect);
        }
    }

    class Line
    {
        public Point Begin;
        public Point End;

        public Line(Point begin, Point end)
        {
            this.Begin = begin;
            this.End = end;
        }
    }
}
