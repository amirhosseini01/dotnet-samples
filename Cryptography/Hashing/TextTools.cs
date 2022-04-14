using System;
using System.Text;

namespace Hashing
{
    public static class TextTools
    {
        public static byte[] GetBytes(this string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }
        public static string GetString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        public static byte[] FromBase64(this string base64)
        {
            return Convert.FromBase64String(base64);
        }
    }
}
