using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class DetalleServicio
    {  
        /// <summary>
        /// Esta lista contiene todos los detalles de la factura
        /// </summary>
        [XmlElement(ElementName="LineaDetalle", Order = 1)]
        public List<LineaDetalle> lineaDetalle { set; get; }
        public DetalleServicio() {
            this.lineaDetalle = new List<LineaDetalle>();

        } 
    }
}
