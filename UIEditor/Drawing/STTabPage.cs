using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIEditor.CommandManager;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.Drawing
{
    public class STTabPage : TabPage
    {
        #region 常量
        private const int HOR_RULE_WIDTH = 20;
        private const int VER_RULE_WIDTH = 50;
        private const int SPACE = 40;
        private const int SCROLL_BAR_WIDTH = 15;
        #endregion

        #region 属性
        private VScrollBar vScrollBar;
        private ComboBox comboBox;
        private HScrollBar hScrollBar;
        #endregion

        #region 变量
        /// <summary>
        /// 水平标尺，为hRulerContainer的子控件
        /// </summary>
        private Ruler hRuler { get; set; }
        /// <summary>
        /// 垂直标尺，为vRulerContainer的子控件
        /// </summary>
        private Ruler vRuler { get; set; }
        /// <summary>
        /// 水平标尺容器，hRuler的父控件，限定hRuler的位置与大小，父控件为STTabPage
        /// </summary>
        private Panel hRulerContainer { get; set; }
        /// <summary>
        /// 垂直标尺容器，vRuler的父控件，限定vRuler的位置与大小，父控件为STTabPage
        /// </summary>
        private Panel vRulerContainer { get; set; }
        /// <summary>
        /// ContentContainer的父容器，限定其位置与大小，父控件为STTabPage
        /// </summary>
        private Panel MainContCon { get; set; }
        /// <summary>
        /// page的父容器，限定其位置与大小，父控件为MainContCon
        /// </summary>
        private Panel ContentContainer { get; set; }
        /// <summary>
        /// 父控件为ContentContainer
        /// </summary>
        private STPage page { get; set; }
        /// <summary>
        /// 视图缩放比例
        /// </summary>
        private float Ratio { get; set; }
        private bool CtrlPress { get; set; }
        #endregion

        #region 构造函数
        public STTabPage(PageNode pageNode)
            : base()
        {
            InitControl();

            this.Text = pageNode.Text;
            this.Tag = pageNode;

            this.Ratio = 1.0f;
            this.CtrlPress = false;

            this.comboBox.SelectedText = "100%";
            //pageNode.DrawAt(null, this.Ratio);
            pageNode.SetRatio(this.Ratio);

            AddSTPage(new STPage(pageNode, this.Ratio));
        }

        private void InitControl()
        {
            InitializeComponent();

            //减少闪烁  
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        private void InitializeComponent()
        {
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.LargeChange = 1;
            this.vScrollBar.Location = new System.Drawing.Point(0, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 80);
            this.vScrollBar.TabIndex = 0;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            this.vScrollBar.ValueChanged += new EventHandler(this.vScrollBar_ValueChanged);
            // 
            // hScrollBar
            // 
            this.hScrollBar.LargeChange = 1;
            this.hScrollBar.Location = new System.Drawing.Point(0, 0);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(80, 17);
            this.hScrollBar.TabIndex = 0;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            this.hScrollBar.ValueChanged += new EventHandler(this.hScrollBar_ValueChanged);
            // 
            // comboBox
            // 
            this.comboBox.Font = new System.Drawing.Font("宋体", 9F);
            this.comboBox.Items.AddRange(new object[] {
            "30%",
            "50%",
            "80%",
            "100%",
            "150%",
            "200%",
            "250%",
            "300%"});
            this.comboBox.Location = new System.Drawing.Point(0, 0);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(121, 20);
            this.comboBox.TabIndex = 0;
            this.comboBox.Visible = false;
            this.comboBox.SelectedValueChanged += new System.EventHandler(this.comboBox_SelectedValueChanged);
            // 
            // STTabPage
            // 
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.comboBox);
            this.SizeChanged += new System.EventHandler(this.STTabPage_SizeChanged);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.STTabPage_MouseWheel);
            this.ResumeLayout(false);

        }
        #endregion

        #region 覆写函数
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    // 左上角矩形框
        //    Rectangle rctlt = new Rectangle(0, 0, this.hRulerContainer.Left, this.vRulerContainer.Top);
        //    e.Graphics.FillRectangle(new SolidBrush(Color.LightSlateGray), rctlt);

        //    // 右上角矩形框
        //    Rectangle rctrt = new Rectangle(this.hRulerContainer.Right, 0, this.vScrollBar.Width, this.hRulerContainer.Height);
        //    e.Graphics.FillRectangle(new SolidBrush(Color.LightSlateGray), rctrt);

        //    // 右下角矩形框
        //    Rectangle rctrb = new Rectangle(this.hScrollBar.Right, this.vScrollBar.Bottom, this.vScrollBar.Width, this.hScrollBar.Height);
        //    e.Graphics.FillRectangle(new SolidBrush(Color.LightSlateGray), rctrb);

        //    // 左下角矩形框
        //    Rectangle rctlb = new Rectangle(0, this.vRulerContainer.Bottom, this.vRulerContainer.Width, this.hScrollBar.Height);
        //    e.Graphics.FillRectangle(new SolidBrush(Color.LightSlateGray), rctlb);
        //}
        #endregion

        #region 公有方法
        /// <summary>
        /// 标定STTabPage的子控件的位置与大小
        /// </summary>
        /// <param name="cWidth">STPage的宽度（像素）</param>
        /// <param name="cHeight">STPage的高度（像素）</param>
        public void OnMeasure()
        {
            if (null != this.page)
            {
                PageNode pageNode = GetPageNode();
                //int pWidth = pageNode.GetDrawWidth(this.Ratio);
                //int pHeight = pageNode.GetDrawHeight(this.Ratio);
                int pWidth = pageNode.DrawWidth;
                int pHeight = pageNode.DrawHeight;

                this.vScrollBar.Value = 0;
                this.hScrollBar.Value = 0;
                this.vRuler.Location = Point.Empty;
                this.hRuler.Location = Point.Empty;

                if (MyCache.DisplayRuler)
                {
                    this.hRulerContainer.Location = new Point(VER_RULE_WIDTH, 0);
                    this.hRulerContainer.Size = new Size(this.Width - this.hRulerContainer.Left - this.vScrollBar.Width, HOR_RULE_WIDTH);

                    this.vRulerContainer.Location = new Point(0, HOR_RULE_WIDTH);
                    this.vRulerContainer.Size = new Size(VER_RULE_WIDTH, this.Height - this.vRulerContainer.Top - this.hScrollBar.Height);
                }
                else
                {
                    this.hRulerContainer.Location = Point.Empty;
                    this.hRulerContainer.Size = Size.Empty;

                    this.vRulerContainer.Location = Point.Empty;
                    this.vRulerContainer.Size = Size.Empty;
                }

                this.comboBox.Location = new Point(0, this.Height - SCROLL_BAR_WIDTH);
                this.comboBox.Size = new Size(VER_RULE_WIDTH, SCROLL_BAR_WIDTH);

                this.vScrollBar.Location = new Point(this.Width - SCROLL_BAR_WIDTH, this.hRulerContainer.Bottom);
                this.vScrollBar.Size = new Size(SCROLL_BAR_WIDTH, this.Height - this.hRulerContainer.Height - SCROLL_BAR_WIDTH);

                this.hScrollBar.Location = new Point(this.comboBox.Right, this.Height - SCROLL_BAR_WIDTH);
                this.hScrollBar.Size = new Size(this.Width - this.comboBox.Width - SCROLL_BAR_WIDTH, SCROLL_BAR_WIDTH);


                this.MainContCon.Location = new Point(this.vRulerContainer.Right, this.hRulerContainer.Bottom);
                this.MainContCon.Size = new Size(this.Width - this.vRulerContainer.Width - this.vScrollBar.Width,
                    this.Height - this.hRulerContainer.Height - this.hScrollBar.Height);

                this.hRuler.ScaleWidth = pWidth;
                this.vRuler.ScaleHeight = pHeight;

                this.ContentContainer.Size = new Size(this.page.Location.X * 2 + pWidth, this.page.Location.Y * 2 + pHeight);

                int RedundancyWidth = this.MainContCon.Width - this.ContentContainer.Width;
                int RedundancyHeight = this.MainContCon.Height - this.ContentContainer.Height;

                if (RedundancyWidth >= 0)
                {
                    this.ContentContainer.Location = new Point(RedundancyWidth / 2, this.ContentContainer.Location.Y);
                    this.hScrollBar.Enabled = false;
                }
                else
                {
                    this.ContentContainer.Location = new Point(0, this.ContentContainer.Location.Y);
                    this.hScrollBar.Enabled = true;
                }
                this.hRuler.ScaleOffset = this.ContentContainer.Left + this.page.Left;

                if (RedundancyHeight >= 0)
                {
                    this.ContentContainer.Location = new Point(this.ContentContainer.Location.X, RedundancyHeight / 2);
                    this.vScrollBar.Enabled = false;
                }
                else
                {
                    this.ContentContainer.Location = new Point(this.ContentContainer.Location.X, 0);
                    this.vScrollBar.Enabled = true;
                }
                this.vRuler.ScaleOffset = this.ContentContainer.Top + this.page.Top;

                this.hScrollBar.Maximum = this.ContentContainer.Width;
                this.hScrollBar.LargeChange = this.MainContCon.Width;

                this.vScrollBar.Maximum = this.ContentContainer.Height;
                this.vScrollBar.LargeChange = this.MainContCon.Height;
            }
        }

        public PageNode GetPageNode()
        {
            return this.Tag as PageNode;
        }

        /// <summary>
        /// 等比例放大
        /// </summary>
        public void ZoomIn()
        {
            Zoom(this.Ratio * 1.05f);
        }

        /// <summary>
        /// 等比例缩小
        /// </summary>
        public void ZoomOut()
        {
            Zoom(this.Ratio * 0.95f);
        }

        public void SelfAdapter()
        {
            SelfAdaption();
        }

        public void SetViewScale(float ratio)
        {
            Zoom(ratio);
        }

        public float GetViewScale()
        {
            return this.Ratio;
        }

        /// <summary>
        /// 投影子控件到标尺
        /// </summary>
        /// <param name="nodes"></param>
        public void SetSelectedControlProjection(List<ViewNode> nodes)
        {
            this.hRuler.ClearProjection();
            this.vRuler.ClearProjection();

            foreach (ViewNode node in nodes)
            {
                this.hRuler.SetProjection(node.RectInPage.Left, HOR_RULE_WIDTH, node.RectInPage.Right, HOR_RULE_WIDTH);
                this.vRuler.SetProjection(VER_RULE_WIDTH, node.RectInPage.Top, VER_RULE_WIDTH, node.RectInPage.Bottom);
            }

            this.hRuler.Refresh();
            this.vRuler.Refresh();
        }

        public CommandQuene GetCommandQueue()
        {
            return this.page.GetCommandQueue();
        }

        public void SelectControl(ViewNode node)
        {
            this.page.SelectControl(node);
        }

        public void AddControl(ViewNode node)
        {
            this.page.AddControl(node);
        }

        public void RemoveControl(ViewNode node)
        {
            this.page.RemoveControl(node);
        }

        public void PropertyChanged(/*int cWidth, int cHeight*/)
        {
            ////this.UpdateThisPage();
            //this.OnMeasure(cWidth, cHeight);
            //this.page.PagePropertyChanged();
            //RefreshThisTabPage(/*cWidth, cHeight*/);
            //PageNode pageNode = GetPageNode();
            //this.OnMeasure(pageNode.Width, pageNode.Height);
            //this.page.PagePropertyChanged(this.Ratio);
            RefreshThisTabPage(this.Ratio);
        }

        public void ControlPropertyChanged(ViewNode node)
        {
            this.page.ControlPropertyChanged(node);
        }

        public void AddNewControl(Type ControlType)
        {
            //LayerControls.ToAddControl = ControlType;
            this.page.AddNewControl(ControlType);
        }

        public void Save()
        {
            this.page.Save();
        }

        public void AlignLeft()
        {
            this.page.AlignLeft();
        }

        public void AlignRight()
        {
            this.page.AlignRight();
        }

        public void AlignTop()
        {
            this.page.AlignTop();
        }

        public void AlignBottom()
        {
            this.page.AlignBottom();
        }

        public void AlignHorizontalCenter()
        {
            this.page.AlignHorizontalCenter();
        }

        public void AlignVerticalCenter()
        {
            this.page.AlignVerticalCenter();
        }

        public void HorizontalEquidistanceAlignment()
        {
            this.page.HorizontalEquidistanceAlignment();
        }

        public void VerticalEquidistanceAlignment()
        {
            this.page.VerticalEquidistanceAlignment();
        }

        public void WidthAlignment()
        {
            this.page.WidthAlignment();
        }

        public void HeightAlignment()
        {
            this.page.HeightAlignment();
        }

        public void CenterHorizontal()
        {
            this.page.CenterHorizontal();
        }

        public void CenterVertical()
        {
            this.page.CenterVertical();
        }

        public void CutControls()
        {
            this.page.CutControls();
        }

        public void CopyControls()
        {
            this.page.CopyControls();
        }

        public void PasteControls()
        {
            this.page.PasteControls();
        }

        public void KeyDowns(KeyEventArgs e)
        {
            if ((Keys.LControlKey == e.KeyCode) || (Keys.RControlKey == e.KeyCode))
            {
                this.CtrlPress = true;
            }

            this.page.KeyDowns(e);
        }

        public void KeyUps(KeyEventArgs e)
        {
            if ((Keys.LControlKey == e.KeyCode) || (Keys.RControlKey == e.KeyCode))
            {
                this.CtrlPress = false;
            }

            this.page.KeyUps(e);
        }

        public event UIEditor.Drawing.LayerControls.ControlSelectedEventDelegate STTabPageControlSelectedEvent
        {
            add
            {
                this.page.PageBoxControlSelectedEvent += value;
            }
            remove
            {
                this.page.PageBoxControlSelectedEvent -= value;
            }
        }

        public event UIEditor.Drawing.LayerControls.PageChangedEventDelegate STTabPagePageChangedEvent
        {
            add
            {
                this.page.PageBoxPageChangedEvent += value;
            }
            remove
            {
                this.page.PageBoxPageChangedEvent -= value;
            }
        }

        public event UIEditor.Drawing.LayerControls.SelectedControlsIsBrotherhoodEventDelegate STTabPageSelectedControlsIsBrotherhoodEvent
        {
            add
            {
                this.page.PageBoxSelectedControlsIsBrotherhoodEvent += value;
            }
            remove
            {
                this.page.PageBoxSelectedControlsIsBrotherhoodEvent -= value;
            }
        }

        public event UIEditor.Drawing.LayerControls.SelectedControlsMoveEventDelegate STTabPageSelectedControlsMoveEvent
        {
            add
            {
                this.page.PageBoxSelectedControlsMoveEvent += value;
            }
            remove
            {
                this.page.PageBoxSelectedControlsMoveEvent -= value;
            }
        }
        #endregion

        #region 事件
        private void STTabPage_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.CtrlPress)
            {
                if (e.Delta > 0) // 缩小
                {
                    ZoomOut();
                }
                else // 放大
                {
                    ZoomIn();
                }
            }
            else
            {
                if (e.Delta > 0) // 向上
                {
                    SetVScrollBarValue(this.vScrollBar.Value - 10);
                }
                else // 向下
                {
                    SetVScrollBarValue(this.vScrollBar.Value + 10);
                }
            }
        }

        private void STTabPage_SizeChanged(object sender, EventArgs e)
        {
            if (null != this.page)
            {
                //SelfAdaption();
                //PageNode pageNode = GetPageNode(); 
                OnMeasure(/*pageNode.Width, pageNode.Height*/);
            }
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.ContentContainer.Location = new Point(-e.NewValue, this.ContentContainer.Location.Y);
            this.hRuler.Location = new Point(-e.NewValue, this.hRuler.Location.Y);

            this.ContentContainer.Refresh();
            this.hRuler.Refresh();
        }

        private void hScrollBar_ValueChanged(object sender, EventArgs e)
        {
            HScrollBar hsb = sender as HScrollBar;
            int v = hsb.Value;

            this.ContentContainer.Location = new Point(-v, this.ContentContainer.Location.Y);
            this.hRuler.Location = new Point(-v, this.hRuler.Location.Y);

            this.ContentContainer.Refresh();
            this.hRuler.Refresh();
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.ContentContainer.Location = new Point(this.ContentContainer.Location.X, -e.NewValue);
            this.vRuler.Location = new Point(this.vRuler.Location.X, -e.NewValue);

            this.ContentContainer.Refresh();
            this.hRuler.Refresh();
        }

        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            VScrollBar vsb = sender as VScrollBar;
            int v = vsb.Value;

            this.ContentContainer.Location = new Point(this.ContentContainer.Location.X, -v);
            this.vRuler.Location = new Point(this.vRuler.Location.X, -v);

            this.ContentContainer.Refresh();
            this.hRuler.Refresh();
        }

        private void comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string nv = this.comboBox.Text;
            nv = nv.Substring(0, nv.IndexOf("%"));
            int v = int.Parse(nv);

            Zoom((float)v / 100);
        }
        #endregion

        #region 私有方法
        private void AddSTPage(STPage page)
        {
            this.page = page;

            this.hRulerContainer = new Panel();
            this.hRulerContainer.BackColor = Color.Transparent; // hRulerContainer 背景透明
            this.hRuler = new Ruler(this.Ratio);
            this.hRuler.Location = Point.Empty; // hRuler 起始位置为（0,0）
            this.hRuler.ScaleHeight = HOR_RULE_WIDTH; // hRuler 高度为HOR_RULE_WIDTH
            this.hRuler.RuleDirection = UIEditor.Drawing.Ruler.Direction.Horizental; // hRuler 为水平标尺
            this.hRuler.BackColor = Color.Gainsboro; // hRuler 背景色
            this.hRuler.ScaleColor = Color.Black; // hRuler 标尺颜色，包括刻度和数字
            this.hRulerContainer.Controls.Add(hRuler);

            this.vRulerContainer = new Panel();
            this.vRulerContainer.BackColor = Color.Transparent; // vRulerContainer 背景透明
            this.vRuler = new Ruler(this.Ratio);
            this.vRuler.Location = Point.Empty; // vRuler 起始位置(0,0)
            this.vRuler.ScaleWidth = VER_RULE_WIDTH; // vRuler 宽度VER_RULE_WIDTH
            this.vRuler.RuleDirection = UIEditor.Drawing.Ruler.Direction.Vertical; // vRuler 为垂直标尺
            this.vRuler.BackColor = Color.Gainsboro; // vRuler 背景色
            this.vRuler.ScaleColor = Color.Black; // vRuler 标尺颜色，包括刻度和数字
            this.vRulerContainer.Controls.Add(vRuler);

            this.MainContCon = new Panel();
            this.MainContCon.BackColor = Color.Transparent; // MainContCon 背景透明
            this.ContentContainer = new Panel();
            this.ContentContainer.Location = Point.Empty; // ContentContainer 起始位置（0,0）
            this.ContentContainer.BackColor = Color.Transparent; // ContentContainer 背景色
            page.Location = new Point(SPACE, SPACE); // page的起始位置（SPACE,SPACE)
            this.ContentContainer.Controls.Add(page);
            this.MainContCon.Controls.Add(this.ContentContainer);

            //PageNode pageNode = GetPageNode();
            OnMeasure(/*pageNode.Width, pageNode.Height*/);

            this.Controls.Add(this.MainContCon);
            this.Controls.Add(this.hRulerContainer);
            this.Controls.Add(this.vRulerContainer);

            //SelfAdaption();
        }

        private void RefreshThisTabPage(float ratio)
        {
            //PageNode pageNode = GetPageNode();
            //int pWidth = pageNode.GetDrawWidth(ratio);
            //int pHeight = pageNode.GetDrawHeight(ratio);

            this.page.PagePropertyChanged(ratio);
            this.OnMeasure(/*pWidth, pHeight*/);

        }

        private void Zoom(float ratio/*int cWidth, int cHeight*/)
        {
            if (ratio > 3.0f)
            {
                ratio = 3.0f;
            }
            else if (ratio < 0.3f)
            {
                ratio = 0.3f;
            }

            this.Ratio = ratio;

            int v = (int)(this.Ratio * 100);
            this.comboBox.Text = v.ToString() + "%";

            PageNode pageNode = GetPageNode();
            pageNode.SetRatio(this.Ratio);

            RefreshThisTabPage(this.Ratio);

            this.hRuler.Zoom(this.Ratio);
            this.vRuler.Zoom(this.Ratio);
        }

        private void SelfAdaption()
        {
            if (MyCache.DisplayRuler)
            {
                this.hRulerContainer.Location = new Point(VER_RULE_WIDTH, 0);
                this.hRulerContainer.Size = new Size(this.Width - this.hRulerContainer.Left - this.vScrollBar.Width, HOR_RULE_WIDTH);

                this.vRulerContainer.Location = new Point(0, HOR_RULE_WIDTH);
                this.vRulerContainer.Size = new Size(VER_RULE_WIDTH, this.Height - this.vRulerContainer.Top - this.hScrollBar.Height);
            }
            else
            {
                this.hRulerContainer.Location = Point.Empty;
                this.hRulerContainer.Size = Size.Empty;

                this.vRulerContainer.Location = Point.Empty;
                this.vRulerContainer.Size = Size.Empty;
            }

            //this.comboBox.Location = new Point(0, this.Height - SCROLL_BAR_WIDTH);
            //this.comboBox.Size = new Size(VER_RULE_WIDTH, SCROLL_BAR_WIDTH);

            this.vScrollBar.Location = new Point(this.Width - SCROLL_BAR_WIDTH, this.hRulerContainer.Bottom);
            this.vScrollBar.Size = new Size(SCROLL_BAR_WIDTH, this.Height - this.hRulerContainer.Height - SCROLL_BAR_WIDTH);

            this.hScrollBar.Location = new Point(/*this.comboBox.Right*/this.vRulerContainer.Right, this.Height - SCROLL_BAR_WIDTH);
            this.hScrollBar.Size = new Size(this.Width - /*this.comboBox.Width*/this.vRulerContainer.Width - SCROLL_BAR_WIDTH, SCROLL_BAR_WIDTH);


            this.MainContCon.Location = new Point(this.vRulerContainer.Right, this.hRulerContainer.Bottom);
            this.MainContCon.Size = new Size(this.Width - this.vRulerContainer.Width - this.vScrollBar.Width,
                this.Height - this.hRulerContainer.Height - this.hScrollBar.Height);

            int pageWidth = this.MainContCon.Width - 2 * this.page.Location.X;
            int pageHeight = this.MainContCon.Height - 2 * this.page.Location.Y;

            PageNode pageNode = GetPageNode();
            //float ratioWidth = (float)pageWidth / (float)pageNode.Width;
            //float ratioHeight = (float)pageHeight / (float)pageNode.Height;
            float ratioWidth = (float)pageWidth / (float)pageNode.Size.Width;
            float ratioHeight = (float)pageHeight / (float)pageNode.Size.Height;

            //this.Ratio = ratioWidth > ratioHeight ? ratioHeight : ratioWidth;

            Zoom(ratioWidth > ratioHeight ? ratioHeight : ratioWidth);
        }

        private void SetVScrollBarValue(int value)
        {
            if (this.vScrollBar.Maximum > this.vScrollBar.LargeChange)
            {
                int range = this.vScrollBar.Maximum - this.vScrollBar.LargeChange;
                if (value > range)
                {
                    value = range;
                }
                else if (value < this.vScrollBar.Minimum)
                {
                    value = this.vScrollBar.Minimum;
                }

                this.vScrollBar.Value = value;
            }
        }
        #endregion
    }
}
