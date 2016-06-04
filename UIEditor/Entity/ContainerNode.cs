using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Structure;
using System.Collections.Generic;

namespace UIEditor.Entity
{
    /// <summary>
    /// 容器节点，主要包括表格类容器
    /// </summary>
    [Serializable]
    public abstract class ContainerNode : ControlBaseNode
    {

        #region 构造函数
        public ContainerNode()
        { }

        public ContainerNode(KNXContainer knx)
            : base(knx)
        { }

        protected ContainerNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion

        /// <summary>
        /// ContainerNode 转 KNXContainer
        /// </summary>
        /// <param name="knx"></param>
        internal void ToKnx(KNXContainer knx)
        {
            base.ToKnx(knx);

            knx.Controls = new List<KNXControlBase>();
        }
    }
}