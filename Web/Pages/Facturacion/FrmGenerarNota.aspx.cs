using Class.Utilidades;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;
using Web.WebServices;
using WSDomain;
using XMLDomain;

namespace Web.Pages.Facturacion
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmGenerarNota : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                this.AsyncMode = true;
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Pages/Login.aspx");
                }
                if (!IsCallback && !IsPostBack)
                {
                    this.cargarCombos();
                    this.cargarDatosDocumento();
                }
                this.refreshData();

            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
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


        #region Metodos Generales

        private void cargarDatosDocumento()
        {
            if (Session["clave"] != null)
            {
                string clave = Session["clave"].ToString();
                using (var conexion = new DataModelFE())
                {
                    WSRecepcionPOST dato = conexion.WSRecepcionPOST.Find(clave);
                    this.txtClave.Text = dato.clave;
                    this.txtConsecutivo.Text = dato.numeroConsecutivo;
                    this.txtFechaEmisor.Text = dato.fecha.ToString();
                    this.cmbTipoDocumento.Value = Session["tipoNota"].ToString();

                    /* SUCURSAL CAJA */
                    string emisor = Session["emisor"].ToString();
                    List<ConsecutivoDocElectronico> lista = conexion.ConsecutivoDocElectronico.Where(x => x.emisor == emisor && 
                    x.tipoDocumento == this.cmbTipoDocumento.Value.ToString() && x.estado == Estado.ACTIVO.ToString()).ToList();
                    if(lista.Count == 0)
                    {
                        ConsecutivoDocElectronico consecutivo = new ConsecutivoDocElectronico();
                        consecutivo.sucursal = ConsecutivoDocElectronico.DEFAULT_SUCURSAL;
                        consecutivo.caja = ConsecutivoDocElectronico.DEFAULT_CAJA;
                        consecutivo.digitoVerificador = ConsecutivoDocElectronico.DEFAULT_DIGITO_VERIFICADOR;
                        consecutivo.emisor = emisor;
                        consecutivo.tipoDocumento = this.cmbTipoDocumento.Value.ToString();
                        consecutivo.consecutivo = 1;
                        consecutivo.estado = Estado.ACTIVO.ToString();
                        consecutivo.fechaCreacion = Date.DateTimeNow();   
                        conexion.ConsecutivoDocElectronico.Add(consecutivo);
                        conexion.SaveChanges();

                        lista = conexion.ConsecutivoDocElectronico.Where(x => x.emisor == emisor &&
                                x.tipoDocumento == this.cmbTipoDocumento.Value.ToString() && x.estado == Estado.ACTIVO.ToString()).ToList();
                    }
                     
                    this.cmbSucursalCaja.Items.Clear();
                    foreach (var item in lista)
                    {
                        this.cmbSucursalCaja.Items.Add(item.ToString(), string.Format("{0}{1}", item.sucursal, item.caja));
                    }
                    this.cmbSucursalCaja.IncrementalFilteringMode = IncrementalFilteringMode.Contains;


                    string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);
                     
                    FacturaElectronica factura = (FacturaElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(FacturaElectronica) );

                    txtNombreEmisor.Text = string.Format("{0} - {1}", factura.emisor.identificacion.numero, factura.emisor.nombre); 
                    txtNombreReceptor.Text = string.Format("{0} - {1}", factura.receptor.identificacion.numero, factura.receptor.nombre);
                    
                    txtCorreoReceptor.Text = factura.receptor.correoElectronico;

                    // deja el monto neto facturado
                    foreach (var item in factura.detalleServicio.lineaDetalle)
                    {
                        item.precioUnitario = item.precioUnitario - item.montoDescuento;
                        item.montoDescuento = 0;
                        item.calcularMontos();
                    }

                    Session["detalleServicio"] = factura.detalleServicio;

                    this.refreshData();

                }
            }
        }

        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            if (Session["detalleServicio"] != null)
            {
                DetalleServicio detalleServicio = (DetalleServicio)Session["detalleServicio"];
                this.ASPxGridView1.DataSource = detalleServicio.lineaDetalle;
                this.ASPxGridView1.DataBind();
            }
        }


        /// <summary>
        /// carga solo una vez para ahorar tiempo 
        /// </summary>
        private void cargarCombos()
        {
            using (var conexion = new DataModelFE())
            {

                /* PRODUCTO */
                string emisor = Session["emisor"].ToString();
                GridViewDataComboBoxColumn comboProducto = this.ASPxGridView1.Columns["codigo.codigo"] as GridViewDataComboBoxColumn;
                comboProducto.PropertiesComboBox.Items.Clear();
                foreach (var item in conexion.Producto.Where(x => x.emisor == emisor).ToList())
                {
                    comboProducto.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
                comboProducto.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;


                /* TIPO DOCUMENTO */
                foreach (var item in conexion.TipoDocumento.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbTipoDocumento.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbTipoDocumento.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                
                /* CODIGO REFERENCIA */
                foreach (var item in conexion.CodigoReferencia.Where(x => x.estado == Estado.ACTIVO.ToString() && x.aplicaNotas==Confirmacion.SI.ToString()).ToList())
                {
                    this.cmbCodigoReferencia.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbCodigoReferencia.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            }
        }
        #endregion

        #region Metodos para el Grid
        protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (this.ASPxGridView1.IsNewRowEditing)
            {
                if (e.Column.FieldName == "subTotal") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "montoTotal") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "montoDescuento") { e.Editor.Value = 0; }
                if (e.Column.FieldName == "precioUnitario") { e.Editor.Value = 0; }
                if (e.Column.FieldName == "naturalezaDescuento") { e.Editor.Value = "N/A"; }
            }
            else
            {
                if (e.Column.FieldName == "subTotal") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "montoTotal") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "producto") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
            }
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    DetalleServicio detalleServicio = (DetalleServicio)Session["detalleServicio"];
                    var id = e.Values["codigo.codigo"].ToString();
                    LineaDetalle dato = detalleServicio.lineaDetalle.Where(x => x.codigo.codigo == id).FirstOrDefault();
                    detalleServicio.lineaDetalle.Remove(dato);

                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridView1.CancelEdit();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
            finally
            {
                //refescar los datos
                this.refreshData();
            }

        }

        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    DetalleServicio detalleServicio = (DetalleServicio)Session["detalleServicio"];

                    string codProducto = e.NewValues["codigo.codigo"] != null ? e.NewValues["codigo.codigo"].ToString().ToUpper() : null;
                    LineaDetalle dato = detalleServicio.lineaDetalle.Where(x => x.codigo.codigo == codProducto).FirstOrDefault();
                    //llena el objeto con los valores de la pantalla

                    Producto producto = conexion.Producto.Where(x => x.codigo == codProducto).FirstOrDefault();
                    decimal tontoTotalLinea = dato.montoTotalLinea;

                    //dato.numeroLinea = detalleServicio.lineaDetalle.Count;
                    dato.cantidad = e.NewValues["cantidad"] != null ? decimal.Parse(e.NewValues["cantidad"].ToString()) : 0;
                    //dato.codigo.tipo = producto.tipo;
                    //dato.codigo.codigo = producto.codigo;
                    //dato.detalle = producto.descripcion;
                    //dato.unidadMedida = producto.unidadMedida;
                    dato.unidadMedidaComercial = "";

                    decimal precio = "0".Equals(e.NewValues["precioUnitario"].ToString()) ? producto.precio : decimal.Parse(e.NewValues["precioUnitario"].ToString());

                    dato.producto = producto.codigo;/*solo para uso del grid*/
                    dato.precioUnitario = precio;
                    dato.montoDescuento = e.NewValues["montoDescuento"] != null ? decimal.Parse(e.NewValues["montoDescuento"].ToString()) : 0;
                    dato.montoTotal = dato.subTotal - dato.montoDescuento;
                    dato.calcularMontos();

                    dato.naturalezaDescuento = e.NewValues["naturalezaDescuento"] != null ? e.NewValues["naturalezaDescuento"].ToString().ToUpper() : null;
                    dato.naturalezaDescuento = dato.naturalezaDescuento;

                    if (TipoDocumento.NOTA_CREDITO.Equals(Session["tipoNota"].ToString()))
                    {
                        if (tontoTotalLinea < dato.montoTotalLinea)
                        {
                            throw new Exception(string.Format("El monto de la línea de la nota de crédito no puede ser mayor al original {0}", tontoTotalLinea));
                        }
                    }
                    
                    //agrega el objeto 
                    Session["detalleServicio"] = detalleServicio;

                }

                //esto es para el manero del devexpress
                e.Cancel = true;
                this.ASPxGridView1.CancelEdit();


            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
            finally
            {
                //refescar los datos
                this.refreshData();
            }
        }

        #endregion

        protected async void btnCrearNota_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();


                List<string> cc = new List<string>();
                Regex validator = new Regex(@"\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*");

                foreach (var correo in this.txtCorreoReceptor.Tokens)
                {
                    if (validator.IsMatch(correo))
                    {
                        cc.Add(correo);
                    }
                    else
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-danger";
                        this.alertMessages.InnerText = String.Format("Favor verifique el formato de la dirección {0}", correo);
                        return;
                    }
                }
                cc.RemoveAt(0);


                DetalleServicio detalle = (DetalleServicio)Session["detalleServicio"];
                if (detalle.lineaDetalle.Count == 0)
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "Debe agregar almenos una linea de detalle a la factura";
                    return;
                }

                // datos de la factura original
                FacturaElectronica factura = new FacturaElectronica();
                string clave = Session["clave"].ToString();
                using (var conexion = new DataModelFE())
                {
                    WSRecepcionPOST datoPost = conexion.WSRecepcionPOST.Find(clave);
                    string xmlFactura = EncodeXML.EncondeXML.base64Decode(datoPost.comprobanteXml);
                    factura = (FacturaElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xmlFactura, typeof(FacturaElectronica));
                     
                    DocumentoElectronico dato = null;
                    if (TipoDocumento.NOTA_CREDITO.Equals(Session["tipoNota"].ToString()))
                    {
                        dato = new NotaCreditoElectronica(); 
                    }
                    else
                    {
                        dato = new NotaDebitoElectronica(); 
                    }
                    

                    /* ENCABEZADO */
                    dato.medioPago = factura.medioPago;
                    dato.plazoCredito = factura.plazoCredito;
                    dato.condicionVenta = factura.condicionVenta; 
                    dato.fechaEmision = Date.DateTimeNow().ToString("yyyy-MM-ddTHH:mm:ss") + "-06:00";
                    /* DETALLE */
                    dato.detalleServicio = detalle;

                    /* EMISOR */
                    dato.emisor = factura.emisor;


                    /* RECEPTOR */
                    dato.receptor = factura.receptor;
                    dato.receptor.identificacion.numero = dato.receptor.identificacion.numero.Replace(" ", "").Trim();
                    dato.receptor.identificacion.numero = dato.receptor.identificacion.numero.Replace("-", "").Trim();


                    /* INFORMACION DE REFERENCIA */
                    InformacionReferencia informacionReferencia = new InformacionReferencia();
                    informacionReferencia.numero = factura.clave;
                    informacionReferencia.fechaEmision = factura.fechaEmision;
                    informacionReferencia.codigo = this.cmbCodigoReferencia.Value.ToString();
                    informacionReferencia.razon = this.txtRazón.Text;
                    informacionReferencia.tipoDocumento = TipoDocumento.FACTURA_ELECTRONICA;
                    dato.informacionReferencia.Add(informacionReferencia);

                    /* RESUMEN */
                    dato.resumenFactura.tipoCambio = factura.resumenFactura.tipoCambio; 
                    dato.resumenFactura.codigoMoneda = factura.resumenFactura.codigoMoneda;
                    foreach (var item in dato.detalleServicio.lineaDetalle)
                    {
                        if (item.tipoServMerc==null) {
                            Producto producto = conexion.Producto.Where(x=>x.codigo==item.codigo.codigo && x.emisor == dato.emisor.identificacion.numero).FirstOrDefault();
                            item.tipoServMerc = producto.tipoServMerc;
                            item.producto = producto.codigo;
                        }
                    } 
                    dato.resumenFactura.calcularResumenFactura(dato.detalleServicio.lineaDetalle);

                    /* VERIFICA VACIOS PARA XML */
                    dato.verificaDatosParaXML();

                    //genera el consecutivo del documento
                    string sucursal = this.cmbSucursalCaja.Value.ToString().Substring(0, 3);
                    string caja = this.cmbSucursalCaja.Value.ToString().Substring(3, 5);
                    object[] key = new object[] { dato.emisor.identificacion.numero, sucursal, caja, this.cmbTipoDocumento.Value.ToString() };
                    ConsecutivoDocElectronico consecutivo = conexion.ConsecutivoDocElectronico.Find(key);
                    if (consecutivo != null)
                    {
                        dato.clave = consecutivo.getClave(Date.DateTimeNow().ToString("yyyyMMdd"));
                        dato.numeroConsecutivo = consecutivo.getConsecutivo();

                        consecutivo.consecutivo += 1;
                        conexion.Entry(consecutivo).State = EntityState.Modified; 
                    }
                    else
                    {
                        consecutivo = new ConsecutivoDocElectronico();
                        consecutivo.sucursal = ConsecutivoDocElectronico.DEFAULT_SUCURSAL;
                        consecutivo.caja = ConsecutivoDocElectronico.DEFAULT_CAJA;
                        consecutivo.digitoVerificador = ConsecutivoDocElectronico.DEFAULT_DIGITO_VERIFICADOR;
                        consecutivo.emisor = dato.emisor.identificacion.numero;
                        consecutivo.tipoDocumento = this.cmbTipoDocumento.Value.ToString();
                        consecutivo.consecutivo = 1;
                        consecutivo.estado = Estado.ACTIVO.ToString();
                        consecutivo.fechaCreacion = Date.DateTimeNow();

                        dato.clave = consecutivo.getClave(Date.DateTimeNow().ToString("yyyyMMdd"));
                        dato.numeroConsecutivo = consecutivo.getConsecutivo();

                        consecutivo.consecutivo += 1;
                        conexion.ConsecutivoDocElectronico.Add(consecutivo);

                    }

                    EmisorReceptorIMEC elEmisor = (EmisorReceptorIMEC)Session["elEmisor"];
                    string responsePost = await Services.enviarDocumentoElectronico(false, dato, elEmisor, this.cmbTipoDocumento.Value.ToString(), Session["usuario"].ToString());

                    if (responsePost.Equals("Success"))
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-info";
                        this.alertMessages.InnerText = String.Format("Documento #{0} enviada.", dato.numeroConsecutivo);

                        if (!string.IsNullOrWhiteSpace(dato.receptor.correoElectronico))
                        {
                            string xml = EncodeXML.EncondeXML.getXMLFromObject(dato);

                            

                            Utilidades.sendMail(Session["emisor"].ToString(),dato.receptor.correoElectronico,
                                string.Format("{0} - {1}", dato.numeroConsecutivo, factura.receptor.nombre),
                                Utilidades.mensageGenerico(), "Documento Electrónico", xml, dato.numeroConsecutivo, dato.clave,cc );
                        }
                    }
                    else if (responsePost.Equals("Error"))
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-danger";
                        this.alertMessages.InnerText = String.Format("Documento #{0} con errores.", dato.numeroConsecutivo);
                    }
                    else
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-warning";
                        this.alertMessages.InnerText = String.Format("Documento #{0} pendiente de envío", dato.numeroConsecutivo);
                    }

                    conexion.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
            finally
            {
                //refescar los datos
                this.refreshData();
            }
        }

        protected void cmbTipoDocumento_ValueChanged(object sender, EventArgs e)
        {
           

        }
    }
}