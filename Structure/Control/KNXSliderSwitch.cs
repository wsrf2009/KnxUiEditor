
namespace Structure.Control
{
    /// <summary>
    /// 滑动开关
    /// 滑动条上添加数据类型：百分比，相对值，绝对值
    /// ValueDisplayNode 添加显示类型
    /// </summary>
    public class KNXSliderSwitch : KNXControlBase
    {
        //Slider左边背景图片(SliderSymbol与此属性不能共存)
        public string LeftImage { get; set; }


        //Slider左边背景图片(SliderSymbol与此属性不能共存)
        public string RightImage { get; set; }


        //Slider滑动图片
        public string SliderImage { get; set; }

        //最小值
        public int MinValue { get; set; }


        //最大值
        public int MaxValue { get; set; }


        //要滑块两侧显示的符号。
        public SliderSymbol ControlSymbol { get; set; }


        //滑动时延迟时间(单位毫秒)
        public int SendInterval { get; set; }

    }
}
