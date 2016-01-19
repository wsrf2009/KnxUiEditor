using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Structure;

namespace UIEditor.Entity
{
    /// <summary>
    /// 容器节点，主要包括表格类容器
    /// </summary>
    [Serializable]
    public abstract class ContainerNode : ViewNode
    {
        /// <summary>
        /// 当前的格子划分为几行
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// 当前的格子划分为几列
        /// </summary>
        public int ColumnCount { get; set; }

        #region 构造函数
        public ContainerNode()
        {
            RowCount = 1;
            ColumnCount = 1;
        }

        public ContainerNode(KNXContainer knx)
            : base(knx)
        {
            this.Text = knx.Text;
            this.RowCount = knx.RowCount;
            this.ColumnCount = knx.ColumnCount;
        }

        protected ContainerNode(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion

        internal void ToKnx(KNXContainer knx)
        {
            // ViewNode 基类中的字段
            knx.Id = this.Id;
            knx.Text = this.Text;
            // 字段
            knx.RowCount = this.RowCount;
            knx.ColumnCount = this.ColumnCount;
        }
    }
}