using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using KNX.DatapointType;
using Structure;
using UIEditor.Component;
using GroupAddress;
using UIEditor;
using UIEditor.Entity;

namespace UIEditor.Component
{
    public static class MyCache
    {
        /// <summary>
        /// 编辑工程时的工作目录
        /// </summary>
        public static string WorkFolder { get; set; }
        /// <summary>
        /// 项目目录，存放当前编辑的项目的所有文件
        /// </summary>
        public static string ProjectFolder { get; set; }
        /// <summary>
        /// 项目资源存放目录
        /// </summary>
        public static string ProjResfolder { get; set; }
        /// <summary>
        /// 工程资源图片存放目录
        /// </summary>
        public static string ProjImgPath { get; set; }
        /// <summary>
        /// 项目的临时目录
        /// </summary>
        public static string ProjTempFolder { get; set; }
        /// <summary>
        /// 项目图片临时存放目录
        /// </summary>
        public static string ProjTempImgFolder { get; set; }
        /// <summary>
        /// 项目导入导出模板、收藏的临时工作目录
        /// </summary>
        public static string ProjTempCollFolder { get; set; }

        /// <summary>
        /// 项目默认的存储目录
        /// </summary>
        public static string DefaultKnxProjectFolder { get; set; }
        /// <summary>
        /// 项目资源目录
        /// </summary>
        public static string DefaultKnxResurceFolder { get; set; }
        /// <summary>
        /// 项目发布目录
        /// </summary>
        public static string DefaultKnxPublishFolder { get; set; }
        /// <summary>
        /// 项目资源目录
        /// </summary>
        public static string DefaultKnxCacheFolder { get; set; }
        /// <summary>
        /// 模板目录
        /// </summary>
        public static string DefaultDownloadFolder { get; set; }
        /// <summary>
        /// 收藏目录
        /// </summary>
        public static string DefatultKnxCollectionFolder { get; set; }

        /// <summary>
        /// 当前运行的程序所在目录
        /// </summary>
        public static string ProjectStartupDir { get; set; }

        /// <summary>
        /// 程序资源文件夹Resource所在的目录
        /// </summary>
        public static string ProjectResourceDir { get; set; }

        /// <summary>
        ///程序 图片文件夹所在的目录
        /// </summary>
        public static string ProjectResImgDir { get; set; }

        /// <summary>
        /// 程序控件图标所在目录
        /// </summary>
        public static string ProjectResCtrlDir { get; set; }

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

        //private static Dictionary<string, Image> _DicImageList;
        //public static Dictionary<string, Image> DicImageList
        //{
        //    get
        //    {
        //        if (null == _DicImageList)
        //        {
        //            _DicImageList = new Dictionary<string,Image>();
        //        }
        //        return _DicImageList;
        //    }
        //}

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

        private static List<EdGroupAddress> _GroupAddressTable;
        public static List<EdGroupAddress> GroupAddressTable
        {
            get
            {
                if (_GroupAddressTable == null)
                {
                    _GroupAddressTable = new List<EdGroupAddress>();
                }
                return _GroupAddressTable;
                //return (from i in _addressTable orderby KNXAddressHelper.StringToAddress(i.KnxAddress) ascending, i.KnxAddress select i).ToList();
            }
            set { _GroupAddressTable = value; }
        }
        //public static bool AddressIsExsit(string addr)
        //{
        //    bool isExsit = false;
        //    foreach (EdGroupAddress address in GroupAddressTable)
        //    {
        //        if (address.KnxAddress == addr)
        //        {
        //            isExsit = true;
        //            break;
        //        }
        //    }

        //    return isExsit;
        //}
        //public static EdGroupAddress GetGroupAddress(string Id)
        //{
        //    if (null == Id)
        //    {
        //        return null;
        //    }

        //    EdGroupAddress addr = null;
        //    foreach (EdGroupAddress address in MyCache.GroupAddressTable)
        //    {
        //        if (address.Id == Id)
        //        {
        //            addr = address;
        //            break;
        //        }
        //    }

        //    return addr;
        //}

        //private static KNXVersion _projectVersion;
        //public static KNXVersion ProjectVersion
        //{
        //    get
        //    {
        //        if (_projectVersion == null)
        //        {
        //            _projectVersion = VersionStorage.Load();
        //        }
        //        return _projectVersion;
        //    }
        //    set { _projectVersion = value; }
        //}
        //public static void ResetVariable()
        //{
        //    //_addressTable = null;
        //    //_projectVersion = null;
        //    //_datapointTypeTable = null;
        //}

        //public static KNXVersion TemplateVersion { get; set; }

        /// <summary>
        /// 当前正在导入的工程/模板文件的版本
        /// </summary>
        public static KNXVersion VersionOfImportedFile { get; set; }

        /// <summary>
        /// 工作中的工程大纲
        /// </summary>
        public static AppNode Project { get; set; }
        /// <summary>
        /// 存储工作状态页面Id和页面配对的词典
        /// </summary>
        //public static Dictionary<int, PageNode> DicPageNodes { get; set; }
        /// <summary>
        /// 工作状态的Node Id和Node图片目录配对的词典
        /// </summary>
        public static Dictionary<int, string> DicNoderResImg { get; set; }
        /// <summary>
        /// 需要导出保存压缩的Node的图片目录
        /// </summary>
        public static Dictionary<int, string> ValidResImg { get; set; }

        public static List<string> ValidResImgNames { get; set; }

        /// <summary>
        /// 在视图编辑界面是否显示标尺
        /// </summary>
        public static bool DisplayRuler { get; set; }

        ///// <summary>
        ///// 当前工程的页面宽度
        ///// </summary>
        //public static int Width { get; set; }
        ///// <summary>
        ///// 当前工程的页面高度
        ///// </summary>
        //public static int Height { get; set; }

        /// <summary>
        /// 当前工程的页面大小
        /// </summary>
        public static Size AppSize { get; set; }

        /// <summary>
        /// 程序内部剪切板
        /// </summary>
        public static object ClipBoard { get; set; }

        /// <summary>
        /// 收藏文件夹中的模板
        /// </summary>
        public static List<Template> Templates { get; set; }

        //public static Dictionary<string, Bitmap> Images { get; set; }
    }
}
