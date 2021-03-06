﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.Reportes
{
    public partial class FrmReporteFacturaEN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            generarReporte();
        }

        /// <summary>
        /// Método para Generar el Reporte
        /// </summary>
        private void generarReporte()
        {
            //Toma el Document Viewer para WEB y se envía el método del reporte
            ASPxWebDocumentViewer1.OpenReport(CreateReport());

        }

        RptComprobanteEN CreateReport()
        {
            RptComprobanteEN report = new RptComprobanteEN();
            XMLDomain.Impresion fe = new XMLDomain.Impresion();
            //fe.iniciarParametros();
            object dataSource = fe;
            report.objectDataSource1.DataSource = dataSource;

            //EL MENSAJE SE DEBE DE ENVIAR SIEMPRE EN MAYUSCULA
            string variable = "WWW.GMAIL.COM";
            //El control debe estar como Público
            //report.codebar.Text = variable.ToUpper();
            //Se crea el documento

            report.CreateDocument();
            return report;
        }
    }
}