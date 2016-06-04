using System.Collections.Generic;

namespace Structure
{
    /// <summary>
    /// 容器类，控件元素的容器
    /// </summary>
    public class KNXContainer : KNXControlBase
    {
        /// <summary>
        /// 摆放在界面上的子控件
        /// </summary>
        public List<KNXControlBase> Controls { get; set; }
    }
}