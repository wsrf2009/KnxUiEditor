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
    }
}
