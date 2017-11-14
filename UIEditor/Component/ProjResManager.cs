using GroupAddress;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace UIEditor.Component
{
    public class ProjResManager
    {
        #region 图片
        public static string CopyImage(string srcPath)
        {
            return FileHelper.CopyFile(srcPath, MyCache.ProjImgPath);
        }

        public static string CopyImageRename(string srcPath)
        {
            return FileHelper.CopyFileRename(srcPath, MyCache.ProjImgPath);
        }

        public static string CopyImageSole(string srcPath)
        {
            return FileHelper.CopyFileSole(srcPath, MyCache.ProjImgPath);
        }
        #endregion

        #region 组地址
        //public static bool AddressIsExsit(string addr)
        //{
        //    bool isExsit = false;
        //    foreach (var address in MyCache.GroupAddressTable)
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
        #endregion
    }
}
