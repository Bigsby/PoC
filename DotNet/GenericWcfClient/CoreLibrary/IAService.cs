using System.ServiceModel;

namespace CoreLibrary
{
    [ServiceContract]
    interface IAService
    {
        [OperationContract]
        string Call(string value);
    }
}
