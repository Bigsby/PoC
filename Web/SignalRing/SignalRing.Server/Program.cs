using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;

using static System.Console;

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
                        GlobalHost.ConnectionManager.GetHubContext<TheHub>().Clients.All.receive("theName", input);
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

    public class TheHub : Hub
    {
        public void Ping(string name)
        {
            Clients.All.ping(name);
        }

        public void Send(string name, string message)
        {
            Clients.All.receive(name, message);
        }
    }
}
