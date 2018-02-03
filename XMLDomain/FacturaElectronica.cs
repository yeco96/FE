using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace XMLDomain
{
    
    //[XmlRoot(ElementName = "FacturaElectronica" , Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica") ]  
    public class FacturaElectronica
    {  
        [XmlIgnore]
        public static string TIPO = "01";
        
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


        [XmlElement(ElementName = "Normativa",Order = 12)]
        public Normativa normativa { set; get; }

        [XmlElement(ElementName = "Otros", Order = 13)]
        public Otros otros { set; get; }
      
        public FacturaElectronica()
        {
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            this.detalleServicio = new DetalleServicio();
            this.resumenFactura = new ResumenFactura();
            this.normativa = new Normativa(); 

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
                resumenFactura.tipoCambio.ToString().Contains(",") )
            {
                resumenFactura.tipoCambio = resumenFactura.tipoCambio / 100;
            }

        }

    }
}
