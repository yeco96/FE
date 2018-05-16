using Class.Utilidades;
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
using WSDomain;
using XMLDomain;
using System.Data.Entity;
using Newtonsoft.Json;
using WebServices.Models.Facturacion;
using WebServices.Models;
using WebServices.Seguridad;
using WebServices.Controllers;

namespace WebServices.ScheduledTask
{
    public class JobsTask : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        { 
            await this.actualizarMensajesHacienda();
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

                using (var conexion = new DataModelFE())
                {
                    // se buscan los paquetes que esten enviados y esten esperando un respuesta de hacienda (indEstado == 8) o esten enviandas (indEstado == 0)
                    List<Models.WS.WSRecepcionPOST> lista = conexion.WSRecepcionPOST.Where(x => x.indEstado == 0 || x.indEstado == 8 ).ToList();
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

                      string respuestaJSON = await ServicesHacienda.getRecepcion(config.token, item.clave);

                        if (!string.IsNullOrWhiteSpace(respuestaJSON))
                        {
                            WSRecepcionGET respuesta = JsonConvert.DeserializeObject<WSRecepcionGET>(respuestaJSON);
                            if (respuesta.respuestaXml != null)
                            {
                                string respuestaXML = EncodeXML.XMLUtils.base64Decode(respuesta.respuestaXml);
                                MensajeHacienda mensajeHacienda = new MensajeHacienda(respuestaXML);
                                  
                                using (var conexionWS = new DataModelFE())
                                {
                                    Models.WS.WSRecepcionPOST dato = conexionWS.WSRecepcionPOST.Find(item.clave);
                                    dato.mensaje = mensajeHacienda.mensajeDetalle;
                                    dato.indEstado = mensajeHacienda.mensaje;
                                    dato.fechaModificacion = Date.DateTimeNow();
                                    dato.usuarioModificacion = Usuario.USUARIO_AUTOMATICO;
                                    dato.montoTotalFactura = mensajeHacienda.montoTotalFactura;
                                    dato.montoTotalImpuesto = mensajeHacienda.montoTotalImpuesto;
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
                                        Models.WS.WSRecepcionPOST dato = conexionWS.WSRecepcionPOST.Find(item.clave); 
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