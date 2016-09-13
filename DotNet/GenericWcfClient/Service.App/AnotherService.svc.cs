using System.ServiceModel;

namespace Service.App
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AnotherService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AnotherService.svc or AnotherService.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract]
    public class AnotherService
    {
        [OperationContract]
        public string Call()
        {
            return "You called?";
        }
    }
}
