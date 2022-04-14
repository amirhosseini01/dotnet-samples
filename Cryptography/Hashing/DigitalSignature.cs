using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    public class DigitalSignature
    {
        public static void DemoCode()
        {
            KeyGenerator.GenerateKey_RSA(1024, out var _publicKey, out var _privateKey);

            string text = "hello world!";

            var hashedData = HashData.ComputeHashSha256(text.GetBytes());

            Console.WriteLine("hashedData: " + hashedData.ToBase64());

            var signedData = SignData(hashedData, 2048, _privateKey);

            Console.WriteLine("signedData: " + signedData.ToBase64());

            var VerifySignatureRes = VerifySignature(hashedData, signedData, 2048, _publicKey);
            Console.WriteLine("VerifySignature Result: " + (VerifySignatureRes ? " vrifyed" : " dont trust this code"));


        }
        /// <summary>
        ///  default key size is 2048
        /// </summary>
        /// <param name="hashOfDataToSign"></param>
        /// <param name="keySize"></param>
        /// <param name="_privateKey"></param>
        /// <returns></returns>
        public static byte[] SignData(byte[] hashOfDataToSign, int keySize, RSAParameters _privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);

                var rsaFormater = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormater.SetHashAlgorithm("SHA256");

                return rsaFormater.CreateSignature(hashOfDataToSign);
            }
        }
        /// <summary>
        /// default key size is 2048
        /// </summary>
        /// <param name="hashOfDataToSign"></param>
        /// <param name="signature"></param>
        /// <param name="keySize"></param>
        /// <param name="_publicKey"></param>
        /// <returns></returns>
        public static bool VerifySignature(byte[] hashOfDataToSign, byte[] signature, int keySize, RSAParameters _publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                rsa.ImportParameters(_publicKey);

                var rsaDeFormater = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeFormater.SetHashAlgorithm("SHA256");

                return rsaDeFormater.VerifySignature(hashOfDataToSign, signature);
            }
        }
    }
}
