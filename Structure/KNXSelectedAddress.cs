using System;

namespace Structure
{
    [Serializable]
    public class KNXSelectedAddress
    {
        public string Id { get; set; }

        public string Name { get; set; }

        // 数据类型
        public int Type { get; set; }

        //public string Type { get; set; }

        //public string Size { get; set; }

        // 默认值
        public string DefaultValue { get; set; }
    }
}