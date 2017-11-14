using System.Collections.Generic;

namespace Structure.Control
{
    /// <summary>
    /// 表格，主要用于界面上元素的布局
    /// </summary>
    public class KNXGroupBox : KNXContainer
    {
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
