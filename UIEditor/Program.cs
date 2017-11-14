using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UIEditor.Component;

namespace UIEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // 项目存储目录
            BuildKnxProjectFolder();
            // 项目发布目录
            BuildKnxPublishFolder();
            // 项目资源目录
            BuildKnxResourceFolder();
            // 项目临时目录
            BuildKnxCacheFolder();
            // 用户收藏目录
            BuildKnxCollectionFolder();
            // 下载存放目录
            BuildDownloadFolder();

            BuildStartupPath();

            InitialVariable();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentUICulture = /*Thread.CurrentThread.CurrentCulture*/new CultureInfo("en-US");
            try
            {
            Application.Run(new FrmMain());
=======

            try
            {
                Process process = DLLHelp.RuningInstance();
                if (process == null)
                {
                    if (args.Length > 0)
                    {
                        Application.Run(new FrmMain(args[0]));
                    }
                    else
                    {
                        Application.Run(new FrmMain(""));
                    }
                }
                else
                {
                    DLLHelp.HandleRunningInstance(process);
                }
>>>>>>> SationKNXUIEditor-Modify
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Application.ApplicationExit += Application_ApplicationExit;
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            // 删除临时目录
            if (Directory.Exists(MyCache.DefaultKnxCacheFolder) == true)
            {
                FileHelper.DeleteFolder(MyCache.DefaultKnxCacheFolder);
            }
        }

        // 
        private const string KnxUiEditorRoot = "KnxUiEditor";
        private static void BuildKnxProjectFolder()
        {
            MyCache.DefaultKnxProjectFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), KnxUiEditorRoot), "Projects");

            if (Directory.Exists(MyCache.DefaultKnxProjectFolder) == false)
            {
                Directory.CreateDirectory(MyCache.DefaultKnxProjectFolder);
            }
        }

        private static void BuildKnxPublishFolder()
        {
            MyCache.DefaultKnxPublishFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), KnxUiEditorRoot), "Publish");

            if (Directory.Exists(MyCache.DefaultKnxPublishFolder) == false)
            {
                Directory.CreateDirectory(MyCache.DefaultKnxPublishFolder);
            }
        }

        private static void BuildKnxResourceFolder()
        {
            MyCache.DefaultKnxResurceFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), KnxUiEditorRoot), "Resources");

            if (Directory.Exists(MyCache.DefaultKnxResurceFolder) == false)
            {
                Directory.CreateDirectory(MyCache.DefaultKnxResurceFolder);
            }
        }

        private static void BuildKnxCacheFolder()
        {
            MyCache.DefaultKnxCacheFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), KnxUiEditorRoot), ".cache");

            var folder = new DirectoryInfo(MyCache.DefaultKnxCacheFolder);

            if (folder.Exists)
            {
                FileHelper.DeleteFolder(MyCache.DefaultKnxCacheFolder);
            }
            else
            {
                folder.Create();
                folder.Attributes = FileAttributes.Hidden;
            }
        }

        private static void BuildKnxCollectionFolder()
        {
            MyCache.DefatultKnxCollectionFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), KnxUiEditorRoot), "Collections");
            var folder = new DirectoryInfo(MyCache.DefatultKnxCollectionFolder);
            if (!folder.Exists)
            {
                folder.Create();
            }
        }

        private static string downloadFolderGuid = "{374DE290-123F-4565-9164-39C4925E467B}";
        [DllImport("Shell32.dll")]
        private static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)]Guid rfid, uint dwFlags, IntPtr hToken,
            out IntPtr ppszPath);
        private static void BuildDownloadFolder()
        {
            IntPtr outPath;
            int result = SHGetKnownFolderPath(new Guid(downloadFolderGuid), 0x00004000, new IntPtr(0), out outPath);
            if (result >= 0)
            {
                MyCache.DefaultDownloadFolder = Marshal.PtrToStringUni(outPath);
            }
            else
            {
                MyCache.DefaultDownloadFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), KnxUiEditorRoot), "Downloads");
                var folder = new DirectoryInfo(MyCache.DefaultDownloadFolder);
                if (!folder.Exists)
                {
                    folder.Create();
                }
            }
        }

        private static void BuildStartupPath()
        {
            MyCache.ProjectStartupDir = System.Threading.Thread.GetDomain().BaseDirectory;
            MyCache.ProjectResourceDir = Path.Combine(MyCache.ProjectStartupDir, "Resources");
            MyCache.ProjectResImgDir = Path.Combine(MyCache.ProjectResourceDir, "Images");
            MyCache.ProjectResCtrlDir = Path.Combine(MyCache.ProjectResourceDir, "controls");
        }

        private static void InitialVariable()
        {
            //MyCache.DicPageNodes = new Dictionary<int, Entity.PageNode>();
            //MyCache.DicNoderResImg = new Dictionary<int, string>();
            //MyCache.ValidResImg = new Dictionary<int, string>();
            MyCache.ValidResImgNames = new List<string>();
            //MyCache.Images = new Dictionary<string, Bitmap>();
        }
    }
}
