This are the steps taken to implement client certificates cross-platforms:

1. Create empty solution (Cross-Certificate)
2. Create Self Hosted Web Application
    1. Reserve HTTP Port. In the command line with eleveated permissions:
        ```
        netsh http add urlacl url=http://*:9080/ user=Everyone
        ```
        The result should be something like so:
        ```
        URL reservation successfully added
        ```
   2. Create Console Applicaiton Project for Self-Hosted Server - Console App - (.Net Framework) - *SelfHostedServer*
   3. Install Web API NuGet packages
      - **Microsoft.AspNet.WebApi.OwinSelfHost**
   4. Add web application boilerplating in application startup:
     ``` csharp
    using Owin;
    using Microsoft.Owin.Hosting;
    using System.Web.Http;
    using static System.Console;

    namespace SelfHostedServer
    {
        class SelftHostedServer
        {
            private const string _baseAddress = "http://*:9080/";

            static void Main(string[] args)
            {
                var so = new StartOptions(_baseAddress);
                using (WebApp.Start(so, appBuilder =>
                {
                    var config = new HttpConfiguration();

                    config.Routes.MapHttpRoute("default", "api/{controller}/{action}");

                    appBuilder.UseWebApi(config);
                }))
                {
                    WriteLine("Listenning to " + _baseAddress);
                    WriteLine("Press RETURN to exit");
                    ReadLine();
                }
            }
        }
    }
     ```
    5. Add a simple API Controller:
    ```csharp
    using System.Web.Http;

    namespace SelfHostedServer.Controllers
    {
        public class SimpleController : ApiController
        {
            public string Get()
            {
                return "In Simple Controller";
            }
        }
    }
    ```
    6. Run and test
       1. Run the application hitting **F5**. A console windows shows displaying:
       ```
       Listenning to http://*:9080/
       Press RETURN to exit
       ```
       2. Invoke the controller in a browser with with "http://localhost:9080/api/Simple/Get". The response is something like:
       ```xml
       <string xmlns="http://schemas.microsoft.com/2003/10/Serialization/">In Simple Controller</string>
       ```

