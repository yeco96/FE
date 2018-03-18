﻿using Class.Seguridad;
using Class.Utilidades;
using DevExpress.Web;
using EncodeXML;
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
    public partial class FrmReenvioOtrosCorreos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                this.AsyncMode = true;
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Pages/Login.aspx");
                }
                if (!IsCallback && !IsPostBack)
                {
                    this.loadComboBox();
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
                using (var conexionWS = new DataModelFE())
                {
                    WSRecepcionPOST dato = conexionWS.WSRecepcionPOST.Find(clave);
                    this.cmbTipoDocumento.Value = Session["tipoDocumento"].ToString();

                    string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);

                    FacturaElectronica factura = (FacturaElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(FacturaElectronica));

                    this.cmbCondicionVenta.Value = factura.condicionVenta;
                    this.cmbMedioPago.Value = factura.medioPago;
                    this.cmbTipoDocumento.Value = factura.tipoDocumento;
                    this.cmbTipoMoneda.Value = factura.resumenFactura.codigoMoneda;
                    this.txtTipoCambio.Text = factura.resumenFactura.tipoCambio.ToString();
                    this.txtPlazoCredito.Text = factura.plazoCredito;
                    this.txtFechaEmision.Text = factura.fechaEmision;

                    foreach (var otros in factura.otros.otrosTextos)
                    {
                        this.txtOtrosTextos.Text += string.Format("{0}\n", otros);
                    }

                    txtNombreEmisor.Text = string.Format("{0} - {1}", factura.emisor.identificacion.numero, factura.emisor.nombre);
                    txtNombreReceptor.Text = string.Format("{0} - {1}", factura.receptor.identificacion.numero, factura.receptor.nombre);


                    txtCorreos.Text = factura.receptor.correoElectronico;

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
        private void loadComboBox()
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


                /* MEDIO PAGO */
                foreach (var item in conexion.MedioPago.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbMedioPago.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbMedioPago.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbMedioPago.SelectedIndex = 0;

                /* CONDICION VENTA */
                foreach (var item in conexion.CondicionVenta.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbCondicionVenta.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbCondicionVenta.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbCondicionVenta.SelectedIndex = 0;
                this.txtPlazoCredito.Text = "0";

                /* TIPO MONEDA */
                foreach (var item in conexion.TipoMoneda.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbTipoMoneda.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbTipoMoneda.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbTipoMoneda.SelectedIndex = 0;


                /* TIPO DOCUMENTO */

                foreach (var item in conexion.TipoDocumento.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbTipoDocumento.Items.Add(item.descripcion, item.codigo);

                }
                this.cmbTipoDocumento.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                this.cmbTipoDocumento.SelectedIndex = 0;


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

        protected void btnEnviarCorreo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    string clave = Session["clave"].ToString();
                    WSRecepcionPOST dato = conexion.WSRecepcionPOST.Find(clave);
                    dato.cargarEmisorReceptor();
                    string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);

                    string numeroConsecutivo = EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(xml), "NumeroConsecutivo", xml);
                    string correoElectronico = EncondeXML.buscarValorEtiquetaXML("Receptor", "CorreoElectronico", xml);

                    if (this.txtCorreos.Tokens.Count >0)
                    {
                        List<string> cc = new List<string>(); 
                        Regex validator = new Regex(@"\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*");

                        foreach (var correo in this.txtCorreos.Tokens)
                        { 
                            if (validator.IsMatch(correo)) { 
                                cc.Add(correo);
                            }else
                            {
                                this.alertMessages.Attributes["class"] = "alert alert-danger";
                                this.alertMessages.InnerText =String.Format("Favor verifique el formato de la dirección {0}", correo);
                                return;
                            }
                        }
                        cc.RemoveAt(0);
                        bool result = Utilidades.sendMail(Session["emisor"].ToString(), correoElectronico,
                            string.Format("{0} - {1}", numeroConsecutivo, dato.Receptor.nombre),
                            Utilidades.mensageGenerico(), "Documento Electrónico", xml, numeroConsecutivo, dato.clave, cc);

                        if (result)
                        {
                            this.alertMessages.Attributes["class"] = "alert alert-info";
                            this.alertMessages.InnerText = "El envió se realizó exitosamente";
                        }
                        else{
                            this.alertMessages.Attributes["class"] = "alert alert-danger";
                            this.alertMessages.InnerText = "Tenemos problema para enviar el correo, favor intente más tarde";
                        }
                    }
                    else
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-danger";
                        this.alertMessages.InnerText ="Debe digitar una dirección de correo eléctronico";
                    }
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

        protected void btnRecresar_Click(object sender, EventArgs e)
        {
            Usuario usuario =(Usuario) Session["elUsuario"];
            if(usuario.rol.Equals(Rol.FACTURADOR))
                Response.Redirect("~/Pages/Facturacion/FrmAdministracionDocElectronico.aspx");
            else
                Response.Redirect("~/Pages/Facturacion/FrmAdministracionDocElectronicoAdmin.aspx");
        }
    }
}