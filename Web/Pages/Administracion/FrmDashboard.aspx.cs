using Class.Utilidades;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Facturacion;
using Web.Models.Grafico;

namespace Web.Pages.Administracion
{
    public partial class FrmDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                if (!IsPostBack && !IsCallback)
                {
                    this.CargarCombos();
                }
                this.generarGrafico();

                this.alertMessages.Attributes["class"] = "";
                this.alertMessages.InnerText = "";
            }
            catch (Exception ex)
            {
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
                this.alertMessages.Attributes["class"] = "alert alert-danger";
            }
        }

        /// <summary>
        /// carga los valores en el combo correspodiente
        /// </summary>
        private void CargarCombos()
        {
            this.cmbPeriodo.Items.Clear();
            int periodoInicio = 2018;
            int periodoFin = Date.DateTimeNow().Year;
            for (int i= periodoInicio; i < periodoFin; i++ )
            {
                this.cmbPeriodo.Items.Add(i.ToString());
            }
            this.cmbPeriodo.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;

        }



        public void generarGrafico()
        {
            String periodo = this.cmbPeriodo.Text;
            List<GraficoDocumento> lista = new List<GraficoDocumento>();
            string emisor = Session["emisor"].ToString();
            using (var conexion = new DataModelFE())
            {

                var datos = conexion.Database.SqlQuery<string>(
                    "select concat(x.periodo, '^', x.tipoDocumento, '^', x.cantidad) "+
                        "from( "+
                        "select date_format(fecha, '%Y%m') periodo, tipoDocumento, count(1)cantidad " +
                        "from ws_recepcion_documento " +
                        "where emisorIdentificacion = '603540974' " +
                        "group by date_format(fecha, '%Y%m'), tipoDocumento " +
                        "order by 1 asc " +
                        ") x").ToList();
                          
                foreach (var item in datos)
                {
                    string[] dato_sql = item.Split('^');
                    GraficoDocumento dato = new GraficoDocumento();
                    dato.periodo = dato_sql[0];
                    dato.tipo = dato_sql[1];
                    dato.cantidad = int.Parse(dato_sql[2]);
                    lista.Add(dato); 
                } 
            }

            this.wbDocumentos.Series.Clear();
            
            if (lista.Count != 0)
            {
                foreach (var item in lista)
                {
                    Series serieCorrecta = new Series(item.tipo, ViewType.Bar);
                    serieCorrecta.Points.Add(new SeriesPoint(item.tipo, item.cantidad));
                    serieCorrecta.CrosshairLabelPattern = "{S} → {V:n2}";
                    serieCorrecta.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    this.wbDocumentos.Series.Add(serieCorrecta);
                }

                ((DevExpress.XtraCharts.XYDiagram)wbDocumentos.Diagram).Rotated = true;

                //Hide the legend (if necessary).
                this.wbDocumentos.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

                // Add a title to the chart (if necessary).
                this.wbDocumentos.Titles.Clear();
                this.wbDocumentos.Titles.Add(new ChartTitle());
                this.wbDocumentos.Titles[0].Font = new Font(FontFamily.GenericSansSerif, 12);
                this.wbDocumentos.Titles[0].Text = String.Format("DOCUEMNTOS EMITIDOS {0}", periodo);

            }
            this.wbDocumentos.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.wbDocumentos.DataBind();

            //this.gvDatosEstadistica.DataSource = lista;
            //this.gvDatosEstadistica.DataBind();

        }
    }
}