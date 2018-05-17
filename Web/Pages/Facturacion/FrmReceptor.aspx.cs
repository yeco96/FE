using Class.Utilidades;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;

namespace Web.Pages.Facturacion
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmReceptor : System.Web.UI.Page
    {

        /// <summary>
        /// constructor
        /// </summary>
        public FrmReceptor()
        {
        }
        /// <summary>
        /// este metodo si inicializa al cada vez que se renderiza la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Pages/Login.aspx");
                }
                if (!IsCallback && !IsPostBack)
                {
                    Session["entro"] = "NO";
                    this.loadComboBox();
                }
                this.refreshData();
            }
            catch (Exception ex)
            {

                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
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

        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            if (!this.ASPxGridView1.IsEditing)
            {
                using (var conexion = new DataModelFE())
                {
                    string emisor = Session["emisor"].ToString();
                    this.ASPxGridView1.DataSource = (from Emisor in conexion.EmisorReceptorIMEC
                                                     from cliente in conexion.Cliente
                                                     where Emisor.identificacion == cliente.receptor && cliente.emisor == emisor
                                                     select Emisor
                                                     ).ToList();
                    conexion.EmisorReceptorIMEC.ToList();
                    this.ASPxGridView1.DataBind();
                }
            }
        } 

        /// <summary>
        /// carga solo una vez para ahorar tiempo 
        /// </summary>
        private void loadComboBox()
        {
            using (var conexion = new DataModelFE())
            {
                /* ESTADO */
                GridViewDataComboBoxColumn comboEstado = this.ASPxGridView1.Columns["estado"] as GridViewDataComboBoxColumn;
                comboEstado.PropertiesComboBox.Items.Clear();
                comboEstado.PropertiesComboBox.Items.AddRange(Enum.GetValues(typeof(Estado)));

                /* IDENTIFICACION TIPO */
                GridViewDataComboBoxColumn comboIdentificacionTipo = this.ASPxGridView1.Columns["identificacionTipo"] as GridViewDataComboBoxColumn;
                comboIdentificacionTipo.PropertiesComboBox.Items.Clear();
                foreach (var item in conexion.TipoIdentificacion.ToList())
                {
                    comboIdentificacionTipo.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }

                /* CODIGO PAIS */
                GridViewDataComboBoxColumn cmbEmisorTelefonoCod = this.ASPxGridView1.Columns["telefonoCodigoPais"] as GridViewDataComboBoxColumn;
                GridViewDataComboBoxColumn cmbEmisorFaxCod = this.ASPxGridView1.Columns["faxCodigoPais"] as GridViewDataComboBoxColumn;
                cmbEmisorTelefonoCod.PropertiesComboBox.Items.Clear();
                cmbEmisorFaxCod.PropertiesComboBox.Items.Clear();
                foreach (var item in conexion.CodigoPais.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    cmbEmisorTelefonoCod.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                    cmbEmisorFaxCod.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
                cmbEmisorTelefonoCod.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                cmbEmisorFaxCod.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }


        /// <summary>
        /// manejo de errores en pantalla
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="column"></param>
        /// <param name="errorText"></param>
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column))
            {
                return;
            }
            else
            {
                errors[column] = errorText;
            }
        }


        /// <summary>
        /// actualiza un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {

                    EmisorReceptorIMEC dato = new EmisorReceptorIMEC();
                    dato.identificacion = e.NewValues["identificacion"] != null ? e.NewValues["identificacion"].ToString().ToUpper() : null;

                    //busca el objeto 
                    dato = conexion.EmisorReceptorIMEC.Find(dato.identificacion);

                    dato.identificacionTipo = e.NewValues["identificacionTipo"].ToString();
                    dato.identificacion = e.NewValues["identificacion"].ToString();
                    dato.nombre = e.NewValues["nombre"].ToString();
                    dato.nombreComercial = e.NewValues["nombreComercial"] != null ? e.NewValues["nombreComercial"].ToString().ToUpper() : null;

                    if (e.NewValues["telefono"] != null)
                    {
                        //dato.telefonoCodigoPais = e.NewValues["telefonoCodigoPais"].ToString();
                        dato.telefonoCodigoPais = "506";
                        dato.telefono = e.NewValues["telefono"].ToString();
                    }


                    dato.correoElectronico = e.NewValues["correoElectronico"] != null ? e.NewValues["correoElectronico"].ToString()  : null;

                    if (e.NewValues["fax"] != null)
                    {
                        //dato.faxCodigoPais = e.NewValues["faxCodigoPais"].ToString();
                        dato.faxCodigoPais = "506";
                        dato.fax = e.NewValues["fax"].ToString();
                    }

                    ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
                    ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutUbicacion");
                    /* PROVINCIA */
                    ASPxComboBox comboProvincia = (ASPxComboBox)form.FindControl("cmbProvincia");
                    /* CANTON */
                    ASPxComboBox comboCanton = (ASPxComboBox)form.FindControl("cmbCanton");
                    /* DISTRITO */
                    ASPxComboBox comboDistrito = (ASPxComboBox)form.FindControl("cmbDistrito");
                    /* BARRIO */
                    ASPxComboBox comboBarrio = (ASPxComboBox)form.FindControl("cmbBarrio");
                    ASPxMemo otraSena = (ASPxMemo)form.FindControl("txtOtraSenas");

                    dato.provincia = comboProvincia.Value.ToString();
                    dato.canton = comboCanton.Value.ToString();
                    dato.distrito = comboDistrito.Value.ToString();
                    dato.barrio = comboBarrio.Value.ToString();
                    dato.otraSena = otraSena.Text;
                    
                    dato.usuarioModificacion = Session["usuario"].ToString();
                    dato.fechaModificacion = Date.DateTimeNow();

                    //modifica objeto 
                    conexion.Entry(dato).State = EntityState.Modified;
                    conexion.SaveChanges();

                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridView1.CancelEdit();

                    ((ASPxGridView)sender).JSProperties["cpUpdatedMessage"] = "Los datos se modificaron correctamente, puede continuar.";
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


        /// <summary>
        /// desabilita los campos que no son editables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "identificacion")
            { 
                ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
                ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutUbicacion");
                ASPxComboBox comboProvincia = (ASPxComboBox)form.FindControl("cmbProvincia");

                if (comboProvincia.Items.Count == 0)
                {
                    this.cargarProvincias();
                }

                EmisorReceptorIMEC dato = null;
                using (var conexion = new DataModelFE())
                {
                    if (e.Editor.Value != null){
                        dato = conexion.EmisorReceptorIMEC.Find(e.Editor.Value.ToString());
                        if (dato != null)
                        {
                            comboProvincia.Value = dato.provincia;
                        }
                    }
                }
                
            }

            if (this.ASPxGridView1.IsNewRowEditing)
            {
                if (e.Column.FieldName == "identificacion")
                {
                    this.cargarProvincias();
                }

                if (e.Column.FieldName == "estado")
                {
                    e.Editor.Value = "ACTIVO";
                }
            }
            else
            {/******************  CUANDO ES UNA MODIFICACION   *******************/



                if (this.ASPxGridView1.IsEditing && e.Column.FieldName == "identificacion" && !String.IsNullOrEmpty(e.Editor.Value.ToString()) && Session["entro"].ToString() == "NO")
                {
                    Session["entro"] = "SI";
                    EmisorReceptorIMEC dato = null;
                    using (var conexion = new DataModelFE())
                    {
                        dato = conexion.EmisorReceptorIMEC.Find(e.Editor.Value.ToString());
                    }

                    ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
                    ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutUbicacion");

                    this.cargarProvincias();

                    /* PROVINCIA */
                    ASPxComboBox comboProvincia = (ASPxComboBox)form.FindControl("cmbProvincia");
                    comboProvincia.Value = dato.provincia;

                    /* CANTON */
                    ASPxComboBox comboCanton = (ASPxComboBox)form.FindControl("cmbCanton");
                    comboCanton.Items.Clear();

                    /* DISTRITO */
                    ASPxComboBox comboDistrito = (ASPxComboBox)form.FindControl("cmbDistrito");
                    comboDistrito.Items.Clear();

                    /* BARRIO */
                    ASPxComboBox comboBarrio = (ASPxComboBox)form.FindControl("cmbBarrio");
                    comboBarrio.Items.Clear();

                    /* OTRA SEÑAS */
                    ASPxMemo otraSena = (ASPxMemo)form.FindControl("txtOtraSenas");
                    otraSena.Text = dato.otraSena;


                    using (var conexion = new DataModelFE())
                    {

                        foreach (var item in conexion.Ubicacion.
                           Where(x => x.codProvincia == dato.provincia).
                           Select(x => new { x.codCanton, x.nombreCanton }).Distinct())
                        {
                            comboCanton.Items.Add(item.nombreCanton, item.codCanton);
                        }
                        comboCanton.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                        comboCanton.Value = dato.canton;


                        foreach (var item in conexion.Ubicacion.
                            Where(x => x.codProvincia == dato.provincia).
                            Where(x => x.codCanton == dato.canton).
                            Select(x => new { x.codDistrito, x.nombreDistrito }).Distinct())
                        {
                            comboDistrito.Items.Add(item.nombreDistrito, item.codDistrito);
                        }
                        comboDistrito.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                        comboDistrito.Value = dato.distrito;


                        foreach (var item in conexion.Ubicacion.
                           Where(x => x.codProvincia == dato.provincia).
                           Where(x => x.codCanton == dato.canton).
                            Where(x => x.codDistrito == dato.distrito).
                           Select(x => new { x.codBarrio, x.nombreBarrio }).Distinct())
                        {
                            comboBarrio.Items.Add(item.nombreBarrio, item.codBarrio);
                        }
                        comboBarrio.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                        comboBarrio.Value = dato.barrio;

                    }

                }
            }
        }


        // <summary>
        /// EXPORTAR DATOS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void exportarPDF_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.WritePdfToResponse();
        }

        protected void exportarXLS_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.WriteXlsToResponse();
        }

        protected void exportarXLSX_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void exportarCSV_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.WriteCsvToResponse();
        }


        protected void DocumentsUploadControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            try
            {
                Session["LlaveCriptograficap12"] = e.UploadedFile.FileBytes;

            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
        }


        public void cargarProvincias()
        {

            ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
            ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutUbicacion");

            /* PROVINCIA */
            ASPxComboBox comboProvincia = (ASPxComboBox)form.FindControl("cmbProvincia");
            comboProvincia.Items.Clear();

            using (var conexion = new DataModelFE())
            {
                foreach (var item in conexion.Ubicacion.
                Select(x => new { x.codProvincia, x.nombreProvincia }).Distinct())
                {
                    comboProvincia.Items.Add(item.nombreProvincia, item.codProvincia);
                }
                comboProvincia.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }

        public void cargarCantones()
        {
            ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
            ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutUbicacion");
            /* PROVINCIA */
            ASPxComboBox comboProvincia = (ASPxComboBox)form.FindControl("cmbProvincia");
            /* CANTON */
            ASPxComboBox comboCanton = (ASPxComboBox)form.FindControl("cmbCanton");

            comboCanton.Items.Clear();
            if (comboProvincia.Value != null)
            {
                using (var conexion = new DataModelFE())
                {
                    foreach (var item in conexion.Ubicacion.
                            Where(x => x.codProvincia == comboProvincia.Value.ToString()).
                            Select(x => new { x.codCanton, x.nombreCanton }).Distinct())
                    {
                        comboCanton.Items.Add(item.nombreCanton, item.codCanton);
                    }
                    comboCanton.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    comboCanton.SelectedIndex = 0;
                }
            }
        }

        public void cargarDistritos()
        {
            ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
            ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutUbicacion");
            /* PROVINCIA */
            ASPxComboBox comboProvincia = (ASPxComboBox)form.FindControl("cmbProvincia");
            /* CANTON */
            ASPxComboBox comboCanton = (ASPxComboBox)form.FindControl("cmbCanton");
            /* DISTRITO */
            ASPxComboBox comboDistrito = (ASPxComboBox)form.FindControl("cmbDistrito");

            comboDistrito.Items.Clear();
            if (comboProvincia.Value != null && comboCanton.Value != null)
            {
                using (var conexion = new DataModelFE())
                {
                    foreach (var item in conexion.Ubicacion.
                        Where(x => x.codProvincia == comboProvincia.Value.ToString()).
                        Where(x => x.codCanton == comboCanton.Value.ToString()).
                        Select(x => new { x.codDistrito, x.nombreDistrito }).Distinct())
                    {
                        comboDistrito.Items.Add(item.nombreDistrito, item.codDistrito);
                    }
                    comboDistrito.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    comboDistrito.SelectedIndex = 0;
                }
            }

        }

        public void cargarBarrio()
        {
            ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
            ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutUbicacion");
            /* PROVINCIA */
            ASPxComboBox comboProvincia = (ASPxComboBox)form.FindControl("cmbProvincia");
            /* CANTON */
            ASPxComboBox comboCanton = (ASPxComboBox)form.FindControl("cmbCanton");
            /* DISTRITO */
            ASPxComboBox comboDistrito = (ASPxComboBox)form.FindControl("cmbDistrito");
            /* BARRIO */
            ASPxComboBox comboBarrio = (ASPxComboBox)form.FindControl("cmbBarrio");

            comboBarrio.Items.Clear();
            if (comboProvincia.Value != null && comboCanton.Value != null && comboDistrito.Value != null)
            {
                using (var conexion = new DataModelFE())
                {
                    foreach (var item in conexion.Ubicacion.
                        Where(x => x.codProvincia == comboProvincia.Value.ToString()).
                        Where(x => x.codCanton == comboCanton.Value.ToString()).
                            Where(x => x.codDistrito == comboDistrito.Value.ToString()).
                        Select(x => new { x.codBarrio, x.nombreBarrio }).Distinct())
                    {
                        comboBarrio.Items.Add(item.nombreBarrio, item.codBarrio);
                    }
                    comboBarrio.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    comboBarrio.SelectedIndex = 0;
                }
            }

        }


        protected void cmbProvincia_ValueChanged(object sender, EventArgs e)
        {
            this.cargarCantones();
            this.cargarDistritos();
            this.cargarBarrio();
        }

        protected void cmbCanton_ValueChanged(object sender, EventArgs e)
        {
            this.cargarDistritos();
            this.cargarBarrio();
        }

        protected void cmbDistrito_ValueChanged(object sender, EventArgs e)
        {
            this.cargarBarrio();
        }

        protected void ASPxGridView1_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            Session["entro"] = "NO";
        }

        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
            if (tabs != null)
            {
                ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutUbicacion");
                /* PROVINCIA */
                ASPxComboBox comboProvincia = (ASPxComboBox)form.FindControl("cmbProvincia");
                /* CANTON */
                ASPxComboBox comboCanton = (ASPxComboBox)form.FindControl("cmbCanton");
                /* DISTRITO */
                ASPxComboBox comboDistrito = (ASPxComboBox)form.FindControl("cmbDistrito");
                /* BARRIO */
                ASPxComboBox comboBarrio = (ASPxComboBox)form.FindControl("cmbBarrio");
                ASPxMemo otraSena = (ASPxMemo)form.FindControl("txtOtraSenas");

                if (comboProvincia.Value == null || comboCanton.Value == null || comboDistrito.Value == null || comboBarrio.Value == null || string.IsNullOrWhiteSpace(otraSena.Text))
                {
                    e.RowError = "La ubicación es obligatoria (provincia, cantón, distrito, barrio y otras señas";
                }
                 
                if (e.NewValues["correoElectronico"] != null )
                { 
                    Regex validator = new Regex(@"\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*");
                    string[] correos = e.NewValues["correoElectronico"].ToString().Split(',');
                    foreach (var correo in correos)
                    {
                        if (!validator.IsMatch(correo))
                        {
                            AddError(e.Errors, this.ASPxGridView1.Columns["correoElectronico"], string.Format("Verificar formato: {0}",correo) );
                        }
                    }

                    if(correos.Length > 5)
                    {
                        e.RowError = "Solo puede agregar un máximo de 5 correos";
                    } 
                }


            }
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    //se declara el objeto a insertar
                    EmisorReceptorIMEC dato = new EmisorReceptorIMEC();
                    //llena el objeto con los valores de la pantalla
                    dato.identificacionTipo = e.NewValues["identificacionTipo"].ToString();
                    dato.identificacion = e.NewValues["identificacion"].ToString();
                    dato.nombre = e.NewValues["nombre"].ToString();
                    dato.nombreComercial = e.NewValues["nombreComercial"] != null ? e.NewValues["nombreComercial"].ToString().ToUpper() : null;

                    if (e.NewValues["telefono"] != null)
                    {
                        //dato.telefonoCodigoPais = e.NewValues["telefonoCodigoPais"].ToString();
                        dato.telefonoCodigoPais = "506";
                        dato.telefono = e.NewValues["telefono"].ToString();
                    }

                    dato.correoElectronico = e.NewValues["correoElectronico"] != null ? e.NewValues["correoElectronico"].ToString() : null;

                    if (e.NewValues["fax"] != null)
                    {
                        //dato.faxCodigoPais = e.NewValues["faxCodigoPais"].ToString();
                        dato.faxCodigoPais = "506";
                        dato.fax = e.NewValues["fax"].ToString();
                    }


                    ASPxPageControl tabs = (ASPxPageControl)ASPxGridView1.FindEditFormTemplateControl("pageControl");
                    ASPxFormLayout form = (ASPxFormLayout)tabs.FindControl("formLayoutUbicacion");
                    /* PROVINCIA */
                    ASPxComboBox comboProvincia = (ASPxComboBox)form.FindControl("cmbProvincia");
                    /* CANTON */
                    ASPxComboBox comboCanton = (ASPxComboBox)form.FindControl("cmbCanton");
                    /* DISTRITO */
                    ASPxComboBox comboDistrito = (ASPxComboBox)form.FindControl("cmbDistrito");
                    /* BARRIO */
                    ASPxComboBox comboBarrio = (ASPxComboBox)form.FindControl("cmbBarrio");
                    ASPxMemo otraSena = (ASPxMemo)form.FindControl("txtOtraSenas");

                    dato.provincia = comboProvincia.Value.ToString();
                    dato.canton = comboCanton.Value.ToString();
                    dato.distrito = comboDistrito.Value.ToString();
                    dato.barrio = comboBarrio.Value.ToString();
                    dato.otraSena = otraSena.Text;

                   
                    dato.estado = e.NewValues["estado"].ToString();
                    dato.usuarioCreacion = Session["usuario"].ToString();
                    dato.fechaCreacion = Date.DateTimeNow();

                    //agregar cliente
                    var existeEmisor = conexion.EmisorReceptorIMEC.Find(dato.identificacion);
                    if (existeEmisor == null)
                    {
                        conexion.EmisorReceptorIMEC.Add(dato);
                    }
                    else
                    { 
                        conexion.Entry(dato).State = EntityState.Modified;
                    }
                    conexion.SaveChanges();


                    //asociar cliente
                    object[] key = new object[] { Session["emisor"].ToString(), dato.identificacion };
                    var cliente = conexion.Cliente.Find(key);
                    if (cliente == null)
                    {
                        cliente = new Cliente();
                        cliente.emisor = Session["emisor"].ToString();
                        cliente.receptor = dato.identificacion;
                        conexion.Cliente.Add(cliente);
                        conexion.SaveChanges();
                    }
                      
                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridView1.CancelEdit();

                    ((ASPxGridView)sender).JSProperties["cpUpdatedMessage"] = "Los datos se agregaron correctamente, puede continuar.";
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

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    var cliente = e.Values["identificacion"].ToString();

                    //busca objeto
                    object[] key = new object[] { Session["emisor"].ToString(), cliente };
                    var itemToRemove = conexion.Cliente.Find(key);
                    conexion.Cliente.Remove(itemToRemove);
                    conexion.SaveChanges();

                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridView1.CancelEdit();
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
    }
}
