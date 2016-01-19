using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace UIEditor
{
    public static class ZipHelper
    {
        #region 文件压缩
        /// <summary>
        /// 目录压缩
        /// </summary>
        /// <param name="directoryToZip"></param>
        /// <param name="zipFileToCreate"></param>
        public static void ZipDir(string directoryToZip, string zipFileToCreate)
        {
            using (ZipFile zip = new ZipFile(Encoding.Default))
            {
                zip.Comment = "This zip was knxui package created by program at " + System.DateTime.Now.ToString("G");
                // recurses subdirectories
                zip.AddDirectory(directoryToZip);
                zip.Save(zipFileToCreate);
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="zipFile"></param>
        /// <param name="unzipToDir"></param>
        public static void UnZipDir(string zipFile, string unzipToDir)
        {
            using (ZipFile zip = ZipFile.Read(zipFile, new ReadOptions() { Encoding = Encoding.Default }))
            {
                zip.ExtractAll(unzipToDir);
            }
        }

        public static void ZipDir(string directoryToZip, string zipFileToCreate, string mykey)
        {
            using (ZipFile zip = new ZipFile(Encoding.Default))
            {
                zip.Comment = "This zip was knxui package created by Peter Guo at " + System.DateTime.Now.ToString("G");
                zip.Password = mykey;
                // recurses subdirectories
                zip.AddDirectory(directoryToZip);
                zip.Save(zipFileToCreate);
            }
        }

        public static void UnZipDir(string zipFile, string unzipToDir, string mykey)
        {
            using (ZipFile zip = ZipFile.Read(zipFile, new ReadOptions() { Encoding = Encoding.Default }))
            {
                zip.Password = mykey;
                zip.ExtractAll(unzipToDir);
            }
        }
        #endregion
    }
}
