using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Structure;
using Structure.ETS;
using UIEditor.GroupAddress;

namespace UIEditor.Component
{
    public class VersionStorage
    {
        public static KNXVersion Load()
        {
            string verFile = Path.Combine(MyCache.ProjectFolder, MyConst.VersionFile);

            if (File.Exists(verFile))
            {
                string json = File.ReadAllText(verFile, Encoding.UTF8);
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var groupAddressList = JsonConvert.DeserializeObject<KNXVersion>(json, settings);
                return groupAddressList;
            }

            return new KNXVersion();
        }

        /// <summary>
        /// 保存 KNXVersion 为 Json 文件
        /// </summary>
        public static void Save()
        {
            MyCache.ProjectVersion.Version += 1;
            MyCache.ProjectVersion.EditorVersion = Application.ProductVersion;
            MyCache.ProjectVersion.LastModified = DateTime.Now.ToString();

            //
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            var jsonData = JsonConvert.SerializeObject(MyCache.ProjectVersion, Formatting.Indented, settings);
            File.WriteAllText(Path.Combine(MyCache.ProjectFolder, MyConst.VersionFile), jsonData, Encoding.UTF8);
        }
    }
}
