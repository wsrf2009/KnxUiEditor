
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

        //public string CMeasurementUnit[] = {
        //    ""
        //    public const string Centigrade = "℃";
        //    public const string Fahrenheit = "℉";
        //    public const string Ampere = "A";
        //    public const string Milliampere = "mA";
        //    public const string Kilowatt = "KW";
        //}

        //// 标签
        //public string Lable { get; set; }


        /// <summary>
        /// 用于添加单元标识符显示的值，例如一个可选字段：用于显示当前温度，”°C”可以插入。
        /// </summary>
        public int Unit { get; set; }


        ///// <summary>
        ///// 显示的值
        ///// </summary>
        //public string Value { get; set; }


        //// 可选的姓名或名称的显示值。这是类似的标签，但直接显示的实际值。
        //public string Description { get; set; }


        ///// <summary>
        ///// 定义小数位数
        ///// </summary>
        //public int DecimalPlaces { get; set; }

    }
}
