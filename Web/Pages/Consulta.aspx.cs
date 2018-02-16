using Class.Utilidades;
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
    public partial class Consulta : System.Web.UI.Page
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

        RptComprobante CreateReport(string clave)
        {
            using (var conexion = new DataModelFE())
            {
                WSRecepcionPOST dato = conexion.WSRecepcionPOST.Where(x => x.clave == clave).FirstOrDefault();
                string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);

                RptComprobante report = new RptComprobante();
                 
                DocumentoElectronico documento = (DocumentoElectronico)EncodeXML.EncondeXML.getObjetcFromXML(xml);
                object dataSource = UtilidadesReporte.cargarObjetoImpresion(documento, dato.mensaje);
                report.objectDataSource1.DataSource = dataSource;
                string enviroment_url = ConfigurationManager.AppSettings["ENVIROMENT_URL"].ToString();
                report.xrBarCode1.Text = (enviroment_url + documento.clave).ToUpper();
                report.CreateDocument();
                   
                return report;
            }
        }
    }
}