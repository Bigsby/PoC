using System;
using System.Web.Http;
using System.Web.Mvc;

namespace MVCRouting
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(config => {
                config.Routes.MapHttpRoute("data", "data/{database}/{collection}/{id}",
                    new {
                        controller = "data",
                        database = "db",
                        collection = "col",
                        id = UrlParameter.Optional
                    });

                config.Routes.MapHttpRoute("api", "api/{controller}");
            });
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}