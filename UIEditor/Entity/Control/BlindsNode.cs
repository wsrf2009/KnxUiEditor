using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using UIEditor.Entity;
using Structure.Control;
using UIEditor;
using UIEditor.Component;
using Structure;
using UIEditor.PropertyGridEditor;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using UIEditor.UserClass;
using Utils;

namespace UIEditor.Entity.Control
{
    /// <summary>
    ///  窗帘开关
    ///  弃用于2.7.1
    ///  由 @ShutterNode 替代
    /// </summary>
    [TypeConverter(typeof(BlindsNode.PropertyConverter))]
    [Serializable]
    public class BlindsNode : ControlBaseNode
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
        [CategoryAttribute("KNX"),
        DisplayName(""),
        EditorAttribute(typeof(PropertyGridKNXSelectedAddressSingleReadEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(MultiSelectedAddressConverter))]
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressMultiWriteEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(MultiSelectedAddressConverter))]
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string LeftImage { get; set; }

        public string LeftText { get; set; }

        [EditorAttribute(typeof(PropertyGridSTFontEditor), typeof(UITypeEditor)), 
        TypeConverterAttribute(typeof(STFontConverter))]
        public STFont LeftTextFont { get; set; }

        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string RightImage { get; set; }

        public string RightText { get; set; }

        [EditorAttribute(typeof(PropertyGridSTFontEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(STFontConverter))]
        public STFont RightTextFont { get; set; }
        #endregion

        #region 构造函数
        public BlindsNode():base()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxBlindsType;

            this.Text = UIResMang.GetString("TextBlinds");
            this.Title = UIResMang.GetString("TextBlinds") + index;
            SetText(this.Title);

            this.Size = new Size(180, 40);
            this.FlatStyle = EFlatStyle.Stereo;

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();

            this.LeftImage = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "arrow_down.png"));
            this.LeftText = "";
            this.LeftTextFont = new STFont(Color.Black, 16);

            this.RightImage = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "arrow_up.png"));
            this.RightText = "";
            this.RightTextFont = new STFont(Color.Black, 16);
        }

        /// <summary>
        /// 从工程文件中导入控件
        /// </summary>
        /// <param name="knx"></param>
        public BlindsNode(KNXBlinds knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxBlindsType;
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
            else
                if (ImportedHelper.IsLessThan2_5_6())
            {
                this.LeftImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_LEFTIMAGE));
                this.RightImage = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_RIGHTIMAGE));
            }
            else
            {
                this.LeftImage = knx.LeftImage;
                this.RightImage = knx.RightImage;
            }

            if (ImportedHelper.IsLessThan2_5_2())
            {
                this.LeftTextFont = new STFont(knx.LeftTextFontColor, knx.LeftTextFontSize);
                this.RightTextFont = new STFont(knx.RightTextFontColor, knx.RightTextFontSize);
            }
            else
            {
                this.LeftTextFont = new STFont(knx.LeftTextFont);
                this.RightTextFont = new STFont(knx.RightTextFont);
            }
            this.LeftText = knx.LeftText;
            this.RightText = knx.RightText;
        }

        /// <summary>
        /// 从模板中导入控件
        /// </summary>
        /// <param name="knx"></param>
        /// <param name="worker"></param>
        /// <param name="DirSrcImg"></param>
        public BlindsNode(KNXBlinds knx, BackgroundWorker worker, string DirSrcImg)
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
            BlindsNode node = base.Clone() as BlindsNode;
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
            node.LeftText = this.LeftText;
            node.LeftTextFont = this.LeftTextFont.Clone();
            node.RightImage = this.RightImage;
            node.RightText = this.RightText;
            node.RightTextFont = this.RightTextFont.Clone();

            return node;
        }

        public override object Copy()
        {
            BlindsNode node = base.Copy() as BlindsNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextBlinds"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextBlinds"));
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
                int y = 0;  // 
                int width = rect.Width;
                int height = rect.Height;

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
                x = p;  // X方向偏移
                y = p;  // Y方向偏移
                height = rect.Height - 2 * y;   // 计算出高度
                width = rect.Height > sv ? rect.Height : sv;     // 计算出宽度
                width -= 2 * x;
                Rectangle rectLeft = new Rectangle(rect.X + x, rect.Y + y, width, height);
                Image img = this.ImgLeftImage;
                if ((null != img) && (width > 0) && (height > 0))
                {
                    gp.DrawImage(ImageHelper.Resize(img, rectLeft.Size, false), rectLeft.X, rectLeft.Y);
                }
                if (null != this.LeftText)
                {
                    Color fontColor = this.LeftTextFont.Color;
                    Font font = this.LeftTextFont.GetFont(ratio);
                    
                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    Size size = TextRenderer.MeasureText(this.LeftText, font);
                    gp.DrawString(this.LeftText, font, new SolidBrush(fontColor), rectLeft, format);
                }

                /* 右图标 */
                x = rect.Width - p - width;
                Rectangle rectRight = new Rectangle(rect.X + x, rect.Y + y, width, height);
                /*Image*/
                img = this.ImgRightImage;
                if ((null != img) && (width > 0) && (height > 0))
                {
                    gp.DrawImage(ImageHelper.Resize(img, rectRight.Size, false), rectRight.X, rectRight.Y);
                }
                if (null != this.RightText)
                {
                    Color fontColor = this.RightTextFont.Color;
                    Font font = this.RightTextFont.GetFont(ratio);
         
                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    Size size = TextRenderer.MeasureText(this.RightText, font);
                    gp.DrawString(this.RightText, font, new SolidBrush(fontColor), rectRight, format);
                }

                x = rectLeft.Right + p;
                y = p;
                width = rect.Width - x - rectRight.Width - 2*p;
                height = rect.Height - 2 * p;
                Rectangle rectCenter = new Rectangle(x, y, width, height);
                /* 中间文本 */
                if (null != this.Title)
                {
                    Color fontColor = this.TitleFont.Color;
                    Font font = this.TitleFont.GetFont(ratio);

                    StringFormat format = new StringFormat();

                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    Size size = TextRenderer.MeasureText(this.Title, font);
                    gp.DrawString(this.Title, font, new SolidBrush(fontColor), rectCenter, format);
                }

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
        /// <summary>
        /// BlindsNode 转 KNXBlinds
        /// </summary>
        /// <returns></returns>
        public KNXBlinds ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXBlinds();

            base.ToKnx(knx, worker);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;

            knx.LeftImage = this.LeftImage;
            knx.LeftText = this.LeftText;
            knx.LeftTextFont = this.LeftTextFont.ToKnx();

            knx.RightImage = this.RightImage;
            knx.RightText = this.RightText;
            knx.RightTextFont = this.RightTextFont.ToKnx();

            MyCache.ValidResImgNames.Add(knx.LeftImage);
            MyCache.ValidResImgNames.Add(knx.RightImage);

            return knx;
        }

        public KNXBlinds ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXBlinds knx = this.ToKnx(worker);
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

                STControlPropertyDescriptor propTitle = new STControlPropertyDescriptor(collection["Title"]);
                propTitle.SetCategory(UIResMang.GetString("CategoryTitle"));
                propTitle.SetDisplayName(UIResMang.GetString("PropTitle"));
                list.Add(propTitle);

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

                STControlPropertyDescriptor PropTitleFont = new STControlPropertyDescriptor(collection["TitleFont"]);
                PropTitleFont.SetCategory(UIResMang.GetString("CategoryTitle"));
                PropTitleFont.SetDisplayName(UIResMang.GetString("PropFont"));
                list.Add(PropTitleFont);

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
                PropLeftImage.SetCategory(UIResMang.GetString("CategoryLeft"));
                PropLeftImage.SetDisplayName(UIResMang.GetString("PropLeftImage"));
                PropLeftImage.SetDescription("");
                list.Add(PropLeftImage);

                STControlPropertyDescriptor PropLeftText = new STControlPropertyDescriptor(collection["LeftText"]);
                PropLeftText.SetCategory(UIResMang.GetString("CategoryLeft"));
                PropLeftText.SetDisplayName(UIResMang.GetString("PropLeftText"));
                PropLeftText.SetDescription("");
                list.Add(PropLeftText);

                STControlPropertyDescriptor PropLeftTextFont = new STControlPropertyDescriptor(collection["LeftTextFont"]);
                PropLeftTextFont.SetCategory(UIResMang.GetString("CategoryLeft"));
                PropLeftTextFont.SetDisplayName(UIResMang.GetString("PropLeftTextFont"));
                PropLeftTextFont.SetDescription("");
                list.Add(PropLeftTextFont);

                STControlPropertyDescriptor PropRightImage = new STControlPropertyDescriptor(collection["RightImage"]);
                PropRightImage.SetCategory(UIResMang.GetString("CategoryRight"));
                PropRightImage.SetDisplayName(UIResMang.GetString("PropRightImage"));
                PropRightImage.SetDescription("");
                list.Add(PropRightImage);

                STControlPropertyDescriptor PropRightText = new STControlPropertyDescriptor(collection["RightText"]);
                PropRightText.SetCategory(UIResMang.GetString("CategoryRight"));
                PropRightText.SetDisplayName(UIResMang.GetString("PropRightText"));
                PropRightText.SetDescription("");
                list.Add(PropRightText);

                STControlPropertyDescriptor PropRightTextFont = new STControlPropertyDescriptor(collection["RightTextFont"]);
                PropRightTextFont.SetCategory(UIResMang.GetString("CategoryRight"));
                PropRightTextFont.SetDisplayName(UIResMang.GetString("PropRightTextFont"));
                PropRightTextFont.SetDescription("");
                list.Add(PropRightTextFont);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion
    }
}
