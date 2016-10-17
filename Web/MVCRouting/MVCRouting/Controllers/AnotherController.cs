using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCRouting.Controllers
{
    public class AnotherController : ApiController
    {
        public string Get()
        {
            return "another controllers";
        }
    }
}
