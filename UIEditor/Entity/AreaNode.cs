
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
using System.Windows.Forms;
using System.ComponentModel;

namespace UIEditor.Entity
{
    [TypeConverter(typeof(AreaNode.PropertyConverter))]
    [Serializable]
    public class AreaNode : ViewNode
    {
        private static int index = 0;

        #region 构造函数
        public AreaNode()
        {
            index++;

            this.Text = ResourceMng.GetString("TextArea") + "_" + index;
            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxAreaType;
        }

        public override object Clone()
        {
            AreaNode node = base.Clone() as AreaNode;

            return node;
        }

        public AreaNode(KNXArea knx)
            : base(knx)
        {
            this.Name = ImageKey = SelectedImageKey = MyConst.View.KnxAreaType;
        }

        protected AreaNode(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        public KNXArea ToKnx()
        {
            var knx = new KNXArea();

            base.ToKnx(knx);

            knx.Rooms = new List<KNXRoom>();

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

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }

    }
}
