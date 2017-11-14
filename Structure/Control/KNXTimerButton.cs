using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structure.Control
{
    /// <summary>
    /// 定时任务按钮
    /// </summary>
    public class KNXTimerButton : KNXControlBase
    {
        /// <summary>
        /// 读地址
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        /// <summary>
        /// 写地址列表
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }
        //public string Icon { get; set; }

        ///// <summary>
        ///// 定时器组地址列表
        ///// 增加于2.6.2
        ///// </summary>
        //public Dictionary<string, KNXSelectedAddress> KNXAddressIds { get; set; }

        /// <summary>
        /// 定时器图标。
        /// 增加于 2.1.1。
        /// </summary>
        public string Symbol { get; set; }
    }
}
