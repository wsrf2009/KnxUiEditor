using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace qycloud.component.crypto
{
    public enum HashType
    {
        NONE,
        MD5,
        SHA1,
        SHA256,
        SHA512
    }

    enum CompareStatus
    {
        clear,
        different,
        same
    }

    /// <summary>
    /// All of our hashing tools, wrapers, etc.
    /// </summary>
    public class HashTools
    {
        public HashTools()
        {
            InError = false;
        }

        #region Methods

        private bool FileCheck(string file)
        {
            Filename = file;
            return FileCheck();
        }

        private bool FileCheck()
        {
            FileInfo fi = new FileInfo(Filename);

            if (!fi.Exists)
            {
                string f2h;
                if (string.IsNullOrEmpty(Filename))
                {
                    f2h = "(Empty)";
                }
                else
                {
                    f2h = Filename;
                }

                ErrorMsg = string.Format("File Doesn't Exist, {0}", f2h);
                InError = true;

                return false;
            }

            InError = false;

            FullPath = fi.Directory.FullName;

            return true;
        }

        private bool CreateMD5FileHash()
        {
            try
            {
                using (FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Read))
                {
                    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                    {
                        ByteHash = md5.ComputeHash(fs);
                        Hash = BitConverter.ToString(ByteHash).Replace("-", "");
                    }

                    fs.Close();
                }
            }
            catch (Exception e)
            {
                ErrorMsg = e.ToString();
                return false;
            }
            return true;

        }

        private bool CreateSHA1FileHash()
        {
            try
            {
                using (FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Read))
                {
                    using (SHA1Managed sha1 = new SHA1Managed())
                    {
                        ByteHash = sha1.ComputeHash(fs);
                        Hash = BitConverter.ToString(ByteHash).Replace("-", "");
                    }
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                ErrorMsg = e.ToString();
                InError = true;
                return false;
            }
            return true;
        }

        private bool CreateSHA256FileHash()
        {
            try
            {
                using (FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Read))
                {
                    using (SHA256Managed sha256 = new SHA256Managed())
                    {
                        ByteHash = sha256.ComputeHash(fs);
                        Hash = BitConverter.ToString(ByteHash).Replace("-", "");
                    }
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                ErrorMsg = e.ToString();
                InError = true;
                return false;
            }
            return true;
        }

        private bool CreateSHA512FileHash()
        {
            try
            {
                using (FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Read))
                {
                    using (SHA512Managed sha512 = new SHA512Managed())
                    {
                        ByteHash = sha512.ComputeHash(fs);
                        Hash = BitConverter.ToString(ByteHash).Replace("-", "");
                    }
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                ErrorMsg = e.ToString();
                InError = true;
                return false;
            }
            return true;
        }

        #endregion

        #region Methods (public)

        /// <summary>
        /// Public method to hash files.  
        /// </summary>
        /// <returns>Result Boolean</returns>
        public bool CreateFileHash(string file, HashType type)
        {
            // verify file exists and write it to property;
            if (!FileCheck(file))
            {
                return false;
            }

            return CreateFileHash(type);
        }

        /// <summary>
        /// Public method to hash files.
        /// </summary>
        /// <returns>Result Boolean</returns>
        public bool CreateFileHash(HashType type)
        {
            // checking file, don't like it may be done twice :(
            if (!FileCheck())
            {
                return false;
            }

            bool rv = false;
            switch (type)
            {
                case HashType.MD5:
                    rv = CreateMD5FileHash();
                    break;
                case HashType.SHA1:
                    rv = CreateSHA1FileHash();
                    break;
                case HashType.SHA256:
                    rv = CreateSHA256FileHash();
                    break;
                case HashType.SHA512:
                    rv = CreateSHA512FileHash();
                    break;
                default:
                    rv = false;
                    break;
            }

            return rv;
        }

        /// <summary>
        /// Search for text type file with like name of the hashed file.  If file is found
        /// check it for a hash string.  Often publisher hashes are stored in a text file with the
        /// same name as the binary, iso, etc.
        /// Uses FileToHash property
        /// </summary>
        /// <returns>Result if file was found</returns>
        public bool FindTextHashFile(HashType ht)
        {
            TextFileFound = string.Empty;
            TextFileHashFound = string.Empty;

            if (!FileCheck())
            {
                return false;
            }

            FileInfo fi = new FileInfo(Filename);
            string[] files = Directory.GetFiles(fi.DirectoryName, Path.GetFileNameWithoutExtension(fi.FullName) + "*");

            if (files.Length < 1)
            {
                return false;
            }

            for (int i = 0; i < files.Length; i++)
            {
                if (Filename != files[i])
                {
                    TextFileHashFound = FileTools.FindHashInFile(files[i], ht);
                    if (TextFileHashFound.Length > 0)
                    {
                        TextFileFound = files[i];
                        i = files.Length + 1;
                    }
                }
            }

            return TextFileHashFound.Length > 0;
        }

        /// <summary>
        /// Create a hash value from provided text
        /// </summary>
        /// <param name="text">Text to hash</param>
        /// <param name="ht">Hash method to use</param>
        /// <returns>result</returns>
        public bool CreateTextHash(string text, HashType type)
        {
            bool rv = true;
            try
            {
                byte[] byteText = Encoding.Default.GetBytes(text);
                switch (type)
                {
                    case HashType.MD5:
                        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                        ByteHash = md5.ComputeHash(byteText);
                        break;
                    case HashType.SHA1:
                        SHA1Managed sha1 = new SHA1Managed();
                        ByteHash = sha1.ComputeHash(byteText);
                        break;
                    case HashType.SHA256:
                        SHA256Managed sha256 = new SHA256Managed();
                        ByteHash = sha256.ComputeHash(byteText);
                        break;
                    case HashType.SHA512:
                        SHA512Managed sha512 = new SHA512Managed();
                        ByteHash = sha512.ComputeHash(byteText);
                        break;
                    default:
                        rv = false;
                        break;
                }

                if (rv)
                {
                    Hash = BitConverter.ToString(ByteHash).Replace("-", "");
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
                InError = true;
                return false;
            }
            return rv;
        }

        #endregion

        #region Properties

        public bool InError { get; private set; }
        public string ErrorMsg { get; private set; }
        public string Filename { get; set; }
        public string Hash { get; private set; }
        public byte[] ByteHash { get; private set; }
        public string TextFileFound { get; private set; }
        public string TextFileHashFound { get; private set; }
        public string FullPath { get; private set; }

        #endregion
    }
}
