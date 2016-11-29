using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApp
{
    [EnableCors("*", "*", "*")]
    public class SimpleController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "From server";
        }
    }
}