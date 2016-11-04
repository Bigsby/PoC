using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encrypting
{
    public static class Encryptor
    {
        private const string saltValue = "This is the salt";
        private const string passPhrase = "This is the pass phrase";
        private const string hashAlgorithm = "SHA1";
        private const int passwordIterations = 3;
        private const int keySize = 256;
        private static readonly byte[] initVectorBytes = new byte[] {
            0x3d, 0x84, 0x30, 0x85, 0x10, 0x34, 0x9f, 0xd3, 0xbb, 0xfb, 0x27, 0xaa, 0xe9, 0x58, 0xd3, 0xe0
        };

        public static string Encrypt(string plainText)
        {
            var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            var password = new PasswordDeriveBytes
            (
                passPhrase,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations
            );

            var keyBytes = password.GetBytes(keySize / 8);

            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            var encryptor = symmetricKey.CreateEncryptor
            (
                keyBytes,
                initVectorBytes
            );

            var memoryStream = new MemoryStream();

            var cryptoStream = new CryptoStream
            (
                memoryStream,
                encryptor,
                CryptoStreamMode.Write
            );

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            cryptoStream.FlushFinalBlock();

            var cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            var cipherText = Convert.ToBase64String(cipherTextBytes);

            return cipherText;
        }

        public static string Decrypt(string cipherText)
        {
            var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            var cipherTextBytes = Convert.FromBase64String(cipherText);

            var password = new PasswordDeriveBytes
            (
                passPhrase,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations
            );

            var keyBytes = password.GetBytes(keySize / 8);
            var symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            var decryptor = symmetricKey.CreateDecryptor
            (
                keyBytes,
                initVectorBytes
            );

            var memoryStream = new MemoryStream(cipherTextBytes);

            var cryptoStream = new CryptoStream
            (
                memoryStream,
                decryptor,
                CryptoStreamMode.Read
            );

            var plainTextBytes = new byte[cipherTextBytes.Length];

            var decryptedByteCount = cryptoStream.Read
            (
                plainTextBytes,
                0,
                plainTextBytes.Length
            );

            memoryStream.Close();
            cryptoStream.Close();

            string plainText = Encoding.UTF8.GetString
            (
                plainTextBytes,
                0,
                decryptedByteCount
            );

            return plainText;
        }


        private static string HashText(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var sha = new SHA512CryptoServiceProvider();
            var hash = sha.ComputeHash(textBytes);
            return Convert.ToBase64String(hash);
        }
    }

    public static class AsymmetricEncryptor
    {
        static public byte[] RSAEncrypt(byte[] byteEncrypt, RSAParameters RSAInfo, bool isOAEP)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (var RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAInfo);

                    //Encrypt the passed byte array and specify OAEP padding.
                    encryptedData = RSA.Encrypt(byteEncrypt, isOAEP);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        static public byte[] RSADecrypt(byte[] byteDecrypt, RSAParameters RSAInfo, bool isOAEP)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAInfo);

                    //Decrypt the passed byte array and specify OAEP padding.
                    decryptedData = RSA.Decrypt(byteDecrypt, isOAEP);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}
