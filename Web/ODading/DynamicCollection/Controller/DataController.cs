using DynamicCollection.Models;
using System.Linq;
using System.Web.OData;

namespace DynamicCollection.Controller
{
    public class DataController : ODataController
    {
        public IQueryable<GenericItem> Get()
        {
            return DataProvider.GetItems(GetCollection()).AsQueryable();
        }

        public GenericItem Get([FromODataUri]string key)
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
    //http://localhost:51270/data/one?$filter=contains(name,'w')
    //http://localhost:51270/data/one?$orderby=value
    //http://localhost:51270/data/one?$orderby=value desc
    //http://localhost:51270/data/one?$filter=value gt 3
}