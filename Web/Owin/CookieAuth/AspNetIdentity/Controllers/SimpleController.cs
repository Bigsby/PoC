using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace AspNetIdentity.Controllers
{
    public class SimpleController : ApiController
    {
        public string Get()
        {
            return "From controller";
        }

        [Authorize]
        [HttpGet]
        public string Auth()
        {
            return "Authorized: " + HttpContext.Current.GetOwinContext()
                .Authentication.User?.Identity.Name;
        }

        [Authorize]
        [HttpGet]
        public string Auth2()
        {
            return "Authorized: " + HttpContext.Current.GetOwinContext()
                .Authentication.User?.Identity.Name;
        }

        [HttpGet]
        public bool Is() => HttpContext.Current.GetOwinContext()
                .Authentication.User?.Identity?.IsAuthenticated == true;

        [HttpGet]
        [AllowAnonymous]
        public string In(string user)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user + "id"),
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.Role, "Role1")
            };
            var identity = new ClaimsIdentity(claims, "myApp");
            var authenticationpropertiers = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddSeconds(10)
            };
            HttpContext.Current.GetOwinContext()
                .Authentication.SignIn(authenticationpropertiers, identity);
            return "Signed In!";
        }

        [HttpGet]
        public string Out()
        {
            HttpContext.Current.GetOwinContext()
                .Authentication.SignOut("myApp");

            return "Signed Out!";
        }
    }
}