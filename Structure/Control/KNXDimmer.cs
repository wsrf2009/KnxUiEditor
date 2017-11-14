using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Control
{
    /// <summary>
    /// 调光执行器
    /// 新增于2.7.1
    /// </summary>
    public class KNXDimmer : KNXControlBase
    {
        public string Symbol { get; set; }

        public string ImageOn { get; set; }

        public string ImageOff { get; set; }

        public KNXObject Switch { get; set; }

        public KNXObject DimRelatively { get; set; }

        public KNXObject DimAbsolutely { get; set; }

        public KNXObject StateOnOff { get; set; }

        public KNXObject StateDimValue { get; set; }
    }
}
