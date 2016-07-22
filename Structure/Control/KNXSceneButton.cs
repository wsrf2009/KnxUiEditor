
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

        public string ImageOn { get; set; }

        /// <summary>
        /// 开启时控件的背景色
        /// </summary>
        public string ColorOn { get; set; }

        public string ImageOff { get; set; }

        /// <summary>
        /// 关闭时控件的背景色
        /// </summary>
        public string ColorOff { get; set; }

        public int IsGroup { get; set; }

        public int DefaultValue { get; set; }

        ////按钮描述
        //public string Description { get; set; }

        ////是否有长按事件
        //public bool HasLongClickCommand { get; set; }
    }
}
