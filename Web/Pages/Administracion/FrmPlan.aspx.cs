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
    [PrincipalPermission(SecurityAction.Demand, Role = "FACT")]
    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
    public partial class FrmPlan : System.Web.UI.Page
    {

        /// <summary>
        /// constructor
        /// </summary>
        public FrmPlan()
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
                this.ASPxGridView1.DataSource = conexion.Plan.Where(x=>x.emisor== emisor).ToList();
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
