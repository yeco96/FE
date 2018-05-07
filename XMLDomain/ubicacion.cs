using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
   public class Ubicacion
    {
        [XmlElement(ElementName = "Provincia", Order = 1)]
        public string provincia { set; get; }//tamaño en hacienda igual a 1
        [XmlElement(ElementName = "Canton", Order = 2)]
        public string canton { set; get; }//tamaño en hacienda igual a 2
        [XmlElement(ElementName = "Distrito", Order = 3)]
        public string distrito { set; get; }//tamaño en hacienda igual a 2
        [XmlElement(ElementName = "Barrio", Order = 4)]
        public string barrio { set; get; }//tamaño en hacienda igual a 2
        [XmlElement(ElementName = "OtrasSenas", Order =5)]
        public string otrassenas { set; get; }//tamaño en hacienda igual a 160

        public Ubicacion() {
        }

}
}
