using System.Security.Cryptography.X509Certificates;

namespace TheServer.Security
{
    public interface IClientX509CertificateValidator
    {
        void Validate(X509Certificate2 certificate);
    }
}
