using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DynamicCollection.Controller
{
    public class SimpleController : ApiController
    {
        public string Get()
        {
            return "In controller";
        }
    }
}