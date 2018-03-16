using Class.Utilidades;
using FirmaXadesNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml.Serialization;
using WebServices.Models;
using WebServices.Models.Facturacion;
using WebServices.Seguridad;
using WSDomain;
using XMLDomain;

namespace WebServices.Controllers
{
    [RoutePrefix("api/services")]
    public class ServicesController : ApiController
    {

        [HttpPost]
        [Route("recepcionmesajehacienda")]
        public async Task<string> recepcionMesajeHacienda()
        {
            string responsePost = "";
            try
            { 
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                string xml = await Request.Content.ReadAsStringAsync();
                DocumentoElectronico documento = (DocumentoElectronico)EncodeXML.EncondeXML.getObjetcFromXML(xml);
                documento.verificaDatosParaXML();
                 
                EmisorReceptorIMEC elEmisor = null;
                using (var conexion = new DataModelFE())
                { 
                    elEmisor = conexion.EmisorReceptorIMEC.Find(documento.emisor.identificacion.numero);
                    if (elEmisor == null)
                    {
                        return "Emisor no registrado!!!";
                    }
                } 
                responsePost = await ServicesHacienda.enviarDocumentoElectronico(false, documento, elEmisor, documento.tipoDocumento, Usuario.USUARIO_AUTOMATICO); 
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
                return fullErrorMessage; 
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return responsePost;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("respuestamesajehacienda/{clave}")] 
        public async Task<IHttpActionResult> respuestamesajehacienda(string clave)
        { 
            using (var conexion = new DataModelFE())
            {

                Models.Facturacion.WSRecepcionPOST dato = conexion.WSRecepcionPOST.Find(clave);
                if (dato.montoTotalFactura > 0)
                {
                    return Ok(new Models.Facturacion.WSRespuestaGET(dato));
                }
                else
                {
                    EmisorReceptorIMEC elEmisor = conexion.EmisorReceptorIMEC.Find(Usuario.USUARIO_TOKEN);
                    string ambiente = ConfigurationManager.AppSettings["ENVIROMENT"].ToString();
                    OAuth2.OAuth2Config config = conexion.OAuth2Config.Where(x => x.enviroment == ambiente).FirstOrDefault();
                    config.username = elEmisor.usernameOAuth2;
                    config.password = elEmisor.passwordOAuth2;

                    await OAuth2.OAuth2Config.getTokenWeb(config);

                    string respuestaJSON = await ServicesHacienda.getRecepcion(config.token, clave);

                    if (!string.IsNullOrWhiteSpace(respuestaJSON))
                    {
                        WSRecepcionGET respuesta = JsonConvert.DeserializeObject<WSRecepcionGET>(respuestaJSON);
                        if (respuesta.respuestaXml != null)
                        {
                            string respuestaXML = EncodeXML.EncondeXML.base64Decode(respuesta.respuestaXml);

                            MensajeHacienda mensajeHacienda = new MensajeHacienda(respuestaXML);

                            dato = conexion.WSRecepcionPOST.Find(clave);
                            dato.mensaje = mensajeHacienda.mensajeDetalle;
                            dato.indEstado = mensajeHacienda.mensaje;
                            dato.fechaModificacion = Date.DateTimeNow();
                            dato.usuarioModificacion = Usuario.USUARIO_AUTOMATICO;
                            //dato.receptorIdentificacion = mensajeHacienda.receptorNumeroCedula;
                            dato.montoTotalFactura = mensajeHacienda.montoTotalFactura;
                            dato.montoTotalImpuesto = mensajeHacienda.montoTotalImpuesto;
                            conexion.Entry(dato).State = EntityState.Modified;
                            conexion.SaveChanges();

                            return Ok(new Models.Facturacion.WSRespuestaGET(dato));
                        }
                        else
                        {
                            if (respuesta.indEstado.ToLower().Equals("recibido"))
                            {
                                using (var conexionWS = new DataModelFE())
                                {
                                    dato = conexionWS.WSRecepcionPOST.Find(clave);
                                    dato.indEstado = 8/*recibido por hacienda*/;
                                    dato.fechaModificacion = Date.DateTimeNow();
                                    dato.usuarioModificacion = Usuario.USUARIO_AUTOMATICO;
                                    conexionWS.Entry(dato).State = EntityState.Modified;
                                    conexionWS.SaveChanges();

                                    return Ok(new Models.Facturacion.WSRespuestaGET(dato));
                                }
                            }
                        }
                    }
                }

            }
            return NotFound();
        }
         
    }
}