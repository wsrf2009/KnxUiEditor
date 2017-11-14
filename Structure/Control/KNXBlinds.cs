using System.Collections.Generic;

namespace Structure.Control
{
    /// <summary>
    /// 窗帘开关
    /// 弃用于2.7.1
    /// 有 @KNXShutter 代替
    /// </summary>
    public class KNXBlinds : KNXControlBase
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
        public string LeftImage { get; set; }

        public string LeftText { get; set; }

        /// <summary>
        /// 已被弃用。2.5.2
        /// </summary>
        public int LeftTextFontSize { get; set; }

        /// <summary>
        /// 已被弃用。2.5.2
        /// </summary>
        public string LeftTextFontColor { get; set; }

        /// <summary>
        /// 新增于2.5.2
        /// </summary>
        public KNXFont LeftTextFont { get; set; }

        /// <summary>
        /// 已被弃用。2.1.1
        /// </summary>
        public string RightImage { get; set; }

        public string RightText { get; set; }

        /// <summary>
        /// 已被弃用。2.5.2
        /// </summary>
        public int RightTextFontSize { get; set; }

        /// <summary>
        /// 已被弃用。2.5.2
        /// </summary>
        public string RightTextFontColor { get; set; }

        /// <summary>
        /// 新增于2.5.2
        /// </summary>
        public KNXFont RightTextFont { get; set; }
    }
}
