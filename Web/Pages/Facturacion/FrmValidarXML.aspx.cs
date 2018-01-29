using EncodeXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace Web.Pages.Facturacion
{
    public partial class FrmValidarXML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        
        protected void xmlUploadControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            try
            {
                string file = Convert.ToBase64String(e.UploadedFile.FileBytes);
                Session["xmlFileValidar"] = EncondeXML.base64Decode(file);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {

            string str = Session["xmlFileValidar"].ToString();
            txtClave.Text = EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(str), "Clave", str);
            //Emisor
            string emisorIdentificacion= EncondeXML.buscarValorEtiquetaXML("Emisor", "Identificacion", str);
            txtNumCedEmisor.Text = emisorIdentificacion.Substring(1);
            txtFechaEmisor.Text = EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(str), "FechaEmision", str);
            //Factura
            double totalImpuesto = Convert.ToDouble(EncondeXML.buscarValorEtiquetaXML("ResumenFactura", "TotalImpuesto", str));
            txtMontoTotalImpuesto.Text = string.Format("{0:C}",totalImpuesto);
            double totalFactura = Convert.ToDouble(EncondeXML.buscarValorEtiquetaXML("ResumenFactura", "TotalComprobante", str));
            txtTotalFactura.Text = string.Format("{0:C}", totalFactura);

            //Receptor
            string receptorIdentificacion = EncondeXML.buscarValorEtiquetaXML("Receptor", "Identificacion", str);
            txtNumCedReceptor.Text = receptorIdentificacion.Substring(1);
            txtNumConsecReceptor.Text = "";


        }


    }
}