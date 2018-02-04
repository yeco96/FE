using Class.Utilidades;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
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
    public partial class FrmGenerarNota : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                this.AsyncMode = true;

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
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex.Message);
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
                using (var conexionWS = new DataModelWS())
                {
                    WSRecepcionPOST dato = conexionWS.WSRecepcionPOST.Find(clave);
                    this.txtClave.Text = dato.clave;
                    this.txtConsecutivo.Text = dato.numeroConsecutivo;
                    this.txtFechaEmisor.Text = dato.fecha.ToString();
                    this.cmbTipoDocumento.Value = Session["tipoNota"].ToString();

                    string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);
                     
                    FacturaElectronica factura = (FacturaElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(FacturaElectronica) );

                    txtNombreEmisor.Text = string.Format("{0} - {1}", factura.emisor.identificacion.numero, factura.emisor.nombre); 
                    txtNombreReceptor.Text = string.Format("{0} - {1}", factura.receptor.identificacion.numero, factura.receptor.nombre);
                    
                    txtCorreoReceptor.Text = factura.receptor.correoElectronico;

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
                /* SUCURSAL CAJA */
                string elEmisor = ((EmisorReceptorIMEC)Session["emisor"]).identificacion;
                foreach (var item in conexion.ConsecutivoDocElectronico.Where(x => x.emisor == elEmisor).Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbSucursalCaja.Items.Add(item.ToString(), string.Format("{0}{1}", item.sucursal, item.caja));
                }
                this.cmbSucursalCaja.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                /* TIPO DOCUMENTO */
                foreach (var item in conexion.TipoDocumento.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbTipoDocumento.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbTipoDocumento.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                
                /* TIPO DOCUMENTO */
                foreach (var item in conexion.CodigoReferencia.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
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
                    var id = e.Values["producto"].ToString();
                    LineaDetalle dato = detalleServicio.lineaDetalle.Where(x => x.codigo.codigo == id).FirstOrDefault();
                    detalleServicio.lineaDetalle.Remove(dato);

                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridView1.CancelEdit();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex.Message), ex.InnerException);
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
                throw new Exception(Utilidades.validarExepcionSQL(ex.Message), ex.InnerException);
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
                using (var conexionWS = new DataModelWS())
                {
                    WSRecepcionPOST dato = conexionWS.WSRecepcionPOST.Find(clave);
                    string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);
                    factura = (FacturaElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(FacturaElectronica));
                }

                using (var conexion = new DataModelFE())
                {
                    NotaCreditoElectronica dato = new NotaCreditoElectronica();

                    /* ENCABEZADO */
                    dato.medioPago = factura.medioPago;
                    dato.plazoCredito = factura.plazoCredito;
                    dato.condicionVenta = factura.condicionVenta;
                    dato.fechaEmision = Date.DateTimeNow(); 

                    /* DETALLE */
                    dato.detalleServicio = detalle;

                    /* EMISOR */
                    dato.emisor = factura.emisor;


                    /* RECEPTOR */
                    dato.receptor = factura.receptor;

                    /* INFORMACION DE REFERENCIA */
                    dato.informacionReferencia.tipoDoc = TipoDocumento.FACTURA_ELECTRONICA;
                    dato.informacionReferencia.numero = factura.clave;
                    dato.informacionReferencia.fechaEmision = factura.fechaEmision;
                    dato.informacionReferencia.codigo = this.cmbCodigoReferencia.Value.ToString();
                    dato.informacionReferencia.razon = this.txtRazón.Text;


                    /* RESUMEN */
                    dato.resumenFactura.tipoCambio = factura.resumenFactura.tipoCambio; 
                    dato.resumenFactura.codigoMoneda = factura.resumenFactura.codigoMoneda;
                    dato.resumenFactura.calcularResumenFactura(dato.detalleServicio.lineaDetalle);

                    /* VERIFICA VACIOS PARA XML */
                    dato.verificaDatosParaXML();

                    //genera el consecutivo del documento
                    string sucursal = this.cmbSucursalCaja.Value.ToString().Substring(0, 3);
                    string caja = this.cmbSucursalCaja.Value.ToString().Substring(3, 5);
                    object[] key = new object[] { dato.emisor.identificacion.numero, sucursal, caja };
                    ConsecutivoDocElectronico consecutivo = conexion.ConsecutivoDocElectronico.Find(key);

                    dato.clave = consecutivo.getClave(this.cmbTipoDocumento.Value.ToString());
                    dato.numeroConsecutivo = consecutivo.getConsecutivo(this.cmbTipoDocumento.Value.ToString());

                    consecutivo.consecutivo += 1;
                    conexion.Entry(consecutivo).State = EntityState.Modified;
                    conexion.SaveChanges();

                    string xml = EncodeXML.EncondeXML.getXMLFromObject(dato);

                    EmisorReceptorIMEC elEmisor = (EmisorReceptorIMEC)base.Session["emisor"];
                    string responsePost = await Services.enviarDocumentoElectronico(false, xml, elEmisor, this.cmbTipoDocumento.Value.ToString(), Session["usuario"].ToString());

                    if (responsePost.Equals("Success"))
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-info";
                        this.alertMessages.InnerText = String.Format("Nota Crédito #{0} enviada.", dato.numeroConsecutivo);

                        if (!string.IsNullOrWhiteSpace(dato.receptor.correoElectronico))
                        {
                            Utilidades.sendMail(dato.receptor.correoElectronico,
                                string.Format("{0} - {1}", dato.numeroConsecutivo, dato.receptor.nombre),
                                Utilidades.mensageGenerico(), "Nota Crédito", xml, dato.numeroConsecutivo, dato.clave);
                        }
                    }
                    else if (responsePost.Equals("Error"))
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-danger";
                        this.alertMessages.InnerText = String.Format("Nota Crédito #{0} con errores.", dato.numeroConsecutivo);
                    }
                    else
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-warning";
                        this.alertMessages.InnerText = String.Format("Factura #{0} pendiente de envío", dato.numeroConsecutivo);
                    }

                }
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex.Message);
            }
            finally
            {
                //refescar los datos
                this.refreshData();
            }
        }
    }
}