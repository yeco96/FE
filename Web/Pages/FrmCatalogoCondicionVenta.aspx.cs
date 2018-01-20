using Class.Utilidades;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;

namespace Web.Pages
{
    public partial class FrmCatalogoCondicionVenta : System.Web.UI.Page
    {

        private DataModelFE dataModelFE;

        /// <summary>
        /// constructor
        /// </summary>
        public FrmCatalogoCondicionVenta()
        { 
            this.dataModelFE = new DataModelFE();
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
                if (!IsCallback && !IsPostBack)
                {
                    this.cargarCombos();
                }
                this.refreshData();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            this.ASPxGridView1.DataSource = this.dataModelFE.CondicionVenta.ToList();
            this.ASPxGridView1.DataBind();
        }

        /// <summary>
        /// carga solo una vez para ahorar tiempo 
        /// </summary>
        private void cargarCombos()
        {
            // Cargar valores de combo para estado
            GridViewDataComboBoxColumn comboEstado = this.ASPxGridView1.Columns["estado"] as GridViewDataComboBoxColumn;
            comboEstado.PropertiesComboBox.Items.Clear();
            comboEstado.PropertiesComboBox.Items.AddRange(Enum.GetValues(typeof(Estado)));
        }


        /// <summary>
        /// manejo de errores en pantalla
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="column"></param>
        /// <param name="errorText"></param>
        void AddError(Dictionary<GridViewColumn, String> errors, GridViewColumn column, String errorText)
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
                //se declara el objeto a insertar
                CondicionVenta dato = new CondicionVenta();
                //llena el objeto con los valores de la pantalla
                dato.codigo = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString() : null;
                dato.descripcion = e.NewValues["descripcion"] != null ? e.NewValues["descripcion"].ToString() : null;

                dato.estado = e.NewValues["estado"].ToString();
                dato.usuarioCreacion = Session["usuario"].ToString();
                dato.fechaCreacion =  Date.DateTimeNow();

                //agrega el objeto
                this.dataModelFE.CondicionVenta.Add(dato);
                this.dataModelFE.SaveChanges();
                 
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

               // DataModelFE.GetInstance().CondicionVenta.Remove(DataModelFE.GetInstance().CondicionVenta.Last() );

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(fullErrorMessage, ex.EntityValidationErrors);

            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
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


                // se declara el objeto a insertar
                CondicionVenta dato = new CondicionVenta();
                //llena el objeto con los valores de la pantalla
                dato.codigo = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString() : null;

                //busca el objeto 
                CondicionVenta oldDato = DataModelFE.GetInstance().CondicionVenta.Find(dato.codigo);
                dato = oldDato;

                dato.descripcion = e.NewValues["descripcion"] != null ? e.NewValues["descripcion"].ToString() : null; 
                dato.estado = e.NewValues["estado"].ToString();
                dato.usuarioModificacion = Session["usuario"].ToString();
                dato.fechaModificacion = Date.DateTimeNow();
                
                //modifica objeto
                DataModelFE.GetInstance().Entry(oldDato).CurrentValues.SetValues(dato);
                DataModelFE.GetInstance().SaveChanges();

                //esto es para el manero del devexpress
                e.Cancel = true;
                this.ASPxGridView1.CancelEdit();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                var id = e.Values["codigo"].ToString();

                //busca objeto
                var itemToRemove = DataModelFE.GetInstance().CondicionVenta.SingleOrDefault(x => x.codigo == id);
                DataModelFE.GetInstance().CondicionVenta.Remove(itemToRemove);
                DataModelFE.GetInstance().SaveChanges();

                //esto es para el manero del devexpress
                e.Cancel = true;
                this.ASPxGridView1.CancelEdit();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            // if (e.Column.FieldName == "idCondicionVenta") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
            //if (e.Column.FieldName == "usuarioCreacion") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
            // if (e.Column.FieldName == "fechaCreacion") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
            // if (e.Column.FieldName == "usuarioModificacion") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
            //if (e.Column.FieldName == "fechaModificacion") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
        }


        protected void exportarPDF_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.FileName = String.Format("ListaCondicionVenta{0}", DateTime.Now.ToString("yyyyMMdd"));
            this.ASPxGridViewExporter1.WritePdfToResponse();
        }

        protected void exportarXLS_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.FileName = String.Format("ListaCondicionVenta{0}", DateTime.Now.ToString("yyyyMMdd"));
            this.ASPxGridViewExporter1.WriteXlsToResponse();
        }

        protected void exportarXLSX_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.FileName = String.Format("ListaCondicionVenta{0}", DateTime.Now.ToString("yyyyMMdd"));
            this.ASPxGridViewExporter1.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void exportarCSV_Click(object sender, ImageClickEventArgs e)
        {
            this.ASPxGridViewExporter1.FileName = String.Format("ListaCondicionVenta{0}", DateTime.Now.ToString("yyyyMMdd"));
            this.ASPxGridViewExporter1.WriteCsvToResponse();
        }
        protected void ASPxGridView1_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
            if (e.Exception != null)
            {
                if (Session["errorMessage"] != null)
                {
                    e.ErrorText = Utilidades.validarExepcionSQL(Session["errorMessage"].ToString());
                    Session["errorMessage"] = null;
                }
            }
        }
    }
}
