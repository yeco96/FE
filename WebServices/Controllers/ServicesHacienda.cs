
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using OAuth2;
using System.Configuration;
using WebServices.Models.Facturacion;
using Class.Utilidades;
using EncodeXML;
using WebServices.Models;
using WSDomain;
using FirmaXadesNet;
using Newtonsoft.Json;
using System.Data.Entity;
using XMLDomain;
using System.Data.Entity.Validation;
using WebServices.Models.Administracion;
using System.Web.Http;

namespace WebServices.Controllers
{
    public class ServicesHacienda : ApiController
    {

        public static void AuthorizeUser(OAuth2Config authorization)
        {
            string Url = GetAuthorizationUrl(authorization);
            HttpContext.Current.Response.Redirect(Url, false);
        }
        /// <summary>    
        ///     
        /// </summary>    
        /// <param name="data"></param>    
        /// <returns></returns>    
        private static string GetAuthorizationUrl(OAuth2Config authorization)
        {
            //get this value by opening your web app in browser.    
            string RedirectUrl = "http://localhost:54762/Pages/facturacion/FrmCargarXML.aspx";
            string Url = authorization.server;
            StringBuilder UrlBuilder = new StringBuilder(Url);
            UrlBuilder.Append("client_id=" + authorization.clientId);
            UrlBuilder.Append("&username=" + authorization.username);
            UrlBuilder.Append("&password=" + authorization.password);
            UrlBuilder.Append("&client_secret=" + authorization.clientSecret);
            UrlBuilder.Append("&scope=" + authorization.scope);
            UrlBuilder.Append("&access_type=" + "password");
            UrlBuilder.Append("&redirect_uri=" + RedirectUrl);

            return UrlBuilder.ToString();
        }

 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static async Task<string> postRecepcion(OAuth2Token token, string jsonData)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                responseMessage = await httpClient.PostAsync(URLServices.RECEPCION_POST(), content);
                string x = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    return "Success";
                }
                else
                {
                    return "Error";
                }
            }


        }


        
         


        private DateTime tokenTime { set; get; }

        private static bool requiereNuevoToken(OAuth2.OAuth2Config config)
        {
            bool requiereToken = false;

            if (config == null)
            {
                requiereToken = true;
                return requiereToken;
            }

            DateTime fechaCreacionToken;
            if (System.Web.HttpContext.Current.Session["tokenTime"] != null)
            {
                fechaCreacionToken = (DateTime)System.Web.HttpContext.Current.Session["tokenTime"];
            }
            else
            {
                fechaCreacionToken = Date.DateTimeNow();
            }

            TimeSpan diferencia = Date.DateTimeNow().Subtract(fechaCreacionToken);
            if (diferencia.TotalSeconds > config.token.expires_in - 10)
            {
                requiereToken = true;
            }
             

            return requiereToken;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tieneFirma">determina si el XML esta firmado o no</param>
        /// <param name="documento">puede ser cualquer tipo de documento electronico</param>
        /// <param name="responsePost">respuesta del webs ervices</param>
        /// <param name="tipoDocumento">Facura, Nota Crédito, Nota Débito</param> 
        public static async Task<string> enviarDocumentoElectronico(bool tieneFirma, DocumentoElectronico documento, EmisorReceptorIMEC emisor, string tipoDocumento, string usuario)
        {
            String responsePost = "";
            try
            {
                string xmlFile = EncodeXML.EncondeXML.getXMLFromObject(documento);

                using (var conexion = new DataModelFE())
                {
                    OAuth2.OAuth2Config config = null;
                    if (System.Web.HttpContext.Current.Session["token"] != null)
                    {
                        config = (OAuth2.OAuth2Config)System.Web.HttpContext.Current.Session["token"];
                    }

                    if (requiereNuevoToken(config))
                    {
                        //Sessison["horaToken"]
                        string ambiente = ConfigurationManager.AppSettings["ENVIROMENT"].ToString();
                        config = conexion.OAuth2Config.Where(x => x.enviroment == ambiente).FirstOrDefault();
                        config.username = emisor.usernameOAuth2;
                        config.password = emisor.passwordOAuth2;

                        await OAuth2.OAuth2Config.getTokenWeb(config);
                        System.Web.HttpContext.Current.Session["token"] = config;
                        System.Web.HttpContext.Current.Session["tokenTime"] = Date.DateTimeNow();
                    }

                    Models.WS.WSRecepcionPOST trama = new Models.WS.WSRecepcionPOST();
                    trama.clave = EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(xmlFile), "Clave", xmlFile);
                    trama.fecha = DateTime.ParseExact(EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(xmlFile), "FechaEmision", xmlFile), "yyyy-MM-ddTHH:mm:ss-06:00",
                                       System.Globalization.CultureInfo.InvariantCulture);


                    string emisorIdentificacion = EncondeXML.buscarValorEtiquetaXML("Emisor", "Identificacion", xmlFile);
                    trama.emisor.tipoIdentificacion = emisorIdentificacion.Substring(0, 2);
                    trama.emisor.numeroIdentificacion = emisorIdentificacion.Substring(2);
                    trama.emisorTipo = trama.emisor.tipoIdentificacion;
                    trama.emisorIdentificacion = trama.emisor.numeroIdentificacion;

                    string receptorIdentificacion = EncondeXML.buscarValorEtiquetaXML("Receptor", "Identificacion", xmlFile);

                    if (!string.IsNullOrWhiteSpace(receptorIdentificacion))
                    {
                        trama.receptor.tipoIdentificacion = receptorIdentificacion.Substring(0, 2);
                        trama.receptor.numeroIdentificacion = receptorIdentificacion.Substring(2);
                    }
                    else
                    {
                        trama.receptor.tipoIdentificacion = "99";
                        trama.receptor.numeroIdentificacion = EncondeXML.buscarValorEtiquetaXML("Receptor", "IdentificacionExtranjero", xmlFile);
                    }

                    trama.receptorTipo = trama.receptor.tipoIdentificacion;
                    trama.receptorIdentificacion = trama.receptor.numeroIdentificacion;
                    trama.tipoDocumento = tipoDocumento;

                    //trama.callbackUrl = ConfigurationManager.AppSettings["ENVIROMENT_URL"].ToString();

                    if (!tieneFirma)
                    {
                        xmlFile = FirmaXML.getXMLFirmadoWeb(xmlFile, emisor.llaveCriptografica, emisor.claveLlaveCriptografica.ToString());
                    }

                    trama.comprobanteXml = EncodeXML.EncondeXML.base64Encode(xmlFile);

                    string jsonTrama = JsonConvert.SerializeObject(trama);

                    if (config.token.access_token != null)
                    {
                        responsePost = await postRecepcion(config.token, jsonTrama);

                        Models.WS.WSRecepcionPOST tramaExiste = conexion.WSRecepcionPOST.Find(trama.clave);

                        if (tramaExiste != null)
                        {// si existe
                            trama.fechaModificacion = Date.DateTimeNow();
                            trama.usuarioModificacion = usuario;
                            trama.indEstado = 0;
                            trama.cargarEmisorReceptor();
                            conexion.Entry(tramaExiste).State = EntityState.Modified;

                            documento.resumenFactura.clave = documento.clave;
                            conexion.Entry(documento.resumenFactura).State = EntityState.Modified;
                        }
                        else//si no existe
                        {
                            trama.fechaCreacion = Date.DateTimeNow();
                            trama.usuarioCreacion = usuario;
                            trama.cargarEmisorReceptor();
                            conexion.WSRecepcionPOST.Add(trama); //trama

                            documento.resumenFactura.clave = documento.clave;//resumen
                            conexion.ResumenFactura.Add(documento.resumenFactura);

                        }
                        conexion.SaveChanges();

                    }
                    else
                    {
                        // error de conexion de red
                        responsePost = "net_error";
                        return responsePost;
                    }
                     

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
            return responsePost;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="clave"></param>
        /// <returns>response ws json</returns>
        public static async Task<string> getRecepcion(OAuth2Token token, string clave)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URLServices.RECEPCION_GET_CLAVE(clave));
                responseMessage = await httpClient.SendAsync(request);
            }

            return await responseMessage.Content.ReadAsStringAsync();
        }




    }
}