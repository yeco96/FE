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

namespace Web {
    public partial class Layout : System.Web.UI.MasterPage
    {
        bool showSearch = true;
        public bool ShowSearch { get { return showSearch; } set { showSearch = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                /*
                if (!Request.Url.Segments[Request.Url.Segments.Length - 1].Contains("DefaultRedirectErrorPage"))
                {
                    if (Session["usuario"] == null)
                    {
                        Response.Redirect("~/Pages/Seguridad/Login.aspx");
                    }
                }*/
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
               // Response.Redirect("~/Pages/Error/DefaultRedirectErrorPage.aspx");
            }


            /*
            Page.Header.DataBind();
            SearchBlock.Visible = ShowSearch;
            if(IsPostBack && hfAction.Contains("search")) {
                Session["query"] = Search.Text;
                Response.Redirect("~/Pages/Search.aspx");
            }*/
        }

        protected void HeadLoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut(); 
            Response.Redirect("~/Pages/Seguridad/FrmLogin.aspx");

        }
    }
}