using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Adform.Todo.Essentials
{
    public class CryptoHandler
    {
        public CryptoHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public readonly IConfiguration _configuration;

        /// <summary>
        /// Method to Encrypt string using MD5
        /// </summary>
        /// <param name="toEncrypt">String to encrypt</param>
        /// <returns></returns>
        public string Encrypt(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            string key = _configuration.GetValue<string>("Secret_Key");

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            var tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            tripleDESCryptoServiceProvider.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Method to Encrypt string using MD5
        /// </summary>
        /// <param name="toDecrypt">String to encrypt</param>
        /// <returns></returns>
        public string Decrypt(string toDecrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            string key = _configuration.GetValue<string>("Secret_Key");
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            var tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            tripleDESCryptoServiceProvider.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
