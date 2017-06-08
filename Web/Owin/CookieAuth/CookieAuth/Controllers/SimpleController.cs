using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace CookieAuth.Controllers
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


        [Authorize(Roles = "Role1")]
        [HttpGet]
        public string Auth2()
        {
            return "Authorized: " + HttpContext.Current.GetOwinContext()
                .Authentication.User?.Identity.Name;
        }

        [Authorize(Roles = "Role2")]
        [HttpGet]
        public string Auth3()
        {
            return "Authorized: " + HttpContext.Current.GetOwinContext()
                .Authentication.User?.Identity.Name;
        }


        [HttpGet]
        public bool Is() => HttpContext.Current.GetOwinContext()
                .Authentication.User?.Identity?.IsAuthenticated == true;

        [HttpGet]
        public string In(string user)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user + "id"),
                new Claim(ClaimTypes.Role, "Role1"),
                new Claim(ClaimTypes.Name, user),

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
                .Authentication.SignOut();

            return "Signed Out!";
        }
    }
}