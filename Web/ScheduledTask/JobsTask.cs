﻿using Class.Utilidades;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.Facturacion;
using WSDomain;
using XMLDomain;
using Class.Seguridad;
using System.Data.Entity;
using Web.WebServices;
using Newtonsoft.Json;
using EncodeXML;

namespace HighSchoolWeb.ScheduledTask
{
    public class JobsTask : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        { 
            await this.actualizarMensajesHacienda();
            this.decodeXML();
        }

        private void decodeXML()
        {
            using (var conexion = new DataModelFE())
            {
                List<WSRecepcionPOST> lista = conexion.WSRecepcionPOST.
                    Where(x => x.comprobanteXml.Substring(0,3) =="PD9" ).ToList();
                foreach (var item in lista)
                {
                    WSRecepcionPOST dato = conexion.WSRecepcionPOST.Find(item.clave); 
                    if(dato.comprobanteXml!=null)
                        dato.comprobanteXml = XMLUtils.base64Decode( dato.comprobanteXml);
                    if (dato.comprobanteRespXML != null )
                        if(dato.comprobanteRespXML.Substring(0, 3) == "PD9")
                            dato.comprobanteRespXML = XMLUtils.base64Decode(dato.comprobanteRespXML);
                     
                    conexion.Entry(dato).State = EntityState.Modified;
                    conexion.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Se actualizan los mensajes del ministerio de hacienda que esten pendientes
        /// </summary>
        /// <returns></returns>
        public async Task<bool> actualizarMensajesHacienda()
        {
            try
            {
                EmisorReceptorIMEC emisor = null;
                OAuth2.OAuth2Config config = null;
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();

                using (var conexion = new DataModelFE())
                {                                                                                                              
                    List<WSRecepcionPOST> lista = conexion.WSRecepcionPOST.
                        Where(x =>  x.indEstado == 0 /*ENVIADO*/    || 
                                    x.indEstado == 8  /*RECIBIDO*/   ||
                                    x.indEstado == 9  /*PENDIENTE  ||
                                    x.comprobanteRespXML == null*/).ToList();
                    foreach (var item in lista)
                    {

                        if (config == null) { 
                            emisor = conexion.EmisorReceptorIMEC.Find(Usuario.USUARIO_TOKEN);
                            string ambiente = ConfigurationManager.AppSettings["ENVIROMENT"].ToString();
                            config = conexion.OAuth2Config.Where(x => x.enviroment == ambiente).FirstOrDefault();
                            config.username = emisor.usernameOAuth2;
                            config.password = emisor.passwordOAuth2;
                            await OAuth2.OAuth2Config.getTokenWeb(config);
                        }

                      string respuestaJSON = await Services.getRecepcion(config.token, item.clave);

                        if (!string.IsNullOrWhiteSpace(respuestaJSON))
                        {
                            WSRecepcionGET respuesta = JsonConvert.DeserializeObject<WSRecepcionGET>(respuestaJSON);
                            if (respuesta.respuestaXml != null)
                            {
                                string respuestaXML = EncodeXML.XMLUtils.base64Decode(respuesta.respuestaXml);
                                MensajeHacienda mensajeHacienda = new MensajeHacienda(respuestaXML);
                                  
                                using (var conexionWS = new DataModelFE())
                                {
                                    WSRecepcionPOST dato = conexionWS.WSRecepcionPOST.Find(item.clave);
                                    dato.mensaje = mensajeHacienda.mensajeDetalle;
                                    dato.indEstado = mensajeHacienda.mensaje;
                                    dato.fechaModificacion = Date.DateTimeNow();
                                    dato.usuarioModificacion = Usuario.USUARIO_AUTOMATICO;
                                    dato.montoTotalFactura = mensajeHacienda.montoTotalFactura;
                                    dato.montoTotalImpuesto = mensajeHacienda.montoTotalImpuesto;
                                    dato.comprobanteRespXML = respuestaXML;

                                    if (mensajeHacienda.montoTotalFactura==0)
                                    {
                                        string xml = XMLUtils.base64Decode(dato.comprobanteXml);
                                        
                                        try
                                        {
                                            dato.montoTotalImpuesto = Convert.ToDecimal(XMLUtils.buscarValorEtiquetaXML("ResumenFactura", "TotalImpuesto", xml));
                                            dato.montoTotalFactura = Convert.ToDecimal(XMLUtils.buscarValorEtiquetaXML("ResumenFactura", "TotalComprobante", xml));
                                        }
                                        catch (Exception ex)
                                        {
                                            dato.montoTotalImpuesto = Convert.ToDecimal(XMLUtils.buscarValorEtiquetaXML("ResumenFactura", "TotalImpuesto", xml).Replace(".", ","));
                                            dato.montoTotalFactura = Convert.ToDecimal(XMLUtils.buscarValorEtiquetaXML("ResumenFactura", "TotalComprobante", xml).Replace(".",",") );
                                        }
                                    }

                                    conexionWS.Entry(dato).State = EntityState.Modified;
                                    conexionWS.SaveChanges();
                                     
                                }
                            }
                            else
                            {
                                if (respuesta.indEstado.Equals("recibido"))
                                {
                                    using (var conexionWS = new DataModelFE())
                                    {
                                        WSRecepcionPOST dato = conexionWS.WSRecepcionPOST.Find(item.clave); 
                                        dato.indEstado = 8/*recibido por hacienda*/;
                                        dato.fechaModificacion = Date.DateTimeNow();
                                        dato.usuarioModificacion = Usuario.USUARIO_AUTOMATICO; 
                                        conexionWS.Entry(dato).State = EntityState.Modified;
                                        conexionWS.SaveChanges();

                                    }
                                }
                                 
                            }
                        }
                    } 
                } 
            }
            catch (Exception e)
            {
                String data = e.Message;
            }
            return true;
        }
        
    }
}