using System;
using System.Text;
using System.Security.Cryptography;

namespace CMCC
{
    class AesTool
    {
        public static string Encrypt(string toEncrypt)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("cmcc=why+ljw+syx");
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            Aes kgen = Aes.Create("AES");
            kgen.Mode = CipherMode.ECB;
            kgen.Key = keyArray;
            ICryptoTransform cTransform = kgen.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string toDecrypt)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("cmcc=why+ljw+syx");
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}