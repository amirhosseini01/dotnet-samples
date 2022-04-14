using System.IO;
using System.Security.Cryptography;

namespace Hashing
{
    public class KeyGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitSize">2048 is default</param>
        /// <param name="_publicKey"></param>
        /// <param name="_privateKey"></param>
        public static void GenerateKey_RSA(int bitSize, out RSAParameters _publicKey, out RSAParameters _privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider(bitSize))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }
        public static byte[] GenerateKey_RNG(int byteSize)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomNum = new byte[byteSize];
                rng.GetBytes(randomNum);
                return randomNum;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitSize">2048 is default</param>
        /// <param name="_publicKey"></param>
        /// <param name="_privateKey"></param>
        public static void GenerateKey_RSA_xml(int bitSize, string publicKeyPath, string privateKeyPath)
        {
            using (var rsa = new RSACryptoServiceProvider(bitSize))
            {
                rsa.PersistKeyInCsp = false;

                if (File.Exists(privateKeyPath))
                {
                    File.Delete(privateKeyPath);
                }

                if (File.Exists(publicKeyPath))
                {
                    File.Delete(publicKeyPath);
                }

                var publicKeyFolder = Path.GetDirectoryName(publicKeyPath);
                var privateKeyFolder = Path.GetDirectoryName(privateKeyPath);

                if (!Directory.Exists(publicKeyFolder))
                {
                    Directory.CreateDirectory(publicKeyFolder);
                }

                if (!Directory.Exists(privateKeyFolder))
                {
                    Directory.CreateDirectory(privateKeyFolder);
                }

                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                File.WriteAllText(privateKeyPath, rsa.ToXmlString(true));
            }
        }
    }
}
