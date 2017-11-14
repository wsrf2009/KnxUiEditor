using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using UIEditor.Entity;
using Structure.Control;
using UIEditor.PropertyGridEditor;
using System.Drawing.Design;
using Structure;
using UIEditor.Component;
using System.Windows.Forms;
using UIEditor;
using SourceGrid;
using System.Drawing.Drawing2D;
using UIEditor.UserClass;
using Utils;

namespace UIEditor.Entity.Control
{
    [TypeConverter(typeof(SliderSwitchNode.PropertyConverter))]
    [Serializable]
    public class SliderSwitchNode : ControlBaseNode
    {
        #region 常量
        private const int PADDING = 2;
        private const int SLIDER_EDGE_WIDTH = 3;
        //private const int SLIDER_WIDTH = 50;
        private const int MIN_SLDER_WIDTH = 5;
        private const string NAME_LEFTIMAGE = "LeftImage.png";
        private const string NAME_RIGHTIMAGE = "RightImage.png";
        private const string NAME_SLIDERIMAGE = "SliderImage.png";
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
        private Image ImgSliderImage
        {
            get
            {
                if (null != this.SliderImage)
                {
                    return ImageHelper.GetDiskImage(Path.Combine(MyCache.ProjImgPath, this.SliderImage));
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

        /// <summary>
        /// Slider左边背景图片(SliderSymbol与此属性不能共存)
        /// </summary>
        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string LeftImage { get; set; }

        /// <summary>
        /// Slider左边背景图片(SliderSymbol与此属性不能共存)
        /// </summary>
        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string RightImage { get; set; }

        /// <summary>
        /// Slider滑动图片
        /// </summary>
        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string SliderImage { get; set; }

        public EBool IsRelativeControl { get; set; }

        public Orientation Orientation { get; set; }

        private int _SliderWidth;
        public int SliderWidth
        {
            get { return _SliderWidth; }
            set
            {
                if (value < MIN_SLDER_WIDTH)
                {
                    _SliderWidth = MIN_SLDER_WIDTH;
                    MessageBox.Show(string.Format(UIResMang.GetString("Message57"), MIN_SLDER_WIDTH), UIResMang.GetString("Message6"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _SliderWidth = value;
                }
            }
        }
        #endregion

        #region 构造函数
        public SliderSwitchNode()
            : base()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSliderSwitchType;

            this.Text = UIResMang.GetString("TextSliderSwitch");
            this.Title = UIResMang.GetString("TextSliderSwitch") + index;
            SetText(this.Title);

            this.Size = new Size(360, 40);
            this.FlatStyle = EFlatStyle.Stereo;

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();

            this.LeftImage = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "reduce.png"));
            this.RightImage = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "increase.png"));
            this.IsRelativeControl = EBool.No;
            this.Orientation = Orientation.Horizontal;
            this.SliderWidth = 50;
        }

        public SliderSwitchNode(KNXSliderSwitch knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSliderSwitchType;
            SetText(this.Title);

            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();

            if (ImportedHelper.IsLessThan2_0_3())
            {
<<<<<<< HEAD
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.LeftImage), FileLeftImage);
=======
                if (!string.IsNullOrEmpty(knx.LeftImage))
                {
                    this.LeftImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.LeftImage));
                }
                if (!string.IsNullOrEmpty(knx.RightImage))
                {
                    this.RightImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.RightImage));
                }
                if (!string.IsNullOrEmpty(knx.SliderImage))
                {
                    this.SliderImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.SliderImage));
                }
>>>>>>> SationKNXUIEditor-Modify
            }
            else if(ImportedHelper.IsLessThan2_5_6())
            {
                this.LeftImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_LEFTIMAGE));
                this.RightImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_RIGHTIMAGE));
                this.SliderImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_SLIDERIMAGE));
            }
            else
            {
<<<<<<< HEAD
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.RightImage), FileRightImage);
=======
                this.LeftImage = knx.LeftImage;
                this.RightImage = knx.RightImage;
                this.SliderImage = knx.SliderImage;
>>>>>>> SationKNXUIEditor-Modify
            }

            this.IsRelativeControl = (EBool)Enum.ToObject(typeof(EBool), knx.IsRelativeControl);
            this.Orientation = (Orientation)Enum.ToObject(typeof(Orientation), knx.Orientation);
            this.SliderWidth = knx.SliderWidth < MIN_SLDER_WIDTH ? MIN_SLDER_WIDTH : knx.SliderWidth;
        }

        public SliderSwitchNode(KNXSliderSwitch knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id

            if (ImportedHelper.IsLessThan2_5_6())
            {
<<<<<<< HEAD
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.SliderImage), FileSliderImage);
=======
                string knxImage = GetImageName(knx.Id); // KNX图片资源名称
                string knxImagePath = Path.Combine(DirSrcImg, knxImage); // KNX图片资源路径

                this.LeftImage = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_LEFTIMAGE));
                this.RightImage = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_RIGHTIMAGE));
                this.SliderImage = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_SLIDERIMAGE));
            }
            else
            {
                this.LeftImage = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.LeftImage));
                this.RightImage = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.RightImage));
                this.SliderImage = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.SliderImage));
>>>>>>> SationKNXUIEditor-Modify
            }
        }
        #endregion

        #region 克隆、复制
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
            node.Orientation = this.Orientation;
            node.SliderWidth = this.SliderWidth;

            return node;
        }

        public override object Copy()
        {
            SliderSwitchNode node = base.Copy() as SliderSwitchNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextSliderSwitch"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextSliderSwitch"));
        }

        public override void DrawAt(Graphics g, float ratio, bool preview)
        {
            base.DrawAt(g, ratio, preview);

            if (Orientation.Horizontal == this.Orientation)
            {
                if (this.Size.Width < this.Size.Height)
                {
                    this.Size = new Size(this.Size.Height, this.Size.Width);
                }
            }
            else
            {
                if (this.Size.Width > this.Size.Height)
                {
                    this.Size = new Size(this.Size.Height, this.Size.Width);
                }
            }

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

                int p = (int)Math.Round(PADDING * ratio, 0);
                int sew = (int)Math.Round(SLIDER_EDGE_WIDTH * ratio, 0);

                /* SliderSwitch的长条形主体 */
                int x;
                int y;  // 
                int width;
                int height;
                if (Orientation.Horizontal == this.Orientation)
                {
                    x = 0;
                    y = sew;
                    width = rect.Width;
                    height = rect.Height - 2 * y;
                }
                else
                {
                    y = 0;
                    x = sew;
                    width = rect.Width - 2 * x;
                    height = rect.Height;
                }
                Rectangle rect1 = new Rectangle(rect.X + x, rect.Y + y, width, height);

                if (EFlatStyle.Stereo == this.FlatStyle)
                {
                    /* 绘制立体效果，三色渐变 */
                    LinearGradientBrush brush;
                    Color[] colors = new Color[3];
                    ColorBlend blend = new ColorBlend();
                    if (Orientation.Horizontal == this.Orientation)
                    {
                        brush = new LinearGradientBrush(rect1, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
                        colors[0] = ColorHelper.changeBrightnessOfColor(backColor, 100);
                        colors[1] = backColor;
                        colors[2] = ColorHelper.changeBrightnessOfColor(backColor, -50);
                        blend.Positions = new float[] { .0f, 0.3f, 1.0f };
                    }
                    else
                    {
                        brush = new LinearGradientBrush(rect1, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal);
                        colors[0] = ColorHelper.changeBrightnessOfColor(backColor, 100);
                        colors[1] = backColor;
                        colors[2] = ColorHelper.changeBrightnessOfColor(backColor, -50);
                        blend.Positions = new float[] { .0f, 0.7f, 1.0f };
                    }
                    blend.Colors = colors;
                    brush.InterpolationColors = blend;
                    FillRoundRectangle(gp, brush, rect1, this.Radius, 1.0f, ratio);
                    brush.Dispose();
                }
                else if (EFlatStyle.Flat == this.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    FillRoundRectangle(gp, brush, rect1, this.Radius, 1.0f, ratio);
                }

                /* 第一个图标 左边/下边 */
                if (Orientation.Horizontal == this.Orientation)
                {
                    x = p;
                    y = p;
                    height = rect.Height - 2 * y;   // 计算出高度
                    width = height;     // 计算出宽度
                }
                else
                {
                    x = p; // PADDING;
                    width = rect.Width - 2 * x;
                    height = width;
                    y = rect.Height - p/*PADDING*/ - height;
                }

                Image img = this.ImgLeftImage;
                if (null != img)
                {
                    gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), rect.X + x, rect.Y + y);
                }

                /* 第二个图标 右边/上边 */
                img = null;
                if (Orientation.Horizontal == this.Orientation)
                {
                    x = rect.Width - p/*PADDING*/ -width;
                }
                else
                {
                    y = p; // PADDING;
                }

                img = this.ImgRightImage;
                if (null != img)
                {
                    gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), rect.X + x, rect.Y + y);
                }

                int sw = (int)Math.Round(this.SliderWidth * ratio, 0);

                /* 中间滑块 */
                if (Orientation.Horizontal == this.Orientation)
                {
                    width = sw; // this.SliderWidth;
                    x = rect.Width / 2 - width / 2;
                    y = 0;
                    height = rect.Height;
                }
                else
                {
                    height = sw; // this.SliderWidth;
                    y = rect.Height / 2 - height / 2;
                    x = 0;
                    width = rect.Width;
                }
                Rectangle rect2 = new Rectangle(rect.X + x, rect.Y + y, width, height);
                Color sliderColor = ColorHelper.changeBrightnessOfColor(backColor, 70);
                LinearGradientBrush sliderBrush;
                Color[] sliderColors = new Color[3];
                ColorBlend sliderBlend = new ColorBlend();
                if (Orientation.Horizontal == this.Orientation)
                {
                    sliderBrush = new LinearGradientBrush(rect2, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
                    sliderColors[0] = ColorHelper.changeBrightnessOfColor(backColor, 100);
                    sliderColors[1] = backColor;
                    sliderColors[2] = ColorHelper.changeBrightnessOfColor(backColor, -50);
                    sliderBlend.Positions = new float[] { .0f, 0.3f, 1.0f };
                }
                else
                {
                    sliderBrush = new LinearGradientBrush(rect2, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal);
                    sliderColors[0] = ColorHelper.changeBrightnessOfColor(backColor, 100);
                    sliderColors[1] = backColor;
                    sliderColors[2] = ColorHelper.changeBrightnessOfColor(backColor, -50);
                    sliderBlend.Positions = new float[] { .0f, 0.7f, 1.0f };
                }
                sliderBlend.Colors = sliderColors;
                sliderBrush.InterpolationColors = sliderBlend;
                FillRoundRectangle(gp, sliderBrush, rect2, this.Radius, .0f, ratio);
                sliderBrush.Dispose();

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
        public KNXSliderSwitch ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXSliderSwitch();
            base.ToKnx(knx, worker);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;

            knx.LeftImage = this.LeftImage;
            knx.RightImage = this.RightImage;
            knx.SliderImage = this.SliderImage;

            knx.IsRelativeControl = (int)this.IsRelativeControl;
            knx.Orientation = (int)this.Orientation;
            knx.SliderWidth = this.SliderWidth;

            MyCache.ValidResImgNames.Add(knx.LeftImage);
            MyCache.ValidResImgNames.Add(knx.RightImage);
            MyCache.ValidResImgNames.Add(knx.SliderImage);

            return knx;
        }

        public KNXSliderSwitch ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXSliderSwitch knx = this.ToKnx(worker);
            knx.Left = this.LocationInPageFact.X - RelPoint.X;
            knx.Top = this.LocationInPageFact.Y - RelPoint.Y;

            knx.ReadAddressId.Clear();
            knx.WriteAddressIds.Clear();

            knx.LeftImage = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.LeftImage), dir);
            knx.RightImage = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.RightImage), dir);
            knx.SliderImage = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.SliderImage), dir);

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

                STControlPropertyDescriptor PropOrientation = new STControlPropertyDescriptor(collection["Orientation"]);
                PropOrientation.SetDisplayName(UIResMang.GetString("PropOrientation"));
                list.Add(PropOrientation);

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
                list.Add(PropEtsWriteAddressIds);

                STControlPropertyDescriptor PropEtsReadAddressId = new STControlPropertyDescriptor(collection["ReadAddressId"]);
                PropEtsReadAddressId.SetCategory(UIResMang.GetString("CategoryGroupAddress"));
                PropEtsReadAddressId.SetDisplayName(UIResMang.GetString("PropEtsReadAddressId"));
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
                PropLeftImage.SetCategory(UIResMang.GetString("CategoryDisplay"));
                PropLeftImage.SetDisplayName(UIResMang.GetString("PropLeftImage"));
                PropLeftImage.SetDescription(UIResMang.GetString("DescriptionForPropLeftImage"));
                list.Add(PropLeftImage);

                STControlPropertyDescriptor PropRightImage = new STControlPropertyDescriptor(collection["RightImage"]);
                PropRightImage.SetCategory(UIResMang.GetString("CategoryDisplay"));
                PropRightImage.SetDisplayName(UIResMang.GetString("PropRightImage"));
                PropRightImage.SetDescription(UIResMang.GetString("DescriptionForPropRightImage"));
                list.Add(PropRightImage);

                STControlPropertyDescriptor PropIsRelative = new STControlPropertyDescriptor(collection["IsRelativeControl"]);
                PropIsRelative.SetDisplayName(UIResMang.GetString("PropIsRelative"));
                PropIsRelative.SetDescription(UIResMang.GetString("DescriptionForPropIsRelativeControl"));
                list.Add(PropIsRelative);

                STControlPropertyDescriptor PropSliderWidth = new STControlPropertyDescriptor(collection["SliderWidth"]);
                PropSliderWidth.SetCategory(UIResMang.GetString("CategorySlider"));
                PropSliderWidth.SetDisplayName(UIResMang.GetString("PropSliderWidth"));
                list.Add(PropSliderWidth);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion
    }
}
