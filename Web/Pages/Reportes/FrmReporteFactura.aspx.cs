using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.Reportes
{
    public partial class FrmReporteFactura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            generarReporte();
        }
        private void generarReporte()
        {
            //ASPxWebDocumentViewer1.Report = CreateReport();
            //ASPxWebDocumentViewer1.ReportTypeName = "Web.Reportes.RptFactura";
            ASPxWebDocumentViewer1.OpenReport(CreateReport());

        }

        RptFactura CreateReport()
        {
            RptFactura report = new RptFactura();
            XMLDomain.FacturaElectronica fe = new XMLDomain.FacturaElectronica();
            fe.iniciarParametros();
            object dataSource = fe;
            report.objectDataSource1.DataSource = dataSource;
            report.codebar.Text = "WWW.GMAIL.COM";
            report.CreateDocument();
           return report;
        }
    }
}