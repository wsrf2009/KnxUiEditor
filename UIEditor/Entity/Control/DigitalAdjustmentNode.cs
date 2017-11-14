
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
using UIEditor;
using UIEditor.Component;
using UIEditor.Entity;
using UIEditor.PropertyGridEditor;
using UIEditor.UserClass;
using Utils;


namespace UIEditor.Entity.Control
{
    [TypeConverter(typeof(DigitalAdjustmentNode.PropertyConverter))]
    [Serializable]
    class DigitalAdjustmentNode : ControlBaseNode
    {
        #region 常量
        private const int PADDING = 2;
        private const int SUBVIEW_WIDTH = 40;
        private const string NAME_LEFTIMAGE = "LeftImage.png";
        private const string NAME_RIGHTIMAGE = "RightImage.png";
        #endregion

        #region 变量
        private static int index = 0;
        private Image ImgLeftImage
        {
            get
            {
                if (null != this.LeftImage)
                {
                    return ImageHelper.GetDiskImage(Path.Combine(MyCache.ProjImgPath, this.LeftImage));
                }

                return null;
            }
        }
        private Image ImgRightImage
        {
            get
            {
                if (null != this.RightImage)
                {
                    return ImageHelper.GetDiskImage(Path.Combine(MyCache.ProjImgPath, this.RightImage));
                }

                return null;
            }
        }
        #endregion

        #region 属性
        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressSingleReadEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(MultiSelectedAddressConverter))]
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressMultiWriteEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(MultiSelectedAddressConverter))]
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string LeftImage { get; set; }

        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string RightImage { get; set; }

        public EDecimalDigit DecimalDigit { get; set; }

        public float MaxValue { get; set; }

        public float MinValue { get; set; }

        [TypeConverter(typeof(UIEditor.PropertyGridTypeConverter.EnumConverter))]
        public ERegulationStep RegulationStep { get; set; }

        [TypeConverter(typeof(UIEditor.PropertyGridTypeConverter.EnumConverter))]
        public EMeasurementUnit Unit { get; set; }

        [EditorAttribute(typeof(PropertyGridSTFontEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(STFontConverter))]
        public STFont ValueFont { get; set; }
        #endregion

        #region 构造函数
        public DigitalAdjustmentNode()
            : base()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxDigitalAdjustmentType;

            this.Text = UIResMang.GetString("TextDigitalAdjustment");
            this.Title = UIResMang.GetString("TextDigitalAdjustment") + index;
            SetText(this.Title);

            this.Size = new Size(180, 40);
            this.FlatStyle = EFlatStyle.Stereo;
            this.TitleFont = new STFont(Color.Yellow, 28);

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();

            this.LeftImage = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "reduce.png"));
            this.RightImage = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "increase.png"));
            this.DecimalDigit = EDecimalDigit.Zero;
            this.MaxValue = 30;
            this.MinValue = 16;
            this.RegulationStep = ERegulationStep.One;
            this.Unit = EMeasurementUnit.None;
            this.ValueFont = new STFont(Color.Yellow, 28);
        }

        public DigitalAdjustmentNode(KNXDigitalAdjustment knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxDigitalAdjustmentType;

            SetText(this.Title);

            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();

            if (ImportedHelper.IsLessThan2_0_3())
            {
                if (!string.IsNullOrEmpty(knx.LeftImage))
                {
                    this.LeftImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.LeftImage));
                }
                if (!string.IsNullOrEmpty(knx.RightImage))
                {
                    this.RightImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.RightImage));
                }
            }
            else if(ImportedHelper.IsLessThan2_5_6())
            {
                this.LeftImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_LEFTIMAGE));
                this.RightImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_RIGHTIMAGE));
            }
            else
            {
                this.LeftImage = knx.LeftImage;
                this.RightImage = knx.RightImage;
            }
            this.DecimalDigit = (EDecimalDigit)Enum.ToObject(typeof(EDecimalDigit), knx.DecimalDigit);
            this.MaxValue = knx.MaxValue;
            this.MinValue = knx.MinValue;

            if (ImportedHelper.IsLessThan2_1_8() && knx.Unit > (int)EMeasurementUnit.Centigrade)
            {
                this.Unit = (EMeasurementUnit)Enum.ToObject(typeof(EMeasurementUnit), knx.Unit - 1);
            }
            else
            {
                this.Unit = (EMeasurementUnit)Enum.ToObject(typeof(EMeasurementUnit), knx.Unit);
            }

            if (ImportedHelper.IsLessThan2_5_3())
            {
                this.RegulationStep = ERegulationStep.One;
            }
            else
            {
                if (ImportedHelper.IsLessThan2_5_4())
                {
                    knx.RegStep += 2;
                    this.RegulationStep = (ERegulationStep)Enum.ToObject(typeof(ERegulationStep), knx.RegStep);
                }
                else
                {
                    this.RegulationStep = (ERegulationStep)EnumExtension.ConvertFrom(typeof(ERegulationStep), knx.RegulationStep);
                }
            }

            if (ImportedHelper.IsLessThan2_5_4())
            {
                this.ValueFont = this.TitleFont.Clone();
            }
            else
            {
                this.ValueFont = new STFont(knx.ValueFont);
            }
        }

        public DigitalAdjustmentNode(KNXDigitalAdjustment knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id

            if (ImportedHelper.IsLessThan2_5_6())
            {
                string knxImage = GetImageName(knx.Id); // KNX图片资源名称
                string knxImagePath = Path.Combine(DirSrcImg, knxImage); // KNX图片资源路径

                this.LeftImage = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_LEFTIMAGE));
                this.RightImage = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_RIGHTIMAGE));
            }
            else
            {
                this.LeftImage = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.LeftImage));
                this.RightImage = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.RightImage));
            }
        }
        #endregion

        #region 克隆、复制
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
            node.DecimalDigit = this.DecimalDigit;
            node.MaxValue = this.MaxValue;
            node.MinValue = this.MinValue;
            node.RegulationStep = this.RegulationStep;
            node.Unit = this.Unit;
            node.ValueFont = this.ValueFont.Clone();

            return node;
        }

        public override object Copy()
        {
            DigitalAdjustmentNode node = base.Copy() as DigitalAdjustmentNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextDigitalAdjustment"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextDigitalAdjustment"));
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

                /* SliderSwitch的长条形主体 */
                int x = 0;
                int y = 0;
                int width = 0;
                int height = 0;

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
                }

                int p = (int)Math.Round(PADDING * ratio, 0);
                int sv = (int)Math.Round(SUBVIEW_WIDTH * ratio, 0);

                /* 左图标 */
                x = p;
                y = p;
                height = rect.Height - 2 * y;   // 计算出高度
                width = rect.Height > sv ? rect.Height : sv;     // 计算出宽度
                width -= 2 * x;
                Image img = this.ImgLeftImage;
                if (null != img)
                {
                    gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), rect.X + x, rect.Y + y);
                }

                /* 右图标 */
                x = rect.Width - p - width;
                /*Image*/
                img = this.ImgRightImage;
                if (null != img)
                {
                    gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), rect.X + x, rect.Y + y);
                }

                /* 中间数字 */
                string valueString = "88";
                if (EDecimalDigit.One == this.DecimalDigit)
                {
                    valueString = "88.8";
                }
                else if (EDecimalDigit.Two == this.DecimalDigit)
                {
                    valueString = "88.88";
                }

                valueString += this.Unit.GetDescription();

                Color fontColor = this.ValueFont.Color;
                Font font = this.ValueFont.GetFont(ratio);
                StringFormat format = new StringFormat();

                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Size size = TextRenderer.MeasureText(valueString, font);
                x = p;
                y = p;
                width = rect.Width - 2 * x;
                height = rect.Height - 2 * y;
                Rectangle rectText = new Rectangle(rect.X + x, rect.Y + y, width, height);
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
        public KNXDigitalAdjustment ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXDigitalAdjustment();
            base.ToKnx(knx, worker);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;

            knx.LeftImage = this.LeftImage;
            knx.RightImage = this.RightImage;

            knx.DecimalDigit = (int)this.DecimalDigit;
            knx.MaxValue = this.MaxValue;
            knx.MinValue = this.MinValue;
            knx.RegulationStep = this.RegulationStep.GetDescription();
            knx.Unit = (int)this.Unit;
            knx.ValueFont = this.ValueFont.ToKnx();

            MyCache.ValidResImgNames.Add(knx.LeftImage);
            MyCache.ValidResImgNames.Add(knx.RightImage);

            return knx;
        }

        public KNXDigitalAdjustment ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXDigitalAdjustment knx = this.ToKnx(worker);
            knx.Left = this.LocationInPageFact.X - RelPoint.X;
            knx.Top = this.LocationInPageFact.Y - RelPoint.Y;

            knx.ReadAddressId.Clear();
            knx.WriteAddressIds.Clear();

            knx.LeftImage = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.LeftImage), dir);
            knx.RightImage = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.RightImage), dir);

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
                list.Add(PropBackColor);

                STControlPropertyDescriptor PropFlatStyle = new STControlPropertyDescriptor(collection["FlatStyle"]);
                PropFlatStyle.SetCategory(UIResMang.GetString("CategoryAppearance"));
                PropFlatStyle.SetDisplayName(UIResMang.GetString("PropFlatStyle"));
                PropFlatStyle.SetDescription(UIResMang.GetString("DescriptionForPropFlatStyle"));
                list.Add(PropFlatStyle);

                STControlPropertyDescriptor PropEtsWriteAddressIds = new STControlPropertyDescriptor(collection["WriteAddressIds"]);
                PropEtsWriteAddressIds.SetCategory(UIResMang.GetString("CategoryGroupAddress"));
                PropEtsWriteAddressIds.SetDisplayName(UIResMang.GetString("PropEtsWriteAddressIds"));
                PropEtsWriteAddressIds.SetDescription(UIResMang.GetString(""));
                list.Add(PropEtsWriteAddressIds);

                STControlPropertyDescriptor PropEtsReadAddressId = new STControlPropertyDescriptor(collection["ReadAddressId"]);
                PropEtsReadAddressId.SetCategory(UIResMang.GetString("CategoryGroupAddress"));
                PropEtsReadAddressId.SetDisplayName(UIResMang.GetString("PropEtsReadAddressId"));
                PropEtsReadAddressId.SetDescription(UIResMang.GetString(""));
                list.Add(PropEtsReadAddressId);

                STControlPropertyDescriptor PropHasTip = new STControlPropertyDescriptor(collection["HasTip"]);
                PropHasTip.SetCategory(UIResMang.GetString("CategoryOperation"));
                PropHasTip.SetDisplayName(UIResMang.GetString("PropHasTip"));
                PropHasTip.SetDescription(UIResMang.GetString("DescriptionForPropHasTip"));
                list.Add(PropHasTip);

                STControlPropertyDescriptor PropTip = new STControlPropertyDescriptor(collection["Tip"]);
                PropTip.SetCategory(UIResMang.GetString("CategoryOperation"));
                PropTip.SetDisplayName(UIResMang.GetString("PropTip"));
                PropTip.SetDescription(UIResMang.GetString("DescriptionForPropTip"));
                list.Add(PropTip);

                STControlPropertyDescriptor PropClickable = new STControlPropertyDescriptor(collection["Clickable"]);
                PropClickable.SetCategory(UIResMang.GetString("CategoryOperation"));
                PropClickable.SetDisplayName(UIResMang.GetString("PropClickable"));
                PropClickable.SetDescription(UIResMang.GetString("DescriptionForPropClickable"));
                list.Add(PropClickable);

                STControlPropertyDescriptor PropLeftImage = new STControlPropertyDescriptor(collection["LeftImage"]);
                PropLeftImage.SetCategory(UIResMang.GetString("CategoryLeft"));
                PropLeftImage.SetDisplayName(UIResMang.GetString("PropLeftImage"));
                list.Add(PropLeftImage);

                STControlPropertyDescriptor PropRightImage = new STControlPropertyDescriptor(collection["RightImage"]);
                PropRightImage.SetCategory(UIResMang.GetString("CategoryRight"));
                PropRightImage.SetDisplayName(UIResMang.GetString("PropRightImage"));
                list.Add(PropRightImage);

                STControlPropertyDescriptor PropMaxValue = new STControlPropertyDescriptor(collection["MaxValue"]);
                PropMaxValue.SetCategory(UIResMang.GetString("CategoryValue"));
                PropMaxValue.SetDisplayName(UIResMang.GetString("PropMaxValue"));
                list.Add(PropMaxValue);

                STControlPropertyDescriptor PropMinValue = new STControlPropertyDescriptor(collection["MinValue"]);
                PropMinValue.SetCategory(UIResMang.GetString("CategoryValue"));
                PropMinValue.SetDisplayName(UIResMang.GetString("PropMinValue"));
                list.Add(PropMinValue);

                STControlPropertyDescriptor PropDecimalDigit = new STControlPropertyDescriptor(collection["DecimalDigit"]);
                PropDecimalDigit.SetCategory(UIResMang.GetString("CategoryValue"));
                PropDecimalDigit.SetDisplayName(UIResMang.GetString("PropDecimalDigit"));
                list.Add(PropDecimalDigit);

                STControlPropertyDescriptor PropRegStep = new STControlPropertyDescriptor(collection["RegulationStep"]);
                PropRegStep.SetCategory(UIResMang.GetString("CategoryValue"));
                PropRegStep.SetDisplayName(UIResMang.GetString("PropRegStep"));
                PropRegStep.SetDescription(UIResMang.GetString(""));
                list.Add(PropRegStep);

                STControlPropertyDescriptor PropMeasurementUnit = new STControlPropertyDescriptor(collection["Unit"]);
                PropMeasurementUnit.SetCategory(UIResMang.GetString("CategoryValue"));
                PropMeasurementUnit.SetDisplayName(UIResMang.GetString("PropMeasurementUnit"));
                list.Add(PropMeasurementUnit);

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
