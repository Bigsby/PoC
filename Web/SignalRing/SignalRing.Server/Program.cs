using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;

using static System.Console;
using SignalRing.Definitions;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRing.Server
{
    class Program
    {
        const string url = "http://*:3000";

        static void Main(string[] args)
        {
            using (WebApp.Start(url))
            {
                
                WriteLine($"Listening to {url}");

                var input = string.Empty;
                while (input != "quit")
                {
                    input = ReadLine();
                    if (input != "quit")
                        GlobalHost.ConnectionManager.GetHubContext<TheHub>().Clients.All.receive(input);
                }
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }

    [HubName(Naming.HubName)]
    public class TheHub : Hub<IHubClient>, IHubServer
    {
        public void Send(string message)
        {
            Clients.Others.Receive("Another sent: " + message);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
    }
}
