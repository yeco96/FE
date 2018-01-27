using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class Fax
    {
        /// <summary>
        /// tamaño 3  DGT
        /// </summary>
        [XmlElement(ElementName = "CodigoPais", Order = 1)]
        public string codigoPais { set; get; }



        [XmlElement(ElementName = "NumTelefono", Order = 2)]
        /// <summary>
        /// tamaño 20  DGT
        /// </summary>
        public string numTelefono { set; get; }
        public Fax() {
    }
    }
}
