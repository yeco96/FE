using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class DocumentoElectronico
    {
        [XmlIgnore]
        public string tipoDocumento { get { return numeroConsecutivo.Substring(8, 2); } }    

        [XmlElement(ElementName = "Clave", Order = 1)]
        public string clave { set; get; }
        [XmlElement(ElementName = "NumeroConsecutivo", Order = 2)]
        public string numeroConsecutivo { set; get; }

        [XmlElement(ElementName = "FechaEmision", Order = 3)]
        public String fechaEmision { set; get; }

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


        public virtual void verificaDatosParaXML()
        {

        }

    }
}
