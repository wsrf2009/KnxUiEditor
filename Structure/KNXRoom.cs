
using System.Collections.Generic;
namespace Structure
{
    /// <summary>
    /// 房间，或者一个指定的区域， 
    /// 主要是为一个小的区域指定一个名称
    /// </summary>
    public class KNXRoom : KNXView
    {
        /// <summary>
        /// 图标
        /// </summary>
        public string Symbol { get; set; }

        public string PinCode { get; set; }

        public List<KNXPage> Pages { get; set; }

    }
}
