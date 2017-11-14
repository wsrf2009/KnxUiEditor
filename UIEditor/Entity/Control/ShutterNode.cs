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
    ///  窗帘控制器
    ///  新增于2.7.1
    /// </summary>
    [TypeConverter(typeof(ShutterNode.PropertyConverter))]
    [Serializable]
    public class ShutterNode : ControlBaseNode
    {
        #region 常量
        //private const int PADDING = 10;
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

        public KNXObject ShutterUpDown { get; set; }

        public KNXObject ShutterStop { get; set; }

        public KNXObject AbsolutePositionOfShutter { get; set; }

        public KNXObject AbsolutePositionOfBlinds { get; set; }

        public KNXObject StateUpperPosition { get; set; }

        public KNXObject StateLowerPosition { get; set; }

        public KNXObject StatusActualPositionOfShutter { get; set; }

        public KNXObject StatusActualPositionOfBlinds { get; set; }
        #endregion

        #region 构造函数
        public ShutterNode()
            : base()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxShutterType;

            this.Text = UIResMang.GetString("TextShutter");
            this.Title = UIResMang.GetString("TextShutter") + index;
            SetText(this.Title);

            this.Size = new Size(60, 60);
            this.Padding = new Padding(15);
            this.Radius = 30;
            this.FlatStyle = EFlatStyle.Flat;
            this.Alpha = 0.6f;
            this.BackgroundColor = Color.White;

            this.Symbol = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResCtrlDir, "Shutter_On.png"));
            this.ImageOn = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResCtrlDir, "Shutter_On.png"));
            this.ImageOff = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResCtrlDir, "Shutter_Off.png"));

            this.ShutterUpDown = new KNXObject();
            this.ShutterStop = new KNXObject();
            this.AbsolutePositionOfShutter = new KNXObject();
            this.AbsolutePositionOfBlinds = new KNXObject();
            this.StateUpperPosition = new KNXObject();
            this.StateLowerPosition = new KNXObject();
            this.StatusActualPositionOfShutter = new KNXObject();
            this.StatusActualPositionOfBlinds = new KNXObject();
        }

        /// <summary>
        /// 从工程文件中导入控件
        /// </summary>
        /// <param name="knx"></param>
        public ShutterNode(KNXShutter knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxShutterType;
            SetText(this.Title);

            this.Symbol = knx.Symbol;
            this.ImageOn = knx.ImageOn;
            this.ImageOff = knx.ImageOff;

            this.ShutterUpDown = knx.ShutterUpDown ?? new KNXObject();
            this.ShutterStop = knx.ShutterStop ?? new KNXObject();
            this.AbsolutePositionOfShutter = knx.AbsolutePositionOfShutter ?? new KNXObject();
            this.AbsolutePositionOfBlinds = knx.AbsolutePositionOfBlinds ?? new KNXObject();
            this.StateUpperPosition = knx.StateUpperPosition ?? new KNXObject();
            this.StateLowerPosition = knx.StateLowerPosition ?? new KNXObject();
            this.StatusActualPositionOfShutter = knx.StatusActualPositionOfShutter ?? new KNXObject();
            this.StatusActualPositionOfBlinds = knx.StatusActualPositionOfBlinds ?? new KNXObject();
        }

        /// <summary>
        /// 从模板中导入控件
        /// </summary>
        /// <param name="knx"></param>
        /// <param name="worker"></param>
        /// <param name="DirSrcImg"></param>
        public ShutterNode(KNXShutter knx, BackgroundWorker worker, string DirSrcImg)
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
            ShutterNode node = base.Clone() as ShutterNode;
            node.Symbol = this.Symbol;
            node.ImageOn = this.ImageOn;
            node.ImageOff = this.ImageOff;

            node.ShutterUpDown = this.ShutterUpDown.Copy();
            node.ShutterStop = this.ShutterStop.Copy();
            node.AbsolutePositionOfShutter = this.AbsolutePositionOfShutter.Copy();
            node.AbsolutePositionOfBlinds = this.AbsolutePositionOfBlinds.Copy();
            node.StateUpperPosition = this.StateUpperPosition.Copy();
            node.StateLowerPosition = this.StateLowerPosition.Copy();
            node.StatusActualPositionOfShutter = this.StatusActualPositionOfShutter.Copy();
            node.StatusActualPositionOfBlinds = this.StatusActualPositionOfBlinds.Copy();

            return node;
        }

        public override object Copy()
        {
            ShutterNode node = base.Copy() as ShutterNode;
            node.SetText(node.Title);

            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextShutter"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextShutter"));
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
                int y = pt; // PADDING;  // 到父视图顶部的距离
                int height = rect.Height - pt - pb;   // 计算出高度
                int width = rect.Width - pl - pr;     // 计算出宽度
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
        /// <summary>
        /// ShutterNode 转 KNXShutter
        /// </summary>
        /// <returns></returns>
        public KNXShutter ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXShutter();

            base.ToKnx(knx, worker);

            knx.Symbol = this.Symbol;
            knx.ImageOn = this.ImageOn;
            knx.ImageOff = this.ImageOff;

            knx.ShutterUpDown = this.ShutterUpDown;
            knx.ShutterStop = this.ShutterStop;
            knx.AbsolutePositionOfShutter = this.AbsolutePositionOfShutter;
            knx.AbsolutePositionOfBlinds = this.AbsolutePositionOfBlinds;
            knx.StateUpperPosition = this.StateUpperPosition;
            knx.StateLowerPosition = this.StateLowerPosition;
            knx.StatusActualPositionOfShutter = this.StatusActualPositionOfShutter;
            knx.StatusActualPositionOfBlinds = this.StatusActualPositionOfBlinds;

            MyCache.ValidResImgNames.Add(knx.Symbol);
            MyCache.ValidResImgNames.Add(knx.ImageOn);
            MyCache.ValidResImgNames.Add(knx.ImageOff);

            return knx;
        }

        public KNXShutter ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXShutter knx = this.ToKnx(worker);
            knx.Left = this.LocationInPageFact.X - RelPoint.X;
            knx.Top = this.LocationInPageFact.Y - RelPoint.Y;

            this.ShutterUpDown.Clean();
            this.ShutterStop.Clean();
            this.AbsolutePositionOfShutter.Clean();
            this.AbsolutePositionOfBlinds.Clean();
            this.StateUpperPosition.Clean();
            this.StateLowerPosition.Clean();
            this.StatusActualPositionOfShutter.Clean();
            this.StatusActualPositionOfBlinds.Clean();

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
                //PropBackColor.SetDescription("");
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
