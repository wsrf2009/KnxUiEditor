using Structure;
using Structure.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor.Entity.Control
{
    /// <summary>
    ///  HVAC控制
    ///  新增于2.7.4
    /// </summary>
    [TypeConverter(typeof(HVACNode.PropertyConverter))]
    [Serializable]
    public class HVACNode : ControlBaseNode
    {
        #region 属性

        #endregion

        #region 变量
        private static int index = 0;
        #endregion

        #region 构造函数
        public HVACNode()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxHVACType;

            this.Text = "HVAC";
            this.Title = "HVAC" + index;
            SetText(this.Title);

            this.Size = new Size(60, 60);
            this.Padding = new Padding(15);
            this.Radius = 30;
            this.FlatStyle = EFlatStyle.Flat;
            this.Alpha = 0.6f;
            this.BackgroundColor = Color.White;
        }
        
        /// <summary>
        /// 从工程文件中导入控件
        /// </summary>
        /// <param name="knx"></param>
        public HVACNode(KNXHVAC knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxHVACType;
            SetText(this.Title);
        }

        /// <summary>
        /// 从模板中导入控件
        /// </summary>
        /// <param name="knx"></param>
        /// <param name="worker"></param>
        /// <param name="DirSrcImg"></param>
        public HVACNode(KNXHVAC knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            HVACNode node = base.Clone() as HVACNode;

            return node;
        }

        public override object Copy()
        {
            HVACNode node = base.Copy() as HVACNode;
            node.SetText(node.Title);

            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText("HVAC");
        }

        public override string GetText(string text)
        {
            return base.GetText("HVAC");
        }

        public override void DrawAt(Graphics g, float ratio, bool preview)
        {
            base.DrawAt(g, ratio, preview);

            //if (ControlState.Move == this.State)
            //{
            //    Pen pen = new Pen(Color.Navy, 2.0f);
            //    DrawRoundRectangle(g, pen, this.RectInPage, this.Radius, 1.0f, ratio);
            //}
            //else
            //{
            //    Rectangle rect = new Rectangle(Point.Empty, this.RectInPage.Size);
            //    Bitmap bm = new Bitmap(this.RectInPage.Width, this.RectInPage.Height);
            //    Graphics gp = Graphics.FromImage(bm);

            //    if (null != this.ImgBackgroundImage)
            //    {
            //        g.DrawImage(ImageHelper.Resize(this.ImgBackgroundImage, this.RectInPage.Size, false), 0, 0);
            //    }
            //    else
            //    {
            //        Color backColor = Color.FromArgb((int)(this.Alpha * 255), this.BackgroundColor);
            //        if (EFlatStyle.Stereo == this.FlatStyle)
            //        {
            //            /* 绘制立体效果，三色渐变 */
            //            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
            //            Color[] colors = new Color[3];
            //            colors[0] = ColorHelper.changeBrightnessOfColor(backColor, 100);
            //            colors[1] = backColor;
            //            colors[2] = ColorHelper.changeBrightnessOfColor(backColor, -50);
            //            ColorBlend blend = new ColorBlend();
            //            blend.Positions = new float[] { 0.0f, 0.3f, 1.0f };
            //            blend.Colors = colors;
            //            brush.InterpolationColors = blend;
            //            FillRoundRectangle(gp, brush, rect, this.Radius, 1.0f, ratio);
            //            brush.Dispose();
            //        }
            //        else if (EFlatStyle.Flat == this.FlatStyle)
            //        {
            //            SolidBrush brush = new SolidBrush(backColor);
            //            FillRoundRectangle(gp, brush, rect, this.Radius, 1.0f, ratio);
            //            brush.Dispose();
            //        }

            //        int pl = (int)Math.Round(this.Padding.Left * ratio, 0);
            //        int pt = (int)Math.Round(this.Padding.Top * ratio, 0);
            //        int pr = (int)Math.Round(this.Padding.Right * ratio, 0);
            //        int pb = (int)Math.Round(this.Padding.Bottom * ratio, 0);

            //        /* 图标 */
            //        int x = pl;
            //        int y = pt;  // 到父视图顶部的距离
            //        int height = rect.Height - pt - pb;   // 计算出高度
            //        int width = rect.Width - pl - pr;
            //        Image img = this.ImgSymbol;
            //        if (null != img)
            //        {
            //            gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), x, y);
            //        }

            //        if (EBool.Yes == this.DisplayBorder)
            //        {
            //            Color borderColor = this.BorderColor;
            //            DrawRoundRectangle(gp, new Pen(borderColor, 1), rect, this.Radius, 1.0f, ratio);
            //        }

            //        g.DrawImage(bm,
            //            this.VisibleRectInPage,
            //            new Rectangle(new Point(this.VisibleRectInPage.X - this.RectInPage.X, this.VisibleRectInPage.Y - this.RectInPage.Y), this.VisibleRectInPage.Size),
            //            GraphicsUnit.Pixel);

            //        if (!preview)
            //        {
            //            this.FrameIsVisible = false;

            //            if (this.IsThisSelected)
            //            {
            //                this.SetFrame();
            //                Pen pen = new Pen(Color.LightGray, 1.0f);
            //                pen.DashStyle = DashStyle.Dot;//设置为虚线,用虚线画四边，模拟微软效果
            //                g.DrawLine(pen, this.LinePoints[0], this.LinePoints[1]);
            //                g.DrawLine(pen, this.LinePoints[2], this.LinePoints[3]);
            //                g.DrawLine(pen, this.LinePoints[4], this.LinePoints[5]);
            //                g.DrawLine(pen, this.LinePoints[6], this.LinePoints[7]);
            //                g.DrawLine(pen, this.LinePoints[8], this.LinePoints[9]);
            //                g.DrawLine(pen, this.LinePoints[10], this.LinePoints[11]);
            //                g.DrawLine(pen, this.LinePoints[12], this.LinePoints[13]);
            //                g.DrawLine(pen, this.LinePoints[14], this.LinePoints[15]);

            //                g.FillRectangles(Brushes.White, this.SmallRects); //填充8个小矩形的内部
            //                g.DrawRectangles(Pens.Black, this.SmallRects);  //绘制8个小矩形的黑色边线

            //                this.FrameIsVisible = true;
            //            }
            //        }
            //    }
            //}
        }
        #endregion

        #region 转为KNX
        public KNXHVAC ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXHVAC();

            base.ToKnx(knx, worker);

            return knx;
        }

        public KNXHVAC ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXHVAC knx = this.ToKnx(worker);
            knx.Left = this.LocationInPageFact.X - RelPoint.X;
            knx.Top = this.LocationInPageFact.Y - RelPoint.Y;

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

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion
    }
}
