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

namespace EncryptDecryptUtility
{
    public class Program
    {

        static string passPhrase = "@C0MM$e!Un!n0R";        // can be any string
        static string saltValue = "@C0MM$e!s@1t";        // can be any string
        static string hashAlgorithm = "MD5";             // can be "SHA1"
        static int passwordIterations = 2;                  // can be any number
        static string initVector = "@1B2c3!%e5F6g7H8"; // must be 16 bytes
        static int keySize = 256;



        public static string DecryptQueryString(string encryptedText)
        {
            string key = "jdsg432387#";
            byte[] DecryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] inputByte = new byte[encryptedText.Length];
            DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByte = HttpServerUtility.UrlTokenDecode(encryptedText);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write))
                {
                    cs.Write(inputByte, 0, inputByte.Length);
                    cs.FlushFinalBlock();
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());
                }
            }

        }
        public static string EncryptQueryString(string plainText)
        {
            string key = "jdsg432387#";
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write))
                {
                    cStream.Write(inputByte, 0, inputByte.Length);
                    cStream.FlushFinalBlock();
                    return HttpServerUtility.UrlTokenEncode(mStream.ToArray());
                }
            }

            //MemoryStream mStream = new MemoryStream();
            //CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            //cStream.Write(inputByte, 0, inputByte.Length);
            //cStream.FlushFinalBlock();
            //return HttpServerUtility.UrlTokenEncode(mStream.ToArray());
        }

      
       

        public static void Main(string[] args)
        {
            int ? choice = null;
            string choice1 = null;
            string PlainText;
            string Result;
            try
            {
            start:
                Console.WriteLine("\n 1-Encrypt the text \n 2-Decrypt the text  \n 3-Exit \n Please select the any above option : ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Please select Plain text which need to be encrypted : ");
                        PlainText = Console.ReadLine();
                        Result = Program.EncryptQueryString(PlainText);
                        Console.WriteLine("Encrypted Text : {0}", Result);
                        break;

                    case 2:
                        Console.WriteLine("Please select Plain text which need to be Decrypted : ");
                        PlainText = Console.ReadLine();
                        Result = Program.DecryptQueryString(PlainText);
                        if(Result != null)
                            Console.WriteLine("Decrypted Text : {0}", Result);
                        else
                            Console.WriteLine("Enter Text is not Ciper text");
                        break;

                    case 3:
                        Console.WriteLine("Exit...Thank you");
                        break;

                    default:
                        Console.WriteLine("Enter Choice is {0} invalid.. please retry", choice);
                        goto start;
                }
                Console.WriteLine("Do you want to continue..Y / N :");
                choice1 = Console.ReadLine();
                switch(choice1)
                {
                    case "Y": goto start;

                    case "N": break;
                }

            }
            catch(Exception e)
            {
                Console.WriteLine("Exception :: " + e.ToString());
            }
            Console.ReadKey();
        }
    }
}
