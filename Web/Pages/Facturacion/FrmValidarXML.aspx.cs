using Class.Utilidades;
using EncodeXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Web.Models.Facturacion;
using Web.WebServices;
using WSDomain;
using XMLDomain;

namespace Web.Pages.Facturacion
{
    public partial class FrmValidarXML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.AsyncMode = true;
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

        protected void xmlUploadControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            try
            {
                string file = Convert.ToBase64String(e.UploadedFile.FileBytes);
                Session["xmlFileValidar"] = EncondeXML.base64Decode(file);

                this.alertMessages.Attributes["class"] = "alert alert-info";
                this.alertMessages.InnerText = "Los datos fueron cargados correctamente!!!";
                 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                string xml = Session["xmlFileValidar"].ToString();
                txtClave.Text = EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(xml), "Clave", xml);
                //Emisor
                string emisorIdentificacion = EncondeXML.buscarValorEtiquetaXML("Emisor", "Identificacion", xml); 
                txtNumCedEmisor.Text = emisorIdentificacion.Substring(2);
                txtFechaEmisor.Text = EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(xml), "FechaEmision", xml);
                //Factura
                double totalImpuesto = Convert.ToDouble(EncondeXML.buscarValorEtiquetaXML("ResumenFactura", "TotalImpuesto", xml));
                txtMontoTotalImpuesto.Text =  totalImpuesto.ToString("N2");
                double totalFactura = Convert.ToDouble(EncondeXML.buscarValorEtiquetaXML("ResumenFactura", "TotalComprobante", xml));
                txtTotalFactura.Text = totalFactura.ToString("N2"); ;

                //Receptor
                string receptorIdentificacion = EncondeXML.buscarValorEtiquetaXML("Receptor", "Identificacion", xml);
                Session["receptor.tipoIdentificacion"] = receptorIdentificacion.Substring(0,2);
                Session["receptor.CorreoElectronico"] = EncondeXML.buscarValorEtiquetaXML("Receptor", "CorreoElectronico", xml);
                Session["receptor.Nombre"] = EncondeXML.buscarValorEtiquetaXML("Receptor", "Nombre", xml);
                txtNumCedReceptor.Text = receptorIdentificacion.Substring(2);
                

                this.alertMessages.Attributes["class"] = "alert alert-info";
                this.alertMessages.InnerText = "Los datos fueron cargados correctamente!!!";

            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex.Message);
            }
        }

        protected async void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtNumConsecutivoReceptor.Text))
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "Número de consecutivo requerido";
                    return;
                }
                 
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                MensajeReceptor dato = new MensajeReceptor();
                dato.clave = this.txtClave.Text;

                dato.numeroCedulaEmisor = this.txtNumCedEmisor.Text;
                dato.numeroCedulaReceptor = this.txtNumCedReceptor.Text;

                dato.mensajeDetalle = this.txtDetalleMensaje.Text;
                dato.mensaje = int.Parse(this.cmbMensaje.Value.ToString());
                dato.numeroConsecutivoReceptor = this.txtNumConsecutivoReceptor.Text;

                dato.montoTotalImpuesto = decimal.Parse(this.txtMontoTotalImpuesto.Text);
                dato.montoTotalFactura = decimal.Parse(this.txtTotalFactura.Text);
                 
                EmisorReceptorIMEC elEmisor = (EmisorReceptorIMEC) Session["emisor"];
                string xml = EncodeXML.EncondeXML.getXMLFromObject(dato);
                //string xmlSigned = FirmaXML.getXMLFirmadoWeb(xml, elEmisor.llaveCriptografica, elEmisor.claveLlaveCriptografica);
                string responsePost = await Services.enviarMensajeReceptor(xml, elEmisor, Session["receptor.tipoIdentificacion"].ToString() );
                
                if (responsePost.Equals("Success"))
                {
                    this.alertMessages.Attributes["class"] = "alert alert-info";
                    this.alertMessages.InnerText = "Los datos fueron enviados correctamente!!!";

                    string correo = Session["receptor.CorreoElectronico"].ToString(); 
                    if (!string.IsNullOrWhiteSpace(correo))
                    {
                        string nombre = Session["receptor.Nombre"].ToString();

                        Utilidades.sendMail(correo,
                            string.Format("{0} - {1}", dato.clave.Substring(21,20), nombre),
                            Utilidades.mensageGenerico(), "Factura Electrónica", xml, dato.clave.Substring(21,20), dato.clave);
                    }
                }
                else
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = String.Format("Factura #{0} con errores.", dato.clave);
                }

            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex.Message);
            }
        }
    }
}