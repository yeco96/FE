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
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Pages/Login.aspx");
                }
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
                GridViewDataComboBoxColumn comboTipoDocumento = this.dgvDatos.Columns["tipoDocumento"] as GridViewDataComboBoxColumn;
                foreach (var item in conexion.TipoDocumento.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    comboTipoDocumento.PropertiesComboBox.Items.Add(item.descripcion.Replace("ELECTRÓNICA", ""), item.codigo);
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
                List<String> claves = new List<string>();
                string emisor = Session["emisor"].ToString();
                List<ResumenFactura> lista = (from resumenFactura in conexion.ResumenFactura
                                                 from recepcioDocumento in conexion.WSRecepcionPOST
                                                 where recepcioDocumento.clave == resumenFactura.clave 
                                                 && recepcioDocumento.emisorIdentificacion == emisor
                                                 && recepcioDocumento.fecha >= txtFechaInicio.Date 
                                                 && recepcioDocumento.fecha <= txtFechaFin.Date
                                                 && recepcioDocumento.indEstado == 1
                                                 select resumenFactura
                                                 ).ToList();


                foreach (var item in lista)
                {
                    item.verificaTipoDocumentoCambioMoneda(this.chkCambioMoneda.Checked);
                    claves.Add(item.clave);
                }
                this.dgvDatos.DataSource = lista;

                this.dgvDatos.DataBind();
                Session["claves"] = claves;
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            this.refreshData();
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            int cuenta = 0;
            var pLista = (List<String>)Session["claves"];
            foreach (var item in pLista)
            {
                cuenta++;
            }

                if (cuenta > 0)
            {
                Response.Redirect("~/Pages/Reportes/FrmReporteDocumentoResumen.aspx");
            }
        }
    }
}