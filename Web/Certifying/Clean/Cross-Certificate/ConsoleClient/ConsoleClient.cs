using System;
using System.Net.Http;
using static System.Console;

namespace ConsoleClient
{
    class ConsoleClient
    {
        private const string _baseAddress = "http://localhost:9080";
        static void Main(string[] args)
        {
            var client = new HttpClient
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
