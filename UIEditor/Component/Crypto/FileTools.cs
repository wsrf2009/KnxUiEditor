using System.IO;
using System.Text.RegularExpressions;


namespace qycloud.component.crypto
{
    public static class FileTools
    {
        static int HashCompareFileMaxSize = 1048576;
        static string MD5RegEx = "\b[0-9a-fA-F]{32}\b";
        static string SHA1RegEx = "\b[0-9a-fA-F]{40}\b";
        static string SHA256RegEx = "\b[0-9a-fA-F]{64}\b";
        static string SHA512RegEx = "\b[0-9a-fA-F]{128}\b";

        public static string MatchMD5String(string file)
        {
            int fileMax = 1048576;

            FileInfo fi = new FileInfo(file);
            if (fileMax < fi.Length)
            {
                return string.Empty;
            }

            using (StreamReader sr = fi.OpenText())
            {
                //string s = sr.ReadToEnd();
                return Regex.Match(sr.ReadToEnd(), MD5RegEx).Value;

            }
        }

        public static string FindHashInFile(string file, HashType ht)
        {
            int fileMax = HashCompareFileMaxSize;
            string HashRegEx = string.Empty;
            switch (ht)
            {
                case HashType.MD5:
                    HashRegEx = MD5RegEx;
                    break;
                case HashType.SHA1:
                    HashRegEx = SHA1RegEx;
                    break;
                case HashType.SHA256:
                    HashRegEx = SHA256RegEx;
                    break;
                case HashType.SHA512:
                    HashRegEx = SHA512RegEx;
                    break;
            }
            FileInfo fi = new FileInfo(file);
            if (fileMax < fi.Length)
                return string.Empty;
            using (StreamReader sr = fi.OpenText())
            {
                return Regex.Match(sr.ReadToEnd(), HashRegEx, RegexOptions.IgnoreCase).Value.Trim();
            }
        }
    }

    /* Example of grabing a chunk of a file (from bdzipper)
     * Don't forget your try/catch wrappers
     *
        using (StreamReader sr = new StreamReader(curdir + filename))
        {
            char[] stub = new char[charLength];
            sr.Read(stub, 0, charLength);
            Response.Write("<p>Reading from: " + filename + "</p>");
            for (int i = 0; i < charLength && '\0' != stub[i]; i++)
            {
                Response.Write(Server.HtmlEncode(stub[i].ToString()));
            }
        }
    */
}
