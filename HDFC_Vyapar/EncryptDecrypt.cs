using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace HDFC_SFTP_FileProcessor
{
    public class EncryptDecrypt
    {
        #region For New Encryption

        public static string EncryptQueryString(string plainText)
        {
            // string key = "sdoidsoiew2#dg$lyeqc@12344jklh";
            string key = "@jds7%37#4Ew@12a";
            byte[] Key = ASCIIEncoding.ASCII.GetBytes(key.PadLeft(32));
            byte[] IV = ASCIIEncoding.ASCII.GetBytes(key.PadLeft(16));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.KeySize = 256;
                aesAlg.BlockSize = 128;
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        return HttpServerUtility.UrlTokenEncode(msEncrypt.ToArray());
                    }
                }
            }
        }

        public static string DecryptQueryString(string cipherText)
        {
            string key = "@jds7%37#4Ew@12a";
            byte[] Key = ASCIIEncoding.ASCII.GetBytes(key.PadLeft(32));
            byte[] IV = ASCIIEncoding.ASCII.GetBytes(key.PadLeft(16));
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            byte[] inputByte = new byte[cipherText.Length];
            inputByte = HttpServerUtility.UrlTokenDecode(cipherText);
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.KeySize = 256;
                aesAlg.BlockSize = 128;
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                // Create the streams used for decryption.
                //using (MemoryStream msDecrypt = new MemoryStream(Convert.ToInt32(inputByte)))
                using (MemoryStream msDecrypt = new MemoryStream())
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV), CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(inputByte, 0, inputByte.Length);
                        csDecrypt.FlushFinalBlock();
                        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                        return encoding.GetString(msDecrypt.ToArray());
                    }
                }
            }
        }


        #endregion For New Encryption

    }
}
