using Class.Utilidades;
using EncodeXML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;
using Web.WebServices;
using WSDomain;
using XMLDomain;

namespace Web.Pages.Facturacion
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmValidarXML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.AsyncMode = true;

            if (!IsCallback && !IsPostBack)
            {

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

        protected void xmlUploadControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            try
            {
                string file = Convert.ToBase64String(e.UploadedFile.FileBytes);

                Session["xmlFileValidar"] = XMLUtils.base64Decode(file);

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

                if(string.IsNullOrWhiteSpace( XMLUtils.buscarValorEtiquetaXML("Receptor", "Identificacion", xml)))
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "XML no tiene etiqueta de <Identificacion>, no se puede validar el receptor del XML!!!";
                    return;
                }

                if (XMLUtils.buscarValorEtiquetaXML("Receptor", "Identificacion", xml).Substring(2) == Session["emisor"].ToString())
                {
                    txtClave.Text = XMLUtils.buscarValorEtiquetaXML(XMLUtils.tipoDocumentoXML(xml), "Clave", xml);
                    //Emisor
                    string emisorIdentificacion = XMLUtils.buscarValorEtiquetaXML("Emisor", "Identificacion", xml);
                    txtNumCedEmisor.Text = emisorIdentificacion.Substring(2);
                    txtFechaEmisor.Text = XMLUtils.buscarValorEtiquetaXML(XMLUtils.tipoDocumentoXML(xml), "FechaEmision", xml);
                      

                    //Factura
                    double totalImpuesto = Convert.ToDouble(XMLUtils.buscarValorEtiquetaXML("ResumenFactura", "TotalImpuesto", xml));
                    txtMontoTotalImpuesto.Text = totalImpuesto.ToString("N2");
                    double totalFactura = Convert.ToDouble(XMLUtils.buscarValorEtiquetaXML("ResumenFactura", "TotalComprobante", xml));
                    txtTotalFactura.Text = totalFactura.ToString("N2"); ;

                    //Receptor
                    string receptorIdentificacion = XMLUtils.buscarValorEtiquetaXML("Receptor", "Identificacion", xml);
                    if (!string.IsNullOrWhiteSpace(receptorIdentificacion))
                    {
                        Session["receptor.tipoIdentificacion"] = receptorIdentificacion.Substring(0, 2);
                        txtNumCedReceptor.Text = receptorIdentificacion.Substring(2);
                    }
                    else
                    {
                        Session["receptor.tipoIdentificacion"] = "99";
                        txtNumCedReceptor.Text = XMLUtils.buscarValorEtiquetaXML("Receptor", "IdentificacionExtranjero", xml);

                    }
                    Session["receptor.CorreoElectronico"] = XMLUtils.buscarValorEtiquetaXML("Receptor", "CorreoElectronico", xml);
                    Session["receptor.Nombre"] = XMLUtils.buscarValorEtiquetaXML("Receptor", "Nombre", xml);

                    /*CONSECUTIVO*/
                    this.obtenerConsecutivo();

                    this.alertMessages.Attributes["class"] = "alert alert-info";
                    this.alertMessages.InnerText = "Los datos fueron cargados correctamente!!!";
                }
                else
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "El receptor del XML seleccionado no corresponde con la identificación del usuario!!!";
                    xmlUploadControl1 = null;
                    return;
                }

            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
        }

        /// <summary>
        /// consulta el consecutivo actual
        /// </summary>
        protected void obtenerConsecutivo()
        {
            //genera el consecutivo del documento
            string emisor = Session["emisor"].ToString();
            string sucursal = ConsecutivoDocElectronico.DEFAULT_SUCURSAL; ;
            string caja = ConsecutivoDocElectronico.DEFAULT_CAJA;
            string tipoDocumento = "";

            if (this.cmbMensaje.Value.Equals(TipoConsecutivo.ACEPTADO.ToString()))
            {
                tipoDocumento = TipoConsecutivo.DOCUMENTO_ACEPTADO;
            }
            else
            {
                if (this.cmbMensaje.Value.Equals(TipoConsecutivo.RECHAZADO_PARCIAL.ToString()))
                {
                    tipoDocumento = TipoConsecutivo.DOCUMENTO_RECHAZADO_PARCIAL;
                }
                else
                {
                    tipoDocumento = TipoConsecutivo.DOCUMENTO_RECHAZADO;
                }
            }
             
            using (var conexion = new DataModelFE())
            {
                object[] key = new object[] { emisor, sucursal, caja, tipoDocumento };
                ConsecutivoDocElectronico consecutivo = conexion.ConsecutivoDocElectronico.Find(key);
                if (consecutivo != null)
                {
                    this.txtNumConsecutivoReceptor.Text = consecutivo.getConsecutivo();
                }
                else
                {
                    consecutivo = new ConsecutivoDocElectronico();
                    consecutivo.sucursal = ConsecutivoDocElectronico.DEFAULT_SUCURSAL;
                    consecutivo.caja = ConsecutivoDocElectronico.DEFAULT_CAJA;
                    consecutivo.digitoVerificador = ConsecutivoDocElectronico.DEFAULT_DIGITO_VERIFICADOR;
                    consecutivo.emisor = emisor;
                    consecutivo.tipoDocumento = tipoDocumento;
                    consecutivo.consecutivo = 1;
                    consecutivo.estado = Estado.ACTIVO.ToString();
                    consecutivo.fechaCreacion = Date.DateTimeNow(); 
                    conexion.ConsecutivoDocElectronico.Add(consecutivo);
                    conexion.SaveChangesAsync();

                    this.txtNumConsecutivoReceptor.Text = consecutivo.getConsecutivo();

                }

            } 
        }

        /// <summary>
        /// guardar el consecutivo utilizado
        /// </summary>
        protected void actualizarConsecutivo()
        {
            //genera el consecutivo del documento
            string emisor = Session["emisor"].ToString();
            string sucursal = ConsecutivoDocElectronico.DEFAULT_SUCURSAL; ;
            string caja = ConsecutivoDocElectronico.DEFAULT_CAJA;
            string tipoDocumento = "";

            if (this.cmbMensaje.Value.Equals(TipoConsecutivo.ACEPTADO.ToString()))
            {
                tipoDocumento = TipoConsecutivo.DOCUMENTO_ACEPTADO;
            }
            else
            {
                if (this.cmbMensaje.Value.Equals(TipoConsecutivo.RECHAZADO_PARCIAL.ToString()))
                {
                    tipoDocumento = TipoConsecutivo.DOCUMENTO_RECHAZADO_PARCIAL;
                }
                else
                {
                    tipoDocumento = TipoConsecutivo.DOCUMENTO_RECHAZADO;
                }
            }

            using (var conexion = new DataModelFE())
            {
                object[] key = new object[] { emisor, sucursal, caja, tipoDocumento };
                ConsecutivoDocElectronico consecutivo = conexion.ConsecutivoDocElectronico.Find(key);
                if (consecutivo != null)
                {
                    consecutivo.consecutivo += 1;
                    conexion.Entry(consecutivo).State = EntityState.Modified;
                    conexion.SaveChanges(); 
                }

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
                dato.fechaEmisionDoc = txtFechaEmisor.Text;

                dato.numeroCedulaEmisor = this.txtNumCedEmisor.Text;
                dato.numeroCedulaReceptor = this.txtNumCedReceptor.Text;

                dato.mensajeDetalle = this.txtDetalleMensaje.Text;
                dato.mensaje = int.Parse(this.cmbMensaje.Value.ToString());
                dato.numeroConsecutivoReceptor = this.txtNumConsecutivoReceptor.Text;
                /*ACTUALIZA CONSECUTIVO*/
                this.actualizarConsecutivo();
                
                dato.montoTotalImpuesto = decimal.Parse(this.txtMontoTotalImpuesto.Text);
                dato.montoTotalFactura = decimal.Parse(this.txtTotalFactura.Text);

                EmisorReceptorIMEC elEmisor = (EmisorReceptorIMEC)Session["elEmisor"];
                string xml = EncodeXML.XMLUtils.getXMLFromObject(dato);
                //string xmlSigned = FirmaXML.getXMLFirmadoWeb(xml, elEmisor.llaveCriptografica, elEmisor.claveLlaveCriptografica);

                //Habilitar solo cuando funcione el servicio
                string responsePost = await Services.enviarMensajeReceptor(xml, elEmisor, Session["receptor.tipoIdentificacion"].ToString());
                
                if (responsePost.Equals("Success"))
                {
                    this.alertMessages.Attributes["class"] = "alert alert-info";
                    this.alertMessages.InnerText = "Los datos fueron enviados correctamente!!!";

                    this.btnEnviar.Enabled = false;
                    this.guardarDocumentoReceptor();

                    using (var conexion = new DataModelFE())
                    {
                        elEmisor.consecutivo += 1;
                        conexion.Entry(elEmisor).State = EntityState.Modified;
                        conexion.SaveChanges();
                        Session["elEmisor"] = elEmisor;
                    }
                    /*
                    string correo = Session["receptor.CorreoElectronico"].ToString();
                    if (!string.IsNullOrWhiteSpace(correo))
                    {
                        string nombre = Session["receptor.Nombre"].ToString();

                        Utilidades.sendMail(Session["emisor"].ToString(), correo,
                            string.Format("{0} - {1}", dato.clave.Substring(21, 20), nombre),
                            Utilidades.mensageGenerico(), "Factura Electrónica", xml, dato.clave.Substring(21, 20), dato.clave,null);
                    }*/
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
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
        }
        public void guardarDocumentoReceptor()
        {
            using (var conexion = new DataModelFE())
            {
                //Se guardan los datos en la tabla de ws_resumen_xml_receptor
                string xmlr = Session["xmlFileValidar"].ToString();
                DocumentoElectronico documento = (DocumentoElectronico)EncodeXML.XMLUtils.getObjetcFromXML(xmlr);
                ResumenFacturaReceptor datos = conexion.ResumenFacturaReceptor.Find(documento.clave);

                if (datos == null)
                {
                    datos = new ResumenFacturaReceptor();
                    datos.clave = documento.clave;
                    datos.tipoCambio = documento.resumenFactura.tipoCambio;
                    datos.codigoMoneda = documento.resumenFactura.codigoMoneda;
                    datos.totalServGravados = documento.resumenFactura.totalServGravados;
                    datos.totalServExentos = documento.resumenFactura.totalServExentos;
                    datos.totalMercanciasGravadas = documento.resumenFactura.totalMercanciasGravadas;
                    datos.totalMercanciasExentas = documento.resumenFactura.totalMercanciasExentas;
                    datos.totalGravado = documento.resumenFactura.totalGravado;
                    datos.totalExento = documento.resumenFactura.totalExento;
                    datos.totalVenta = documento.resumenFactura.totalVenta;
                    datos.totalDescuentos = documento.resumenFactura.totalDescuentos;
                    datos.totalVentaNeta = documento.resumenFactura.totalVentaNeta;
                    datos.totalImpuesto = documento.resumenFactura.totalImpuesto;
                    datos.totalComprobante = documento.resumenFactura.totalComprobante;
                    conexion.ResumenFacturaReceptor.Add(datos);

                    //Guardar la información en ws_recepcion_documento_receptor
                    WSRecepcionPOSTReceptor datosReceptor = new WSRecepcionPOSTReceptor();
                    datosReceptor.clave = datos.clave;
                    //Emisor
                    string emisorIdentificacion = XMLUtils.buscarValorEtiquetaXML("Emisor", "Identificacion", xmlr);
                    datosReceptor.emisorIdentificacion = emisorIdentificacion.Substring(2);
                    datosReceptor.emisorTipo = emisorIdentificacion.Substring(0, 2);
                    //Receptor
                    string receptorIdentificacion = XMLUtils.buscarValorEtiquetaXML("Receptor", "Identificacion", xmlr);
                    if (!string.IsNullOrWhiteSpace(receptorIdentificacion))
                    {
                        datosReceptor.receptorIdentificacion = receptorIdentificacion.Substring(2);
                        datosReceptor.receptorTipo = receptorIdentificacion.Substring(0, 2);
                    }
                    else
                    {
                        datosReceptor.receptorTipo = "99";
                        datosReceptor.receptorIdentificacion = XMLUtils.buscarValorEtiquetaXML("Receptor", "IdentificacionExtranjero", xmlr);
                    }
                    //Comprobante XML
                    datosReceptor.comprobanteXml = EncodeXML.XMLUtils.base64Encode(xmlr);

                    //Auditoría
                    datosReceptor.usuarioCreacion = Session["usuario"].ToString();
                    datosReceptor.fechaCreacion = Date.DateTimeNow();

                    //Valores de la tabla
                    datosReceptor.montoTotalFactura = decimal.Parse(this.txtTotalFactura.Text);
                    datosReceptor.montoTotalImpuesto = decimal.Parse(this.txtMontoTotalImpuesto.Text);
                    datosReceptor.tipoDocumento = datos.clave.Substring(29, 2);

                    //Verificar si este dato es el correcto
                    datosReceptor.mensaje = this.txtDetalleMensaje.Text;
                    datosReceptor.fecha = DateTime.ParseExact(documento.fechaEmision.Replace("-06:00", ""), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                    //Datos Hacienda
                    using (var conexionWS = new DataModelFE())
                    {
                        WSRecepcionPOST datoHacienda = conexionWS.WSRecepcionPOST.Find(datosReceptor.clave);
                        if (datoHacienda != null)
                        {
                            datosReceptor.indEstado = datoHacienda.indEstado;
                            datosReceptor.mensaje = datoHacienda.mensaje;
                        }
                        else
                        {
                            datosReceptor.indEstado = 1;
                        }
                    }
                    conexion.WSRecepcionPOSTReceptor.Add(datosReceptor);
                    conexion.SaveChanges();
                }

            }
        }

        private static bool existeClave(string pClave)
        {
            try
            {
                bool existe = false;
                using (var conexion = new DataModelFE())
                {
                    // se declara el objeto a insertar
                    ResumenFacturaReceptor dato = new ResumenFacturaReceptor();
                    //llena el objeto con los valores de la pantalla
                    dato.clave = pClave;
                    //busca el objeto 
                    dato = conexion.ResumenFacturaReceptor.Find(dato.clave);
                    if (dato != null)
                    {
                        existe = true;
                    }
                }
                return existe;
            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
        }

        protected void cmbMensaje_ValueChanged(object sender, EventArgs e)
        {
            this.obtenerConsecutivo();
        }
    }
}