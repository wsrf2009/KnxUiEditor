using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure
{
    [Serializable]
    public class KNXObject
    {
        /// <summary>
        /// 对象群组地址
        /// </summary>
        public Dictionary<string, KNXSelectedAddress> MapSelectedAddress { get; set; }

        /// <summary>
        /// 对象是否启用
        /// </summary>
        public bool Enable { get; set; }

        public KNXObject()
        {
            this.MapSelectedAddress = new Dictionary<string, KNXSelectedAddress>();
            this.Enable = false;
        }

        public KNXObject Copy()
        {
            KNXObject obj = new KNXObject();
            foreach (var item in this.MapSelectedAddress)
            {
                obj.MapSelectedAddress.Add(item.Key, item.Value);
            }

            obj.Enable = this.Enable;

            return obj;
        }

        public void Clean()
        {
            this.MapSelectedAddress.Clear();
            this.Enable = false;
        }

        public string GetGroupAddressName()
        {
            string valString = "";
            foreach (var item in MapSelectedAddress)
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
