
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
        /// Slider左边背景图片(SliderSymbol与此属性不能共存)
        /// </summary>
        public string LeftImage { get; set; }
        //public int LeftImageLeft { get; set; }
        //public int LeftImageTop { get; set; }
        //public int LeftImageWidth { get; set; }
        //public int LeftImageHeight { get; set; }



        /// <summary>
        /// Slider左边背景图片(SliderSymbol与此属性不能共存)
        /// </summary>
        public string RightImage { get; set; }
        //public int RightImageLeft { get; set; }
        //public int RightImageTop { get; set; }
        //public int RightImageWidth { get; set; }
        //public int RightImageHeight { get; set; }

        /// <summary>
        /// Slider滑动图片
        /// </summary>
        public string SliderImage { get; set; }

        ///// <summary>
        ///// 最小值
        ///// </summary>
        //public int MinValue { get; set; }

        ///// <summary>
        ///// 最大值
        ///// </summary>
        //public int MaxValue { get; set; }


        //要滑块两侧显示的符号。
        //public SliderSymbol ControlSymbol { get; set; }


        ////滑动时延迟时间(单位毫秒)
        //public int SendInterval { get; set; }

        public bool IsRelativeControl { get; set; }
    }
}
