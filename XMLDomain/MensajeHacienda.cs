
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
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
        public decimal montoTotalImpuesto { set; get; }

        [XmlElement(ElementName = "TotalFactura", Order = 11)]
        public decimal montoTotalFactura { set; get; }
         
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
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalDigits = 2;
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ",";
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            this.clave = buscarValorEtiquetaXML("MensajeHacienda", "Clave", xml);

            this.emisorNombre = buscarValorEtiquetaXML("MensajeHacienda", "NombreEmisor", xml);
            this.emisorTipoIdentificacion = buscarValorEtiquetaXML("MensajeHacienda", "TipoIdentificacionEmisor", xml);
            this.emisorNumeroCedula = buscarValorEtiquetaXML("MensajeHacienda", "NumeroCedulaEmisor", xml);

            this.receptorNombre = buscarValorEtiquetaXML("MensajeHacienda", "NombreReceptor", xml);
            this.receptorTipoIdentificacion = buscarValorEtiquetaXML("MensajeHacienda", "TipoIdentificacionReceptor", xml);
            this.receptorNumeroCedula = buscarValorEtiquetaXML("MensajeHacienda", "NumeroCedulaReceptor", xml);

            this.mensaje = int.Parse(buscarValorEtiquetaXML("MensajeHacienda", "Mensaje", xml));
            this.mensajeDetalle = buscarValorEtiquetaXML("MensajeHacienda", "DetalleMensaje", xml);

            string totalFactura = buscarValorEtiquetaXML("MensajeHacienda", "TotalFactura", xml);
            if (!String.IsNullOrWhiteSpace(totalFactura))
            {
                this.montoTotalFactura = decimal.Parse(totalFactura);
            }
            
            string totalImpueto = buscarValorEtiquetaXML("MensajeHacienda", "MontoTotalImpuesto", xml);
            if (!String.IsNullOrWhiteSpace(totalFactura))
            {
                this.montoTotalImpuesto = decimal.Parse(totalImpueto,System.Globalization.NumberStyles.Number);
            }
        }
         

        /// <summary>
        /// Busca el valor de una etiqueta dentro de un XML
        /// </summary>
        /// <param name="tagPadre">nombre de la etiqueta padre Ej: emisor receptor MensajeHacienda FacturaElectronica NotaCreditoElectronica NotaDebitoElectronica</param>
        /// <param name="label">nombre de la etiqueta</param>
        /// <param name="xml">XML donde se busca la etiqueta</param>
        /// <returns></returns>
        public static string buscarValorEtiquetaXML(string tagPadre, string label, string xml)
        {
            try
            {
                string dato = "";
                XmlDocument xm = new XmlDocument();
                xm.LoadXml(xml);
                XmlNodeList xmlTag = xm.GetElementsByTagName(tagPadre);
                XmlNodeList lista = ((XmlElement)xmlTag[0]).GetElementsByTagName(label);
                foreach (XmlElement nodo in lista)
                {
                    XmlNodeList nNombre = nodo.GetElementsByTagName(label);
                    dato = nodo.InnerText;
                }
                return dato;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }


    }
}
