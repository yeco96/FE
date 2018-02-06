using Class.Utilidades;
using System;
using System.Collections.Generic;
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

namespace Web.Pages.Facturacion
{
    public partial class FrmConsulta : System.Web.UI.Page
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
                    //Se llama el método del botón
                    ASPxWebDocumentViewer1.OpenReport(CreateReport(dato));
                }
            }
        }

        protected void UpdatePanel_Unload(object sender, EventArgs e)
        {
            RegisterUpdatePanel((UpdatePanel)sender);
        }
        protected void RegisterUpdatePanel(UpdatePanel panel)
        {
            var sType = typeof(ScriptManager);
            var mInfo = sType.GetMethod("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel", BindingFlags.NonPublic | BindingFlags.Instance);
            if (mInfo != null)
                mInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { panel });
        }

        //
        RptFacturacionElectronica CreateReport(string clave)
        {
            using (var conexion = new DataModelWS())
            {
                WSRecepcionPOST dato = conexion.WSRecepcionPOST.Where(x => x.clave == clave).FirstOrDefault();
                string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);


                RptFacturacionElectronica report = new RptFacturacionElectronica();

                if (TipoDocumento.FACTURA_ELECTRONICA.Equals(dato.tipoDocumento))
                    {
                        FacturaElectronica factura = (FacturaElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(FacturaElectronica));
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(factura, dato.mensaje);
                        report.objectDataSource1.DataSource = dataSource;
                        report.xrBarCode1.Text = ("http://localhost:54762/Pages/Facturacion/FrmConsulta.aspx/" + factura.clave).ToUpper();
                        report.CreateDocument();
                    }

                    if (TipoDocumento.NOTA_CREDITO.Equals(dato.tipoDocumento))
                    {
                        NotaCreditoElectronica notaCredito = (NotaCreditoElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(NotaCreditoElectronica));
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(notaCredito, dato.mensaje);
                        report.objectDataSource1.DataSource = dataSource;
                        report.xrBarCode1.Text = ("http://localhost:54762/Pages/Facturacion/FrmConsulta.aspx/" + notaCredito.clave).ToUpper();
                        report.CreateDocument();
                    }

                    if (TipoDocumento.NOTA_DEBITO.Equals(dato.tipoDocumento))
                    {
                        NotaDebitoElectronica notaDebito = (NotaDebitoElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(NotaDebitoElectronica));
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(notaDebito, dato.mensaje);
                        report.objectDataSource1.DataSource = dataSource;
                        report.xrBarCode1.Text = ("http://localhost:54762/Pages/Facturacion/FrmConsulta.aspx/" + notaDebito.clave).ToUpper();
                        report.CreateDocument();
                    }

                    if (TipoDocumento.TIQUETE_ELECTRONICO.Equals(dato.tipoDocumento))
                    {
                        TiqueteElectronico notaDebito = (TiqueteElectronico)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(TiqueteElectronico));
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(notaDebito, dato.mensaje);
                        report.objectDataSource1.DataSource = dataSource;
                        report.xrBarCode1.Text = ("http://localhost:54762/Pages/Facturacion/FrmConsulta.aspx/"+notaDebito.clave).ToUpper();
                        report.CreateDocument();
                    }

                return report;
            }
        }
    }
}