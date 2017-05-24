using Owin;
using Microsoft.Owin.Hosting;
using System.Web.Http;
using System.Linq;
using static System.Console;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System;
using System.IdentityModel.Claims;
using System.Security.Claims;
using Microsoft.Owin;

namespace TheServer
{
    public class Server
    {
        public static void Main()
        {
            var baseAddress = "https://*:9001/";

            var so = new StartOptions(baseAddress);
            using (WebApp.Start(so, appBuilder =>
            {
                var config = new HttpConfiguration();
                
                config.Routes.MapHttpRoute("default", "api/{controller}/{action}");

                appBuilder.Use<ClientCertificateAuthMiddleware>(new ClientCertificateAuthenticationOptions(), new DefaultClientCertificateValidator());
                //appBuilder.Use<ClientCertificateAuthorizationMiddleware>();
                appBuilder.UseWebApi(config);
            })) {
                WriteLine("Listenning to " + baseAddress);
                WriteLine("Press RETURN to exit");
                ReadLine();
            }
        }

    }

    public class ClientCertificateAuthenticationOptions : AuthenticationOptions
    {
        public ClientCertificateAuthenticationOptions() : base("X.509")
        { }
    }

    public class ClientCertificateValidationResult
    {
        private readonly bool _certificateValid;
        private readonly IEnumerable<string> _validationExceptions;

        public ClientCertificateValidationResult(bool certificateValid)
        {
            _certificateValid = certificateValid;
            _validationExceptions = new List<string>();
        }

        public void AddValidationExceptions(IEnumerable<string> validationExceptions)
        {
            _validationExceptions.Concat(validationExceptions);
        }

        public void AddValidationException(string validationException)
        {
            _validationExceptions.Concat(new[] { validationException });
        }

        public IEnumerable<string> ValidationExceptions
        {
            get
            {
                return _validationExceptions;
            }
        }

        public bool CertificateValid
        {
            get
            {
                return _certificateValid;
            }
        }
    }

    public interface IClientCertificateValidator
    {
        ClientCertificateValidationResult Validate(X509Certificate2 certificate);
    }

    public class ClientCertificateAuthenticationHandler : AuthenticationHandler<ClientCertificateAuthenticationOptions>
    {
        private readonly IClientCertificateValidator _clientCertificateValidator;
        private readonly string _owinClientCertKey = "ssl.ClientCertificate";

        public ClientCertificateAuthenticationHandler(IClientCertificateValidator clientCertificateValidator)
        {
            _clientCertificateValidator = clientCertificateValidator ?? throw new ArgumentNullException("ClientCertificateValidator");
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            ClientCertificateValidationResult validationResult = await Task<ClientCertificateValidationResult>.Run(() => ValidateCertificate(Request.Environment));
            if (validationResult.CertificateValid)
            {
                AuthenticationProperties authProperties = new AuthenticationProperties();
                authProperties.IssuedUtc = DateTime.UtcNow;
                authProperties.ExpiresUtc = DateTime.UtcNow.AddDays(1);
                authProperties.AllowRefresh = true;
                authProperties.IsPersistent = true;
                IList<System.Security.Claims.Claim> claimCollection = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "Andras")
                , new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Country, "Sweden")
                , new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Gender, "M")
                , new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Surname, "Nemes")
                , new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, "hello@me.com")
                , new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "IT")
                , new System.Security.Claims.Claim("HasValidClientCertificate", "true")
            };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimCollection, "X.509");
                AuthenticationTicket ticket = new AuthenticationTicket(claimsIdentity, authProperties);
                return ticket;
            }
            return await Task.FromResult<AuthenticationTicket>(null);
        }

        private ClientCertificateValidationResult ValidateCertificate(IDictionary<string, object> owinEnvironment)
        {
            if (owinEnvironment.ContainsKey(_owinClientCertKey))
            {
                X509Certificate2 clientCert = Context.Get<X509Certificate2>(_owinClientCertKey);
                return _clientCertificateValidator.Validate(clientCert);
            }

            ClientCertificateValidationResult invalid = new ClientCertificateValidationResult(false);
            invalid.AddValidationException("There's no client certificate attached to the request.");
            return invalid;
        }
    }

    public class DefaultClientCertificateValidator : IClientCertificateValidator
    {
        public ClientCertificateValidationResult Validate(X509Certificate2 certificate)
        {
            bool isValid = false;
            List<string> exceptions = new List<string>();
            try
            {
                X509Chain chain = new X509Chain();
                X509ChainPolicy chainPolicy = new X509ChainPolicy()
                {
                    RevocationMode = X509RevocationMode.NoCheck,
                    RevocationFlag = X509RevocationFlag.EntireChain
                };
                chain.ChainPolicy = chainPolicy;
                if (!chain.Build(certificate))
                {
                    foreach (X509ChainElement chainElement in chain.ChainElements)
                    {
                        foreach (X509ChainStatus chainStatus in chainElement.ChainElementStatus)
                        {
                            exceptions.Add(chainStatus.StatusInformation);
                        }
                    }
                }
                else
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                exceptions.Add(ex.Message);
            }
            ClientCertificateValidationResult res = new ClientCertificateValidationResult(isValid);
            res.AddValidationExceptions(exceptions);
            return res;
        }
    }

    public class ClientCertificateAuthMiddleware : AuthenticationMiddleware<ClientCertificateAuthenticationOptions>
    {
        private readonly IClientCertificateValidator _clientCertificateValidator;

        public ClientCertificateAuthMiddleware(OwinMiddleware nextMiddleware, ClientCertificateAuthenticationOptions authOptions,
            IClientCertificateValidator clientCertificateValidator)
            : base(nextMiddleware, authOptions)
        {
            _clientCertificateValidator = clientCertificateValidator ?? throw new ArgumentNullException("ClientCertificateValidator");
        }

        protected override AuthenticationHandler<ClientCertificateAuthenticationOptions> CreateHandler()
        {
            return new ClientCertificateAuthenticationHandler(_clientCertificateValidator);
        }
    }
}
