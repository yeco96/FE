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
using Web.Models.Catalogos;
using Web.Models.Facturacion;

namespace Web.Pages.Catalogos
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
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
                if (!IsCallback && !IsPostBack)
                {
                    using (var conexion = new DataModelFE())
                    {
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
        /// carga datos del receptor
        /// </summary>
        /// <param name="receptor"></param>
        private void loadReceptor(EmisorReceptorIMEC receptor)
        {
            this.cmbReceptorTipo.Value = receptor.identificacionTipo;
            this.txtReceptorIdentificacion.Text = receptor.identificacion;
            this.txtReceptorNombre.Text = receptor.nombre;
            this.txtReceptorNombreComercial.Text = receptor.nombreComercial;

            this.cmbReceptorTelefonoCod.Value = receptor.telefonoCodigoPais;
            this.cmbReceptorFaxCod.Value = receptor.faxCodigoPais;
            this.txtReceptorTelefono.Value = receptor.telefono;
            this.txtReceptorFax.Value = receptor.fax;
            this.txtReceptorCorreo.Text = receptor.correoElectronico;

            this.cmbReceptorProvincia.Value = receptor.provincia;

            this.cmbReceptorProvincia_ValueChanged(null, null);
            this.cmbReceptorCanton.Value = receptor.canton;

            this.cmbReceptorCanton_ValueChanged(null, null);
            this.cmbReceptorDistrito.Value = receptor.distrito;

            this.cmbReceptorDistrito_ValueChanged(null, null);
            this.cmbReceptorBarrio.Value = receptor.barrio;
            this.txtReceptorOtraSenas.Value = receptor.otraSena;
            this.cmbEstado.Value = receptor.estado;

        }

        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
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

                this.cmbEstado.Items.Clear();
                this.cmbEstado.Items.AddRange(Enum.GetValues(typeof(Estado)));

                /* IDENTIFICACION TIPO */
                GridViewDataComboBoxColumn comboIdentificacionTipo = this.ASPxGridView1.Columns["identificacionTipo"] as GridViewDataComboBoxColumn;
                comboIdentificacionTipo.PropertiesComboBox.Items.Clear();
                foreach (var item in conexion.TipoIdentificacion.ToList())
                {
                    this.cmbReceptorTipo.Items.Add(item.descripcion, item.codigo);
                    comboIdentificacionTipo.PropertiesComboBox.Items.Add(item.descripcion, item.codigo);
                }

                /* PROVINCIA*/
                foreach (var item in conexion.Ubicacion.Select(x => new { x.codProvincia, x.nombreProvincia }).Distinct())
                {
                    this.cmbReceptorProvincia.Items.Add(item.nombreProvincia, item.codProvincia);
                }

                /* CODIGO PAIS */
                foreach (var item in conexion.CodigoPais.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                { 
                    this.cmbReceptorTelefonoCod.Items.Add(item.descripcion, item.codigo);
                    this.cmbReceptorFaxCod.Items.Add(item.descripcion, item.codigo);
                } 
                this.cmbReceptorTelefonoCod.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                this.cmbReceptorFaxCod.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                

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

       
        protected void cmbReceptorProvincia_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
               
                this.cmbReceptorCanton.SelectedItem = null;
                this.cmbReceptorCanton.Items.Clear();
                this.cmbReceptorDistrito.SelectedItem = null;
                this.cmbReceptorDistrito.Items.Clear();
                this.cmbReceptorBarrio.SelectedItem = null;
                this.cmbReceptorBarrio.Items.Clear();

                foreach (var item in conexion.Ubicacion.
                    Where(x => x.codProvincia == this.cmbReceptorProvincia.Value.ToString()).
                    Select(x => new { x.codCanton, x.nombreCanton }).Distinct())
                {
                    this.cmbReceptorCanton.Items.Add(item.nombreCanton, item.codCanton);
                }
                this.cmbReceptorCanton.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }
        protected void cmbReceptorCanton_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            { 
                this.cmbReceptorDistrito.SelectedItem = null;
                this.cmbReceptorDistrito.Items.Clear();
                this.cmbReceptorBarrio.SelectedItem = null;
                this.cmbReceptorBarrio.Items.Clear();

                foreach (var item in conexion.Ubicacion.
                    Where(x => x.codProvincia == this.cmbReceptorProvincia.Value.ToString()).
                    Where(x => x.codCanton == this.cmbReceptorCanton.Value.ToString()).
                    Select(x => new { x.codDistrito, x.nombreDistrito }).Distinct())
                {
                    this.cmbReceptorDistrito.Items.Add(item.nombreDistrito, item.codDistrito);
                }
                this.cmbReceptorDistrito.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }


        protected void cmbReceptorDistrito_ValueChanged(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                this.cmbReceptorBarrio.SelectedItem = null;
                this.cmbReceptorBarrio.Items.Clear();
                foreach (var item in conexion.Ubicacion.
                    Where(x => x.codProvincia == this.cmbReceptorProvincia.Value.ToString()).
                    Where(x => x.codCanton == this.cmbReceptorCanton.Value.ToString()).
                     Where(x => x.codDistrito == this.cmbReceptorDistrito.Value.ToString()).
                    Select(x => new { x.codBarrio, x.nombreBarrio }).Distinct())
                {
                    this.cmbReceptorBarrio.Items.Add(item.nombreBarrio, item.codBarrio);
                }
                this.cmbReceptorBarrio.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }

          
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {

                    string buscar = this.txtReceptorIdentificacion.Text;
                    EmisorReceptorIMEC receptor = conexion.EmisorReceptorIMEC.Where(x => x.identificacion == buscar).FirstOrDefault();

                    receptor.identificacionTipo = this.cmbReceptorTipo.Value.ToString();
                    receptor.identificacion = this.txtReceptorIdentificacion.Text;
                    receptor.nombre = this.txtReceptorNombre.Text;
                    receptor.nombreComercial = this.txtReceptorNombreComercial.Text;

                    if (this.cmbReceptorTelefonoCod.Value != null)
                    {
                        receptor.telefonoCodigoPais = this.cmbReceptorTelefonoCod.Value.ToString();
                        receptor.telefono = this.txtReceptorTelefono.Text;
                    }

                    receptor.correoElectronico = this.txtReceptorCorreo.Text;

                    if (this.cmbReceptorFaxCod.Value != null)
                    {
                        receptor.faxCodigoPais = this.cmbReceptorFaxCod.Value.ToString();
                        receptor.fax = this.txtReceptorFax.Text;
                    }

                    if (this.cmbReceptorProvincia.Value != null)
                    {
                        receptor.provincia = this.cmbReceptorProvincia.Value.ToString();
                    }
                    if (this.cmbReceptorCanton.Value != null)
                    {
                        receptor.canton = this.cmbReceptorCanton.Value.ToString();
                    }
                    if (this.cmbReceptorDistrito.Value != null)
                    {
                        receptor.distrito = this.cmbReceptorDistrito.Value.ToString();
                    }
                    if (this.cmbReceptorBarrio.Value != null)
                    {
                        receptor.barrio = this.cmbReceptorBarrio.Value.ToString();
                    }
                    receptor.otraSena = this.txtReceptorOtraSenas.Text;
                    receptor.estado = this.cmbEstado.Text;

                    //modifica objeto
                    conexion.Entry(receptor).State = EntityState.Modified;
                    conexion.SaveChanges();
                     
                    this.alertMessages.Attributes["class"] = "alert alert-info";
                    this.alertMessages.InnerText = "Los datos fueron aplicados correctamente!!!"; 

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
                this.refreshData();
            }

        }


        protected void ASPxGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as ASPxGridView).GetSelectedFieldValues("identificacion").Count > 0)
                {
                    string emisorUsuario = (sender as ASPxGridView).GetSelectedFieldValues("identificacion")[0].ToString();
                    using (var conexion = new DataModelFE())
                    {
                        EmisorReceptorIMEC receptor = conexion.EmisorReceptorIMEC.Where(x => x.identificacion == emisorUsuario).FirstOrDefault();
                        
                        this.loadReceptor(receptor);
                        this.alertMessages.Attributes["class"] = "alert alert-info";
                        this.alertMessages.InnerText = "Los datos fueron cargados correctamente!!!";
                        this.btnActualizar.Enabled = true;
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
        }
    }
}
