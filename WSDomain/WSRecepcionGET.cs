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
   
    public class WSRecepcionGET
    {
       
        [JsonProperty("clave")]
        public string clave { set; get; }

        [JsonProperty("fecha")]
        public string fecha { set; get; }

        [JsonProperty("ind-estado")]
        public string indEstado { set; get; }

        [JsonProperty("respuesta-xml")]
        public string respuestaXml { set; get; }
        
        public WSRecepcionGET() {
            this.fecha = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");  
            // 2016-01-01T00:00:00-0600
        }
    }
}
