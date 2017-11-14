using System;
using System.Collections.Generic;

namespace Structure
{
    [Serializable]
    public class KNXSelectedAddress
    {
        public string Id { get; set; }

        public string Name { get; set; }

        // 数据类型
        public int Type { get; set; }

        // 默认值
        public string DefaultValue { get; set; }

        public static string GetGroupAddressName(Dictionary<string, KNXSelectedAddress> GroupAddress)
        {
            string valString = "";
            foreach (var item in GroupAddress)
            {
                if (!string.IsNullOrEmpty(valString))
                {
                    valString += ";";
                }

                valString += item.Value.Name;

            }
            return valString;
        }
    }
}