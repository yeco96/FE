
using Class.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using Web.Models.Catalogos;
using WSDomain;
using XMLDomain;

namespace Web.Controllers
{
    [RoutePrefix("api/services")]
    public class ServicesController : ApiController
    {
         
        [HttpPost]
        [Route("recepcionmesajehacienda")]
        public async Task<string> recepcionMesajeHacienda()
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            string respuestaJSON = await Request.Content.ReadAsStringAsync();

            WSRecepcionGET respuesta = JsonConvert.DeserializeObject<WSRecepcionGET>(respuestaJSON);
            string respuestaXML = EncodeXML.EncondeXML.base64Decode(respuesta.respuestaXml);

            MensajeHacienda mensajeHacienda = new MensajeHacienda(respuestaXML);

            using (var conexionWS = new DataModelWS())
            {
                WSRecepcionPOST dato = conexionWS.WSRecepcionPOST.Find(mensajeHacienda.clave);
                dato.mensaje = mensajeHacienda.mensajeDetalle;
                dato.indEstado = mensajeHacienda.mensaje;
                dato.fechaModificacion = Date.DateTimeNow();
                //dato.usuarioModificacion = Session["usuario"].ToString();
                dato.montoTotalFactura = mensajeHacienda.montoTotalFactura;
                dato.montoTotalImpuesto = mensajeHacienda.montoTotalImpuesto;
                conexionWS.Entry(dato).State = EntityState.Modified;
                conexionWS.SaveChanges();
            }

            return "";
        }

        [HttpPost] 
        [Route("recepcionmesajehacienda2/{id}")]
        public IHttpActionResult recepcionMesajeHacienda2(string id)
        {
            try {
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                string respuestaJSON = id;

                WSRecepcionGET respuesta = JsonConvert.DeserializeObject<WSRecepcionGET>(respuestaJSON);
                string respuestaXML = EncodeXML.EncondeXML.base64Decode(respuesta.respuestaXml);

                MensajeHacienda mensajeHacienda = new MensajeHacienda(respuestaXML);

                using (var conexionWS = new DataModelWS())
                {
                    WSRecepcionPOST dato = conexionWS.WSRecepcionPOST.Find(mensajeHacienda.clave);
                    dato.mensaje = mensajeHacienda.mensajeDetalle;
                    dato.indEstado = mensajeHacienda.mensaje;
                    dato.fechaModificacion = Date.DateTimeNow();
                    //dato.usuarioModificacion = Session["usuario"].ToString();
                    dato.montoTotalFactura = mensajeHacienda.montoTotalFactura;
                    dato.montoTotalImpuesto = mensajeHacienda.montoTotalImpuesto;
                    conexionWS.Entry(dato).State = EntityState.Modified;
                    conexionWS.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return NotFound();
            }
            return Ok(""); 
        }



        [HttpPost]
        public async Task<string> recepcionXML()
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            string result = await Request.Content.ReadAsStringAsync();
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult comprobanteElectronicoXML(string id)
        {
            WSRecepcionPOST dato = null;
            string xml = "";
            using (var conexion = new DataModelWS())
            {
                dato = conexion.WSRecepcionPOST.Find(id);
            }

            if (dato != null)
            {
                xml = dato.comprobanteXml;
                xml = EncodeXML.EncondeXML.base64Decode(xml);
                return Ok(xml);
            }
            else
            {
                return NotFound();
            } 
        }

          
    }
}