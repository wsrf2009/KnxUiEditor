

using System.Collections.Generic;
namespace Structure.Control
{
    /// <summary>
    /// 表格，主要用于界面上元素的布局
    /// </summary>
    public class KNXGroupBox : KNXContainer
    {
        ///// <summary>
        ///// 当前格子从哪一行开始
        ///// </summary>
        //public int Top { get; set; }

        ///// <summary>
        ///// 当前的表格从哪一列开始
        ///// </summary>
        //public int Left { get; set; }

        ///// <summary>
        ///// 占几个行
        ///// </summary>
        //public int Height { get; set; }

        ///// <summary>
        ///// 占几列
        ///// </summary>
        //public int Width { get; set; }

        ///// <summary>
        ///// grid 边框样式
        ///// </summary>
        //public string BorderStyle { get; set; }

        /// <summary>
        /// 读地址
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        /// <summary>
        /// 写地址列表
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }
    }
}
