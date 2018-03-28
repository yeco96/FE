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
using Web.Models.Administracion;
using Web.Models.Catalogos;
using Web.Models.Facturacion;

namespace Web.Pages.Administracion
{

    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmPlanAdmin : System.Web.UI.Page
    {

        /// <summary>
        /// constructor
        /// </summary>
        public FrmPlanAdmin()
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
                List<Plan> lista = conexion.Plan.ToList();
                foreach (var item in lista)
                {
                    item.Emisor = conexion.EmisorReceptorIMEC.Find(item.emisor);
                }
                this.ASPxGridView1.DataSource = lista;
                this.ASPxGridView1.DataBind();
            }
        }

        /// <summary>
        /// carga solo una vez para ahorar tiempo 
        /// </summary>
        private void cargarCombos()
        {
            using (var conexion = new DataModelFE())
            {
                /* ESTADO */
                GridViewDataComboBoxColumn comboEstado = this.ASPxGridView1.Columns["estado"] as GridViewDataComboBoxColumn;
                comboEstado.PropertiesComboBox.Items.Clear();
                comboEstado.PropertiesComboBox.Items.AddRange(Enum.GetValues(typeof(Estado)));


                /* PLAN */
                GridViewDataComboBoxColumn comboPlan = this.ASPxGridView1.Columns["plan"] as GridViewDataComboBoxColumn;
                comboPlan.PropertiesComboBox.Items.Clear(); 
                foreach (var item in conexion.TipoPlan.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    comboPlan.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
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
                    Plan dato = new Plan();
                    //llena el objeto con los valores de la pantalla
                    dato.emisor = e.NewValues["emisor"] != null ? e.NewValues["emisor"].ToString() : null;
                    dato.plan = e.NewValues["plan"] != null ? e.NewValues["plan"].ToString().ToUpper() : null;
                    dato.cantidadDocEmitido = e.NewValues["cantidadDocEmitido"] != null ? int.Parse(e.NewValues["cantidadDocEmitido"].ToString()) : 0;
                    dato.cantidadDocPlan = e.NewValues["cantidadDocPlan"] != null ? int.Parse(e.NewValues["cantidadDocPlan"].ToString()) : 0;
                    dato.fechaInicio = e.NewValues["fechaInicio"] != null ? DateTime.Parse(e.NewValues["fechaInicio"].ToString()) : Date.DateTimeNow();
                    dato.fechaFin = e.NewValues["fechaFin"] != null ? DateTime.Parse(e.NewValues["fechaFin"].ToString()) : Date.DateTimeNow();
                    
                    dato.estado = e.NewValues["estado"].ToString();
                    dato.usuarioCreacion = Session["usuario"].ToString();
                    dato.fechaCreacion = Date.DateTimeNow();

                    //agrega el objeto
                    conexion.Plan.Add(dato);
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
                    //busca el objeto 
                    Plan dato = conexion.Plan.Find(e.NewValues["emisor"].ToString());
                     
                    dato.plan = e.NewValues["plan"] != null ? e.NewValues["plan"].ToString().ToUpper() : null;
                    dato.cantidadDocEmitido = e.NewValues["cantidadDocEmitido"] != null ? int.Parse(e.NewValues["cantidadDocEmitido"].ToString()) : 0;
                    dato.cantidadDocPlan = e.NewValues["cantidadDocPlan"] != null ? int.Parse(e.NewValues["cantidadDocPlan"].ToString()) : 0;
                    dato.fechaInicio = e.NewValues["fechaInicio"] != null ? DateTime.Parse(e.NewValues["fechaInicio"].ToString()) : Date.DateTimeNow();
                    dato.fechaFin = e.NewValues["fechaFin"] != null ? DateTime.Parse(e.NewValues["fechaFin"].ToString()) : Date.DateTimeNow();

                    dato.estado = e.NewValues["estado"].ToString();
                    dato.usuarioModificacion = Session["usuario"].ToString();
                    dato.fechaModificacion = Date.DateTimeNow();

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
                    var id = e.Values["emisor"].ToString();

                    //busca objeto
                    var itemToRemove = conexion.Plan.Find(id);
                    conexion.Plan.Remove(itemToRemove);
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
               // if (e.Column.FieldName == "emisor") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray;  }
            }
            else
            {
                if (e.Column.FieldName == "emisor") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
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

        protected void ASPxGridView2_BeforePerformDataSelect(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                long idProducto = long.Parse((sender as ASPxGridView).GetMasterRowKeyValue().ToString());


                ASPxGridView detailGird = ASPxGridView1.FindDetailRowTemplateControl(ASPxGridView1.FocusedRowIndex, "ASPxGridView2") as ASPxGridView;
                //detailGird.DataSource = conexion.ProductoImpuesto.Where(x => x.idProducto == idProducto).ToList();
                // detailGird.DataBind();
            }
        }

        protected void ASPxGridView2_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                long idProducto = long.Parse(ASPxGridView1.GetRowValues(e.VisibleIndex, "id").ToString());

                ASPxGridView detailGird = ASPxGridView1.FindDetailRowTemplateControl(e.VisibleIndex, "ASPxGridView2") as ASPxGridView;
                if (detailGird != null)
                {
                    detailGird.DataSource = conexion.ProductoImpuesto.Where(x => x.idProducto == idProducto).ToList();
                    detailGird.DataBind();
                }
            }
        }
    }
}
