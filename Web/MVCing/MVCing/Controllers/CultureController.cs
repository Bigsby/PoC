using System.Web.Http;

namespace MVCing.Controllers
{
    public class CultureController : ApiController
    {
        public string Get()
        {
            return "No paramter";
        }

        public string Get([FromUri]string id)
        {
            var req = Request;
            var result = "";

            foreach (var lang in Request.Headers.AcceptLanguage)
            {
                result += lang.Value + ";";
            }
            return result;
        }
    }
}
