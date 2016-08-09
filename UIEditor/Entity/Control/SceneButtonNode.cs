using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SourceGrid;
using Structure;
using Structure.Control;
using UIEditor.Component;
using System.ComponentModel;
using System.Collections.Generic;
using UIEditor.PropertyGridEditor;
using System.Drawing.Design;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UIEditor.Entity.Control
{
    [TypeConverter(typeof(SceneButtonNode.PropertyConverter))]
    [Serializable]
    public class SceneButtonNode : ControlBaseNode
    {
        #region 常量
        private const int PADDING = 2;
        #endregion

        #region 变量
        private static int index = 0;
        #endregion

        #region 属性
        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressSingleReadEditor), typeof(UITypeEditor))]
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        [EditorAttribute(typeof(PropertyGridKNXSelectedAddressMultiWriteEditor), typeof(UITypeEditor))]
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string ImageOn { get; set; }

        /// <summary>
        /// 开启时控件的背景色
        /// </summary>
        public Color ColorOn { get; set; }

        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string ImageOff { get; set; }

        /// <summary>
        /// 关闭时控件的背景色
        /// </summary>
        public Color ColorOff { get; set; }

        public EBool IsGroup { get; set; }

        public int DefaultValue { get; set; }
        #endregion

        #region 构造函数
        public SceneButtonNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextSceneButton") + "_" + index;
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSceneButtonType;

            this.Width = 150;
            this.Height = 40;
            //this.Size = new Size(150, 40);
            this.FlatStyle = EFlatStyle.Stereo;

            this.Clickable = EBool.Yes;

            this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();

            this.ImageOn = "stripOn.png";
            this.ColorOn = this.BackgroundColor;
            this.ImageOff = "stripOff.png";
            this.ColorOff = this.BackgroundColor;
            this.IsGroup = EBool.No;

            string FileImageOn = Path.Combine(MyCache.ProjImagePath, this.ImageOn);
            if (!File.Exists(FileImageOn))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.ImageOn), FileImageOn);
            }

            string FileImageOff = Path.Combine(MyCache.ProjImagePath, this.ImageOff);
            if (!File.Exists(FileImageOff))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.ImageOff), FileImageOff);
            }
        }

        public override object Clone()
        {
            SceneButtonNode node = base.Clone() as SceneButtonNode;
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
            node.ColorOn = this.ColorOn;
            node.ImageOff = this.ImageOff;
            node.ColorOff = this.ColorOff;
            node.IsGroup = this.IsGroup;
            node.DefaultValue = this.DefaultValue;

            return node;
        }

        public SceneButtonNode(KNXSceneButton knx)
            : base(knx)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.Controls.KnxSceneButtonType;

            this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();

            this.ImageOn = knx.ImageOn;
            this.ColorOn = FrmMainHelp.HexStrToColor(knx.ColorOn);// knx.ColorOn;
            this.ImageOff = knx.ImageOff;
            this.ColorOff = FrmMainHelp.HexStrToColor(knx.ColorOff);// knx.ColorOff;
            this.IsGroup = (EBool)Enum.ToObject(typeof(EBool), knx.IsGroup);
            this.DefaultValue = knx.DefaultValue;
        }

        protected SceneButtonNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        public KNXSceneButton ToKnx()
        {
            var knx = new KNXSceneButton();

            base.ToKnx(knx);

            knx.ReadAddressId = this.ReadAddressId;
            knx.WriteAddressIds = this.WriteAddressIds;

            knx.ImageOn = this.ImageOn;
            knx.ColorOn = FrmMainHelp.ColorToHexStr(this.ColorOn);// this.ColorOn;
            knx.ImageOff = this.ImageOff;
            knx.ColorOff = FrmMainHelp.ColorToHexStr(this.ColorOff);// this.ColorOff;
            knx.IsGroup = (int)this.IsGroup;
            knx.DefaultValue = this.DefaultValue;

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

                //STControlPropertyDescriptor PropBackColor = new STControlPropertyDescriptor(collection["BackgroundColor"]);
                //PropBackColor.SetCategory(ResourceMng.GetString("CategoryAppearance"));
                //PropBackColor.SetDisplayName(ResourceMng.GetString("PropBackColor"));
                //PropBackColor.SetDescription(ResourceMng.GetString("DescriptionForPropBackgroundColor"));
                //list.Add(PropBackColor);

                STControlPropertyDescriptor PropFlatStyle = new STControlPropertyDescriptor(collection["FlatStyle"]);
                PropFlatStyle.SetCategory(ResourceMng.GetString("CategoryStyle"));
                PropFlatStyle.SetDisplayName(ResourceMng.GetString("PropFlatStyle"));
                PropFlatStyle.SetDescription(ResourceMng.GetString("DescriptionForPropFlatStyle"));
                list.Add(PropFlatStyle);

                STControlPropertyDescriptor PropFontColor = new STControlPropertyDescriptor(collection["FontColor"]);
                PropFontColor.SetCategory(ResourceMng.GetString("CategoryAppearance"));
                PropFontColor.SetDisplayName(ResourceMng.GetString("PropFontColor"));
                PropFontColor.SetDescription(ResourceMng.GetString("DescriptionForPropFontColor"));
                list.Add(PropFontColor);

                STControlPropertyDescriptor PropFontSize = new STControlPropertyDescriptor(collection["FontSize"]);
                PropFontSize.SetCategory(ResourceMng.GetString("CategoryAppearance"));
                PropFontSize.SetDisplayName(ResourceMng.GetString("PropFontSize"));
                PropFontSize.SetDescription(ResourceMng.GetString("DescriptionForPropFontSize"));
                list.Add(PropFontSize);

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

                STControlPropertyDescriptor PropHasTip = new STControlPropertyDescriptor(collection["HasTip"]);
                PropHasTip.SetCategory(ResourceMng.GetString("CategoryOperation"));
                PropHasTip.SetDisplayName(ResourceMng.GetString("PropHasTip"));
                PropHasTip.SetDescription(ResourceMng.GetString("DescriptionForPropHasTip"));
                list.Add(PropHasTip);

                STControlPropertyDescriptor PropTip = new STControlPropertyDescriptor(collection["Tip"]);
                PropTip.SetCategory(ResourceMng.GetString("CategoryOperation"));
                PropTip.SetDisplayName(ResourceMng.GetString("PropTip"));
                PropTip.SetDescription(ResourceMng.GetString("DescriptionForPropTip"));
                list.Add(PropTip);

                STControlPropertyDescriptor PropClickable = new STControlPropertyDescriptor(collection["Clickable"]);
                PropClickable.SetCategory(ResourceMng.GetString("CategoryOperation"));
                PropClickable.SetDisplayName(ResourceMng.GetString("PropClickable"));
                PropClickable.SetDescription(ResourceMng.GetString("DescriptionForPropClickable"));
                list.Add(PropClickable);

                STControlPropertyDescriptor PropSwitchImageOn = new STControlPropertyDescriptor(collection["ImageOn"]);
                PropSwitchImageOn.SetCategory(ResourceMng.GetString("CategoryDisplay"));
                PropSwitchImageOn.SetDisplayName(ResourceMng.GetString("PropSwitchImageOn"));
                PropSwitchImageOn.SetDescription(ResourceMng.GetString("DescriptionForPropImageOn"));
                list.Add(PropSwitchImageOn);

                STControlPropertyDescriptor PropColorOn = new STControlPropertyDescriptor(collection["ColorOn"]);
                PropColorOn.SetCategory(ResourceMng.GetString("CategoryDisplay"));
                PropColorOn.SetDisplayName(ResourceMng.GetString("PropColorOn"));
                PropColorOn.SetDescription(ResourceMng.GetString("DescriptionForPropColorOn"));
                list.Add(PropColorOn);

                STControlPropertyDescriptor PropSwitchImageOff = new STControlPropertyDescriptor(collection["ImageOff"]);
                PropSwitchImageOff.SetCategory(ResourceMng.GetString("CategoryDisplay"));
                PropSwitchImageOff.SetDisplayName(ResourceMng.GetString("PropSwitchImageOff"));
                PropSwitchImageOff.SetDescription(ResourceMng.GetString("DescriptionForPropImageOff"));
                list.Add(PropSwitchImageOff);

                STControlPropertyDescriptor PropColorOff = new STControlPropertyDescriptor(collection["ColorOff"]);
                PropColorOff.SetCategory(ResourceMng.GetString("CategoryDisplay"));
                PropColorOff.SetDisplayName(ResourceMng.GetString("PropColorOff"));
                PropColorOff.SetDescription(ResourceMng.GetString("DescriptionForPropColorOff"));
                list.Add(PropColorOff);

                STControlPropertyDescriptor PropIsGroup = new STControlPropertyDescriptor(collection["IsGroup"]);
                PropIsGroup.SetCategory(ResourceMng.GetString("CategoryGroupBox"));
                PropIsGroup.SetDisplayName(ResourceMng.GetString("PropIsGroup"));
                PropIsGroup.SetDescription(ResourceMng.GetString("DescriptionForPropIsGroup"));
                list.Add(PropIsGroup);

                STControlPropertyDescriptor PropDefaultValue = new STControlPropertyDescriptor(collection["DefaultValue"]);
                PropDefaultValue.SetCategory(ResourceMng.GetString("CategoryGroupBox"));
                PropDefaultValue.SetDisplayName(ResourceMng.GetString("PropDefaultValue"));
                PropDefaultValue.SetDescription(ResourceMng.GetString("DescriptionForPropDefaultValue"));
                list.Add(PropDefaultValue);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }

        public override void DrawAt(Point basePoint, Graphics g)
        {
            base.DrawAt(basePoint, g);

            Rectangle rect = new Rectangle(Point.Empty, this.RectInPage.Size);
            Bitmap bm = new Bitmap(this.RectInPage.Width, this.RectInPage.Height);
            Graphics gp = Graphics.FromImage(bm);

            Color backColor = Color.White;
            if (null != this.ColorOn)
            {
                backColor = Color.FromArgb((int)(this.Alpha * 255), this.ColorOn);
            }
            else if (null != this.ColorOff)
            {
                backColor = Color.FromArgb((int)(this.Alpha * 255), this.ColorOff);
            }
            else
            {
                backColor = Color.FromArgb((int)(this.Alpha * 255), this.BackgroundColor);
            }

            if ((null == this.BackgroundImage) || (string.Empty == this.BackgroundImage))
            {
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
                    FillRoundRectangle(gp, brush, rect, this.Radius, 1.0f);
                    brush.Dispose();
                }
                else if (EFlatStyle.Flat == this.FlatStyle)
                {
                    SolidBrush brush = new SolidBrush(backColor);
                    FillRoundRectangle(gp, brush, rect, this.Radius, 1.0f);
                }
            }

            /* 图标 */
            int x = PADDING;
            int y = PADDING;  // 到父视图顶部的距离
            int width = 0;
            int height = 0;
            Image img = null;
            if (!string.IsNullOrEmpty(this.ImageOn))
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.ImageOn));
            }
            else if (!string.IsNullOrEmpty(this.ImageOff))
            {
                img = Image.FromFile(Path.Combine(MyCache.ProjImagePath, this.ImageOff));
            }
            if (null != img)
            {
                height = rect.Height - 2 * y;
                width = height;
                gp.DrawImage(ImageHelper.Resize(img, new Size(width, height), false), rect.X + x, rect.Y + y);
            }

            /* 文本 */
            if (null != this.Text)
            {
                if (null != img)
                {
                    x += width + PADDING;
                    width = rect.Width - x - PADDING;
                    height =rect.Height - 2 * y;
                }
                else
                {
                    width = rect.Width - 2 * x;
                    height = rect.Height - 2 * y;
                }

                Rectangle stateRect = new Rectangle(rect.X+x, rect.Y+y, width, height);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                Color fontColor = this.FontColor;
                gp.DrawString(this.Text, new Font("宋体", this.FontSize), new SolidBrush(fontColor), stateRect, sf);
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
            }
        }
    }
}
