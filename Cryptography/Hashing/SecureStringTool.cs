using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Hashing
{
    public static class SecureStringTool
    {
        public static void DemoCode()
        {
            var str = ToSecureString(new[] { '5', '1', '2', '5' });

            char[] charArr = CharacterData(str);

            string unsecure = ConvertToUnSecureString(str);
        }
        public static SecureString ToSecureString(this char[] str)
        {
            SecureString secureString = new();

            Array.ForEach(str, secureString.AppendChar);

            return secureString;
        }

        public static char[] CharacterData(SecureString secureString)
        {
            char[] bytes;
            var ptr = IntPtr.Zero;

            try
            {
                // Allocate a BSTR and Copy contents of Secure String into it
                ptr = Marshal.SecureStringToBSTR(secureString);
                bytes = new char[secureString.Length];

                // Copy data from unmanaged memory into a managed char array
                Marshal.Copy(ptr, bytes, 0, secureString.Length);
            }
            finally
            {

                if (ptr != IntPtr.Zero)
                {
                    // Free unManaged Memory
                    Marshal.ZeroFreeBSTR(ptr);
                }
            }

            return bytes;
        }

        /// <summary>
        /// Dot Recomended
        /// </summary>
        /// <param name="secureString"></param>
        /// <returns></returns>
        public static string ConvertToUnSecureString(SecureString secureString)
        {
            if (secureString == null)
                throw new ArgumentNullException("secureString");

            var unManagedStr = IntPtr.Zero;


            try
            {
                // Copy the contents of the secureString to unmanaged memory
                unManagedStr = Marshal.SecureStringToGlobalAllocUnicode(secureString);

                //Alocate a managed string and copy the contents of the unmanaged
                // string data into it
                return Marshal.PtrToStringUni(unManagedStr);
            }
            finally
            {
                // Free the unmanaged string pointer
                Marshal.ZeroFreeGlobalAllocUnicode(unManagedStr);
            }
        }
    }
}
