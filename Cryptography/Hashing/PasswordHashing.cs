using System;
using System.Security.Cryptography;

namespace Hashing
{
    public class PasswordHashing
    {
        public static byte[] GenerateSalt(int keySize)
        {
            return KeyGenerator.GenerateKey_RNG(keySize);
        }

        private static byte[] Combine(byte[] password, byte[] salt)
        {
            byte[] ret = new byte[password.Length + salt.Length]; ;
            Buffer.BlockCopy(password, 0, ret, 0, password.Length);
            Buffer.BlockCopy(salt, 0, ret, password.Length, salt.Length);

            return ret;
        }
        public static byte[] HashPassword(byte[] password, byte[] salt)
        {
            return HashData.ComputeHashSha256(
                Combine(password, salt)
                );
        }
        public static byte[] HashPassword(byte[] password, byte[] salt, int rounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(password, salt, rounds))
            {
                return rfc2898.GetBytes(32);
            }
        }
    }
}
