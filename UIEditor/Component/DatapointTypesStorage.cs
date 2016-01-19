using Newtonsoft.Json;
using Structure.ETS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace UIEditor.Component
{
    public class DatapointTypesStorage
    {
        public static List<KNXDatapointType> Load()
        {
            string datapointType = Properties.Resources.DatapointType;

            //Console.Write("datapointType: " + datapointType);

            if (null != datapointType)
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var datapointTypeList = JsonConvert.DeserializeObject<List<KNXDatapointType>>(datapointType, settings);

                List<KNXDatapointType> list = new List<KNXDatapointType>(datapointTypeList);

                //Console.Write("list: " + list);

                return list;
            }

            return new List<KNXDatapointType>();
        }
    }
}
