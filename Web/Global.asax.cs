using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DevExpress.Web;
using Web.Pages.Error;
using System.Web.Http;
using Web.App_Start;

namespace Web
{
    public class Global_asax : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            System.Web.Routing.RouteTable.Routes.MapPageRoute("defaultRoute", "", "~/Pages/Home.aspx");
            DevExpress.Web.ASPxWebControl.CallbackError += new EventHandler(Application_Error);

           GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            //Server.Transfer("/Pages/Error/DefaultRedirectErrorPage.aspx");
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}