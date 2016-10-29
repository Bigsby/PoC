using System.Web.Http;

namespace WebPackagingChild.Controllers
{
    public class SimpleController : ApiController
    {
        public string Get()
        {
            return "In the controller";
        }
    }
}
