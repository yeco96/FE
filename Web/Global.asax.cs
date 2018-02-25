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
using System.Web.Routing;
using HighSchoolWeb.ScheduledTask;
using Web.Models;

namespace Web
{
    public class Global_asax : System.Web.HttpApplication
    {
        //static public System.Timers.Timer MyKillTimer = new System.Timers.Timer();
         
        void Application_Start(object sender, EventArgs e)
        {
            System.Web.Routing.RouteTable.Routes.MapPageRoute("defaultRoute", "", "~/Pages/Home.aspx");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DevExpress.Web.ASPxWebControl.CallbackError += new EventHandler(Application_Error);

           GlobalConfiguration.Configure(WebApiConfig.Register);

           JobScheduler.Start();


            //MyKillTimer.Interval = 60000; // check sleeping connections every 1 minute
            //MyKillTimer.Elapsed += new System.Timers.ElapsedEventHandler(MyKillTimer_Event);
            //MyKillTimer.AutoReset = true;
            //MyKillTimer.Enabled = true;
        }

        private void MyKillTimer_Event(object source, System.Timers.ElapsedEventArgs e)
        {
          //  DataModelFE.KillSleepingConnections(60);
        }

        void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ASPxGridView))
            {
                if( ((ASPxGridView)sender).AppRelativeTemplateSourceDirectory.Contains("Catalogo"))
                {
                    // el erro lo maneja el grid
                    return;
                }else{
                    Server.Transfer("/Pages/Error/DefaultRedirectErrorPage.aspx");
                }
            }
            else
            {
                Server.Transfer("/Pages/Error/DefaultRedirectErrorPage.aspx");
            }
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
            FormsAuthentication.SignOut();
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false))
            {
                if(!Request.ServerVariables["HTTP_HOST"].Contains("des.fe.msasoft.net"))
                    Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
            }
        }
    }
}