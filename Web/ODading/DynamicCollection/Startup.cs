using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.OData.Routing;
using System.Web.OData.Extensions;
using System.Collections.Generic;
using System;
using Microsoft.OData.Edm;
using Newtonsoft.Json.Linq;
using System.Web.OData.Builder;
using System.Web.OData.Routing.Template;
using System.Web.OData.Routing.Conventions;
using System.Text.RegularExpressions;
using System.Linq;

[assembly: OwinStartup(typeof(DynamicCollection.Startup))]

namespace DynamicCollection
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();            
            config.Routes.MapHttpRoute("api", "api/{controller}/{action}");
            ConfigureOdata(config);
            app.UseWebApi(config);
        }

        static void ConfigureOdata(HttpConfiguration config)
        {
            var routePrefix = "data";
            var routeName = "odata";

            config.MapODataServiceRoute(routeName, routePrefix, Item.GetModel(), new DynamicPathHandler(), ODataRoutingConventions.CreateDefault());
            config.Count().Filter().Select().OrderBy();
            config.AddODataQueryFilter();
        }
    }

    public class DynamicPathHandler : DefaultODataPathHandler
    {
        private static Regex _collectionRegex = new Regex("^([^/(\\?%]+)", RegexOptions.Compiled);

        public override ODataPath Parse(string serviceRoot, string odataPath, IServiceProvider requestContainer)
        {
            return base.Parse(serviceRoot, _collectionRegex.Replace(odataPath, "data"), requestContainer);
        }

        public override ODataPathTemplate ParseTemplate(string odataPathTemplate, IServiceProvider requestContainer)
        {
            return base.ParseTemplate(odataPathTemplate, requestContainer);
        }

        internal static string GetCollection(string path)
        {
            return _collectionRegex.Match(path).Groups[1].Value;
        }
    }



    public class Item
    {
        public string id { get; set; }

        public JToken InnerObject { get; set; }

        public IDictionary<string, object> DynamicProperties
        {
            get
            {
                return InnerObject == null ? new Dictionary<string, object>() : InnerObject.ToObject<Dictionary<string, object>>();
            }
        }

        internal static IEdmModel GetModel()
        {
            var builder = new ODataModelBuilder();
            var itemType = builder.EntityType<Item>();
            itemType.HasKey(i => i.id);
            itemType.HasDynamicProperties(i => i.DynamicProperties);
            builder.EntitySet<Item>("data");

            return builder.GetEdmModel();
        }
    }
}
