using System.Security.Cryptography;

namespace Hashing
{
    public class HashData
    {
        public static byte[] ComputeHashSha1(byte[] toBeHashed)
        {
            using (var sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha1(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA1(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha256(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha512(byte[] toBeHashed)
        {
            using (var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashSha512(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA512(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashMD5(byte[] toBeHashed)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(toBeHashed);
            }
        }
        public static byte[] ComputeHashShaMD5(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACMD5(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }
    }
}
