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
    [Table("ws_recepcion_documento")] 
    public class WSRespuestaGET
    {
        [Key]
        [JsonProperty("clave",  Order = 1)]
        public string clave { set; get; }

        [JsonProperty("fecha", Order = 2)]
        public DateTime? fecha { set; get; }

        [JsonProperty("numeroConsecutivo", Order = 3)]
        public string numeroConsecutivo { set; get; }

        [JsonProperty("tipoDocumento", Order = 4)]
        public string tipoDocumento { set; get; }
		 
        [JsonProperty("indEstado", Order = 5)]
        public int indEstado { set; get; }

        [JsonProperty("mensaje", Order = 6)]
        public string mensaje { set; get; }

        [JsonProperty("montoTotalImpuesto", Order = 7)]
        public decimal montoTotalImpuesto { set; get; }

        [JsonProperty("montoTotalFactura", Order = 8)]
        public decimal montoTotalFactura { set; get; }
        
		 
        public WSRespuestaGET(WSRecepcionPOST dato) { 
            this.fecha = dato.fecha;
            this.clave = dato.clave;
            this.numeroConsecutivo = dato.numeroConsecutivo;
            this.indEstado = dato.indEstado;
            this.mensaje = dato.mensaje;
            this.tipoDocumento = this.numeroConsecutivo.Substring(8, 2);
            this.montoTotalFactura = dato.montoTotalFactura;
            this.montoTotalImpuesto = dato.montoTotalImpuesto;
        }
		 

    }
}
