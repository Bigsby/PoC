using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Aesing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
            this.InitializeComponent();
            go.Click += (s, e) =>
            {
                {
                    var dataString = "host\r\nid\r\nmac\r\n";
                    //var dataBytes = Encoding.UTF8.GetBytes(dataString);

                    var encryptor = new Encryptor();

                    var encryptedBytes = encryptor.Encrypt(dataString);

                    var encrtyptedString = BitConverter.ToString(encryptedBytes).Replace("-", string.Empty);

                    var toDecryptBytes = StringToByteArray(encrtyptedString);

                    var decryptedBytes = encryptor.Decrypt(toDecryptBytes);

                    var decryptedString = Encoding.UTF8.GetString(decryptedBytes);
                    //var dataBytes = CryptographicBuffer.ConvertStringToBinary(dataString, BinaryStringEncoding.Utf8);
                    //var sap = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);
                    //var key = sap.CreateSymmetricKey(keyMaterial.AsBuffer());
                    //var encryptedBuffer = CryptographicEngine.Encrypt(key, dataBytes, IV.AsBuffer());


                    //var encryptedString = Encoding.UTF8.GetString(encryptedBuffer.ToArray());

                }

            };

 
        }

        static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }

    class Encryptor
    {
        private readonly byte[] keyMaterial = Convert.FromBase64String("P9KcPLf+q21f9DUnI0cyP1xgALRa8+uKfZXiNIcjphM=");
        private readonly byte[] IV = Convert.FromBase64String("lEvSlTN0Q7gu9sAUvPTySQ==");
        private IBuffer m_iv = null;
        private CryptographicKey m_key;

        public Encryptor()
        {
            m_iv = IV.AsBuffer();
            SymmetricKeyAlgorithmProvider provider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);
            m_key = provider.CreateSymmetricKey(keyMaterial.AsBuffer());
        }

        public byte[] Encrypt(string data)
        {
            var bufferMsg = CryptographicBuffer.ConvertStringToBinary(data, BinaryStringEncoding.Utf8);
            var bufferEncrypt = CryptographicEngine.Encrypt(m_key, bufferMsg, m_iv);
            return bufferEncrypt.ToArray();
        }

        public byte[] Decrypt(byte[] input)
        {
            var bufferDecrypt = CryptographicEngine.Decrypt(m_key, input.AsBuffer(), m_iv);
            return bufferDecrypt.ToArray();
        }
    }
}
