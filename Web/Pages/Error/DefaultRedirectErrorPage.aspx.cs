using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.Error
{
    public partial class DefaultRedirectErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the last error from the server
            Exception ex = Server.GetLastError();

            // Create a safe message
            string safeMsg = "A problem has occurred in the web site. ";

            // Show Inner Exception fields for local access
            if (ex!=null && ex.InnerException != null)
            {
                innerTrace.Text = ex.InnerException.StackTrace;
                InnerErrorPanel.Visible = Request.IsLocal;
                innerMessage.Text = ex.InnerException.Message;
            }
            // Show Trace for local access
            if (Request.IsLocal)
                exTrace.Visible = true;
            else
                ex = new Exception(safeMsg, ex);

            // Fill the page fields
            if (ex != null)
            {
                exMessage.Text = ex.Message;
                exTrace.Text = ex.StackTrace;

                // Log the exception and notify system operators
                ExceptionUtility.LogException(ex, "Generic Error Page");
                ExceptionUtility.NotifySystemOps(ex);
            }
            else
            {
                if (Session["error"] != null){ 
                    exMessage.Text = Session["error"].ToString();
                }
            }

           

            // Clear the error from the server
           // Server.ClearError();
        }
    }
}