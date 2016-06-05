﻿using System;
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
    class STBlinds : STControl
    {
        private const int PADDING = 5;
        private const int SUBVIEW_WIDTH = 40;

        private BlindsNode node;

        public STBlinds() { }

        public STBlinds(BlindsNode node)
            : base(node)
        {
            this.node = node;

            this.MinimumSize = new Size(80, 30);
            this.MaximumSize = new Size(300, 80);

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

            /* SliderSwitch的长条形主体 */
            int x = 0;
            int y = 0;  // 
            int width = this.Width;
            int height = this.Height;
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
                    //FillRoundRectangle(g, brush, rect1, this.node.Radius);
                    g.FillRegion(brush, Region);
                    brush.Dispose();
                }
                else if (UIEditor.Entity.ViewNode.EFlatStyle.Flat == this.node.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    //FillRoundRectangle(g, brush, rect1, this.node.Radius);
                    g.FillRegion(brush, Region);
                }
            }

            /* 左图标 */
            x = PADDING;  // 偏移为5
            y = PADDING;  // 
            height = this.Height - 2 * y;   // 计算出高度
            width = this.Height > SUBVIEW_WIDTH ? this.Height : SUBVIEW_WIDTH;     // 计算出宽度
            width -= 2 * x;
            Image img = null;
            if (null != this.node.LeftImage)
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.node.LeftImage));
            }
            if (null != img)
            {
                g.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), x, y);
            }
            if (null != this.node.LeftText)
            {
                Color fontColor = ColorTranslator.FromHtml(this.node.LeftTextFontColor);
                Font font = new Font("宋体", this.node.LeftTextFontSize);
                StringFormat format = new StringFormat();

                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Size size = TextRenderer.MeasureText(this.node.LeftText, font);
                //x = (this.Width - size.Width) / 2;
                //y = PADDING;
                Rectangle rectText = new Rectangle(x, y, width, height);
                g.DrawString(this.node.LeftText, font, new SolidBrush(fontColor), rectText, format);
            }

            /* 右图标 */
            x = this.Width - PADDING - width;
            /*Image*/
            img = null;
            if (null != this.node.RightImage)
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.node.RightImage));
            }
            if (null != img)
            {
                g.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), x, y);
            }
            if (null != this.node.RightText)
            {
                Color fontColor = ColorTranslator.FromHtml(this.node.RightTextFontColor);
                Font font = new Font("宋体", this.node.RightTextFontSize);
                StringFormat format = new StringFormat();

                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Size size = TextRenderer.MeasureText(this.node.RightText, font);
                //x = (this.Width - size.Width) / 2;
                //y = PADDING;
                Rectangle rectText = new Rectangle(x, y, width, height);
                g.DrawString(this.node.RightText, font, new SolidBrush(fontColor), rectText, format);
            }

            /* 中间文本 */
            if (null != this.node.Text)
            {
                Color fontColor = ColorTranslator.FromHtml(this.node.FontColor);
                Font font = new Font("宋体", this.node.FontSize);
                StringFormat format = new StringFormat();

                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Size size = TextRenderer.MeasureText(this.node.Text, font);
                x = (this.Width - size.Width) / 2;
                y = PADDING;
                Rectangle rectText = new Rectangle(x, y, size.Width, height);
                g.DrawString(this.node.Text, font, new SolidBrush(fontColor), rectText, format);
            }
        }
    }
}