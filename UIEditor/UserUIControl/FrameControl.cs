using DevAge.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Entity;
using UIEditor.SationUIControl;

namespace UIEditor
{
    public class FrameControl : UserControl
    {
        public ViewNode node { get; set; }
        public STPanel Panel { get; set; }

        #region Constructors
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrameControl(STPanel panel)
        {
            Panel = panel;
            AddEvents();
        }
        #endregion

        #region Fields
        //private int lineWidth = 2;
        const int Band = 6; //调整大小的响应边框
        private int MinWidth = 20; //最小宽度
        private int MinHeight = 20;//最小高度
        Size square = new Size(Band, Band);//小矩形大小

        Rectangle[] smallRects = new Rectangle[8];//边框中的八个小圆圈
        Point[] linePoints = new Point[16];//四条边，用于画虚线
        Graphics g; //画图板
        Rectangle ControlRect; //控件包含边框的区域  
        private Point pPoint; //上个鼠标坐标
        private Point cPoint; //当前鼠标坐标
        private MousePosOnCtrl mpoc;
        private bool FixAspectRatio = false;

        public delegate void ControlMouseUpEventDelegate(object sender, EventArgs e);
        public event ControlMouseUpEventDelegate ControlMouseUpEvent;
        #endregion

        #region Properties
        /// <summary>
        /// 鼠标在控件中位置
        /// </summary>
        enum MousePosOnCtrl
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
        }
        #endregion

        #region Methods
        /// <summary>
        /// 加载事件
        /// </summary>
        private void AddEvents()
        {
            this.MouseDown += new MouseEventHandler(FrameControl_MouseDown);
            this.MouseMove += new MouseEventHandler(FrameControl_MouseMove);
            this.MouseUp += new MouseEventHandler(FrameControl_MouseUp);
        }

        #region 创建边框

        /// <summary>
        /// 建立控件可视区域
        /// </summary>
        public void CreateBounds()
        {
            //创建边界
            int X = Panel.Bounds.X - square.Width - 1;
            int Y = Panel.Bounds.Y - square.Height - 1;
            int Height = Panel.Bounds.Height + (square.Height * 2) + 2;
            int Width = Panel.Bounds.Width + (square.Width * 2) + 2;
            this.Bounds = new Rectangle(X, Y, Width, Height);

            SetRectangles();
            //设置可视区域
            this.Region = new Region(BuildFrame());
        }

        /// <summary>
        /// 设置定义8个小矩形的范围
        /// </summary>
        void SetRectangles()
        {
            /* 八个小圆圈 */
            smallRects[0] = new Rectangle(new Point(0, 0), square); //左上
            smallRects[1] = new Rectangle(new Point((this.Width - square.Width) / 2 - 1, 0), square); //上中
            smallRects[2] = new Rectangle(new Point(this.Width - square.Width, 0), square); //右上
            smallRects[3] = new Rectangle(new Point(this.Width - square.Width, (this.Height - square.Height) / 2 - 1), square); //右中
            smallRects[4] = new Rectangle(new Point(this.Width - square.Width, this.Height - square.Height - 1), square); //右下
            smallRects[5] = new Rectangle(new Point((this.Width - square.Width) / 2 - 1, this.Height - square.Height - 1), square); //下中
            smallRects[6] = new Rectangle(new Point(0, this.Height - square.Height - 1), square); //左下
            smallRects[7] = new Rectangle(new Point(0, (this.Height - square.Height) / 2 - 1), square); //左中

            /* 八条边线 */
            linePoints[0] = new Point(smallRects[0].Right, smallRects[0].Height / 2);
            linePoints[1] = new Point(smallRects[1].Left, smallRects[1].Height / 2);
            linePoints[2] = new Point(smallRects[1].Right, smallRects[1].Height / 2);
            linePoints[3] = new Point(smallRects[2].Left, smallRects[2].Height / 2);
            linePoints[4] = new Point(smallRects[2].Left + smallRects[2].Width / 2-1, smallRects[2].Bottom);
            linePoints[5] = new Point(smallRects[3].Left + smallRects[3].Width / 2-1, smallRects[3].Top);
            linePoints[6] = new Point(smallRects[3].Left + smallRects[3].Width / 2-1, smallRects[3].Bottom);
            linePoints[7] = new Point(smallRects[4].Left + smallRects[4].Width / 2-1, smallRects[4].Top);
            linePoints[8] = new Point(smallRects[4].Left, smallRects[4].Top + smallRects[4].Height / 2);
            linePoints[9] = new Point(smallRects[5].Right, smallRects[5].Top + smallRects[5].Height / 2);
            linePoints[10] = new Point(smallRects[5].Left, smallRects[5].Top + smallRects[5].Height / 2);
            linePoints[11] = new Point(smallRects[6].Right, smallRects[6].Top + smallRects[6].Height / 2);
            linePoints[12] = new Point(smallRects[6].Width / 2, smallRects[6].Top);
            linePoints[13] = new Point(smallRects[7].Width / 2, smallRects[7].Bottom);
            linePoints[14] = new Point(smallRects[7].Width / 2, smallRects[7].Top);
            linePoints[15] = new Point(smallRects[0].Width / 2, smallRects[0].Bottom);

            /* 整个包括周围边框的范围 */
            ControlRect = new Rectangle(new Point(0, 0), this.Bounds.Size);
        }

        /// <summary>
        /// 设置边框控件可视区域
        /// </summary>
        /// <returns></returns>
        private GraphicsPath BuildFrame()
        {
            GraphicsPath path = new GraphicsPath();

            path.AddRectangles(smallRects);

            Rectangle[] rects1 = new Rectangle[8];
            rects1[0] = new Rectangle(linePoints[0], new Size(linePoints[1].X - linePoints[0].X, 1));
            rects1[1] = new Rectangle(linePoints[2], new Size(linePoints[3].X - linePoints[2].X, 1));
            rects1[2] = new Rectangle(linePoints[4], new Size(1, linePoints[5].Y - linePoints[4].Y));
            rects1[3] = new Rectangle(linePoints[6], new Size(1, linePoints[7].Y - linePoints[6].Y));
            rects1[4] = new Rectangle(linePoints[9], new Size(linePoints[8].X - linePoints[9].X, 1));
            rects1[5] = new Rectangle(linePoints[11], new Size(linePoints[10].X - linePoints[11].X, 1));
            rects1[6] = new Rectangle(linePoints[13], new Size(1, linePoints[12].Y - linePoints[13].Y));
            rects1[7] = new Rectangle(linePoints[15], new Size(1, linePoints[14].Y - linePoints[15].Y));

            path.AddRectangles(rects1);

            return path;
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Draw();
        }

        public override void Refresh()
        {
            this.CreateBounds();

            base.Refresh();
        }

        /// <summary>
        /// 绘图
        /// </summary>
        public void Draw()
        {
            Console.WriteLine("FrameControl:" + "InitializeComponent:");

            g = this.CreateGraphics();

            SolidBrush brush = new SolidBrush(Color.Black);
            g.FillRegion(brush, Region);

            this.BringToFront();

            Pen pen = new Pen(Color.LightGray, 0.8f);
            pen.DashStyle = DashStyle.Dot;//设置为虚线,用虚线画四边，模拟微软效果
            g.DrawLine(pen, linePoints[0], linePoints[1]);
            g.DrawLine(pen, linePoints[2], linePoints[3]);
            g.DrawLine(pen, linePoints[4], linePoints[5]);
            g.DrawLine(pen, linePoints[6], linePoints[7]);
            g.DrawLine(pen, linePoints[8], linePoints[9]);
            g.DrawLine(pen, linePoints[10], linePoints[11]);
            g.DrawLine(pen, linePoints[12], linePoints[13]);
            g.DrawLine(pen, linePoints[14], linePoints[15]);


            g.FillRectangles(Brushes.White, smallRects); //填充8个小矩形的内部

            pen.Width = 2.0f;
            pen.DashStyle = DashStyle.Solid;
            pen.Color = Color.Black;
            g.DrawRectangles(pen, smallRects);  //绘制8个小矩形的黑色边线
        }

        public void DrawBoundsLine()
        {
            g = this.CreateGraphics();

            this.BringToFront();

            Pen pen = new Pen(Color.White, 1);
            pen.DashStyle = DashStyle.Solid;
            g.DrawLines(pen, linePoints);//绘制四条边线
        }

        public Point getControlLocation()
        {
            int x = this.Left + square.Width + 1;
            int y = this.Top + square.Height + 1;

            return new Point(x, y);
        }

        /// <summary>
        /// 设置光标状态
        /// </summary>
        public bool SetCursorShape(int x, int y)
        {
            Point point = new Point(x, y);
            if (!ControlRect.Contains(point))
            {
                Cursor.Current = Cursors.Arrow;
                return false;
            }
            else if (smallRects[0].Contains(point))
            {
                Cursor.Current = Cursors.SizeNWSE;
                mpoc = MousePosOnCtrl.TOPLEFT;
            }
            else if (smallRects[1].Contains(point))
            {
                Cursor.Current = Cursors.SizeNS;
                mpoc = MousePosOnCtrl.TOP;
            }
            else if (smallRects[2].Contains(point))
            {
                Cursor.Current = Cursors.SizeNESW;
                mpoc = MousePosOnCtrl.TOPRIGHT;
            }
            else if (smallRects[3].Contains(point))
            {
                Cursor.Current = Cursors.SizeWE;
                mpoc = MousePosOnCtrl.RIGHT;
            }
            else if (smallRects[4].Contains(point))
            {
                Cursor.Current = Cursors.SizeNWSE;
                mpoc = MousePosOnCtrl.BOTTOMRIGHT;
            }
            else if (smallRects[5].Contains(point))
            {
                Cursor.Current = Cursors.SizeNS;
                mpoc = MousePosOnCtrl.BOTTOM;
            }
            else if (smallRects[6].Contains(point))
            {
                Cursor.Current = Cursors.SizeNESW;
                mpoc = MousePosOnCtrl.BOTTOMLEFT;
            }
            else if (smallRects[7].Contains(point))
            {
                Cursor.Current = Cursors.SizeWE;
                mpoc = MousePosOnCtrl.LEFT;
            }
            else
            {
                Cursor.Current = Cursors.Arrow;
            }
            return true;
        }

        /// <summary>
        /// 控件移动
        /// </summary>
        private void ControlMove()
        {
            cPoint = Cursor.Position;
            int x = cPoint.X - pPoint.X;
            int y = cPoint.Y - pPoint.Y;
            //Rectangle rect = baseControl.Bounds;
            int baseControlTop = Panel.Top;
            int baseControlLeft = Panel.Left;
            int baseControlWidth = Panel.Width;
            int baseControlHeight = Panel.Height;
            double oldAspectRatio = (double)baseControlWidth / baseControlHeight;
            switch (this.mpoc)
            {
                case MousePosOnCtrl.TOP:
                    if (baseControlHeight - y > MinHeight)
                    {
                        baseControlTop += y;
                        baseControlHeight -= y;
                    }
                    else
                    {
                        baseControlTop -= MinHeight - baseControlHeight;
                        baseControlHeight = MinHeight;
                    }
                    break;
                case MousePosOnCtrl.BOTTOM:
                    if (baseControlHeight + y > MinHeight)
                    {
                        baseControlHeight += y;
                    }
                    else
                    {
                        baseControlHeight = MinHeight;
                    }
                    break;
                case MousePosOnCtrl.LEFT:
                    if (baseControlWidth - x > MinWidth)
                    {
                        baseControlLeft += x;
                        baseControlWidth -= x;
                    }
                    else
                    {
                        baseControlLeft -= MinWidth - baseControlWidth;
                        baseControlWidth = MinWidth;
                    }

                    break;
                case MousePosOnCtrl.RIGHT:
                    if (baseControlWidth + x > MinWidth)
                    {
                        baseControlWidth += x;
                    }
                    else
                    {
                        baseControlWidth = MinWidth;
                    }
                    break;
                case MousePosOnCtrl.TOPLEFT:
                    if (baseControlHeight - y > MinHeight)
                    {
                        baseControlTop += y;
                        baseControlHeight -= y;
                    }
                    else
                    {
                        baseControlTop -= MinHeight - baseControlHeight;
                        baseControlHeight = MinHeight;
                    }
                    if (baseControlWidth - x > MinWidth)
                    {
                        baseControlLeft += x;
                        baseControlWidth -= x;
                    }
                    else
                    {
                        baseControlLeft -= MinWidth - baseControlWidth;
                        baseControlWidth = MinWidth;
                    }
                    break;
                case MousePosOnCtrl.TOPRIGHT:
                    if (baseControlHeight - y > MinHeight)
                    {
                        baseControlTop += y;
                        baseControlHeight -= y;
                    }
                    else
                    {
                        baseControlTop -= MinHeight - baseControlHeight;
                        baseControlHeight = MinHeight;
                    }
                    if (baseControlWidth + x > MinWidth)
                    {
                        baseControlWidth += x;
                    }
                    else
                    {
                        baseControlWidth = MinWidth;
                    }
                    break;
                case MousePosOnCtrl.BOTTOMLEFT:
                    if (baseControlHeight + y > MinHeight)
                    {
                        baseControlHeight += y;
                    }
                    else
                    {
                        baseControlHeight = MinHeight;
                    }
                    if (baseControlWidth - x > MinWidth)
                    {
                        baseControlLeft += x;
                        baseControlWidth -= x;
                    }
                    else
                    {
                        baseControlLeft -= MinWidth - baseControlWidth;
                        baseControlWidth = MinWidth;
                    }
                    break;
                case MousePosOnCtrl.BOTTOMRIGHT:
                    if (baseControlHeight + y > MinHeight)
                    {
                        baseControlHeight += y;
                    }
                    else
                    {
                        baseControlHeight = MinHeight;
                    }
                    if (baseControlWidth + x > MinWidth)
                    {
                        baseControlWidth += x;
                    }
                    else
                    {
                        baseControlWidth = MinWidth;
                    }
                    break;

            }
            pPoint = Cursor.Position;

            if (this.FixAspectRatio)
            {
                double newAspectRatio = (double)baseControlWidth / baseControlHeight;
                if (oldAspectRatio > newAspectRatio)
                {
                    baseControlHeight = (int)(baseControlWidth / oldAspectRatio);
                }
                else
                {
                    baseControlWidth = (int)(baseControlHeight * oldAspectRatio);
                }
            }

            Panel.Bounds = new Rectangle(baseControlLeft, baseControlTop, baseControlWidth, baseControlHeight);
        }

        #endregion

        #region Events
        /// <summary>
        /// 鼠标按下事件：记录当前鼠标相对窗体的坐标
        /// </summary>
        void FrameControl_MouseDown(object sender, MouseEventArgs e)
        {
            pPoint = Cursor.Position;
        }

        /// <summary>
        /// 鼠标移动事件：让控件跟着鼠标移动
        /// </summary>
        void FrameControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = false;
                ControlMove();
            }
            else
            {
                this.Visible = true;
                SetCursorShape(e.X, e.Y); //更新鼠标指针样式
            }
        }

        /// <summary>
        /// 鼠标弹起事件：让自定义的边框出现
        /// </summary>
        void FrameControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.Refresh();
            this.Visible = true;

            ControlMouseUpNotify(this, EventArgs.Empty);
        }
        #endregion



        public void ControlMouseUpNotify(object sender, EventArgs e)
        {
            if (null == ControlMouseUpEvent)
            {
                ControlMouseUpEvent += new ControlMouseUpEventDelegate(ControlMouseUpEventException);
            }

            ControlMouseUpEvent(sender, e);
        }

        void ControlMouseUpEventException(object sender, EventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
