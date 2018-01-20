using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDomain
{
    public class Emisor
    {
        [JsonProperty("tipoIdentificacion", Order = 1)]
        public string tipoIdentificacion { set; get; }

        [JsonProperty("numeroIdentificacion", Order = 2)]
        public string numeroIdentificacion { set; get; }

       // [JsonProperty("nombre")]
       // public string nombre { set; get; }

        public Emisor() { }
    }
}
