using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    //[XmlRoot(ElementName = "NotaCreditoElectronica", Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4.2/notaCreditoElectronica")]
    public class NotaCreditoElectronica
    {
        [XmlElement(ElementName = "Clave", Order = 1)]
        public string clave { set; get; }
        [XmlElement(ElementName = "NumeroConsecutivo", Order = 2)]
        public string numeroConsecutivo { set; get; }

        [XmlElement(ElementName = "FechaEmision", Order = 3)]
        public DateTime fechaEmision { set; get; }

        [XmlElement(ElementName = "Emisor", Order = 4)]
        public Emisor emisor { set; get; }

        [XmlElement(ElementName = "Receptor", Order = 5)]
        public Receptor receptor { set; get; }

        [XmlElement(ElementName = "CondicionVenta", Order = 6)]
        public string condicionVenta { set; get; }

        [XmlElement(ElementName = "PlazoCredito", Order = 7)]
        public string plazoCredito { set; get; }

        [XmlElement(ElementName = "MedioPago", Order = 8)]
        public string medioPago { set; get; }

        [XmlElement(ElementName = "DetalleServicio", Order = 9)]
        public DetalleServicio detalleServicio { set; get; }



        [XmlElement(ElementName = "ResumenFactura", Order = 10)]
        public ResumenFactura resumenFactura { set; get; }

        [XmlElement(ElementName = "InformacionReferencia", Order = 11)]
        public InformacionReferencia informacionReferencia { set; get; }


        [XmlElement(ElementName = "Normativa", Order = 12)]
        public Normativa normativa { set; get; }

        [XmlElement(ElementName = "Otros", Order = 13)]
        public Otros otros { set; get; }

        public NotaCreditoElectronica()
        {
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            this.detalleServicio = new DetalleServicio();
            this.resumenFactura = new ResumenFactura();
            this.normativa = new Normativa();
            this.otros = new Otros();
        }

       
    }
}
