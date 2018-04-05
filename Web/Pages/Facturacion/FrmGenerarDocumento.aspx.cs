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
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Administracion;
using Web.Models.Catalogos;
using Web.Models.Facturacion;
using Web.Utils;
using Web.WebServices;
using WSDomain;
using XMLDomain;

namespace Web.Pages.Facturacion
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmGenerarDocumento : System.Web.UI.Page
    {
        private DetalleServicio detalleServicio;
        private List<InformacionReferencia> informacionReferencia;

        public FrmGenerarDocumento()
        {
            this.detalleServicio = new DetalleServicio();
            this.informacionReferencia = new List<InformacionReferencia>();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            try
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Pages/Login.aspx");
                }

                this.alertMessages.Attributes["class"]="";
                this.alertMessages.InnerText = "";
                 
                this.AsyncMode = true;

                EmisorReceptorIMEC elEmisor = (EmisorReceptorIMEC)Session["elEmisor"];
                if (!Utilidades.verificaDatosHacienda(elEmisor))
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "Se requiere configurar datos del emisor";
                    return;
                }

                //Se obtiene datos del emisor
                using (var conexionPlan = new DataModelFE())
                {
                    string emisorPlan = Session["emisor"].ToString();
                    Plan dato = conexionPlan.Plan.Where(x => x.emisor == emisorPlan && x.estado == "ACTIVO").FirstOrDefault();
                    Session["fechaVencimientoPlan"] = dato.fechaFin.ToString();
                    Session["documentosPendPlan"] = int.Parse(dato.cantidadDocPlan.ToString()) - int.Parse(dato.cantidadDocEmitido.ToString());
                    Session["PlanPago"] = dato.plan.ToString();

                    //Mensajes
                    this.alertMessages.Attributes["class"] = "alert alert-info";

                    if (Session["PlanPago"].ToString() != "PPRO1")
                    {
                        this.alertMessages.InnerText = "Plan: " + dato.plan.ToString() + "; Documentos Pendientes: " + Session["documentosPendPlan"].ToString() + "; Fecha de Vencimiento: " + DateTime.Parse(dato.fechaFin.ToString()).ToShortDateString();
                    }
                    else {
                        //Si el plan es PLAN PROFESIONAL SIN LIMITE entonces se le agrega 1 documento
                        Session["documentosPendPlan"] = 1;
                        this.alertMessages.InnerText = "Plan: " + dato.plan.ToString() + "; Fecha de Vencimiento: " + DateTime.Parse(dato.fechaFin.ToString()).ToShortDateString();
                    }

                    //
                    //int dias = int.Parse(Session["documentosPendPlan"].ToString());
                    //DateTime fechaVenc = DateTime.Parse(Session["fechaVencimientoPlan"].ToString());
                    if (int.Parse(Session["documentosPendPlan"].ToString()) <= 0 || (DateTime.Today >= DateTime.Parse(Session["fechaVencimientoPlan"].ToString())))
                    {
                        //Colocar el mensaje
                        this.alertMessages.Attributes["class"] = "alert alert-danger";
                        this.alertMessages.InnerText = "Su plan ha expirado, favor contactenos para renovar su plan.";
                        this.btnFacturar.Enabled = false;
                        return;
                    }


                }//Fin del Using

                    if (!IsCallback && !IsPostBack)
                {
                    this.txtFechaEmision.Date = Date.DateTimeNow();
                    this.txtFechaEmision.MinDate = Date.DateTimeNow().AddHours(-48);
                    this.txtFechaEmision.MaxDate = Date.DateTimeNow();
                    
                    this.cmbTipoMoneda.Value = TipoMoneda.CRC;
                    this.txtTipoCambio.Text = "1";
                    this.loadComboBox();

                    this.detalleServicio = new DetalleServicio();

                    using (var conexion = new DataModelFE())
                    {
                        string emisor = Session["emisor"].ToString();
                        foreach (var producto in conexion.Producto.Where(x=> x.estado==Estado.ACTIVO.ToString() && x.cargaAutFactura==Confirmacion.SI.ToString() && x.emisor==emisor).
                            OrderBy(x=>x.orden))
                        {
                            LineaDetalle dato = new LineaDetalle();
                            dato.numeroLinea = this.detalleServicio.lineaDetalle.Count+1;
                            dato.numeroLinea = detalleServicio.lineaDetalle.Count + 1;
                            dato.cantidad = 1;
                            dato.codigo.tipo = producto.tipo;
                            dato.codigo.codigo = producto.codigo;
                            dato.detalle = producto.descripcion.ToUpper();
                            dato.unidadMedida = producto.unidadMedida;
                            dato.unidadMedidaComercial = "";
                            dato.tipoServMerc = producto.tipoServMerc;
                            dato.producto = producto.codigo;/*solo para uso del grid*/
                            dato.precioUnitario = producto.precio;
                            dato.montoDescuento = 0;
                            dato.naturalezaDescuento = "N/A";
                            dato.calcularMontos();
                            int idProducto = producto.id;
                            using (var conexion2 = new DataModelFE())
                            {
                                foreach (var item in conexion2.ProductoImpuesto.Where(x => x.idProducto == idProducto).OrderByDescending(x => x.tipoImpuesto))
                                {
                                    if (TipoImpuesto.IMPUESTO_VENTA.Equals(item.tipoImpuesto))
                                    {
                                        dato.impuestos.Add(new Impuesto(item.tipoImpuesto, item.porcentaje, dato.montoTotalLinea));
                                    }
                                    else
                                    {
                                        dato.impuestos.Add(new Impuesto(item.tipoImpuesto, item.porcentaje, dato.subTotal));
                                    }
                                    dato.calcularMontos();
                                }
                            }
                            /*EXONERACION*/
                            dato = this.verificaExoneracion(dato);
                            dato.calcularMontos();

                            this.detalleServicio.lineaDetalle.Add(dato);
                        }

                        Empresa empresa = conexion.Empresa.Find(emisor);
                        if (empresa != null)
                        {
                            this.cmbCondicionVenta.Value = empresa.condicionVenta;
                            this.cmbMedioPago.Value = empresa.medioPago;
                            this.cmbTipoMoneda.Value = empresa.moneda;
                            this.txtPlazoCredito.Text = empresa.plazoCredito.ToString();
                            this.cmbMoneda_ValueChanged(sender, e);
                        }

                    }
                    Session["detalleServicio"] = detalleServicio;

                    this.informacionReferencia =  new List<InformacionReferencia>();
                    Session["informacionReferencia"] = informacionReferencia;
                }
                this.refreshData();
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
        }

        #region METODOS

        public EmisorReceptorIMEC crearModificarReceptor(EmisorReceptorIMEC receptor)
        {
            try
            { 
                
                if ( ((ASPxComboBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("cmbReceptorTipo")).Value != null)
                {
                    receptor.identificacionTipo = ((ASPxComboBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("cmbReceptorTipo")).Value.ToString();
                }
                if (!string.IsNullOrWhiteSpace(((ASPxTextBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorNombre")).Text))
                    receptor.nombre = ((ASPxTextBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorNombre")).Text.ToUpper();

                if (!string.IsNullOrWhiteSpace(((ASPxTextBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorNombreComercial")).Text))
                    receptor.nombreComercial = ((ASPxTextBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorNombreComercial")).Text.ToUpper();

                if (((ASPxComboBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("cmbReceptorTelefonoCod")).Value != null)
                {
                    receptor.telefonoCodigoPais = ((ASPxComboBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("cmbReceptorTelefonoCod")).Value.ToString();
                    receptor.telefono = ((ASPxSpinEdit)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("txtReceptorTelefono")).Value.ToString();
                }

                if (!string.IsNullOrWhiteSpace(((ASPxTextBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("txtReceptorCorreo")).Text))
                {
                    receptor.correoElectronico = ((ASPxTextBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("txtReceptorCorreo")).Text.ToLower();
                }

                if (((ASPxComboBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("cmbReceptorFaxCod")).Value != null)
                {
                    receptor.faxCodigoPais = ((ASPxComboBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("cmbReceptorFaxCod")).Value.ToString();
                    receptor.fax = ((ASPxSpinEdit)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("txtReceptorFax")).Value.ToString();
                }

                if (((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorProvincia")).Value != null)
                {
                    receptor.provincia = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorProvincia")).Value.ToString();
                }
                if (((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorCanton")).Value != null)
                {
                    receptor.canton = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorCanton")).Value.ToString();
                }
                if (((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorDistrito")).Value != null)
                {
                    receptor.distrito = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorDistrito")).Value.ToString();
                }
                if (((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorBarrio")).Value != null)
                {
                    receptor.barrio = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorBarrio")).Value.ToString();
                }
                if (!string.IsNullOrWhiteSpace(((ASPxMemo)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("txtReceptorOtraSenas")).Text))
                {
                    receptor.otraSena = ((ASPxMemo)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("txtReceptorOtraSenas")).Text.ToUpper();
                }


            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
            return receptor;
        }

         

        protected void UpdatePanel_Unload(object sender, EventArgs e)
        {
            RegisterUpdatePanel((UpdatePanel)sender);
        }

        public LineaDetalle verificaExoneracion(LineaDetalle dato)
        {
            ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
            if (tabs != null)
            {
                ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutExoneracion");
                /* EXONERACION */
                ASPxComboBox cmbTipoDocumento = (ASPxComboBox)form.FindControl("cmbTipoDocumento");
                ASPxTextBox numeroDocumento = (ASPxTextBox)form.FindControl("numeroDocumento");
                ASPxTextBox nombreInstitucion = (ASPxTextBox)form.FindControl("nombreInstitucion");
                ASPxDateEdit fechaEmision = (ASPxDateEdit)form.FindControl("fechaEmision");
                ASPxSpinEdit porcentajeCompra = (ASPxSpinEdit)form.FindControl("porcentajeCompra");
                ASPxSpinEdit montoImpuesto = (ASPxSpinEdit)form.FindControl("montoImpuesto");

                if (cmbTipoDocumento.Value != null && !string.IsNullOrWhiteSpace(numeroDocumento.Text) && !string.IsNullOrWhiteSpace(nombreInstitucion.Text)
                    && !string.IsNullOrWhiteSpace(porcentajeCompra.Text) && !string.IsNullOrWhiteSpace(fechaEmision.Text))
                {
                    foreach (var item in dato.impuestos)
                    {
                        item.exoneracion.tipoDocumento = cmbTipoDocumento.Value.ToString();
                        item.exoneracion.numeroDocumento = numeroDocumento.Text;
                        item.exoneracion.nombreInstitucion = nombreInstitucion.Text;
                        item.exoneracion.fechaEmision = fechaEmision.Date.ToString("yyyy-MM-ddTHH:mm:ss-06:00");
                        item.exoneracion.porcentajeCompra = int.Parse(porcentajeCompra.Text);
                        // item.exoneracion.montoImpuesto =  item.monto * (item.exoneracion.porcentajeCompra / new decimal(100.0));

                        //modifica el monto
                        item.monto = item.monto - item.exoneracion.montoImpuesto;
                    }
                }
            }
            return dato;
        }
        protected void RegisterUpdatePanel(UpdatePanel panel)
        {
            var sType = typeof(ScriptManager);
            var mInfo = sType.GetMethod("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel", BindingFlags.NonPublic | BindingFlags.Instance);
            if (mInfo != null)
                mInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { panel });
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

            if (Session["informacionReferencia"] != null)
            {
                List<InformacionReferencia> informacionReferencia = (List<InformacionReferencia>)Session["informacionReferencia"];
                this.ASPxGridView2.DataSource = informacionReferencia;
                this.ASPxGridView2.DataBind();
            }

            using (var conexion = new DataModelFE())
            { 
                /* EMISOR */
                string emisor = Session["emisor"].ToString();
                this.ASPxGridViewClientes.DataSource = (from Emisor in conexion.EmisorReceptorIMEC
                                                        from cliente in conexion.Cliente
                                                        where Emisor.identificacion == cliente.receptor && cliente.emisor == emisor
                                                        select Emisor
                                             ).ToList();
                conexion.EmisorReceptorIMEC.ToList();
                this.ASPxGridViewClientes.DataBind();
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
                string emisor = Session["emisor"].ToString();
                 
                /* IDENTIFICACION TIPO */
                GridViewDataComboBoxColumn comboIdentificacionTipo = this.ASPxGridViewClientes.Columns["identificacionTipo"] as GridViewDataComboBoxColumn;
                foreach (var item in conexion.TipoIdentificacion.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    ((ASPxComboBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("cmbReceptorTipo")).Items.Add(item.descripcion, item.codigo);
                    comboIdentificacionTipo.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
                ((ASPxComboBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("cmbReceptorTipo")).IncrementalFilteringMode = IncrementalFilteringMode.Contains;


                /* CODIGO PAIS */
                foreach (var item in conexion.CodigoPais.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {

                    ((ASPxComboBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("cmbReceptorTelefonoCod")).Items.Add(item.descripcion, item.codigo);
                    ((ASPxComboBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("cmbReceptorFaxCod")).Items.Add(item.descripcion, item.codigo);
                }

                ((ASPxComboBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("cmbReceptorTelefonoCod")).IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                ((ASPxComboBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("cmbReceptorFaxCod")).IncrementalFilteringMode = IncrementalFilteringMode.Contains;


                /* PROVINCIA*/
                foreach (var item in conexion.Ubicacion.Select(x => new { x.codProvincia, x.nombreProvincia }).Distinct())
                {
                    ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorProvincia")).Items.Add(item.nombreProvincia, item.codProvincia);
                }
                ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorProvincia")).IncrementalFilteringMode = IncrementalFilteringMode.Contains;

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

                /* PRODUCTO */
                GridViewDataComboBoxColumn comboProducto = this.ASPxGridView1.Columns["producto"] as GridViewDataComboBoxColumn;
                comboProducto.PropertiesComboBox.Items.Clear();
                foreach (var item in conexion.Producto.Where(x => x.estado == Estado.ACTIVO.ToString()).Where(x => x.emisor == emisor).ToList())
                {
                    comboProducto.PropertiesComboBox.Items.Add(item.ToString(), item.codigo);
                }
                comboProducto.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                
                /* TIPO DOCUMENTO */
                GridViewDataComboBoxColumn comboTipoDocumento = this.ASPxGridView2.Columns["tipoDocumento"] as GridViewDataComboBoxColumn;
                foreach (var item in conexion.TipoDocumento.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    this.cmbTipoDocumento.Items.Add(item.descripcion, item.codigo);
                    comboTipoDocumento.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
                this.cmbTipoDocumento.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                comboTipoDocumento.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbTipoDocumento.SelectedIndex = 0;

                /* SUCURSAL CAJA */
                foreach (var item in conexion.ConsecutivoDocElectronico.Where(x => x.emisor == emisor && x.estado == Estado.ACTIVO.ToString() && x.tipoDocumento== cmbTipoDocumento.Value.ToString()).ToList())
                {
                    this.cmbSucursalCaja.Items.Add(item.ToString(), string.Format("{0}{1}", item.sucursal, item.caja));
                }
                this.cmbSucursalCaja.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbSucursalCaja.SelectedIndex = 0;


                /* CODIGO REFERENCIA */
                GridViewDataComboBoxColumn comboCodigo = this.ASPxGridView2.Columns["codigo"] as GridViewDataComboBoxColumn;
                foreach (var item in conexion.CodigoReferencia.Where(x => x.estado == Estado.ACTIVO.ToString() && x.aplicaFacturas == Confirmacion.SI.ToString()).ToList())
                {
                    comboCodigo.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
                comboCodigo.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;



            }
        }

        private void loadReceptor(EmisorReceptorIMEC emisor)
        { 

            ASPxComboBox cmbReceptorTipo = ((ASPxComboBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("cmbReceptorTipo"));
            ASPxSpinEdit txtReceptorIdentificacion = ((ASPxSpinEdit)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorIdentificacion"));
            ASPxTextBox txtReceptorNombre = ((ASPxTextBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorNombre"));
            ASPxTextBox txtReceptorNombreComercial = ((ASPxTextBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorNombreComercial"));

            ASPxComboBox cmbReceptorTelefonoCod = ((ASPxComboBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("cmbReceptorTelefonoCod"));
            ASPxComboBox cmbReceptorFaxCod = ((ASPxComboBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("cmbReceptorFaxCod"));
            ASPxSpinEdit txtReceptorTelefono = ((ASPxSpinEdit)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("txtReceptorTelefono"));
            ASPxSpinEdit txtReceptorFax = ((ASPxSpinEdit)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("txtReceptorFax"));
            ASPxTextBox txtReceptorCorreo = ((ASPxTextBox)acordionReceptor.Groups[1].FindControl("ASPxFormLayout").FindControl("txtReceptorCorreo"));

            ASPxComboBox cmbReceptorProvincia = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorProvincia"));
            ASPxComboBox cmbReceptorCanton = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorCanton"));
            ASPxComboBox cmbReceptorDistrito = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorDistrito"));
            ASPxComboBox cmbReceptorBarrio = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorBarrio"));
            ASPxMemo txtReceptorOtraSenas = ((ASPxMemo)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("txtReceptorOtraSenas"));
            

            cmbReceptorTipo.Value = emisor.identificacionTipo;
            txtReceptorIdentificacion.Text = emisor.identificacion;
            txtReceptorNombre.Text = emisor.nombre;
            txtReceptor.Text = emisor.nombre;
            if (emisor.nombreComercial != null)
            {
                txtReceptorNombreComercial.Text = emisor.nombreComercial.ToUpper();
            }
            cmbReceptorTelefonoCod.Value = emisor.telefonoCodigoPais;
            cmbReceptorFaxCod.Value = emisor.faxCodigoPais;
            txtReceptorTelefono.Value = emisor.telefono;
            txtReceptorFax.Value = emisor.fax;
            if (emisor.correoElectronico != null)
            {
                txtReceptorCorreo.Text = emisor.correoElectronico.ToLower();
            }
            cmbReceptorProvincia.Value = emisor.provincia;

            if (emisor.provincia != null)
            {
                cmbReceptorProvincia_ValueChanged(null, null);
                cmbReceptorCanton.Value = emisor.canton;

                cmbReceptorCanton_ValueChanged(null, null);
                cmbReceptorDistrito.Value = emisor.distrito;

                cmbReceptorDistrito_ValueChanged(null, null);
                cmbReceptorBarrio.Value = emisor.barrio;
            }
            if (emisor.otraSena != null)
            {
                txtReceptorOtraSenas.Value = emisor.otraSena.ToUpper();
            }
        }

        protected void cmbReceptorProvincia_ValueChanged(object sender, EventArgs e)
        { 
            ASPxComboBox cmbReceptorProvincia = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorProvincia"));
            ASPxComboBox cmbReceptorCanton = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorCanton"));
            ASPxComboBox cmbReceptorDistrito = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorDistrito")); 

            using (var conexion = new DataModelFE())
            {
                cmbReceptorDistrito.SelectedItem = null;
                cmbReceptorDistrito.Items.Clear();
                cmbReceptorCanton.SelectedItem = null;

                cmbReceptorCanton.Items.Clear();
                foreach (var item in conexion.Ubicacion.Where(x => x.codProvincia == cmbReceptorProvincia.Value.ToString()).Select(x => new { x.codCanton, x.nombreCanton }).Distinct())
                {
                    cmbReceptorCanton.Items.Add(item.nombreCanton, item.codCanton);
                }
                cmbReceptorCanton.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }

        protected void cmbReceptorCanton_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            { 
                ASPxComboBox cmbReceptorProvincia = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorProvincia"));
                ASPxComboBox cmbReceptorCanton = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorCanton"));
                ASPxComboBox cmbReceptorDistrito = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorDistrito")); 

                cmbReceptorDistrito.SelectedItem = null;
                cmbReceptorDistrito.Items.Clear();
                foreach (var item in conexion.Ubicacion.
                    Where(x => x.codProvincia == cmbReceptorProvincia.Value.ToString()).
                    Where(x => x.codCanton == cmbReceptorCanton.Value.ToString()).
                    Select(x => new { x.codDistrito, x.nombreDistrito }).Distinct())
                {
                    cmbReceptorDistrito.Items.Add(item.nombreDistrito, item.codDistrito);
                }
                cmbReceptorDistrito.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }

        protected void cmbReceptorDistrito_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                 
                ASPxComboBox cmbReceptorProvincia = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorProvincia"));
                ASPxComboBox cmbReceptorCanton = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorCanton"));
                ASPxComboBox cmbReceptorDistrito = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorDistrito"));
                ASPxComboBox cmbReceptorBarrio = ((ASPxComboBox)acordionReceptor.Groups[2].FindControl("ASPxFormLayout").FindControl("cmbReceptorBarrio")); 

                cmbReceptorBarrio.SelectedItem = null;
                cmbReceptorBarrio.Items.Clear();
                foreach (var item in conexion.Ubicacion.
                    Where(x => x.codProvincia == cmbReceptorProvincia.Value.ToString()).
                    Where(x => x.codCanton == cmbReceptorCanton.Value.ToString()).
                     Where(x => x.codDistrito == cmbReceptorDistrito.Value.ToString()).
                    Select(x => new { x.codBarrio, x.nombreBarrio }).Distinct())
                {
                    cmbReceptorBarrio.Items.Add(item.nombreBarrio, item.codBarrio);
                }
                cmbReceptorBarrio.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }

        protected void cmbMoneda_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TipoMoneda.CRC.Equals(this.cmbTipoMoneda.Value.ToString()))
                {
                    this.txtTipoCambio.Enabled = false;
                    this.txtTipoCambio.Value = 1;
                }
                else
                {
                    this.txtTipoCambio.Enabled = true;
                    this.txtTipoCambio.Value = BCCR.tipoCambioDOLAR(this.txtFechaEmision.Date);
                }
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = "En este momento no se puede establecer comunicación con el BANCO CENTRAL DE CR, favor digite el tipo de cambio a utilizar";

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


        #endregion

        #region METODOS DEL GRID
        protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (this.ASPxGridView1.IsNewRowEditing)
            {
                if (e.Column.FieldName == "subTotal") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "montoTotal") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "montoTotalLinea") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "montoDescuento") { e.Editor.Value = 0; }
                if (e.Column.FieldName == "precioUnitario") { e.Editor.Value = 0; }
                if (e.Column.FieldName == "cantidad") { e.Editor.Value = 1; }
                if (e.Column.FieldName == "naturalezaDescuento") { e.Editor.Value = "N/A"; }
            }
            else
            {
                if (e.Column.FieldName == "montoTotalLinea") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "subTotal") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "montoTotal") { e.Editor.Value = 0; e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "producto") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
            }

            if (e.Column.FieldName == "producto")
            {
                /* TIPO EXONERACIÓN */
                ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
                ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutExoneracion");
                ASPxComboBox cmbTipoDocumento = (ASPxComboBox)form.FindControl("cmbTipoDocumento");
                using (var conexion = new DataModelFE())
                {
                    foreach (var item in conexion.Exoneracion.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                    {
                        cmbTipoDocumento.Items.Add(item.descripcion, item.codigo);
                    }
                }
                cmbTipoDocumento.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
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
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
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

                    dato.numeroLinea = detalleServicio.lineaDetalle.Count + 1;
                    dato.cantidad = e.NewValues["cantidad"] != null ? decimal.Parse(e.NewValues["cantidad"].ToString()) : 0;
                    dato.codigo.tipo = producto.tipo;
                    dato.codigo.codigo = producto.codigo;
                    dato.detalle = producto.descripcion;
                    dato.unidadMedida = producto.unidadMedida;
                    dato.unidadMedidaComercial = "";

                    decimal precio = "0".Equals(e.NewValues["precioUnitario"].ToString()) ? producto.precio : decimal.Parse(e.NewValues["precioUnitario"].ToString());

                    dato.tipoServMerc = producto.tipoServMerc;
                    dato.producto = producto.codigo;/*solo para uso del grid*/
                    dato.precioUnitario = precio;
                    dato.montoDescuento = e.NewValues["montoDescuento"] != null ? decimal.Parse(e.NewValues["montoDescuento"].ToString()) : 0;

                    if (dato.montoDescuento > (dato.precioUnitario * dato.cantidad))
                    {
                        throw new Exception("El descuento no puede ser mayor al total de la linea");
                    }


                    dato.calcularMontos();
                    dato.impuestos.Clear();
                    foreach (var item in conexion.ProductoImpuesto.Where(x => x.idProducto == producto.id).OrderByDescending(x => x.tipoImpuesto))
                    {
                        if (TipoImpuesto.IMPUESTO_VENTA.Equals(item.tipoImpuesto))
                        {
                            dato.impuestos.Add(new Impuesto(item.tipoImpuesto, item.porcentaje, dato.montoTotalLinea));
                        }
                        else
                        {
                            dato.impuestos.Add(new Impuesto(item.tipoImpuesto, item.porcentaje, dato.subTotal));
                        }
                        dato.calcularMontos();
                    }
                    /*EXONERACION*/
                    dato = this.verificaExoneracion(dato);
                    dato.calcularMontos();



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

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(fullErrorMessage, ex.EntityValidationErrors);

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

                    string codProducto = e.NewValues["producto"] != null ? e.NewValues["producto"].ToString().ToUpper() : null;
                    LineaDetalle dato = detalleServicio.lineaDetalle.Where(x => x.codigo.codigo == codProducto).FirstOrDefault();
                    //llena el objeto con los valores de la pantalla

                    Producto producto = conexion.Producto.Where(x => x.codigo == codProducto).FirstOrDefault();

                    //dato.numeroLinea = detalleServicio.lineaDetalle.Count;
                    dato.cantidad = e.NewValues["cantidad"] != null ? decimal.Parse(e.NewValues["cantidad"].ToString()) : 0;
                    //dato.codigo.tipo = producto.tipo;
                    //dato.codigo.codigo = producto.codigo;
                    //dato.detalle = producto.descripcion;
                    //dato.unidadMedida = producto.unidadMedida;
                    dato.unidadMedidaComercial = "";

                    decimal precio = "0".Equals(e.NewValues["precioUnitario"].ToString()) ? producto.precio : decimal.Parse(e.NewValues["precioUnitario"].ToString());

                    dato.tipoServMerc = producto.tipoServMerc;
                    dato.producto = producto.codigo;/*solo para uso del grid*/
                    dato.precioUnitario = precio;
                    dato.montoDescuento = e.NewValues["montoDescuento"] != null ? decimal.Parse(e.NewValues["montoDescuento"].ToString()) : 0;

                    dato.calcularMontos();
                    dato.impuestos.Clear();
                    foreach (var item in conexion.ProductoImpuesto.Where(x => x.idProducto == producto.id).OrderByDescending(x => x.tipoImpuesto))
                    {
                        if (TipoImpuesto.IMPUESTO_VENTA.Equals(item.tipoImpuesto))
                        {
                            dato.impuestos.Add(new Impuesto(item.tipoImpuesto, item.porcentaje, dato.montoTotalLinea));
                        }
                        else
                        {
                            dato.impuestos.Add(new Impuesto(item.tipoImpuesto, item.porcentaje, dato.subTotal));
                        }
                        dato.calcularMontos();
                    }
                    /*EXONERACION*/
                    dato = this.verificaExoneracion(dato);
                    dato.calcularMontos();


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
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
            finally
            {
                //refescar los datos
                this.refreshData();
            }
        }

        protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    List<InformacionReferencia> informacionReferencia = (List<InformacionReferencia>)Session["informacionReferencia"];
                    var id = e.Values["numero"].ToString();
                    InformacionReferencia dato = informacionReferencia.Where(x => x.numero == id).FirstOrDefault();
                    informacionReferencia.Remove(dato);

                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridView2.CancelEdit();
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

        protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    List<InformacionReferencia> informacionReferencia = (List<InformacionReferencia>)Session["informacionReferencia"];

                    //se declara el objeto a insertar
                    InformacionReferencia dato = new InformacionReferencia();
                    //llena el objeto con los valores de la pantalla
                    string clave = e.NewValues["numero"] != null ? e.NewValues["numero"].ToString().ToUpper() : "";
                    WSRecepcionPOST documento = null;

                    if (clave.Length == 20)
                    {
                        documento = conexion.WSRecepcionPOST.Where(x => x.clave.Substring(21, 20) == clave).FirstOrDefault();
                    }
                    else
                    {
                        documento = conexion.WSRecepcionPOST.Find(clave);
                    }

                    if (documento != null)
                    {
                        dato.fechaEmision = ((DateTime)documento.fecha).ToString("yyyy-MM-dd");
                        dato.fechaEmisionTotal = ((DateTime)documento.fecha).ToString("yyyy-MM-ddTHH:mm:dd-06:00");
                        DateTime date = DateTime.Now;
                        if (!DateTime.TryParse(dato.fechaEmision, out date))
                        {
                            throw new Exception("Fecha invalida, favor verifique el formato yyyy-MM-dd (año-mes-día)");
                        }
                        dato.tipoDocumento = documento.tipoDocumento;
                        dato.numero = documento.clave;
                    }
                    else
                    {
                        dato.fechaEmision = e.NewValues["fechaEmision"].ToString();
                        dato.fechaEmisionTotal = e.NewValues["fechaEmision"].ToString() + DateTime.Now.ToString("THH:mm:dd-06:00");
                        dato.tipoDocumento = e.NewValues["tipoDocumento"] != null ? e.NewValues["tipoDocumento"].ToString().ToUpper() : "";
                        dato.numero = clave;
                    }


                    dato.razon = e.NewValues["razon"] != null ? e.NewValues["razon"].ToString().ToUpper() : null;
                    dato.codigo = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString().ToUpper() : null;

                    //agrega el objeto
                    informacionReferencia.Add(dato);
                    Session["informacionReferencia"] = informacionReferencia;

                }

                //esto es para el manero del devexpress
                e.Cancel = true;
                this.ASPxGridView2.CancelEdit();


            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(fullErrorMessage, ex.EntityValidationErrors);

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

        protected void ASPxGridView2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    List<InformacionReferencia> informacionReferencia = (List<InformacionReferencia>)Session["informacionReferencia"];

                    //se declara el objeto a insertar
                    InformacionReferencia dato = new InformacionReferencia();
                    //llena el objeto con los valores de la pantalla
                    string clave = e.NewValues["numero"] != null ? e.NewValues["numero"].ToString().ToUpper() : "";

                    dato = informacionReferencia.Where(x => x.numero == clave).FirstOrDefault();
                    if (dato != null)
                    {
                        dato.fechaEmision = e.NewValues["fechaEmision"].ToString();
                        DateTime date = DateTime.Now;
                        if (!DateTime.TryParse(dato.fechaEmision, out date))
                        {
                            throw new Exception("Fecha invalida, favor verifique el formato yyyy-MM-dd (año-mes-día)");
                        }
                        dato.tipoDocumento = e.NewValues["tipoDocumento"] != null ? e.NewValues["tipoDocumento"].ToString().ToUpper() : "";
                    }
                    dato.razon = e.NewValues["razon"] != null ? e.NewValues["razon"].ToString().ToUpper() : null;
                    dato.codigo = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString().ToUpper() : null;

                    //modifica el objeto
                    Session["informacionReferencia"] = informacionReferencia;
                }

                //esto es para el manero del devexpress
                e.Cancel = true;
                this.ASPxGridView2.CancelEdit();


            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(fullErrorMessage, ex.EntityValidationErrors);

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

        protected void ASPxGridView2_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (ASPxGridView2.IsNewRowEditing)
            {
                if (e.Column.FieldName == "tipoDocumento") { e.Editor.Value = "08"; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "fechaEmision") { e.Editor.Value = Date.DateTimeNow().ToString("yyyy-MM-dd"); e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "razon") { e.Editor.Value = "DETALLE DE REFERENCIA"; e.Editor.BackColor = System.Drawing.Color.White; }
            }
        }

        #endregion
        

        /// <summary>
        /// obtiene los valores para crear el documento electronico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected async void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            { 

                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                DetalleServicio detalle = (DetalleServicio)Session["detalleServicio"];

                if (string.IsNullOrWhiteSpace(((ASPxTextBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorNombre")).Text) ||
                    string.IsNullOrWhiteSpace(((ASPxSpinEdit)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorIdentificacion")).Text))
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "Debe agregar un receptor";
                    return;
                } 
                if (TipoIdentificacion.FISICA.Equals(((ASPxComboBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("cmbReceptorTipo")).Value.ToString()) 
                    && ((ASPxSpinEdit)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorIdentificacion")).Text.Length != 9)
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "La identificación debe tener de 9 digitos";
                    return;
                }
                if (TipoIdentificacion.JURIDICA.Equals(((ASPxComboBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("cmbReceptorTipo")).Value.ToString())
                    && ((ASPxSpinEdit)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorIdentificacion")).Text.Length != 10)
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "La identificación debe tener de 10 digitos";
                    return;
                }
                if (TipoIdentificacion.DIMEX.Equals(((ASPxComboBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("cmbReceptorTipo")).Value.ToString()) 
                    &&(((ASPxSpinEdit)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorIdentificacion")).Text.Length > 12 || ((ASPxTextBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorIdentificacion")).Text.Length < 11) )
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "La identificación debe tener en 11 y 12 digitos";
                    return;
                }
                if (TipoIdentificacion.NITE.Equals(((ASPxComboBox)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("cmbReceptorTipo")).Value.ToString()) 
                    && ((ASPxSpinEdit)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorIdentificacion")).Text.Length != 10)
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "La identificación debe tener de 10 digitos";
                    return;
                }

                if (detalle.lineaDetalle.Count == 0)
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "Debe agregar almenos una linea de detalle a la factura";
                    return;
                }
                else
                {
                    decimal total = detalle.lineaDetalle.Sum(x => x.montoTotalLinea);
                    if (total <= 0)
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-danger";
                        this.alertMessages.InnerText = "No se puede realizar una factura sin montos";
                        return;
                    }
                }


                using (var conexion = new DataModelFE())
                {

                    DocumentoElectronico dato = new DocumentoElectronico();
                    if (TipoDocumento.FACTURA_ELECTRONICA.Equals(this.cmbTipoDocumento.Value))
                    {
                        dato = new FacturaElectronica();
                    }
                    if (TipoDocumento.TIQUETE_ELECTRONICO.Equals(this.cmbTipoDocumento.Value))
                    {
                        dato = new TiqueteElectronico();
                    }
                    if (TipoDocumento.NOTA_CREDITO.Equals(this.cmbTipoDocumento.Value))
                    {
                        dato = new NotaCreditoElectronica();
                    }

                    if (TipoDocumento.NOTA_DEBITO.Equals(this.cmbTipoDocumento.Value))
                    {
                        dato = new NotaDebitoElectronica();
                    }

                    /* ENCABEZADO */
                    dato.medioPago = this.cmbMedioPago.Value.ToString();
                    dato.plazoCredito = this.txtPlazoCredito.Text;
                    dato.condicionVenta = this.cmbCondicionVenta.Value.ToString();
                    dato.fechaEmision = this.txtFechaEmision.Date.ToString("yyyy-MM-ddTHH:mm:ss-06:00");
                    dato.medioPago = this.cmbMedioPago.Value.ToString();

                    /* DETALLE */
                    dato.detalleServicio = detalle;

                    /* EMISOR */
                    EmisorReceptorIMEC elEmisor = (EmisorReceptorIMEC)Session["elEmisor"];
                    
                    dato.emisor.identificacion.tipo = elEmisor.identificacionTipo;
                    dato.emisor.identificacion.numero = elEmisor.identificacion;
                    dato.emisor.ubicacion.otrassenas = elEmisor.otraSena.ToUpper();
                    dato.emisor.nombre = elEmisor.nombre;
                    dato.emisor.ubicacion.otrassenas = elEmisor.otraSena.ToUpper();
                    dato.emisor.nombreComercial = elEmisor.nombreComercial;

                    dato.emisor.telefono.codigoPais = elEmisor.telefonoCodigoPais;
                    dato.emisor.telefono.numTelefono = elEmisor.telefono;
                    dato.emisor.fax.codigoPais = elEmisor.faxCodigoPais;
                    dato.emisor.fax.numTelefono = elEmisor.fax;
                    dato.emisor.correoElectronico = elEmisor.correoElectronico.ToLower();

                    dato.emisor.ubicacion.provincia = elEmisor.provincia.ToUpper();
                    dato.emisor.ubicacion.canton = elEmisor.canton.ToUpper();
                    dato.emisor.ubicacion.distrito = elEmisor.distrito.ToUpper();
                    dato.emisor.ubicacion.barrio = elEmisor.barrio.ToUpper();
                    dato.emisor.ubicacion.otrassenas = elEmisor.otraSena.ToUpper();


                    /* RECEPTOR */
                    bool nuevo = true;
                    string identificacion = ((ASPxSpinEdit)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorIdentificacion")).Text;
                    EmisorReceptorIMEC elReceptor = conexion.EmisorReceptorIMEC.Find(identificacion);
                    if (elReceptor != null)
                    {
                        nuevo = false;

                    }
                    else
                    {
                        elReceptor = new EmisorReceptorIMEC();
                        elReceptor.identificacion = identificacion;
                        nuevo = true;
                    }
                    elReceptor = this.crearModificarReceptor(elReceptor);

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

                    dato.receptor.verificar();
                    if (!string.IsNullOrWhiteSpace(elReceptor.identificacion))
                    {

                        if (nuevo == false)
                        {
                            conexion.Entry(elReceptor).State = EntityState.Modified;
                        }
                        else
                        {
                            conexion.EmisorReceptorIMEC.Add(elReceptor);
                        }
                        conexion.SaveChanges();
                    }

                    /* RESUMEN */
                    dato.resumenFactura.codigoMoneda = this.cmbTipoMoneda.Value.ToString();
                    if (!TipoMoneda.CRC.Equals(dato.resumenFactura.codigoMoneda))
                    {
                        dato.resumenFactura.tipoCambio = decimal.Parse(this.txtTipoCambio.Text.Replace(",", "").Replace(".", "")) / 100;
                    }
                    dato.resumenFactura.calcularResumenFactura(dato.detalleServicio.lineaDetalle);

                    /* INFORMACION DE REFERENCIA */
                    dato.informacionReferencia = (List<InformacionReferencia>)Session["informacionReferencia"];
                    foreach (var item in dato.informacionReferencia)
                    {
                        item.fechaEmision = item.fechaEmisionTotal;
                    }

                    /* OTROS */
                    if (!string.IsNullOrWhiteSpace(this.txtOtros.Text))
                    {
                        dato.otros.otrosTextos.Add(this.txtOtros.Text);
                    }
                    Empresa empresa = conexion.Empresa.Find(dato.emisor.identificacion.numero);
                    if (empresa != null)
                    {
                        if (!string.IsNullOrWhiteSpace(empresa.leyenda))
                        {
                            dato.otros.otrosTextos.Add(empresa.leyenda);
                        }
                    }

                    /* VERIFICA VACIOS PARA XML */
                    dato.verificaDatosParaXML();

                    //genera el consecutivo del documento
                    string sucursal = this.cmbSucursalCaja.Value.ToString().Substring(0, 3);
                    string caja = this.cmbSucursalCaja.Value.ToString().Substring(3, 5); 
                    object[] key = new object[] { dato.emisor.identificacion.numero, sucursal, caja , this.cmbTipoDocumento.Value.ToString()};
                    ConsecutivoDocElectronico consecutivo = conexion.ConsecutivoDocElectronico.Find(key);
                    if (consecutivo != null)
                    {
                        dato.clave = consecutivo.getClave( this.txtFechaEmision.Date.ToString("yyyyMMdd"));
                        dato.numeroConsecutivo = consecutivo.getConsecutivo();

                        consecutivo.consecutivo += 1;
                        conexion.Entry(consecutivo).State = EntityState.Modified;
                    }else
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

                        dato.clave = consecutivo.getClave(this.txtFechaEmision.Date.ToString("yyyyMMdd"));
                        dato.numeroConsecutivo = consecutivo.getConsecutivo();

                        consecutivo.consecutivo += 1;
                        conexion.ConsecutivoDocElectronico.Add(consecutivo);

                    }

                    string xml = EncodeXML.EncondeXML.getXMLFromObject(dato);
                    string xmlSigned = FirmaXML.getXMLFirmadoWeb(xml, elEmisor.llaveCriptografica, elEmisor.claveLlaveCriptografica);
                    string responsePost = await Services.enviarDocumentoElectronico(false, dato, elEmisor, this.cmbTipoDocumento.Value.ToString(), Session["usuario"].ToString());

                    if (responsePost.Equals("Success"))
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-info";
                        this.alertMessages.InnerText = String.Format("Documento #{0} enviado", dato.numeroConsecutivo);

                        if (!string.IsNullOrWhiteSpace(dato.receptor.correoElectronico))
                        {
                            Utilidades.sendMail(Session["emisor"].ToString(), dato.receptor.correoElectronico,
                                string.Format("{0} - {1}", dato.numeroConsecutivo, elEmisor.nombre),
                                Utilidades.mensageGenerico(), "Documento Electrónico", EncodeXML.EncondeXML.getXMLFromObject(dato), dato.numeroConsecutivo, dato.clave,null);
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

                    this.btnFacturar.Enabled = false; 

                    conexion.SaveChanges();

                    if (empresa.tipoImpresion.Equals("A4"))
                    {
                        Response.Redirect("~/Pages/Consulta/" + dato.clave);
                    }
                    else {
                        Response.Redirect("~/Pages/ConsultaRP/" + dato.clave);
                    }

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

                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = fullErrorMessage;
                Server.ClearError();

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

        protected void btnBuscarReceptor_Click(object sender, EventArgs e)
        {
            try {
                using (var conexion = new DataModelFE())
                { 

                    /* RECEPTOR */
                    if (string.IsNullOrWhiteSpace(((ASPxSpinEdit)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorIdentificacion")).Text))
                    {
                        this.alertMessages.Attributes["class"] = "alert alert-danger";
                        this.alertMessages.InnerText = "El número de identifiación es requerida";
                    }
                    else { 
                        string elReceptor = ((ASPxSpinEdit)acordionReceptor.Groups[0].FindControl("ASPxFormLayout").FindControl("txtReceptorIdentificacion")).Text;
                        EmisorReceptorIMEC receptor = conexion.EmisorReceptorIMEC.Where(x => x.identificacion == elReceptor).FirstOrDefault();
                        if (receptor != null)
                        {
                            this.loadReceptor(receptor);
                            this.alertMessages.Attributes["class"] = "alert alert-info";
                            this.alertMessages.InnerText = "Datos del receptor cargados correctamente";
                        }
                        else
                        {
                            this.alertMessages.Attributes["class"] = "alert alert-danger";
                            this.alertMessages.InnerText = "Datos del receptor no existen";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
        }

        protected void ASPxGridViewClientes_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as ASPxGridView).GetSelectedFieldValues("identificacion").Count > 0)
            {
                string identificacion = (sender as ASPxGridView).GetSelectedFieldValues("identificacion")[0].ToString();
                using (var conexion = new DataModelFE())
                {
                    EmisorReceptorIMEC receptor = conexion.EmisorReceptorIMEC.Find(identificacion);
                    this.loadReceptor(receptor);

                    this.documento.ActiveTabIndex = 3;
                     
                }

                this.alertMessages.Attributes["class"] = "alert alert-info";
                this.alertMessages.InnerText = "Datos cargados con correctamente";
            } 
        }

        protected void cmbTipoDocumento_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                /* SUCURSAL CAJA */
                string emisor = Session["emisor"].ToString();
                List<ConsecutivoDocElectronico> lista = conexion.ConsecutivoDocElectronico.Where(x => x.emisor == emisor &&
                x.tipoDocumento == this.cmbTipoDocumento.Value.ToString() && x.estado == Estado.ACTIVO.ToString()).ToList();
                if (lista.Count == 0)
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
                this.cmbSucursalCaja.SelectedIndex = 0;
                this.cmbSucursalCaja.Items.Clear();
                foreach (var item in lista)
                {
                    this.cmbSucursalCaja.Items.Add(item.ToString(), string.Format("{0}{1}", item.sucursal, item.caja));
                }
                this.cmbSucursalCaja.IncrementalFilteringMode = IncrementalFilteringMode.Contains;



            }
        }
        
    }
}