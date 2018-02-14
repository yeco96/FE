using Class.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;

namespace Web.Pages.Facturacion
{
    public partial class FrmConsultaResumen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.alertMessages.Attributes["class"] = "";
            this.alertMessages.InnerText = "";
            this.AsyncMode = true;

            try
            {
                if (!IsCallback && !IsPostBack)
                {
                }
                this.refreshData();
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex.Message);
            }

        }

        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            using (var conexion = new DataModelFE())
            {
                //this.ASPxGridView1.DataSource = conexion.WSRecepcionPOST.Where(x => x.fecha >= txtFechaInicio.Date && x.fecha <= txtFechaFin.Date).OrderByDescending(x => x.fecha).ToList();
                this.ASPxGridView1.DataSource = conexion.ResumenFactura.Where(x => x.clave == "50611021800060354097400100001010000000095188888888").OrderByDescending(x => x.clave).ToList();
                this.ASPxGridView1.DataBind();
            }
        }
    }
}