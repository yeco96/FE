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
using Web.Models;
using Web.Models.Facturacion;
using WSDomain;
using XMLDomain;
using Class.Seguridad;
using System.Data.Entity;
using Web.WebServices;
using Newtonsoft.Json;

namespace HighSchoolWeb.ScheduledTask
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
                    List<WSRecepcionPOST> lista = conexion.WSRecepcionPOST.Where(x => x.indEstado == 0).ToList();
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
                                string respuestaXML = EncodeXML.EncondeXML.base64Decode(respuesta.respuestaXml);
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
                                    conexionWS.Entry(dato).State = EntityState.Modified;
                                    conexionWS.SaveChanges();

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