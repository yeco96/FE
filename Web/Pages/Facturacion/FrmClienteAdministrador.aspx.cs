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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;

namespace Web.Pages.Catalogos
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmClienteAdministrador : System.Web.UI.Page
    {

        /// <summary>
        /// constructor
        /// </summary>
        public FrmClienteAdministrador()
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
                    using (var conexion = new DataModelFE())
                    {
                        /* EMISOR */
                        string emisorUsuario = Session["usuario"].ToString();
                        EmisorReceptorIMEC emisor = conexion.EmisorReceptorIMEC.Where(x => x.identificacion == emisorUsuario).FirstOrDefault();
                        this.loadEmisor(emisor);
                    }

                    this.loadComboBox(); 
                }
                this.refreshData();
            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
        }


        /// <summary>
        /// carga datos del emisor
        /// </summary>
        /// <param name="emisor"></param>
        private void loadEmisor(EmisorReceptorIMEC emisor)
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

            this.cmbEmisorProvincia_ValueChanged(null, null);
            this.cmbEmisorCanton.Value = emisor.canton;

            this.cmbEmisorCanton_ValueChanged(null, null);
            this.cmbEmisorDistrito.Value = emisor.distrito;

            this.cmbEmisorDistrito_ValueChanged(null, null);
            this.cmbEmisorBarrio.Value = emisor.barrio;
            this.txtEmisorOtraSenas.Value = emisor.otraSena;

            this.txtUsernameOAuth2.Text = emisor.usernameOAuth2;
            this.txtPasswordOAuth2.Text = emisor.passwordOAuth2;
            this.txtClaveLlaveCriptografica.Text = emisor.claveLlaveCriptografica.ToString();


        }

        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            using (var conexion = new DataModelFE())
            {
                string emisorUsuario = Session["usuario"].ToString();
                this.ASPxGridView1.DataSource = conexion.EmisorReceptorIMEC.Where(x=>x.identificacion== emisorUsuario).ToList();
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

                /* IDENTIFICACION TIPO */
                foreach (var item in conexion.TipoIdentificacion.ToList())
                {
                    this.cmbEmisorTipo.Items.Add(item.descripcion, item.codigo); 
                }
                this.cmbEmisorTipo.IncrementalFilteringMode = IncrementalFilteringMode.Contains; 
                 
                /* PROVINCIA*/
                foreach (var item in conexion.Ubicacion.Select(x => new { x.codProvincia, x.nombreProvincia }).Distinct())
                {
                    this.cmbEmisorProvincia.Items.Add(item.nombreProvincia, item.codProvincia); 
                }
                this.cmbEmisorProvincia.IncrementalFilteringMode = IncrementalFilteringMode.Contains; 

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
                    EmisorReceptorIMEC dato = new EmisorReceptorIMEC();
                    //llena el objeto con los valores de la pantalla
                    dato.identificacionTipo = e.NewValues["identificacionTipo"] != null ? e.NewValues["identificacionTipo"].ToString().ToUpper() : null;
                    dato.identificacion = e.NewValues["identificacion"] != null ? e.NewValues["identificacion"].ToString().ToUpper() : null;

                    dato.nombre = e.NewValues["nombre"] != null ? e.NewValues["nombre"].ToString().ToUpper() : null;
                    
                    dato.nombreComercial = e.NewValues["nombreComercial"] != null ? e.NewValues["nombreComercial"].ToString().ToUpper() : null;
                    
                    dato.usuarioCreacion = Session["usuario"].ToString();
                    dato.fechaCreacion = Date.DateTimeNow();

                    //agrega el objeto
                    conexion.EmisorReceptorIMEC.Add(dato);
                    conexion.SaveChanges();

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
                    var id = e.Values["identificacionTipo"].ToString();

                    //busca objeto
                    var itemToRemove = conexion.EmisorReceptorIMEC.SingleOrDefault(x => x.identificacionTipo == id);
                    conexion.EmisorReceptorIMEC.Remove(itemToRemove);
                    conexion.SaveChanges();

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



        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }
    }
}
