using System.Collections.Generic;
using System.ComponentModel;

namespace Structure.Control
{
    /// <summary>
    /// 数码显示
    /// </summary>
    public class KNXValueDisplay : KNXControlBase
    {
        public enum EDisplayAccurancy
        {
            /// <summary>
            /// 没有小数
            /// </summary>
            [Description("无小数")]
            None,

            /// <summary>
            /// 1位小数
            /// </summary>
            [Description("1位小数")]
            Bit1,
        }

        /// <summary>
        /// 读地址
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        /// <summary>
        /// 用于添加单元标识符显示的值，例如一个可选字段：用于显示当前温度，”°C”可以插入。
        /// </summary>
        public int Unit { get; set; }

        /// <summary>
        /// 小数位数
        /// 新增于2.5.2
        /// </summary>
        public int DecimalDigit { get; set; }

        /// <summary>
        /// 数值字体
        /// 新增于2.5.7
        /// </summary>
        public KNXFont ValueFont { get; set; }
    }
}
