using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Structure;

namespace UIEditor.Component
{
    public class AppStorage
    {
        private static long initId = DateTime.Now.Ticks;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static KNXApp Load()
        {
            string metaDataFile = Path.Combine(MyCache.ProjectFolder, MyConst.KnxUiMetaDataFile);

            if (File.Exists(metaDataFile))
            {
                string json = File.ReadAllText(metaDataFile, Encoding.UTF8);

                // 替换旧文件
                //int index = json.IndexOf("control_id", StringComparison.Ordinal);
                //while (index > 0)
                //{
                //    json = json.Substring(0, index) + Convert.ToString(initId + index) + json.Substring(index + 10);
                //    index = json.IndexOf("control_id", StringComparison.Ordinal);
                //}

                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var knxApp = JsonConvert.DeserializeObject<KNXApp>(json, settings);
                return knxApp;
            }

            return null;
        }


        public static void Save(KNXApp knxApp)
        {
            //
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            var jsonData = JsonConvert.SerializeObject(knxApp, Formatting.None, settings);
            File.WriteAllText(Path.Combine(MyCache.ProjectFolder, MyConst.KnxUiMetaDataFile), jsonData, Encoding.UTF8);
        }
    }
}