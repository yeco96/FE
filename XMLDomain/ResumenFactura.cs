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
        public decimal totalServGravados { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalServExentos", Order = 4)]
        public decimal totalServExentos { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalMercanciasGravadas", Order = 5)]
        public decimal totalMercanciasGravadas { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalMercanciasExentas", Order = 6)]
        public decimal totalMercanciasExentas { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalGravado", Order = 7)]
        public decimal totalGravado { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalExento", Order = 8)]
        public decimal totalExento { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalVenta", Order = 9)]
        public decimal totalVenta { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalDescuentos", Order = 10)]
        public decimal totalDescuentos { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalVentaNeta", Order = 11)]
        public decimal totalVentaNeta { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalImpuesto", Order = 12)]
        public decimal totalImpuesto { set; get; }//tamaño 18,5  DGT
        [XmlElement(ElementName = "TotalComprobante", Order = 13)]
        public decimal totalComprobante { set; get; }//tamaño 18,5  DGT

        
        public ResumenFactura() { }
    }
}
