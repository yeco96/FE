using Class.Seguridad;
using Class.Utilidades;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using EncodeXML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using Web.Models.Facturacion;
using Web.WebServices;
using WSDomain;
using XMLDomain;

namespace Web.Pages.Facturacion
{
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public partial class FrmAdministracionDocProforma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.alertMessages.Attributes["class"] = "";
            this.alertMessages.InnerText = "";
            this.AsyncMode = true;

            try
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Pages/Login.aspx");
                }
                if (!IsCallback && !IsPostBack)
                {
                    this.txtFechaInicio.Date = DateTime.Today;
                    this.txtFechaFin.Date = Date.DateTimeNow();
                    this.cargarCombos();
                }
                this.refreshData();
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
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

            /* TIPO DOCUMENTO */
            GridViewDataComboBoxColumn comboTipoDocumento = this.ASPxGridView1.Columns["tipoDocumento"] as GridViewDataComboBoxColumn;
            using (var conexion = new DataModelFE())
            {
                foreach (var item in conexion.TipoDocumento.Where(x => x.estado == Estado.ACTIVO.ToString()).ToList())
                {
                    comboTipoDocumento.PropertiesComboBox.Items.Add(item.descripcion.Replace("ELECTRÓNICA",""), item.codigo);
                }
                comboTipoDocumento.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
        }



        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            string emisor = Session["emisor"].ToString();  
            using (var conexion = new DataModelFE())
            {
                List<WSRecepcionPOSTProforma> lista = conexion.WSRecepcionPOSTProforma.Where(x => x.fecha >= txtFechaInicio.Date && x.fecha <= txtFechaFin.Date && x.emisorIdentificacion == emisor).
                    OrderByDescending(x => x.fecha).OrderByDescending(x => x.fechaCreacion).ToList();

                foreach (var item in lista)
                {
                    item.verificaTipoDocumentoCambioMoneda();
                    item.Receptor = conexion.EmisorReceptor.Find(item.receptorIdentificacion);
                }
                this.ASPxGridView1.DataSource = lista;
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

        protected void ASPxGridView1_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            Session["clave"] = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "clave");
            Session["indEstado"] = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "indEstado");
            Session["tipoDocumento"] = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "tipoDocumento");
        }

        protected void btnDescargarXML_Click(object sender, EventArgs e)
        {
            try
            { 
                    string xml = "";

                    using (var conexion = new DataModelFE())
                    {
                        string clave = Session["clave"].ToString();
                        WSRecepcionPOST dato = conexion.WSRecepcionPOST.Where(x => x.clave == clave).FirstOrDefault();
                        xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);
                    }
                    Response.Clear();
                    Response.ClearHeaders();

                    Response.AddHeader("Content-Length", xml.Length.ToString());
                    Response.ContentType = "application/xml";
                Response.AppendHeader("content-disposition", String.Format("attachment;filename=\"{0}.xml\"", Session["clave"].ToString()));

                    Response.Write(xml);
                    Response.End(); 
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }

        }

        protected void btnReenvioCorreo_Click(object sender, EventArgs e)
        {
            try
            {

                if (TipoDocumento.ACEPTADO.ToString().Equals(Session["indEstado"].ToString()))
                {
                    Response.Redirect("~/Pages/Facturacion/FrmReenvioOtrosCorreos.aspx");
                }
                else
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = String.Format("Documento eléctronico no se encuentra ACEPTADO");
                }
            }
            catch (Exception ex)
            {
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
        }
 
     
        protected void btnDescargarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (TipoDocumento.ACEPTADO.ToString().Equals(Session["indEstado"].ToString()))
                { 
                    string clave = Session["clave"].ToString(); 
                      
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-disposition", String.Format("attachment;filename=\"{0}.pdf\"", Session["clave"].ToString()));

                    Response.BinaryWrite(UtilidadesReporte.generarPDF(clave).ToArray()); 
                    Response.Flush(); 
                    Response.End();

                }
                else
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = String.Format("Documento eléctronico no se encuentra ACEPTADO");
                }

            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
        }

        protected void btnVerDocumento_Click(object sender, EventArgs e)
        {
            string clave = Session["clave"].ToString();
            string emisor = Session["emisor"].ToString();

            using (var conexion = new DataModelFE())
            {
                Empresa empresa = conexion.Empresa.Find(emisor);
                //Validamos la informacion de empresa
                if (empresa.tipoImpresion.Equals("A4"))
                {
                    Response.Redirect("~/Pages/Consulta/" + clave);
                }
                else
                {
                    Response.Redirect("~/Pages/ConsultaRP/" + clave);
                }
            }
        }
    }
}