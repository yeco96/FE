﻿using Class.Utilidades;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
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
        [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
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
                //List<String> claves = new List<string>();
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
                    //claves.Add(item.clave);
                }
                this.dgvDatos.DataSource = lista;

                this.dgvDatos.DataBind();
                //Session["claves"] = claves;
            }
        }

        protected void exportarPDF_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.WritePdfToResponse();
        }

        protected void exportarXLS_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.WriteXlsToResponse();
        }

        protected void exportarXLSX_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void exportarCSV_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.WriteCsvToResponse();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            this.refreshData();
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            int cuenta = 0;
            List<String> claves = new List<string>();
            //var pLista = (List<String>)Session["claves"];
            //foreach (var item in pLista)
            //{
            //    cuenta++;
            //}

            //Recorremos Grid View

            //for (int i = 0; i < dgvDatos.; i++)
            //{

            //}
            dgvDatos.SettingsPager.Mode= GridViewPagerMode.ShowAllRecords;

            foreach (var item in dgvDatos.GetCurrentPageRowValues("clave"))
            {
                if (dgvDatos.Selection.IsRowSelectedByKey(item))
                {
                    cuenta++;
                    claves.Add(item.ToString());
                }
            }

            dgvDatos.SettingsPager.Mode = GridViewPagerMode.ShowPager;

            if (cuenta > 0)
            {
                Session["claves"] = claves;
                Response.Redirect("~/Pages/Reportes/FrmReporteDocumentoResumen.aspx");
            }
            else {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = "No hay documentos que procesar!!!";
            }
        }
    }
}