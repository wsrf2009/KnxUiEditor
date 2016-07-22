
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using DevAge.Text.FixedLength;
using SourceGrid;
using SourceGrid.Cells.Controllers;
using Structure;
using UIEditor.Controls;
using Button = SourceGrid.Cells.Button;
using System.Drawing;
using System.ComponentModel;
using UIEditor.Component;
using System.Drawing.Design;
using UIEditor.PropertyGridEditor;

namespace UIEditor.Entity
{
    [Serializable]
    public abstract class ControlBaseNode : ViewNode
    {
        #region 属性
        //public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        //public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        public EBool HasTip { get; set; }

        [EditorAttribute(typeof(PropertyGridRichTextEditor), typeof(UITypeEditor))]
        public string Tip { get; set; }

        /// <summary>
        /// 控件是否可点击
        /// </summary>
        public EBool Clickable { get; set; }

        /// <summary>
        /// 用户自定义的控件图标图标
        /// </summary>
        [EditorAttribute(typeof(PropertyGridImageEditor), typeof(UITypeEditor))]
        public string Icon { get; set; }

        #endregion

        #region 构造函数

        public ControlBaseNode()
        {
            //this.ReadAddressId = new Dictionary<string, KNXSelectedAddress>();
            //this.WriteAddressIds = new Dictionary<string, KNXSelectedAddress>();
            this.HasTip = EBool.No;
            this.Tip = "";
            this.Clickable = EBool.No;
            this.Icon = null;
        }

        public override object Clone()
        {
            ControlBaseNode node = base.Clone() as ControlBaseNode;
            node.HasTip = this.HasTip;
            node.Tip = this.Tip;
            node.Clickable = this.Clickable;
            node.Icon = this.Icon;

            return node;
        }

        /// <summary>
        /// KNXControlBase 转 ControlBaseNode
        /// </summary>
        /// <param name="knx"></param>
        public ControlBaseNode(KNXControlBase knx)
            : base(knx)
        {
            //    this.ReadAddressId = knx.ReadAddressId ?? new Dictionary<string, KNXSelectedAddress>();
            //    this.WriteAddressIds = knx.WriteAddressIds ?? new Dictionary<string, KNXSelectedAddress>();
            this.HasTip = (EBool)Enum.ToObject(typeof(EBool), knx.HasTip);
            this.Tip = knx.Tip;
            this.Clickable = (EBool)Enum.ToObject(typeof(EBool), knx.Clickable);
            this.Icon = knx.Icon;
        }

        protected ControlBaseNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region KNX

        /// <summary>
        /// ControlBaseNode 转 KNXControlBase
        /// </summary>
        /// <param name="knx"></param>
        public void ToKnx(KNXControlBase knx)
        {
            base.ToKnx(knx);

            //knx.ReadAddressId = this.ReadAddressId;
            //knx.WriteAddressIds = this.WriteAddressIds;
            knx.HasTip = (int)this.HasTip;
            knx.Tip = this.Tip;
            knx.Clickable = (int)this.Clickable;
            knx.Icon = this.Icon;
        }

        /// <summary>
        /// 转换ETS Address ID字典为逗号分隔的字符串。
        /// </summary>
        /// <param name="etsAddIdDict"></param>
        /// <returns></returns>
        public static string EtsAddressDictToString(Dictionary<string, KNXSelectedAddress> etsAddIdDict)
        {
            var builder = new StringBuilder();
            foreach (var it in etsAddIdDict)
            {
                // 检查地址对象是否存在
                var address = MyCache.GroupAddressTable.FirstOrDefault(x => x.Id == it.Key);
                if (address != null)
                {
                    // 如果系统的地址列表中有则显示其名称
                    builder.Append(it.Value.Name).Append(MyConst.SplitSymbol);
                }
            }
            return builder.ToString().TrimEnd(MyConst.SplitSymbol);
        }

        #endregion
    }
}
