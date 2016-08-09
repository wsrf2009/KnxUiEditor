using SourceGrid;
using Structure;
using Structure.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.PropertyGridEditor;

namespace UIEditor.Entity.Control
{
    [TypeConverter(typeof(DigitalAdjustmentNode.PropertyConverter))]
    [Serializable]
    class DigitalAdjustmentNode : ControlBaseNode
    {
        #region 常量
        private const int PADDING = 2;
        private const int SUBVIEW_WIDTH = 40;
        #endregion

        #region 变量
        private static int index = 0;
        #endregion

        #region 属性
        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressSingleReadEditor), typeof(UITypeEditor))]
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressMultiWriteEditor), typeof(UITypeEditor))]
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string LeftImage { get; set; }

        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string RightImage { get; set; }
        public Structure.Control.KNXDigitalAdjustment.EDigitalNumber DigitalNumber { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        [TypeConverter(typeof(UIEditor.Component.EnumConverter))]
        public EMeasurementUnit Unit { get; set; }
        #endregion

        #region 构造函数
        public DigitalAdjustmentNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextDigitalAdjustment") + index;

            this.Width = 150;
            this.Height = 45;
            //this.Size = new Size(150, 45);
            this.FlatStyle = EFlatStyle.Stereo;
            this.FontColor = Color.Yellow;// FrmMainHelp.ColorToHexStr(Color.Yellow);
            this.FontSize = 28;

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();

            this.LeftImage = "sl_left.png";
            this.RightImage = "sl_right.png";
            this.DigitalNumber = KNXDigitalAdjustment.EDigitalNumber.TwoDigit;
            this.MaxValue = 30;
            this.MinValue = 16;
            this.Unit = EMeasurementUnit.None;

            string FileLeftImage = Path.Combine(MyCache.ProjImagePath, this.LeftImage);
            if (!File.Exists(FileLeftImage))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.LeftImage), FileLeftImage);
            }

            string FileRightImage = Path.Combine(MyCache.ProjImagePath, this.RightImage);
            if (!File.Exists(FileRightImage))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.RightImage), FileRightImage);
            }

            Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxDigitalAdjustment;
        }

        public override object Clone()
        {
            DigitalAdjustmentNode node = base.Clone() as DigitalAdjustmentNode;
            node.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            foreach (var item in this.ReadAddressId)
            {
                node.ReadAddressId.Add(item.Key, item.Value);
            }
            node.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();
            foreach (var item in this.WriteAddressIds)
            {
                node.WriteAddressIds.Add(item.Key, item.Value);
            }

            node.LeftImage = this.LeftImage;
            node.RightImage = this.RightImage;
            node.DigitalNumber = this.DigitalNumber;
            node.MaxValue = this.MaxValue;
            node.MinValue = this.MinValue;
            node.Unit = this.Unit;

            return node;
        }

        public DigitalAdjustmentNode(KNXDigitalAdjustment knx)
            : base(knx)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxDigitalAdjustment;

            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();

            this.LeftImage = knx.LeftImage;
            this.RightImage = knx.RightImage;
            this.DigitalNumber = (KNXDigitalAdjustment.EDigitalNumber)Enum.ToObject(typeof(KNXDigitalAdjustment.EDigitalNumber), knx.DigitalNumber);
            this.MaxValue = knx.MaxValue;
            this.MinValue = knx.MinValue;
            this.Unit = (EMeasurementUnit)Enum.ToObject(typeof(EMeasurementUnit), knx.Unit);
        }

        protected DigitalAdjustmentNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        public KNXDigitalAdjustment ToKnx()
        {
            var knx = new KNXDigitalAdjustment();
            base.ToKnx(knx);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;

            knx.LeftImage = this.LeftImage;
            knx.RightImage = this.RightImage;
            knx.DigitalNumber = (int)this.DigitalNumber;
            knx.MaxValue = this.MaxValue;
            knx.MinValue = this.MinValue;
            knx.Unit = (int)this.Unit;

            return knx;
        }

        private class PropertyConverter : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(value, true);

                List<PropertyDescriptor> list = new List<PropertyDescriptor>();

                STControlPropertyDescriptor propText = new STControlPropertyDescriptor(collection["Text"]);
                propText.SetCategory(ResourceMng.GetString("CategoryAppearance"));
                propText.SetDisplayName(ResourceMng.GetString("PropText"));
                propText.SetDescription(ResourceMng.GetString("DescriptionForPropText"));
                list.Add(propText);

                STControlPropertyDescriptor PropX = new STControlPropertyDescriptor(collection["X"]);
                PropX.SetCategory(ResourceMng.GetString("CategoryLayout"));
                PropX.SetDisplayName(ResourceMng.GetString("PropX"));
                PropX.SetDescription(ResourceMng.GetString(""));
                list.Add(PropX);

                STControlPropertyDescriptor PropY = new STControlPropertyDescriptor(collection["Y"]);
                PropY.SetCategory(ResourceMng.GetString("CategoryLayout"));
                PropY.SetDisplayName(ResourceMng.GetString("PropY"));
                PropY.SetDescription(ResourceMng.GetString(""));
                list.Add(PropY);

                STControlPropertyDescriptor PropWidth = new STControlPropertyDescriptor(collection["Width"]);
                PropWidth.SetCategory(ResourceMng.GetString("CategoryLayout"));
                PropWidth.SetDisplayName(ResourceMng.GetString("PropWidth"));
                PropWidth.SetDescription(ResourceMng.GetString(""));
                list.Add(PropWidth);

                STControlPropertyDescriptor PropHeight = new STControlPropertyDescriptor(collection["Height"]);
                PropHeight.SetCategory(ResourceMng.GetString("CategoryLayout"));
                PropHeight.SetDisplayName(ResourceMng.GetString("PropHeight"));
                PropHeight.SetDescription(ResourceMng.GetString(""));
                list.Add(PropHeight);

                //STControlPropertyDescriptor PropLocation = new STControlPropertyDescriptor(collection["Location"]);
                //PropLocation.SetCategory(ResourceMng.GetString("CategoryLayout"));
                //PropLocation.SetDisplayName(ResourceMng.GetString("PropLocation"));
                //PropLocation.SetDescription(ResourceMng.GetString(""));
                //list.Add(PropLocation);

                //STControlPropertyDescriptor PropSize = new STControlPropertyDescriptor(collection["Size"]);
                //PropSize.SetCategory(ResourceMng.GetString("CategoryLayout"));
                //PropSize.SetDisplayName(ResourceMng.GetString("PropSize"));
                //PropSize.SetDescription(ResourceMng.GetString(""));
                //list.Add(PropSize);

                STControlPropertyDescriptor PropBorderWidth = new STControlPropertyDescriptor(collection["DisplayBorder"]);
                PropBorderWidth.SetCategory(ResourceMng.GetString("CategoryBorder"));
                PropBorderWidth.SetDisplayName(ResourceMng.GetString("PropDisplayBorder"));
                PropBorderWidth.SetDescription(ResourceMng.GetString(""));
                list.Add(PropBorderWidth);

                STControlPropertyDescriptor PropBorderColor = new STControlPropertyDescriptor(collection["BorderColor"]);
                PropBorderColor.SetCategory(ResourceMng.GetString("CategoryBorder"));
                PropBorderColor.SetDisplayName(ResourceMng.GetString("PropBorderColor"));
                PropBorderColor.SetDescription(ResourceMng.GetString(""));
                list.Add(PropBorderColor);

                STControlPropertyDescriptor PropAlpha = new STControlPropertyDescriptor(collection["Alpha"]);
                PropAlpha.SetCategory(ResourceMng.GetString("CategoryStyle"));
                PropAlpha.SetDisplayName(ResourceMng.GetString("PropAlpha"));
                PropAlpha.SetDescription(ResourceMng.GetString("DescriptionForPropAlpha"));
                list.Add(PropAlpha);

                STControlPropertyDescriptor PropRadius = new STControlPropertyDescriptor(collection["Radius"]);
                PropRadius.SetCategory(ResourceMng.GetString("CategoryStyle"));
                PropRadius.SetDisplayName(ResourceMng.GetString("PropRadius"));
                PropRadius.SetDescription(ResourceMng.GetString("DescriptionForPropRadius"));
                list.Add(PropRadius);

                STControlPropertyDescriptor PropBackColor = new STControlPropertyDescriptor(collection["BackgroundColor"]);
                PropBackColor.SetCategory(ResourceMng.GetString("CategoryAppearance"));
                PropBackColor.SetDisplayName(ResourceMng.GetString("PropBackColor"));
                PropBackColor.SetDescription(ResourceMng.GetString("DescriptionForPropBackgroundColor"));
                list.Add(PropBackColor);

                STControlPropertyDescriptor PropFlatStyle = new STControlPropertyDescriptor(collection["FlatStyle"]);
                PropFlatStyle.SetCategory(ResourceMng.GetString("CategoryStyle"));
                PropFlatStyle.SetDisplayName(ResourceMng.GetString("PropFlatStyle"));
                PropFlatStyle.SetDescription(ResourceMng.GetString("DescriptionForPropFlatStyle"));
                list.Add(PropFlatStyle);

                STControlPropertyDescriptor PropFontColor = new STControlPropertyDescriptor(collection["FontColor"]);
                PropFontColor.SetCategory(ResourceMng.GetString("CategoryAppearance"));
                PropFontColor.SetDisplayName(ResourceMng.GetString("PropFontColor"));
                PropFontColor.SetDescription(ResourceMng.GetString("DescriptionForPropFontColor"));
                list.Add(PropFontColor);

                STControlPropertyDescriptor PropFontSize = new STControlPropertyDescriptor(collection["FontSize"]);
                PropFontSize.SetCategory(ResourceMng.GetString("CategoryAppearance"));
                PropFontSize.SetDisplayName(ResourceMng.GetString("PropFontSize"));
                PropFontSize.SetDescription(ResourceMng.GetString("DescriptionForPropFontSize"));
                list.Add(PropFontSize);

                STControlPropertyDescriptor PropEtsWriteAddressIds = new STControlPropertyDescriptor(collection["WriteAddressIds"]);
                PropEtsWriteAddressIds.SetCategory("KNX");
                PropEtsWriteAddressIds.SetDisplayName(ResourceMng.GetString("PropEtsWriteAddressIds"));
                PropEtsWriteAddressIds.SetDescription(ResourceMng.GetString(""));
                list.Add(PropEtsWriteAddressIds);

                STControlPropertyDescriptor PropEtsReadAddressId = new STControlPropertyDescriptor(collection["ReadAddressId"]);
                PropEtsReadAddressId.SetCategory("KNX");
                PropEtsReadAddressId.SetDisplayName(ResourceMng.GetString("PropEtsReadAddressId"));
                PropEtsReadAddressId.SetDescription(ResourceMng.GetString(""));
                list.Add(PropEtsReadAddressId);

                STControlPropertyDescriptor PropHasTip = new STControlPropertyDescriptor(collection["HasTip"]);
                PropHasTip.SetCategory(ResourceMng.GetString("CategoryOperation"));
                PropHasTip.SetDisplayName(ResourceMng.GetString("PropHasTip"));
                PropHasTip.SetDescription(ResourceMng.GetString("DescriptionForPropHasTip"));
                list.Add(PropHasTip);

                STControlPropertyDescriptor PropTip = new STControlPropertyDescriptor(collection["Tip"]);
                PropTip.SetCategory(ResourceMng.GetString("CategoryOperation"));
                PropTip.SetDisplayName(ResourceMng.GetString("PropTip"));
                PropTip.SetDescription(ResourceMng.GetString("DescriptionForPropTip"));
                list.Add(PropTip);

                STControlPropertyDescriptor PropClickable = new STControlPropertyDescriptor(collection["Clickable"]);
                PropClickable.SetCategory(ResourceMng.GetString("CategoryOperation"));
                PropClickable.SetDisplayName(ResourceMng.GetString("PropClickable"));
                PropClickable.SetDescription(ResourceMng.GetString("DescriptionForPropClickable"));
                list.Add(PropClickable);

                STControlPropertyDescriptor PropLeftImage = new STControlPropertyDescriptor(collection["LeftImage"]);
                PropLeftImage.SetCategory(ResourceMng.GetString("CategoryDisplay"));
                PropLeftImage.SetDisplayName(ResourceMng.GetString("PropLeftImage"));
                PropLeftImage.SetDescription(ResourceMng.GetString("DescriptionForPropLeftImage"));
                list.Add(PropLeftImage);

                STControlPropertyDescriptor PropRightImage = new STControlPropertyDescriptor(collection["RightImage"]);
                PropRightImage.SetCategory(ResourceMng.GetString("CategoryDisplay"));
                PropRightImage.SetDisplayName(ResourceMng.GetString("PropRightImage"));
                PropRightImage.SetDescription(ResourceMng.GetString("DescriptionForPropRightImage"));
                list.Add(PropRightImage);

                //STControlPropertyDescriptor PropDigitalNumber = new STControlPropertyDescriptor(collection["DigitalNumber"]);
                //PropDigitalNumber.SetCategory(ResourceMng.GetString(""));
                //PropDigitalNumber.SetDisplayName(ResourceMng.GetString("PropDigitalNumber"));
                //PropDigitalNumber.SetDescription(ResourceMng.GetString(""));
                //list.Add(PropDigitalNumber);

                //STControlPropertyDescriptor PropMax = new STControlPropertyDescriptor(collection["MaxValue"]);
                //PropMax.SetCategory(ResourceMng.GetString(""));
                //PropMax.SetDisplayName(ResourceMng.GetString("PropMax"));
                //PropMax.SetDescription(ResourceMng.GetString(""));
                //list.Add(PropMax);

                //STControlPropertyDescriptor PropMin = new STControlPropertyDescriptor(collection["MinValue"]);
                //PropMin.SetCategory(ResourceMng.GetString(""));
                //PropMin.SetDisplayName(ResourceMng.GetString("PropMin"));
                //PropMin.SetDescription(ResourceMng.GetString(""));
                //list.Add(PropMin);

                STControlPropertyDescriptor PropMeasurementUnit = new STControlPropertyDescriptor(collection["Unit"]);
                PropMeasurementUnit.SetCategory(ResourceMng.GetString(""));
                PropMeasurementUnit.SetDisplayName(ResourceMng.GetString("PropMeasurementUnit"));
                PropMeasurementUnit.SetDescription(ResourceMng.GetString(""));
                list.Add(PropMeasurementUnit);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }

        public override void DrawAt(Point basePoint, Graphics g)
        {
            base.DrawAt(basePoint, g);

            Rectangle rect = new Rectangle(Point.Empty, this.RectInPage.Size);
            Bitmap bm = new Bitmap(this.RectInPage.Width, this.RectInPage.Height);
            Graphics gp = Graphics.FromImage(bm);

            Color backColor = Color.FromArgb((int)(this.Alpha * 255), this.BackgroundColor);

            /* SliderSwitch的长条形主体 */
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            if ((null == this.BackgroundImage) || (string.Empty == this.BackgroundImage))
            {
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
                    FillRoundRectangle(gp, brush, rect, this.Radius, 1.0f);
                    brush.Dispose();
                }
                else if (EFlatStyle.Flat == this.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    FillRoundRectangle(gp, brush, rect, this.Radius, 1.0f);
                }
            }

            /* 左图标 */
            x = PADDING;  // 偏移为5
            y = PADDING;  // 
            height = rect.Height - 2 * y;   // 计算出高度
            width = rect.Height > SUBVIEW_WIDTH ? rect.Height : SUBVIEW_WIDTH;     // 计算出宽度
            width -= 2 * x;
            Image img = null;
            if (!string.IsNullOrEmpty(this.LeftImage))
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.LeftImage));
            }

            if (null != img)
            {
                gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), rect.X+x, rect.Y+y);
            }

            /* 右图标 */
            x = rect.Width - PADDING - width;
            /*Image*/
            img = null;
            if (!string.IsNullOrEmpty(this.RightImage))
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.RightImage));
            }

            if (null != img)
            {
                gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), rect.X+x, rect.Y+y);
            }

            /* 中间数字 */
            string valueString = null;
            if (KNXDigitalAdjustment.EDigitalNumber.OneDigit == this.DigitalNumber)
            {
                valueString = "8";
            }
            else if (KNXDigitalAdjustment.EDigitalNumber.TwoDigit == this.DigitalNumber)
            {
                valueString = "88";
            }
            else if (KNXDigitalAdjustment.EDigitalNumber.ThreeDigit == this.DigitalNumber)
            {
                valueString = "888";
            }

            if (null != valueString)
            {
                valueString += this.Unit.GetDescription();

                Color fontColor = this.FontColor;
                Font font = new Font("宋体", this.FontSize);
                StringFormat format = new StringFormat();

                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Size size = TextRenderer.MeasureText(valueString, font);
                x = PADDING;
                y = PADDING;
                width = rect.Width - 2 * x;
                height = rect.Height - 2 * y;
                Rectangle rectText = new Rectangle(rect.X+x, rect.Y+y, width, height);
                gp.DrawString(valueString, font, new SolidBrush(fontColor), rectText, format);
            }

            if (EBool.Yes == this.DisplayBorder)
            {
                Color borderColor = this.BorderColor;
                DrawRoundRectangle(gp, new Pen(borderColor, 1), rect, this.Radius, 1.0f);
            }

            g.DrawImage(bm,
                this.VisibleRectInPage,
                new Rectangle(new Point(this.VisibleRectInPage.X - this.RectInPage.X, this.VisibleRectInPage.Y - this.RectInPage.Y), this.VisibleRectInPage.Size),
                GraphicsUnit.Pixel);

            this.FrameIsVisible = false;
            if (ControlState.Move == this.State)
            {
                Pen pen = new Pen(Color.Navy, 2.0f);
                DrawRoundRectangle(g, pen, this.RectInPage, this.Radius, 1.0f);
            }
            else if (this.IsSelected)
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
