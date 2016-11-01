using InterfaceLibrary;
using System;
using System.Threading.Tasks;
using WebClientProxy;

namespace UsingClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new Model
            {
                Name = "Client app model",
                Value = 47
            };

            Task.Run(async () =>
            {
                await WebApiClient.Invoke<IService>(s => s.VoidMethod());

                var returnedString = await WebApiClient.Invoke<IService>(s => s.JustString());
                Console.WriteLine($"Returned string: {returnedString}");

                var newModel = await WebApiClient.Invoke<IService, Model>(s => s.NewModel());
                Console.WriteLine($"Model built in server: {newModel.Name} - {newModel.Value}");

                var returnedName = await WebApiClient.Invoke<IService>(s => s.GetName(model));
                Console.WriteLine($"Returned name: {returnedName}");

                //var returnedModel = await WebApiClient.Invoke<IService, Model>(s => s.BuildModel("Built model", 31));
                //Console.WriteLine($"Model built in server: {returnedModel.Name} - {returnedModel.Value}");

            }).Wait();

            Console.ReadLine();
        }
    }
}
