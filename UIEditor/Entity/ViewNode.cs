
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using SourceGrid;
using Structure;
using UIEditor.Component;
using Button = SourceGrid.Cells.Button;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using UIEditor.SationUIControl;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace UIEditor.Entity
{
    /// <summary>
    /// 所有树上面添加元素的基础类，主要分配ID
    /// </summary>
    [Serializable]
    public abstract class ViewNode : TreeNode, ISerializable
    {
        private static int InitId = Convert.ToInt32((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds);

        #region
        public FrameControl fc { get; set; }
        public STPanel panel { get; set; }
        #endregion

        #region 属性

        /// <summary>
        /// 控件的唯一标识
        /// </summary>
        [BrowsableAttribute(false),
        ReadOnlyAttribute(true)]
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        //private Point _Location;
        //public Point Location
        //{
        //    get
        //    {
        //        return _Location;
        //    }
        //    set
        //    {
        //        _Location = value;
        //    }
        //}

        public int Width { get; set; }

        public int Height { get; set; }

        //private Size _Size;
        //public Size Size
        //{
        //    get
        //    {
        //        return _Size;
        //    }
        //    set
        //    {
        //        _Size = value;
        //    }
        //}

        /// <summary>
        /// 是否显示边框
        /// </summary>
        public EBool DisplayBorder { get; set; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// 控件的不透明度
        /// </summary>
        public double Alpha { get; set; }

        /// <summary>
        /// 控件的圆角半径
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// 控件的外观
        /// </summary>
        public EFlatStyle FlatStyle { get; set; }

        /// <summary>
        /// 控件的背景色
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// 控件的背景图片
        /// </summary>
        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string BackgroundImage { get; set; }

        /// <summary>
        /// 控件的字体颜色
        /// </summary>
        public Color FontColor { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }
        #endregion

        #region 变量
        private const int Band = 6; //调整大小的响应边框
        private const int SideMobile = 15;
        private Size square = new Size(Band, Band);//小矩形大小
        private int MinWidth = 20; //最小宽度
        private int MinHeight = 20;//最小高度

        internal enum ControlState
        {
            Down,
            Move,
            Up,
            Normal,
            ChangeSize
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
        internal bool IsSelected { get; set; }

        internal ControlState State { get; set; }

        /// <summary>
        /// 相对于父视图的矩形
        /// </summary>
        internal Rectangle FactRect
        {
            get
            {
                return new Rectangle(new Point(this.X, this.Y), new Size(this.Width, this.Height));
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
                    return new Point(this.X + pNode.LocationInPage.X, this.Y + pNode.LocationInPage.Y);
                }
                else
                {
                    return new Point(this.X, this.Y);
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
                return new Rectangle(this.LocationInPage, new Size(this.Width, this.Height));
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
                int X = this.RectInPage.X - square.Width - 1;
                int Y = this.RectInPage.Y - square.Height - 1;
                int Height = this.RectInPage.Height + (square.Height * 2) + 2;
                int Width = this.RectInPage.Width + (square.Width * 2) + 2;
                return new Rectangle(X, Y, Width, Height);
            }
        }

        internal Point PPoint { get; set; }

        internal Rectangle[] SmallRects { get; set; }

        /// <summary>
        /// 定义的一个内含朝向东西南北四个放下小箭头的小矩形，
        /// 鼠标在这个小矩形内按下左键并移动鼠标可以拖动控件，
        /// 现仅用于GroupBox
        /// </summary>
        internal Rectangle MobileRect { get; set; }

        internal Point[] LinePoints { get; set; }

        internal bool FrameIsVisible { get; set; }

        internal MousePosOnCtrl MPOC { get; set; }

        public Point PreLocation { get; set; }

        public Point PrePoint { get; set; }

        public Rectangle PreBound { get; set; }

        /// <summary>
        /// 与父控件比对时因吸附效应产生的X轴方向补偿
        /// </summary>
        public int ParCompX { get; set; }
        /// <summary>
        /// 与父控件比对时因吸附效应产生的Y轴方向补偿
        /// </summary>
        public int ParCompY { get; set; }
        public ViewNode ParNode { get; set; }

        /// <summary>
        /// 与同一父控件下的兄弟控件比对时因吸附效应产生的X轴方向补偿
        /// </summary>
        public int GapCompX { get; set; }
        /// <summary>
        /// 与同一父控件下的兄弟控件比对时因吸附效应产生的X轴方向补偿
        /// </summary>
        public int GapCompY { get; set; }
        public ViewNode GapNode { get; set; }

        /// <summary>
        /// 与同一页面下的兄弟控件比对时因吸附效应产生的X轴方向补偿
        /// </summary>
        public int AliCompX { get; set; }
        /// <summary>
        /// 与同一页面下的兄弟控件比对时因吸附效应产生的X轴方向补偿
        /// </summary>
        public int AliCompY { get; set; }
        public ViewNode AliNode { get; set; }
        #endregion

        public static int GenId()
        {
            return InitId++;
        }

        #region 构造函数

        public ViewNode()
        {
            this.Id = GenId();
            this.X = 0;
            this.Y = 0;
            this.Width = 0;
            this.Height = 0;
            //this.Location = Point.Empty;
            //this.Size = Size.Empty;
            this.DisplayBorder = EBool.No;
            this.BorderColor = Color.Black;
            this.Alpha = 0.7;
            this.Radius = 0;
            this.FlatStyle = EFlatStyle.Flat;
            this.BackgroundColor = Color.BlanchedAlmond;
            this.BackgroundImage = null;
            this.FontColor = Color.Black;
            this.FontSize = 16;
            this.Text = "ViewNode";

            this.State = ControlState.Normal;
            //this.LocationInPage = Point.Empty;
        }

        public override object Clone()
        {
            ViewNode node = base.Clone() as ViewNode;
            node.Text = this.Text + " " + ResourceMng.GetString("NCopy");

            node.Id = GenId();
            node.X = this.X;
            node.Y = this.Y;
            node.Width = this.Width;
            node.Height = this.Height;
            //node.Location = this.Location;
            //node.Size = this.Size;
            node.DisplayBorder = this.DisplayBorder;
            node.BorderColor = this.BorderColor;
            node.Alpha = this.Alpha;
            node.Radius = this.Radius;
            node.FlatStyle = this.FlatStyle;
            node.BackgroundColor = this.BackgroundColor;
            node.BackgroundImage = this.BackgroundImage;
            node.FontColor = this.FontColor;
            node.FontSize = this.FontSize;

            node.State = ControlState.Normal;

            return node;
        }

        /// <summary>
        /// KNXView 转 ViewNode
        /// </summary>
        /// <param name="knx"></param>
        public ViewNode(KNXView knx)
        {
            this.Id = knx.Id;
            this.Text = knx.Text;
            this.X = knx.Left;
            this.Y = knx.Top;
            this.Width = knx.Width;
            this.Height = knx.Height;
            //this.Location = new Point(knx.Left, knx.Top);
            //this.Size = new Size(knx.Width, knx.Height);
            this.DisplayBorder = (EBool)Enum.ToObject(typeof(EBool), knx.DisplayBorder);
            this.BorderColor = FrmMainHelp.HexStrToColor(knx.BorderColor);
            this.Alpha = knx.Alpha;
            this.Radius = knx.Radius;
            this.FlatStyle = (EFlatStyle)Enum.ToObject(typeof(EFlatStyle), knx.FlatStyle);
            this.BackgroundColor = FrmMainHelp.HexStrToColor(knx.BackgroundColor ?? "#FFFFFF");
            this.BackgroundImage = knx.BackgroundImage;
            this.FontColor = FrmMainHelp.HexStrToColor(knx.FontColor ?? "#000000");
            this.FontSize = knx.FontSize;

            this.State = ControlState.Normal;
        }

        protected ViewNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        /// <summary>
        /// ViewNode 转 KNXView
        /// </summary>
        /// <param name="knx"></param>
        protected void ToKnx(KNXView knx)
        {
            knx.Id = this.Id;
            knx.Text = this.Text;
            knx.Left = this.X;
            knx.Top = this.Y;
            knx.Width = this.Width;
            knx.Height = this.Height;
            //knx.Left = this.Location.X;
            //knx.Top = this.Location.Y;
            //knx.Width = this.Size.Width;
            //knx.Height = this.Size.Height;
            knx.DisplayBorder = (int)this.DisplayBorder;
            knx.BorderColor = FrmMainHelp.ColorToHexStr(this.BorderColor);
            knx.Alpha = this.Alpha;
            knx.Radius = this.Radius;
            knx.FlatStyle = (int)this.FlatStyle;
            knx.BackgroundColor = FrmMainHelp.ColorToHexStr(this.BackgroundColor);
            knx.BackgroundImage = this.BackgroundImage;
            knx.FontColor = FrmMainHelp.ColorToHexStr(this.FontColor);
            knx.FontSize = this.FontSize;
        }

        #region
        //private void UpdateLocationInPage()
        //{
        //    ViewNode pNode = this.Parent as ViewNode;
        //    if ((null != pNode) && (MyConst.Controls.KnxGroupBoxType == pNode.Name))
        //    {
        //        this.LocationInPage = new Point(this.Location.X + pNode.LocationInPage.X, this.Location.Y + pNode.LocationInPage.Y);
        //    }
        //    else
        //    {
        //        this.LocationInPage = new Point(this.Location.X, this.Location.Y);
        //    }
        //}

        //private void UpdateRectInPage()
        //{
        //    this.RectInPage = new Rectangle(this.LocationInPage, this.Size);
        //}

        //private void UpdateVisibleRectInPage()
        //{
        //    ViewNode pNode = this.Parent as ViewNode;
        //    if ((null != pNode) && (MyConst.Controls.KnxGroupBoxType == pNode.Name))
        //    {
        //        Rectangle pRect = pNode.VisibleRectInPage;
        //        int x = this.VisibleRect.X + pNode.LocationInPage.X;
        //        int y = this.VisibleRect.Y + pNode.LocationInPage.Y;
        //        int width = this.VisibleRect.Width;
        //        int height = this.VisibleRect.Height;
        //        int right = x + width;
        //        int bottom = y + height;

        //        x = x >= pRect.X ? x : pRect.X;
        //        y = y >= pRect.Y ? y : pRect.Y;

        //        width = right - x;
        //        height = bottom - y;

        //        width = right > pRect.Right ? pRect.Right - x : width;
        //        height = bottom > pRect.Bottom ? pRect.Bottom - y : height;
        //        width = width > 0 ? width : 0;
        //        height = height > 0 ? height : 0;

        //        this.VisibleRectInPage = new Rectangle(x, y, width, height);
        //    }
        //    else
        //    {
        //        this.VisibleRectInPage = this.VisibleRect;
        //    }
        //}

        //private void UpdateFrameBound()
        //{
        //    int X = this.RectInPage.X - square.Width - 1;
        //    int Y = this.RectInPage.Y - square.Height - 1;
        //    int Height = this.RectInPage.Height + (square.Height * 2) + 2;
        //    int Width = this.RectInPage.Width + (square.Width * 2) + 2;
        //    this.FrameBound = new Rectangle(X, Y, Width, Height);
        //}

        //private void UpdateFactRect()
        //{
        //    this.FactRect = new Rectangle(this.Location, this.Size);
        //}

        //private void UpdateVisibleRect()
        //{
        //    if (MyConst.View.KnxPageType == this.Name)
        //    {
        //        this.VisibleRect = this.FactRect;
        //    }
        //    else
        //    {
        //        ViewNode pNode = this.Parent as ViewNode;
        //        if (null != pNode)
        //        {
        //            int x = this.Left > 0 ? this.Left : 0;
        //            int y = this.Top > 0 ? this.Top : 0;
        //            int right = this.Right >= pNode.Size.Width ? pNode.Size.Width : this.Right;
        //            int bottom = this.Bottom >= pNode.Size.Height ? pNode.Size.Height : this.Bottom;

        //            this.VisibleRect = new Rectangle(new Point(x, y), new Size(right - x, bottom - y));
        //        }
        //        else
        //        {
        //            this.VisibleRect = this.FactRect;
        //        }
        //    }
        //}
        #endregion

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

        public virtual void Selected()
        {
            this.IsSelected = true;
            this.State = ControlState.Move;
        }

        public virtual void Deselected()
        {
            this.IsSelected = false;
            this.State = ControlState.Normal;
        }

        public int GetLocationXFromLocationXInPage(int x)
        {
            ViewNode pNode = this.Parent as ViewNode;
            if ((null != pNode) && (MyConst.Controls.KnxGroupBoxType == pNode.Name))
            {
                return x - pNode.LocationInPage.X;
                //this.Location = new Point(x - pNode.LocationInPage.X, this.Location.Y);
            }
            else
            {
                return x;
                //this.Location = new Point(x, this.Location.Y);
            }
        }

        public int GetLocationXFromLocationRightInPage(int right)
        {
            //SetLocationXFromLocationXInPage(right - this.Size.Width);
            return GetLocationXFromLocationXInPage(right - this.Width);
        }

        public int GetLocationYFromLocationYInPage(int y)
        {
            ViewNode pNode = this.Parent as ViewNode;
            if ((null != pNode) && (MyConst.Controls.KnxGroupBoxType == pNode.Name))
            {
                //this.Location = new Point(this.Location.X, y - pNode.LocationInPage.Y);
                return y - pNode.LocationInPage.Y;
            }
            else
            {
                //this.Location = new Point(this.Location.X, y);
                return y;
            }
        }

        public int GetLocationYFromLocationBottomInPage(int bottom)
        {
            //SetLocationYFromLocationYInPage(bottom - this.Size.Height);
            return GetLocationYFromLocationYInPage(bottom - this.Height);
        }

        #region 控件绘制
        public virtual void ContainsPoint(Point p, List<ViewNode> nodes)
        {
            if (this.VisibleRectInPage.Contains(p))
            {
                nodes.Add(this);
            }
            else if (this.FrameIsVisible && FrameBoundContainsPoint(p)/*this.FrameBound.Contains(p)*/) // 调节大小
            {
                nodes.Add(this);
            }
        }
        public virtual void DrawAt(Point basePoint, Graphics g)
        {
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

            if (this.IsSelected && this.FrameIsVisible)
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

        internal GraphicsPath GetRoundRectangle(Rectangle rectangle, int r)
        {
            int l = 2 * r;
            // 把圆角矩形分成八段直线、弧的组合，依次加到路径中 
            GraphicsPath gp = new GraphicsPath();

            if (r > 0)
            {
                gp.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right - r, rectangle.Y)); // 顶端横线
                gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Y, l, l), 270, 90); // 右上圆角

                gp.AddLine(new Point(rectangle.Right, rectangle.Y + r), new Point(rectangle.Right, rectangle.Bottom - r)); // 右端竖线
                gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Bottom - l, l, l), 0, 90); // 右下圆角

                gp.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom)); // 底端横线
                gp.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90, 90); // 左下圆角

                gp.AddLine(new Point(rectangle.X, rectangle.Bottom - r), new Point(rectangle.X, rectangle.Y + r)); // 左端竖线
                gp.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180, 90); // 左上圆角
            }
            else
            {
                gp.AddLine(new Point(rectangle.X, rectangle.Y), new Point(rectangle.Right, rectangle.Y)); // 顶端横线
                gp.AddLine(new Point(rectangle.Right, rectangle.Y), new Point(rectangle.Right, rectangle.Bottom)); // 右端竖线
                gp.AddLine(new Point(rectangle.Right, rectangle.Bottom), new Point(rectangle.X, rectangle.Bottom)); // 底端横线
                gp.AddLine(new Point(rectangle.X, rectangle.Bottom), new Point(rectangle.X, rectangle.Y)); // 左端竖线
            }

            gp.CloseFigure();

            return gp;
        }

        public static void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int cornerRadius, float borderWidth)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius, borderWidth))
            {
                g.DrawPath(pen, path);
            }
        }

        public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius, float borderWidth)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius, borderWidth))
            {
                g.FillPath(brush, path);
            }
        }

        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius, float borderWidth)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            if (cornerRadius > 0)
            {
                int min = Math.Min(rect.Width, rect.Height);
                cornerRadius = Math.Min(min / 2, cornerRadius);

                roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90); // 左上角弧线
                roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius, rect.Y); // 顶部线段
                roundedRect.AddArc(rect.Right - cornerRadius * 2 - borderWidth, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90); // 右上角弧线
                roundedRect.AddLine(rect.Right - borderWidth, rect.Y + cornerRadius, rect.Right - borderWidth, rect.Bottom - cornerRadius); // 右侧线段
                roundedRect.AddArc(rect.Right - cornerRadius * 2 - borderWidth, rect.Bottom - cornerRadius * 2 - borderWidth, cornerRadius * 2, cornerRadius * 2, 0, 90); // 右下角弧线
                roundedRect.AddLine(rect.Right - cornerRadius , rect.Bottom - borderWidth, rect.X + cornerRadius, rect.Bottom - borderWidth); // 底部线段
                roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2 - borderWidth, cornerRadius * 2, cornerRadius * 2, 90, 90); // 左下角弧线
                roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius , rect.X, rect.Y + cornerRadius); // 左侧线段
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
            rects[0] = new Rectangle(this.FrameBound.Location, square); //左上
            rects[1] = new Rectangle(new Point(this.FrameBound.Location.X + (this.FrameBound.Width - square.Width) / 2 - 1, this.FrameBound.Location.Y), square); //上中
            rects[2] = new Rectangle(new Point(this.FrameBound.Location.X + this.FrameBound.Width - square.Width, this.FrameBound.Location.Y), square); //右上
            rects[3] = new Rectangle(new Point(this.FrameBound.Location.X + this.FrameBound.Width - square.Width, this.FrameBound.Location.Y + (this.FrameBound.Height - square.Height) / 2 - 1), square); //右中
            rects[4] = new Rectangle(new Point(this.FrameBound.Location.X + this.FrameBound.Width - square.Width, this.FrameBound.Location.Y + this.FrameBound.Height - square.Height - 1), square); //右下
            rects[5] = new Rectangle(new Point(this.FrameBound.Location.X + (this.FrameBound.Width - square.Width) / 2 - 1, this.FrameBound.Location.Y + this.FrameBound.Height - square.Height - 1), square); //下中
            rects[6] = new Rectangle(new Point(this.FrameBound.Location.X, this.FrameBound.Location.Y + this.FrameBound.Height - square.Height - 1), square); //左下
            rects[7] = new Rectangle(new Point(this.FrameBound.Location.X, this.FrameBound.Location.Y + (this.FrameBound.Height - square.Height) / 2 - 1), square); //左中
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

        public Rectangle FrameChanged(int dx, int dy)
        {
            int newX = this.Y;
            int newY = this.X;
            int newWidth = this.Width;
            int newHeight = this.Height;
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
        #endregion

    }


}