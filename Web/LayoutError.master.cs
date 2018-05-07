using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using Class.Utilidades;
using System.Threading;
using Web.Models;
using Web.Models.Facturacion;
using System.Web.Security;
using Class.Seguridad;

namespace Web {
    public partial class LayoutError : System.Web.UI.MasterPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
              
        }

        protected void HeadLoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut(); 
            Response.Redirect("~/Pages/Seguridad/FrmLogin.aspx");
        }
    }
}