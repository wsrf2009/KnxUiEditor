using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using UIEditor.Component;
using System.Drawing.Design;
using UIEditor.PropertyGridEditor;
using Structure;
using SourceGrid;
using SourceGrid.Cells.Controllers;
using UIEditor.UserClass;

namespace UIEditor.Entity
{
    [Serializable]
    public abstract class ControlBaseNode : ViewNode
    {
        #region 属性
        public EBool HasTip { get; set; }

        [EditorAttribute(typeof(PropertyGridMultiLineTextEditor), typeof(UITypeEditor))]
        public string Tip { get; set; }

        /// <summary>
        /// 控件是否可点击
        /// </summary>
        public EBool Clickable { get; set; }
        #endregion

        #region 构造函数
        public ControlBaseNode() : base()
        {
            this.HasTip = EBool.No;
            this.Tip = "";
            this.Clickable = EBool.Yes;
        }

        public override object Clone()
        {
            ControlBaseNode node = base.Clone() as ControlBaseNode;
            node.HasTip = this.HasTip;
            node.Tip = this.Tip;
            node.Clickable = this.Clickable;

            return node;
        }

        public override object Copy()
        {
            return base.Copy() as ControlBaseNode;
        }

        /// <summary>
        /// KNXControlBase 转 ControlBaseNode
        /// </summary>
        /// <param name="knx"></param>
        public ControlBaseNode(KNXControlBase knx, BackgroundWorker worker)
            : base(knx, worker)
        {
            this.HasTip = (EBool)Enum.ToObject(typeof(EBool), knx.HasTip);
            this.Tip = knx.Tip;
            this.Clickable = (EBool)Enum.ToObject(typeof(EBool), knx.Clickable);
        }
        #endregion

        #region KNX

        /// <summary>
        /// ControlBaseNode 转 KNXControlBase
        /// </summary>
        /// <param name="knx"></param>
        public void ToKnx(KNXControlBase knx, BackgroundWorker worker)
        {
            base.ToKnx(knx, worker);

            knx.HasTip = (int)this.HasTip;
            knx.Tip = this.Tip;
            knx.Clickable = (int)this.Clickable;
        }
        #endregion
    }
}
