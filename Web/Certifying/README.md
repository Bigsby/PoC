## Requirements

- **Visual Studio** 2015 or later, any edition

## Steps
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
   2. Create Console Applicaiton Project for Self-Hosted Server - Console App (.Net Framework) - *SelfHostedServer*
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

3. Create Client Console Application
    1. Create Console Applicaiton Project for client - Console App (.Net Framework) - *ConsoleClient*
    2. Add HTTP request code on application start:
    ```csharp
    using System;
    using System.Net.Http;
    using static System.Console;

    namespace ConsoleClient
    {
        class ConsoleClient
        {
            private const string _baseAddress = "http://localhost:9080";
            static void Main(string[] args)
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(_baseAddress)
                };

                WriteLine("Making request...");
                var response = client.GetStringAsync("api/Simple/Get").Result;
                WriteLine("Response:");
                WriteLine(response);

                WriteLine("Press RETURN to exit");
                ReadLine();
            }
        }
    }
    ```
    3. Configure multiple startup projects
        1. In VisuaStudio, *right-click* solution name and select *Properties*
        2. Select *Common Properties* - *Starup Project*
        3. Select *Multiple startup projects*
        4. Set *Action* to *Start* on both *ConsoleClient* and *SelfHostedServer*
        5. Just by principle, set the correct project startup order, using the *arrows* on the right of the projects' list, *i.e.*:
            - *SelfHostedServer*
            - *ConsoleClient*
    4. Run and Test
        
        Run both application hitting **F5**. Two console windows open.
        - One for the server application, as before:
            ```
            Listenning to http://*:9080/
            Press RETURN to exit
            ```
        - Another for the client displaying:
            ```
            Making request...
            Response:
            "In Simple Controller"
            Press RETURN to exit
            ```
4. Add SSL to  Self Hosted application
    1. Create Certificate, in *Developer Command Prompt for Visual Studio* as *Adminstrator*:
        ```
        makecert -r -n "CN=localhost" -pe -sv Localhost.pvk Localhost.cer
        ```
        
        A pop up will show asking for the password (and confirmation) for the certificate's private key

        ![](PrivateKeyPassword.png)

        Enter the password just inserted

        ![](EnterPrivateKeyPassword.png)

        Two files are created:
        - *Localhost.cer*
        - *Localhost.pvk*

    2. Create certificate export file, in *Developer Command Prompt for Visual Studio* as *Adminstrator*:
        ```
        pvk2pfx -pvk Localhost.pvk -spc Localhost.cer -pfx Localhost.pfx
        ```
        That has not output but results in the creation of the file:
        - *Localhost.pfx*

    3. Reserve HTTP port for HTTP, in *Developer Command Prompt for Visual Studio* as *Adminstrator*:
        ```
        netsh http add urlacl url=https://*:9443/ user=Everyone
        ```
        The result should be something like so:
        ```
        URL reservation successfully added
        ```
    4. Add the certificate to the HTTP port, in *Developer Command Prompt for Visual Studio* as *Adminstrator*:
        1. Import the certificate to the computer store:
            ```
            certutil -importpfx Localhost.pfx
            ```
            A prompt for the import password will show:
            ```
            Enter PFX password:
            ```
            If none was (like in this sequence of steps), just hit ENTER. And the output is:
            ```
            Certificate "localhost" added to store.

            CertUtil: -importPFX command completed successfully.
            ```
        2. Get certificate's thumbprint
            ```
            certutil Localhost.cer | findstr /c:"Cert Hash(sha1)" | for /f "tokens=3-22" %f in ('more') do @echo %f%g%h%i%j%k%l%m%n%o%p%q%r%s%t%u%v%w%x%y
            ```
            That outputs the certificate's thumbprint, e.g.:
            ```
            4911cbee6cd686e0cb132fdb16677a0aef81391e
            ```
        3. Get application Id. In *SelfHostedServer* project, open *Properties* > *AssemblyInfo.cs* file and get the assembly's GUID, e.g.:
            ```csharp
            [assembly: Guid("74552ba6-d975-4763-a02a-7c767190b941")]
            ```
        4. Add the certicate to the HTTP port, in *Developer Command Prompt for Visual Studio* as *Adminstrator*:
            ```
            netsh http add sslcert ipport=0.0.0.0:9443 certhash=4911cbee6cd686e0cb132fdb16677a0aef81391e appid={74552ba6-d975-4763-a02a-7c767190b941}
            ```
            That outpus:
            ```
            SSL Certificate successfully added
            ```
        5. Change *SelfHostedServer* to use the new HTTP URL by changing:
            ```csharp
            private const string _baseAddress = "http://*:9080/";
            ```
            to:
            ```csharp
            private const string _baseAddress = "https://*:9443/";
            ```
        6. Change *ConsoleClient* to connection tot the new URL by changing:
            ```csharp
            private const string _baseAddress = "http://localhost:9080";
            ```
            to:
            ```csharp
            private const string _baseAddress = "https://localhost:9443";
            ```
        7. Run and test by hitting **F5**. The server console window displays:
            ```
            Listenning to https://*:9443/
            Press RETURN to exit
            ```
            The client window shows the same as before, *i.e.*:
            ```
            Making request...
            Response:
            "In Simple Controller"
            Press RETURN to exit
            ```
        8. Test on the browser(s)
            1. **Google Chrome** will download the content but will display visual indication the certificate is not trusted:

                ![](ChromeUnsecureIcon.PNG)
            2. **Mozilla Firefox**, before downloading any content, will show a warning saying the connection is not secure:

                ![](FirefoxUnsecure.PNG)
                Clicking *Advanced* will allow to create a security exception and, only them download the content but, still, displaying visual indication that the connection is not secure:

                ![](FirefoxUnsecureIcon.PNG)
            3. **Microsoft Edge** will just download the content and show a indication the communication is encrypted:

                ![](EdgeEncryptedIcon.PNG)
            
        


5. Add Certificate to Client Request
    1. Create Client Certificate, in *Developer Command Prompt for Visual Studio* as *Adminstrator*:
        ```
        makecert -r -n "CN=ClientCertificate" -pe -sv ClientCertificate.pvk ClientCertificate.cer
        ```

        A pop up will show asking for the password (and confirmation) for the certificate's private key

        ![](PrivateKeyPassword.png)

        Enter the password just inserted

        ![](EnterPrivateKeyPassword.png)

        Two files are created:
        - *ClientCertificate.cer*
        - *ClientCertificate.pvk*
    2. Create certificate export file, in Developer Command Prompt for Visual Studio as Adminstrator:
        ```
        pvk2pfx -pvk ClientCertificate.pvk -spc ClientCertificate.cer -pfx ClientCertificate.pfx
        ```
    3. Import the certificate to the computer store:
        ```
        certutil -importpfx ClientCertificate.pfx
        ```
        A prompt for the import password will show:
        ```
        Enter PFX password:
        ```
        If none was (like in this sequence of steps), just hit ENTER. And the output is:
        ```
        Certificate "ClientCertificate" added to store.

        CertUtil: -importPFX command completed successfully.
        ```

    4. Add Reference to **System.Net.Http.WebRequest** to *ConsoleClient* project by:
        1. *Right-click* *ConsoleClient* *References*

            ![](AddWebRequestReference.PNG)
        2. Check *System.Net.Http.WebRequest* and click *OK*
    5. Add client certificate to HTTP request, adding an *HttpMessageHandler* to the *HttpClient* constructor:
        ```csharp
        using System;
        using System.Net.Http;
        using System.Security.Cryptography.X509Certificates;
        using static System.Console;

        namespace ConsoleClient
        {
            class ConsoleClient
            {
                private const string _baseAddress = "https://localhost:9443";
                static void Main(string[] args)
                {
                    var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                    store.Open(OpenFlags.ReadOnly);
                    var query = store.Certificates.Find(X509FindType.FindBySubjectName, "ClientCertificate", false);
                    var certificate = query[0];

                    var handler = new WebRequestHandler();
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ClientCertificates.Add(certificate);

                    var client = new HttpClient(handler)
                    {
                        BaseAddress = new Uri(_baseAddress)
                    };

                    WriteLine("Making request...");
                    var response = client.GetStringAsync("api/Simple/Get").Result;
                    WriteLine("Response:");
                    WriteLine(response);

                    WriteLine("Press RETURN to exit");
                    ReadLine();
                }
            }
        }
        ```
    6. Read Certificate in Server Response
        
        In the controller:
        ```csharp
        using System.Web.Http;

        namespace SelfHostedServer.Controllers
        {
            public class SimpleController : ApiController
            {
                public string Get()
                {
                    var certificate = RequestContext.ClientCertificate;
                    return $"In Simple Controller with {certificate.SubjectName.Name} certificate.";
                }
            }
        }
        ```
    7. Run and Test
        Run both application hitting **F5**. Two console windows open.
        - One for the server application, as before:
            ```
            Listenning to https://*:9443/
            Press RETURN to exit
            ```
        - And client will, now display:
            ```
            Making request...
            Response:
            "In Simple Controller with CN=ClientCertificate certificate."
            Press RETURN to exit
            ```
6. Request for Client Certificate Negotiation
7. Add Client Certificate to browsers requests
    