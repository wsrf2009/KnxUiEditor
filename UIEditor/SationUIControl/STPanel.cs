using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.SationUIControl
{
    public class STPanel : Panel
    {

        #region
        private ViewNode node { get; set; }

        //public override string Text { get; set; }

        //public int Left { get; set; }

        //public int Top { get; set; }

        //public int Width { get; set; }

        //public int Height { get; set; }

        //public double Alpha { get; set; }

        //public int Radius { get; set; }

        //public Structure.FlatStyle FlatStyle { get; set; }

        //public Color BackgroundColor { get; set; }

        ////public string BackgroundImage { get; set; }

        //public Color FontColor { get; set; }

        //public int FontSize { get; set; }
        #endregion

        public STPanel()
        {

        }

        public STPanel(ViewNode node)
        {
            this.node = node;
            //this.Text = node.Text;
            //this.Left = node.Left;
            //this.Top = node.Top;
            //this.Width = node.Width;
            //this.Height = node.Height;
            //this.Alpha = node.Alpha;
            //this.Radius = node.Radius;
            //this.FlatStyle = node.FlatStyle;
            //this.BackgroundColor = Color.FromArgb((int)(node.Alpha * 255), ColorTranslator.FromHtml(node.BackgroundColor));
            //this.BackgroundImage = node.BackgroundImage;
            //this.FontColor = ColorTranslator.FromHtml(node.FontColor);
            //this.FontSize = node.FontSize;

            this.SetStyle(
                ControlStyles.UserPaint |  //控件自行绘制，而不使用操作系统的绘制
                //ControlStyles.ContainerControl |
                ControlStyles.AllPaintingInWmPaint | //忽略擦出的消息，减少闪烁。
                ControlStyles.OptimizedDoubleBuffer |//在缓冲区上绘制，不直接绘制到屏幕上，减少闪烁。
                ControlStyles.ResizeRedraw | //控件大小发生变化时，重绘。                  
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.DoubleBuffer,
                true);//支持透明背景颜色

            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //this.Location = new Point(this.node.Left, this.node.Top);
            //this.Size = new Size(this.node.Width, this.node.Height);

            //Invalidate();
            //this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Graphics g = e.Graphics;
            //g.Clear(this.BackColor);

            Console.WriteLine("text:" + this.node.Text);

            //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            //Region = new System.Drawing.Region(GetRoundRectangle(rect, this.node.Radius)); // 圆角矩形

            //if ((null == this.node.BackgroundImage) || (string.Empty == this.node.BackgroundImage))
            //{
            //    if (Structure.FlatStyle.Stereo == this.node.FlatStyle)
            //    {
            //        Color backColor = Color.FromArgb((int)(this.node.Alpha * 255), ColorTranslator.FromHtml(this.node.BackgroundColor));

            //        //    /* 上半部分渐变 */
            //        //    Rectangle rect1 = new Rectangle(0, 0, this.node.Width, this.node.Height / 2);
            //        //    LinearGradientBrush bBackground1 =
            //        //         new LinearGradientBrush(rect1, ColorHelper.changeBrightnessOfColor(backColor, 100), backColor, LinearGradientMode.Vertical);
            //        //    g.FillRectangle(bBackground1, rect1);
            //        //    g.DrawRectangle(new Pen(backColor, .0f), rect1);
            //        //    bBackground1.Dispose();

            //        //    /* 下半部分渐变 */
            //        //    Rectangle rect2 = new Rectangle(0, this.node.Height / 2, this.node.Width, this.node.Height / 2);
            //        //    LinearGradientBrush bBackground2 =
            //        //         new LinearGradientBrush(rect2, backColor, ColorHelper.changeBrightnessOfColor(backColor, -50), LinearGradientMode.Vertical);
            //        //    g.FillRectangle(bBackground2, rect2);
            //        //    g.DrawRectangle(new Pen(backColor, .0f), rect2);
            //        //    bBackground2.Dispose();

            //        //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            //        /* 绘制立体效果，三色渐变 */
            //        LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
            //        Color[] colors = new Color[3];
            //        colors[0] = ColorHelper.changeBrightnessOfColor(backColor, 100);
            //        colors[1] = backColor;
            //        colors[2] = ColorHelper.changeBrightnessOfColor(backColor, -50);
            //        ColorBlend blend = new ColorBlend();
            //        blend.Positions = new float[] { 0.0f, 0.3f, 1.0f };
            //        blend.Colors = colors;
            //        brush.InterpolationColors = blend;
            //        g.FillRegion(brush, Region);
            //        brush.Dispose();
            //    }
            //    else if (Structure.FlatStyle.Flat == this.node.FlatStyle)
            //    {
            //        Color backColor = Color.FromArgb((int)(this.node.Alpha * 255), ColorTranslator.FromHtml(this.node.BackgroundColor));
            //        SolidBrush brush = new SolidBrush(backColor);
            //        g.FillRegion(brush, Region);
            //        //g.FillRectangle(brush, rect);
            //        //this.BackColor = backColor;
            //    }
            //}
            //else
            //{
            //    //Image img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.node.BackgroundImage));//建立要绘制的Image图像
            //    //Rectangle srcRect = new Rectangle(0, 0, img.Width, img.Height);//显示图像的位置
            //    //Rectangle desRect = new Rectangle(0, 0, this.Width, this.Height);//显示图像那一部分
            //    //GraphicsUnit units = GraphicsUnit.Pixel;//源矩形的度量单位设置为像素
            //    //g.DrawImage(img, desRect, srcRect, units);//显示

            //}
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

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    base.OnPaintBackground(e);

        //    //Graphics g = e.Graphics;
        //    //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

        //    //Color backColor = Color.FromArgb((int)(this.node.Alpha * 255), ColorTranslator.FromHtml(this.node.BackgroundColor));
        //    //SolidBrush brush = new SolidBrush(backColor);
        //    //g.FillRegion(brush, Region);

        //}

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
                roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
                roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
                roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2-borderWidth, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
                roundedRect.AddLine(rect.Right-borderWidth, rect.Y + cornerRadius * 2, rect.Right-borderWidth, rect.Y + rect.Height - cornerRadius * 2);
                roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2-borderWidth, rect.Y + rect.Height - cornerRadius * 2-borderWidth, cornerRadius * 2, cornerRadius * 2, 0, 90);
                roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom-borderWidth, rect.X + cornerRadius * 2, rect.Bottom-borderWidth);
                roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2-borderWidth, cornerRadius * 2, cornerRadius * 2, 90, 90);
                roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            }
            else
            {
                roundedRect.AddLine(rect.X, rect.Y, rect.Right, rect.Y);
                roundedRect.AddLine(rect.Right-borderWidth, rect.Y , rect.Right-borderWidth, rect.Y + rect.Height);
                roundedRect.AddLine(rect.Right, rect.Bottom-borderWidth, rect.X, rect.Bottom-borderWidth);
                roundedRect.AddLine(rect.X, rect.Bottom, rect.X, rect.Y);
            }
            roundedRect.CloseFigure();
            return roundedRect;
        }

        public void RefreshUI()
        {
            //this.Location = new Point(this.node.Left, this.node.Top);
            //this.Size = new Size(this.node.Width, this.node.Height);
            //this.Text = node.Text;
            //this.Left = node.Left;
            //this.Top = node.Top;
            //this.Width = node.Width;
            //this.Height = node.Height;
            //this.Alpha = node.Alpha;
            //this.Radius = node.Radius;
            //this.FlatStyle = node.FlatStyle;
            //this.BackgroundColor = Color.FromArgb((int)(node.Alpha * 255), ColorTranslator.FromHtml(node.BackgroundColor));
            ////this.BackgroundImage = node.BackgroundImage;
            //this.FontColor = ColorTranslator.FromHtml(node.FontColor);
            //this.FontSize = node.FontSize;

            if ((null != this.node.BackgroundImage) && (string.Empty != this.node.BackgroundImage))
            {
                this.BackgroundImage = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.node.BackgroundImage));
            }
            else
            {
                //this.BackColor = Color.FromArgb((int)(node.Alpha * 255), ColorTranslator.FromHtml(node.BackgroundColor));
                this.BackColor = Color.Transparent;
            }

            this.Location = new Point(this.node.Left, this.node.Top);
            this.Size = new Size(this.node.Width, this.node.Height);

            this.Refresh();
            Console.WriteLine("RefreshUI:" + this.node.Text);
        }
    }
}


