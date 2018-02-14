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
                this.NavMenu.Visible = true;
                this.NavMenuAdmin.Visible = false;

                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                
                if (Request.Url.ToString().Contains("Facturacion") ||
                    Request.Url.ToString().Contains("Catalogos") ||
                    Request.Url.ToString().Contains("Seguridad") ||
                    Request.Url.ToString().Contains("Reportes"))
                {
                    if (Session["usuario"] == null)
                    { 
                        if (!Request.Url.ToString().Contains("Login")) {
                            Session.RemoveAll();
                            FormsAuthentication.SignOut();
                            Response.Redirect("~/Pages/Login.aspx");
                        }
                    } 
                }


                if (Session["usuario"] != null)
                { 
                    this.NavMenu.Visible = false;
                    this.NavMenuAdmin.Visible = true;
                }
                else
                {
                    FormsAuthentication.SignOut();
                }
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