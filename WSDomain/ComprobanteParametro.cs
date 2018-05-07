using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDomain
{
    public class ComprobanteParametro
    {
        public int offset;
        public int limit;
        public string emisor;
        public string receptor;

        public ComprobanteParametro()
        {
            this.offset = 0;
            this.limit = 0;
            this.emisor = "";
            this.receptor = "";
        }
    }
}

