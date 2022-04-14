using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    public class HybridEncryption
    {
        public static void DemoCode()
        {
            string text = "Hello World!";
            KeyGenerator.GenerateKey_RSA(1024, out var _publicKey, out var _privateKey);
            var rsa = new RSAWithRSAParameterKey(_publicKey, _privateKey);
            var E_res = EncryptData(text.GetBytes(), rsa);
            var D_res = DecryptData(E_res, rsa);

            Console.WriteLine("Encrypted: " + E_res.EncryptedData.ToBase64());
            Console.WriteLine("iv: " + E_res.Iv.ToBase64());
            Console.WriteLine("key: " + E_res.EncryptedSessionKey.ToBase64());
            Console.WriteLine("--------");
            Console.WriteLine("decrypted: " + D_res.GetString());
        }
        public static EncryptedPacket EncryptData(byte[] original, RSAWithRSAParameterKey rsa)
        {
            var sessionKey = KeyGenerator.GenerateKey_RNG(32);

            var packet = new EncryptedPacket() { Iv = KeyGenerator.GenerateKey_RNG(16) };

            AES aes = new();
            packet.EncryptedData = aes.Encrypt(original, sessionKey, packet.Iv);

            packet.EncryptedSessionKey = rsa.EncryptData(sessionKey, 2048);

            return packet;
        }
        public static byte[] DecryptData(EncryptedPacket packet, RSAWithRSAParameterKey rsa)
        {
            var sessionKey = rsa.DecryptData(packet.EncryptedSessionKey, 2048);



            AES aes = new();
            var decrypted = aes.Decrypt(packet.EncryptedData, sessionKey, packet.Iv);


            return decrypted;
        }
    }
    public class EncryptedPacket
    {
        public byte[] EncryptedSessionKey { get; set; }
        public byte[] EncryptedData { get; set; }
        public byte[] Iv { get; set; }
    }
}
