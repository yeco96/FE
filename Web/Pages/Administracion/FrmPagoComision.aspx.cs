using Class.Utilidades;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using WSDomain;
using XMLDomain;


namespace Web.Pages.Administracion
{
    public partial class FrmPagoComision : System.Web.UI.Page
    { 
        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.alertMessages.Attributes["class"] = "";
            this.alertMessages.InnerText = "";
            this.AsyncMode = true;
            try
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Pages/Login.aspx");
                }
                if (!IsCallback && !IsPostBack)
                {
                    this.txtFechaInicio.Date = DateTime.Today;
                    this.txtFechaFin.Date = Date.DateTimeNow();
                }
                this.txtPagoComision1.Enabled = false;
                this.txtPagoComision2.Enabled = false;
                this.refreshData();
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
        }

        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            double valor = 0;
            using (var conexion = new DataModelFE())
            {
                string emisor = Session["emisor"].ToString();
                List<Comision> lista = (from comision in conexion.Comision
                                        where comision.fechaPago >= txtFechaInicio.Date
                                                && comision.fechaPago <= txtFechaFin.Date
                                        select comision
                                                 ).ToList();

                foreach (var item in lista)
                {
                    valor += item.montoPago;
                }

                txtPagoComision1.Text = (valor / 4).ToString();
                txtPagoComision2.Text = (valor / 4).ToString();
                this.dgvDatos.DataSource = lista;
                this.dgvDatos.DataBind();
            }
        }


        protected void btnReporte_Click(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            this.refreshData();
        }
    }
}