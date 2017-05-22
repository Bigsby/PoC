using Owin;
using Microsoft.Owin.Hosting;
using System.Web.Http;
using static System.Console;
using TheServer.Security;

namespace TheServer
{
    public class Server
    {
        public static void Main()
        {
            var baseAddress = "http://+:9000/";

            var so = new StartOptions(baseAddress);
            using (WebApp.Start(so, appBuilder =>
            {
                var config = new HttpConfiguration();
                
                config.Routes.MapHttpRoute("default", "api/{controller}/{action}");

                appBuilder.Use<ClientCertificateAuthorizationMiddleware>();
                appBuilder.UseWebApi(config);
            })) {
                WriteLine("Listenning to " + baseAddress);
                WriteLine("Press RETURN to exit");
                ReadLine();
            }
        }

    }
}
