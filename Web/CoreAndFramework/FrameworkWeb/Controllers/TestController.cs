using System.Web.Http;

namespace FrameworkWeb.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Test()
        {
            return Ok("This is .Net Framework controller.");
        }
    }
}