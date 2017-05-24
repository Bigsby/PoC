using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace MVCClient
{
    class MVCClient
    {
        const string _baseUrl = "http://localhost:60593/";
        static void Main(string[] args)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //ServicePointManager.CheckCertificateRevocationList = false;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return  true; };

            var handler = new WebRequestHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateValidationCallback = delegate { return true; };

            var cert = GetClientCertificate();
            var verified = cert.Verify();
            handler.ClientCertificates.Add(cert);

            //var handler = new CustomMessageHandler();

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(_baseUrl)
            };

            WriteLine("Requesting...");
            var response = client.GetStringAsync("api/Customers/Get").Result;
            WriteLine("Received:\n" + response);
            ReadLine();
        }

        public class CustomMessageHandler : HttpClientHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var req = WebRequest.CreateHttp(request.RequestUri);

                foreach (var header in request.Headers)
                    req.Headers[header.Key] = string.Join(",", header.Value);

                req.ClientCertificates = new X509CertificateCollection();
                req.ClientCertificates.Add(GetClientCertificate());

                var wresponse = (HttpWebResponse)await req.GetResponseAsync().ConfigureAwait(false);


                return new HttpResponseMessage(wresponse.StatusCode)
                {
                    Content = new StreamContent(wresponse.GetResponseStream())
                };
            }
        }

        public class CustomHttpRequest : WebRequest
        {
        }

        private static X509Certificate2 GetClientCertificate()
        {
            X509Store userCaStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                userCaStore.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificatesInStore = userCaStore.Certificates;
                X509Certificate2Collection findResult = certificatesInStore.Find(X509FindType.FindBySubjectName, "bigsbylocalclient", true);
                X509Certificate2 clientCertificate = null;
                if (findResult.Count == 1)
                {
                    clientCertificate = findResult[0];
                }
                else
                {
                    throw new Exception("Unable to locate the correct client certificate.");
                }
                return clientCertificate;
            }
            catch
            {
                throw;
            }
            finally
            {
                userCaStore.Close();
            }
        }
    }
}
