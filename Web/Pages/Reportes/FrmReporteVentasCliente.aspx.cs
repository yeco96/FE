﻿using Class.Seguridad;
using Class.Utilidades;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using EncodeXML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;
using Web.WebServices;
using WSDomain;
using XMLDomain;

namespace Web.Pages.Reportes
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmReporteVentasCliente : System.Web.UI.Page
    {
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
                    //Cargar Combos
                    this.cargarCombos();
                }
                //this.refreshData();
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
        }

        protected void txtConsultar_Click(object sender, EventArgs e)
        {
            //Genera el reporte
            ASPxWebDocumentViewer1.OpenReport(CreateReport(cmbTipoResumen.Value.ToString()));
        }


        public void cargarCombos()
        {
            cmbTipoResumen.Items.Clear();
            cmbTipoResumen.Items.Add("DETALLADO", "D");
            cmbTipoResumen.Items.Add("RESUMEN", "R");
            cmbTipoResumen.SelectedIndex = 0;
        }

        XtraReport CreateReport(string pTipo)
        {
            XtraReport report = null;
            string emisor = Session["emisor"].ToString();

            if (pTipo == "D")
            {
                RptVentaClientesEmisor reporte = new RptVentaClientesEmisor();
                reporte.pEmisor.Value = emisor;
                reporte.pFechaInicio.Value = txtFechaInicio.Text.Replace("/", "");
                reporte.pFechaFin.Value = txtFechaFin.Text.Replace("/", "");
                reporte.pTipo.Value = cmbTipoResumen.Value;

                reporte.CreateDocument();
                report = reporte;
            }
            else {
                RptVentaClientesEmisorResumen reporte = new RptVentaClientesEmisorResumen();
                reporte.pEmisor.Value = emisor;
                reporte.pFechaInicio.Value = txtFechaInicio.Text.Replace("/", "");
                reporte.pFechaFin.Value = txtFechaFin.Text.Replace("/", "");
                reporte.pTipo.Value = cmbTipoResumen.Value;

                reporte.CreateDocument();
                report = reporte;
            }
            return report;
        }
    }
}