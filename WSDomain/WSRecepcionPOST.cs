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
    public class WSRecepcionPOST
    {
        [Key]
        [JsonProperty("clave",  Order = 1)]
        public string clave { set; get; }

        [JsonProperty("fecha", Order = 2)]
        public DateTime? fecha { set; get; }

        [NotMapped]
        [JsonProperty("emisor", Order = 3)]
        public Emisor emisor { set; get; }

        [NotMapped]
        [JsonProperty("receptor", Order = 4)]
        public Receptor receptor { set; get; }
        [NotMapped]
        public string factura { get { return clave.Substring(21,20); } }

        [JsonProperty("comprobanteXml", Order = 5)]
        public string comprobanteXml { set; get; }

        public int indEstado { set; get; }
        public string mensaje { set; get; }

        public string emisorTipo { set; get; }
        //[ForeignKey("emisor")]
        public string emisorIdentificacion { set; get; }
        public string receptorTipo { set; get; }
        //[ForeignKey("receptor")]
        public string receptorIdentificacion { set; get; }
         
        public double montoTotalImpuesto { set; get; }
         
        public double montoTotalFactura { set; get; }


        /// <summary>
        /// AUDIOTORIA
        /// </summary> 

        // [ForeignKey("UsuarioCreacion")]
        public string usuarioCreacion { set; get; }

        public DateTime? fechaCreacion { set; get; }

       // [ForeignKey("UsuarioModificacion")]
        public string usuarioModificacion { set; get; }

        public DateTime? fechaModificacion { set; get; }


        public WSRecepcionPOST() {
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            //this.fecha = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");
            this.fecha = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central America Standard Time");
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
       // public virtual Usuario UsuarioCreacion { get; set; }
       // public virtual Usuario UsuarioModificacion { get; set; }


    }
}
