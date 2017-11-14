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
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.PropertyGridEditor;
using Utils;

namespace UIEditor.Entity.Control
{
    /// <summary>
    ///  网络摄像头
    ///  新增于2.7.4
    /// </summary>
    [TypeConverter(typeof(WebCamerNode.PropertyConverter))]
    [Serializable]
    public class WebCamerNode : ControlBaseNode
    {
        #region 属性
        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string Symbol { get; set; }
        #endregion

        #region 变量
        private static int index = 0;
        public Image ImgBackgroundImage
        {
            get
            {
                if (null != this.BackgroundImage)
                {
                    return ImageHelper.GetDiskImage(Path.Combine(MyCache.ProjImgPath, this.BackgroundImage));
                }

                return null;
            }
        }
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
        #endregion

        #region 构造函数
        public WebCamerNode()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxWebCamViewerType;

            this.Text = UIResMang.GetString("TextWebCamer");
            this.Title = UIResMang.GetString("TextWebCamer") + index;
            SetText(this.Title);

            this.Size = new Size(400, 400);
            this.Padding = new Padding(15);
            this.DisplayBorder = EBool.Yes;
            this.BorderColor = Color.DarkGreen;
            this.Radius = 10;
            this.FlatStyle = EFlatStyle.Flat;
            this.Alpha = 0.3f;
            this.BackgroundColor = Color.DarkGreen;

            this.Symbol = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResCtrlDir, "WebCamer.png"));
        }

        /// <summary>
        /// 从工程文件中导入控件
        /// </summary>
        /// <param name="knx"></param>
        public WebCamerNode(KNXWebCamer knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxWebCamViewerType;
            SetText(this.Title);

            this.Symbol = knx.Symbol;
        }

        /// <summary>
        /// 从模板中导入控件
        /// </summary>
        /// <param name="knx"></param>
        /// <param name="worker"></param>
        /// <param name="DirSrcImg"></param>
        public WebCamerNode(KNXWebCamer knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id

            this.BackgroundImage = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.BackgroundImage));
            this.Symbol = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.Symbol));
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            WebCamerNode node = base.Clone() as WebCamerNode;
            node.Symbol = this.Symbol;

            return node;
        }

        public override object Copy()
        {
            WebCamerNode node = base.Copy() as WebCamerNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextWebCamer"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextWebCamer"));
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

                if (null != this.ImgBackgroundImage)
                {
                    gp.DrawImage(ImageHelper.Resize(this.ImgBackgroundImage, rect.Size, false), 0, 0);
                }
                else
                {
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
                }

                int pl = (int)Math.Round(this.Padding.Left * ratio, 0);
                int pt = (int)Math.Round(this.Padding.Top * ratio, 0);
                int pr = (int)Math.Round(this.Padding.Right * ratio, 0);
                int pb = (int)Math.Round(this.Padding.Bottom * ratio, 0);

                /* 图标 */
                int x = pl;
                int y = pt;  // 到父视图顶部的距离
                int height = rect.Height - pt - pb;   // 计算出高度
                int width = rect.Width - pl - pr;
                Image img = this.ImgSymbol;
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
        public KNXWebCamer ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXWebCamer();

            base.ToKnx(knx, worker);

            knx.Symbol = this.Symbol;

            MyCache.ValidResImgNames.Add(knx.Symbol);


            return knx;
        }

        public KNXWebCamer ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXWebCamer knx = this.ToKnx(worker);
            knx.Left = this.LocationInPageFact.X - RelPoint.X;
            knx.Top = this.LocationInPageFact.Y - RelPoint.Y;

            knx.Symbol = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.Symbol), dir);
            knx.BackgroundImage = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.BackgroundImage), dir);

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

                STControlPropertyDescriptor PropBackgroundImage = new STControlPropertyDescriptor(collection["BackgroundImage"]);
                PropBackgroundImage.SetCategory(UIResMang.GetString("CategoryAppearance"));
                PropBackgroundImage.SetDisplayName(UIResMang.GetString("PropBackgroundImage"));
                PropBackgroundImage.SetDescription(UIResMang.GetString(""));
                list.Add(PropBackgroundImage);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion
    }
}
