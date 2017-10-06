using System.Web.Http;

namespace EFCore.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok("This is the controller.");
        }
    }
}
