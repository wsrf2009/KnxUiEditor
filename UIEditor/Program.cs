using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using UIEditor.Component;
using UIEditor.Controls;

namespace UIEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 项目存储目录
            BuildKnxProjectFolder();
            // 项目发布目录
            BuildKnxPublishFolder();
            // 项目资源目录
            BuildKnxResourceFolder();
            // 项目临时目录
            BuildKnxCacheFolder();

            BuildStartupPath();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentUICulture = /*Thread.CurrentThread.CurrentCulture*/new CultureInfo("en-US");
            
            Application.Run(new FrmMain());

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

            folder.Create();
            folder.Attributes = FileAttributes.Hidden;

        }

        private static void BuildStartupPath()
        {
            MyCache.ProjectStartupDir = System.Threading.Thread.GetDomain().BaseDirectory;
            MyCache.ProjectResourceDir = Path.Combine(MyCache.ProjectStartupDir, "Resources");
            MyCache.ProjectResImgDir = Path.Combine(MyCache.ProjectResourceDir, "Images");
            MyCache.ProjectResCtrlDir = Path.Combine(MyCache.ProjectResourceDir, "controls");
        }
    }
}
