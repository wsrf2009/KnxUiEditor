using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


namespace qycloud.component.crypto
{
    public class MD5
    {
        public MD5()
        {
        }

        public static string GetMD5(byte[] data)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(data));
        }

        public static string GetMD5(string data)
        {
            return GetMD5(ASCIIEncoding.Default.GetBytes(data));
        }
    }
}
