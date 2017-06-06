using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System.Web.Http;

namespace ng4Owin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var apiConfig = new HttpConfiguration();
            apiConfig.Routes.MapHttpRoute("default", "api/{controller}/{action}");
            app.UseWebApi(apiConfig);

            var fileSystem = new PhysicalFileSystem(@".\wwwroot");
            var fileServerOptions = new FileServerOptions
            {
                FileSystem = fileSystem
            };
            app.UseFileServer(fileServerOptions);

            app.Run(context => {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("OWIN here!");
            });
        }
    }
}