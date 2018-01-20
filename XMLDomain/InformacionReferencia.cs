using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
   public  class InformacionReferencia

    {

        [XmlElement(ElementName = "TipoDoc", Order = 1)]
        public string tipoDoc { set; get; }//tamaño 2 DGT
        [XmlElement(ElementName = "Numero", Order = 2)]
        public string numero { set; get; }//tamaño 50 DGT
        [XmlElement(ElementName = "FechaEmision", Order = 3)]
        public DateTime fechaEmision { set; get; }//tamaño 2 DGT
        /// <summary>
        /// Este Codigo campo será de condición obligatoria para la factura y
        ///tiquete electrónico, cuando se incluya información en el
        ///campo “Tipo de documento de referencia”
        /// </summary>
        [XmlElement(ElementName = "Codigo", Order = 4)]
        public string codigo { set; get; }//tamaño 2 DGT
        [XmlElement(ElementName = "Razon", Order = 5)]
        public string razon { set; get; }//tamaño 180 DGT
        public InformacionReferencia()
        {
        }
    }
}
