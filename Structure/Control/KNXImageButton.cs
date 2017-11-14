using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure.Control
{
    /// <summary>
    /// 图像按钮
    /// </summary>
    public class KNXImageButton : KNXControlBase
    {
        /// <summary>
        /// 读地址
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }

        /// <summary>
        /// 写地址列表
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

        /// <summary>
        /// 开启时显示的图片。
        /// 新增于2.6.1。
        /// </summary>
        public string ImageOn { get; set; }

        /// <summary>
        /// 关闭时显示的图片。
        /// 新增于2.6.1。
        /// </summary>
        public string ImageOff { get; set; }
    }
}
