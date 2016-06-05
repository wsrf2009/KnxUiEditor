using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.Entity;
using UIEditor.Entity.Control;

namespace UIEditor.SationUIControl
{
    class STSwitch : STControl
    {
        private const int PADDING = 8;

        #region 属性
        private SwitchNode node { get; set; }
        #endregion

        public STSwitch()
        {

        }

        public STSwitch(SwitchNode node)
            : base(node)
        {
            this.node = node;

            this.MinimumSize = new Size(60, 30);
            this.MaximumSize = new Size(300, 60);

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

            Color backColor = Color.White;
            if (null != this.node.ColorOn)
            {
                backColor = Color.FromArgb((int)(this.node.Alpha * 255), ColorTranslator.FromHtml(this.node.ColorOn));
            }
            else if (null != this.node.ColorOff)
            {
                backColor = Color.FromArgb((int)(this.node.Alpha * 255), ColorTranslator.FromHtml(this.node.ColorOff));
            }
            else
            {
                backColor = Color.FromArgb((int)(this.node.Alpha * 255), ColorTranslator.FromHtml(this.node.BackgroundColor));
            }
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
                    //g.FillRegion(brush, Region);
                    FillRoundRectangle(g, brush, rect, this.node.Radius, .0f);
                    brush.Dispose();
                }
                else if (UIEditor.Entity.ViewNode.EFlatStyle.Flat == this.node.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    //g.FillRegion(brush, Region);
                    FillRoundRectangle(g, brush, rect, this.node.Radius, .0f);
                }
            }

            if (this.node.DisplayBorder && (this.node.Radius == 0))
            {
                Color borderColor = ColorTranslator.FromHtml(this.node.BorderColor);
                DrawRoundRectangle(g, new Pen(borderColor, 1), rect, this.node.Radius, 1.0f);
            }

            /* 图标 */
            //int x = PADDING;
            //int y = PADDING;
            //int width = 0;
            int height = this.Height*3/5;
            int y = (this.Height - height) / 2;
            int x = y;
            int width = height;
            Image img = null;
            if (null != this.node.ImageOn)
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.node.ImageOn));
            }
            else if (null != this.node.ImageOff)
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.node.ImageOff));
            }
            if (null != img)
            {
                height = this.Height - 2 * y;
                width = height;
                g.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), x, y);
            }

            /* 文本 */
            if (null != this.node.Text)
            {
                if (null != img)
                {
                    x += width + PADDING;
                    width = this.Width - x - PADDING;
                    height = this.Height - 2 * y;
                }
                else
                {
                    width = this.Width - 2 * x;
                    height = this.Height - 2 * y;
                }

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
