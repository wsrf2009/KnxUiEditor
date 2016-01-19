
namespace Structure.Control
{
    /// <summary>
    /// 步进开关
    /// </summary>
    public class KNXSnapperSwitch : KNXControlBase
    {
        //Snapper左边背景图片(SliderSymbol与此属性不能共存)
        public string LeftImage { get; set; }

        //Snapper左边背景图片(SliderSymbol与此属性不能共存)
        public string RightImage { get; set; }

        //Slider滑动图片
        public string SnapperImage { get; set; }

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
