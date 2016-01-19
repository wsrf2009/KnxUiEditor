using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;


namespace qycloud.component.crypto
{
    public class DES
    {
        private string fPassword;
        private byte[] fSalt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

        /// <summary>
        /// Initialize new TripleDESEncryptor.
        /// </summary>
        /// <param name="password">The password to use for encryption/decryption.</param>
        public DES(string password)
        {
            fPassword = password;
        }

        /// <summary>
        /// Initialize new TripleDESEncryptor.
        /// </summary>
        /// <param name="password">The password to use for encryption/decryption.</param>
        /// <param name="salt">Salt bytes. Bytes length must be 13.</param>
        public DES(string password, byte[] salt)
        {
            fPassword = password;
            fSalt = salt;
        }

        private byte[] iEncrypt(byte[] data, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            TripleDES alg = TripleDES.Create();

            alg.Key = key;
            alg.IV = iv;

            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(data, 0, data.Length);
            cs.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// Encrypt string with TripleDES algorith.
        /// </summary>
        /// <param name="data">String to encrypt.</param>
        /// <returns>Encrypted string.</returns>
        public string Encrypt(string data)
        {
            byte[] dataToEncrypt = System.Text.Encoding.Unicode.GetBytes(data);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(fPassword, fSalt);

            return Convert.ToBase64String(iEncrypt(dataToEncrypt, pdb.GetBytes(16), pdb.GetBytes(8)));
        }

        /// <summary>
        /// Encrypt byte array with TripleDES algorithm.
        /// </summary>
        /// <param name="data">Bytes to encrypt.</param>
        /// <returns>Encrypted bytes.</returns>
        public byte[] Encrypt(byte[] data)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(fPassword, fSalt);
            return iEncrypt(data, pdb.GetBytes(16), pdb.GetBytes(8));
        }

        private byte[] iDecrypt(byte[] data, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            TripleDES alg = TripleDES.Create();

            alg.Key = key;
            alg.IV = iv;

            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.Close();

            return ms.ToArray();
        }

        /// <summary>
        /// Decrypt string with TripleDES algorithm.
        /// </summary>
        /// <param name="data">Encrypted string.</param>
        /// <returns>Decrypted string.</returns>
        public string Decrypt(string data)
        {
            byte[] cipherBytes = Convert.FromBase64String(data);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(fPassword, fSalt);
            return System.Text.Encoding.Unicode.GetString(iDecrypt(cipherBytes, pdb.GetBytes(16), pdb.GetBytes(8)));
        }

        /// <summary>
        /// Decrypt byte array with TripleDES algorithm.
        /// </summary>
        /// <param name="data">Encrypted byte array.</param>
        /// <returns>Decrypted byte array.</returns>
        public byte[] Decrypt(byte[] data)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(fPassword, fSalt);
            return iDecrypt(data, pdb.GetBytes(16), pdb.GetBytes(8));
        }

        /// <summary>
        /// Encryption/Decryption password.
        /// </summary>
        public string Password
        {
            get { return fPassword; }
            set { fPassword = value; }
        }

        /// <summary>
        /// Salt bytes (bytes length must be 15).
        /// </summary>
        public byte[] Salt
        {
            get { return fSalt; }
            set { fSalt = value; }
        }


    }
}
