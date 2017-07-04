using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System.Web.Http;
using System.Web.OData.Extensions;
using ODading.Controllers;
using System.Web.OData.Builder;

[assembly: OwinStartup(typeof(ODading.Startup))]

namespace ODading
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var apiConfig = new HttpConfiguration();

            ConfigureOData(apiConfig);

            apiConfig.Routes.MapHttpRoute("default", "api/{controller}/{action}");
            app.UseWebApi(apiConfig);

            var fileSystem = new PhysicalFileSystem("wwwroot");
            var defaultFileOptions = new DefaultFilesOptions();

            var staticFilesConfig = new FileServerOptions
            {
                FileSystem = fileSystem,
                EnableDefaultFiles = true,
            };

            staticFilesConfig.DefaultFilesOptions.DefaultFileNames.Clear();
            staticFilesConfig.DefaultFilesOptions.DefaultFileNames.Add("index.html");

            app.UseFileServer(staticFilesConfig);

            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("OWIN here!");
            });
        }

        private static void ConfigureOData(HttpConfiguration config)
        {
            var builder = new ODataModelBuilder();
            var itemType = builder.EntityType<Item>();
            itemType.HasKey(i => i.Id);
            itemType.Property(i => i.Name);
            itemType.Property(i => i.Value);
            itemType.HasDynamicProperties(i => i.DynamicProperties);
            builder.EntitySet<Item>("items");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "odata",
                model: builder.GetEdmModel());
            config.Count().Filter().Select().OrderBy();
            config.AddODataQueryFilter();
        }
    }
}
