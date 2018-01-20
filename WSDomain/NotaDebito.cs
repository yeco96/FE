using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDomain
{
    public class NotaDebito
    {
        [JsonProperty("clave")]
        public string clave { set; get; }

        [JsonProperty("fecha")]
        public string fecha { set; get; }

        public NotaDebito() { }
    }
}
