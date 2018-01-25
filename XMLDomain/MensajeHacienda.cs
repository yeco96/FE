using EncodeXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class MensajeHacienda
    {
        [XmlElement(ElementName = "Clave", Order = 1)]
         public string clave { set; get; }

         
        [XmlElement(ElementName = "NombreEmisor", Order = 2)]
        public string emisorNombre { set; get; }

        [XmlElement(ElementName = "TipoIdentificacionEmisor", Order = 3)]
        public string emisorTipoIdentificacion { set; get; }

        [XmlElement(ElementName = "NumeroCedulaEmisor", Order = 4)]
        public string emisorNumeroCedula { set; get; }


        [XmlElement(ElementName = "NombreReceptor", Order = 5)]
        public string receptorNombre { set; get; }

        [XmlElement(ElementName = "TipoIdentificacionReceptor", Order = 6)]
        public string receptorTipoIdentificacion { set; get; }

        [XmlElement(ElementName = "NumeroCedulaReceptor", Order = 7)]
        public string receptorNumeroCedula { set; get; }

        
        [XmlElement(ElementName = "Mensaje", Order = 8)]
        public int mensaje { set; get; }
        
        [XmlElement(ElementName = "DetalleMensaje", Order = 9)]
        public string mensajeDetalle{ set; get; }
        
        [XmlElement(ElementName = "MontoTotalImpuesto", Order = 10)]
        public double montoTotalImpuesto { set; get; }

        [XmlElement(ElementName = "TotalFactura", Order = 11)]
        public double montoTotalFactura { set; get; }
         
        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public MensajeHacienda()
        {

        }

        /// <summary>
        /// CONSTRUCTOR SOBRECARGADO
        /// </summary>
        /// <param name="xml">Genera un documento MensajeHacienda con los datos del XML</param>
        public MensajeHacienda(string xml)
        {
            this.clave = EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "Clave", xml);

            this.emisorNombre = EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "NombreEmisor", xml);
            this.emisorTipoIdentificacion = EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "TipoIdentificacionEmisor", xml);
            this.emisorNumeroCedula = EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "NumeroCedulaEmisor", xml);

            this.receptorNombre = EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "NombreReceptor", xml);
            this.receptorTipoIdentificacion = EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "TipoIdentificacionReceptor", xml);
            this.receptorNumeroCedula = EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "NumeroCedulaReceptor", xml);

            this.mensaje = int.Parse(EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "Mensaje", xml));
            this.mensajeDetalle = EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "DetalleMensaje", xml);
            this.montoTotalFactura = double.Parse(EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "TotalFactura", xml));
            this.montoTotalImpuesto = double.Parse(EncondeXML.buscarValorEtiquetaXML("MensajeHacienda", "MontoTotalImpuesto", xml));
        }


    }
}
