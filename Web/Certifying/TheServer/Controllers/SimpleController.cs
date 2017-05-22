using System.Web.Http;

namespace TheServer.Controllers
{
    public class SimpleController : ApiController
    {
        public string Get()
        {
            return "From simple controller";
        }
    }
}