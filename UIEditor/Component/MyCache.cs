using System.ComponentModel;
using Structure;
using UIEditor.Component;
using UIEditor.ETS;
using Structure.ETS;
using System.Collections.Generic;

namespace UIEditor
{
    public static class MyCache
    {
        // 项目目录，存放当前编辑的项目的所有文件
        public static string ProjectFolder;
        // 项目的临时目录
        public static string ProjTempFolder;
        // 项目图片存放目录
        public static string ProjImagePath;
        // 项目默认的存储目录
        public static string DefaultKnxProjectFolder;
        // 项目资源目录
        public static string DefaultKnxResurceFolder;
        // 项目发布目录
        public static string DefaultKnxPublishFolder;
        // 项目资源目录
        public static string DefaultKnxCacheFolder;

        //public static string ProjectStartupDir;

        private static BindingList<GroupAddress> _addressTable;
        private static KNXVersion _projectVersion;
        private static List<KNXDatapointType> _datapointTypeTable;

        public static BindingList<GroupAddress> GroupAddressTable
        {
            get
            {
                if (_addressTable == null)
                {
                    _addressTable = new BindingList<GroupAddress>(GroupAddressStorage.Load());
                }
                return _addressTable;
            }
            set { _addressTable = value; }
        }


        public static KNXVersion ProjectVersion
        {
            get
            {
                if (_projectVersion == null)
                {
                    _projectVersion = VersionStorage.Load();
                }
                return _projectVersion;
            }
            set { _projectVersion = value; }
        }

        public static List<KNXDatapointType> DatapointTypeTable
        {
            get
            {
                if (_datapointTypeTable == null)
                {
                    _datapointTypeTable = new List<KNXDatapointType>(DatapointTypesStorage.Load());
                }
                return _datapointTypeTable;
            }
            set { _datapointTypeTable = value; }
        }
    }
}
