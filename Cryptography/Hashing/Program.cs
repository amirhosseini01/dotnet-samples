using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            SecureStringTool.DemoCode();
        }
        private static void test()
        {
            Thread thread1 = new Thread(new ThreadStart(PBKDF2For));
            thread1.Start();

            Thread thread2 = new Thread(new ThreadStart(PBKDF2For));
            thread2.Start();
        }

        private static void PBKDF2For()
        {
            var sw = new Stopwatch();
            sw.Start();

            string pass = "hello";
            //for (int i = 0; i <= 6; i++)
            //{
            PBKDF2(pass, 100);
            //PBKDF2(pass, 1_000);
            //PBKDF2(pass, 10_000);
            //PBKDF2(pass, 50_000);
            //PBKDF2(pass, 100_000);
            //PBKDF2(pass, 200_000);
            //PBKDF2(pass, 500_000);
            sw.Stop();

            Console.WriteLine($"all : {sw.ElapsedMilliseconds} ms");

        }

        private static string hash(string str)
        {
            byte[] myStrBytes = TextTools.GetBytes(str);

            byte[] hashed = HashData.ComputeHashSha256(myStrBytes);
            Console.WriteLine(TextTools.ToBase64(hashed));

            Console.WriteLine("--------");

            var key = KeyGenerator.GenerateKey_RNG(32);

            var withKey = HashData.ComputeHashSha512(myStrBytes, key);

            return TextTools.ToBase64(withKey);
        }

        private static void hashPass(string pass)
        {
            Console.WriteLine("please enter your Password to hash");

            byte[] salt = PasswordHashing.GenerateSalt(32);
            byte[] passByte = TextTools.GetBytes(pass);

            byte[] hashed = PasswordHashing.HashPassword(passByte, salt);

            Console.WriteLine(TextTools.ToBase64(hashed));

            Console.WriteLine("--------");
        }

        private static void PBKDF2(string pass, int numberOfRounds)
        {
            var sw = new Stopwatch();
            sw.Start();
            //Console.WriteLine("please enter your Password to hash");

            byte[] salt = PasswordHashing.GenerateSalt(32);
            byte[] passByte = TextTools.GetBytes(pass);

            byte[] hashed = PasswordHashing.HashPassword(passByte, salt, numberOfRounds);

            string strHashed = TextTools.ToBase64(hashed);

            //Console.WriteLine(strHashed);

            //Console.WriteLine(numberOfRounds);

            sw.Stop();

            Console.WriteLine($"time elapse for {numberOfRounds} is : {sw.ElapsedMilliseconds} ms");
        }
    }
}
