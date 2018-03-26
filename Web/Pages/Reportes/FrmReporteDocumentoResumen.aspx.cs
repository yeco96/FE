using Class.Utilidades;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        private List<XtraReport> CreateData()
        {
            List<XtraReport> data = new List<XtraReport>();
            var pLista = (List<String>)Session["claves"];
            foreach (var item in pLista)
            {
                
                //data.Add(CreateReport(item.ToString()));
            }
                return data;
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
            List<XtraReport> oLista = new List<XtraReport>();
            XtraReport report = null;
            var pLista = (List<String>)Session["claves"];
            foreach (var item in pLista)
            {
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
                        report = reportEN;
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
                        report = reportES;
                    }
                }
                oLista.Add(report);
                report.DataSource = oLista;
            }
            
            return report;
            //return oLista;
        }

        //RptDocumentoResumen CreateReport()
        //{
        //    try
        //    {

        //        RptDocumentoResumen report = new RptDocumentoResumen();
        //        //XMLDomain.Documento fe = new XMLDomain.Documento();
        //        //fe.iniciarParametros();
        //        object dataSource = null;
        //        report.objectDataSource1.DataSource = dataSource;

        //        report.CreateDocument();
        //        return report;
        //    }
        //    catch (Exception er)
        //    {

        //        throw;
        //    }
        //}

    }
}