using Newtonsoft.Json.Linq;
using System.Linq;
using System.Web.OData;
using System.Collections.Generic;

namespace ODading.Controllers
{
    public class ItemsController : ODataController
    {
        private static Item[] _items = new[] {
            new Item { Id = 1, Name = "One", Value = 1 },
            new Item { Id = 2, Name = "Two", Value = 2 },
            new Item { Id = 3, Name = "Three", Value = 3, InnerObject = JToken.FromObject(new { someProp = 3, otherProp = "asdfas" })},
            new Item { Id = 4, Name = "Four", Value = 4, InnerObject = JToken.FromObject(new { someProp3 = 3, otherProp4 = "asdfas" }) },
            new Item { Id = 5, Name = "Five", Value = 5 }
        };

        public IQueryable<Item> Get()
        {
            return _items.AsQueryable();
        }

        //http://localhost:61497/odata/items?$filter=someProp%20eq%203

        //http://localhost:61497/odata/items
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public JToken InnerObject { get; set; }

        public IDictionary<string, object> DynamicProperties
        {
            get
            {
                return InnerObject == null ? new Dictionary<string, object>() : InnerObject.ToObject<Dictionary<string, object>>();
            }
        }
    }
}