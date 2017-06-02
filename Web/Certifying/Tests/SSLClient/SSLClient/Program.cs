using System;
using System.Net.Http;
using static System.Console;

namespace SSLClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:9000")
            };

            var response = client.GetStringAsync("api/DeviceRegistration/String").Result;
            WriteLine("Response: " + response);
            ReadLine();
        }
    }
}
