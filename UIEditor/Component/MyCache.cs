using System.ComponentModel;
using Structure;
using UIEditor.Component;
using UIEditor.GroupAddress;
using Structure.ETS;
using System.Collections.Generic;
using System.Windows.Forms;
using UIEditor.KNX.DatapointType;

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

        /// <summary>
        /// 当前运行的程序所在目录
        /// </summary>
        public static string ProjectStartupDir;

        /// <summary>
        /// 资源文件夹Resource所在的目录
        /// </summary>
        public static string ProjectResourceDir;

        /// <summary>
        /// 图片文件夹所在的目录
        /// </summary>
        public static string ProjectResImgDir;

        /// <summary>
        /// 控件图标所在目录
        /// </summary>
        public static string ProjectResCtrlDir;

        private static Dictionary<int, string> _DicMainNum;
        public static Dictionary<int, string> DicMainNumber
        {
            get
            {
                if (null == _DicMainNum)
                {
                    _DicMainNum = DatapointType.GetDPTMainNumber();
                }
                return _DicMainNum;
            }
            set
            {
                _DicMainNum = value;
            }
        }

        private static Dictionary<int, string> _DicSubNum;
        public static Dictionary<int, string> DicSubNumber
        {
            get
            {
                if (null == _DicSubNum)
                {
                    _DicSubNum = DatapointType.GetDPTSubNumber();
                }
                return _DicSubNum;
            }
            set { _DicSubNum = value; }
        }

        private static List<TreeNode> _NodeTypes;
        public static List<TreeNode> NodeTypes
        {
            get
            {
                if (null == _NodeTypes)
                {
                    _NodeTypes = DatapointType.GetAllTypeNodes();
                }
                return _NodeTypes;
            }
            set
            {
                _NodeTypes = value;
            }
        }

        private static List<TreeNode> _NodeActions;
        public static List<TreeNode> NodeActions
        {
            get
            {
                if (null == _NodeActions)
                {
                    _NodeActions = DatapointType.GetAllActionNodes();
                }

                return _NodeActions;
            }
            set
            {
                _NodeActions = value;
            }
        }

        private static BindingList<EdGroupAddress> _addressTable;
        public static BindingList<EdGroupAddress> GroupAddressTable
        {
            get
            {
                if (_addressTable == null)
                {
                    _addressTable = new BindingList<EdGroupAddress>(GroupAddressStorage.Load());
                }
                return _addressTable;
            }
            set { _addressTable = value; }
        }
        public static bool AddressIsExsit(string addr)
        {
            bool isExsit = false;
            foreach (EdGroupAddress address in GroupAddressTable)
            {
                if (address.KnxAddress == addr)
                {
                    isExsit = true;
                    break;
                }
            }

            return isExsit;
        }
        public static EdGroupAddress GetGroupAddress(string Id)
        {
            if (null == Id)
            {
                return null;
            }

            EdGroupAddress addr = null;
            foreach (EdGroupAddress address in MyCache.GroupAddressTable)
            {
                if (address.Id == Id)
                {
                    addr = address;
                    break;
                }
            }

            return addr;
        }

        private static KNXVersion _projectVersion;
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

        public static void ResetVariable()
        {
            _addressTable = null;
            _projectVersion = null;
            //_datapointTypeTable = null;
        }
    }
}
