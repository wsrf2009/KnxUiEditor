using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.SationUIControl;

namespace UIEditor
{
    class FrameControl : Panel
    {
        #region Constructors
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrameControl(STPanel panel)
        {
            //this.SetStyle(
            //    ControlStyles.UserPaint |  //控件自行绘制，而不使用操作系统的绘制
            //    ControlStyles.AllPaintingInWmPaint| //忽略擦出的消息，减少闪烁。
            //    ControlStyles.OptimizedDoubleBuffer//在缓冲区上绘制，不直接绘制到屏幕上，减少闪烁。
                 //ControlStyles.ResizeRedraw //控件大小发生变化时，重绘。                  
                 //ControlStyles.SupportsTransparentBackColor
            //     | ControlStyles.DoubleBuffer
                //, true);//支持透明背景颜色

            baseControl = panel;
            AddEvents();
        }
        #endregion

        #region Fields
        //private int lineWidth = 2;
        const int Band = 4; //调整大小的响应边框
        private int MinWidth = 20; //最小宽度
        private int MinHeight = 20;//最小高度
        Size square = new Size(Band, Band);//小矩形大小
        public STPanel baseControl; //基础控件，即被包围的控件
        Rectangle[] smallRects = new Rectangle[8];//边框中的八个小圆圈
        Rectangle[] sideRects = new Rectangle[4];//四条边框，用来做响应区域
        Point[] linePoints = new Point[5];//四条边，用于画虚线
        Graphics g; //画图板
        Rectangle ControlRect; //控件包含边框的区域  
        private Point pPoint; //上个鼠标坐标
        private Point cPoint; //当前鼠标坐标
        private MousePosOnCtrl mpoc;
        private bool FixAspectRatio = false;
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
            int X = baseControl.Bounds.X - square.Width - 1;
            int Y = baseControl.Bounds.Y - square.Height - 1;
            int Height = baseControl.Bounds.Height + (square.Height * 2) + 2;
            int Width = baseControl.Bounds.Width + (square.Width * 2) + 2;
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
            smallRects[1] = new Rectangle(new Point(this.Width - square.Width - 1, 0), square); //右上
            smallRects[2] = new Rectangle(new Point(0, this.Height - square.Height - 1), square); //左下
            smallRects[3] = new Rectangle(new Point(this.Width - square.Width - 1, this.Height - square.Height - 1), square); //右下
            smallRects[4] = new Rectangle(new Point(this.Width / 2 - 1, 0), square); //上中
            smallRects[5] = new Rectangle(new Point(this.Width / 2 - 1, this.Height - square.Height - 1), square); //下中
            smallRects[6] = new Rectangle(new Point(0, this.Height / 2 - 1), square); //左中
            smallRects[7] = new Rectangle(new Point(square.Width + baseControl.Width + 1, this.Height / 2 - 1), square); //右中

            /* 四条边线 */
            linePoints[0] = new Point(square.Width / 2, square.Height / 2); //左上
            linePoints[1] = new Point(this.Width - square.Width / 2 - 1, square.Height / 2); //右上
            linePoints[2] = new Point(this.Width - square.Width / 2 - 1, this.Height - square.Height / 2-1); //右下
            linePoints[3] = new Point(square.Width / 2, this.Height - square.Height / 2 - 1); //左下
            linePoints[4] = new Point(square.Width / 2, square.Height / 2); //左上

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

            sideRects[0] = new Rectangle(0, 0, this.Width - square.Width - 1, square.Height + 1); //上边框
            sideRects[1] = new Rectangle(0, square.Height + 1, square.Width + 1, this.Height - square.Height - 1); //左边框
            sideRects[2] = new Rectangle(square.Width + 1, this.Height - square.Height - 1, this.Width - square.Width - 1, square.Height + 1); //下边框
            sideRects[3] = new Rectangle(this.Width - square.Width - 1, 0, square.Width + 1, this.Height - square.Height - 1); //右边框

            path.AddRectangle(sideRects[0]);
            path.AddRectangle(sideRects[1]);
            path.AddRectangle(sideRects[2]);
            path.AddRectangle(sideRects[3]);

            return path;
        }
        #endregion

        /// <summary>
        /// 绘图
        /// </summary>
        public void Draw()
        {
            Console.WriteLine("FrameControl:" + "InitializeComponent:");

            g = this.CreateGraphics();

            this.BringToFront();

            Pen pen = new Pen(Color.Black);
            pen.DashStyle = DashStyle.Dot;//设置为虚线,用虚线画四边，模拟微软效果
            g.DrawLines(pen, linePoints);//绘制四条边线
            g.FillRectangles(Brushes.White, smallRects); //填充8个小矩形的内部

            g.DrawRectangles(Pens.Black, smallRects);  //绘制8个小矩形的黑色边线
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
            int x = this.Left + square.Width+1;
            int y = this.Top + square.Height+1;

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
                Cursor.Current = Cursors.SizeNESW;
                mpoc = MousePosOnCtrl.TOPRIGHT;
            }
            else if (smallRects[2].Contains(point))
            {
                Cursor.Current = Cursors.SizeNESW;
                mpoc = MousePosOnCtrl.BOTTOMLEFT;
            }
            else if (smallRects[3].Contains(point))
            {
                Cursor.Current = Cursors.SizeNWSE;
                mpoc = MousePosOnCtrl.BOTTOMRIGHT;
            }
            else if (sideRects[0].Contains(point))
            {
                Cursor.Current = Cursors.SizeNS;
                mpoc = MousePosOnCtrl.TOP;
            }
            else if (sideRects[1].Contains(point))
            {
                Cursor.Current = Cursors.SizeWE;
                mpoc = MousePosOnCtrl.LEFT;
            }
            else if (sideRects[2].Contains(point))
            {
                Cursor.Current = Cursors.SizeNS;
                mpoc = MousePosOnCtrl.BOTTOM;
            }
            else if (sideRects[3].Contains(point))
            {
                Cursor.Current = Cursors.SizeWE;
                mpoc = MousePosOnCtrl.RIGHT;
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
            int baseControlTop = baseControl.Top;
            int baseControlLeft = baseControl.Left;
            int baseControlWidth = baseControl.Width;
            int baseControlHeight = baseControl.Height;
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

            //ControlMouseUpNotify(this, EventArgs.Empty);

            baseControl.Bounds = new Rectangle(baseControlLeft, baseControlTop, baseControlWidth, baseControlHeight);
            //baseControl.Left = left;
            //baseControl.Top = top;
            //baseControl.Width = width;
            //baseControl.Height = height;
            //baseControl.Refresh();
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
            CreateBounds();
            this.Visible = true;
            Draw();

            ControlMouseUpNotify(this, EventArgs.Empty);
        }
        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrameControl
            // 
            this.Name = "FrameControl";
            this.Size = new System.Drawing.Size(129, 170);
            this.ResumeLayout(false);

            //Console.WriteLine("FrameControl:" + "InitializeComponent:");
        }

        public delegate void ControlMouseUpEventDelegate(object sender, EventArgs e);
        public event ControlMouseUpEventDelegate ControlMouseUpEvent;

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
