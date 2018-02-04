using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    //[XmlRoot(ElementName = "NotaDebitoElectronica", Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4.2/notaDebitoElectronica")]
    public class NotaDebitoElectronica
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

        public NotaDebitoElectronica()
        {
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            this.detalleServicio = new DetalleServicio();
            this.resumenFactura = new ResumenFactura();
            this.informacionReferencia = new InformacionReferencia();
            this.normativa = new Normativa();
            this.otros = new Otros();
        }





        /// <summary>
        /// Este método determina los valores que no tienen datos y los asigna NULL para que no se generen los notos
        /// </summary>
        public void verificaDatosParaXML()
        {
            /* RECEPTOR */
            if (string.IsNullOrWhiteSpace(this.emisor.fax.codigoPais) || string.IsNullOrWhiteSpace(this.emisor.fax.numTelefono))
            {
                this.emisor.fax = null;
            }
            if (string.IsNullOrWhiteSpace(this.receptor.fax.codigoPais) || string.IsNullOrWhiteSpace(this.receptor.fax.numTelefono))
            {
                this.emisor.fax = null;
            }
            /* EMISOR */
            if (string.IsNullOrWhiteSpace(this.receptor.telefono.codigoPais) || string.IsNullOrWhiteSpace(this.receptor.telefono.numTelefono))
            {
                this.receptor.fax = null;
            }
            if (string.IsNullOrWhiteSpace(this.receptor.fax.codigoPais) || string.IsNullOrWhiteSpace(this.receptor.fax.numTelefono))
            {
                this.receptor.fax = null;
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

            if (string.IsNullOrWhiteSpace(this.informacionReferencia.numero))
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

            /*TIPO CAMBIO*/
            if (resumenFactura.tipoCambio.ToString().Contains(".") ||
                resumenFactura.tipoCambio.ToString().Contains(","))
            {
                resumenFactura.tipoCambio = resumenFactura.tipoCambio / 100;
            }

        }
    }
}
