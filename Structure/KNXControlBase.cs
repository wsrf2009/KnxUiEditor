using System.Collections.Generic;

namespace Structure
{
    /// <summary>
    /// 控件基础类
    /// </summary>
    public class KNXControlBase : KNXView
    {
        /// <summary>
        /// 控件位置，位于父控件网格中的位置
        /// 列
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// 行
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// 跨越的列数
        /// </summary>
        public int ColumnSpan { get; set; }
        /// <summary>
        /// 跨越的行数
        /// </summary>
        public int RowSpan { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        public string BackColor { get; set; }

        /// <summary>
        /// 前景色
        /// </summary>
        public string ForeColor { get; set; }

        public bool HasTip { get; set; }

        public string Tip { get; set; }

        /// <summary>
        /// 读地址
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> ReadAddressId { get; set; }
        /// <summary>
        /// ETS项目中该控件的ID
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> WriteAddressIds { get; set; }

    }
}