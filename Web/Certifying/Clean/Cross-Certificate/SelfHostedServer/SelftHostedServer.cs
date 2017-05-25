using Owin;
using Microsoft.Owin.Hosting;
using System.Web.Http;
using static System.Console;
using SelfHostedServer.Security;

namespace SelfHostedServer
{
    class SelftHostedServer
    {
        private const string _baseAddress = "https://*:9443/";

        static void Main(string[] args)
        {
            var so = new StartOptions(_baseAddress);
            using (WebApp.Start(so, appBuilder =>
            {
                var config = new HttpConfiguration();
                
                config.Routes.MapHttpRoute("default", "api/{controller}/{action}");

                appBuilder.Use<ClientCertificateAuthMiddleware>();
                appBuilder.UseWebApi(config);
            }))
            {
                WriteLine("Listenning to " + _baseAddress);
                WriteLine("Press RETURN to exit");
                ReadLine();
            }
        }
    }
}