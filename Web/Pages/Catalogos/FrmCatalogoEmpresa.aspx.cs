using Class.Utilidades;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Internal;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;

namespace Web.Pages.Catalogos
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmCatalogoEmpresa : System.Web.UI.Page
    {

        /// <summary>
        /// constructor
        /// </summary>
        public FrmCatalogoEmpresa()
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
                    Session["logo"] = null;
                    this.cargarCombos(); 
                }
                this.refreshData();
            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
        }
        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            using (var conexion = new DataModelFE())
            {
                string emisor = Session["emisor"].ToString();
                this.ASPxGridView1.DataSource = conexion.Empresa.Where(x => x.codigo == emisor).ToList(); 
                this.ASPxGridView1.DataBind();
            }
        }

        /// <summary>
        /// carga solo una vez para ahorar tiempo 
        /// </summary>
        private void cargarCombos()
        {
            /* ESTADO */
            GridViewDataComboBoxColumn comboEstado = this.ASPxGridView1.Columns["estado"] as GridViewDataComboBoxColumn;
            comboEstado.PropertiesComboBox.Items.Clear();
            comboEstado.PropertiesComboBox.Items.AddRange(Enum.GetValues(typeof(Estado)));

            /* IDIOMA */
            GridViewDataComboBoxColumn comboIdioma = this.ASPxGridView1.Columns["idioma"] as GridViewDataComboBoxColumn;
            comboIdioma.PropertiesComboBox.Items.Clear();
            comboIdioma.PropertiesComboBox.Items.Add(new ListEditItem("ESPAÑOL","ES"));
            comboIdioma.PropertiesComboBox.Items.Add(new ListEditItem("INGLES", "EN"));

            /* TIPO IMPRESION */
            GridViewDataComboBoxColumn comboTipoImpresion = this.ASPxGridView1.Columns["tipoImpresion"] as GridViewDataComboBoxColumn;
            comboTipoImpresion.PropertiesComboBox.Items.Clear();
            comboTipoImpresion.PropertiesComboBox.Items.Add(new ListEditItem("NORMAL (A4)", "A4"));
            comboTipoImpresion.PropertiesComboBox.Items.Add(new ListEditItem("ROLL PAPER", "RP"));

            using (var conexion = new DataModelFE())
            {
                /* MEDIO PAGO */
                GridViewDataComboBoxColumn cmbMedioPago = this.ASPxGridView1.Columns["medioPago"] as GridViewDataComboBoxColumn;
                foreach (var item in conexion.MedioPago.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    cmbMedioPago.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
                cmbMedioPago.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains; 

                /* CONDICION VENTA */
                GridViewDataComboBoxColumn cmbCondicionVenta = this.ASPxGridView1.Columns["condicionVenta"] as GridViewDataComboBoxColumn;
                foreach (var item in conexion.CondicionVenta.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    cmbCondicionVenta.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
                cmbCondicionVenta.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                /* TIPO MONEDA */
                GridViewDataComboBoxColumn cmbTipoMoneda = this.ASPxGridView1.Columns["moneda"] as GridViewDataComboBoxColumn;
                foreach (var item in conexion.TipoMoneda.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    cmbTipoMoneda.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
                cmbTipoMoneda.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
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
        /// inserta un registro nuevo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    //se declara el objeto a insertar
                    Empresa dato = new Empresa();
                    //llena el objeto con los valores de la pantalla
                    dato.codigo = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString() : null;
                    dato.descripcion = e.NewValues["descripcion"] != null ? e.NewValues["descripcion"].ToString().ToUpper() : null;
                    dato.idioma = e.NewValues["idioma"] != null ? e.NewValues["idioma"].ToString().ToUpper() : null;
                    dato.tipoImpresion = e.NewValues["tipoImpresion"] != null ? e.NewValues["tipoImpresion"].ToString().ToUpper() : null;
                    dato.leyenda = e.NewValues["leyenda"] != null ? e.NewValues["leyenda"].ToString().ToUpper() : null;
                    dato.estado = e.NewValues["estado"].ToString();

                    dato.medioPago= e.NewValues["medioPago"].ToString();
                    dato.condicionVenta = e.NewValues["condicionVenta"].ToString();
                    dato.plazoCredito = int.Parse(e.NewValues["plazoCredito"].ToString());
                    dato.moneda = e.NewValues["moneda"].ToString();

                    dato.usuarioCreacion = Session["usuario"].ToString();
                    dato.fechaCreacion = Date.DateTimeNow();

                    if (Session["logo"] != null)
                    {
                        dato.logo = (byte[])Session["logo"];
                    } 
                    //agrega el objeto
                    conexion.Empresa.Add(dato);
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
               // conexion.Empresa.Remove(conexion.Empresa.Last() );

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
                    // se declara el objeto a insertar
                    Empresa dato = new Empresa();
                    //llena el objeto con los valores de la pantalla
                    dato.codigo = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString() : null;

                    //busca el objeto 
                    dato = conexion.Empresa.Find(dato.codigo);
                    dato.idioma = e.NewValues["idioma"] != null ? e.NewValues["idioma"].ToString().ToUpper() : null;
                    dato.tipoImpresion = e.NewValues["tipoImpresion"] != null ? e.NewValues["tipoImpresion"].ToString().ToUpper() : null;
                    dato.leyenda = e.NewValues["leyenda"] != null ? e.NewValues["leyenda"].ToString().ToUpper() : null;
                    dato.descripcion = e.NewValues["descripcion"] != null ? e.NewValues["descripcion"].ToString().ToUpper() : null;

                    dato.medioPago = e.NewValues["medioPago"].ToString();
                    dato.condicionVenta = e.NewValues["condicionVenta"].ToString();
                    dato.plazoCredito = int.Parse(e.NewValues["plazoCredito"].ToString());
                    dato.moneda = e.NewValues["moneda"].ToString();

                    dato.estado = e.NewValues["estado"].ToString();
                    dato.usuarioModificacion = Session["usuario"].ToString();
                    dato.fechaModificacion = Date.DateTimeNow();

                    if (Session["logo"] != null)
                    {
                        dato.logo = (byte[])Session["logo"];
                    }

                    //modifica objeto
                    conexion.Entry(dato).State = EntityState.Modified;
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
                // conexion.Empresa.Remove(conexion.Empresa.Last() );

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
        /// elimina un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    var id = e.Values["codigo"].ToString();

                    //busca objeto
                    var itemToRemove = conexion.Empresa.SingleOrDefault(x => x.codigo == id);
                    conexion.Empresa.Remove(itemToRemove);
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
                // conexion.Empresa.Remove(conexion.Empresa.Last() );

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
            if (e.Column.FieldName == "codigo") {
                e.Editor.ReadOnly = true;
                e.Column.ReadOnly = true;
                e.Editor.BackColor = System.Drawing.Color.LightGray;
                e.Editor.Value = Session["usuario"].ToString();
            }
            if (this.ASPxGridView1.IsNewRowEditing)
            {
                if (e.Column.FieldName == "estado")
                {
                    e.Editor.Value = Estado.ACTIVO.ToString();
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

        protected void fileUpload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            try
            {
                using (System.Drawing.Image original = System.Drawing.Image.FromStream(e.UploadedFile.FileContent))
                using (System.Drawing.Image thumbnail = new ImageThumbnailCreator(original).CreateImageThumbnail(new Size(100, 100))) 
                Session["logo"] = imageToByteArray(thumbnail);

            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

    }
}
