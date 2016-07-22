using System.Collections.Generic;

namespace Structure
{
    /// <summary>
    /// 控件基础类
    /// </summary>
    public class KNXControlBase : KNXView
    {
        ///// <summary>
        ///// 读地址
        ///// </summary>
        //public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        ///// <summary>
        ///// 写地址列表
        ///// </summary>
        //public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        public int HasTip { get; set; }

        public string Tip { get; set; }

        /// <summary>
        /// 控件是否可点击
        /// </summary>
        public int Clickable { get; set; }

        /// <summary>
        /// 用户自定义的控件图标图标
        /// </summary>
        public string Icon { get; set; }
    }
}