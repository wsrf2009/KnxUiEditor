using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Structure.ETS;
using UIEditor.GroupAddress;

namespace UIEditor
{
    public class GroupAddressStorage
    {
        /// <summary>
        /// 从 Json 文件中装载 KNXGroupAddress
        /// </summary>
        /// <returns></returns>
        public static BindingList<EdGroupAddress> Load()
        {
            string addressFile = Path.Combine(MyCache.ProjectFolder, MyConst.GroupAddressFile);

            if (File.Exists(addressFile))
            {
                string json = File.ReadAllText(addressFile, Encoding.UTF8);
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var groupAddressList = JsonConvert.DeserializeObject<List<KNXGroupAddress>>(json, settings);

                return new BindingList<EdGroupAddress>(groupAddressList.Select(it => new EdGroupAddress(it)).ToList());
            }

            return new BindingList<EdGroupAddress>();
        }

        /// <summary>
        /// 保存 KNXGroupAddress 为 Json 文件
        /// </summary>
        /// <param name="addressTable"></param>
        public static void Save()
        {
            // 对象
            List<KNXGroupAddress> data = MyCache.GroupAddressTable.Select(it => it.ToKnx()).ToList();

            //
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            var jsonData = JsonConvert.SerializeObject(data, Formatting.None, settings);
            File.WriteAllText(Path.Combine(MyCache.ProjectFolder, MyConst.GroupAddressFile), jsonData, Encoding.UTF8);
        }
    }
}