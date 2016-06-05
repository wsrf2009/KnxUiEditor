using Structure.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.Entity.Control;

namespace UIEditor.SationUIControl
{
    class STValueDisplay : STControl
    {
        private const int PADDING = 5;

        private ValueDisplayNode node { get; set; }

        public STValueDisplay() { }

        public STValueDisplay(ValueDisplayNode node)
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

            Color fontColor = ColorTranslator.FromHtml(this.node.FontColor);
            Font font = new Font("宋体", this.node.FontSize);
            StringFormat format = new StringFormat();

            int x = PADDING;
            int y = PADDING;
            int width = this.Width - 2 * x;
            int height = this.Height - 2 * y;

            /* 文本 */
            //if (null != this.node.Text)
            //{
            //    format.Alignment = StringAlignment.Center;
            //    format.LineAlignment = StringAlignment.Center;
            //    Size size = TextRenderer.MeasureText(this.node.Text, font);
            //    Rectangle rectText = new Rectangle(x, y, size.Width, height);
            //    g.DrawString(this.node.Text, font, new SolidBrush(fontColor), rectText, format);
            //}

            string valueString = "88";
            //if (KNXValueDisplay.EDisplayAccurancy.None == this.node.DecimalPlaces)
            //{
            //    valueString = "888";
            //}
            //else if (KNXValueDisplay.EDisplayAccurancy.Bit1 == this.node.DecimalPlaces)
            //{
            //    valueString = "88.8";
            //}

            if (null != valueString)
            {
                //var fields = typeof(UIEditor.Entity.Control.ValueDisplayNode.EMeasurementUnit).GetFields(BindingFlags.Static | BindingFlags.Public);
                //foreach (var fi in fields)
                //{
                //    fi.Attributes.GetDescription();
                //}

                //this.node.UnitString.GetDescription();
                valueString +=  /*this.node.Unit.GetDescription()*/this.node.Unit.GetDescription();

                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                //Size size = TextRenderer.MeasureText(valueString, font);
                //x = this.Width - x - size.Width;
                Rectangle rectText = new Rectangle(x, y, width, height);
                g.DrawString(valueString, font, new SolidBrush(fontColor), rectText, format);
            }
        }
    }
}
