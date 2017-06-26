using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GenericData.Controllers
{
    [RoutePrefix("api/data")]
    public class DataController : ApiController
    {
        [HttpGet]
        [Route("{set}")]
        public IQueryable<JToken> Get(string set)
        {
            return GetData().AsQueryable();
        }

        private IEnumerable<JToken> GetData()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return JToken.FromObject(new Item
                {
                    Name = $"Item {i + 1}",
                    Value = i
                });
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
