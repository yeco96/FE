using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.SessionState;

namespace WebServices
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }
}
