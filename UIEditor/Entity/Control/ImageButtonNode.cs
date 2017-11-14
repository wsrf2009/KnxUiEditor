using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIEditor.Component;
using UIEditor.PropertyGridEditor;
using System.Drawing;
using Structure;
using System.IO;
using Structure.Control;
using System.Drawing.Drawing2D;
using Utils;
using System.Windows.Forms;

namespace UIEditor.Entity.Control
{
    [TypeConverter(typeof(ImageButtonNode.PropertyConverter))]
    [Serializable]
    public class ImageButtonNode : ControlBaseNode
    {
        #region 常量
        private const string NAME_IMAGEON = "ImageOn.png";
        private const string NAME_IMAGEOFF = "ImageOff.png";
        #endregion

        #region 变量
        private static int index = 0;
        private Image ImgImageOn
        {
            get
            {
                if (null != this.ImageOn)
                {
                    return ImageHelper.GetDiskImage(Path.Combine(MyCache.ProjImgPath, this.ImageOn));
                }

                return null;
            }
        }
        private Image ImgImageOff
        {
            get
            {
                if (null != this.ImageOff)
                {
                    return ImageHelper.GetDiskImage(Path.Combine(MyCache.ProjImgPath, this.ImageOff));
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
        /// 开启时显示的图片
        /// </summary>
        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string ImageOn { get; set; }

        /// <summary>
        /// 关闭时显示的图片
        /// </summary>
        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string ImageOff { get; set; }
        #endregion

        #region 构造函数
        public ImageButtonNode()
            : base()
        {
            index++;

            this.Name = this.ImageKey = this.SelectedImageKey = MyConst.Controls.KnxImageButtonType;

            this.Text = UIResMang.GetString("TextImageButton");
            this.Title = UIResMang.GetString("TextImageButton") + index;
            SetText(this.Title);

            this.Size = new Size(90, 90);
            this.Padding = new Padding(10, 28, 10, 27);
            this.FlatStyle = EFlatStyle.Flat;
            this.Radius = 45;
            this.Alpha = 0.6f;
            this.BackgroundColor = Color.White;

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();

            this.ImageOn = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResCtrlDir, "ImageButton_On.png"));
            this.ImageOff = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResCtrlDir, "ImageButton_Off.png"));
        }

        /// <summary>
        /// KNXImageButton 转 ImageButtonNode
        /// </summary>
        /// <param name="knx"></param>
        public ImageButtonNode(KNXImageButton knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxImageButtonType;

            SetText(this.Title);

            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();

            if (ImportedHelper.IsLessThan2_5_6())
            {
                this.ImageOn = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_IMAGEON));
                this.ImageOff = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_IMAGEOFF));
            }
            else
            {
                this.ImageOn = knx.ImageOn;
                this.ImageOff = knx.ImageOff;
            }

            if (ImportedHelper.IsLessThan2_7_1())
            {
                this.Alpha = .0f;
            }
        }

        public ImageButtonNode(KNXImageButton knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id

            if (ImportedHelper.IsLessThan2_5_6())
            {
                string knxImage = GetImageName(knx.Id); // KNX图片资源名称
                string knxImagePath = Path.Combine(DirSrcImg, knxImage); // KNX图片资源路径

                this.ImageOn = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_IMAGEON));
                this.ImageOff = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_IMAGEOFF));
            }
            else
            {
                this.ImageOn = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.ImageOn));
                this.ImageOff = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.ImageOff));
            }
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            ImageButtonNode node = base.Clone() as ImageButtonNode;
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
            node.ImageOn = this.ImageOn;
            node.ImageOff = this.ImageOff;

            return node;
        }

        public override object Copy()
        {
            ImageButtonNode node = base.Copy() as ImageButtonNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextImageButton"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextImageButton"));
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

                int pl = (int)Math.Round(this.Padding.Left * ratio, 0);
                int pt = (int)Math.Round(this.Padding.Top * ratio, 0);
                int pr = (int)Math.Round(this.Padding.Right * ratio, 0);
                int pb = (int)Math.Round(this.Padding.Bottom * ratio, 0);

                /* 图标 */
                int x = pl;
                int y = pt;  // 到父视图顶部的距离
                int height = rect.Height - pt - pb;   // 计算出高度
                int width = rect.Width - pl - pr;  // 计算出宽度
                Image img = (null != this.ImgImageOn) ? this.ImgImageOn : this.ImgImageOff;
                if (null != img)
                {
                    gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), x, y);
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
        /// ImageButtonNode 转 KNXImageButton
        /// </summary>
        /// <returns></returns>
        public KNXImageButton ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXImageButton();

            base.ToKnx(knx, worker);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;

            knx.ImageOn = this.ImageOn;
            knx.ImageOff = this.ImageOff;

            MyCache.ValidResImgNames.Add(knx.ImageOn);
            MyCache.ValidResImgNames.Add(knx.ImageOff);

            return knx;
        }

        public KNXImageButton ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXImageButton knx = this.ToKnx(worker);
            knx.Left = this.LocationInPageFact.X - RelPoint.X;
            knx.Top = this.LocationInPageFact.Y - RelPoint.Y;

            knx.ReadAddressId.Clear();
            knx.WriteAddressIds.Clear();

            knx.ImageOn = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.ImageOn), dir);
            knx.ImageOff = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.ImageOff), dir);

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

                STControlPropertyDescriptor PropPadding = new STControlPropertyDescriptor(collection["Padding"]);
                PropPadding.SetCategory(UIResMang.GetString("CategoryLayout"));
                PropPadding.SetDisplayName(UIResMang.GetString("PropPadding"));
                PropPadding.SetDescription(UIResMang.GetString(""));
                list.Add(PropPadding);

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

                STControlPropertyDescriptor PropSwitchImageOn = new STControlPropertyDescriptor(collection["ImageOn"]);
                PropSwitchImageOn.SetCategory(UIResMang.GetString("CategoryDisplay"));
                PropSwitchImageOn.SetDisplayName(UIResMang.GetString("PropSwitchImageOn"));
                PropSwitchImageOn.SetDescription(UIResMang.GetString("DescriptionForPropImageOn"));
                list.Add(PropSwitchImageOn);

                STControlPropertyDescriptor PropSwitchImageOff = new STControlPropertyDescriptor(collection["ImageOff"]);
                PropSwitchImageOff.SetCategory(UIResMang.GetString("CategoryDisplay"));
                PropSwitchImageOff.SetDisplayName(UIResMang.GetString("PropSwitchImageOff"));
                PropSwitchImageOff.SetDescription(UIResMang.GetString("DescriptionForPropImageOff"));
                list.Add(PropSwitchImageOff);


                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion
    }
}
