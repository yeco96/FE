using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
   public  class LineaDetalle
    {
        [XmlElement(ElementName = "NumeroLinea", Order = 1)]
        public int numeroLinea { set; get; }
        [XmlElement(ElementName = "Codigo", Order = 2)]
        public Codigo codigo { set; get; }
        [XmlElement(ElementName = "Cantidad", Order = 3)]
        public decimal cantidad { set; get; }//tamaño 16,3 DGT
        [XmlElement(ElementName = "UnidadMedida", Order = 4)]
        public string unidadMedida { set; get; }//tamaño 15 DGT
        [XmlElement(ElementName = "UnidadMedidaComercial", Order = 5)]
        public string unidadMedidaComercial { set; get; }//tamaño 15 DGT
        [XmlElement(ElementName = "Detalle", Order = 6)]
        public string detalle { set; get; }//tamaño 160 DGT
        [XmlElement(ElementName = "PrecioUnitario", Order = 7)]
        public decimal precioUnitario { set; get; }//tamaño 18,3 DGT
        [XmlElement(ElementName = "MontoTotal", Order = 8)]
        public decimal montoTotal { set; get; }//tamaño 18,3 DGT
        [XmlElement(ElementName = "MontoDescuento", Order = 9)]
        public decimal montoDescuento { set; get; }//tamaño 18,3 DGT
        [XmlElement(ElementName = "NaturalezaDescuento", Order = 10)]
        public string naturalezaDescuento { set; get; }//tamaño 80 DGT
        [XmlElement(ElementName = "SubTotal", Order = 11)]
        public decimal subTotal { set; get; }//tamaño 80 DGT
       
        public LineaDetalle() {
            this.codigo = new Codigo();
        }
    }
}
