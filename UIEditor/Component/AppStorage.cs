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
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var knxApp = JsonConvert.DeserializeObject<KNXApp>(json, settings);
                return knxApp;
            }

            return null;
        }

        public static void Save(KNXApp knxApp)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            var jsonData = JsonConvert.SerializeObject(knxApp, Formatting.None, settings);
            File.WriteAllText(Path.Combine(MyCache.ProjectFolder, MyConst.KnxUiMetaDataFile), jsonData, Encoding.UTF8);
        }

        /// <summary>
        /// 将KNXView对象保存为文件
        /// </summary>
        /// <param name="knx"></param>
        /// <param name="path"></param>
        public static void SaveAsFile(TemplateMeta tpla, string path)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            var json = JsonConvert.SerializeObject(tpla, Formatting.None, settings);
            File.WriteAllText(path, json, Encoding.UTF8);
        }

        public static TemplateMeta Import(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path, Encoding.UTF8);
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var knx = JsonConvert.DeserializeObject<TemplateMeta>(json, settings);

                return knx;
            }

            return null;
        }
    }
}