using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace XMLDomain
{
    
    //[XmlRoot(ElementName = "FacturaElectronica" , Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica") ]  
    public class FacturaElectronica : DocumentoElectronico
    {  
        
        public FacturaElectronica()
        {
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            this.detalleServicio = new DetalleServicio();
            this.resumenFactura = new ResumenFactura();
            this.normativa = new Normativa();
            this.informacionReferencia = new List<InformacionReferencia>();
            this.otros = new Otros();
        }

     
        public void iniciarParametros()
        {
            this.clave = "50608011800060354097400100001010000000038188888888";
            this.numeroConsecutivo = "00100001010000000038";
            this.fechaEmision = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") +"-06:00"; 

            this.emisor = new Emisor();
            this.emisor.nombre = "MAIKOL JESUS SALAMANCA ARIAS";
            this.emisor.identificacion.tipo = "01";
            this.emisor.identificacion.numero = "603540974";
            this.emisor.nombreComercial = "MSA SOFT";
            this.emisor.ubicacion.provincia = "4";
            this.emisor.ubicacion.canton = "01";
            this.emisor.ubicacion.distrito = "01";
            this.emisor.ubicacion.barrio = "01";
            this.emisor.ubicacion.otrassenas = "125 norte dela biblioteca publica";
            this.emisor.telefono.codigoPais = "506";
            this.emisor.telefono.numTelefono = "88729065";
            this.emisor.fax.codigoPais = "506";
            this.emisor.fax.numTelefono = "24402090";
            this.emisor.correoElectronico = "jupmasalamanca@gmail.com";
            this.receptor = new Receptor();
            this.receptor.nombre = "Andrea Santamaria Quesada";
            this.receptor.identificacion.tipo = "01";
            this.receptor.identificacion.numero = "207550498";
            this.receptor.nombreComercial = "MSA SOFT";
            this.receptor.ubicacion.provincia ="4";
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
            this.plazoCredito = "0";
            this.medioPago = "01";


             
            this.detalleServicio = new DetalleServicio();

            LineaDetalle lineaDetalle = new LineaDetalle();
            lineaDetalle.numeroLinea = 1;
            lineaDetalle.codigo.tipo = "04";
            lineaDetalle.codigo.codigo = "01";
            lineaDetalle.cantidad = 1;
            lineaDetalle.unidadMedida = "1";
            lineaDetalle.detalle = "COMBO INDIRECTO";
            lineaDetalle.precioUnitario = 1900000000;
            //lineaDetalle.montoTotal = 1900000000;
            lineaDetalle.montoDescuento = 380000000;
            lineaDetalle.naturalezaDescuento = "PRMOCION";
            //lineaDetalle.subTotal = lineaDetalle.precioUnitario * lineaDetalle.cantidad;
            //lineaDetalle.montoTotalLinea = 152000000;
            lineaDetalle.calcularMontos();

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
        

    }
}
