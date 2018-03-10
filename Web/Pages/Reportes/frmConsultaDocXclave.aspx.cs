using Class.Utilidades;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using WSDomain;
using XMLDomain;

namespace Web.Pages.Reportes
{
    public partial class frmConsultaDocXclave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.AsyncMode = true;
        }

        XtraReport CreateReport(string clave)
        {
            XtraReport report = null;
            using (var conexion = new DataModelFE())
            {
                WSRecepcionPOST dato = conexion.WSRecepcionPOST.Where(x => x.clave == clave).FirstOrDefault();
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
                    reportEN.CreateDocument();
                    report = reportEN;
                }
                else
                {
                    object dataSource = UtilidadesReporte.cargarObjetoImpresion(documento, dato.mensaje, empresa);
                    reportES.objectDataSource1.DataSource = dataSource;
                    string enviroment_url = ConfigurationManager.AppSettings["ENVIROMENT_URL"].ToString();
                    reportES.xrBarCode1.Text = (enviroment_url + documento.clave).ToUpper();
                    reportES.CreateDocument();
                    report = reportES;
                }
            }
            return report;
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ASPxWebDocumentViewer1.OpenReport(CreateReport(txtClave.Text));
        }
    }
}