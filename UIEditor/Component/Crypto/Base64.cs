using System;
using System.Text;
using System.Security.Cryptography;

namespace HU8.Helpers.Base
{
    /// <summary>
    /// 常用的加密方法
    /// </summary>
    /// <author>Chiron</author>
    public static class Base64
    {
        #region Base64
        /// <summary>
        /// Base64 编码
        /// </summary>
        /// <param name="source">源数据</param>
        /// <returns></returns>
        public static string Base64Encode(string source)
        {
            if (source == null) throw new ArgumentNullException("source");
            var bytes = Encoding.UTF8.GetBytes(source);
            try
            {
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return source;
            }

        }

        /// <summary>
        /// Base64 解码
        /// </summary>
        /// <param name="source">源数据</param>
        /// <returns></returns>
        public static string Base64Decode(string source)
        {
            if (source == null) throw new ArgumentNullException("source");
            var t = source.Length % 4;
            if (t != 0) source += new string('=', 4 - t);
            var bytes = Convert.FromBase64String(source);
            try
            {
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return source;
            }
        }
        #endregion Base64
    }
}
