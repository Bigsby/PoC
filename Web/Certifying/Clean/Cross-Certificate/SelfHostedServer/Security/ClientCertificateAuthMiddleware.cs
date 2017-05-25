using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace SelfHostedServer.Security
{
    class ClientCertificateAuthMiddleware : OwinMiddleware
    {
        private const string _sslClientCertificateKey = "ssl.ClientCertificate";
        public ClientCertificateAuthMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var clientCertificate = context.Get<X509Certificate2>("ssl.ClientCertificate");
            if (null == clientCertificate)
            {
                context.Response.StatusCode = 403;
                context.Response.ReasonPhrase = "No Client Certificate Provided";
            }
            else if (clientCertificate.Thumbprint != "AF57A0F6E385546DDFEA7F96242FA88C4DAAEA53")
            {
                context.Response.StatusCode = 403;
                context.Response.ReasonPhrase = "Wrong Client Certificate Provided";
            }
            else
                await Next.Invoke(context).ConfigureAwait(false);
        }
    }
}