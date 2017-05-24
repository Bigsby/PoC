using System.Net.Http;

namespace CertificateLibrary
{
    public static class HttpClientFactory
    {
        private static HttpClient _client;

        public static HttpClient GetClient()
        {
            return _client ?? (_client = BuildClient());
        }

        private static HttpClient BuildClient()
        {
            var handler = new WebRequestHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ClientCertificates.Add(CertificateProvider.GetCertificate());
            return new HttpClient(handler);
        }
    }
}
