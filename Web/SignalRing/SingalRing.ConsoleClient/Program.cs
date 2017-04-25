using Microsoft.AspNet.SignalR.Client;
using SignalRing.Definitions;
using static System.Console;

namespace SingalRing.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:3000");
            var proxy = connection.CreateHubProxy(Naming.HubName);

            proxy.On<string>(nameof(IHubClient.Receive), message => WriteLine(message));
            
            connection.Start().ConfigureAwait(false);

            var input = string.Empty;
            while (input != "quit")
            {
                input = ReadLine();
                proxy.Invoke(nameof(IHubServer.Send), input);
            }
        }
    }
}