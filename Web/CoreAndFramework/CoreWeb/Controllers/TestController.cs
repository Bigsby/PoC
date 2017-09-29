using Microsoft.AspNetCore.Mvc;

namespace CoreWeb.Controllers
{
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [Route("")]
        public IActionResult Test()
        {
            return Ok("This a Core controller.");
        }
    }
}
