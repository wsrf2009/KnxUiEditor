using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UIEditor.Component
{
    public class DLLHelp
    {
        #region 常量
        private const int SW_SHOWNOMAL = 1;
        #endregion

        #region User32.dll
        /// <summary>
        /// 获取系统当前活动窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("User32.DLL")]
        static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// 获取指定窗体的标题
        /// </summary>
        /// <param name="WinHandle">窗体句柄</param>
        /// <param name="Title">缓冲区取用于存储标题</param>
        /// <param name="size">缓冲区大小</param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        static extern int GetWindowText(IntPtr WinHandle, StringBuilder Title, int size);

        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        #region 单实例运行
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL);//显示
            SetForegroundWindow(instance.MainWindowHandle);//当到最前端
        }
        public static Process RuningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in Processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
        #endregion

        #region 窗体是否活跃
        public static bool ApplicationIsActive(IntPtr AppPtr)
        {
            IntPtr ptr = GetForegroundWindow();
            if (AppPtr == ptr)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
