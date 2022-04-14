﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    public class HybridSignitureEncryption
    {
        public static void DemoCode()
        {
            string text = "Hello World!";
            KeyGenerator.GenerateKey_RSA(1024, out var _publicKey, out var _privateKey);
            var rsa = new RSAWithRSAParameterKey(_publicKey, _privateKey);
            var E_res = EncryptData(text.GetBytes(), rsa, _privateKey);
            var D_res = DecryptData(E_res, rsa, _publicKey);

            Console.WriteLine("Encrypted: " + E_res.EncryptedData.ToBase64());
            Console.WriteLine("iv: " + E_res.Iv.ToBase64());
            Console.WriteLine("hmac: " + E_res.Hmac.ToBase64());
            Console.WriteLine("key: " + E_res.EncryptedSessionKey.ToBase64());
            Console.WriteLine("sign: " + E_res.Signiture.ToBase64());
            Console.WriteLine("--------");
            Console.WriteLine("decrypted: " + D_res.GetString());
        }
        public static SignitureIntegrityEncryptedPacket EncryptData(byte[] original, RSAWithRSAParameterKey rsa, RSAParameters privateKey)
        {
            var sessionKey = KeyGenerator.GenerateKey_RNG(32);

            var packet = new SignitureIntegrityEncryptedPacket() { Iv = KeyGenerator.GenerateKey_RNG(16) };

            AES aes = new();
            packet.EncryptedData = aes.Encrypt(original, sessionKey, packet.Iv);

            packet.EncryptedSessionKey = rsa.EncryptData(sessionKey, 2048);

            packet.Hmac = HashData.ComputeHashSha256(packet.EncryptedData, sessionKey);

            packet.Signiture = DigitalSignature.SignData(packet.Hmac, 2048, privateKey);

            return packet;
        }
        public static byte[] DecryptData(SignitureIntegrityEncryptedPacket packet, RSAWithRSAParameterKey rsa, RSAParameters publicKey)
        {
            var sessionKey = rsa.DecryptData(packet.EncryptedSessionKey, 2048);

            var hmacToCheck = HashData.ComputeHashSha256(packet.EncryptedData, sessionKey);

            if (!Compare(packet.Hmac, hmacToCheck))
                throw new CryptographicException("HMAC doesnt match");

            if (!DigitalSignature.VerifySignature(packet.Hmac, packet.Signiture, 2048, publicKey))
                throw new CryptographicException("This is not valid signiture");

            AES aes = new();
            var decrypted = aes.Decrypt(packet.EncryptedData, sessionKey, packet.Iv);

            return decrypted;
        }

        private static bool Compare(byte[] array1, byte[] array2)
        {
            bool res = array1.Length == array2.Length;

            for (int i = 0; i < array1.Length && i < array2.Length; ++i)
            {
                res &= array1[i] == array2[i];
            }

            return res;
        }
    }
    public class SignitureIntegrityEncryptedPacket
    {
        public byte[] EncryptedSessionKey { get; set; }
        public byte[] EncryptedData { get; set; }
        public byte[] Iv { get; set; }
        public byte[] Hmac { get; set; }
        public byte[] Signiture { get; set; }
    }
}
