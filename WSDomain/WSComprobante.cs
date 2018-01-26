using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDomain
{
    class WSComprobante
    {
        [JsonProperty("clave", Order = 1)]
        public string clave;

        [JsonProperty("attributes", Order = 2)]
        public string fecha;

        [JsonProperty("emisor", Order = 3)]
        public EmisorReceptor emisor;

        [JsonProperty("receptor", Order = 4)]
        public EmisorReceptor receptor;

        [JsonProperty("notasCredito", Order = 5)]
        public List<NotaCredito> notasCredito;

        [JsonProperty("notasDebito", Order = 6)]
        public List<NotaDebito> notasDebito;

        public WSComprobante()
        {
            this.emisor = new EmisorReceptor();
            this.receptor = new EmisorReceptor();
            this.notasCredito = new List<NotaCredito>();
            this.notasDebito = new List<NotaDebito>();
            this.fecha = null;
            this.clave = null; 
        }
    }
}
