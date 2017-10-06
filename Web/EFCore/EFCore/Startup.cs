using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(EFCore.Startup))]

namespace EFCore
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var apiConfig = new HttpConfiguration();
            apiConfig.MapHttpAttributeRoutes();
            app.UseWebApi(apiConfig);

            var fileSystem = new PhysicalFileSystem("wwwroot");

            var staticFilesConfig = new FileServerOptions
            {
                FileSystem = fileSystem
            };
            staticFilesConfig.DefaultFilesOptions.DefaultFileNames = new[]
            {
                "index.html"
            };
            app.UseFileServer(staticFilesConfig);

            app.Run(async context => {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Resource not found!");
            });
        }
    }
}
