using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TwitterClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var key = ConfigurationManager.AppSettings["twitterKey"];
            //var secret = ConfigurationManager.AppSettings["twitterSecret"];

            Task.Run(async () =>
            {
                var client = new HttpClient();

                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", BuildAuthenticationHeaer(key, secret));
                //var response = await client.PostAsync("https://api.twitter.com/oauth2/token", new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded"));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "AAAAAAAAAAAAAAAAAAAAAA8rxwAAAAAAWODkx9SO8%2BQEfXIXp%2FP3DZMZrCQ%3DyQvlG6QtZv1gQXfuFKc9ap9NcJnCdvmXJhpD8wLxGb9lM9AMbM");
                var responseString = await client.GetStringAsync("https://api.twitter.com/1.1/statuses/user_timeline.json?count=10&screen_name=qwattiesports");


                WriteLine(responseString);


                WriteLine("Done!");
                ReadLine();

            }).Wait();
        }

        private static string BuildAuthenticationHeaer(string key, string secret)
        {
            var encodedKey = Uri.EscapeDataString(key);
            var encodedSecret = Uri.EscapeDataString(secret);
            var bytes = Encoding.UTF8.GetBytes($"{encodedKey}:{encodedSecret}");
            return Convert.ToBase64String(bytes);
        }
    }
}
