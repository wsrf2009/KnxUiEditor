using System.Collections.Generic;

namespace Structure.Control
{
    /// <summary>
    /// 滑动开关
    /// 滑动条上添加数据类型：百分比，相对值，绝对值
    /// ValueDisplayNode 添加显示类型
    /// </summary>
    public class KNXSliderSwitch : KNXControlBase
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
        /// Slider左边背景图片(SliderSymbol与此属性不能共存)。
        /// 已被弃用，2.1.1。
        /// </summary>
        public string LeftImage { get; set; }

        /// <summary>
        /// Slider左边背景图片(SliderSymbol与此属性不能共存)。
        /// 已被弃用，2.1.1。
        /// </summary>
        public string RightImage { get; set; }

        /// <summary>
        /// Slider滑动图片。
        /// 已被弃用，2.1.1。
        /// </summary>
        public string SliderImage { get; set; }

        public int IsRelativeControl { get; set; }

        public int Orientation { get; set; }

        public int SliderWidth { get; set; }
    }
}
