using System;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

namespace Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Passwprd:");
            var pass = ReadLine();
            WriteLine("Hash:");
            WriteLine(HashPassword(pass));
            WriteLine("Hash again:");
            WriteLine(HashPassword(pass));
            ReadLine();
        }

        public static string HashPassword(string password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}
