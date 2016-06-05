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

        public string LeftImage { get; set; }
        public string RightImage { get; set; }
        public int DigitalNumber { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int Unit { get; set; }
    }
}
