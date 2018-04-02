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
using Web.Utils;

namespace Web.Pages.Catalogos
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmCatalogoConfiguracionCorreo : System.Web.UI.Page
    {

        /// <summary>
        /// constructor
        /// </summary>
        public FrmCatalogoConfiguracionCorreo()
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
                this.alertMessages.InnerText = "";
                this.alertMessages.Attributes["class"] = "";
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
                this.ASPxGridView1.DataSource = conexion.ConfiguracionCorreo.Where(x=>x.codigo== emisor).ToList(); 
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

            /* SSL */
            GridViewDataComboBoxColumn comboSSL= this.ASPxGridView1.Columns["ssl"] as GridViewDataComboBoxColumn;
            comboSSL.PropertiesComboBox.Items.Clear();
            comboSSL.PropertiesComboBox.Items.AddRange(Enum.GetValues(typeof(Confirmacion)));

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
                    ConfiguracionCorreo dato = new ConfiguracionCorreo();
                    //llena el objeto con los valores de la pantalla
                    dato.codigo =  Session["usuario"].ToString();
                    dato.host = e.NewValues["host"] != null ? e.NewValues["host"].ToString() : null;
                    dato.port = e.NewValues["port"] != null ? e.NewValues["port"].ToString().ToUpper() : null;
                    dato.user = e.NewValues["user"] != null ? e.NewValues["user"].ToString() : null;
                    dato.ssl = e.NewValues["ssl"] != null ? e.NewValues["ssl"].ToString() : null;
                    dato.password = e.NewValues["password"] != null ? Ale5Util.Encriptar( e.NewValues["password"].ToString()) : null;
                    
                    dato.estado = e.NewValues["estado"].ToString();
                    dato.usuarioCreacion = Session["usuario"].ToString();
                    dato.fechaCreacion = Date.DateTimeNow();

                    //agrega el objeto
                    conexion.ConfiguracionCorreo.Add(dato);
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
                    ConfiguracionCorreo dato = new ConfiguracionCorreo();
                    //llena el objeto con los valores de la pantalla
                    dato.codigo = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString() : null;

                    //busca el objeto 
                    dato = conexion.ConfiguracionCorreo.Find(dato.codigo);

                    if(e.NewValues["password"]!=null)
                        dato.password = e.NewValues["password"] != null ? Ale5Util.Encriptar(e.NewValues["password"].ToString()) : null;
                    
                    dato.codigo = e.NewValues["codigo"] != null ? e.NewValues["codigo"].ToString() : null;
                    dato.ssl = e.NewValues["ssl"] != null ? e.NewValues["ssl"].ToString() : null;
                    dato.port = e.NewValues["port"] != null ? e.NewValues["port"].ToString().ToUpper() : null;
                    dato.user = e.NewValues["user"] != null ? e.NewValues["user"].ToString(): null;
                    dato.host = e.NewValues["host"] != null ? e.NewValues["host"].ToString() : null;
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
                    var codigo = e.Values["codigo"].ToString();

                    //busca objeto
                    var itemToRemove = conexion.ConfiguracionCorreo.SingleOrDefault(x => x.codigo == codigo);
                    conexion.ConfiguracionCorreo.Remove(itemToRemove);
                    conexion.SaveChanges();

                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridView1.CancelEdit();

                    ((ASPxGridView)sender).JSProperties["cpUpdatedMessage"] = "Los datos se eliminaron correctamente, puede continuar.";
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

        /// <summary>
        /// desabilita los campos que no son editables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (this.ASPxGridView1.IsNewRowEditing)
            {
                if (e.Column.FieldName.Equals("codigo"))
                {
                    e.Editor.ReadOnly = true;
                    e.Column.ReadOnly = true;
                    e.Editor.BackColor = System.Drawing.Color.LightGray;
                    e.Editor.Value = Session["usuario"].ToString();
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

        protected void btnVerificarCorreo_Click(object sender, EventArgs e)
        {
            try
            {
                string emisor = Session["emisor"].ToString();
                using (var conexion = new DataModelFE())
                {
                    ConfiguracionCorreo correo = conexion.ConfiguracionCorreo.Find(emisor);
                    if (correo != null && !string.IsNullOrWhiteSpace(this.txtCorreo.Text) )
                    {
                       bool result =  Utilidades.sendMail(emisor, this.txtCorreo.Text,
                          "TEST", Utilidades.mensageGenericoPruebaCorreo(), "Confuguración CORREO",null);

                        if (result)
                        {
                            this.alertMessages.InnerText = "Correo enviado con éxito";
                            this.alertMessages.Attributes["class"] = "alert alert-info";
                        }
                        else
                        {
                            this.alertMessages.InnerText = "Correo no enviado, verificar configuración";
                            this.alertMessages.Attributes["class"] = "alert alert-danger";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
                this.alertMessages.Attributes["class"] = "alert alert-danger";
            }
        }
    }
}
