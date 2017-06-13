using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.AspNetCore.Builder;

[assembly: OwinStartup(typeof(BrowserLinking.Startup))]

namespace BrowserLinking
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            var apiConfig = new HttpConfiguration();
            apiConfig.Routes.MapHttpRoute("default", "api/{controller}/{action}");
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

            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("OWIN here!");
            });
        }
    }
}
