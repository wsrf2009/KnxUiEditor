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
using System.Windows.Forms;
using System.ComponentModel;

namespace UIEditor.Entity
{
    [TypeConverter(typeof(PageNode.PropertyConverter))]
    [Serializable]
    public class PageNode : ContainerNode
    {
        #region 变量
        private static int index = 0;
        #endregion

        #region 构造函数

        public PageNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextPage") + "_" + index;
            this.Width = 1280;
            this.Height = 800;
            //this.Size = new Size(1280, 800);
            this.Alpha = 1;
            this.Radius = 0;
            //this.BackgroundColor = "#F0FFFF";
            this.BackgroundImage = "bj_kt.png";

            string FileImageOn = Path.Combine(MyCache.ProjImagePath, this.BackgroundImage);
            if (!File.Exists(FileImageOn))
            {
                File.Copy(Path.Combine(MyCache.ProjectResImgDir, this.BackgroundImage), Path.Combine(MyCache.ProjImagePath, this.BackgroundImage));
            }

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxPageType;
        }

        public override object Clone()
        {
            PageNode node = base.Clone() as PageNode;
            return node;
        }

        /// <summary>
        /// PageNode 转 KNXPage
        /// </summary>
        /// <param name="knx"></param>
        public PageNode(KNXPage knx)
            : base(knx)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxPageType;
        }

        protected PageNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        /// <summary>
        /// PageNode 转 KNXPage
        /// </summary>
        /// <returns></returns>
        public KNXPage ToKnx()
        {
            var knx = new KNXPage();

            base.ToKnx(knx);

            //knx.GroupBoxs = new List<KNXGroupBox>();

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

                //STControlPropertyDescriptor PropSize = new STControlPropertyDescriptor(collection["Size"]);
                //PropSize.SetCategory(ResourceMng.GetString("CategoryLayout"));
                //PropSize.SetDisplayName(ResourceMng.GetString("PropSize"));
                //PropSize.SetDescription(ResourceMng.GetString(""));
                //list.Add(PropSize);

                STControlPropertyDescriptor PropBackColor = new STControlPropertyDescriptor(collection["BackgroundColor"]);
                PropBackColor.SetCategory(ResourceMng.GetString(""));
                PropBackColor.SetDisplayName(ResourceMng.GetString("PropBackColor"));
                PropBackColor.SetDescription(ResourceMng.GetString(""));
                list.Add(PropBackColor);

                STControlPropertyDescriptor PropBackgroundImage = new STControlPropertyDescriptor(collection["BackgroundImage"]);
                PropBackgroundImage.SetCategory(ResourceMng.GetString(""));
                PropBackgroundImage.SetDisplayName(ResourceMng.GetString("PropBackgroundImage"));
                PropBackgroundImage.SetDescription(ResourceMng.GetString(""));
                list.Add(PropBackgroundImage);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
    }
}