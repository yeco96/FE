using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
   public  class Impuesto
    {
        [XmlElement(ElementName = "Codigo", Order = 1)]
        public string codigo { set; get; }//tamaño 2  DGT
        [XmlElement(ElementName = "Tarifa", Order = 2)]
        public decimal tarifa { set; get; }//tamaño 4,2  DGT
        [XmlElement(ElementName = "Monto", Order = 3)]
        public decimal monto { set; get; }//tamaño 18,5  DGT

        public Impuesto(){ }
    }
}
