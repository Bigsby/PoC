using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(IISApp.Startup))]

namespace IISApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{action}");

            app.UseWebApi(config);

            var fileSystem = new PhysicalFileSystem("./static");
            var fsOptions = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = fileSystem
            };
            fsOptions.StaticFileOptions.FileSystem = fileSystem;
            fsOptions.StaticFileOptions.ServeUnknownFileTypes = true;
            fsOptions.DefaultFilesOptions.DefaultFileNames = new[] 
            {
                "index.html"
            };

            app.UseFileServer(fsOptions);
            
            app.Run(context => {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Me here");
            });
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}

//https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/getting-started-with-owin-and-katana
//https://www.roelvanlisdonk.nl/2015/03/18/usewebapi-not-found-missing-in-web-api-2-2/
//https://richardniemand.wordpress.com/2015/06/18/hosting-web-api-and-static-content-with-owin/

  //<system.web>
  //  <compilation debug = "true" targetFramework="4.5.2" />
  //  <httpRuntime targetFramework = "4.5.2" />
  //  < customErrors mode="Off"/>
  //  <trust level = "Full" />
  //</ system.web >
// or set it in ASP.Net Settings in Plest Hosting configuration of the web site

// NuGet Packges required
// Microsoft.owin.host.systemweb
// Microsoft.aspnet.webapi.owin
// Microsoft.owin.staticfiles
