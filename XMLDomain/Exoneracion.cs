using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class Exoneracion
    {
        [XmlElement(ElementName = "TipoDocumento", Order = 1)]
        public string tipoDocumento { set; get; }//tamaño 2  DGT
        [XmlElement(ElementName = "NumeroDocumento", Order = 2)]
        public string numeroDocumento { set; get; }//tamaño 17  DGT
        [XmlElement(ElementName = "NombreInstitucion", Order = 3)]
        public string nombreInstitucion { set; get; }//tamaño 100  DGT
        [XmlElement(ElementName = "FechaEmision", Order = 4)]
        public DateTime fechaEmision { set; get; }
        [XmlElement(ElementName = "MontoImpuesto", Order = 5)]
        public decimal montoImpuesto { set; get; }//tamaño 18,5
        [XmlElement(ElementName = "PorcentajeCompra", Order = 6)]
        public int porcentajeCompra { set; get; }//tamaño 3  DGT
        public Exoneracion(){ }
    }
}
