using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.Reportes
{
    public partial class FrmReporteDocumentoResumen : System.Web.UI.Page
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

        RptDocumentoResumen CreateReport()
        {
            RptDocumentoResumen report = new RptDocumentoResumen();
            XMLDomain.ResumenFactura fe = new XMLDomain.ResumenFactura();
            //fe.iniciarParametros();
            object dataSource = fe;
            report.objectDataSource1.DataSource = dataSource;

            report.CreateDocument();
            return report;
        }

    }
}