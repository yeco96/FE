using Class.Utilidades;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;

namespace Web.Pages.Catalogos
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmCatalogoProductoImpuesto : System.Web.UI.Page
    {

        /// <summary>
        /// constructor
        /// </summary>
        public FrmCatalogoProductoImpuesto()
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
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                if (!IsCallback && !IsPostBack)
                {
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
                this.ASPxGridView1.DataSource = (from impuesto in conexion.ProductoImpuesto
                                                 from producto in conexion.Producto
                                                 where producto.id == impuesto.idProducto && producto.emisor == emisor
                                                 select impuesto).ToList(); 
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

            /* PRODUCTO */
            GridViewDataComboBoxColumn comboProducto = this.ASPxGridView1.Columns["idProducto"] as GridViewDataComboBoxColumn;
            comboProducto.PropertiesComboBox.Items.Clear();
            using (var conexion = new DataModelFE())
            {
                string emisor =Session["emisor"].ToString();
                foreach (var item in conexion.Producto.Where(x=>x.emisor == emisor).Where(x=>x.estado==Estado.ACTIVO.ToString()).ToList())
                {
                    comboProducto.PropertiesComboBox.Items.Add(item.descripcion, item.id); 
                }
            }
            comboProducto.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            /* TIPO IMPUESTO */
            GridViewDataComboBoxColumn comboTipoImpuesto = this.ASPxGridView1.Columns["tipoImpuesto"] as GridViewDataComboBoxColumn;
            comboTipoImpuesto.PropertiesComboBox.Items.Clear();
            using (var conexion = new DataModelFE())
            { 
                foreach (var item in conexion.TipoImpuesto.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    comboTipoImpuesto.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
            }
            comboProducto.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
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
                    ProductoImpuesto dato = new ProductoImpuesto();
                    //llena el objeto con los valores de la pantalla
                    dato.idProducto = e.NewValues["idProducto"] != null ? int.Parse(e.NewValues["idProducto"].ToString()) : 0 ;
                    dato.tipoImpuesto = e.NewValues["tipoImpuesto"] != null ? e.NewValues["tipoImpuesto"].ToString().ToUpper() : null;
                    dato.porcentaje = e.NewValues["porcentaje"] != null ? decimal.Parse(e.NewValues["porcentaje"].ToString()) : 0;
                    dato.emisor =Session["emisor"].ToString();
                    dato.estado = e.NewValues["estado"].ToString();
                    dato.usuarioCreacion = Session["usuario"].ToString();
                    dato.fechaCreacion = Date.DateTimeNow();

                    //agrega el objeto
                    conexion.ProductoImpuesto.Add(dato);
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
                    ProductoImpuesto dato = new ProductoImpuesto();
                    //llena el objeto con los valores de la pantalla
                    dato.idProducto = e.NewValues["idProducto"] != null ? int.Parse(e.NewValues["idProducto"].ToString()) : 0;
                    dato.tipoImpuesto = e.NewValues["tipoImpuesto"] != null ? e.NewValues["tipoImpuesto"].ToString().ToUpper() : null;

                    object[] key = new object[] { dato.idProducto, dato.tipoImpuesto};
                    //busca el objeto 
                    dato = conexion.ProductoImpuesto.Where(x=>x.idProducto == dato.idProducto).Where(x => x.tipoImpuesto == dato.tipoImpuesto).FirstOrDefault();
                   
                    dato.porcentaje = e.NewValues["porcentaje"] != null ? decimal.Parse(e.NewValues["porcentaje"].ToString()) : 0;
                    dato.emisor =Session["emisor"].ToString();
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

                    var idProducto = long.Parse(e.Values["idProducto"].ToString());
                    var tipoImpuesto =  e.Values["tipoImpuesto"].ToString().ToUpper();

                    object[] key = new object[] { idProducto, tipoImpuesto };


                    //busca objeto
                    var itemToRemove = conexion.ProductoImpuesto.Where(x => x.idProducto == idProducto).Where(x => x.tipoImpuesto == tipoImpuesto).FirstOrDefault(); 
                    conexion.ProductoImpuesto.Remove(itemToRemove);
                    conexion.SaveChanges();

                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridView1.CancelEdit();

                    ((ASPxGridView)sender).JSProperties["cpUpdatedMessage"] = "Los datos se eliminaron correctamente, puede continuar.";
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
            if (this.ASPxGridView1.IsNewRowEditing)
            {
                if (e.Column.FieldName == "id") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; e.Editor.Value = 0; }
                if (e.Column.FieldName == "estado")
                {
                    e.Editor.Value = Estado.ACTIVO.ToString();
                }
                
            }
            else
            {
                if (e.Column.FieldName == "id") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
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
         
    }
}
