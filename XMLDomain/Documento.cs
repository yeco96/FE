using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLDomain
{
    public class Documento
    {
        public List<Impresion> documentoImpreso { set; get; }
        public Documento()
        {
            this.documentoImpreso = new List<Impresion>();

        }

    }
}
