using System.Web.Http;

namespace ODading.Controllers
{
    public class SimpleController : ApiController
    {
        public string Get()
        {
            return "in controller";
        }
    }
}