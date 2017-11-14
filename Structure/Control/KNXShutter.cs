using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Control
{
    /// <summary>
    /// 窗帘控制器
    /// 新增于2.7.1
    /// </summary>
    public class KNXShutter : KNXControlBase
    {
        public string Symbol { get; set; }

        /// <summary>
        /// 窗帘开启时显示的图片。
        /// </summary>
        public string ImageOn { get; set; }

        /// <summary>
        /// 窗帘关闭时显示的图片。
        /// </summary>
        public string ImageOff { get; set; }

        public KNXObject ShutterUpDown { get; set; }

        public KNXObject ShutterStop { get; set; }

        public KNXObject AbsolutePositionOfShutter { get; set; }

        public KNXObject AbsolutePositionOfBlinds { get; set; }

        public KNXObject StateUpperPosition { get; set; }

        public KNXObject StateLowerPosition { get; set; }

        public KNXObject StatusActualPositionOfShutter { get; set; }

        public KNXObject StatusActualPositionOfBlinds { get; set; }
    }
}
