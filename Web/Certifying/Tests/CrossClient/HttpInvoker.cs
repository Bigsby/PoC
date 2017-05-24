using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CrossClient
{
    public static class HttpInvoker
    {
        const string _baseUrl = "https://localhost:60593/";

        public static string Invoke()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(_baseUrl)
            };

            return client.GetStringAsync("api/Simple/Get").Result;
        }
    }

    public class CustomHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var wr = WebRequest.CreateHttp(request.RequestUri);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
