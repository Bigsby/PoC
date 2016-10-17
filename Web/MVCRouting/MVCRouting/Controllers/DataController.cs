using System.Web.Http;
using System.Web.Http.Results;

namespace MVCRouting.Controllers
{
    public class DataController : ApiController
    {
        public JsonResult<object> Get(string database, string collection, string id)
        {
            return Json<object>(new {
                database = database,
                collection = collection,
                id = id
            });
        }
    }
}
