using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using Utils;

namespace UIEditor.Component
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
        /// 将源目录整个拷贝到目标目录下
        /// </summary>
        /// <param name="srcFolder"></param>
        /// <param name="tarFolder"></param>
        /// <param name="overwrite"></param>
        public static void CopyFolder(string srcFolder, string desFolder, bool overwrite = false)
        {
            try
            {
                DirectoryInfo f = new DirectoryInfo(desFolder);
                if (!f.Exists)
                {
                    f.Create();
                }

                DirectoryInfo f1 = new DirectoryInfo(srcFolder);
                DirectoryInfo f2 = new DirectoryInfo(Path.Combine(desFolder, f1.Name));
                if (!f1.Exists)
                {
                    Console.WriteLine("源文件夹 " + srcFolder + " 不存在");
                    return;
                }

                if (!f2.Exists)
                {
                    //Console.WriteLine("目标文件夹 " + desFolder + " 已存在");
                    f2.Create();
                }

                // 复制子目录
                foreach (string dir in Directory.GetDirectories(srcFolder))
                {
                    CopyFolder(dir, f2.FullName, overwrite);
                }

                // 复制子文件
                foreach (string file in Directory.GetFiles(srcFolder))
                {
                    string dstFile = Path.Combine(f2.FullName, Path.GetFileName(file));
                    File.Copy(file, dstFile, overwrite);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 将源目录下的所有内用拷贝到目标目录下
        /// </summary>
        /// <param name="srcFolder"></param>
        /// <param name="desFolder"></param>
        /// <param name="overWrite"></param>
        public static void CopyFolderContent(string srcFolder, string desFolder, bool overWrite = false)
        {
            try
            {
                DirectoryInfo f1 = new DirectoryInfo(srcFolder);
                DirectoryInfo f2 = new DirectoryInfo(desFolder);
                if (!f1.Exists)
                {
                    Console.WriteLine("源文件夹 " + srcFolder + " 不存在");
                    return;
                }

                if (!f2.Exists)
                {
                    //Console.WriteLine("目标文件夹 " + desFolder + " 已存在");
                    f2.Create();
                }

                // 复制子目录
                foreach (string dir in Directory.GetDirectories(srcFolder))
                {
                    CopyFolder(dir, f2.FullName, overWrite);
                }

                // 复制子文件
                foreach (string file in Directory.GetFiles(srcFolder))
                {
                    string dstFile = Path.Combine(f2.FullName, Path.GetFileName(file));
                    File.Copy(file, dstFile, overWrite);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                try
                {
                    Directory.Move(folderName, deleteFolder);
                    Directory.Delete(deleteFolder, true);
                }
                catch (Exception)
                {
                    MessageBox.Show(folderName + UIResMang.GetString("Message40"));
                }
            }
        }

        public static void MoveFolder(string srcFolder, string desFolder)
        {
            try
            {
                DirectoryInfo f1 = new DirectoryInfo(srcFolder);
                DirectoryInfo f2 = new DirectoryInfo(Path.Combine(desFolder, f1.Name));
                if (!f1.Exists)
                {
                    Console.WriteLine("源文件夹 " + srcFolder + " 不存在");
                    return;
                }

                if (f2.Exists)
                {
                    Console.WriteLine("目标文件夹 " + desFolder + " 已存在");
                    f2.Delete(true);
                }

                f1.MoveTo(f2.FullName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void MoveFolderContent(string srcFolder, string desFolder)
        {
            try
            {
                DirectoryInfo f1 = new DirectoryInfo(srcFolder);
                DirectoryInfo f2 = new DirectoryInfo(desFolder);
                if (!f1.Exists)
                {
                    Console.WriteLine("源文件夹 " + srcFolder + " 不存在");
                    return;
                }

                if (!f2.Exists)
                {
                    Directory.CreateDirectory(desFolder);
                }

                // 复制子目录
                foreach (string dir in Directory.GetDirectories(srcFolder))
                {
                    MoveFolder(dir, desFolder);
                }

                // 复制自文件
                foreach (string file in Directory.GetFiles(srcFolder))
                {
                    string dstFile = Path.Combine(desFolder, Path.GetFileName(file));
                    File.Move(file, dstFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
        public static string MD5(string fileName)
        {
            string strResult = "";
            string strHashData = "";

            byte[] arrbytHashValue;

            FileStream oFileStream = null;

            MD5CryptoServiceProvider oMd5Hasher = new MD5CryptoServiceProvider();

            using (oFileStream = new FileStream(fileName.Replace("\"", ""), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //计算指定Stream 对象的哈希值
                arrbytHashValue = oMd5Hasher.ComputeHash(oFileStream);
            }

            //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            strHashData = BitConverter.ToString(arrbytHashValue);

            //替换-
            strHashData = strHashData.Replace("-", "");

            strResult = strHashData;

            return strResult;
        }

        /// <summary>
        /// 将文件 srcPath（全路径）拷贝到目录desDir中，目标目录有同名文件则取消复制
        /// </summary>
        /// <param name="srcPath">需要拷贝的文件</param>
        /// <param name="desDir">拷贝的目标目录</param>
        /// <returns>拷贝后的文件名</returns>
        public static string CopyFile(string srcPath, string desDir)
        {
            if (!File.Exists(srcPath) || !(new DirectoryInfo(desDir).Exists))
            {
                return null;
            }

            string name = Path.GetFileName(srcPath);
            string desFile = Path.Combine(desDir, name);
            if (File.Exists(desFile))
            {
                return name;
            }
            else
            {
                File.Copy(srcPath, desFile, false);
                return name;
            }
        }

        /// <summary>
        /// 将文件 srcPath（全路径）拷贝到目录desDir中,目标目录没有同名文件则直接拷贝
        /// 否则以格式 文件名(数字).文件格式 的方式拷贝到目标目录
        /// </summary>
        /// <param name="srcPath">需要拷贝的源文件，全路径</param>
        /// <param name="desDir">拷贝的目标目录</param>
        /// <returns>拷贝后目标目录下的文件名</returns>
        public static string CopyFileRename(string srcPath, string desDir)
        {
            if (!File.Exists(srcPath) || !(new DirectoryInfo(desDir).Exists))
            {
                return null;
            }

            string name = Path.GetFileName(srcPath);
            string nwe = Path.GetFileNameWithoutExtension(srcPath); // 文件名，不包含后缀
            string ext = Path.GetExtension(srcPath); // 文件后缀
            string desFile = Path.Combine(desDir, name);
            int i = 1;
            do
            {
                desFile = Path.Combine(desDir, name);
                if (!File.Exists(desFile))
                {
                    File.Copy(srcPath, desFile, false);
                    break;
                }
                
                name = nwe + "(" + (i++) + ")" + ext;
            } while (true);

            return name;
        }

        /// <summary>
        /// 将文件 stcPath 以唯一的方式拷贝到目标目录 desDir。不能有相同的文件和相同的文件名
        /// </summary>
        /// <param name="srcPath">需要拷贝的源文件，全路径</param>
        /// <param name="desDir">拷贝的目标目录</param>
        /// <returns>拷贝后的文件名</returns>
        public static string CopyFileSole(string srcPath, string desDir)
        {
            if (!File.Exists(srcPath) || !(new DirectoryInfo(desDir).Exists))
            {
                return null;
            }

            string fileName = null;
            bool hasSameFile = false;
            foreach (string file in Directory.GetFiles(desDir))
            {
                bool isSame = ImageHelper.IsSameImage(srcPath, file);
                if (isSame)
                {
                    fileName = Path.GetFileName(file);
                    hasSameFile = true;
                    break;
                }
            }

            if (!hasSameFile)
            {
                fileName = FileHelper.CopyFileRename(srcPath, desDir);
            }

            return fileName;
        }
    }
}
