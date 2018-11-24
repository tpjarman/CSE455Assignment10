using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLoigc
{
    public static class SecurityManager
    {
        /// <summary>
        /// Encrpytion algorithim using CryptoStreams and the ICryptoTransfrom library
        /// </summary>
        /// <param name="text">text to encrypt</param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            //set up the encryption
            byte[] seed = ASCIIEncoding.ASCII.GetBytes("98934726");
            SymmetricAlgorithm symmetricAlgorithm = DES.Create();
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor(seed, seed);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);

            //encrpyt the text and return the result to the caller
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(text);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            string encryptedText = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            return encryptedText;
        }

        /// <summary>
        /// Decrypt an encrypted string using CryptoStreams and the ICryptoTransfrom library
        /// </summary>
        /// <param name="textToDecrypt">The text to decrypt</param>
        /// <returns></returns>
        public static string Decrypt(string textToDecrypt)
        {
            //set up the decryption
            byte[] seed = ASCIIEncoding.ASCII.GetBytes("98934726");
            SymmetricAlgorithm symmetricAlgorithm = DES.Create();
            byte[] textAsBytes = Convert.FromBase64String(textToDecrypt);
            MemoryStream memoryStream = new MemoryStream(textAsBytes);
            ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor(seed, seed);

            //decrypt the text and return the result to the caller
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            string result = reader.ReadLine();
            return result;

        }
    }
}
