using InterfaceLibrary;
using System.Web.Http;

namespace TheWebApi
{
    public class ServiceController : ApiController, IService
    {
        [HttpPost]
        public Model BuildModel(string name, int value)
        {
            return new Model
            {
                Name = name,
                Value = value
            };
        }

        [HttpPost]
        public string GetName(Model model)
        {
            return model.Name;
        }

        [HttpGet]
        public string JustString()
        {
            return "This is a string from the service implementation";
        }

        [HttpGet]
        public Model NewModel()
        {
            return new Model
            {
                Name = "New model",
                Value = 59
            };
        }

        [HttpGet]
        public void VoidMethod()
        {
            
        }
    }
}