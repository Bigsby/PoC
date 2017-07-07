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
using Newtonsoft.Json;

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

        public JObject InnerObject { get; set; }

        public IDictionary<string, object> DynamicProperties
        {
            get
            {
                //return InnerObject == null ? new Dictionary<string, object>() : InnerObject.ToObject<Dictionary<string, object>>();
                return ConvertDynamicProperties(InnerObject);
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

        private static IDictionary<string, object> ConvertDynamicProperties(JObject token)
        {
            var result = new Dictionary<string, object>();
            foreach (var prop in token.Properties())
            {
                if (null == prop.Value) continue;
                result[prop.Name] = ConvertValue(prop.Value);
            }
            
            return result;
        }

        private static object ConvertValue(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Comment:
                case JTokenType.Property:
                case JTokenType.Constructor:
                case JTokenType.None:
                case JTokenType.Object:
                case JTokenType.Undefined:
                case JTokenType.Null:
                    return null;
                case JTokenType.Array:
                    return token.ToObject<object[]>();
                case JTokenType.Integer:
                    return token.ToObject<int>();
                case JTokenType.Float:
                    return token.ToObject<double>();
                case JTokenType.String:
                case JTokenType.Uri:
                case JTokenType.Raw:
                default:
                    return token.ToObject<string>();
                case JTokenType.Boolean:
                    return token.ToObject<bool>();
                case JTokenType.Date:
                    return token.ToObject<DateTime>();
                case JTokenType.Bytes:
                    return token.ToObject<byte[]>();
                case JTokenType.Guid:
                    return token.ToObject<Guid>();
                case JTokenType.TimeSpan:
                    return token.ToObject<TimeSpan>();
            }
        }

        private static object ConvertArray(JArray array)
        {
            if (array.Count == 0)
                return new string[0];

            switch (array.First.Type)
            {
                case JTokenType.Integer:
                    return ((JToken)array).ToObject<int[]>();
                case JTokenType.Float:
                    break;
                case JTokenType.String:
                    break;
                case JTokenType.Boolean:
                    break;
                case JTokenType.Null:
                    break;
                case JTokenType.Undefined:
                    break;
                case JTokenType.Date:
                    break;
                case JTokenType.Raw:
                    break;
                case JTokenType.Bytes:
                    break;
                case JTokenType.Guid:
                    break;
                case JTokenType.Uri:
                    break;
                case JTokenType.TimeSpan:
                    break;
                default:
                    return new string[0];
            }
        }
    }
}
