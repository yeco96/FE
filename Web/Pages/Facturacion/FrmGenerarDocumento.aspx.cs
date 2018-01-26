using Class.Utilidades;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;
using Web.Utils;
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
            if(Session["detalleServicio"]!=null)
            {
                DetalleServicio detalleServicio = (DetalleServicio) Session["detalleServicio"];
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
                EmisorReceptor emisor = conexion.EmisorReceptor.Where(x => x.identificacion == elEmisor).FirstOrDefault();
                this.loadEmisor(emisor);

                string elReceptor = "601230863";
                EmisorReceptor receptor = conexion.EmisorReceptor.Where(x => x.identificacion == elReceptor).FirstOrDefault();
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
                foreach (var item in conexion.Ubicacion.Select(x=> new { x.codProvincia ,x.nombreProvincia } ).Distinct() )
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

                GridViewDataComboBoxColumn comboCodigo = this.ASPxGridView1.Columns["codigo"] as GridViewDataComboBoxColumn;
                comboCodigo.PropertiesComboBox.Items.Clear(); 
                foreach (var item in conexion.Producto.Where(x => x.estado == Estado.ACTIVO.ToString()).Where( x => x.emisor == elEmisor).ToList())
                {
                    this.cmbTipoMoneda.Items.Add(item.descripcion, item.codigo);
                }
                comboCodigo.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            }
        }

        /// <summary>
        /// carga datos del emisor
        /// </summary>
        /// <param name="emisor"></param>
        private void loadEmisor(EmisorReceptor emisor)
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

            this.cmbEmisorProvincia_ValueChanged(null,null);
            this.cmbEmisorCanton.Value = emisor.canton;

            this.cmbEmisorCanton_ValueChanged(null, null);
            this.cmbEmisorDistrito.Value = emisor.distrito;

            this.cmbEmisorDistrito_ValueChanged(null, null);
            this.cmbEmisorBarrio.Value = emisor.barrio;
            this.txtEmisorOtraSenas.Value = emisor.otraSena;
            
        }


        private void loadReceptor(EmisorReceptor emisor)
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

        protected void btnFacturar_Click(object sender, EventArgs e)
        {

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
            if (CondicionVenta.CREDITO.Equals(this.cmbCondicionVenta.Value.ToString()))
            {
                this.cmbPlazoCredito.Enabled = true;
                this.cmbPlazoCredito.Value = 3;
            }
            else
            {
                this.cmbPlazoCredito.Value = 0;
                this.cmbPlazoCredito.Enabled = false;
            }
        }

        protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        { 
            if (e.Column.FieldName == "subTotal") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
            if (e.Column.FieldName == "montoTotal") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
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
                    string codProducto = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString().ToUpper() : null;
                    Producto producto = conexion.Producto.Where(x => x.codigo == codProducto).FirstOrDefault();

                    dato.numeroLinea = e.NewValues["numeroLinea"] != null ? int.Parse(e.NewValues["numeroLinea"].ToString()) : 0;
                    dato.cantidad = e.NewValues["cantidad"] != null ? double.Parse(e.NewValues["cantidad"].ToString()) : 0;
                    dato.codigo.tipo = producto.tipo;
                    dato.codigo.codigo = producto.codigo;
                    dato.precioUnitario = producto.precio;
                    dato.subTotal = producto.precio * dato.cantidad;
                    dato.montoDescuento = e.NewValues["montoDescuento"] != null ? double.Parse(e.NewValues["montoDescuento"].ToString()) : 0;
                    dato.montoTotal = dato.subTotal - dato.montoDescuento;

                    dato.naturalezaDescuento = e.NewValues["naturalezaDescuento"] != null ? e.NewValues["naturalezaDescuento"].ToString().ToUpper() : null;
  
                    //agrega el objeto
                    detalleServicio.lineaDetalle.Add(dato);
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

                    string codProducto = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString().ToUpper() : null;
                    LineaDetalle dato = detalleServicio.lineaDetalle.Where(x => x.codigo.codigo == codProducto).FirstOrDefault();
                    //llena el objeto con los valores de la pantalla
                    
                    Producto producto = conexion.Producto.Where(x => x.codigo == codProducto).FirstOrDefault();

                    dato.cantidad = e.NewValues["cantidad"] != null ? double.Parse(e.NewValues["cantidad"].ToString()) : 0;
                    dato.codigo.tipo = producto.tipo;
                    dato.codigo.codigo = producto.codigo;
                    dato.precioUnitario = producto.precio;
                    dato.subTotal = producto.precio * dato.cantidad;
                    dato.montoDescuento = e.NewValues["montoDescuento"] != null ? double.Parse(e.NewValues["montoDescuento"].ToString()) : 0;
                    dato.montoTotal = dato.subTotal - dato.montoDescuento;

                    dato.naturalezaDescuento = e.NewValues["naturalezaDescuento"] != null ? e.NewValues["naturalezaDescuento"].ToString().ToUpper() : null;

                     
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
    }
}