using Class.Utilidades;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;

namespace Web.Pages.Catalogos
{
    public partial class FrmCatalogoProducto : System.Web.UI.Page
    {

        /// <summary>
        /// constructor
        /// </summary>
        public FrmCatalogoProducto()
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
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                if (!IsCallback && !IsPostBack)
                {
                    this.cargarCombos(); 
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
            using (var conexion = new DataModelFE())
            {
                this.ASPxGridView1.DataSource = conexion.Producto.ToList(); 
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

            /* TIPO */
            GridViewDataComboBoxColumn comboTipo = this.ASPxGridView1.Columns["tipo"] as GridViewDataComboBoxColumn;
            comboTipo.PropertiesComboBox.Items.Clear();
            using (var conexion = new DataModelFE())
            { 
                foreach (var item in conexion.TipoProductoServicio.Where(x=>x.estado==Estado.ACTIVO.ToString()).ToList())
                {
                    comboTipo.PropertiesComboBox.Items.Add(item.descripcion, item.codigo); 
                }
            }

            /* UNIDAD MEDIDA */
            GridViewDataComboBoxColumn comboUnidadMedida = this.ASPxGridView1.Columns["unidadMedida"] as GridViewDataComboBoxColumn;
            comboUnidadMedida.PropertiesComboBox.Items.Clear();
            using (var conexion = new DataModelFE())
            {
                foreach (var item in conexion.UnidadMedida.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    comboUnidadMedida.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
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
                    Producto dato = new Producto();
                    //llena el objeto con los valores de la pantalla
                    dato.codigo = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString() : null;
                    dato.descripcion = e.NewValues["descripcion"] != null ? e.NewValues["descripcion"].ToString().ToUpper() : null;
                    dato.tipo = e.NewValues["tipo"] != null ? e.NewValues["tipo"].ToString().ToUpper() : null;
                    dato.unidadMedida = e.NewValues["unidadMedida"] != null ? e.NewValues["unidadMedida"].ToString().ToUpper() : null;
                    dato.precio = e.NewValues["precio"] != null ? double.Parse(e.NewValues["precio"].ToString()) : 0;
                    dato.emisor = ((EmisorReceptor)Session["emisor"]).identificacion;
                    dato.estado = e.NewValues["estado"].ToString();
                    dato.usuarioCreacion = Session["usuario"].ToString();
                    dato.fechaCreacion = Date.DateTimeNow();

                    //agrega el objeto
                    conexion.Producto.Add(dato);
                    conexion.SaveChanges();

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

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

               // conexion.Producto.Remove(conexion.Producto.Last() );

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
                    Producto dato = new Producto();
                    //llena el objeto con los valores de la pantalla
                    dato.codigo = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString() : null;

                    //busca el objeto 
                    dato = conexion.Producto.Find(dato.codigo);

                    dato.precio = e.NewValues["precio"] != null ? double.Parse(e.NewValues["precio"].ToString()) : 0;
                    dato.emisor = ((EmisorReceptor)Session["emisor"]).identificacion;
                    dato.tipo = e.NewValues["tipo"] != null ? e.NewValues["tipo"].ToString().ToUpper() : null;
                    dato.unidadMedida = e.NewValues["unidadMedida"] != null ? e.NewValues["unidadMedida"].ToString(): null;
                    dato.descripcion = e.NewValues["descripcion"] != null ? e.NewValues["descripcion"].ToString().ToUpper() : null;
                    dato.estado = e.NewValues["estado"].ToString();
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
                    var id = e.Values["codigo"].ToString().ToUpper();

                    //busca objeto
                    var itemToRemove = conexion.Producto.SingleOrDefault(x => x.codigo == id);
                    conexion.Producto.Remove(itemToRemove);
                    conexion.SaveChanges();

                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridView1.CancelEdit();

                    ((ASPxGridView)sender).JSProperties["cpUpdatedMessage"] = "Los datos se eliminaron correctamente, puede continuar.";
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

        /// <summary>
        /// desabilita los campos que no son editables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (!this.ASPxGridView1.IsNewRowEditing)
            {
                if (e.Column.FieldName == "codigo") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
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

        /// <summary>
        /// errore personalizados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView1_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
            if (e.Exception != null)
            {
                string error = e.Exception.InnerException.Message;
                error = e.Exception.InnerException.InnerException.Message;

                e.ErrorText = Utilidades.validarExepcionSQL(error);
            }
        }
    }
}
