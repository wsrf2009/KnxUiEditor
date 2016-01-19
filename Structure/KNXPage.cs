
using System.Collections.Generic;

namespace Structure
{
    /// <summary>
    /// 页面，主要用于显示用户的
    /// </summary>
    public class KNXPage : KNXContainer
    {
        /// <summary>
        /// 背景图片，如果有图片，则优先显示
        /// </summary>
        public string BackgroudImage { get; set; }

        /// <summary>
        /// 界面上摆放的表格，控件容器
        /// </summary>
        public List<KNXGrid> Grids { get; set; }

    }
}
