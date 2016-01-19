using System.Collections.Generic;

namespace Structure
{
    /// <summary>
    /// 容器类，控件元素的容器
    /// </summary>
    public class KNXContainer : KNXView
    {
        /// <summary>
        /// 当前的格子划分为几行
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// 当前的格子划分为几列
        /// </summary>
        public int ColumnCount { get; set; }

        /// <summary>
        /// 摆放在界面上的子控件
        /// </summary>
        public List<KNXControlBase> Controls { get; set; }
    }
}