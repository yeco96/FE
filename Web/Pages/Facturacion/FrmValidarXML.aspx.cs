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

                string file1 = Convert.ToBase64String(e.UploadedFile.FileBytes);
                Session["xmlFile"] = EncondeXML.base64Decode(file1);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {

            string str = Session["xmlFile"].ToString();
            txtXML.Text = str;
            txtClave.Text = valores(str);
            

        }

        private static string valores(string pXML)
        {
            string dato = "";
            
            dato = EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(pXML),"Clave",pXML);



            return dato;
        }

    }
}