using EncodeXML;
using FirmaXadesNet;
using Newtonsoft.Json;
using OAuth2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Facturacion;
using Web.WebServices;
using WSDomain;

namespace Web.Pages.Facturacion
{
    public partial class FrmCargarXML : System.Web.UI.Page
    {

        public FrmCargarXML()
        {
            //Session.Remove("xmlFile");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.AsyncMode = true;
            this.loadHTML();
        }

        public void loadHTML()
        {
            if (Session["xmlFile"] != null)
            {
                this.HtmlEditor.Html = Session["xmlFile"].ToString();
                this.HtmlEditor.DataBind();
            }
        }
        protected void HTMLEditorXMLData_HtmlCorrecting(object sender, DevExpress.Web.ASPxHtmlEditor.HtmlCorrectingEventArgs e)
        {
            e.Handled = true;
        }

        protected void DocumentsUploadControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            try
            {
                string file = Convert.ToBase64String(e.UploadedFile.FileBytes);
                Session["xmlFile"] = EncondeXML.base64Decode(file);


                this.loadHTML();

                //if (e.IsValid)
                //  e.CallbackData = tempFileInfo.UniqueFileName + "|" + isSubmissionExpired;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }


        protected void btnFirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string xmlFile = Session["xmlFile"].ToString();

                EmisorReceptor dato = null;
                using (var conexion = new DataModelFE())
                {
                    string usuario = Session["usuario"].ToString();
                    dato = conexion.EmisorReceptor.Where(x => x.identificacion == usuario).FirstOrDefault();
                }

                string xmlFileSigned = FirmaXML.getXMLFirmadoWeb(xmlFile, dato.llaveCriptografica, dato.claveLlaveCriptografica);
                Session["xmlFile"] = xmlFileSigned;

                this.loadHTML();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }



        protected async void btnEnviar_Click(object sender, EventArgs e)
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
                      
                    string xmlFile = Session["xmlFile"].ToString();
                    WSDomain.WSRecepcionPOST trama = new WSDomain.WSRecepcionPOST();
                    trama.clave = EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(xmlFile), "Clave", xmlFile);
                    trama.emisor.tipoIdentificacion = EncondeXML.buscarValorEtiquetaXML("Identificacion", "Tipo", xmlFile);
                    trama.emisor.numeroIdentificacion = EncondeXML.buscarValorEtiquetaXML("Identificacion", "Numero", xmlFile);
                    trama.receptor.tipoIdentificacion = EncondeXML.buscarValorEtiquetaXML("Identificacion", "Tipo", xmlFile);
                    trama.receptor.numeroIdentificacion = EncondeXML.buscarValorEtiquetaXML("Identificacion", "Numero", xmlFile);
                    trama.comprobanteXml = EncodeXML.EncondeXML.base64Encode(xmlFile);

                    string jsonTrama = JsonConvert.SerializeObject(trama);

                    using (var conexion2 = new DataModelWS())
                    {
                        WSRecepcionPOST tramaObjeto = JsonConvert.DeserializeObject<WSRecepcionPOST>(jsonTrama);
                        conexion2.WSRecepcionPOST.Add(tramaObjeto);
                        conexion2.SaveChanges();
                    }

                    string responsePost = "";
                    await Services.postRecepcion(config.token, jsonTrama, responsePost); 
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        protected void btnMostrarXML_Click(object sender, EventArgs e)
        {
            this.loadHTML();
        }
 
   
        

    }

}