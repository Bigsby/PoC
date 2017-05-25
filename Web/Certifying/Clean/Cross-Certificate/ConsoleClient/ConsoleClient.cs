using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using static System.Console;

namespace ConsoleClient
{
    class ConsoleClient
    {
        private const string _baseAddress = "https://localhost:9443";
        static void Main(string[] args)
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            var query = store.Certificates.Find(X509FindType.FindBySubjectName, "ClientCertificate", false);
            var certificate = query[0];

            var handler = new WebRequestHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ClientCertificates.Add(certificate);

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(_baseAddress)
            };

            WriteLine("Making request...");
            var response = client.GetStringAsync("api/Simple/Get").Result;
            WriteLine("Response:");
            WriteLine(response);

            WriteLine("Press RETURN to exit");
            ReadLine();
        }
    }
}
