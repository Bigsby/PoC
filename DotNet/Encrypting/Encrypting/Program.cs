using System;
using System.Security.Cryptography;
using static System.Console;
using static System.Text.Encoding;

namespace Encrypting
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSymetric();

            WriteLine("Done!");
            ReadLine();
        }

        private static void TestAsymmetric()
        {
            var text = "This is my text";
            WriteLine("Original text: " + text);
            var textBytes = UTF8.GetBytes(text);

            var hash1 = HashText(text);
            WriteLine("H1: " + hash1);

            var hash2 = HashText(text);
            WriteLine("H2: " + hash2);

        }

        private static string HashText(string text)
        {
            var textBytes = UTF8.GetBytes(text);
            
            var sha = new SHA512CryptoServiceProvider();
            var hash = sha.ComputeHash(textBytes);
            return System.Convert.ToBase64String(hash);
        }

        private static void TestSymetric()
        {
            var plainText = "Hello, World!";    // original plaintext

            WriteLine(String.Format("Plaintext : {0}", plainText));

            var cipherText = Encryptor.Encrypt(plainText);

            WriteLine(String.Format("Encrypted : {0}", cipherText));

            plainText = Encryptor.Decrypt(cipherText);

            WriteLine(String.Format("Decrypted : {0}", plainText));
        }
    }
}
