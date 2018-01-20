using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDomain
{
    public class RecepcionGET
    {
        [JsonProperty("clave")]
        public string clave { set; get; }

        [JsonProperty("fecha")]
        public string fecha { set; get; }

        [JsonProperty("ind-estado")]
        public string indEstado { set; get; }

        [JsonProperty("respuesta-xml")]
        public string respuestaXml { set; get; }
        
        public RecepcionGET() {
            this.fecha = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");  
            // 2016-01-01T00:00:00-0600
        }
    }
}
