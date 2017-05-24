using System.Web.Http;

namespace SelfHostedServer.Controllers
{
    public class SimpleController : ApiController
    {
        public string Get()
        {
            return "In Simple Controller";
        }
    }
}
