using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;


namespace qycloud.component.crypto
{
    public enum AESBits
    {
        BITS128,
        BITS192,
        BITS256
    };

    public class AES
    {
        private string fPassword;
        private AESBits fEncryptionBits;
        private byte[] fSalt = new byte[] { 0x00, 0x01, 0x02, 0x1C, 0x1D, 0x1E, 0x03, 0x04, 0x05, 0x0F, 0x20, 0x21, 0xAD, 0xAF, 0xA4 };

        /// <summary>
        /// Initialize new AESEncryptor.
        /// </summary>
        /// <param name="password">The password to use for encryption/decryption.</param>
        /// <param name="encryptionBits">Encryption bits (128,192,256).</param>
        public AES(string password, AESBits encryptionBits)
        {
            fPassword = password;
            fEncryptionBits = encryptionBits;
        }

        /// <summary>
        /// Initialize new AESEncryptor.
        /// </summary>
        /// <param name="password">The password to use for encryption/decryption.</param>
        /// <param name="encryptionBits">Encryption bits (128,192,256).</param>
        /// <param name="salt">Salt bytes. Bytes length must be 15.</param>
        public AES(string password, AESBits encryptionBits, byte[] salt)
        {
            fPassword = password;
            fEncryptionBits = encryptionBits;
            fSalt = salt;
        }

        private byte[] InternalEncrypt(byte[] data, byte[] key, byte[] iV)
        {
            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();
            alg.Key = key;

            alg.IV = iV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(data, 0, data.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();

            return encryptedData;
        }

        /// <summary>
        /// Encrypt string with AES algorith.
        /// </summary>
        /// <param name="data">String to encrypt.</param>
        public string Encrypt(string data)
        {
            if (data == null)
            {
                return null;
            }

            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(data);

            return Convert.ToBase64String(Encrypt(clearBytes));
        }

        /// <summary>
        /// Encrypt byte array with AES algorithm.
        /// </summary>
        /// <param name="data">Bytes to encrypt.</param>
        public byte[] Encrypt(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            byte[] result = null;

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(fPassword, fSalt);

            switch (fEncryptionBits)
            {
                case AESBits.BITS128:
                    result = InternalEncrypt(data, pdb.GetBytes(16), pdb.GetBytes(16));
                    break;
                case AESBits.BITS192:
                    result = InternalEncrypt(data, pdb.GetBytes(24), pdb.GetBytes(16));
                    break;
                case AESBits.BITS256:
                    result = InternalEncrypt(data, pdb.GetBytes(32), pdb.GetBytes(16));
                    break;
            }

            return result;
        }

        private byte[] InternalDecrypt(byte[] data, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }


        /// <summary>
        /// Decrypt string with AES algorithm.
        /// </summary>
        /// <param name="data">Encrypted string.</param>
        public string Decrypt(string data)
        {
            if (data == null)
            {
                return null;
            }

            byte[] dataToDecrypt = Convert.FromBase64String(data);
            return System.Text.Encoding.Unicode.GetString(Decrypt(dataToDecrypt));
        }

        /// <summary>
        /// Decrypt byte array with AES algorithm.
        /// </summary>
        /// <param name="data">Encrypted byte array.</param>
        public byte[] Decrypt(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            byte[] result = null;

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(fPassword, fSalt);

            switch (fEncryptionBits)
            {
                case AESBits.BITS128:
                    result = InternalDecrypt(data, pdb.GetBytes(16), pdb.GetBytes(16));
                    break;
                case AESBits.BITS192:
                    result = InternalDecrypt(data, pdb.GetBytes(24), pdb.GetBytes(16));
                    break;
                case AESBits.BITS256:
                    result = InternalDecrypt(data, pdb.GetBytes(32), pdb.GetBytes(16));
                    break;
            }

            return result;
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
        /// Encryption/Decryption bits.
        /// </summary>
        public AESBits EncryptionBits
        {
            get { return fEncryptionBits; }
            set { fEncryptionBits = value; }
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
