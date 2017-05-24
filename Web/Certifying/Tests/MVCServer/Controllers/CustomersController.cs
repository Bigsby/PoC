using System.Collections.Generic;
using System.Web.Http;

namespace MVCServer.Controllers
{
    public class CustomersController : ApiController
    {
        public IHttpActionResult Get()
        {
            var cert = RequestContext.ClientCertificate;
            var customers = new List<Customer>
            {
                new Customer() { Name = "Nice customer", Address = "USA", Telephone = "123345456" },
                new Customer() { Name = "Good customer", Address = "UK", Telephone = "9878757654" },
                new Customer() { Name = "Awesome customer", Address = "France", Telephone = "34546456" }
            };
            return Ok(customers);
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
    }
}
