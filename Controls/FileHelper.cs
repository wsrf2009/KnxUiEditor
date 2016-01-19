using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;


namespace UIEditor.Controls
{
    public static class FileHelper
    {

        /// <summary>
        /// 转换文档大小显示
        /// </summary>
        /// <param name="fileSizeKB"></param>
        /// <returns></returns>
        public static String FormatFileSize(Int64 fileSizeKB)
        {
            string fileSize = "";

            if (fileSizeKB < 0)
            {
                throw new ArgumentOutOfRangeException("fileSize");
            }
            else if (fileSizeKB >= 1024 * 1024 * 1024)
            {
                fileSize = string.Format("{0:########0.00} Tb", ((Double)fileSizeKB) / (1024 * 1024 * 1024));
            }
            else if (fileSizeKB >= 1024 * 1024)
            {
                fileSize = string.Format("{0:####0.00} Gb", ((Double)fileSizeKB) / (1024 * 1024));
            }
            else if (fileSizeKB >= 1024)
            {
                fileSize = string.Format("{0:####0.00} Mb", ((Double)fileSizeKB) / 1024);
            }
            else
            {
                fileSize = string.Format("{0} Kb", fileSizeKB);
            }

            if (fileSize == "0 Kb")
            {
                fileSize = "1 Kb";	// min.							
            }

            return fileSize;
        }



        /// <summary>
        /// 目录拷贝
        /// </summary>
        /// <param name="srcFolder"></param>
        /// <param name="tarFolder"></param>
        /// <param name="overwrite"></param>
        public static void CopyFolder(string srcFolder, string targetFolder, bool overwrite = false)
        {
            Directory.CreateDirectory(targetFolder);

            // 复制子目录
            foreach (string dir in Directory.GetDirectories(srcFolder))
            {
                CopyFolder(dir, Path.Combine(targetFolder, Path.GetFileName(dir)), overwrite);
            }

            // 复制自文件
            foreach (string file in Directory.GetFiles(srcFolder))
            {
                string dstFile = Path.Combine(targetFolder, Path.GetFileName(file));
                File.Copy(file, dstFile, overwrite);
            }
        }

        /// <summary>
        /// 删除文件夹及其子
        /// </summary>
        /// <param name="folderName"></param>
        public static void DeleteFolder(string folderName)
        {
            // 可靠删除，先重命名文件夹，然后在后台删除
            var deleteFolder = folderName + DateTime.Now.Ticks;

            if (Directory.Exists(folderName))
            {
                Directory.Move(folderName, deleteFolder);
                Directory.Delete(deleteFolder, true);
            }
        }

        /// <summary>
        /// 删除空的父文件夹
        /// </summary>
        /// <param name="folderName"></param>
        public static void DeleteEmptyParentFolder(string folderName)
        {
            if (Directory.GetDirectories(folderName).Length == 0)
            {
                string parentFolder = Directory.GetParent(folderName).FullName;
                if (Directory.Exists(folderName))
                {
                    Directory.Delete(folderName);
                }

                DeleteEmptyParentFolder(parentFolder);
            }
        }


        /// <summary>
        /// 计算文件的MD5码
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5(string fileName)
        {
            string strResult = "";
            string strHashData = "";

            byte[] arrbytHashValue;

            FileStream oFileStream = null;

            MD5CryptoServiceProvider oMD5Hasher = new MD5CryptoServiceProvider();

            using (oFileStream = new FileStream(fileName.Replace("\"", ""), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //计算指定Stream 对象的哈希值
                arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream);
            }

            //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            strHashData = BitConverter.ToString(arrbytHashValue);

            //替换-
            strHashData = strHashData.Replace("-", "");

            strResult = strHashData;

            return strResult;
        }
    }
}
