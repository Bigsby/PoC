using DataLibrary;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace FrameworkWeb.Controllers
{
    [RoutePrefix("api/items")]
    public class ItemsController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var items = DataProvider.GetItems();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody] Item item)
        {
            try
            {
                await DataProvider.AddItem(item);
                return Created("items", item);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}