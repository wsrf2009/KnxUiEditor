
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
using Button = SourceGrid.Cells.Button;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;

namespace UIEditor.Entity
{
    [TypeConverter(typeof(RoomNode.PropertyConverter))]
    [Serializable]
    public class RoomNode : ViewNode
    {
        private static int index = 0;

        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string Symbol { get; set; }

        public string PinCode { get; set; }

        /// <summary>
        /// 是否将该房间作为默认显示页面
        /// </summary>
        public EBool IsDefaultRoom { get; set; }

        #region 构造函数

        public RoomNode()
        {
            index++;

            Text = ResourceMng.GetString("TextRoom") + "_" + index;

            Symbol = MyConst.DefaultIcon;
            PinCode = "";
            this.IsDefaultRoom = EBool.No;

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxRoomType;
        }

        public override object Clone()
        {
            RoomNode node = base.Clone() as RoomNode;
            node.Symbol = this.Symbol;
            node.PinCode = this.PinCode;
            node.IsDefaultRoom = this.IsDefaultRoom;

            return node;
        }

        public RoomNode(KNXRoom knx)
            : base(knx)
        {
            Symbol = knx.Symbol;
            PinCode = knx.PinCode;
            this.IsDefaultRoom = (EBool)Enum.ToObject(typeof(EBool), knx.DefaultRoom);

            Name = ImageKey = SelectedImageKey = MyConst.View.KnxRoomType;
        }

        protected RoomNode(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion

        public KNXRoom ToKNX()
        {
            var knx = new KNXRoom();

            base.ToKnx(knx);

            knx.Symbol = this.Symbol;
            knx.PinCode = this.PinCode;
            knx.DefaultRoom = (int)this.IsDefaultRoom;

            knx.Pages = new List<KNXPage>();

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

                STControlPropertyDescriptor PropIcon = new STControlPropertyDescriptor(collection["Symbol"]);
                PropIcon.SetCategory(ResourceMng.GetString(""));
                PropIcon.SetDisplayName(ResourceMng.GetString("PropIcon"));
                PropIcon.SetDescription(ResourceMng.GetString(""));
                list.Add(PropIcon);

                STControlPropertyDescriptor PropPassword = new STControlPropertyDescriptor(collection["PinCode"]);
                PropPassword.SetCategory(ResourceMng.GetString(""));
                PropPassword.SetDisplayName(ResourceMng.GetString("PropPassword"));
                PropPassword.SetDescription(ResourceMng.GetString(""));
                list.Add(PropPassword);

                STControlPropertyDescriptor PropIsDefaultRoom = new STControlPropertyDescriptor(collection["IsDefaultRoom"]);
                PropIsDefaultRoom.SetCategory(ResourceMng.GetString(""));
                PropIsDefaultRoom.SetDisplayName(ResourceMng.GetString("PropIsDefaultRoom"));
                PropIsDefaultRoom.SetDescription(ResourceMng.GetString(""));
                list.Add(PropIsDefaultRoom);

                return new PropertyDescriptorCollection(list.ToArray());
            }
        }
    }
}
