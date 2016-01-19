
namespace Structure.Control
{

    /// <summary>
    /// 窗帘开关
    /// </summary>
    public class KNXBlinds : KNXControlBase
    {

        public KNXBlinds()
        {
        }

        /// <summary>
        /// true：只有一个标签，标签将显示在百叶窗控制中心
        /// false：在左右各显示的标签，
        /// </summary>
        public bool HasSingleLabel { get; set; }

        /// <summary>
        /// 左标签
        /// </summary>
        public string LeftText { get; set; }

        /// <summary>
        /// 右标签
        /// </summary>
        public string RightText { get; set; }

        /// <summary>
        /// 是否有长按事件
        /// </summary>
        public bool HasLongClickCommand { get; set; }

        /// <summary>
        /// 滑块两侧要显示的符号
        /// </summary>
        public string ControlSymbol { get; set; }
    }
}
