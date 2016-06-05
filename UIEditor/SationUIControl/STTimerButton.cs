using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.Entity.Control;

namespace UIEditor.SationUIControl
{
    class STTimerButton : STControl
    {
        private const int PADDING = 5;

        private TimerButtonNode node { get; set; }

        public STTimerButton() { }

        public STTimerButton(TimerButtonNode node)
            : base(node)
        {
            this.node = node;

            this.MinimumSize = new Size(80, 30);
            this.MaximumSize = new Size(300, 200);

            this.Location = new Point(this.node.Left, this.node.Top);
            this.Size = new Size(this.node.Width, this.node.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Region = new System.Drawing.Region(GetRoundRectangle(rect, this.node.Radius)); // 圆角矩形

            Color backColor = Color.FromArgb((int)(this.node.Alpha * 255), ColorTranslator.FromHtml(this.node.BackgroundColor));
            //Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            if ((null == this.node.BackgroundImage) || (string.Empty == this.node.BackgroundImage))
            {
                if (UIEditor.Entity.ViewNode.EFlatStyle.Stereo == this.node.FlatStyle)
                {
                    /* 绘制立体效果，三色渐变 */
                    LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
                    Color[] colors = new Color[3];
                    colors[0] = ColorHelper.changeBrightnessOfColor(backColor, 100);
                    colors[1] = backColor;
                    colors[2] = ColorHelper.changeBrightnessOfColor(backColor, -50);
                    ColorBlend blend = new ColorBlend();
                    blend.Positions = new float[] { 0.0f, 0.3f, 1.0f };
                    blend.Colors = colors;
                    brush.InterpolationColors = blend;
                    g.FillRegion(brush, Region);
                    brush.Dispose();
                }
                else if (UIEditor.Entity.ViewNode.EFlatStyle.Flat == this.node.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    g.FillRegion(brush, Region);
                }
            }

            /* 图标 */
            int x = PADDING;
            int y = PADDING;  // 到父视图顶部的距离
            int height = this.Height - 2 * y;   // 计算出高度
            int width = height;     // 计算出宽度
            Image img = null;
            if (null != this.node.Icon)
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.node.Icon));
            }
            if (null != img)
            {
                g.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), x, y);
            }

            /* 文本 */
            if (null != this.node.Text)
            {
                x += width + PADDING;
                y = PADDING;
                width = this.Width - x - PADDING;
                height = this.Height - 2 * y;

                Rectangle stateRect = new Rectangle(x, y, width, height);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                Color fontColor = ColorTranslator.FromHtml(this.node.FontColor);
                g.DrawString(this.node.Text, new Font("宋体", this.node.FontSize), new SolidBrush(fontColor), stateRect, sf);
            }
        }
    }
}
