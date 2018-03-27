using Class.Utilidades;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using WSDomain;
using XMLDomain;

namespace Web.Pages.Reportes
{
    public partial class FrmReporteDocumentoResumen : System.Web.UI.Page
    {
        public FrmReporteDocumentoResumen()
        {
        }
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

        private XtraReport CreateReport()
        {
            XtraReport report1 = new XtraReport();
            XtraReport report2 = new XtraReport();

            List<XtraReport> oLista = new List<XtraReport>();
            XtraReport report = null;
            var pLista = (List<String>)Session["claves"];
            foreach (var item in pLista)
            {

                XtraReport reportP = new XtraReport();

                using (var conexion = new DataModelFE())
                {
                    WSRecepcionPOST dato = conexion.WSRecepcionPOST.Where(x => x.clave == item.ToString()).FirstOrDefault();
                    string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);

                    RptComprobante reportES = new RptComprobante();
                    RptComprobanteEN reportEN = new RptComprobanteEN();

                    DocumentoElectronico documento = (DocumentoElectronico)EncodeXML.EncondeXML.getObjetcFromXML(xml);
                    Empresa empresa = conexion.Empresa.Find(documento.emisor.identificacion.numero);

                    if (empresa != null && "EN".Equals(empresa.idioma))
                    {
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(documento, dato.mensaje, empresa);
                        reportEN.objectDataSource1.DataSource = dataSource;
                        string enviroment_url = ConfigurationManager.AppSettings["ENVIROMENT_URL"].ToString();
                        reportEN.xrBarCode1.Text = (enviroment_url + documento.clave).ToUpper();
                        if (empresa != null && empresa.logo != null)
                        {
                            reportEN.pbLogo.Image = UtilidadesReporte.byteArrayToImage(empresa.logo);
                        }

                        reportEN.CreateDocument();
                        reportP = reportEN;
                    }
                    else
                    {
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(documento, dato.mensaje, empresa);
                        reportES.objectDataSource1.DataSource = dataSource;
                        string enviroment_url = ConfigurationManager.AppSettings["ENVIROMENT_URL"].ToString();
                        reportES.xrBarCode1.Text = (enviroment_url + documento.clave).ToUpper();
                        if (empresa != null && empresa.logo != null)
                        {
                            reportES.pbLogo.Image = UtilidadesReporte.byteArrayToImage(empresa.logo);
                        }
                        reportES.CreateDocument();
                        reportP = reportES;
                    }
                    
                }
                oLista.Add(reportP);
                //if (oLista.Count > 1)
                //{
                //    reportP.DataSource = oLista;
                    
                //}
            }



            if (oLista.Count == 1)
            {
                report1 = oLista[0];
            }
            else
            {
                if (oLista.Count == 2)
                {
                    report1 = oLista[0];
                    report2 = oLista[1];
                    report1.Pages.AddRange(report2.Pages);
                    report1.PrintingSystem.ContinuousPageNumbering = true;
                }
                else
                {
                    for (int i = 0; i < oLista.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            report1 = oLista[i];
                        }
                        else {
                            report1 = report1;
                        }
                        
                        report2 = oLista[i+1];
                        report1.Pages.AddRange(report2.Pages);
                        report1.PrintingSystem.ContinuousPageNumbering = true;
                    }
                }
            }
            //XtraReport report1 = new XtraReport();
            //report1 = oLista[0];

            //XtraReport report2 = new XtraReport();
            //report2 = oLista[1];

            //report1.Pages.AddRange(report2.Pages);
            //report1.PrintingSystem.ContinuousPageNumbering = true;

            report = report1;
            return report;
        }
    
    }
}