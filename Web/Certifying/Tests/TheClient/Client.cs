using System;
using System.Net.Http;
using static System.Console;

namespace TheClient
{
    class Client
    {
        static void Main(string[] args)
        {
            while (true)
            {
                WriteLine("Requesting...");

                try
                {
                    var client = CertificateLibrary.HttpClientFactory.GetClient();
                    var response = client.GetStringAsync("https://localhost:9001/api/Simple/Get").Result;
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
    }
}
