using System.Web.Http;

namespace ng4Owin.Controllers
{
    public class SimpleController : ApiController
    {
        public string Get()
        {
            return "From controller";
        }
    }
}