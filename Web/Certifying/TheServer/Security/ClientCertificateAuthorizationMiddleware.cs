using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace TheServer.Security
{
    public class ClientCertificateAuthorizationMiddleware : OwinMiddleware
    { 
        // that header is expected to be forwarded by an API proxy which doesn't pass the certificate otherwise and the API requires validation beyond capabilities of the proxy
        // or in case if there are concerns that the IP based restrictions can be bypassed
        // For the Azure API Management then the next policy can be used:
        // <policies>
        // 	<inbound>
        // 		<base />
        // 		<choose>
        // 			<when condition = "@(context.Request.Certificate == null || context.Deployment.Certificates.Any(c => c.Value.Thumbprint == context.Request.Certificate.Thumbprint))" >
        //                 <return-response>
        // 					<set-status code = "401" reason="Invalid client certificate" />
        // 				</return-response>
        // 			</when>
        // 		</choose>
        // 		<set-header name = "ov-clientcert" exists-action="skip">
        // 			<value>@(Convert.ToBase64String(context.Request.Certificate.RawData))</value>
        // 		</set-header>
        // 	</inbound>
        // 	<backend>
        // 		<base />
        // 	</backend>
        // 	<outbound>
        // 		<base />
        // 	</outbound>
        // </policies>
        public const string FallbackCustomClientCertificateHeader = "ov-clientcert";

        private readonly IClientX509CertificateValidator _validator;

        public ClientCertificateAuthorizationMiddleware(OwinMiddleware next)
            : this(next, null)
        {
        }

        public ClientCertificateAuthorizationMiddleware(OwinMiddleware next, IClientX509CertificateValidator validator) 
            : base(next)
        {
            _validator = validator ?? new CertificateThumbprintValidator();
        }

        public override Task Invoke(IOwinContext context)
        {
            try
            {
                var cert = GetClientCerttificate(context);

                if (cert != null)
                {
                    _validator.Validate(cert);

                    return Next.Invoke(context);
                }
            }
            catch(Exception e)
            {

            }

            context.Response.StatusCode = 403;
            context.Response.ContentType = "text/plain charset=utf-8";

            return context.Response.WriteAsync("Client certificate validation failed");
        }

        private static X509Certificate2 GetClientCerttificate(IOwinContext context)
        {
            var cert = context.Get<X509Certificate2>("ssl.ClientCertificate");
            if (cert != null)
                return cert;

            var headerValue = context.Request.Headers[FallbackCustomClientCertificateHeader];

            return string.IsNullOrWhiteSpace(headerValue) 
                ? null 
                : new X509Certificate2(Convert.FromBase64String(headerValue));
        }
    }
}
