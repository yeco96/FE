using Class.Utilidades;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;

namespace Web.Pages.Catalogos
{
    public partial class FrmEmisorReceptor : System.Web.UI.Page
    {

        /// <summary>
        /// constructor
        /// </summary>
        public FrmEmisorReceptor()
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
                if (!IsCallback && !IsPostBack)
                {
                    this.loadComboBox(); 
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
                this.ASPxGridView1.DataSource = conexion.EmisorReceptor.ToList();
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
                /* ESTADO */
                GridViewDataComboBoxColumn comboEstado = this.ASPxGridView1.Columns["estado"] as GridViewDataComboBoxColumn;
                comboEstado.PropertiesComboBox.Items.Clear();
                comboEstado.PropertiesComboBox.Items.AddRange(Enum.GetValues(typeof(Estado)));

                /* PROVINCIA */
                GridViewDataComboBoxColumn provinciaCombo = this.ASPxGridView1.Columns["provincia"] as GridViewDataComboBoxColumn;
                provinciaCombo.PropertiesComboBox.Items.Clear();
                foreach (var item in conexion.Ubicacion.Select(x => new { x.codProvincia, x.nombreProvincia }).Distinct())
                {
                    provinciaCombo.PropertiesComboBox.Items.Add(item.nombreProvincia, item.codProvincia);
                }
                provinciaCombo.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
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
                    EmisorReceptor dato = new EmisorReceptor();
                    //llena el objeto con los valores de la pantalla
                    dato.identificacionTipo = e.NewValues["identificacionTipo"] != null ? e.NewValues["identificacionTipo"].ToString().ToUpper() : null;
                    dato.identificacion = e.NewValues["identificacion"] != null ? e.NewValues["identificacion"].ToString().ToUpper() : null;

                    dato.nombre = e.NewValues["nombre"] != null ? e.NewValues["nombre"].ToString().ToUpper() : null;
                    
                    dato.nombreComercial = e.NewValues["nombreComercial"] != null ? e.NewValues["nombreComercial"].ToString().ToUpper() : null;
                    
                    dato.usuarioCreacion = Session["usuario"].ToString();
                    dato.fechaCreacion = Date.DateTimeNow();

                    //agrega el objeto
                    conexion.EmisorReceptor.Add(dato);
                    conexion.SaveChanges();

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
                    EmisorReceptor dato = new EmisorReceptor();
                    //llena el objeto con los valores de la pantalla
                    dato.identificacionTipo = e.NewValues["identificacionTipo"] != null ? e.NewValues["identificacionTipo"].ToString().ToUpper() : null;

                    //busca el objeto 
                    dato = conexion.EmisorReceptor.Find(dato.identificacionTipo);

                    dato.identificacion = e.NewValues["identificacion"] != null ? e.NewValues["identificacion"].ToString().ToUpper() : null;
                 
                    dato.usuarioModificacion = Session["usuario"].ToString();
                    dato.fechaModificacion = Date.DateTimeNow();

                    //modifica objeto
                    //conexion.Entry(oldDato).CurrentValues.SetValues(dato);
                    conexion.Entry(dato).State = EntityState.Modified;
                    conexion.SaveChanges();

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
                    var id = e.Values["identificacionTipo"].ToString();

                    //busca objeto
                    var itemToRemove = conexion.EmisorReceptor.SingleOrDefault(x => x.identificacionTipo == id);
                    conexion.EmisorReceptor.Remove(itemToRemove);
                    conexion.SaveChanges();

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

        /// <summary>
        /// desabilita los campos que no son editables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (!this.ASPxGridView1.IsNewRowEditing)
            {
                if (e.Column.FieldName == "identificacionTipo") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
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
                string error = "";
                if (e.Exception.InnerException!=null)
                {
                    error =  e.Exception.InnerException.Message;
                    error = e.Exception.InnerException.InnerException.Message;
                }
                else
                {
                    error = e.ErrorText; 
                }

            e.ErrorText = Utilidades.validarExepcionSQL(error); 
            }
        }
    }
}
