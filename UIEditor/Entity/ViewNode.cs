using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using UIEditor.Drawing;
using UIEditor.Component;
using Structure;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using UIEditor.PropertyGridEditor;
using UIEditor.UserClass;
using Utils;

namespace UIEditor.Entity
{
    /// <summary>
    /// 所有树上面添加元素的基础类，主要分配ID
    /// </summary>
    [Serializable]
    public abstract class ViewNode : TreeNode, ISerializable
    {
        #region 常量
        private const int ControlMinWidth = 20;
        private const int ControlMinHeight = 20;
        #endregion

        #region 属性
        /// <summary>
        /// 控件的唯一标识
        /// </summary>
        [BrowsableAttribute(true),
        ReadOnlyAttribute(true)]
        public int Id { get; set; }

        public string Title { get; set; }

        [EditorAttribute(typeof(PropertyGridSTFontEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(STFontConverter))]
        public STFont TitleFont { get; set; }

        /// <summary>
        /// 新增于2.7.1
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// 新增于2.7.1
        /// </summary>
        private Size _Size;
        public Size Size
        {
            get
            {
                return _Size;
            }
            set
            {
                int w = value.Width;
                int h = value.Height;
                if (w < ControlMinWidth)
                {
                    w = ControlMinWidth;
                }
                if (h < ControlMinHeight)
                {
                    h = ControlMinHeight;
                }

                _Size = new Size(w, h);
            }
        }

        /// <summary>
        /// 新增于2.7.1
        /// </summary>
        public Padding Padding { get; set; }

        /// <summary>
        /// 是否显示边框
        /// </summary>
        public EBool DisplayBorder { get; set; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor { get; set; }

        private float _Alpha;
        /// <summary>
        /// 控件的不透明度
        /// </summary>
        [EditorAttribute(typeof(PropertyGridAlphaEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(AlphaConverter))]
        public float Alpha
        {
            get
            {
                return _Alpha;
            }
            set
            {
                if ((value >= .0f) && (value <= 1.0f))
                {
                    _Alpha = value;
                }
                else
                {
                    _Alpha = 1.0f;

                    //MessageBox.Show(UIResMang.GetString("Message53"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new Exception(UIResMang.GetString("Message53"));
                }
            }
        }

        private int _Radius;
        /// <summary>
        /// 控件的圆角半径
        /// </summary>
        public int Radius
        {
            get
            {
                return _Radius;
            }
            set
            {
                if (value >= 0)
                {
                    _Radius = value;
                }
                else
                {
                    _Radius = 0;

                    MessageBox.Show(UIResMang.GetString("Message54"), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 控件的外观
        /// </summary>
        public EFlatStyle FlatStyle { get; set; }

        /// <summary>
        /// 控件的背景色
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// 控件背景图片
        /// </summary>
        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string BackgroundImage { get; set; }
        #endregion

        #region 变量
        private static int InitId = Convert.ToInt32((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds);

        private const int Band = 8; //调整大小的响应边框
        private const int SideMobile = 15;
        private Size square = new Size(Band, Band);//小矩形大小
        private int MinWidth = 20; //最小宽度
        private int MinHeight = 20;//最小高度
        internal float Ratio;

        private string _ImagePath;
        /// <summary>
        /// 在工程中控件图片所在路径
        /// </summary>
        public string ImagePath
        {
            get
            {
                _ImagePath = GetImageName(this.Id);

                _ImagePath = Path.Combine(MyCache.ProjImgPath, _ImagePath);

                return _ImagePath;
            }

            set
            {
                _ImagePath = value;
            }
        }

        internal enum ControlState
        {
            /// <summary>
            /// 控件被鼠标按下
            /// </summary>
            Selected,
            /// <summary>
            /// 控件处于移动状态
            /// </summary>
            Move,
            /// <summary>
            /// 控件被鼠标释放
            /// </summary>
            Up,
            /// <summary>
            /// 普通状态
            /// </summary>
            Normal,
            /// <summary>
            /// 正在对控件进行大小调整
            /// </summary>
            ChangeSize,
            /// <summary>
            /// 需要从已选控件列表移除的控件
            /// </summary>
            SelectedAgain
        }

        internal enum MousePosOnCtrl
        {
            NONE = 0,
            TOP = 1,
            RIGHT = 2,
            BOTTOM = 3,
            LEFT = 4,
            TOPLEFT = 5,
            TOPRIGHT = 6,
            BOTTOMLEFT = 7,
            BOTTOMRIGHT = 8,
            SizeAll,
        }

        /// <summary>
        /// 当前控件是否已被选中
        /// </summary>
        internal bool IsThisSelected { get; set; }

        internal ControlState State { get; set; }

        #region 在界面绘制视图时用到的方法
        private Size _DrawSquare;
        internal Size DrawSquare
        {
            get { return _DrawSquare; }
            set { _DrawSquare = value; }
        }

        /// <summary>
        /// 绘制时视图的水平位置
        /// </summary>
        internal int DrawX
        {
            get
            {
                return ConvertToDraw(/*this.X*/this.Location.X, this.Ratio);
            }
        }

        /// <summary>
        /// 绘制时视图的垂直位置
        /// </summary>
        internal int DrawY
        {
            get
            {
                return ConvertToDraw(/*this.Y*/this.Location.Y, this.Ratio);
            }
        }

        internal Point DrawLocation
        {
            get
            {
                return new Point(this.DrawX, this.DrawY);
            }
        }

        /// <summary>
        /// 绘制时视图的宽度
        /// </summary>
        internal int DrawWidth
        {
            get
            {
                return ConvertToDraw(/*this.Width*/this.Size.Width, this.Ratio);
            }
        }

        /// <summary>
        /// 绘制时视图的高度
        /// </summary>
        internal int DrawHeight
        {
            get
            {
                return ConvertToDraw(/*this.Height*/this.Size.Height, this.Ratio);
            }
        }

        /// <summary>
        /// 绘制时视图的大小
        /// </summary>
        internal Size DrawSize
        {
            get
            {
                return new Size(this.DrawWidth, this.DrawHeight); ;
            }
        }

        /// <summary>
        /// 相对于父视图的矩形
        /// </summary>
        internal Rectangle FactRect
        {
            get
            {
                return new Rectangle(/*new Point(this.X, this.Y)*/this.Location, /*new Size(this.Width, this.Height)*/this.Size);
            }
        }

        /// <summary>
        /// 在页面中的位置，绝对位置
        /// </summary>
        internal Point LocationInPage
        {
            get
            {
                ViewNode pNode = this.Parent as ViewNode;
                if ((null != pNode) && (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    return new Point(this.DrawX + pNode.LocationInPage.X, this.DrawY + pNode.LocationInPage.Y);
                }
                else
                {
                    return this.DrawLocation;
                }
            }
        }

        /// <summary>
        /// 在页面中所占矩形，绝对位置
        /// </summary>
        internal Rectangle RectInPage
        {
            get
            {
                return new Rectangle(this.LocationInPage, this.DrawSize);
            }
        }

        /// <summary>
        /// 相对于页面的控件位置，没有缩放
        /// </summary>
        internal Point LocationInPageFact
        {
            get
            {
                ViewNode pNode = this.Parent as ViewNode;
                if ((null != pNode) && (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    return new Point(/*this.X*/this.Location.X + pNode.LocationInPageFact.X, /*this.Y*/this.Location.Y + pNode.LocationInPageFact.Y);
                }
                else
                {
                    return /*new Point(this.X, this.Y)*/this.Location;
                }
            }
        }

        /// <summary>
        /// 相对于页面的控件矩形框，没有缩放
        /// </summary>
        internal Rectangle RectInPageFact
        {
            get
            {
                return new Rectangle(this.LocationInPageFact, /*new Size(this.Width, this.Height)*/this.Size);
            }
        }

        /// <summary>
        /// 在页面中相对于父视图的可见矩形，绝对位置
        /// </summary>
        internal Rectangle VisibleRectInPage
        {
            get
            {
                ViewNode pNode = this.Parent as ViewNode;
                if ((null != pNode) && (MyConst.Controls.KnxGroupBoxType == pNode.Name))
                {
                    Rectangle rect = this.RectInPage;
                    rect.Intersect(pNode.VisibleRectInPage);
                    return rect;
                }
                else
                {
                    return this.RectInPage;
                }
            }
        }

        /// <summary>
        /// 调节大小的矩形边框
        /// </summary>
        internal Rectangle FrameBound
        {
            get
            {
                return new Rectangle(this.RectInPage.X - this.DrawSquare.Width - 1,
                    this.RectInPage.Y - this.DrawSquare.Height - 1,
                    this.RectInPage.Width + (this.DrawSquare.Width * 2) + 2,
                    this.RectInPage.Height + (this.DrawSquare.Height * 2) + 2);
            }
        }
        #endregion

        internal Point PPoint { get; set; }

        internal Rectangle[] SmallRects { get; set; }

        /// <summary>
        /// 定义的一个内含朝向东西南北四个放下小箭头的小矩形，
        /// 鼠标在这个小矩形内按下左键并移动鼠标可以拖动控件，
        /// 现仅用于GroupBox
        /// </summary>
        internal Rectangle MobileRect { get; set; }

        internal Point[] LinePoints { get; set; }

        /// <summary>
        /// 控件的可变大小边框是否已经显示
        /// </summary>
        internal bool FrameIsVisible { get; set; }

        internal MousePosOnCtrl MPOC { get; set; }

        public Point PreLocation { get; set; }

        public Rectangle PreBound { get; set; }

        /// <summary>
        /// 与父控件比对时因吸附效应产生的X轴方向补偿
        /// </summary>
        public int ParCompX { get; set; }
        /// <summary>
        /// 与父控件比对时因吸附效应产生的Y轴方向补偿
        /// </summary>
        public int ParCompY { get; set; }

        /// <summary>
        /// 与同一父控件下的兄弟控件比对时因吸附效应产生的X轴方向补偿
        /// </summary>
        public int GapCompX { get; set; }
        /// <summary>
        /// 与同一父控件下的兄弟控件比对时因吸附效应产生的X轴方向补偿
        /// </summary>
        public int GapCompY { get; set; }

        /// <summary>
        /// 与同一页面下的兄弟控件比对时因吸附效应产生的X轴方向补偿
        /// </summary>
        public int AliCompX { get; set; }
        /// <summary>
        /// 与同一页面下的兄弟控件比对时因吸附效应产生的X轴方向补偿
        /// </summary>
        public int AliCompY { get; set; }
        #endregion

        #region 构造函数
        public ViewNode()
        {
            this.Id = GenId();
            this.Location = Point.Empty;
            this.Size = new Size(ControlMinWidth, ControlMinHeight);
            this.Padding = new Padding(0);
            this.DisplayBorder = EBool.No;
            this.BorderColor = Color.Black;
            this.Alpha = 0.7f;
            this.Radius = 0;
            this.FlatStyle = EFlatStyle.Flat;
            this.BackgroundColor = Color.BlanchedAlmond;
            this.TitleFont = new STFont(Color.Black, 16);
            this.Text = "ViewNode";
            this.Title = "ViewNode";

            this.State = ControlState.Normal;
        }

        /// <summary>
        /// KNXView 转 ViewNode
        /// </summary>
        /// <param name="knx"></param>
        public ViewNode(KNXView knx, BackgroundWorker worker)
        {
            this.Id = knx.Id;
            this.Text = knx.Text;

            if (ImportedHelper.IsLessThan2_0_3())
            {
                this.Title = knx.Text;
            }
            else
            {
                this.Title = knx.Title;
            }

            if (null != worker)
            {
                worker.ReportProgress(0, string.Format(UIResMang.GetString("TextIsImporting"), this.Title));
            }

            this.Location = new Point(knx.Left, knx.Top);
            this.Size = new Size(knx.Width, knx.Height);
            if (null != knx.Padding)
            {
                this.Padding = knx.Padding.ToPadding();
            }
            else
            {
                this.Padding = new Padding(0);
            }
            this.DisplayBorder = (EBool)Enum.ToObject(typeof(EBool), knx.DisplayBorder);
            this.BorderColor = ColorHelper.HexStrToColor(knx.BorderColor);
            this.Alpha = knx.Alpha;
            this.Radius = knx.Radius;
            this.FlatStyle = (EFlatStyle)Enum.ToObject(typeof(EFlatStyle), knx.FlatStyle);
            this.BackgroundColor = ColorHelper.HexStrToColor(knx.BackgroundColor ?? "#FFFFFF");
            this.BackgroundImage = knx.BackgroundImage;
            if (ImportedHelper.IsLessThan2_5_2())
            {
                this.TitleFont = new STFont(knx.FontColor, knx.FontSize);
            }
            else
            {
                this.TitleFont = new STFont(knx.TitleFont);
            }

            this.State = ControlState.Normal;
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            ViewNode node = base.Clone() as ViewNode;
            node.Title = this.Title;
            node.Location = this.Location;
            node.Size = this.Size;
            node.Padding = this.Padding;
            node.DisplayBorder = this.DisplayBorder;
            node.BorderColor = this.BorderColor;
            node.Alpha = this.Alpha;
            node.Radius = this.Radius;
            node.FlatStyle = this.FlatStyle;
            node.BackgroundColor = this.BackgroundColor;
            node.BackgroundImage = this.BackgroundImage;
            node.TitleFont = this.TitleFont.Clone();

            node.State = ControlState.Normal;

            return node;
        }

        public virtual object Copy()
        {
            ViewNode node = this.Clone() as ViewNode;

            node.Title = this.Title + UIResMang.GetString("NCopy");

            return node;
        }
        #endregion

        #region 抽象函数
        public virtual void SetText(string title)
        {
            this.Text = this.Name + " " + title + "   " + this.Title;
        }

        public virtual string GetText(string text)
        {
            return this.Name + " " + text + "   " + this.Title;
        }
        #endregion

        #region 导出到KNX
        /// <summary>
        /// ViewNode 转 KNXView
        /// </summary>
        /// <param name="knx"></param>
        public void ToKnx(KNXView knx, BackgroundWorker worker)
        {
            if (null != worker)
            {
                worker.ReportProgress(0, string.Format(UIResMang.GetString("TextIsExporting"), this.Title));
            }

            knx.Id = this.Id;
            knx.Text = this.Text;
            knx.Title = this.Title;
            knx.Left = this.Location.X;
            knx.Top = this.Location.Y;
            knx.Width = this.Size.Width;
            knx.Height = this.Size.Height;
            knx.Padding = new KNXPadding(this.Padding);
            knx.DisplayBorder = (int)this.DisplayBorder;
            knx.BorderColor = ColorHelper.ColorToHexStr(this.BorderColor);
            knx.Alpha = this.Alpha;
            knx.Radius = this.Radius;
            knx.FlatStyle = (int)this.FlatStyle;
            knx.BackgroundColor = ColorHelper.ColorToHexStr(this.BackgroundColor);
            knx.BackgroundImage = this.BackgroundImage;
            knx.TitleFont = this.TitleFont.ToKnx();

            MyCache.ValidResImgNames.Add(knx.BackgroundImage);
        }
        #endregion

        public static int GenId()
        {
            return InitId++;
        }

        public string GetImageName(int id)
        {
            return "id_" + id;
        }

        public int GetAppNodeWidth()
        {
            if (MyConst.View.KnxAppType == this.Name)
            {
                //return this.Width;
                return this.Size.Width;
            }

            if (null == this.Parent)
            {
                return -1;
            }

            AppNode parentNode = this.Parent as AppNode;
            if (null != parentNode)
            {
                return GetAppNodeWidth();
            }
            else
            {
                //return parentNode.Width;
                return parentNode.Size.Width;
            }
        }

        public int GetAppNodeHeight()
        {
            if (MyConst.View.KnxAppType == this.Name)
            {
                //return this.Height;
                return this.Size.Height;
            }

            if (null == this.Parent)
            {
                return -1;
            }

            AppNode parentNode = this.Parent as AppNode;
            if (null != parentNode)
            {
                return GetAppNodeHeight();
            }
            else
            {
                //return parentNode.Height;
                return parentNode.Size.Height;
            }
        }

        /// <summary>
        /// 坐标p是否包含于控件可变大小框范围内
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool FrameBoundContainsPoint(Point p)
        {
            bool contains = false;
            if (null != this.SmallRects)
            {
                for (int i = 0; i < this.SmallRects.Length; i++)
                {
                    if (this.SmallRects[i].Contains(p))
                    {
                        contains = true;
                        break;
                    }
                }
            }

            return contains;
        }

        public void CompensateReset()
        {
            this.ParCompX = 0;
            this.ParCompY = 0;
            this.GapCompX = 0;
            this.GapCompY = 0;
            this.AliCompX = 0;
            this.AliCompY = 0;
        }

        public virtual void Selected()
        {
            this.IsThisSelected = true;
        }

        public virtual void Deselected()
        {
            this.IsThisSelected = false;
            this.State = ControlState.Normal;
        }

        /// <summary>
        /// 给定控件的水平绝对位置（相对于页面），求得水平相对位置（相对于父控件）
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int GetLocationXFromLocationXInPage(int x)
        {
            ViewNode pNode = this.Parent as ViewNode;
            if ((null != pNode) && (MyConst.Controls.KnxGroupBoxType == pNode.Name))
            {
                return x - pNode.LocationInPage.X;
            }
            else
            {
                return x;
            }
        }

        /// <summary>
        /// 给定控件的Right，求得控件的Left
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        public int GetLocationXFromLocationRightInPage(int right)
        {
            return GetLocationXFromLocationXInPage(right - this.DrawWidth);
        }

        /// <summary>
        /// 给定控件的绝对垂直位置（相对于页面），求得控件的相对垂直位置（相对于父控件）
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetLocationYFromLocationYInPage(int y)
        {
            ViewNode pNode = this.Parent as ViewNode;
            if ((null != pNode) && (MyConst.Controls.KnxGroupBoxType == pNode.Name))
            {
                return y - pNode.LocationInPage.Y;
            }
            else
            {
                return y;
            }
        }

        /// <summary>
        /// 给定控件的Bottom，求得控件的Top
        /// </summary>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public int GetLocationYFromLocationBottomInPage(int bottom)
        {
            return GetLocationYFromLocationYInPage(bottom - this.DrawHeight);
        }

        #region 控件绘制
        public virtual void ContainsPoint(Point p, List<ViewNode> nodes)
        {
            if (this.VisibleRectInPage.Contains(p))
            {
                nodes.Add(this);
            }
            else if (this.FrameIsVisible && FrameBoundContainsPoint(p)) // 调节大小
            {
                nodes.Add(this);
            }
        }

        public static int ConvertToDraw(int v, float ratio)
        {
            return (int)Math.Round((float)v * ratio, 0);
        }

        public static int ConvertToFact(int v, float ratio)
        {
            return (int)Math.Round((float)v / ratio, 0);
        }

        public void SetRatio(float ratio)
        {
            this.Ratio = ratio;
        }

        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="g">绘图图画</param>
        /// <param name="ratio">绘制时缩放比例</param>
        /// <param name="preview">是否为预览</param>
        public virtual void DrawAt(Graphics g, float ratio, bool preview)
        {
            if (ratio != this.Ratio)
            {
                this.Ratio = ratio;

                this.DrawSquare = new Size(ConvertToDraw(this.square.Width, ratio), ConvertToDraw(this.square.Height, ratio));
            }
        }

        public virtual void MouseDown(MouseEventArgs e)
        {
            this.PPoint = e.Location;
        }

        public virtual void MouseMove(MouseEventArgs e)
        {
            Point p = e.Location;

            if (p.Equals(this.PPoint))
            {
                return;
            }

            if (this.IsThisSelected && this.FrameIsVisible)
            {
                if (this.SmallRects[0].Contains(e.Location))
                {
                    this.MPOC = MousePosOnCtrl.TOPLEFT;
                }
                else if (this.SmallRects[1].Contains(e.Location))
                {
                    this.MPOC = MousePosOnCtrl.TOP;
                }
                else if (this.SmallRects[2].Contains(e.Location))
                {
                    this.MPOC = MousePosOnCtrl.TOPRIGHT;
                }
                else if (this.SmallRects[3].Contains(e.Location))
                {
                    this.MPOC = MousePosOnCtrl.RIGHT;
                }
                else if (this.SmallRects[4].Contains(e.Location))
                {
                    this.MPOC = MousePosOnCtrl.BOTTOMRIGHT;
                }
                else if (this.SmallRects[5].Contains(e.Location))
                {
                    this.MPOC = MousePosOnCtrl.BOTTOM;
                }
                else if (this.SmallRects[6].Contains(e.Location))
                {
                    this.MPOC = MousePosOnCtrl.BOTTOMLEFT;
                }
                else if (this.SmallRects[7].Contains(e.Location))
                {
                    this.MPOC = MousePosOnCtrl.LEFT;
                }
                else if (this.MobileRect.Contains(e.Location))
                {
                    this.MPOC = MousePosOnCtrl.SizeAll;
                }
                else
                {
                    this.MPOC = MousePosOnCtrl.NONE;
                }
            }
        }

        public virtual void MouseUp(MouseEventArgs e)
        {
            //this.State = ControlState.Normal;
            //Cursor.Current = Cursors.Default;
        }

        public static void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int cornerRadius, float borderWidth, float ratio)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius, borderWidth, ratio))
            {
                g.DrawPath(pen, path);
            }
        }

        public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius, float borderWidth, float ratio)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius, borderWidth, ratio))
            {
                g.FillPath(brush, path);
            }
        }

        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius, float borderWidth, float ratio)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            if (cornerRadius > 0)
            {
                int min = Math.Min(rect.Width, rect.Height);
                cornerRadius = Math.Min(min / 2, cornerRadius);
                cornerRadius = (int)Math.Round(cornerRadius * ratio, 0);

                roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90); // 左上角弧线
                roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius, rect.Y); // 顶部线段
                roundedRect.AddArc(rect.Right - cornerRadius * 2 - borderWidth, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90); // 右上角弧线
                roundedRect.AddLine(rect.Right - borderWidth, rect.Y + cornerRadius, rect.Right - borderWidth, rect.Bottom - cornerRadius); // 右侧线段
                roundedRect.AddArc(rect.Right - cornerRadius * 2 - borderWidth, rect.Bottom - cornerRadius * 2 - borderWidth, cornerRadius * 2, cornerRadius * 2, 0, 90); // 右下角弧线
                roundedRect.AddLine(rect.Right - cornerRadius, rect.Bottom - borderWidth, rect.X + cornerRadius, rect.Bottom - borderWidth); // 底部线段
                roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2 - borderWidth, cornerRadius * 2, cornerRadius * 2, 90, 90); // 左下角弧线
                roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius, rect.X, rect.Y + cornerRadius); // 左侧线段
            }
            else
            {
                roundedRect.AddLine(rect.X, rect.Y, rect.Right, rect.Y);
                roundedRect.AddLine(rect.Right - 1, rect.Y, rect.Right - 1, rect.Y + rect.Height);
                roundedRect.AddLine(rect.Right, rect.Bottom - 1, rect.X, rect.Bottom - 1);
                roundedRect.AddLine(rect.X, rect.Bottom, rect.X, rect.Y);
            }
            roundedRect.CloseFigure();
            return roundedRect;
        }

        internal void SetFrame()
        {
            /* 八个小矩形 */
            Rectangle[] rects = new Rectangle[8];
            rects[0] = new Rectangle(this.FrameBound.Location, this.DrawSquare); //左上
            rects[1] = new Rectangle(new Point(this.FrameBound.Location.X + (this.FrameBound.Width - this.DrawSquare.Width) / 2 - 1, this.FrameBound.Location.Y), this.DrawSquare); //上中
            rects[2] = new Rectangle(new Point(this.FrameBound.Location.X + this.FrameBound.Width - this.DrawSquare.Width - 1, this.FrameBound.Location.Y), this.DrawSquare); //右上
            rects[3] = new Rectangle(new Point(this.FrameBound.Location.X + this.FrameBound.Width - this.DrawSquare.Width - 1, this.FrameBound.Location.Y + (this.FrameBound.Height - this.DrawSquare.Height) / 2 - 1), this.DrawSquare); //右中
            rects[4] = new Rectangle(new Point(this.FrameBound.Location.X + this.FrameBound.Width - this.DrawSquare.Width - 1, this.FrameBound.Location.Y + this.FrameBound.Height - this.DrawSquare.Height - 1), this.DrawSquare); //右下
            rects[5] = new Rectangle(new Point(this.FrameBound.Location.X + (this.FrameBound.Width - this.DrawSquare.Width) / 2 - 1, this.FrameBound.Location.Y + this.FrameBound.Height - this.DrawSquare.Height - 1), this.DrawSquare); //下中
            rects[6] = new Rectangle(new Point(this.FrameBound.Location.X, this.FrameBound.Location.Y + this.FrameBound.Height - this.DrawSquare.Height - 1), this.DrawSquare); //左下
            rects[7] = new Rectangle(new Point(this.FrameBound.Location.X, this.FrameBound.Location.Y + (this.FrameBound.Height - this.DrawSquare.Height) / 2 - 1), this.DrawSquare); //左中
            this.SmallRects = rects;

            /* 八条边线 */
            Point[] points = new Point[16];
            points[0] = new Point(rects[0].Right, rects[0].Top + rects[0].Height / 2);
            points[1] = new Point(rects[1].Left, rects[1].Top + rects[1].Height / 2);
            points[2] = new Point(rects[1].Right, rects[1].Top + rects[1].Height / 2);
            points[3] = new Point(rects[2].Left, rects[2].Top + rects[2].Height / 2);

            points[4] = new Point(rects[2].Left + rects[2].Width / 2, rects[2].Bottom);
            points[5] = new Point(rects[3].Left + rects[3].Width / 2, rects[3].Top);
            points[6] = new Point(rects[3].Left + rects[3].Width / 2, rects[3].Bottom);
            points[7] = new Point(rects[4].Left + rects[4].Width / 2, rects[4].Top);

            points[8] = new Point(rects[4].Left, rects[4].Top + rects[4].Height / 2);
            points[9] = new Point(rects[5].Right, rects[5].Top + rects[5].Height / 2);
            points[10] = new Point(rects[5].Left, rects[5].Top + rects[5].Height / 2);
            points[11] = new Point(rects[6].Right, rects[6].Top + rects[6].Height / 2);
            points[12] = new Point(rects[6].Left + rects[6].Width / 2, rects[6].Top);
            points[13] = new Point(rects[7].Left + rects[7].Width / 2, rects[7].Bottom);
            points[14] = new Point(rects[7].Left + rects[7].Width / 2, rects[7].Top);
            points[15] = new Point(rects[0].Left + rects[0].Width / 2, rects[0].Bottom);
            this.LinePoints = points;
        }

        public Rectangle FrameChanged(int dx, int dy, bool respectRatio)
        {
            if (respectRatio)
            {
                //float r = (float)this.Width / (float)this.Height;
                float r = (float)this.Size.Width / (float)this.Size.Height;
                dx = (int)Math.Round((float)dy * Ratio, 0);
            }

            //int newX = this.Y;
            //int newY = this.X;
            //int newWidth = this.Width;
            //int newHeight = this.Height;
            int newX = this.Location.Y;
            int newY = this.Location.X;
            int newWidth = this.Size.Width;
            int newHeight = this.Size.Height;
            switch (this.MPOC)
            {
                case MousePosOnCtrl.TOP:
                    if (newHeight - dy > MinHeight)
                    {
                        newX += dy;
                        newHeight -= dy;
                    }
                    else
                    {
                        newX -= MinHeight - newHeight;
                        newHeight = MinHeight;
                    }
                    break;
                case MousePosOnCtrl.BOTTOM:
                    if (newHeight + dy > MinHeight)
                    {
                        newHeight += dy;
                    }
                    else
                    {
                        newHeight = MinHeight;
                    }
                    break;
                case MousePosOnCtrl.LEFT:
                    if (newWidth - dx > MinWidth)
                    {
                        newY += dx;
                        newWidth -= dx;
                    }
                    else
                    {
                        newY -= MinWidth - newWidth;
                        newWidth = MinWidth;
                    }

                    break;
                case MousePosOnCtrl.RIGHT:
                    if (newWidth + dx > MinWidth)
                    {
                        newWidth += dx;
                    }
                    else
                    {
                        newWidth = MinWidth;
                    }
                    break;
                case MousePosOnCtrl.TOPLEFT:
                    if (newHeight - dy > MinHeight)
                    {
                        newX += dy;
                        newHeight -= dy;
                    }
                    else
                    {
                        newX -= MinHeight - newHeight;
                        newHeight = MinHeight;
                    }
                    if (newWidth - dx > MinWidth)
                    {
                        newY += dx;
                        newWidth -= dx;
                    }
                    else
                    {
                        newY -= MinWidth - newWidth;
                        newWidth = MinWidth;
                    }
                    break;
                case MousePosOnCtrl.TOPRIGHT:
                    if (newHeight - dy > MinHeight)
                    {
                        newX += dy;
                        newHeight -= dy;
                    }
                    else
                    {
                        newX -= MinHeight - newHeight;
                        newHeight = MinHeight;
                    }
                    if (newWidth + dx > MinWidth)
                    {
                        newWidth += dx;
                    }
                    else
                    {
                        newWidth = MinWidth;
                    }
                    break;
                case MousePosOnCtrl.BOTTOMLEFT:
                    if (newHeight + dy > MinHeight)
                    {
                        newHeight += dy;
                    }
                    else
                    {
                        newHeight = MinHeight;
                    }
                    if (newWidth - dx > MinWidth)
                    {
                        newY += dx;
                        newWidth -= dx;
                    }
                    else
                    {
                        newY -= MinWidth - newWidth;
                        newWidth = MinWidth;
                    }
                    break;
                case MousePosOnCtrl.BOTTOMRIGHT:
                    if (newHeight + dy > MinHeight)
                    {
                        newHeight += dy;
                    }
                    else
                    {
                        newHeight = MinHeight;
                    }
                    if (newWidth + dx > MinWidth)
                    {
                        newWidth += dx;
                    }
                    else
                    {
                        newWidth = MinWidth;
                    }
                    break;

            }
            return new Rectangle(newY, newX, newWidth, newHeight);
        }
        #endregion

        #region 静态方法
        public static Point GetLocationInParent(Point parentLocation, Point pageLocation)
        {
            int x = pageLocation.X - parentLocation.X;
            int y = pageLocation.Y - parentLocation.Y;
            return new Point(x, y);
        }

        /// <summary>
        /// 获取node的PageNode
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static PageNode GetPageNodeFromParent(ViewNode node)
        {
            if (null != node)
            {
                if (MyConst.View.KnxPageType == node.Name)
                {
                    return node as PageNode;
                }
                else
                {
                    ViewNode parentNode = node.Parent as ViewNode;
                    return GetPageNodeFromParent(parentNode);
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取得包含所有控件的最小矩形
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static Rectangle GetMinimumCommonRectangleInPage(List<ViewNode> nodes)
        {
            if ((null == nodes) || (nodes.Count <= 0))
            {
                return Rectangle.Empty;
            }

            int x = nodes[0].RectInPageFact.X;
            int y = nodes[0].RectInPageFact.Y;
            int right = nodes[0].RectInPageFact.Right;
            int bottom = nodes[0].RectInPageFact.Bottom;

            foreach (ViewNode node in nodes)
            {
                /* 取得能包含两个控件的最小矩形框 */
                x = x > node.RectInPageFact.X ? node.RectInPageFact.X : x;
                y = y > node.RectInPageFact.Y ? node.RectInPageFact.Y : y;
                right = right > node.RectInPageFact.Right ? right : node.RectInPageFact.Right;
                bottom = bottom > node.RectInPageFact.Bottom ? bottom : node.RectInPageFact.Bottom;
            }

            int width = right - x;
            int height = bottom - y;

            return new Rectangle(x, y, width, height);
        }

        public static Rectangle GetMinimumCommonRectangleInParent(List<ViewNode> nodes)
        {
            if ((null == nodes) || (nodes.Count <= 0))
            {
                return Rectangle.Empty;
            }

            //int x = nodes[0].X;
            //int y = nodes[0].Y;
            //int right = nodes[0].X + nodes[0].Width;
            //int bottom = nodes[0].Y + nodes[0].Height;
            int x = nodes[0].Location.X;
            int y = nodes[0].Location.Y;
            int right = nodes[0].Location.X + nodes[0].Size.Width;
            int bottom = nodes[0].Location.Y + nodes[0].Size.Height;

            foreach (ViewNode node in nodes)
            {
                /* 取得能包含两个控件的最小矩形框 */
                //x = x > node.X ? node.X : x;
                //y = y > node.Y ? node.Y : y;
                //right = right > node.X + node.Width ? right : node.X + node.Width;
                //bottom = bottom > node.Y + node.Height ? bottom : node.Y + node.Height;
                x = x > node.Location.X ? node.Location.X : x;
                y = y > node.Location.Y ? node.Location.Y : y;
                right = right > node.Location.X + node.Size.Width ? right : node.Location.X + node.Size.Width;
                bottom = bottom > node.Location.Y + node.Size.Height ? bottom : node.Location.Y + node.Size.Height;
            }

            int width = right - x;
            int height = bottom - y;

            return new Rectangle(x, y, width, height);
        }
        #endregion
    }


}