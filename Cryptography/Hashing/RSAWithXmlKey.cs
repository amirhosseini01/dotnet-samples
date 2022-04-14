using System;
using System.IO;
using System.Security.Cryptography;

namespace Hashing
{
    public class RSAWithXmlKey
    {
        public static void DemoCode()
        {
            string text = "hello world";

            string publicPath = "C:\\Users\\Amir\\Desktop\\xml\\publickey.xml";
            string privatePath = "C:\\Users\\Amir\\Desktop\\xml\\privatekey.xml";

            KeyGenerator.GenerateKey_RSA_xml(2048, publicPath, privatePath);

            RSAWithXmlKey rsa = new();

            var encryptedByte = rsa.EncryptData(text.GetBytes(), 2048, publicPath);
            string encrypted = encryptedByte.ToBase64();

            var decrypted = rsa.DecryptData(encrypted.FromBase64(), 2048, privatePath);

            Console.WriteLine(encrypted);
            Console.WriteLine(decrypted.GetString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <param name="bitSize">2048 default</param>
        /// <returns></returns>
        public byte[] EncryptData(byte[] dataToEncrypt, int bitSize, string publicKeyPath)
        {
            byte[] cipherBytes;

            using (var rsa = new RSACryptoServiceProvider(bitSize))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));

                cipherBytes = rsa.Encrypt(dataToEncrypt, false);
            }

            return cipherBytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <param name="bitSize">2048 default</param>
        /// <returns></returns>
        public byte[] DecryptData(byte[] dataToDecrypt, int bitSize, string privateKeyPath)
        {
            byte[] plain;

            using (var rsa = new RSACryptoServiceProvider(bitSize))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));

                plain = rsa.Decrypt(dataToDecrypt, false);
            }

            return plain;
        }
    }
}
