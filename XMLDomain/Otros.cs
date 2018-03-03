using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class Otros
    {
        [XmlElement(ElementName = "OtroTexto", Order = 1)]
        public List<string>  otrosTextos;

        public Otros() {
            otrosTextos = new List<string>();
        }
    }
}
