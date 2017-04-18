using System.IO;
using System.Security.Cryptography;

namespace OneView.Utility.Security
{

    public class ClientIdentity {
        public string HostName { get; set; }
        public string MachineID { get; set; }
        public string MACAddress { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ClientEncryption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static byte[] EncryptIdentity(ClientIdentity ci)
        {
            byte[] key = System.Convert.FromBase64String("P9KcPLf+q21f9DUnI0cyP1xgALRa8+uKfZXiNIcjphM=");
            byte[] IV = System.Convert.FromBase64String("lEvSlTN0Q7gu9sAUvPTySQ==");

            byte[] encrypted;

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.WriteLine(ci.HostName);
                            swEncrypt.WriteLine(ci.MachineID);
                            swEncrypt.WriteLine(ci.MACAddress);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static ClientIdentity DecryptIdentity(byte[] identity)
        {
            byte[] key = System.Convert.FromBase64String("P9KcPLf+q21f9DUnI0cyP1xgALRa8+uKfZXiNIcjphM=");
            byte[] IV = System.Convert.FromBase64String("lEvSlTN0Q7gu9sAUvPTySQ==");

            var ci = new ClientIdentity();

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = IV;

                // Create inboxMsg decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(identity))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            ci.HostName = srDecrypt.ReadLine();
                            ci.MachineID = srDecrypt.ReadLine();
                            ci.MACAddress = srDecrypt.ReadLine();
                        }
                    }
                }

            }
            return ci;
        }

        public static byte[] EncryptString(string str, byte[] IV)
        {
            byte[] key = System.Convert.FromBase64String("P9KcPLf+q21f9DUnI0cyP1xgALRa8+uKfZXiNIcjphM=");

            byte[] encrypted;

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(str);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public static string DecryptString(byte[] identity, byte[] IV)
        {
            byte[] key = System.Convert.FromBase64String("P9KcPLf+q21f9DUnI0cyP1xgALRa8+uKfZXiNIcjphM=");

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = key;
                aesAlg.IV = IV;

                // Create inboxMsg decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(identity))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
        }

    }
}
