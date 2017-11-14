using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.Drawing
{
    public class STPanel : Panel
    {
        public enum ControlState
        {
            Move,
            Normal
        }

        #region
        public ViewNode node { get; set; }

        public bool IsSelected { get; set; }

        public ControlState State { get; set; }

        #endregion

        public STPanel()
        {

        }

        public STPanel(ViewNode node)
        {
            this.node = node;
            //this.State = ControlState.Normal;

            this.SetStyle(
                ControlStyles.UserPaint |  //控件自行绘制，而不使用操作系统的绘制
                //ControlStyles.ContainerControl |
                ControlStyles.AllPaintingInWmPaint | //忽略擦出的消息，减少闪烁。
                ControlStyles.OptimizedDoubleBuffer |//在缓冲区上绘制，不直接绘制到屏幕上，减少闪烁。
                ControlStyles.ResizeRedraw | //控件大小发生变化时，重绘。                  
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.DoubleBuffer,
                true);//支持透明背景颜色
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
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
    }
}


