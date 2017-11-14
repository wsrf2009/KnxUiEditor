using System.Collections.Generic;

namespace Structure.Control
{
    /// <summary>
    /// 场景开关
    /// </summary>
    public class KNXSceneButton : KNXControlBase
    {
        /// <summary>
        /// 读地址
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        /// <summary>
        /// 写地址列表
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        /// <summary>
        /// 已被弃用。2.1.1
        /// </summary>
        public string ImageOn { get; set; }

        /// <summary>
        /// 开启时控件的背景色
        /// </summary>
        public string ColorOn { get; set; }

        /// <summary>
        /// 已被弃用。2.1.1
        /// </summary>
        public string ImageOff { get; set; }

        /// <summary>
        /// 关闭时控件的背景色
        /// </summary>
        public string ColorOff { get; set; }

        public int IsGroup { get; set; }

        public int DefaultValue { get; set; }
    }
}
