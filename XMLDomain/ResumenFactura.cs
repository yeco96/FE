using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
   public class ResumenFactura
    {
        [XmlElement(ElementName = "CodigoMoneda", Order = 1)]
        public string codigoMoneda { set; get; }//tamaño 3  DGT
        [XmlElement(ElementName = "TipoCambio", Order = 2)]
        public string tipoCambio { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalServGravados", Order = 3)]
        public double totalServGravados { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalServExentos", Order = 4)]
        public double totalServExentos { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalMercanciasGravadas", Order = 5)]
        public double totalMercanciasGravadas { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalMercanciasExentas", Order = 6)]
        public double totalMercanciasExentas { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalGravado", Order = 7)]
        public double totalGravado { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalExento", Order = 8)]
        public double totalExento { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalVenta", Order = 9)]
        public double totalVenta { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalDescuentos", Order = 10)]
        public double totalDescuentos { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalVentaNeta", Order = 11)]
        public double totalVentaNeta { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalImpuesto", Order = 12)]
        public double totalImpuesto { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalComprobante", Order = 13)]
        public Double totalComprobante { set; get; }//tamaño 18,5  DGT

        
        public ResumenFactura() { }
    }
}
