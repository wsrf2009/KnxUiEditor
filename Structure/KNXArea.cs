
using System.Collections.Generic;


namespace Structure
{
    /// <summary>
    /// 楼层，区域或者空间： 比如 1楼，二楼，室外等
    /// 区域是一个逻辑的概念，你可以任意划分
    /// </summary>
    public class KNXArea : KNXView
    {
        /// <summary>
        /// 包含的房间
        /// </summary>
        public List<KNXRoom> Rooms { get; set; }
    }
}
