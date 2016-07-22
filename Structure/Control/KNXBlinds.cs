
using System.Collections.Generic;
namespace Structure.Control
{

    /// <summary>
    /// 窗帘开关
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
        public string LeftImage { get; set; }
        public string LeftText { get; set; }
        public int LeftTextFontSize { get; set; }
        public string LeftTextFontColor { get; set; }
        public string RightImage { get; set; }
        public string RightText { get; set; }
        public int RightTextFontSize { get; set; }
        public string RightTextFontColor { get; set; }

        ///// <summary>
        ///// true：只有一个标签，标签将显示在百叶窗控制中心
        ///// false：在左右各显示的标签，
        ///// </summary>
        //public bool HasSingleLabel { get; set; }

        ///// <summary>
        ///// 左标签
        ///// </summary>
        //public string LeftText { get; set; }

        ///// <summary>
        ///// 右标签
        ///// </summary>
        //public string RightText { get; set; }

        ///// <summary>
        ///// 是否有长按事件
        ///// </summary>
        //public bool HasLongClickCommand { get; set; }

        ///// <summary>
        ///// 滑块两侧要显示的符号
        ///// </summary>
        //public string ControlSymbol { get; set; }
    }
}
