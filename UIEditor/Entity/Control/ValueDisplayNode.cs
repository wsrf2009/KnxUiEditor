using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.Reflection;
using System.ComponentModel;

using System.Collections.Generic;
using System.Drawing;
using UIEditor.Entity;
using UIEditor.PropertyGridEditor;
using System.Drawing.Design;
using Structure;
using Structure.Control;
using UIEditor.Component;
using System.Drawing.Drawing2D;
using UIEditor;
using UIEditor.UserClass;
using Utils;


namespace UIEditor.Entity.Control
{
    [TypeConverter(typeof(ValueDisplayNode.PropertyConverter))]
    [Serializable]
    public class ValueDisplayNode : ControlBaseNode
    {
        #region 常量
        private const int PADDING = 2;
        #endregion

        #region 变量
        private static int _index = 0;
        #endregion

        #region 属性
        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressSingleReadEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(MultiSelectedAddressConverter))]
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        /// <summary>
        /// 用于添加单元标识符显示的值，例如一个可选字段：用于显示当前温度，”°C”可以插入。
        /// </summary>
        [TypeConverter(typeof(UIEditor.PropertyGridTypeConverter.EnumConverter))]
        public EMeasurementUnit Unit { get; set; }

        public EDecimalDigit DecimalDigit { get; set; }

        [EditorAttribute(typeof(PropertyGridSTFontEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(STFontConverter))]
        public STFont ValueFont { get; set; }
        #endregion

        #region 构造函数
        public ValueDisplayNode()
            : base()
        {
            _index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxValueDisplayType;

            this.Text = UIResMang.GetString("TextValueDisplay");
            this.Title = UIResMang.GetString("TextValueDisplay") + _index;
            SetText(this.Title);

            this.Size = new Size(180, 40);

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.Unit = EMeasurementUnit.Centigrade;
            this.DecimalDigit = EDecimalDigit.Zero;

            this.ValueFont = new STFont(Color.Yellow, 28);
        }

        public ValueDisplayNode(KNXValueDisplay knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxValueDisplayType;
            SetText(this.Title);

            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();

            if (ImportedHelper.IsLessThan2_1_8() && knx.Unit > (int)EMeasurementUnit.Centigrade)
            {
                this.Unit = (EMeasurementUnit)Enum.ToObject(typeof(EMeasurementUnit), knx.Unit - 1);
            }
            else
            {
                this.Unit = (EMeasurementUnit)Enum.ToObject(typeof(EMeasurementUnit), knx.Unit);
            }

            if (ImportedHelper.IsLessThan2_5_7())
            {
                this.ValueFont = this.TitleFont.Clone();
            }
            else
            {
                this.ValueFont = new STFont(knx.ValueFont);
            }

            this.DecimalDigit = (EDecimalDigit)Enum.ToObject(typeof(EDecimalDigit), knx.DecimalDigit);
        }

        public ValueDisplayNode(KNXValueDisplay knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            ValueDisplayNode node = base.Clone() as ValueDisplayNode;
            node.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            foreach (var item in this.ReadAddressId)
            {
                node.ReadAddressId.Add(item.Key, item.Value);
            }

            node.Unit = this.Unit;
            node.DecimalDigit = this.DecimalDigit;
            node.ValueFont = this.ValueFont;

            return node;
        }

        public override object Copy()
        {
            ValueDisplayNode node = base.Copy() as ValueDisplayNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextValueDisplay"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextValueDisplay"));
        }

        public override void DrawAt(Graphics g, float ratio, bool preview)
        {
            base.DrawAt(g, ratio, preview);

            if (ControlState.Move == this.State)
            {
                Pen pen = new Pen(Color.Navy, 2.0f);
                DrawRoundRectangle(g, pen, this.RectInPage, this.Radius, 1.0f, ratio);
            }
            else
            {
                Rectangle rect = new Rectangle(Point.Empty, this.RectInPage.Size);
                Bitmap bm = new Bitmap(this.RectInPage.Width, this.RectInPage.Height);
                Graphics gp = Graphics.FromImage(bm);

                Color backColor = Color.FromArgb((int)(this.Alpha * 255), this.BackgroundColor);

                if (EFlatStyle.Stereo == this.FlatStyle)
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
                    FillRoundRectangle(gp, brush, rect, this.Radius, 1.0f, ratio);
                    brush.Dispose();
                }
                else if (EFlatStyle.Flat == this.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    FillRoundRectangle(gp, brush, rect, this.Radius, 1.0f, ratio);
                    brush.Dispose();
                }

                int p = (int)Math.Round(PADDING * ratio, 0);

                Color fontColor = this.ValueFont.Color;
                Font font = this.ValueFont.GetFont(ratio);
                StringFormat format = new StringFormat();

                int x = 0;
                int y = 0; ;
                int width = rect.Width - 2 * x;
                int height = rect.Height - 2 * y;

                /* 文本 */
                string valueString = "88";
                if (EDecimalDigit.One == this.DecimalDigit)
                {
                    valueString = "88.8";
                }
                else if (EDecimalDigit.Two == this.DecimalDigit)
                {
                    valueString = "88.88";
                }
                valueString += " " + this.Unit.GetDescription();

                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Rectangle rectText = new Rectangle(x, y, width, height);
                gp.DrawString(valueString, font, new SolidBrush(fontColor), rectText, format);

                if (EBool.Yes == this.DisplayBorder)
                {
                    Color borderColor = this.BorderColor;
                    DrawRoundRectangle(gp, new Pen(borderColor, 1), rect, this.Radius, 1.0f, ratio);
                }

                g.DrawImage(bm,
                    this.VisibleRectInPage,
                    new Rectangle(new Point(this.VisibleRectInPage.X - this.RectInPage.X, this.VisibleRectInPage.Y - this.RectInPage.Y), this.VisibleRectInPage.Size),
                    GraphicsUnit.Pixel);

                if (!preview)
                {
                    this.FrameIsVisible = false;

                    if (this.IsThisSelected)
                    {
                        this.SetFrame();
                        Pen pen = new Pen(Color.LightGray, 1.0f);
                        pen.DashStyle = DashStyle.Dot;//设置为虚线,用虚线画四边，模拟微软效果
                        g.DrawLine(pen, this.LinePoints[0], this.LinePoints[1]);
                        g.DrawLine(pen, this.LinePoints[2], this.LinePoints[3]);
                        g.DrawLine(pen, this.LinePoints[4], this.LinePoints[5]);
                        g.DrawLine(pen, this.LinePoints[6], this.LinePoints[7]);
                        g.DrawLine(pen, this.LinePoints[8], this.LinePoints[9]);
                        g.DrawLine(pen, this.LinePoints[10], this.LinePoints[11]);
                        g.DrawLine(pen, this.LinePoints[12], this.LinePoints[13]);
                        g.DrawLine(pen, this.LinePoints[14], this.LinePoints[15]);

                        g.FillRectangles(Brushes.White, this.SmallRects); //填充8个小矩形的内部
                        g.DrawRectangles(Pens.Black, this.SmallRects);  //绘制8个小矩形的黑色边线

                        this.FrameIsVisible = true;
                    }
                }
            }
        }
        #endregion

        #region 转为KNX
        public KNXValueDisplay ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXValueDisplay();

            base.ToKnx(knx, worker);

            knx.ReadAddressId = this.ReadAddressId;
            knx.Unit = (int)this.Unit;
            knx.DecimalDigit = (int)this.DecimalDigit;
            knx.ValueFont = this.ValueFont.ToKnx();

            return knx;
        }

        public KNXValueDisplay ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXValueDisplay knx = this.ToKnx(worker);
            knx.Left = this.LocationInPageFact.X - RelPoint.X;
            knx.Top = this.LocationInPageFact.Y - RelPoint.Y;

            knx.ReadAddressId.Clear();

            return knx;
        }
        #endregion

        #region 属性框显示
        private class PropertyConverter : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(value, true);

                List<PropertyDescriptor> list = new List<PropertyDescriptor>();

                STControlPropertyDescriptor PropLocation = new STControlPropertyDescriptor(collection["Location"]);
                PropLocation.SetCategory(UIResMang.GetString("CategoryLayout"));
                PropLocation.SetDisplayName(UIResMang.GetString("PropLocation"));
                PropLocation.SetDescription(UIResMang.GetString(""));
                list.Add(PropLocation);

                STControlPropertyDescriptor PropSize = new STControlPropertyDescriptor(collection["Size"]);
                PropSize.SetCategory(UIResMang.GetString("CategoryLayout"));
                PropSize.SetDisplayName(UIResMang.GetString("PropSize"));
                PropSize.SetDescription(UIResMang.GetString(""));
                list.Add(PropSize);

                STControlPropertyDescriptor PropBorderWidth = new STControlPropertyDescriptor(collection["DisplayBorder"]);
                PropBorderWidth.SetCategory(UIResMang.GetString("CategoryBorder"));
                PropBorderWidth.SetDisplayName(UIResMang.GetString("PropDisplayBorder"));
                PropBorderWidth.SetDescription(UIResMang.GetString(""));
                list.Add(PropBorderWidth);

                STControlPropertyDescriptor PropBorderColor = new STControlPropertyDescriptor(collection["BorderColor"]);
                PropBorderColor.SetCategory(UIResMang.GetString("CategoryBorder"));
                PropBorderColor.SetDisplayName(UIResMang.GetString("PropBorderColor"));
                PropBorderColor.SetDescription(UIResMang.GetString(""));
                list.Add(PropBorderColor);

                STControlPropertyDescriptor PropAlpha = new STControlPropertyDescriptor(collection["Alpha"]);
                PropAlpha.SetCategory(UIResMang.GetString("CategoryAppearance"));
                PropAlpha.SetDisplayName(UIResMang.GetString("PropAlpha"));
                PropAlpha.SetDescription(UIResMang.GetString("DescriptionForPropAlpha"));
                list.Add(PropAlpha);

                STControlPropertyDescriptor PropRadius = new STControlPropertyDescriptor(collection["Radius"]);
                PropRadius.SetCategory(UIResMang.GetString("CategoryAppearance"));
                PropRadius.SetDisplayName(UIResMang.GetString("PropRadius"));
                PropRadius.SetDescription(UIResMang.GetString("DescriptionForPropRadius"));
                list.Add(PropRadius);

                STControlPropertyDescriptor PropBackColor = new STControlPropertyDescriptor(collection["BackgroundColor"]);
                PropBackColor.SetCategory(UIResMang.GetString("CategoryAppearance"));
                PropBackColor.SetDisplayName(UIResMang.GetString("PropBackColor"));
                //PropBackColor.SetDescription(UIResMang.GetString("DescriptionForPropBackgroundColor"));
                list.Add(PropBackColor);

                STControlPropertyDescriptor PropFlatStyle = new STControlPropertyDescriptor(collection["FlatStyle"]);
                PropFlatStyle.SetCategory(UIResMang.GetString("CategoryAppearance"));
                PropFlatStyle.SetDisplayName(UIResMang.GetString("PropFlatStyle"));
                PropFlatStyle.SetDescription(UIResMang.GetString("DescriptionForPropFlatStyle"));
                list.Add(PropFlatStyle);

                STControlPropertyDescriptor PropEtsReadAddressId = new STControlPropertyDescriptor(collection["ReadAddressId"]);
                PropEtsReadAddressId.SetCategory(UIResMang.GetString("CategoryGroupAddress"));
                PropEtsReadAddressId.SetDisplayName(UIResMang.GetString("PropEtsReadAddressId"));
                PropEtsReadAddressId.SetDescription(UIResMang.GetString(""));
                list.Add(PropEtsReadAddressId);

                STControlPropertyDescriptor PropMeasurementUnit = new STControlPropertyDescriptor(collection["Unit"]);
                PropMeasurementUnit.SetCategory(UIResMang.GetString("CategoryValue"));
                PropMeasurementUnit.SetDisplayName(UIResMang.GetString("PropMeasurementUnit"));
                PropMeasurementUnit.SetDescription(UIResMang.GetString(""));
                list.Add(PropMeasurementUnit);

                STControlPropertyDescriptor PropDecimalDigit = new STControlPropertyDescriptor(collection["DecimalDigit"]);
                PropDecimalDigit.SetCategory(UIResMang.GetString("CategoryValue"));
                PropDecimalDigit.SetDisplayName(UIResMang.GetString("PropDecimalDigit"));
                PropDecimalDigit.SetDescription(UIResMang.GetString(""));
                list.Add(PropDecimalDigit);

                STControlPropertyDescriptor PropValueFont = new STControlPropertyDescriptor(collection["ValueFont"]);
                PropValueFont.SetCategory(UIResMang.GetString("CategoryValue"));
                PropValueFont.SetDisplayName(UIResMang.GetString("PropValueFont"));
                list.Add(PropValueFont);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion
    }
}
