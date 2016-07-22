using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SourceGrid;
using SourceGrid.Cells.Editors;
using SourceGrid.Cells.Views;
using Structure;
using UIEditor.Component;
using System.Drawing;
using Structure.Control;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using UIEditor.PropertyGridEditor;
using System.Drawing.Drawing2D;

namespace UIEditor.Entity
{
    [TypeConverter(typeof(GroupBoxNode.PropertyConverter))]
    [Serializable]
    public class GroupBoxNode : ContainerNode
    {
        #region 变量
        private static int index = 0;
        #endregion

        #region 属性
        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressSingleReadEditor), typeof(UITypeEditor))]
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressMultiWriteEditor), typeof(UITypeEditor))]
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }
        #endregion

        #region 构造函数
        public GroupBoxNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextGroupBox") + "_" + index;
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxGroupBoxType;

            this.Width = 300;
            this.Height = 400;
            this.Alpha = .0f;
            this.DisplayBorder = EBool.Yes;
            this.BorderColor = Color.BlanchedAlmond;

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();

            
        }

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

        /// <summary>
        /// KNXGrid 转 GridNode
        /// </summary>
        /// <param name="knx"></param>
        public GroupBoxNode(KNXGroupBox knx)
            : base(knx)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxGroupBoxType;

            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();
        }

        protected GroupBoxNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion

        /// <summary>
        /// GridNode转KNXGrid
        /// </summary>
        /// <returns></returns>
        public KNXGroupBox ToKnx()
        {
            var knx = new KNXGroupBox();

            base.ToKnx(knx);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;

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

                STControlPropertyDescriptor PropBorderWidth = new STControlPropertyDescriptor(collection["DisplayBorder"]);
                PropBorderWidth.SetCategory(ResourceMng.GetString("CategoryBorder"));
                PropBorderWidth.SetDisplayName(ResourceMng.GetString("PropDisplayBorder"));
                PropBorderWidth.SetDescription(ResourceMng.GetString(""));
                list.Add(PropBorderWidth);

                STControlPropertyDescriptor PropBorderColor = new STControlPropertyDescriptor(collection["BorderColor"]);
                PropBorderColor.SetCategory(ResourceMng.GetString("CategoryBorder"));
                PropBorderColor.SetDisplayName(ResourceMng.GetString("PropBorderColor"));
                PropBorderColor.SetDescription(ResourceMng.GetString(""));
                list.Add(PropBorderColor);

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

            if ((null == this.BackgroundImage) || (string.Empty == this.BackgroundImage))
            {
                if (EFlatStyle.Flat == this.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    FillRoundRectangle(gp, brush, rect, this.Radius, 1.0f);
                }
            }

            if (EBool.Yes == this.DisplayBorder)
            {
                Color borderColor = this.BorderColor;
                DrawRoundRectangle(gp, new Pen(borderColor, 1), rect, this.Radius, 1.0f);
            }

            g.DrawImage(bm,
                this.VisibleRectInPage,
                new Rectangle(new Point(this.VisibleRectInPage.X - this.RectInPage.X, this.VisibleRectInPage.Y - this.RectInPage.Y), this.VisibleRectInPage.Size),
                GraphicsUnit.Pixel);

            foreach (ViewNode node in this.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType != node.Name)
                {
                    node.DrawAt(LocationInPage, g);
                }
            }

            foreach (ViewNode node in this.Nodes)
            {
                if (MyConst.Controls.KnxGroupBoxType == node.Name)
                {
                    node.DrawAt(LocationInPage, g);
                }
            }

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

                int centerX;
                int centerY;
                int SideMobile = 16;
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
                int lY = centerY - SideMobile / 2;

                this.MobileRect = new Rectangle(new Point(lX, lY), new Size(SideMobile, SideMobile));

                g.DrawRectangle(Pens.Black, this.MobileRect);
                g.FillRectangle(Brushes.White, this.MobileRect);
                GraphicsPath path = new GraphicsPath();
                int x1 = this.MobileRect.X + this.MobileRect.Width / 2;
                int y1 = this.MobileRect.Y;

                int xr1 = x1 + 3;
                int yr1 = y1 + 3;

                int xl1 = x1 - 3;
                int yl1 = y1 + 3;
                path.AddLines(new Point[]{new Point(x1, y1), new Point(xr1, yr1), new Point(xl1, yl1)});
                g.DrawPath(Pens.Black, path);
                g.FillPath(Brushes.Black, path);

                int x2 = this.MobileRect.Right;
                int y2 = this.MobileRect.Y + this.MobileRect.Height / 2;
                int xb2 = x2 - 3;
                int yb2 = y2 + 3;
                int xt2 = x2 - 3;
                int yt2 = y2 - 3;
                path.Reset();
                path.AddLines(new Point[] { new Point(x2, y2), new Point(xb2, yb2), new Point(xt2, yt2) });
                g.DrawPath(Pens.Black, path);
                g.FillPath(Brushes.Black, path);

                int x3 = x1;
                int y3 = this.MobileRect.Bottom;
                int xl3 = x3 - 3;
                int yl3 = y3 - 3;
                int xr3 = x3 + 3;
                int yr3 = y3 - 3;
                path.Reset();
                path.AddLines(new Point[] { new Point(x3, y3), new Point(xl3, yl3), new Point(xr3, yr3) });
                g.DrawPath(Pens.Black, path);
                g.FillPath(Brushes.Black, path);

                int x4 = this.MobileRect.Left;
                int y4 = y2;
                int xt4 = x4 + 3;
                int yt4 = y4 - 3;
                int xb4 = x4 + 3;
                int yb4 = y4 + 3;
                path.Reset();
                path.AddLines(new Point[] { new Point(x4, y4), new Point(xt4, yt4), new Point(xb4, yb4) });
                g.DrawPath(Pens.Black, path);
                g.FillPath(Brushes.Black, path);

                g.DrawLine(Pens.Black, x1, y1, x3, y3);
                g.DrawLine(Pens.Black, x2, y2, x4, y4);
            }
        }

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
    }
}
