
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
    [TypeConverter(typeof(TimerButtonNode.PropertyConverter))]
    [Serializable]
    public class TimerButtonNode : ControlBaseNode
    {
        #region 常量
        private const int PADDING = 2;
<<<<<<< HEAD
=======
        private const string NAME_SYMBOL = "Symbol.png";
>>>>>>> SationKNXUIEditor-Modify
        #endregion

        #region 变量
        private static int index;
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

        #region 属性
        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressMultiReadEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(MultiSelectedAddressConverter))]
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressMultiWriteActionEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(MultiSelectedAddressConverter))]
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        [EditorAttribute(typeof(PropertyGridStringImageEditor), typeof(UITypeEditor))]
        public string Symbol { get; set; }
        #endregion

        #region 构造函数
        public TimerButtonNode()
            : base()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxTimerButtonType;

            this.Text = UIResMang.GetString("TextTimer");
            this.Title = UIResMang.GetString("TextTimer") + index;
            SetText(this.Title);

            this.Size = new Size(180, 40);
            this.Padding = new Padding(0);
            this.FlatStyle = EFlatStyle.Flat;

            this.Symbol = ProjResManager.CopyImage(Path.Combine(MyCache.ProjectResImgDir, "timer1.png"));

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();
        }

<<<<<<< HEAD
            string FileImageIcon = Path.Combine(MyCache.ProjImagePath, this.Icon);
            if (!File.Exists(FileImageIcon))
            {
                File.Copy(Path.Combine(MyCache.ProjectResCtrlDir, this.Icon), FileImageIcon);
=======
        public TimerButtonNode(KNXTimerButton knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxTimerButtonType;
            SetText(this.Title);

            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();

            if (ImportedHelper.IsLessThan2_0_3())
            {
                if (!string.IsNullOrEmpty(knx.Icon))
                {
                    this.Symbol = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, knx.Icon));
                }
            }
            else if(ImportedHelper.IsLessThan2_5_6())
            {
                this.Symbol = ProjResManager.CopyImageSole(Path.Combine(this.ImagePath, NAME_SYMBOL));
            }
            else
            {
                this.Symbol = knx.Symbol;
            }
        }

        public TimerButtonNode(KNXTimerButton knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id

            if (ImportedHelper.IsLessThan2_5_6())
            {
                string knxImage = GetImageName(knx.Id); // KNX图片资源名称
                string knxImagePath = Path.Combine(DirSrcImg, knxImage); // KNX图片资源路径

                this.Symbol = ProjResManager.CopyImageRename(Path.Combine(knxImagePath, NAME_SYMBOL));
            }
            else
            {
                this.Symbol = ProjResManager.CopyImageRename(Path.Combine(DirSrcImg, knx.Symbol));
>>>>>>> SationKNXUIEditor-Modify
            }
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            TimerButtonNode node = base.Clone() as TimerButtonNode;

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

            node.Symbol = this.Symbol;

            return node;
        }

        public override object Copy()
        {
            TimerButtonNode node = base.Copy() as TimerButtonNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextTimer"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextTimer"));
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

                /* 图标 */
                int x = p; // PADDING;
                int y = p; // PADDING;  // 到父视图顶部的距离
                int height = rect.Height - 2 * y;   // 计算出高度
                int width = height;     // 计算出宽度
                Image img = this.ImgSymbol;
                if (null != img)
                {
                    gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), x, y);
                }

                /* 文本 */
                if (null != this.Title)
                {
                    x += width + p;
                    y = p;
                    width = rect.Width - x - p;
                    height = rect.Height - 2 * y;

                    Rectangle stateRect = new Rectangle(x, y, width, height);
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    Color fontColor = this.TitleFont.Color;
                    Font font = this.TitleFont.GetFont(ratio);
                    gp.DrawString(this.Title, font, new SolidBrush(fontColor), stateRect, sf);
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
        public KNXTimerButton ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXTimerButton();
            base.ToKnx(knx, worker);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;
            knx.Symbol = this.Symbol;


            MyCache.ValidResImgNames.Add(knx.Symbol);

            return knx;
        }

        public KNXTimerButton ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXTimerButton knx = this.ToKnx(worker);
            knx.Left = this.LocationInPageFact.X - RelPoint.X;
            knx.Top = this.LocationInPageFact.Y - RelPoint.Y;

            knx.ReadAddressId.Clear();
            knx.WriteAddressIds.Clear();

            knx.Symbol = FileHelper.CopyFileSole(Path.Combine(MyCache.ProjImgPath, this.Symbol), dir);

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

                //STControlPropertyDescriptor PropPadding = new STControlPropertyDescriptor(collection["Padding"]);
                //PropPadding.SetCategory(UIResMang.GetString("CategoryLayout"));
                //PropPadding.SetDisplayName(UIResMang.GetString("PropPadding"));
                //PropPadding.SetDescription(UIResMang.GetString(""));
                //list.Add(PropPadding);

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
                PropBackColor.SetDescription(UIResMang.GetString("DescriptionForPropBackgroundColor"));
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
                PropEtsReadAddressId.SetDisplayName(UIResMang.GetString("PropEtsReadAddressIds"));
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

                STControlPropertyDescriptor PropSymbol = new STControlPropertyDescriptor(collection["Symbol"]);
                PropSymbol.SetCategory(UIResMang.GetString("CategoryDisplay"));
                PropSymbol.SetDisplayName(UIResMang.GetString("PropSymbol"));
                list.Add(PropSymbol);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion
    }
}
