using System.Web.Http;

namespace SelfHostedServer.Controllers
{
    public class SimpleController : ApiController
    {
        public string Get()
        {
            var certificate = RequestContext.ClientCertificate;
            return $"In Simple Controller with {certificate?.SubjectName.Name} certificate.";
        }
    }
}