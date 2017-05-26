using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace StPierre.helpers
{
    public static class Encryption
    {
        private static string _encryptionKey = ",v!VUVW~W{s0qm|8X4<h6cZw!Z?~26";
        /// <summary>
        /// Encrypts a given string
        /// </summary>
        /// <param name="clearText">The string to be encrypted</param>
        /// <returns></returns>
        private static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x82, 0xdd, 0x65, 0xF4, 0x26, 0x95, 0x84, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Decrypts a given string
        /// </summary>
        /// <param name="cipherText">The encrypted string to be decoded</param>
        /// <returns></returns>
        private static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptionKey, new byte[] { 0x21, 0x12, 0x66, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// <summary>
        /// Get an encrypted object from a given filename
        /// If the file doesn't exist, returns null
        /// </summary>
        /// <param name="filename">The filename in which the object is saved</param>
        /// <returns>The object encrypted into a file</returns>
        public static T GetEncryptedObjectFromFile<T>(string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            T obj = default(T);
            FileStream stream = null;
            try
            {
                stream = new FileStream(filename,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                obj = (T)formatter.Deserialize(stream);
            } catch (Exception ex) { } finally
            {
                stream?.Close();
            }
            return obj;
        }

        /// <summary>
        /// Encrypts a given object into a file
        /// </summary>
        /// <param name="objectToEncrypt">The object to encrypt</param>
        /// <param name="filename">The filename the object will have</param>
        /// <returns>Success status</returns>
        public static bool EncryptObjectInFile<T>(T objectToEncrypt, string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename,
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            stream.Seek(0,SeekOrigin.Begin);
            bool success = true;
            try {
                formatter.Serialize(stream, objectToEncrypt);
            } catch(Exception ex) {
                success = false;
            } finally {
                stream.Seek(0,SeekOrigin.Begin);
                stream.Close();
            }
            return success;
        }

    }
}
