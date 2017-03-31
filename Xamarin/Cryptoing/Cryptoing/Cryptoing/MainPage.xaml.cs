using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PCLCrypto;

namespace Cryptoing
{
    public partial class MainPage : ContentPage
    {
        byte[] keyMaterial = System.Convert.FromBase64String("P9KcPLf+q21f9DUnI0cyP1xgALRa8+uKfZXiNIcjphM=");
        byte[] IV = System.Convert.FromBase64String("lEvSlTN0Q7gu9sAUvPTySQ==");
        string data = "I'm data";

        public MainPage()
        {
            

            InitializeComponent();
            theButton.Clicked += (s, e) => {
                var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
                var key = provider.CreateSymmetricKey(keyMaterial);
                var encryptBytes = WinRTCrypto.CryptographicEngine.Encrypt(key, Encoding.UTF8.GetBytes(data), IV);

                var decryptBytes = WinRTCrypto.CryptographicEngine.Decrypt(key, encryptBytes, IV);
                var decryptData = Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);

                var success = decryptData == data;


                DisplayAlert("Result", success ? "Suuccess" : "Failure", "OK");

            };
        }
    }
}
