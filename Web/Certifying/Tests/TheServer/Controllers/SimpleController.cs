using System.Web.Http;

namespace TheServer.Controllers
{
    public class SimpleController : ApiController
    {
        public string Get()
        {
            var cert = RequestContext.ClientCertificate;

            return cert == null ? "no Cert" : cert.SubjectName.Name;
        }
    }
}