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

namespace Web.Pages.Facturacion
{
    public partial class FrmConsultaResumen : System.Web.UI.Page
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
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
                    this.txtFechaInicio.Date = DateTime.Today;
                    this.txtFechaFin.Date = Date.DateTimeNow();
                    this.loadComboBox();
                }
                this.refreshData();
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }

        }

        /// <summary>
        /// carga solo una vez para ahorar tiempo 
        /// </summary>
        private void loadComboBox()
        {
            using (var conexion = new DataModelFE())
            {
                /* TIPO DOCUMENTO */
                GridViewDataComboBoxColumn comboTipoDocumento = this.ASPxGridView1.Columns["tipoDocumento"] as GridViewDataComboBoxColumn;
                foreach (var item in conexion.TipoDocumento.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    comboTipoDocumento.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }


            }
        }

        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            using (var conexion = new DataModelFE())
            {
                string emisor = Session["emisor"].ToString();
                this.ASPxGridView1.DataSource = (from resumenFactura in conexion.ResumenFactura
                                                 from recepcioDocumento in conexion.WSRecepcionPOST
                                                 where recepcioDocumento.clave == resumenFactura.clave 
                                                 && recepcioDocumento.emisorIdentificacion == emisor
                                                 && recepcioDocumento.fecha >= txtFechaInicio.Date 
                                                 && recepcioDocumento.fecha <= txtFechaFin.Date
                                                 && recepcioDocumento.indEstado == 1
                                                 select resumenFactura
                                                 ).ToList();
                
                this.ASPxGridView1.DataBind();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            this.refreshData();
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Reportes/FrmReporteDocumentoResumen.aspx");
        }
    }
}