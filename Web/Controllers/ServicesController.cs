﻿using Class.Seguridad;
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
using Web.Models;
using Web.Models.Facturacion;
using WSDomain;
using XMLDomain;

namespace Web.Controllers
{
    [RoutePrefix("api/services")]
    [Authorize]
    public class ServicesController : ApiController
    {
        [HttpPost]
        [Route("recepcionmesajehacienda")]
        public async Task<HttpResponseMessage> recepcionmesajehacienda()
        {
            string responsePost = "";
            try
            {
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                string xml = await Request.Content.ReadAsStringAsync();
                DocumentoElectronico documento = (DocumentoElectronico)EncodeXML.XMLUtils.getObjetcFromXML(xml);
                documento.verificaDatosParaXML();

                EmisorReceptorIMEC elEmisor = null;
                using (var conexion = new DataModelFE())
                {
                    long idE = long.Parse(documento.emisor.identificacion.numero);
                    elEmisor = conexion.EmisorReceptorIMEC.Find(idE.ToString());
                    if (elEmisor == null)
                    {
                        //return "Emisor no registrado!!!";
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Emisor no registrado!!!");
                        //return Ok("Emisor no registrado!!!");
                    }
                    else// si el xml es de un emisor valido, verifica si existe el receptor y si no lo crea 
                    {
                        string tipo = "";
                        string idReceptor = "";
                        if (!string.IsNullOrWhiteSpace(documento.receptor.identificacionExtranjero))
                        {
                            tipo = "99";
                            idReceptor = documento.receptor.identificacionExtranjero;
                        }
                        else{
                            //elimina ceros a la izquierda
                            long idR = long.Parse(documento.receptor.identificacion.numero);
                            tipo = documento.receptor.identificacion.tipo;
                            idReceptor = idR.ToString();
                        }
                        EmisorReceptorIMEC elReceptor = conexion.EmisorReceptorIMEC.Find(idReceptor);
                        if (elReceptor == null)
                        {
                            // crea un cliente para que se muestre el emisor
                            elReceptor = new EmisorReceptorIMEC();
                            elReceptor.identificacion = idReceptor;
                            elReceptor.identificacionTipo = tipo;
                            elReceptor.correoElectronico = documento.receptor.correoElectronico;
                            if (documento.receptor.telefono != null)
                            {
                                elReceptor.telefonoCodigoPais = documento.receptor.telefono.codigoPais;
                                elReceptor.telefono = documento.receptor.telefono.numTelefono;
                            }
                            elReceptor.nombre = documento.receptor.nombre;
                            elReceptor.nombreComercial = documento.receptor.nombreComercial;
                            conexion.EmisorReceptorIMEC.Add(elReceptor);
                            conexion.SaveChanges();
                        }
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
                //return fullErrorMessage;
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, fullErrorMessage);
                //return Ok(fullErrorMessage);
            }
            catch (Exception ex)
            {
                //return ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
                //return Ok(ex.Message);
            }
            //return responsePost;
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, responsePost);
            //return Ok(responsePost);

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

            try
            {
                using (var conexion = new DataModelFE())
                {

                   WSRecepcionPOST dato = conexion.WSRecepcionPOST.Find(clave);
                    if (dato != null)
                    {
                        if (dato.montoTotalFactura == 0)
                        {
                            return Ok(new WSRespuestaGET(dato));
                        }
                        else
                        {
                            string respuestaJSON = await ServicesHacienda.getRecepcion(await this.getToken(), clave);

                            if (!string.IsNullOrWhiteSpace(respuestaJSON))
                            {
                                WSRecepcionGET respuesta = JsonConvert.DeserializeObject<WSRecepcionGET>(respuestaJSON);
                                if (respuesta.respuestaXml != null)
                                {
                                    string respuestaXML = respuesta.respuestaXml;

                                    MensajeHacienda mensajeHacienda = new MensajeHacienda(respuestaXML);

                                    //dato = conexion.WSRecepcionPOST.Where(x=>x.clave==clave).FirstOrDefault();
                                    dato.mensaje = mensajeHacienda.mensajeDetalle;
                                    dato.indEstado = mensajeHacienda.mensaje;
                                    dato.fechaModificacion = Date.DateTimeNow();
                                    dato.usuarioModificacion = Usuario.USUARIO_AUTOMATICO;
                                    //dato.receptorIdentificacion = mensajeHacienda.receptorNumeroCedula;
                                    dato.montoTotalFactura = mensajeHacienda.montoTotalFactura;
                                    dato.montoTotalImpuesto = mensajeHacienda.montoTotalImpuesto;
                                    conexion.Entry(dato).State = EntityState.Modified;
                                    conexion.SaveChanges();

                                    return Ok(new WSRespuestaGET(dato));
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

                                            return Ok(new WSRespuestaGET(dato));
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                return NotFound();

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
                //return fullErrorMessage; 
                return Ok(fullErrorMessage);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }





        private async Task<OAuth2.OAuth2Token> getToken()
        {
            OAuth2.OAuth2Config config = null;
            using (var conexionAuth = new DataModelFE())
            {
                EmisorReceptorIMEC elEmisor = conexionAuth.EmisorReceptorIMEC.Find(Usuario.USUARIO_TOKEN);
                string ambiente = ConfigurationManager.AppSettings["ENVIROMENT"].ToString();
                config = conexionAuth.OAuth2Config.Where(x => x.enviroment == ambiente).FirstOrDefault();
                config.username = elEmisor.usernameOAuth2;
                config.password = elEmisor.passwordOAuth2;
                await OAuth2.OAuth2Config.getTokenWeb(config);
               
            }
            return config.token;
        }

    }
}