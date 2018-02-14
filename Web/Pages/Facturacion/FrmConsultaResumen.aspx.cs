using Class.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;

namespace Web.Pages.Facturacion
{
    public partial class FrmConsultaResumen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.alertMessages.Attributes["class"] = "";
            this.alertMessages.InnerText = "";
            this.AsyncMode = true;

            try
            {
                if (!IsCallback && !IsPostBack)
                {
                    this.txtFechaInicio.Date = DateTime.Today;
                    this.txtFechaFin.Date = Date.DateTimeNow();
                }
                this.refreshData();
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex.Message);
            }

        }

        /// <summary>
        /// carga inicial de todos los registros
        /// </summary>  
        private void refreshData()
        {
            using (var conexion = new DataModelFE())
            {
                //this.ASPxGridView1.DataSource = conexion.WSRecepcionPOST.Where(x => x.fecha >= txtFechaInicio.Date && x.fecha <= txtFechaFin.Date).OrderByDescending(x => x.fecha).ToList();
                this.ASPxGridView1.DataSource = conexion.ResumenFactura.Where(x => x.clave.Contains("%%")).OrderByDescending(x => x.clave).ToList();


                string usuario = Session["usuario"].ToString();
                this.ASPxGridView1.DataSource = (from ResumenFactura in conexion.ResumenFactura
                                                 from recepcioDocumento in conexion.WSRecepcionPOST
                                                 where (recepcioDocumento.clave == ResumenFactura.clave && recepcioDocumento.emisorIdentificacion == usuario && recepcioDocumento.fecha >= txtFechaInicio.Date && recepcioDocumento.fecha <= txtFechaFin.Date)
                                                 select new {
                                                     clave = ResumenFactura.clave.Substring(21, 20),
                                                     codigoMoneda = ResumenFactura.codigoMoneda,
                                                     tipoCambio=ResumenFactura.tipoCambio,
                                                     totalServGravados=ResumenFactura.totalServGravados,
                                                     totalServExentos=ResumenFactura.totalServExentos,
                                                     totalMercanciasGravadas=ResumenFactura.totalMercanciasGravadas,
                                                     totalMercanciasExentas=ResumenFactura.totalMercanciasExentas,
                                                     totalGravado = ResumenFactura.totalGravado,
                                                     totalExento=ResumenFactura.totalExento,
                                                     totalVenta=ResumenFactura.totalVenta,
                                                     totalDescuentos=ResumenFactura.totalDescuentos,
                                                     totalVentaNeta=ResumenFactura.totalVentaNeta,
                                                     totalImpuesto=ResumenFactura.totalImpuesto,
                                                     totalComprobante=ResumenFactura.totalComprobante
                                                 }
                                                 ).ToList();



                this.ASPxGridView1.DataBind();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            refreshData();
        }
    }
}