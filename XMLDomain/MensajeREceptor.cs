
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class MensajeReceptor
    {
        // <summary>
        /// Clave numérica del comprobante
        /// </summary>
        [XmlElement("Clave", Order = 1)]
        public string clave { set; get; }

        /// <summary>
        /// Número de cédula fisica/jurídica/NITE/DIMEX del vendedor
        /// </summary>
        [XmlElement("NumeroCedulaEmisor", Order = 2)]
        public string numeroCedulaEmisor { set; get; }

        /// <summary>
        /// Fecha de emision de la confirmación
        /// </summary>
        [XmlElement("FechaEmisionDoc", Order = 3)]
        public string fechaEmisionDoc { set; get; }

        /// <summary>
        /// Codigo del mensaje de respuesta. 1 aceptado, 2 aceptado parcialmente, 3 rechazado
        /// </summary>
        [XmlElement("Mensaje", Order = 4)]
        public int mensaje { set; get; }

        /// <summary>
        /// Detalle del mensaje
        /// </summary>
        [XmlElement("DetalleMensaje", Order = 5)]
        public string mensajeDetalle { set; get; }

        /// <summary>
        /// Monto total del impuesto, que es obligatorio si el  comprobante tenga impuesto.
        /// </summary>
        [XmlElement("MontoTotalImpuesto", Order = 6)]
        public decimal montoTotalImpuesto { set; get; }

        /// <summary>
        /// Monto total de la factura
        /// </summary>
        [XmlElement("TotalFactura", Order = 7)]
        public decimal montoTotalFactura { set; get; }

        /// <summary>
        /// Número de cédula fisica/jurídica/NITE/DIMEX del comprador
        /// </summary>
        [XmlElement("NumeroCedulaReceptor", Order = 8)]
        public string numeroCedulaReceptor { set; get; }


        /// <summary>
        /// umeración consecutiva de los mensajes de confirmación
        /// </summary>
        [XmlElement("NumeroConsecutivoReceptor", Order = 9)]
        public string numeroConsecutivoReceptor { set; get; }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public MensajeReceptor()
        {
            this.fechaEmisionDoc = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss-06:00");
        }

          
        /// <summary>
        /// CONSTRUCTOR SOBRECARGADO
        /// </summary>
        /// <param name="xml">Genera un documento MensajeHacienda con los datos del XML</param>
        public MensajeReceptor(string xml)
        {
            this.clave = buscarValorEtiquetaXML("MensajeReceptor", "Clave", xml);

            this.numeroCedulaEmisor = buscarValorEtiquetaXML("MensajeReceptor", "NumeroCedulaEmisor", xml);
            this.fechaEmisionDoc = buscarValorEtiquetaXML("MensajeReceptor", "FechaEmisionDoc", xml);
            this.numeroConsecutivoReceptor = buscarValorEtiquetaXML("MensajeReceptor", "NumeroConsecutivoReceptor", xml);

            this.numeroCedulaEmisor = buscarValorEtiquetaXML("MensajeReceptor", "NumeroCedulaEmisor", xml);
            this.numeroCedulaReceptor = buscarValorEtiquetaXML("MensajeReceptor", "NumeroCedulaReceptor", xml);
          
            this.mensaje = int.Parse(buscarValorEtiquetaXML("MensajeReceptor", "Mensaje", xml));
            this.mensajeDetalle = buscarValorEtiquetaXML("MensajeReceptor", "DetalleMensaje", xml);

            string totalFactura = buscarValorEtiquetaXML("MensajeReceptor", "TotalFactura", xml);
            if (!String.IsNullOrWhiteSpace(totalFactura))
            {
                this.montoTotalFactura = decimal.Parse(totalFactura); 
            }
            
            string totalImpueto = buscarValorEtiquetaXML("MensajeHacienda", "MontoTotalImpuesto", xml);
            if (!String.IsNullOrWhiteSpace(totalFactura))
            {
                this.montoTotalImpuesto = decimal.Parse(totalImpueto);
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
