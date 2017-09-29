using DataLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoreWeb.Controllers
{
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        [Route("")]
        public IActionResult Get()
        {
            try
            {
                var items = DataProvider.GetItems();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Item item)
        {
            try
            {
                await DataProvider.AddItem(item);
                return Created("items", item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
