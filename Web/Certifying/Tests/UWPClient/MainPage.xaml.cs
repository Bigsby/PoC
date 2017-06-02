using Windows.Security.Cryptography.Certificates;
using Windows.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using System.Collections.Generic;
using Windows.Storage;
using CrossClient;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            theButton.Click += async (s, e) =>
            {
                var response = await HttpInvoker.Invoke("api/deviceregistration?identity=asdfasdf").ConfigureAwait(false);
                // var request = await CreteRequest().ConfigureAwait(false);

                ////var response = CrossClient.HttpInvoker.Invoke();
                //var certificate = await GetClientCertificate();
                ////////await VerifyCertificateKeyAccess(certificate);

                //var filter = new HttpBaseProtocolFilter();
                //filter.AllowUI = true;
                //filter.ClientCertificate = certificate;

                ////foreach (var error in Enum.GetValues(typeof(ChainValidationResult)))
                ////    try
                ////    {
                ////        filter.IgnorableServerCertificateErrors.Add((ChainValidationResult)error);
                ////    }
                ////    catch
                ////    {
                ////    }
                //////{
                //////    var chainError = (ChainValidationResult)error;
                //////    if (chainError != ChainValidationResult.Success && chainError != ChainValidationResult.Revoked)
                //////        filter.IgnorableServerCertificateErrors.Add((ChainValidationResult)error);
                //////}

                //////var certStream = typeof(MainPage).GetTypeInfo().Assembly.GetManifestResourceStream("UWPClient.BigsbyClientCert.cer");

                //////filter.ClientCertificate = new Certificate(certStream.AsOutputStream());
                //var client = new Windows.Web.Http.HttpClient(filter);
                //var response = await client.GetStringAsync(new Uri("https://localhost:9000/api/deviceregistration?identity=asdfasdf"));



                var stop = "here";
            };

        }

        private async Task<string> CreteRequest()
        {
            CertificateRequestProperties certRequestProperties = new CertificateRequestProperties();
            certRequestProperties.Exportable = ExportOption.NotExportable; 
            certRequestProperties.FriendlyName = "Bigsby Client"; 
            certRequestProperties.KeyProtectionLevel = KeyProtectionLevel.NoConsent; 
            certRequestProperties.KeyUsages = EnrollKeyUsages.Signing; 
            certRequestProperties.Subject = "BigsbyClient"; 

            return await CertificateEnrollmentManager.CreateRequestAsync(certRequestProperties);
        }

        private async Task<Certificate> GetClientCertificate()
        {
            //await InstallCertificate(@"Assets\BigsbyRootCert.pfx", "RootCert");
            await InstallCertificate(@"Assets\BigsbyClientCert.pfx", "ClientCert");
            var names = new List<string>();
            var certList = await CertificateStores.FindAllAsync();

            var certs = certList.ToList();

            var query = new CertificateQuery
            {
                FriendlyName = "ClientCert",
            };

            var queried = (await CertificateStores.FindAllAsync(query)).ToList();

            Certificate clientCert = null;
            // Always choose first enumerated certificate.  Works so long as there is only one cert
            // installed and it's the right one.
            if ((null != certList) && (certList.Count > 0))
            {
                clientCert = certList.First();
            }

            return queried.First();
        }

        public static async Task<bool> VerifyCertificateKeyAccess(Certificate selectedCertificate)
        {
            bool VerifyResult = false;  // default to access failure
            CryptographicKey keyPair = await PersistedKeyProvider.OpenKeyPairFromCertificateAsync(
                                                selectedCertificate, HashAlgorithmNames.Sha1,
                                                CryptographicPadding.RsaPkcs1V15);
            String buffer = "Data to sign";
            IBuffer Data = CryptographicBuffer.ConvertStringToBinary(buffer, BinaryStringEncoding.Utf16BE);

            try
            {
                //sign the data by using the key
                IBuffer Signed = await CryptographicEngine.SignAsync(keyPair, Data);
                VerifyResult = CryptographicEngine.VerifySignature(keyPair, Data, Signed);
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine("Verification Failed. Exception Occurred : {0}", exp.Message);
                // default result is false so drop through to exit.
            }

            return VerifyResult;
        }

        private static async Task InstallCertificate(string filePath, string friendlyName)
        {
            //string certPath = @"Assets\BigsbyClientCert.pfx";
            StorageFile file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(filePath);
            IBuffer buffer = await FileIO.ReadBufferAsync(file);
            string certData = CryptographicBuffer.EncodeToBase64String(buffer);

            // Will ask the user if they want this app to install the certificate if its not already installed.
            await CertificateEnrollmentManager.ImportPfxDataAsync(
                 certData,
                 "",
                 ExportOption.NotExportable,
                 KeyProtectionLevel.NoConsent,
                 InstallOptions.None,
                 friendlyName);
        }
    }
}
