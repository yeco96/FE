﻿using Class.Utilidades;
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
using Web.Models.Facturacion;

namespace Web.Pages.Facturacion
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmConsecutivoDocElectronico : System.Web.UI.Page
    {

        /// <summary>
        /// constructor
        /// </summary>
        public FrmConsecutivoDocElectronico()
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
                string usuario = Session["emisor"].ToString();
                this.ASPxGridView1.DataSource = conexion.ConsecutivoDocElectronico.Where(x => x.emisor == usuario).ToList();
                this.ASPxGridView1.DataBind();
            }
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

            /* TIPO DOCUMENTO */
            using (var conexion = new DataModelFE())
            {
                GridViewDataComboBoxColumn comboTipoConsecutivo = this.ASPxGridView1.Columns["tipoDocumento"] as GridViewDataComboBoxColumn;
                foreach (var item in conexion.TipoConsecutivo.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    comboTipoConsecutivo.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }
                comboTipoConsecutivo.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
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
                    ConsecutivoDocElectronico dato = new ConsecutivoDocElectronico();
                    //llena el objeto con los valores de la pantalla
                    dato.emisor = e.NewValues["emisor"] != null ? e.NewValues["emisor"].ToString().ToUpper() : null;
                    dato.sucursal = e.NewValues["sucursal"] != null ? e.NewValues["sucursal"].ToString().PadLeft(3, '0') : "001";
                    dato.caja = e.NewValues["caja"] != null ? e.NewValues["caja"].ToString().PadLeft(3, '0') : "00001";
                    dato.tipoDocumento = e.NewValues["tipoDocumento"] != null ? e.NewValues["tipoDocumento"].ToString() : "01";
                    dato.consecutivo = e.NewValues["consecutivo"] != null ? long.Parse(e.NewValues["consecutivo"].ToString()) : 0 ;
                    dato.digitoVerificador= e.NewValues["digitoVerificador"].ToString();
                    dato.estado = e.NewValues["estado"].ToString();
                    dato.usuarioCreacion = Session["usuario"].ToString();
                    dato.fechaCreacion = Date.DateTimeNow();

                    //agrega el objeto
                    conexion.ConsecutivoDocElectronico.Add(dato);
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
                    // se declara el objeto a insertar
                    ConsecutivoDocElectronico dato = new ConsecutivoDocElectronico();
                    //llena el objeto con los valores de la pantalla
                    dato.emisor = e.NewValues["emisor"] != null ? e.NewValues["emisor"].ToString().ToUpper() : null;
                    dato.sucursal = e.NewValues["sucursal"] != null ? e.NewValues["sucursal"].ToString().PadLeft(3, '0') : "001";
                    dato.caja = e.NewValues["caja"] != null ? e.NewValues["caja"].ToString().PadLeft(3,'0') : "00001";
                    dato.tipoDocumento = e.NewValues["tipoDocumento"] != null ? e.NewValues["tipoDocumento"].ToString() : "01";

                    //busca el objeto  
                    object[] key = new object[] { dato.emisor, dato.sucursal, dato.caja, dato.tipoDocumento };
                    dato = conexion.ConsecutivoDocElectronico.Find(key);

                    dato.digitoVerificador = e.NewValues["digitoVerificador"].ToString();
                    dato.consecutivo = e.NewValues["consecutivo"] != null ? long.Parse(e.NewValues["consecutivo"].ToString()) : 0;
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
                    ConsecutivoDocElectronico dato = new ConsecutivoDocElectronico(); 

                    dato.emisor = e.Values["emisor"] != null ? e.Values["emisor"].ToString().ToUpper() : null;
                    dato.sucursal = e.Values["sucursal"] != null ? e.Values["sucursal"].ToString().PadLeft(3, '0') : "001";
                    dato.caja = e.Values["caja"] != null ? e.Values["caja"].ToString().PadLeft(3, '0') : "00001";
                    dato.tipoDocumento = e.Values["tipoDocumento"] != null ? e.Values["tipoDocumento"].ToString() : "01";

                    //busca el objeto  
                    object[] key = new object[] { dato.emisor, dato.sucursal, dato.caja, dato.tipoDocumento };
                    dato = conexion.ConsecutivoDocElectronico.Find(key); 
                    conexion.ConsecutivoDocElectronico.Remove(dato);
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
            if (this.ASPxGridView1.IsNewRowEditing)
            {
                if (e.Column.FieldName == "emisor") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; e.Editor.Value = Session["usuario"].ToString(); }
            }
            else
            {
                if (e.Column.FieldName == "emisor") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "sucursal") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "caja") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
                if (e.Column.FieldName == "tipoDocumento") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
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


 