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
    /// </summary>
    [TypeConverter(typeof(DimmerNode.PropertyConverter))]
    [Serializable]
    public class DimmerNode : ControlBaseNode
    {
        #region 常量
        private const int PADDING = 5;
        #endregion

        #region 变量
        private static int index = 0;
        private Image ImgSymbol
        {
            get
            {
                if (null != this.Symbol)
                {
                    return ImageHelper.GetDiskImage(Path.Combine(MyCache.ProjImgPath, this.Symbol));
                }

                return null;
            }
        }
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
        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string Symbol { get; set; }

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

        public KNXObject Switch { get; set; }

        public KNXObject DimRelatively { get; set; }

        public KNXObject DimAbsolutely { get; set; }

        public KNXObject StateOnOff { get; set; }

        public KNXObject StateDimValue { get; set; }
        #endregion

        #region 构造函数
        public DimmerNode()
            : base()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxDimmerType;

            this.Text = UIResMang.GetString("TextDimmer");
            this.Title = UIResMang.GetString("TextDimmer") + index;
            SetText(this.Title);

            this.Size = new Size(60, 60);
            this.Padding = new Padding(15);
            this.FlatStyle = EFlatStyle.Flat;
            this.Radius = 30;
            this.Alpha = 0.6f;
            this.BackgroundColor = Color.White;

            this.Symbol = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResCtrlDir, "Dimmer_Off.png"));
            this.ImageOn = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResCtrlDir, "Dimmer_On.png"));
            this.ImageOff = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResCtrlDir, "Dimmer_Off.png"));

            this.Switch = new KNXObject();
            this.DimRelatively = new KNXObject();
            this.DimAbsolutely = new KNXObject();
            this.StateOnOff = new KNXObject();
            this.StateDimValue = new KNXObject();
        }

        /// <summary>
        /// 从工程文件中导入控件
        /// </summary>
        /// <param name="knx"></param>
        public DimmerNode(KNXDimmer knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxDimmerType;
            SetText(this.Title);

            this.Symbol = knx.Symbol;
            this.ImageOn = knx.ImageOn;
            this.ImageOff = knx.ImageOff;

            this.Switch = knx.Switch ?? new KNXObject();
            this.DimRelatively = knx.DimRelatively ?? new KNXObject();
            this.DimAbsolutely = knx.DimAbsolutely ?? new KNXObject();
            this.StateOnOff = knx.StateOnOff ?? new KNXObject();
            this.StateDimValue = knx.StateDimValue ?? new KNXObject();
        }

        /// <summary>
        /// 从模板中导入控件
        /// </summary>
        /// <param name="knx"></param>
        /// <param name="worker"></param>
        /// <param name="DirSrcImg"></param>
        public DimmerNode(KNXDimmer knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id

            this.Symbol = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.Symbol));
            this.ImageOn = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.ImageOn));
            this.ImageOff = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.ImageOff));
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            DimmerNode node = base.Clone() as DimmerNode;

            node.Symbol = this.Symbol;
            node.ImageOn = this.ImageOn;
            node.ImageOff = this.ImageOff;

            node.Switch = this.Switch.Copy();
            node.DimRelatively = this.DimRelatively.Copy();
            node.DimAbsolutely = this.DimAbsolutely.Copy();
            node.StateOnOff = this.StateOnOff.Copy();
            node.StateDimValue = this.StateDimValue.Copy();

            return node;
        }

        public override object Copy()
        {
            DimmerNode node = base.Copy() as DimmerNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextDimmer"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextDimmer"));
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

                //int p = (int)Math.Round(PADDING * ratio, 0);
                int pl = (int)Math.Round(this.Padding.Left * ratio, 0);
                int pt = (int)Math.Round(this.Padding.Top * ratio, 0);
                int pr = (int)Math.Round(this.Padding.Right * ratio, 0);
                int pb = (int)Math.Round(this.Padding.Bottom * ratio, 0);

                /* 图标 */
                int x = pl;
                int y = pt;  // 到父视图顶部的距离
                int height = rect.Height - pt - pb; // -2 * y;   // 计算出高度
                //int width = height;     // 计算出宽度
                int width = rect.Width - pl - pr;
                //Image img = this.ImgSymbol;
                Image img = (null != this.ImgImageOn) ? this.ImgImageOn : this.ImgImageOff;
                if (null != img)
                {
                    gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), x, y);
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
        public KNXDimmer ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXDimmer();

            base.ToKnx(knx, worker);

            knx.Symbol = this.Symbol;
            knx.ImageOn = this.ImageOn;
            knx.ImageOff = this.ImageOff;

            knx.Switch = this.Switch;
            knx.DimRelatively = this.DimRelatively;
            knx.DimAbsolutely = this.DimAbsolutely;
            knx.StateOnOff = this.StateOnOff;
            knx.StateDimValue = this.StateDimValue;

            MyCache.ValidResImgNames.Add(knx.Symbol);
            MyCache.ValidResImgNames.Add(knx.ImageOn);
            MyCache.ValidResImgNames.Add(knx.ImageOff);

            return knx;
        }

        public KNXDimmer ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXDimmer knx = this.ToKnx(worker);
            knx.Left = this.LocationInPageFact.X - RelPoint.X;
            knx.Top = this.LocationInPageFact.Y - RelPoint.Y;

            this.Switch.Clean();
            this.DimRelatively.Clean();
            this.DimAbsolutely.Clean();
            this.StateOnOff.Clean();
            this.StateDimValue.Clean();

            knx.Symbol = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.Symbol), dir);
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
