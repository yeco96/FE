using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    //[XmlRoot(ElementName = "TiqueteElectronico", Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4.2/tiqueteElectronico")]
    public class TiqueteElectronico : DocumentoElectronico
    {
         

        public TiqueteElectronico()
        {
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            this.detalleServicio = new DetalleServicio();
            this.resumenFactura = new ResumenFactura();
            this.informacionReferencia = new InformacionReferencia();
            this.normativa = new Normativa();
            this.otros = new Otros();
        }

        public void iniciarParametros()
        {
            this.clave = "5060801180006035409740010000104000000000188888888";
            this.numeroConsecutivo = "00100001040000000001";
            // 001            00001        01         0000000001 
            //sucursal(2)  terminal(5)  tipo(2)   consecutivo (10)  
            this.fechaEmision = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "-06:00";

            this.emisor = new Emisor();
            this.emisor.nombre = "MAIKOL JESUS SALAMANCA ARIAS";
            this.emisor.identificacion.tipo = "1";
            this.emisor.identificacion.numero = "603540974";
            this.emisor.nombreComercial = "MSA SOFT";
            this.emisor.ubicacion.provincia = "1";
            this.emisor.ubicacion.canton = "1";
            this.emisor.ubicacion.distrito = "1";
            this.emisor.ubicacion.barrio = "1";
            this.emisor.ubicacion.otrassenas = "125n de la biblioteca";
            this.emisor.telefono.codigoPais = "506";
            this.emisor.telefono.numTelefono = "88729065";
            this.emisor.fax.codigoPais = "506";
            this.emisor.fax.numTelefono = "24402090";
            this.emisor.correoElectronico = "jupmasalamanca@gmail.com";
            this.receptor = new Receptor();
            this.receptor.nombre = "Andrea Santamaria Quesada";
            this.receptor.identificacion.tipo = "1";
            this.receptor.identificacion.numero = "207550498";
            this.receptor.nombreComercial = "MSA SOFT";
            this.receptor.ubicacion.provincia = "4";
            this.receptor.ubicacion.canton = "01";
            this.receptor.ubicacion.distrito = "01";
            this.receptor.ubicacion.barrio = "01";
            this.receptor.ubicacion.otrassenas = "125 norte dela biblioteca publica";
            this.receptor.telefono.codigoPais = "506";
            this.receptor.telefono.numTelefono = "61818738";
            this.receptor.fax.codigoPais = "506";
            this.receptor.fax.numTelefono = "24402090";
            this.receptor.correoElectronico = "jandreasantamariaquesada@gmail.com";
            this.condicionVenta = "01";
            this.plazoCredito = "1";
            this.medioPago = "01";



            this.detalleServicio = new DetalleServicio();

            LineaDetalle lineaDetalle = new LineaDetalle();
            lineaDetalle.numeroLinea = 1;
            lineaDetalle.codigo.tipo = "04";
            lineaDetalle.codigo.codigo = "01";
            lineaDetalle.cantidad = 1;
            lineaDetalle.unidadMedida = "1";
            lineaDetalle.detalle = "COOMBS INDIRECTO";
            lineaDetalle.precioUnitario = 1900000000;
            lineaDetalle.montoTotal = 1900000000;
            lineaDetalle.montoDescuento = 380000000;
            lineaDetalle.naturalezaDescuento = "Descuento";
            lineaDetalle.subTotal = 152000000;

            this.detalleServicio.lineaDetalle.Add(lineaDetalle);


            this.resumenFactura = new ResumenFactura();
            this.resumenFactura.codigoMoneda = "CRC";
            this.resumenFactura.totalServGravados = 0;
            this.resumenFactura.totalServExentos = 1900000000;
            this.resumenFactura.totalMercanciasGravadas = 0;
            this.resumenFactura.totalMercanciasExentas = 0;
            this.resumenFactura.totalGravado = 0;
            this.resumenFactura.totalExento = 1900000000;
            this.resumenFactura.totalVenta = 1900000000;
            this.resumenFactura.totalDescuentos = 380000000;
            this.resumenFactura.totalVentaNeta = 1520000000;
            this.resumenFactura.totalImpuesto = 0;
            this.resumenFactura.totalComprobante = 1520000000;

            this.normativa = new Normativa();
            this.normativa.numeroResolucion = "DGT-R-48-2016";
            this.normativa.fechaResolucion = "07-10-2016 08:00:00";

            //  valores para la firma

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
