using Class.Utilidades;
using DevExpress.Web;
using EncodeXML;
using FirmaXadesNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;
using Web.Utils;
using Web.WebServices;
using WSDomain;
using XMLDomain;

namespace Web.Pages.Facturacion
{
    public partial class FrmGenerarDocumento : System.Web.UI.Page
    {
        private DetalleServicio detalleServicio;

        public FrmGenerarDocumento()
        {
            this.detalleServicio = new DetalleServicio();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.lblMensaje.Text = "";
                this.AsyncMode = true;
                if (!IsCallback && !IsPostBack)
                {
                    this.txtFechaEmision.Date = Date.DateTimeNow();
                    this.cmbTipoMoneda.Value = TipoMoneda.CRC;
                    this.txtTipoCambio.Text = "0";
                    this.loadComboBox();

                    this.detalleServicio = new DetalleServicio();
                    Session["detalleServicio"] = detalleServicio;
                }
                this.refreshData();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
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


                /* EMISOR */
                string elEmisor = "603540974";
                EmisorReceptorIMEC emisor = conexion.EmisorReceptorIMEC.Where(x => x.identificacion == elEmisor).FirstOrDefault();
                this.loadEmisor(emisor);


                string elReceptor = "601230863";
                EmisorReceptorIMEC receptor = conexion.EmisorReceptorIMEC.Where(x => x.identificacion == elReceptor).FirstOrDefault();
                this.loadReceptor(receptor);

                /* IDENTIFICACION TIPO */
                foreach (var item in conexion.TipoIdentificacion.ToList())
                {
                    this.cmbEmisorTipo.Items.Add(item.descripcion, item.codigo);
                    this.cmbReceptorTipo.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbEmisorTipo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbReceptorTipo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;


                /* CODIGO PAIS */
                foreach (var item in conexion.CodigoPais.ToList())
                {
                    this.cmbEmisorTelefonoCod.Items.Add(item.descripcion, item.codigo);
                    this.cmbEmisorFaxCod.Items.Add(item.descripcion, item.codigo);
                    this.cmbReceptorTelefonoCod.Items.Add(item.descripcion, item.codigo);
                    this.cmbReceptorFaxCod.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbEmisorTelefonoCod.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbEmisorFaxCod.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbReceptorTelefonoCod.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbReceptorFaxCod.IncrementalFilteringMode = IncrementalFilteringMode.Contains;


                /* PROVINCIA*/
                foreach (var item in conexion.Ubicacion.Select(x => new { x.codProvincia, x.nombreProvincia }).Distinct())
                {
                    this.cmbEmisorProvincia.Items.Add(item.nombreProvincia, item.codProvincia);
                    this.cmbReceptorProvincia.Items.Add(item.nombreProvincia, item.codProvincia);
                }
                this.cmbEmisorProvincia.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbReceptorProvincia.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                /* MEDIO PAGO */
                foreach (var item in conexion.MedioPago.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbMedioPago.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbMedioPago.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                /* CONDICION VENTA */
                foreach (var item in conexion.CondicionVenta.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbCondicionVenta.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbCondicionVenta.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                /* TIPO MONEDA */
                foreach (var item in conexion.TipoMoneda.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbTipoMoneda.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbTipoMoneda.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                /* PRODUCTO */
                GridViewDataComboBoxColumn comboProducto = this.ASPxGridView1.Columns["producto"] as GridViewDataComboBoxColumn;
                comboProducto.PropertiesComboBox.Items.Clear();
                foreach (var item in conexion.Producto.Where(x => x.estado == Estado.ACTIVO.ToString()).Where(x => x.emisor == elEmisor).ToList())
                {
                    comboProducto.PropertiesComboBox.Items.Add(item.ToString(), item.codigo);
                }
                comboProducto.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            }
        }

        /// <summary>
        /// carga datos del emisor
        /// </summary>
        /// <param name="emisor"></param>
        private void loadEmisor(EmisorReceptorIMEC emisor)
        {
            this.cmbEmisorTipo.Value = emisor.identificacionTipo;
            this.txtEmisorIdentificacion.Text = emisor.identificacion;
            this.txtEmisorNombre.Text = emisor.nombre;
            this.txtEmisorNombreComercial.Text = emisor.nombreComercial;

            this.cmbEmisorTelefonoCod.Value = emisor.telefonoCodigoPais;
            this.cmbEmisorFaxCod.Value = emisor.faxCodigoPais;
            this.txtEmisorTelefono.Value = emisor.telefono;
            this.txtEmisorFax.Value = emisor.fax;
            this.txtEmisorCorreo.Text = emisor.correoElectronico;

            this.cmbEmisorProvincia.Value = emisor.provincia;

            this.cmbEmisorProvincia_ValueChanged(null, null);
            this.cmbEmisorCanton.Value = emisor.canton;

            this.cmbEmisorCanton_ValueChanged(null, null);
            this.cmbEmisorDistrito.Value = emisor.distrito;

            this.cmbEmisorDistrito_ValueChanged(null, null);
            this.cmbEmisorBarrio.Value = emisor.barrio;
            this.txtEmisorOtraSenas.Value = emisor.otraSena;

        }


        private void loadReceptor(EmisorReceptorIMEC emisor)
        {
            this.cmbReceptorTipo.Value = emisor.identificacionTipo;
            this.txtReceptorIdentificacion.Text = emisor.identificacion;
            this.txtReceptorNombre.Text = emisor.nombre;
            this.txtReceptorNombreComercial.Text = emisor.nombreComercial;

            this.cmbReceptorTelefonoCod.Value = emisor.telefonoCodigoPais;
            this.cmbReceptorFaxCod.Value = emisor.faxCodigoPais;
            this.txtReceptorTelefono.Value = emisor.telefono;
            this.txtReceptorFax.Value = emisor.fax;
            this.txtReceptorCorreo.Text = emisor.correoElectronico;

            this.cmbReceptorProvincia.Value = emisor.provincia;

            this.cmbReceptorProvincia_ValueChanged(null, null);
            this.cmbReceptorCanton.Value = emisor.canton;

            this.cmbReceptorCanton_ValueChanged(null, null);
            this.cmbReceptorDistrito.Value = emisor.distrito;

            this.cmbReceptorDistrito_ValueChanged(null, null);
            this.cmbReceptorBarrio.Value = emisor.barrio;
            this.txtEmisorOtraSenas.Value = emisor.otraSena;

        }

        protected void cmbEmisorProvincia_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                this.cmbEmisorDistrito.SelectedItem = null;
                this.cmbEmisorDistrito.Items.Clear();
                this.cmbEmisorCanton.SelectedItem = null;
                this.cmbEmisorCanton.Items.Clear();

                foreach (var item in conexion.Ubicacion.
                    Where(x => x.codProvincia == this.cmbEmisorProvincia.Value.ToString()).
                    Select(x => new { x.codCanton, x.nombreCanton }).Distinct())
                {
                    this.cmbEmisorCanton.Items.Add(item.nombreCanton, item.codCanton);
                }
                this.cmbEmisorCanton.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }
        protected void cmbEmisorCanton_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                this.cmbEmisorDistrito.SelectedItem = null;
                this.cmbEmisorDistrito.Items.Clear();

                foreach (var item in conexion.Ubicacion.
                    Where(x => x.codProvincia == this.cmbEmisorProvincia.Value.ToString()).
                    Where(x => x.codCanton == this.cmbEmisorCanton.Value.ToString()).
                    Select(x => new { x.codDistrito, x.nombreDistrito }).Distinct())
                {
                    this.cmbEmisorDistrito.Items.Add(item.nombreDistrito, item.codDistrito);
                }
                this.cmbEmisorDistrito.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }


        protected void cmbReceptorProvincia_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                this.cmbReceptorDistrito.SelectedItem = null;
                this.cmbReceptorDistrito.Items.Clear();
                this.cmbReceptorCanton.SelectedItem = null;

                this.cmbReceptorCanton.Items.Clear();
                foreach (var item in conexion.Ubicacion.Where(x => x.codProvincia == this.cmbReceptorProvincia.Value.ToString()).Select(x => new { x.codCanton, x.nombreCanton }).Distinct())
                {
                    this.cmbReceptorCanton.Items.Add(item.nombreCanton, item.codCanton);
                }
                this.cmbReceptorCanton.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }

        protected void cmbReceptorCanton_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                this.cmbReceptorDistrito.SelectedItem = null;
                this.cmbReceptorDistrito.Items.Clear();
                foreach (var item in conexion.Ubicacion.
                    Where(x => x.codProvincia == this.cmbReceptorProvincia.Value.ToString()).
                    Where(x => x.codCanton == this.cmbReceptorCanton.Value.ToString()).
                    Select(x => new { x.codDistrito, x.nombreDistrito }).Distinct())
                {
                    this.cmbReceptorDistrito.Items.Add(item.nombreDistrito, item.codDistrito);
                }
                this.cmbReceptorDistrito.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }


        protected void cmbEmisorDistrito_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                this.cmbEmisorBarrio.SelectedItem = null;
                this.cmbEmisorBarrio.Items.Clear();
                foreach (var item in conexion.Ubicacion.
                    Where(x => x.codProvincia == this.cmbEmisorProvincia.Value.ToString()).
                    Where(x => x.codCanton == this.cmbEmisorCanton.Value.ToString()).
                     Where(x => x.codDistrito == this.cmbEmisorDistrito.Value.ToString()).
                    Select(x => new { x.codBarrio, x.nombreBarrio }).Distinct())
                {
                    this.cmbEmisorBarrio.Items.Add(item.nombreBarrio, item.codBarrio);
                }
                this.cmbEmisorBarrio.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }

        protected void cmbReceptorDistrito_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                this.cmbReceptorBarrio.SelectedItem = null;
                this.cmbReceptorBarrio.Items.Clear();
                foreach (var item in conexion.Ubicacion.
                    Where(x => x.codProvincia == this.cmbReceptorProvincia.Value.ToString()).
                    Where(x => x.codCanton == this.cmbReceptorCanton.Value.ToString()).
                     Where(x => x.codDistrito == this.cmbReceptorDistrito.Value.ToString()).
                    Select(x => new { x.codBarrio, x.nombreBarrio }).Distinct())
                {
                    this.cmbReceptorBarrio.Items.Add(item.nombreBarrio, item.codBarrio);
                }
                this.cmbReceptorBarrio.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }



        protected void cmbMoneda_ValueChanged(object sender, EventArgs e)
        {
            if (TipoMoneda.CRC.Equals(this.cmbTipoMoneda.Value.ToString()))
            {
                this.txtTipoCambio.Enabled = false;
                this.txtTipoCambio.Value = 0;
            }
            else
            {

                if (Session["tipoCambio"] != null)
                {
                    this.txtTipoCambio.Value = Session["tipoCambio"];
                }
                else
                {
                    this.txtTipoCambio.Value = BCCR.tipoCambioDOLAR();
                    Session["tipoCambio"] = this.txtTipoCambio.Value;
                }
                this.txtTipoCambio.Enabled = true;
            }
        }

        protected void cmbCondicionVenta_ValueChanged(object sender, EventArgs e)
        {
            if (CondicionVenta.CREDITO.Equals(this.cmbCondicionVenta.Text.ToString()))
            {
                this.txtPlazoCredito.Enabled = true;
                this.txtPlazoCredito.Value = 3;
            }
            else
            {
                this.txtPlazoCredito.Value = 0;
                this.txtPlazoCredito.Enabled = false;
            }
        }

        protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "subTotal") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
            if (e.Column.FieldName == "montoTotal") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
            if (e.Column.FieldName == "montoDescuento") { e.Editor.Value = 0; }
            if (e.Column.FieldName == "precioUnitario") { e.Editor.Value = 0; }
            if (e.Column.FieldName == "naturalezaDescuento") { e.Editor.Value = "N/A"; }

            if (!this.ASPxGridView1.IsNewRowEditing)
            {
                if (e.Column.FieldName == "producto") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
            }
        }
        protected void ASPxGridView1_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {

        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    DetalleServicio detalleServicio = (DetalleServicio)Session["detalleServicio"];
                    var id = e.Values["codigo"].ToString();
                    LineaDetalle dato = detalleServicio.lineaDetalle.Where(x => x.codigo.codigo == id).FirstOrDefault();
                    detalleServicio.lineaDetalle.Remove(dato);

                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridView1.CancelEdit();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                //refescar los datos
                this.refreshData();
            }

        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    DetalleServicio detalleServicio = (DetalleServicio)Session["detalleServicio"];

                    //se declara el objeto a insertar
                    LineaDetalle dato = new LineaDetalle();
                    //llena el objeto con los valores de la pantalla
                    string codProducto = e.NewValues["producto"] != null ? e.NewValues["producto"].ToString().ToUpper() : null;
                    Producto producto = conexion.Producto.Where(x => x.codigo == codProducto).FirstOrDefault();

                    dato.numeroLinea = detalleServicio.lineaDetalle.Count;
                    dato.cantidad = e.NewValues["cantidad"] != null ? double.Parse(e.NewValues["cantidad"].ToString()) : 0;
                    dato.codigo.tipo = producto.tipo;
                    dato.codigo.codigo = producto.codigo;
                    dato.detalle = producto.descripcion;
                    dato.unidadMedida = producto.unidadMedida;
                    dato.unidadMedidaComercial = "";

                    double precio = "0".Equals(e.NewValues["precioUnitario"].ToString()) ? producto.precio :  double.Parse(e.NewValues["precioUnitario"].ToString());

                    dato.producto = producto.codigo;/*solo para uso del grid*/
                    dato.precioUnitario = producto.precio;
                    dato.subTotal = precio * dato.cantidad;
                    dato.montoDescuento = e.NewValues["montoDescuento"] != null ? double.Parse(e.NewValues["montoDescuento"].ToString()) : 0;
                    dato.montoTotal = dato.subTotal - dato.montoDescuento;

                    dato.naturalezaDescuento = e.NewValues["naturalezaDescuento"] != null ? e.NewValues["naturalezaDescuento"].ToString().ToUpper() : null;
                    dato.naturalezaDescuento = dato.naturalezaDescuento;

                    //agrega el objeto
                    detalleServicio.lineaDetalle.Add(dato);
                    Session["detalleServicio"] = detalleServicio;
                }

                //esto es para el manero del devexpress
                e.Cancel = true;
                this.ASPxGridView1.CancelEdit();


            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // conexion.CodigoReferencia.Remove(conexion.CodigoReferencia.Last() );

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(fullErrorMessage, ex.EntityValidationErrors);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
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

                    string codProducto = e.NewValues["producto"] != null ? e.NewValues["producto"].ToString().ToUpper() : null;
                    LineaDetalle dato = detalleServicio.lineaDetalle.Where(x => x.codigo.codigo == codProducto).FirstOrDefault();
                    //llena el objeto con los valores de la pantalla

                    Producto producto = conexion.Producto.Where(x => x.codigo == codProducto).FirstOrDefault();

                    //dato.numeroLinea = detalleServicio.lineaDetalle.Count;
                    dato.cantidad = e.NewValues["cantidad"] != null ? double.Parse(e.NewValues["cantidad"].ToString()) : 0;
                    //dato.codigo.tipo = producto.tipo;
                    //dato.codigo.codigo = producto.codigo;
                    //dato.detalle = producto.descripcion;
                    //dato.unidadMedida = producto.unidadMedida;
                    dato.unidadMedidaComercial = "";

                    double precio = "0".Equals(e.NewValues["precioUnitario"].ToString()) ? producto.precio : double.Parse(e.NewValues["precioUnitario"].ToString());

                    dato.producto = producto.codigo;/*solo para uso del grid*/
                    dato.precioUnitario = producto.precio;
                    dato.subTotal = precio * dato.cantidad;
                    dato.montoDescuento = e.NewValues["montoDescuento"] != null ? double.Parse(e.NewValues["montoDescuento"].ToString()) : 0;
                    dato.montoTotal = dato.subTotal - dato.montoDescuento;


                    dato.naturalezaDescuento = e.NewValues["naturalezaDescuento"] != null ? e.NewValues["naturalezaDescuento"].ToString().ToUpper() : null;
                    dato.naturalezaDescuento = dato.naturalezaDescuento;

                    //agrega el objeto 
                    Session["detalleServicio"] = detalleServicio;

                }

                //esto es para el manero del devexpress
                e.Cancel = true;
                this.ASPxGridView1.CancelEdit();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                //refescar los datos
                this.refreshData();
            }
        }



        protected void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    FacturaElectronica dato = new FacturaElectronica();

                    /* ENCABEZADO */
                    dato.medioPago = this.cmbMedioPago.Value.ToString();
                    dato.plazoCredito = this.txtPlazoCredito.Text;
                    dato.condicionVenta = this.cmbCondicionVenta.Value.ToString();
                    dato.fechaEmision = this.txtFechaEmision.Date;
                    dato.medioPago = this.cmbMedioPago.Value.ToString();
                    dato.fechaEmision = this.txtFechaEmision.Date;

                    /* DETALLE */
                    dato.detalleServicio = (DetalleServicio)Session["detalleServicio"];

                    /* EMISOR */
                    EmisorReceptorIMEC elEmisor = conexion.EmisorReceptorIMEC.Where(x => x.identificacion == txtEmisorIdentificacion.Text).FirstOrDefault();

                    dato.emisor.identificacion.tipo = elEmisor.identificacionTipo;
                    dato.emisor.identificacion.numero = elEmisor.identificacion;
                    dato.emisor.nombre = elEmisor.nombre;
                    dato.emisor.nombreComercial = elEmisor.nombreComercial;

                    dato.emisor.telefono.codigoPais = elEmisor.telefonoCodigoPais;
                    dato.emisor.telefono.numTelefono = elEmisor.telefono;
                    dato.emisor.fax.codigoPais = elEmisor.faxCodigoPais;
                    dato.emisor.fax.numTelefono = elEmisor.fax;
                    dato.emisor.correoElectronico = elEmisor.correoElectronico;

                    dato.emisor.ubicacion.provincia = elEmisor.provincia;
                    dato.emisor.ubicacion.canton = elEmisor.canton;
                    dato.emisor.ubicacion.distrito = elEmisor.distrito;
                    dato.emisor.ubicacion.barrio = elEmisor.barrio;
                    dato.emisor.ubicacion.otrassenas = elEmisor.otraSena;

                    /* RECEPTOR */

                    EmisorReceptorIMEC elReceptor = conexion.EmisorReceptorIMEC.Where(x => x.identificacion == txtReceptorIdentificacion.Text).FirstOrDefault();

                    dato.receptor.identificacion.tipo = elReceptor.identificacionTipo;
                    dato.receptor.identificacion.numero = elReceptor.identificacion;
                    dato.receptor.nombre = elReceptor.nombre;
                    dato.receptor.nombreComercial = elReceptor.nombreComercial;

                    dato.receptor.telefono.codigoPais = elReceptor.telefonoCodigoPais;
                    dato.receptor.telefono.numTelefono = elReceptor.telefono;
                     
                    dato.receptor.fax.codigoPais = elReceptor.faxCodigoPais;
                    dato.receptor.fax.numTelefono = elReceptor.fax;
                    dato.receptor.correoElectronico = elReceptor.correoElectronico;

                    dato.receptor.ubicacion.provincia = elReceptor.provincia;
                    dato.receptor.ubicacion.canton = elReceptor.canton;
                    dato.receptor.ubicacion.distrito = elReceptor.distrito;
                    dato.receptor.ubicacion.barrio = elReceptor.barrio;
                    dato.receptor.ubicacion.otrassenas = elReceptor.otraSena;

                    /* RESUMEN */
                    dato.resumenFactura.tipoCambio = this.txtTipoCambio.Text;
                    dato.resumenFactura.codigoMoneda = this.cmbTipoMoneda.Value.ToString();

                    /*
                    dato.subTotal = producto.precio * dato.cantidad;
                    dato.montoDescuento = montoDescuento;
                    dato.montoTotal = dato.subTotal - dato.montoDescuento;
                    */
                    dato.resumenFactura.totalServGravados = 0;
                    dato.resumenFactura.totalServExentos = dato.detalleServicio.lineaDetalle.Sum(x => x.subTotal);
                    dato.resumenFactura.totalMercanciasGravadas = 0;
                    dato.resumenFactura.totalMercanciasExentas = 0;
                    dato.resumenFactura.totalGravado = 0;
                    dato.resumenFactura.totalExento = dato.detalleServicio.lineaDetalle.Sum(x => x.subTotal);
                    dato.resumenFactura.totalVenta = dato.detalleServicio.lineaDetalle.Sum(x => x.subTotal);
                    dato.resumenFactura.totalDescuentos = dato.detalleServicio.lineaDetalle.Sum(x => x.montoDescuento);
                    dato.resumenFactura.totalVentaNeta = dato.detalleServicio.lineaDetalle.Sum(x => x.montoTotal);
                    dato.resumenFactura.totalImpuesto = 0;
                    dato.resumenFactura.totalComprobante = dato.detalleServicio.lineaDetalle.Sum(x => x.montoTotal); // + impuesto

                    dato.clave = "50608011800060354097400100001010000000018188888888";
                    dato.numeroConsecutivo = "00100001010000000018";

                    string xml = EncodeXML.EncondeXML.getXMLFromObject(dato);
                    string responsePost = "";
                    enviarDocumentoElectronico(xml, responsePost);

                    if (responsePost.Equals("Success")) { 
                        this.lblMensaje.Text = String.Format("Factura #{0} enviada.",dato.numeroConsecutivo);
                        this.lblMensaje.CssClass = "colorAzul";
                    }
                    else
                    {
                        this.lblMensaje.Text = String.Format("Factura #{0} con errores.", dato.numeroConsecutivo);
                        this.lblMensaje.CssClass = "colorRojo";
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                //refescar los datos
                this.refreshData();
            }

        }


        public async void enviarDocumentoElectronico(string xmlFile, string responsePost )
        {
            using (var conexion = new DataModelOAuth2())
            {
                EmisorReceptorIMEC emisor = (EmisorReceptorIMEC)base.Session["emisor"];
                string ambiente = ConfigurationManager.AppSettings["ENVIROMENT"].ToString();
                OAuth2.OAuth2Config config = conexion.OAuth2Config.Where(x => x.enviroment == ambiente).FirstOrDefault();
                config.username = emisor.usernameOAuth2;
                config.password = emisor.passwordOAuth2;

                await OAuth2.OAuth2Config.getTokenWeb(config);

                WSDomain.WSRecepcionPOST trama = new WSDomain.WSRecepcionPOST();
                trama.clave = EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(xmlFile), "Clave", xmlFile);

                string emisorIdentificacion = EncondeXML.buscarValorEtiquetaXML("Emisor", "Identificacion", xmlFile);
                trama.emisor.tipoIdentificacion = emisorIdentificacion.Substring(0, 2);
                trama.emisor.numeroIdentificacion = emisorIdentificacion.Substring(2);
                trama.emisorTipo = trama.emisor.tipoIdentificacion;
                trama.emisorIdentificacion = trama.emisor.numeroIdentificacion;
                
                string receptorIdentificacion = EncondeXML.buscarValorEtiquetaXML("Receptor", "Identificacion", xmlFile);
                trama.receptor.tipoIdentificacion = receptorIdentificacion.Substring(0, 2);
                trama.receptor.numeroIdentificacion = receptorIdentificacion.Substring(2);
                trama.receptorTipo = trama.receptor.tipoIdentificacion;
                trama.receptorIdentificacion = trama.receptor.numeroIdentificacion;

                using (var conexionFE = new DataModelFE())
                {
                    EmisorReceptorIMEC dato = conexionFE.EmisorReceptorIMEC.Where(x => x.identificacion == trama.emisor.numeroIdentificacion).FirstOrDefault();
                    xmlFile = FirmaXML.getXMLFirmadoWeb(xmlFile, dato.llaveCriptografica, dato.claveLlaveCriptografica); 
                }

                trama.comprobanteXml = EncodeXML.EncondeXML.base64Encode(xmlFile);

                string jsonTrama = JsonConvert.SerializeObject(trama);

                using (var conexion2 = new DataModelWS())
                {
                    WSRecepcionPOST tramaObjeto = JsonConvert.DeserializeObject<WSRecepcionPOST>(jsonTrama);
                    conexion2.WSRecepcionPOST.Add(tramaObjeto);
                    conexion2.SaveChanges();
                }

                
                await Services.postRecepcion(config.token, jsonTrama, responsePost);
            }
        } 
            

        public void crearModificarReceptor()
        {
            try
            {
                using (var conexion = new DataModelFE())
                {

                    bool existe = true;
                    string buscar = this.txtReceptorIdentificacion.Text;
                    EmisorReceptorIMEC receptor = conexion.EmisorReceptorIMEC.Where(x => x.identificacion == buscar).FirstOrDefault();

                    if (receptor == null)
                    {
                        existe = false;
                        receptor = new EmisorReceptorIMEC();
                    }

                    receptor.identificacionTipo = this.cmbReceptorTipo.Value.ToString();
                    receptor.identificacion = this.txtReceptorIdentificacion.Text;
                    receptor.nombre = this.txtReceptorNombre.Text;
                    receptor.nombreComercial = this.txtReceptorNombreComercial.Text;

                    if (this.cmbReceptorTelefonoCod != null)
                    {
                        receptor.telefonoCodigoPais = this.cmbReceptorTelefonoCod.Value.ToString();
                        receptor.telefono = this.txtReceptorTelefono.Value.ToString();
                    }

                    receptor.correoElectronico = this.txtReceptorCorreo.Text;

                    if (this.cmbReceptorFaxCod.Value != null)
                    {
                        receptor.faxCodigoPais = this.cmbReceptorFaxCod.Value.ToString();
                        receptor.fax = this.txtReceptorFax.Value.ToString();
                    }

                    if (this.cmbReceptorProvincia.Value != null)
                    {
                        receptor.provincia = this.cmbReceptorProvincia.Value.ToString();
                    }
                    if (this.cmbReceptorCanton.Value != null)
                    {
                        receptor.canton = this.cmbReceptorCanton.Value.ToString();
                    }
                    if (this.cmbReceptorDistrito.Value != null)
                    {
                        receptor.distrito = this.cmbReceptorDistrito.Value.ToString();
                    }
                    if (this.cmbReceptorBarrio.Value != null)
                    {
                        receptor.barrio = this.cmbReceptorBarrio.Value.ToString();
                    }
                    receptor.otraSena = this.txtReceptorOtraSenas.Text;

                    //modifica el recetor
                    if (existe)
                    {
                        conexion.Entry(receptor).State = EntityState.Modified;
                    }
                    else//crea el receptor
                    {
                        conexion.EmisorReceptorIMEC.Add(receptor);
                    }
                    conexion.SaveChanges();
                }


            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // conexion.CodigoReferencia.Remove(conexion.CodigoReferencia.Last() );

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(fullErrorMessage, ex.EntityValidationErrors);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
    } 
}