using System.IO;
using System.Security.Cryptography;

namespace Hashing
{
    public class DES
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <param name="key">8 byte random key</param>
        /// <param name="iv">8 byte random key</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var desc = new DESCryptoServiceProvider())
            {
                desc.Mode = CipherMode.CBC;
                desc.Padding = PaddingMode.PKCS7;

                desc.Key = key;
                desc.IV = iv;

                using (var memory = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memory, desc.CreateEncryptor(),
                        CryptoStreamMode.Write);

                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    return memory.ToArray();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <param name="key">8 byte random key</param>
        /// <param name="iv">8 byte random key</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var desc = new DESCryptoServiceProvider())
            {
                desc.Mode = CipherMode.CBC;
                desc.Padding = PaddingMode.PKCS7;

                desc.Key = key;
                desc.IV = iv;

                using (var memory = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memory, desc.CreateDecryptor(),
                        CryptoStreamMode.Write);

                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    return memory.ToArray();
                }
            }
        }
    }
}
