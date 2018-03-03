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
        public static string TIPO_IDENTIFICACION_EXTRANGERO = "99";

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
        public List<InformacionReferencia> informacionReferencia { set; get; }


        [XmlElement(ElementName = "Normativa", Order = 12)]
        public Normativa normativa { set; get; }

        [XmlElement(ElementName = "Otros", Order = 13)]
        public Otros otros { set; get; }


        public virtual void verificaDatosParaXML()
        {
            /* EMISOR */
            if (this.emisor.telefono != null)
            {
                if (string.IsNullOrWhiteSpace(this.emisor.telefono.codigoPais) || string.IsNullOrWhiteSpace(this.emisor.telefono.numTelefono))
                {
                    this.emisor.telefono = null;
                }
            }
            if (this.emisor.fax != null)
            {
                if (string.IsNullOrWhiteSpace(this.emisor.fax.codigoPais) || string.IsNullOrWhiteSpace(this.emisor.fax.numTelefono))
                {
                    this.emisor.fax = null;
                }
            }
            /* RECEPTOR */
            if (this.receptor.telefono != null)
            {
                if (string.IsNullOrWhiteSpace(this.receptor.telefono.codigoPais) || string.IsNullOrWhiteSpace(this.receptor.telefono.numTelefono))
                {
                    this.receptor.telefono = null;
                }
            }
            if (this.receptor.fax != null)
            {
                if (string.IsNullOrWhiteSpace(this.receptor.fax.codigoPais) || string.IsNullOrWhiteSpace(this.receptor.fax.numTelefono))
                {
                    this.receptor.fax = null;
                }
            }
            if (TIPO_IDENTIFICACION_EXTRANGERO.Equals(this.receptor.identificacion.tipo))
            {
                this.receptor.identificacionExtranjero = this.receptor.identificacion.numero;
                this.receptor.identificacion = null;
            }
            if (this.receptor.identificacion!=null) {
                if (String.IsNullOrWhiteSpace(this.receptor.identificacion.numero))
                {
                    this.receptor.identificacion = null;
                }
            }


            if (string.IsNullOrWhiteSpace(this.receptor.ubicacion.barrio) ||
                string.IsNullOrWhiteSpace(this.receptor.ubicacion.distrito) ||
                string.IsNullOrWhiteSpace(this.receptor.ubicacion.canton) ||
                string.IsNullOrWhiteSpace(this.receptor.ubicacion.provincia) ||
                 string.IsNullOrWhiteSpace(this.receptor.ubicacion.otrassenas)
                )
            {
                this.receptor.ubicacion = null;
            }

            /* INFORMACION DE REFERENCIA */
            if (this.informacionReferencia.Count == 0)
            {
                this.informacionReferencia = null;
            }

            /* LINEA DE DETALLES (IMPUESTOS) */
            int numeroLinea = 1;
            foreach (var item in this.detalleServicio.lineaDetalle)
            {
                item.verificaDatosParaXML();
                item.numeroLinea = numeroLinea;
                numeroLinea = numeroLinea + 1;
            }
             
            /*CLAVE RESUMEN*/
            resumenFactura.clave = clave;


            /*OTROS*/
            if (this.otros.otrosTextos!=null && this.otros.otrosTextos.Count == 0)
            {
                this.otros = null;
            }

        }

    }
}
