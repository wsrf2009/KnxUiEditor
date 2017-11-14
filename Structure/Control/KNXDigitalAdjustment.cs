using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Structure.Control
{
    public class KNXDigitalAdjustment : KNXControlBase
    {
        public enum EDigitalNumber
        {
            [Description("1位数字")]
            OneDigit,

            [Description("2位数字")]
            TwoDigit,

            [Description("3位数字")]
            ThreeDigit
        }

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

        /// <summary>
        /// 已被弃用。2.1.1
        /// </summary>
        public string RightImage { get; set; }

        /// <summary>
        /// 已被弃用。2.5.3
        /// </summary>
        public int DigitalNumber { get; set; }

        /// <summary>
        /// 小数位数
        /// 新增于2.5.3
        /// </summary>
        public int DecimalDigit { get; set; }

        public float MaxValue { get; set; }

        public float MinValue { get; set; }

        /// <summary>
        /// 调节步长
        /// 新增于2.5.3
        /// 弃用于2.5.4，请查看 #RegulationStep
        /// </summary>
        public int RegStep { get; set; }

        /// <summary>
        /// 调节步长，代替 #RegStep
        /// 新增于2.5.4
        /// </summary>
        public string RegulationStep { get; set; }

        public int Unit { get; set; }

        /// <summary>
        /// 数值字体
        /// 新增于2.5.4
        /// </summary>
        public KNXFont ValueFont { get; set; }
    }
}
