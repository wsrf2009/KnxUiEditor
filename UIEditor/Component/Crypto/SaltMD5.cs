using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qycloud.component.crypto
{
    public class SaltMD5
    {
        public static string GetRandomString()
        {
            //Random String Generator for Salt
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, 32).Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }

        public static string GetSaltMD5(string input)
        {
            string hash = "";
            string salt = GetRandomString();
            string source = input + salt;

            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, source);
            }

            // Return the hexadecimal string. 
            return hash + ":" + salt; ;
        }


        public static bool SaltMD5Check(string saltMD5, string password)
        {
            var mypass = saltMD5.Split(':');
            string MD5Text = mypass[0];
            string saltText = mypass[1];

            string passSalt = password + saltText;
            string hash;
            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, passSalt);
            }

            if (MD5Text == hash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string GetMd5Hash(System.Security.Cryptography.MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
