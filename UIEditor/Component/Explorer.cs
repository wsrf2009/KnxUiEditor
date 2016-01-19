using System.Diagnostics;

namespace UIEditor.Component
{
    public static class Explorer
    {
        private const string ExplorerName = "Explorer";

        public static void SelectFile(string filePath)
        {

            Process.Start(ExplorerName, "/select," + filePath);

        }

        public static void OpenFolder(string path)
        {
            Process.Start(ExplorerName, path);
        }

        public static void OpenIE(string url)
        {
            var startInfo = new ProcessStartInfo("IExplore.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            startInfo.Arguments = url;
            Debug.WriteLine(url);
            Process.Start(startInfo);
        }
    }
}