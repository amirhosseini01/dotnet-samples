using System;
using System.Security.Cryptography;

namespace Hashing
{
    public class RSAWithRSAParameterKey
    {
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;
        public RSAWithRSAParameterKey(RSAParameters publicKey, RSAParameters privateKey)
        {
            _publicKey = publicKey;
            _privateKey = privateKey;
        }

        public static void DemoCode()
        {
            KeyGenerator.GenerateKey_RSA(1024, out var _publicKey, out var _privateKey);

            string text = "hello world";

            RSAWithRSAParameterKey rsa = new(_publicKey, _privateKey);

            var encryptedByte = rsa.EncryptData(text.GetBytes(), 2048);
            string encrypted = encryptedByte.ToBase64();
            Console.WriteLine("encrypted: " + encrypted);
            Console.WriteLine(" --- ");
            Console.WriteLine("decrypted: " + rsa.DecryptData(encrypted.FromBase64(), 2048).GetString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <param name="bitSize">2048 default</param>
        /// <returns></returns>
        public byte[] EncryptData(byte[] dataToEncrypt, int bitSize)
        {
            byte[] cipherBytes;

            using (var rsa = new RSACryptoServiceProvider(bitSize))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_publicKey);

                cipherBytes = rsa.Encrypt(dataToEncrypt, true);
            }

            return cipherBytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <param name="bitSize">2048 default</param>
        /// <returns></returns>
        public byte[] DecryptData(byte[] dataToDecrypt, int bitSize)
        {
            byte[] plain;

            using (var rsa = new RSACryptoServiceProvider(bitSize))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);

                plain = rsa.Decrypt(dataToDecrypt, true);
            }

            return plain;
        }
    }
}
