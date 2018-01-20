using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDomain
{
    public class RecepcionPOST
    {
        [JsonProperty("clave",  Order = 1)]
        public string clave { set; get; }

        [JsonProperty("fecha", Order = 2)]
        public string fecha { set; get; }

        [JsonProperty("emisor", Order = 3)]
        public Emisor emisor;

        [JsonProperty("receptor", Order = 4)]
        public Receptor receptor;

        [JsonProperty("comprobanteXml", Order = 5)]
        public string comprobanteXml { set; get; }
        
        public RecepcionPOST() {
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            this.fecha = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");
        }
    }
}
