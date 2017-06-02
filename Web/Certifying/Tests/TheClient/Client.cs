using System;
using System.Net.Http;
using System.Net.Http.Headers;
using static System.Console;

namespace TheClient
{
    class Client
    {
        static string _baseUrl = "https://localhost:9000/api/";
        static string identity = "1234";
        static string _baseAddress = $"DeviceRegistration";
        static void Main(string[] args)
        {
            while (true)
            {
                WriteLine("Requesting...");

                try
                {
                    var client = BuildClient(true);
                    var response = client.GetAsync($"{_baseAddress}?identity={identity}").Result;
                    WriteLine("Received: " + response);
                }
                catch (Exception ex)
                {
                    WriteLine("Error:" + ex.Message);
                }

                WriteLine("RETURN to make new request");
                ReadLine();
            }
        }

        private static HttpClient BuildClient(bool addHeaders)
        {
            var client = new HttpClient(new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Automatic
            })
            {
                BaseAddress = new Uri(_baseUrl)
            };

            if (addHeaders)
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            return client;
        }
    }
}
