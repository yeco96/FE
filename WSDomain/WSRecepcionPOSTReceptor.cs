﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDomain
{
    [Table("ws_recepcion_xml_documento")] 
    public class WSRecepcionPOSTReceptor
    {
        [Key]
        [JsonProperty("clave",  Order = 1)]
        public string clave { set; get; }

        [JsonProperty("fecha", Order = 2)]
        public DateTime? fecha { set; get; }

        [NotMapped]
        [JsonProperty("emisor", Order = 3)]
        public EmisorReceptor emisor { set; get; }
        [NotMapped]
        [JsonProperty("receptor", Order = 4)]
        public EmisorReceptor receptor { set; get; }

        [NotMapped]
        [JsonIgnore]
        public string numeroConsecutivo { get { return clave.Substring(21,20); } }

        
        [JsonIgnore]
        public string tipoDocumento { set; get; }

        //[NotMapped]
        //[JsonProperty("callbackUrl", Order = 5)]
        //public string callbackUrl { set; get; }


        [JsonProperty("comprobanteXml", Order = 6)]
        public string comprobanteXml { set; get; }

        [JsonIgnore]
        public int indEstado { set; get; }
        [JsonIgnore]
        public string mensaje { set; get; }

        [JsonIgnore]
        public string emisorTipo { set; get; }
        [JsonIgnore]
        public string emisorIdentificacion { set; get; }

        [JsonIgnore]
        public string receptorTipo { set; get; }

        [ForeignKey("Receptor")]
        [JsonIgnore]
        public string receptorIdentificacion { set; get; }

        [JsonIgnore]
        public decimal montoTotalImpuesto { set; get; }
        [JsonIgnore]
        public decimal montoTotalFactura { set; get; }
        

        /// <summary>
        /// AUDIOTORIA
        /// </summary> 

        [JsonIgnore]
        public string usuarioCreacion { set; get; }
        [JsonIgnore]
        public DateTime? fechaCreacion { set; get; }

        [JsonIgnore]
        public string usuarioModificacion { set; get; }
        [JsonIgnore]
        public DateTime? fechaModificacion { set; get; }


        public WSRecepcionPOSTReceptor() {
            this.receptor = new EmisorReceptor();
            this.emisor = new EmisorReceptor();
            //this.fecha = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");
            this.fecha = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central America Standard Time");
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        // public virtual Usuario UsuarioCreacion { get; set; }
        // public virtual Usuario UsuarioModificacion { get; set; }
        [JsonIgnore]
        public virtual EmisorReceptor Receptor { set; get; }

        /// <summary>
        /// carga los datos del emisor y recetor a las variables mateadas a la base de datos
        /// </summary>
        public void cargarEmisorReceptor()
        {
            this.emisorIdentificacion = this.emisor.numeroIdentificacion;
            this.emisorTipo = this.emisor.tipoIdentificacion;

            this.receptorIdentificacion = this.receptor.numeroIdentificacion;
            this.receptorTipo = this.receptor.tipoIdentificacion;

        }

        /// <summary>
        /// Verifica si existe en el documento
        /// </summary>
        /// <returns></returns>
        public bool existe()
        {
            if (string.IsNullOrWhiteSpace(this.usuarioCreacion) || string.IsNullOrWhiteSpace(this.usuarioModificacion))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
