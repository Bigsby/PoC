namespace InterfaceLibrary
{
    public interface IService
    {
        void VoidMethod();
        string JustString();
        Model NewModel();
        string GetName(Model model);
        Model BuildModel(string name, int value);
    }

    public class Model
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }
}
