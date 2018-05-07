using Class.Utilidades;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using WSDomain;
using XMLDomain;

namespace Web.Pages
{
    public partial class ConsultaProforma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.AsyncMode = true;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (url.Contains('/'))
            {
                string[] valor = url.Split('/');
                string dato = valor[valor.Length - 1];
                if (dato.Length == 50)
                {

                    ASPxWebDocumentViewer1.OpenReport(CreateReport(dato));
                }
            }
        }

        XtraReport CreateReport(string clave)
        {
            XtraReport report = null;
            using (var conexion = new DataModelFE())
            {
                WSRecepcionPOSTProforma dato = conexion.WSRecepcionPOSTProforma.Where(x => x.clave == clave).FirstOrDefault();
                if (dato != null)
                {

                    string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);

                    //Revertimos lo de la proforma
                    xml = xml.Replace("FacturaElectronica", "ProformaElectronica");


                    RptComprobanteProformas reportES = new RptComprobanteProformas();
                    //Crear Proforma en Inglés
                    RptComprobanteProformasEN reportEN = new RptComprobanteProformasEN();

                    ProformaElectronica documento = (ProformaElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml);
                    Empresa empresa = conexion.Empresa.Find(documento.emisor.identificacion.numero);

                    if (empresa != null && "EN".Equals(empresa.idioma))
                    {
                        object dataSource = UtilidadesReporte.cargarObjetoImpresionProforma(documento, dato.mensaje, empresa);
                        reportEN.objectDataSource1.DataSource = dataSource;
                        string enviroment_url = ConfigurationManager.AppSettings["ENVIROMENT_URL_PROFORMA"].ToString();
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
                        object dataSource = UtilidadesReporte.cargarObjetoImpresionProforma(documento, dato.mensaje, empresa);
                        reportES.objectDataSource1.DataSource = dataSource;
                        string enviroment_url = ConfigurationManager.AppSettings["ENVIROMENT_URL_PROFORMA"].ToString();
                        reportES.xrBarCode1.Text = (enviroment_url + documento.clave).ToUpper();
                        if (empresa != null && empresa.logo != null)
                        {
                            reportES.pbLogo.Image = UtilidadesReporte.byteArrayToImage(empresa.logo);
                        }

                        reportES.CreateDocument();
                        report = reportES;
                    }
                }
            }
            return report;
        }
    }
}