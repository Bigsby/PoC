using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.Cookies;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(CookieAuth.Startup))]

namespace CookieAuth
{
    public class Startup
    {
        public static List<string> requestedTypes = new List<string>();
        public void Configuration(IAppBuilder app)
        {
            var cookieOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = "myApp"
            };

            app.UseCookieAuthentication(cookieOptions);

            var apiConfig = new HttpConfiguration();
            apiConfig.Routes.MapHttpRoute("default", "api/{controller}/{action}");
            app.UseWebApi(apiConfig);
            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("OWIN here!");
            });
        }
    }
}
