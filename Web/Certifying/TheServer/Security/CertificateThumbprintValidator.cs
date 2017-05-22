using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TheServer.Security
{
    public class CertificateThumbprintValidator : X509CertificateValidator, IClientX509CertificateValidator
    {
        public StoreLocation[] Locations { get; set; } = { StoreLocation.CurrentUser };
        public StoreName[] Stores { get; set; } = { StoreName.My };

        public override void Validate(X509Certificate2 certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException(nameof(certificate));

            var now = DateTime.Now;

            var thumbprint = certificate.Thumbprint;
            if(string.IsNullOrWhiteSpace(thumbprint))
                throw new SecurityTokenValidationException("The certificate is invalid");

            if (now > certificate.NotAfter || now < certificate.NotBefore)
                throw new SecurityTokenValidationException($"The certificate {thumbprint} is only valid between {certificate.NotAfter} and {certificate.NotBefore}. Now is {now}");

            if (!ContainsCertificate(thumbprint))
                throw new SecurityTokenValidationException($"The certificate {thumbprint} is not found {string.Join(",", Locations)} {string.Join(",", Stores)}");

            if (ContainsCertificate(StoreName.Disallowed, thumbprint))
                throw new SecurityTokenValidationException($"The certificate {thumbprint} have been withdrawn");
        }

        private bool ContainsCertificate(string thumbprint)
        {
            return Stores.Any(store => ContainsCertificate(store, thumbprint));
        }

        private bool ContainsCertificate(StoreName storeName, string thumbprint)
        {
            return Locations.Any(location => ContainsCertificate(location, storeName, thumbprint));
        }

        private static bool ContainsCertificate(StoreLocation location, StoreName storeName, string thumbprint)
        {
            var store = new X509Store(storeName, location);

            try
            {
                store.Open(OpenFlags.ReadOnly);
                var found = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
                return found.Count > 0;
            }
            finally
            {
                store.Close();
            }
        }
    }
}
