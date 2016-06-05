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
    class STSliderSwitch : STControl
    {
        private SliderSwitchNode node;

        private const int PADDING = 5;
        private const int SLIDER_EDGE_WIDTH = 3;
        private const int SLIDER_WIDTH = 40;

        public STSliderSwitch()
        {

        }

        public STSliderSwitch(SliderSwitchNode node)
            : base(node)
        {
            this.node = node;

            this.MinimumSize = new Size(150, 30);
            this.MaximumSize = new Size(500, 60);

            this.Location = new Point(this.node.Left, this.node.Top);
            this.Size = new Size(this.node.Width, this.node.Height);

            //this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //this.BackColor = Color.Transparent;
            base.OnPaint(e);

            Graphics g = e.Graphics;
            //g.Clear(this.BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Region = new System.Drawing.Region(GetRoundRectangle(rect, this.node.Radius)); // 圆角矩形

            Color backColor = Color.FromArgb((int)(this.node.Alpha * 255), ColorTranslator.FromHtml(this.node.BackgroundColor));

            /* SliderSwitch的长条形主体 */
            int x = 0;
            int y = SLIDER_EDGE_WIDTH;  // 
            int width = this.Width;
            int height = this.Height - 2 * y;
            Rectangle rect1 = new Rectangle(x, y, width, height);
            if ((null == this.node.BackgroundImage) || (string.Empty == this.node.BackgroundImage))
            {
                if (UIEditor.Entity.ViewNode.EFlatStyle.Stereo == this.node.FlatStyle)
                {
                    /* 绘制立体效果，三色渐变 */
                    LinearGradientBrush brush = new LinearGradientBrush(rect1, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
                    Color[] colors = new Color[3];
                    colors[0] = ColorHelper.changeBrightnessOfColor(backColor, 100);
                    colors[1] = backColor;
                    colors[2] = ColorHelper.changeBrightnessOfColor(backColor, -50);
                    ColorBlend blend = new ColorBlend();
                    blend.Positions = new float[] { 0.0f, 0.3f, 1.0f };
                    blend.Colors = colors;
                    brush.InterpolationColors = blend;
                    FillRoundRectangle(g, brush, rect1, this.node.Radius, .0f);
                    //g.FillRegion(brush, Region);
                    brush.Dispose();
                }
                else if (UIEditor.Entity.ViewNode.EFlatStyle.Flat == this.node.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    FillRoundRectangle(g, brush, rect1, this.node.Radius, .0f);
                    //g.FillRegion(brush, Region);
                }
            }

            /* 左图标 */
            x = PADDING;  // 偏移为5
            y = SLIDER_EDGE_WIDTH + PADDING;  // 
            height = this.Height - 2 * y;   // 计算出高度
            width = height;     // 计算出宽度
            Image img = null;
            if (null != this.node.LeftImage)
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.node.LeftImage));
            }
            if (null != img)
            {
                g.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), x, y);
            }

            /* 右图标 */
            x = this.Width - PADDING - width;
            img = null;
            if (null != this.node.RightImage)
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.node.RightImage));
            }
            if (null != img)
            {
                g.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), x, y);
            }

            /* 中间滑块 */
            width = SLIDER_WIDTH;
            x = this.Width / 2 - width / 2;
            y = 0;
            height = this.Height;
            Rectangle rect2 = new Rectangle(x, y, width, height);
            Color sliderColor = ColorHelper.changeBrightnessOfColor(backColor, 70);
            LinearGradientBrush sliderBrush = new LinearGradientBrush(rect2, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
            Color[] sliderColors = new Color[3];
            sliderColors[0] = ColorHelper.changeBrightnessOfColor(sliderColor, 100);
            sliderColors[1] = sliderColor;
            sliderColors[2] = ColorHelper.changeBrightnessOfColor(sliderColor, -30);
            ColorBlend sliderBlend = new ColorBlend();
            sliderBlend.Positions = new float[] { 0.0f, 0.3f, 1.0f };
            sliderBlend.Colors = sliderColors;
            sliderBrush.InterpolationColors = sliderBlend;
            FillRoundRectangle(g, sliderBrush, rect2, this.node.Radius, .0f);
            sliderBrush.Dispose();
        }
    }
}
