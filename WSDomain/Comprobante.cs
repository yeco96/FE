using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDomain
{
    class Comprobante
    {
        [JsonProperty("clave", Order = 1)]
        public string clave;

        [JsonProperty("attributes", Order = 2)]
        public string fecha;

        [JsonProperty("emisor", Order = 3)]
        public Emisor emisor;

        [JsonProperty("receptor", Order = 4)]
        public Receptor receptor;

        [JsonProperty("notasCredito", Order = 5)]
        public List<NotaCredito> notasCredito;

        [JsonProperty("notasDebito", Order = 6)]
        public List<NotaDebito> notasDebito;

        public Comprobante()
        {
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            this.notasCredito = new List<NotaCredito>();
            this.notasDebito = new List<NotaDebito>();
            this.fecha = null;
            this.clave = null; 
        }
    }
}
