using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Structure;
using System.ComponentModel;

namespace UIEditor.Entity
{
    /// <summary>
    /// 容器节点，主要包括表格类容器
    /// </summary>
    [Serializable]
    public abstract class ContainerNode : ControlBaseNode
    {

        #region 构造函数
        public ContainerNode():base()
        { }

        public override object Clone()
        {
            return base.Clone() as ContainerNode;
        }

        public override object Copy()
        {
            return base.Copy() as ContainerNode;
        }

        public ContainerNode(KNXContainer knx, BackgroundWorker worker)
            : base(knx, worker)
        { }
        #endregion

        /// <summary>
        /// ContainerNode 转 KNXContainer
        /// </summary>
        /// <param name="knx"></param>
        internal void ToKnx(KNXContainer knx, BackgroundWorker worker)
        {
            base.ToKnx(knx, worker);

            knx.Controls = new List<KNXControlBase>();
        }
    }
}