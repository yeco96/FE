using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDomain
{
   
    public class WSReceptorPOST
    {
        /// <summary>
        /// Clave numérica del comprobante
        /// </summary>
        [JsonProperty("Clave", Order =1)] 
        public string clave { set; get; }

        /// <summary>
        /// Número de cédula fisica/jurídica/NITE/DIMEX del vendedor
        /// </summary>
        [JsonProperty("NumeroCedulaEmisor", Order = 2)]
        public string numeroCedulaEmisor { set; get; }

        /// <summary>
        /// Fecha de emision de la confirmación
        /// </summary>
        [JsonProperty("FechaEmisionDoc", Order = 3)]
        public string fechaEmisionDoc { set; get; }

        /// <summary>
        /// Codigo del mensaje de respuesta. 1 aceptado, 2 aceptado parcialmente, 3 rechazado
        /// </summary>
        [JsonProperty("Mensaje", Order = 4)]
        public int mensaje { set; get; }

        /// <summary>
        /// Detalle del mensaje
        /// </summary>
        [JsonProperty("DetalleMensaje", Order = 5)]
        public string detalleMensaje { set; get; }

        /// <summary>
        /// Monto total del impuesto, que es obligatorio si el  comprobante tenga impuesto.
        /// </summary>
        [JsonProperty("MontoTotalImpuesto", Order = 6)]
        public decimal montoTotalImpuesto { set; get; }

        /// <summary>
        /// Monto total de la factura
        /// </summary>
        [JsonProperty("TotalFactura", Order =7)]
        public decimal montoTotalFactura { set; get; }

        /// <summary>
        /// Número de cédula fisica/jurídica/NITE/DIMEX del comprador
        /// </summary>
        [JsonProperty("NumeroCedulaReceptor", Order = 8)]
        public string numeroCedulaReceptor { set; get; }

        /// <summary>
        /// umeración consecutiva de los mensajes de confirmación
        /// </summary>
        [JsonProperty("NumeroConsecutivoReceptor", Order = 9)]
        public string numeroConsecutivoReceptor { set; get; }


        public WSReceptorPOST() {
            this.fechaEmisionDoc = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss-06:00");  
            // 2016-01-01T00:00:00-0600
        }
    }
}
