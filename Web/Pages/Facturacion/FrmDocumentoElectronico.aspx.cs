using Class.Utilidades;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Facturacion;
using Web.WebServices;
using WSDomain;
using XMLDomain;

namespace Web.Pages.Facturacion
{
    public partial class FrmDocumentoElectronico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.AsyncMode = true;
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
                throw new Exception(ex.Message, ex.InnerException);
            } 
        }

        /// <summary>
        /// carga solo una vez para ahorar tiempo 
        /// </summary>
        private void cargarCombos()
        {
            // Cargar valores de combo para estado
            GridViewDataComboBoxColumn comboEstado = this.ASPxGridView1.Columns["indEstado"] as GridViewDataComboBoxColumn;
            comboEstado.PropertiesComboBox.Items.Clear(); 
            comboEstado.PropertiesComboBox.Items.AddRange(EstadoMensajeHaciendaClass.values());
        }

        

        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            using (var conexion = new DataModelWS())
            {
                this.ASPxGridView1.DataSource = conexion.WSRecepcionPOST.ToList();
                this.ASPxGridView1.DataBind();
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

        protected void ASPxGridView1_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
        {

        }

        protected async void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexion = new DataModelOAuth2())
                {
                    EmisorReceptor emisor = (EmisorReceptor)Session["emisor"];
                    string ambiente = ConfigurationManager.AppSettings["ENVIROMENT"].ToString();
                    OAuth2.OAuth2Config config = conexion.OAuth2Config.Where(x => x.enviroment == ambiente).FirstOrDefault();
                    config.username = emisor.usernameOAuth2;
                    config.password = emisor.passwordOAuth2;

                    await OAuth2.OAuth2Config.getTokenWeb(config);

                    string clave = Session["clave"].ToString();
                    string respuestaJSON = await Services.getRecepcion(config.token, clave);

                    WSRecepcionGET respuesta = JsonConvert.DeserializeObject<WSRecepcionGET>(respuestaJSON);
                    string respuestaXML = EncodeXML.EncondeXML.base64Decode(respuesta.respuestaXml);
                     
                    MensajeHacienda mensajeHacienda = new MensajeHacienda(respuestaXML);

                    using (var conexionWS = new DataModelWS())
                    {
                        WSRecepcionPOST dato = conexionWS.WSRecepcionPOST.Find(clave);
                        dato.mensaje = mensajeHacienda.mensajeDetalle;
                        dato.indEstado = mensajeHacienda.mensaje;
                        dato.fechaModificacion = Date.DateTimeNow();
                        dato.usuarioModificacion = Session["usuario"].ToString();
                        dato.receptorIdentificacion = mensajeHacienda.receptorNumeroCedula;
                        dato.montoTotalFactura = mensajeHacienda.montoTotalFactura;
                        dato.montoTotalImpuesto = mensajeHacienda.montoTotalImpuesto;
                        conexionWS.Entry(dato).State = EntityState.Modified;
                        conexionWS.SaveChanges();
                    }
                    

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
                if (e.Column.FieldName == "clave") { e.Editor.ReadOnly = true; e.Column.ReadOnly = true; e.Editor.BackColor = System.Drawing.Color.LightGray; }
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

               throw new Exception(Utilidades.validarExepcionSQL(error), e.Exception.InnerException);
            }
        }

        protected void ASPxGridView1_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            Session["clave"] = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "clave");
        }
    }
}