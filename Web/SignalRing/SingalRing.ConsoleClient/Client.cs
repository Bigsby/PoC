using Microsoft.AspNet.SignalR.Client;
using SignalRing.Definitions;
using System;
using static System.Console;

namespace SingalRing.ConsoleClient
{
    class Client
    {
        static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:3000");
            var proxy = connection.CreateHubProxy(Naming.HubName);
            
            proxy.On<string>(nameof(IHubClient.Receive), message => WriteLine(message));
            
            connection.Start().ConfigureAwait(false);

            AppDomain.CurrentDomain.ProcessExit += (s, e) => {
                if (connection.State != ConnectionState.Disconnected) connection.Stop();
            };
            var input = string.Empty;
            while (input != "quit")
            {
                input = ReadLine();
                proxy.Invoke(nameof(IHubServer.Send), input);
            }
        }
    }
}