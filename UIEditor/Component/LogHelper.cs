using System;
using System.Text;

namespace UIEditor.Component
{
    /// <summary>
    /// 日志助理
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static string Format(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine(ex.ToString());
            sb.AppendLine(new string('-', 20));

            return sb.ToString();
        }
    }
}
