using System.ServiceModel;

namespace Service.Contract
{
    [ServiceContract]
    public interface IServiceContract
    {
        [OperationContract]
        int GetInt();

        [OperationContract]
        Item GetItem(string name, int value);
    }

    public class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}