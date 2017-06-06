Angular 4 & OWIN Web Api

1. Create VS Project
    1. New Project - ASP.Net Web Application (.Net Framework) - Name: ng4Owin
    2. Template: Empty

2. Add OWIN
    1. Add NuGet Package: Microsoft.Owin.Host.SystemWeb
    2. Add New Item - OWIN Startup class - Name: Startup.cs
        ```csharp
        using Owin;

        namespace ng4Owin
        {
            public class Startup
            {
                public void Configuration(IAppBuilder app)
                {
                    app.Run(context => {
                        context.Response.ContentType = "text/plain";
                        return context.Response.WriteAsync("OWIN here!");
                    });
                }
            }
        }
        ```
    3. Run and test by hitting **F5**. The default browser should open in "http://localhost:60478/" (60478 is a generated random port) and display:
        ```
        OWIN here!
        ```

3. Add Web Api
    1. Add Nuget Package: Microsoft.AspNet.WebApi.Owin
    2. Add Web Api Middleware and configure routes in *Startup.cs*:
        ```csharp
        using Owin;
        using System.Web.Http;

        namespace ng4Owin
        {
            public class Startup
            {
                public void Configuration(IAppBuilder app)
                {
                    var apiConfig = new HttpConfiguration();
                    apiConfig.Routes.MapHttpRoute("default", "api/{controller}/{action}");
                    app.UseWebApi(apiConfig);

                    
                    app.Run(context => {
                        context.Response.ContentType = "text/plain";
                        return context.Response.WriteAsync("OWIN here!");
                    });
                }
            }
        }
        ```
    3. Create an Api controller
        1. (optional) Create a project folder called Controllers
        2. Add - Controller... - Web Api 2 Controller - Empty - Controller Name: SimpleController
        3. Add action to controller, in *SimpleController.cs*
            ```csharp
            using System.Web.Http;

            namespace ng4Owin.Controllers
            {
                public class SimpleController : ApiController
                {
                    public string Get()
                    {
                        return "From controller";
                    }
                }
            }
            ```
        4. Run and test by hittin **F5**. The default browser should open, as before, but, now, add the controller action to the path, i.e., "http://localhost:60478/api/Simple/Get", and the following shoudl display:
            ```
            From controller
            ```
            
            *Note*: There might be some wrapping on the browser display, depending on the accept sent by the browser. E.g, Firefox displays ```<string>From controller</string>``` and Edge displays ```"From controller"```
        
4. Add Static Files Support
    1. NuGet Package: Microsoft.Owin.StaticFiles
    2. Add Static Files Middleware and configura root path in *Startup.cs*:
        ```csharp
        using Microsoft.Owin.FileSystems;
        using Microsoft.Owin.StaticFiles;
        using Owin;
        using System.Web.Http;

        namespace ng4Owin
        {
            public class Startup
            {
                public void Configuration(IAppBuilder app)
                {
                    var apiConfig = new HttpConfiguration();
                    apiConfig.Routes.MapHttpRoute("default", "api/{controller}/{action}");
                    app.UseWebApi(apiConfig);

                    var fileSystem = new PhysicalFileSystem("wwwroot");
                    var staticFilesConfig = new FileServerOptions
                    {
                        FileSystem = fileSystem
                    };
                    app.Run(context => {
                        context.Response.ContentType = "text/plain";
                        return context.Response.WriteAsync("OWIN here!");
                    });
                }
            }
        }
        ```
    3. Change ```web.config``` to use OWIN Handler
        ```xml
        <?xml version="1.0"?>
        <configuration>
        <system.webServer>
            <handlers>
            <clear/>
            <add name="Owin" verb="" path="*" type="Microsoft.Owin.Host.SystemWeb.OwinHttpHandler, Microsoft.Owin.Host.SystemWeb"/>
            </handlers>
        </system.webServer>
        <runtime>
            <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
            <dependentAssembly>
                <assemblyIdentity name="Microsoft.Owin" publicKeyToken="31bf3856ad364e35" culture="neutral" />
                <bindingRedirect oldVersion="0.0.0.0-3.1.0.0" newVersion="3.1.0.0" />
            </dependentAssembly>
            </assemblyBinding>
        </runtime>
        <system.web>
            <compilation debug="true"/>
        </system.web>
        </configuration>
        ```
    4. Create ```wwwroot``` folder
    5. Add test file
        1. Add - New Item... - Text File - Name: test.txt
            ```
            Static content
            ```
    6. Run and test by hittin **F5**. The default browser should open, as before, but, now, add the controller action to the path, i.e., "http://localhost:60478/api/Simple/Get", and the following shoudl display:
        ```
        Static content
        ```
    