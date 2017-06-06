using System.Web.Http;

namespace IISApp.Controllers
{
    public class SimpleController : ApiController
    {
        public string Get() {
            return "In controller";
        }
    }
}
