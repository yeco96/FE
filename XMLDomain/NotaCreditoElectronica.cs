using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    //[XmlRoot(ElementName = "NotaCreditoElectronica", Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4.2/notaCreditoElectronica")]
    public class NotaCreditoElectronica : DocumentoElectronico
    {
       

        public NotaCreditoElectronica()
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
        public override void verificaDatosParaXML()
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

            /* INFORMACION DE REFERENCIA */
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

            /*CLAVE RESUMEN*/
            resumenFactura.clave = clave;

        }


    }
}
