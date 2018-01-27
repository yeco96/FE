using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class Telefono
    {
        [XmlElement(ElementName = "CodigoPais", Order = 1)]
        public string codigoPais { set; get; }//tamaño 3  DGT
        [XmlElement(ElementName = "numTelefono", Order = 2)]
        public string numTelefono { set; get; }//tamaño 20  DGT

         
        public Telefono() {

    }
}
}

