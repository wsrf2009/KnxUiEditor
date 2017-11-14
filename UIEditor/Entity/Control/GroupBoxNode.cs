using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.ComponentModel;
using UIEditor.Entity;
using UIEditor.PropertyGridEditor;
using System.Drawing.Design;
using Structure;
using Structure.Control;
using UIEditor.Component;
using UIEditor;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UIEditor.UserClass;

namespace UIEditor.Entity.Control
{
    [TypeConverter(typeof(GroupBoxNode.PropertyConverter))]
    [Serializable]
    public class GroupBoxNode : ContainerNode
    {
        #region 常量
        private const int SIDEMOBILE = 12;
        #endregion

        #region 变量
        private static int index = 0;
        #endregion

        #region 属性
        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressSingleReadEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(MultiSelectedAddressConverter))]
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressMultiWriteEditor), typeof(UITypeEditor)),
        TypeConverterAttribute(typeof(MultiSelectedAddressConverter))]
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }
        #endregion

        #region 构造函数
        public GroupBoxNode()
            : base()
        {
            index++;

            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxGroupBoxType;

            this.Text = UIResMang.GetString("TextGroupBox");
            this.Title = UIResMang.GetString("TextGroupBox") + index;
            SetText(this.Title);

            this.Size = new Size(300, 400);
            this.Alpha = .0f;
            this.DisplayBorder = EBool.Yes;
            this.BorderColor = Color.BlanchedAlmond;

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();
        }

        /// <summary>
        /// KNXGrid 转 GridNode
        /// </summary>
        /// <param name="knx"></param>
        public GroupBoxNode(KNXGroupBox knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxGroupBoxType;

            SetText(this.Title);

            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();
        }

        public GroupBoxNode(KNXGroupBox knx, BackgroundWorker worker, string DirSrcImg)
            : this(knx, worker)
        {
            this.Id = GenId(); // 创建新的Id
        }
        #endregion

        #region 克隆、复制
        public override object Clone()
        {
            GroupBoxNode node = base.Clone() as GroupBoxNode;
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

            return node;
        }

        public override object Copy()
        {
            GroupBoxNode node = base.Copy() as GroupBoxNode;
            node.SetText(node.Title);
            return node;
        }
        #endregion

        #region 覆写方法
        public override void SetText(string title)
        {
            base.SetText(UIResMang.GetString("TextGroupBox"));
        }

        public override string GetText(string text)
        {
            return base.GetText(UIResMang.GetString("TextGroupBox"));
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

                if (EFlatStyle.Flat == this.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    FillRoundRectangle(gp, brush, rect, this.Radius, 1.0f, ratio);
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

                foreach (ViewNode node in this.Nodes)
                {
                    if (MyConst.Controls.KnxGroupBoxType != node.Name)
                    {
                        node.DrawAt(g, ratio, preview);
                    }
                }

                foreach (ViewNode node in this.Nodes)
                {
                    if (MyConst.Controls.KnxGroupBoxType == node.Name)
                    {
                        node.DrawAt(g, ratio, preview);
                    }
                }

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

                        int sm = (int)Math.Round(SIDEMOBILE * ratio, 0);

                        int centerX;
                        int centerY;
                        int SideMobile = sm;
                        if ((this.LinePoints[1].X - this.LinePoints[0].X) > SideMobile)
                        {
                            centerX = this.LinePoints[0].X + (this.LinePoints[1].X - this.LinePoints[0].X) / 2;
                            centerY = this.LinePoints[0].Y;
                        }
                        else
                        {
                            centerX = this.SmallRects[0].X + this.SmallRects[0].Width / 2;
                            centerY = this.SmallRects[0].Y + this.SmallRects[0].Height / 2;
                        }

                        int lX = centerX - SideMobile / 2;
                        int lY = centerY - SideMobile / 2 + 2;

                        this.MobileRect = new Rectangle(new Point(lX, lY), new Size(SideMobile, SideMobile));

                        g.FillRectangle(Brushes.White, this.MobileRect);
                        GraphicsPath path = new GraphicsPath();

                        float x1 = this.MobileRect.X + this.MobileRect.Width / 2;
                        float y1 = this.MobileRect.Y;
                        float xr1 = x1 + 2;
                        float yr1 = y1 + 2;
                        float xl1 = x1 - 2;
                        float yl1 = y1 + 2;
                        path.AddLines(new PointF[] { new PointF(x1, y1), new PointF(xr1, yr1), new PointF(xl1, yl1) });
                        g.DrawPath(Pens.Black, path);
                        g.FillPath(Brushes.Black, path);

                        float x4 = this.MobileRect.Left;
                        float y4 = this.MobileRect.Y + this.MobileRect.Height / 2;
                        float xt4 = x4 + 2;
                        float yt4 = y4 - 2;
                        float xb4 = x4 + 2;
                        float yb4 = y4 + 2;
                        path.Reset();
                        path.AddLines(new PointF[] { new PointF(x4, y4), new PointF(xt4, yt4), new PointF(xb4, yb4) });
                        g.DrawPath(Pens.Black, path);
                        g.FillPath(Brushes.Black, path);

                        float x2 = this.MobileRect.Right;
                        float y2 = y4;
                        float xb2 = x2 - 2.5f;
                        float yb2 = yb4;
                        float xt2 = x2 - 2.5f;
                        float yt2 = yt4;
                        path.Reset();
                        path.AddLines(new PointF[] { new PointF(x2, y2), new PointF(xb2, yb2), new PointF(xt2, yt2) });
                        g.DrawPath(Pens.Black, path);
                        g.FillPath(Brushes.Black, path);

                        float x3 = x1;
                        float y3 = this.MobileRect.Bottom;
                        float xl3 = xl1;
                        float yl3 = y3 - 2.5f;
                        float xr3 = xr1;
                        float yr3 = y3 - 2.5f;
                        path.Reset();
                        path.AddLines(new PointF[] { new PointF(x3, y3), new PointF(xl3, yl3), new PointF(xr3, yr3) });
                        g.DrawPath(Pens.Black, path);
                        g.FillPath(Brushes.Black, path);

                        g.DrawLine(Pens.Black, x1, y1, x1, y3);
                        g.DrawLine(Pens.Black, x4, y4, x2, y4);
                    }
                }
            }
        }
        #endregion

        #region 转为KNX
        /// <summary>
        /// GridNode转KNXGrid
        /// </summary>
        /// <returns></returns>
        public KNXGroupBox ToKnx(BackgroundWorker worker)
        {
            var knx = new KNXGroupBox();

            base.ToKnx(knx, worker);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;

            return knx;
        }

        public KNXGroupBox ExportTo(BackgroundWorker worker, string dir, Point RelPoint)
        {
            KNXGroupBox knx = this.ToKnx(worker);
            knx.Left = this.LocationInPageFact.X - RelPoint.X;
            knx.Top = this.LocationInPageFact.Y - RelPoint.Y;

            knx.ReadAddressId.Clear();
            knx.WriteAddressIds.Clear();

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
                list.Add(PropBorderWidth);

                STControlPropertyDescriptor PropBorderColor = new STControlPropertyDescriptor(collection["BorderColor"]);
                PropBorderColor.SetCategory(UIResMang.GetString("CategoryBorder"));
                PropBorderColor.SetDisplayName(UIResMang.GetString("PropBorderColor"));
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

                STControlPropertyDescriptor PropEtsWriteAddressIds = new STControlPropertyDescriptor(collection["WriteAddressIds"]);
                PropEtsWriteAddressIds.SetCategory(UIResMang.GetString("CategoryGroupAddress"));
                PropEtsWriteAddressIds.SetDisplayName(UIResMang.GetString("PropEtsWriteAddressIds"));
                list.Add(PropEtsWriteAddressIds);

                STControlPropertyDescriptor PropEtsReadAddressId = new STControlPropertyDescriptor(collection["ReadAddressId"]);
                PropEtsReadAddressId.SetCategory(UIResMang.GetString("CategoryGroupAddress"));
                PropEtsReadAddressId.SetDisplayName(UIResMang.GetString("PropEtsReadAddressId"));
                list.Add(PropEtsReadAddressId);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
        #endregion

        #region 公共方法
        public override void ContainsPoint(Point p, List<ViewNode> nodes)
        {
            base.ContainsPoint(p, nodes);

            foreach (ViewNode node in this.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType != node.Name)
                {
                    node.ContainsPoint(p, nodes);
                }
            }

            foreach (ViewNode node in this.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType == node.Name)
                {
                    node.ContainsPoint(p, nodes);
                }
            }
        }

        public override void MouseDown(MouseEventArgs e)
        {
            base.MouseDown(e);
        }

        public override void MouseMove(MouseEventArgs e)
        {
            base.MouseMove(e);

            foreach (ViewNode node in this.Nodes)
            {
                node.MouseMove(e);
            }
        }

        public override void MouseUp(MouseEventArgs e)
        {
            base.MouseUp(e);
        }
        #endregion
    }
}
