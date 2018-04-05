using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Facturacion;

namespace Web.Pages
{
    [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
    public partial class SeleccionarEmisor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                /* EMISORES POR SUPERVISOR */
                string supervisorUser = Session["usuario"].ToString();
                this.ASPxGridViewClientes.DataSource = (from Emisor in conexion.EmisorReceptorIMEC
                                                        from supervisor in conexion.Supervisor
                                                        where Emisor.identificacion == supervisor.emisor && supervisor.supervisor == supervisorUser
                                                        select Emisor
                                             ).ToList();
                conexion.EmisorReceptorIMEC.ToList();
                this.ASPxGridViewClientes.DataBind();
            }
        }

        protected void ASPxGridViewClientes_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as ASPxGridView).GetSelectedFieldValues("identificacion").Count > 0)
            {
                string identificacion = (sender as ASPxGridView).GetSelectedFieldValues("identificacion")[0].ToString();
                using (var conexion = new DataModelFE())
                { 
                    EmisorReceptorIMEC emisor = conexion.EmisorReceptorIMEC.Find(identificacion);
                    //Session["usuario"] = emisor.identificacion;
                    //Session["elUsuario"] = conexion.Usuario.Find(emisor.identificacion);
                    Session["emisor"] = emisor.identificacion;
                    Session["elEmisor"] = emisor;
                    Response.Redirect("~/");
                }
                 
            }
        }
    }
}