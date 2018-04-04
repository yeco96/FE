using DevExpress.Utils.OAuth;
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
using Web.Models.Facturacion;
using Class.Utilidades;
using EncodeXML;
using Web.Models;
using WSDomain;
using FirmaXadesNet;
using Newtonsoft.Json;
using System.Data.Entity;
using XMLDomain;
using System.Data.Entity.Validation;
using Web.Models.Administracion;

namespace Web.WebServices
{
    public class Services
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public static async Task<string> getComprobante(OAuth2Token token, string clave)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URLServices.COMPROBANTE_GET_CLAVE(clave));
                responseMessage = await httpClient.SendAsync(request);
            }

            return await responseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public static async Task<string> getComprobantes(OAuth2Token token, string parametro)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URLServices.COMPROBANTE_GET(parametro));
                responseMessage = await httpClient.SendAsync(request);
            }

            return await responseMessage.Content.ReadAsStringAsync();
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
                    string ambiente = ConfigurationManager.AppSettings["ENVIROMENT"].ToString();
                    OAuth2.OAuth2Config config = conexion.OAuth2Config.Where(x => x.enviroment == ambiente).FirstOrDefault();
                    config.username = emisor.usernameOAuth2;
                    config.password = emisor.passwordOAuth2;

                    await OAuth2.OAuth2Config.getTokenWeb(config);

                    WSDomain.WSRecepcionPOST trama = new WSDomain.WSRecepcionPOST();
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
                    trama.consecutivoReceptor = null;
                    trama.comprobanteXml = EncodeXML.EncondeXML.base64Encode(xmlFile);

                    string jsonTrama = JsonConvert.SerializeObject(trama);

                    if (config.token.access_token != null)
                    {
                        responsePost = await Services.postRecepcion(config.token, jsonTrama);
                    }
                    else
                    {
                        // facturar guardada para envio en linea
                        trama.indEstado = 9;
                    }


                    WSRecepcionPOST tramaExiste = conexion.WSRecepcionPOST.Find(trama.clave);

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
                        conexion.WSRecepcionPOST.Add(trama);

                        documento.resumenFactura.clave = documento.clave;
                        conexion.ResumenFactura.Add(documento.resumenFactura);

                        Plan plan = conexion.Plan.Find(emisor.identificacion);
                        if (plan != null)
                        {
                            plan.cantidadDocEmitido += 1;
                            conexion.Entry(plan).State = EntityState.Modified;
                        }
                    } 
                    conexion.SaveChanges();

                    //guarda la relacion de clientes asociados al emisor
                    Cliente cliente = conexion.Cliente.Where(x => x.emisor == trama.emisor.numeroIdentificacion).Where(x => x.receptor == trama.receptor.numeroIdentificacion).FirstOrDefault();
                    if (cliente == null && !string.IsNullOrWhiteSpace(trama.receptor.numeroIdentificacion))
                    {
                        cliente = new Cliente();
                        cliente.emisor = trama.emisor.numeroIdentificacion;
                        cliente.receptor = trama.receptor.numeroIdentificacion;
                        conexion.Cliente.Add(cliente);
                        conexion.SaveChanges();
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
        /// <param name="xmlFile">XML sin firmar</param>
        /// <param name="responsePost">respuesta del webs ervices</param>
        /// <param name="receptorTipoIdentificacion">tipoo identificacion del receptor</param>
        public static async Task<string> enviarMensajeReceptor(string xmlFile, EmisorReceptorIMEC emisor, string receptorTipoIdentificacion)
        {
            String responsePost = "";
            try
            {
                using (var conexion = new DataModelFE())
                {
                    string ambiente = ConfigurationManager.AppSettings["ENVIROMENT"].ToString();
                    OAuth2.OAuth2Config config = conexion.OAuth2Config.Where(x => x.enviroment == ambiente).FirstOrDefault();
                    config.username = emisor.usernameOAuth2;
                    config.password = emisor.passwordOAuth2;

                    await OAuth2.OAuth2Config.getTokenWeb(config);

                    WSDomain.WSRecepcionPOST trama = new WSDomain.WSRecepcionPOST();
                    trama.clave = EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(xmlFile), "Clave", xmlFile);

                    trama.emisor.tipoIdentificacion = emisor.identificacionTipo;
                    trama.emisor.numeroIdentificacion = EncondeXML.buscarValorEtiquetaXML("MensajeReceptor", "NumeroCedulaEmisor", xmlFile);

                    trama.receptor.tipoIdentificacion = receptorTipoIdentificacion;
                    trama.receptor.numeroIdentificacion = EncondeXML.buscarValorEtiquetaXML("MensajeReceptor", "NumeroCedulaReceptor", xmlFile);

                    xmlFile = FirmaXML.getXMLFirmadoWeb(xmlFile, emisor.llaveCriptografica, emisor.claveLlaveCriptografica.ToString());

                    trama.consecutivoReceptor= EncondeXML.buscarValorEtiquetaXML(EncondeXML.tipoDocumentoXML(xmlFile), "NumeroConsecutivoReceptor", xmlFile);
                    trama.comprobanteXml = EncodeXML.EncondeXML.base64Encode(xmlFile);

                    string jsonTrama = JsonConvert.SerializeObject(trama);

                    responsePost = await Services.postRecepcion(config.token, jsonTrama);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
            return responsePost;
        }


    }
}