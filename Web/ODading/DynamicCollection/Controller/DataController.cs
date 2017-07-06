using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.OData;

namespace DynamicCollection.Controller
{
    public class DataController : ODataController
    {
        public IQueryable<Item> Get()
        {
            return DataProvider.GetItems(GetCollection()).AsQueryable();
        }

        public Item Get([FromODataUri]string key)
        {
            return DataProvider.GetItem(GetCollection(), key);
        }

        private string GetCollection()
        {
            return DynamicPathHandler.GetCollection((string)RequestContext.RouteData.Values["odataPath"]);
        }
    }

    //Requests
    //http://localhost:51270/data/one
    //http://localhost:51270/data/one?$filter=contains(name,%27w%27)
    //http://localhost:51270/data/one?$orderby=value
    //http://localhost:51270/data/one?$orderby=value%20desc
    //http://localhost:51270/data/one?$filter=value%20gt%20cast(3,%27Edm.Int64%27)

    internal static class DataProvider
    {
        public static IEnumerable<Item> GetItems(string collection)
        {
            var folderPath = HostingEnvironment.MapPath($"~/App_Data/{collection}");
            if (!Directory.Exists(folderPath))
                yield break;

            foreach (var filePath in Directory.GetFiles(folderPath))
            {
                var fileContent = File.ReadAllText(filePath);
                yield return new Item {
                    id = Path.GetFileNameWithoutExtension(filePath),
                    InnerObject = ParseContent(fileContent)
                };
            }   
        }

        private static JToken ParseContent(string content)
        {
            var result = JToken.Parse(content);
            foreach (var property in result)
            {
                //property.
            }
            return result;
        }

        public static Item GetItem(string collection, string id)
        {
            var filePath = HostingEnvironment.MapPath($"~/App_Data/{collection}/{id}.json");
            if (!File.Exists(filePath))
                return null;
            var fileContent = File.ReadAllText(filePath);
            return new Item {
                id = id,
                InnerObject = JToken.Parse(fileContent)
            };
        }
    }
}