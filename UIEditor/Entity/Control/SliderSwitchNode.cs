using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SourceGrid;
using Structure;
using Structure.Control;
using UIEditor.Component;
using UIEditor.Controls;
using System.ComponentModel;
using UIEditor.PropertyGridEditor;
using System.Drawing.Design;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UIEditor.Entity.Control
{
    [TypeConverter(typeof(SliderSwitchNode.PropertyConverter))]
    [Serializable]
    public class SliderSwitchNode : ControlBaseNode
    {
        #region 常量
        private const int PADDING = 2;
        private const int SLIDER_EDGE_WIDTH = 3;
        private const int SLIDER_WIDTH = 50;
        #endregion

        #region 变量
        private static int index = 0;
        #endregion

        #region 属性
        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressSingleReadEditor), typeof(UITypeEditor))]
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressMultiWriteEditor), typeof(UITypeEditor))]
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        /// <summary>
        /// Slider左边背景图片(SliderSymbol与此属性不能共存)
        /// </summary>
        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string LeftImage { get; set; }

        /// <summary>
        /// Slider左边背景图片(SliderSymbol与此属性不能共存)
        /// </summary>
        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string RightImage { get; set; }

        /// <summary>
        /// Slider滑动图片
        /// </summary>
        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string SliderImage { get; set; }

        public EBool IsRelativeControl { get; set; }
        #endregion

        #region 构造函数
        public SliderSwitchNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextSliderSwitch") + "_" + index;
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSliderSwitchType;

            this.Width = 260;
            this.Height = 40;
            //this.Size = new Size(260, 40);
            this.FlatStyle = EFlatStyle.Stereo;

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();

            this.LeftImage = "sl_left.png";
            this.RightImage = "sl_right.png";
            this.SliderImage = "sl.png";
            this.IsRelativeControl = EBool.No;

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

            string FileSliderImage = Path.Combine(MyCache.ProjImagePath, this.SliderImage);
            if (!File.Exists(FileSliderImage))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.SliderImage), FileSliderImage);
            }
        }

        public override object Clone()
        {
            SliderSwitchNode node = base.Clone() as SliderSwitchNode;
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
            node.SliderImage = this.SliderImage;
            node.IsRelativeControl = this.IsRelativeControl;

            return node;
        }

        public SliderSwitchNode(KNXSliderSwitch knx)
            : base(knx)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSliderSwitchType;

            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();

            this.LeftImage = knx.LeftImage;
            this.RightImage = knx.RightImage;
            this.SliderImage = knx.SliderImage;
            this.IsRelativeControl = (EBool)Enum.ToObject(typeof(EBool), knx.IsRelativeControl);
        }

        protected SliderSwitchNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        public KNXSliderSwitch ToKnx()
        {
            var knx = new KNXSliderSwitch();
            base.ToKnx(knx);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;

            knx.LeftImage = this.LeftImage;
            knx.RightImage = this.RightImage;
            knx.SliderImage = this.SliderImage;
            knx.IsRelativeControl = (int)this.IsRelativeControl;

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

                //STControlPropertyDescriptor PropBorderWidth = new STControlPropertyDescriptor(collection["DisplayBorder"]);
                //PropBorderWidth.SetCategory(ResourceMng.GetString("CategoryBorder"));
                //PropBorderWidth.SetDisplayName(ResourceMng.GetString("PropDisplayBorder"));
                //PropBorderWidth.SetDescription(ResourceMng.GetString(""));
                //list.Add(PropBorderWidth);

                //STControlPropertyDescriptor PropBorderColor = new STControlPropertyDescriptor(collection["BorderColor"]);
                //PropBorderColor.SetCategory(ResourceMng.GetString("CategoryBorder"));
                //PropBorderColor.SetDisplayName(ResourceMng.GetString("PropBorderColor"));
                //PropBorderColor.SetDescription(ResourceMng.GetString(""));
                //list.Add(PropBorderColor);

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

                //STControlPropertyDescriptor PropSlideImage = new STControlPropertyDescriptor(collection["SliderImage"]); 
                //PropSlideImage.SetCategory(ResourceMng.GetString(""));
                //PropSlideImage.SetDisplayName(ResourceMng.GetString("PropSlideImage"));
                //PropSlideImage.SetDescription(ResourceMng.GetString(""));
                //list.Add(PropSlideImage);

                STControlPropertyDescriptor PropIsRelative = new STControlPropertyDescriptor(collection["IsRelativeControl"]);
                PropIsRelative.SetCategory(ResourceMng.GetString(""));
                PropIsRelative.SetDisplayName(ResourceMng.GetString("PropIsRelative"));
                PropIsRelative.SetDescription(ResourceMng.GetString("DescriptionForPropIsRelativeControl"));
                list.Add(PropIsRelative);

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
            int y = SLIDER_EDGE_WIDTH;  // 
            int width = rect.Width;
            int height = rect.Height - 2 * y;
            Rectangle rect1 = new Rectangle(rect.X+x, rect.Y+y, width, height);
            if ((null == this.BackgroundImage) || (string.Empty == this.BackgroundImage))
            {
                if (EFlatStyle.Stereo == this.FlatStyle)
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
                    FillRoundRectangle(gp, brush, rect1, this.Radius, 1.0f);
                    brush.Dispose();
                }
                else if (EFlatStyle.Flat == this.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    FillRoundRectangle(gp, brush, rect1, this.Radius, 1.0f);
                }
            }

            /* 左图标 */
            x = PADDING;  // 偏移为5
            y = SLIDER_EDGE_WIDTH + PADDING;  // 
            height = rect.Height - 2 * y;   // 计算出高度
            width = height;     // 计算出宽度
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
            img = null;
            if (!string.IsNullOrEmpty(this.RightImage))
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.RightImage));
            }
            if (null != img)
            {
                gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), rect.X+x, rect.Y+y);
            }

            /* 中间滑块 */
            width = SLIDER_WIDTH;
            x = rect.Width / 2 - width / 2;
            y = 0;
            height = rect.Height;
            Rectangle rect2 = new Rectangle(rect.X+x,rect.Y+y, width, height);
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
            FillRoundRectangle(gp, sliderBrush, rect2, this.Radius, .0f);
            sliderBrush.Dispose();

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
