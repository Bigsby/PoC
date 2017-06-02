using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CrossClient
{
    public static class HttpInvoker
    {
        const string _baseUrl = "https://localhost:9000/";

        public static async Task<string> Invoke(string url)
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Automatic;

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(_baseUrl)
            };

            return await client.GetStringAsync(url).ConfigureAwait(false);
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
