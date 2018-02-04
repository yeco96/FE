using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Web.Models;
using Web.Models.Catalogos;
using WSDomain;
using XMLDomain;

namespace Class.Utilidades
{
    public class UtilidadesReporte
    {

        public static MemoryStream generarPDF(string clave)
        {
            var reportStream = new MemoryStream();
            using (var conexion = new DataModelWS())
            { 
                WSRecepcionPOST dato = conexion.WSRecepcionPOST.Where(x => x.clave == clave).FirstOrDefault();
                string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);
                 
                using (RptFacturacionElectronica report = new RptFacturacionElectronica())
                {

                    if (TipoDocumento.FACTURA_ELECTRONICA.Equals(dato.tipoDocumento))
                    {
                        FacturaElectronica factura = (FacturaElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(FacturaElectronica));
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(factura);
                        report.objectDataSource1.DataSource = dataSource;
                        report.xrBarCode1.Text = factura.clave;
                        report.CreateDocument();
                        report.ExportToPdf(reportStream);
                    }

                    if (TipoDocumento.NOTA_CREDITO.Equals(dato.tipoDocumento))
                    {
                        NotaCreditoElectronica notaCredito = (NotaCreditoElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(NotaCreditoElectronica));
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(notaCredito);
                        report.objectDataSource1.DataSource = dataSource;
                        report.xrBarCode1.Text = notaCredito.clave;
                        report.CreateDocument();
                        report.ExportToPdf(reportStream);
                    }

                    if (TipoDocumento.NOTA_DEBITO.Equals(dato.tipoDocumento))
                    {
                        NotaDebitoElectronica notaDebito = (NotaDebitoElectronica)EncodeXML.EncondeXML.getObjetcFromXML(xml, typeof(NotaDebitoElectronica));
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(notaDebito);
                        report.objectDataSource1.DataSource = dataSource;
                        report.xrBarCode1.Text = notaDebito.clave;
                        report.CreateDocument();
                        report.ExportToPdf(reportStream);
                    }



                }
            }
            return reportStream;
        }
       


        public static Impresion cargarObjetoImpresion(FacturaElectronica dato)
        {
            Impresion impresion = new Impresion();

            impresion.emisorNombre = dato.emisor.nombre;
            impresion.emisorIdentificacion = dato.emisor.identificacion.numero;
            impresion.emisorNombreComercial = dato.emisor.nombreComercial;
            impresion.emisorIdentificacionCorreo = dato.emisor.correoElectronico;
            impresion.emisorTelefonos = dato.emisor.telefono.numTelefono;
            impresion.emisorDireccion = dato.emisor.ubicacion.otrassenas;

            impresion.receptorNombre = dato.receptor.nombre;
            impresion.receptorIdentificacion = dato.receptor.identificacion.numero;
            impresion.receptorIdentificacionCorreo = dato.receptor.correoElectronico;

            impresion.clave = dato.clave;
            impresion.consecutivo = dato.numeroConsecutivo;
            impresion.fecha = Convert.ToDateTime(dato.fechaEmision);
            impresion.moneda = dato.resumenFactura.codigoMoneda;
            impresion.tipoCambio = dato.resumenFactura.tipoCambio.ToString();
            
            impresion.leyenda = "Acá va una leyenda";
            
            using (var conexion = new DataModelFE())
            {
                impresion.tipoDocumento = conexion.TipoDocumento.Find(TipoDocumento.FACTURA_ELECTRONICA).descripcion;
                impresion.CondicionVenta = conexion.CondicionVenta.Find(dato.condicionVenta).descripcion;
                impresion.MedioPago = conexion.MedioPago.Find(dato.medioPago).descripcion;
            }

            impresion.detalles = new List<ImpresionDetalle>();

            foreach (var item in dato.detalleServicio.lineaDetalle)
            {
                ImpresionDetalle detalle = new ImpresionDetalle();
                detalle.cantidad = int.Parse(item.cantidad.ToString());
                detalle.codigo = item.codigo.codigo;
                detalle.descripcion = item.detalle;
                detalle.monto = item.precioUnitario;

                impresion.detalles.Add(detalle);
            }


            impresion.montoSubTotal = dato.resumenFactura.totalVenta;
            impresion.montoDescuento = dato.resumenFactura.totalDescuentos;
            impresion.montoImpuestoVenta = dato.resumenFactura.totalImpuesto;
            impresion.montoTotal = dato.resumenFactura.totalComprobante;

            impresion.Normativa = "Autorizada mediante resolución No DGT-R-48-2016 del 7 de Octubre de 2016";

            return impresion;
        }



        public static Impresion cargarObjetoImpresion(NotaCreditoElectronica dato)
        {
            Impresion impresion = new Impresion();

            impresion.emisorNombre = dato.emisor.nombre;
            impresion.emisorIdentificacion = dato.emisor.identificacion.numero;
            impresion.emisorNombreComercial = dato.emisor.nombreComercial;
            impresion.emisorIdentificacionCorreo = dato.emisor.correoElectronico;
            impresion.emisorTelefonos = dato.emisor.telefono.numTelefono;
            impresion.emisorDireccion = dato.emisor.ubicacion.otrassenas;

            impresion.receptorNombre = dato.receptor.nombre;
            impresion.receptorIdentificacion = dato.receptor.identificacion.numero;
            impresion.receptorIdentificacionCorreo = dato.receptor.correoElectronico;

            impresion.clave = dato.clave;
            impresion.consecutivo = dato.numeroConsecutivo;
            impresion.fecha = Convert.ToDateTime(dato.fechaEmision);
            impresion.moneda = dato.resumenFactura.codigoMoneda;
            impresion.tipoCambio = dato.resumenFactura.tipoCambio.ToString();
            
            impresion.leyenda = "Acá va una leyenda";


            using (var conexion = new DataModelFE())
            {
                impresion.tipoDocumento = conexion.TipoDocumento.Find(TipoDocumento.NOTA_CREDITO).descripcion;
                impresion.CondicionVenta = conexion.CondicionVenta.Find(dato.condicionVenta).descripcion;
                impresion.MedioPago = conexion.MedioPago.Find(dato.medioPago).descripcion;
            }

            impresion.detalles = new List<ImpresionDetalle>();

            foreach (var item in impresion.detalles)
            {
                ImpresionDetalle detalle = new ImpresionDetalle();
                detalle.cantidad = item.cantidad;
                detalle.codigo = item.codigo;
                detalle.descripcion = item.descripcion;
                detalle.monto = item.monto;

                impresion.detalles.Add(detalle);
            }


            impresion.montoSubTotal = dato.resumenFactura.totalVenta;
            impresion.montoDescuento = dato.resumenFactura.totalDescuentos;
            impresion.montoImpuestoVenta = dato.resumenFactura.totalImpuesto;
            impresion.montoTotal = dato.resumenFactura.totalComprobante;

            impresion.Normativa = "Autorizada mediante resolución No DGT-R-48-2016 del 7 de Octubre de 2016";

            return impresion;
        }



        public static Impresion cargarObjetoImpresion(NotaDebitoElectronica dato)
        {
            Impresion impresion = new Impresion();

            impresion.emisorNombre = dato.emisor.nombre;
            impresion.emisorIdentificacion = dato.emisor.identificacion.numero;
            impresion.emisorNombreComercial = dato.emisor.nombreComercial;
            impresion.emisorIdentificacionCorreo = dato.emisor.correoElectronico;
            impresion.emisorTelefonos = dato.emisor.telefono.numTelefono;
            impresion.emisorDireccion = dato.emisor.ubicacion.otrassenas;

            impresion.receptorNombre = dato.receptor.nombre;
            impresion.receptorIdentificacion = dato.receptor.identificacion.numero;
            impresion.receptorIdentificacionCorreo = dato.receptor.correoElectronico;

            impresion.clave = dato.clave;
            impresion.consecutivo = dato.numeroConsecutivo;
            impresion.fecha = Convert.ToDateTime(dato.fechaEmision);
            impresion.moneda = dato.resumenFactura.codigoMoneda;
            impresion.tipoCambio = dato.resumenFactura.tipoCambio.ToString();
            
            impresion.leyenda = "Acá va una leyenda";


            using (var conexion = new DataModelFE())
            {
                impresion.tipoDocumento = conexion.TipoDocumento.Find(TipoDocumento.NOTA_DEBITO).descripcion;
                impresion.CondicionVenta = conexion.CondicionVenta.Find(dato.condicionVenta).descripcion;
                impresion.MedioPago = conexion.MedioPago.Find(dato.medioPago).descripcion;
            }

            impresion.detalles = new List<ImpresionDetalle>();

            foreach (var item in impresion.detalles)
            {
                ImpresionDetalle detalle = new ImpresionDetalle();
                detalle.cantidad = item.cantidad;
                detalle.codigo = item.codigo;
                detalle.descripcion = item.descripcion;
                detalle.monto = item.monto;

                impresion.detalles.Add(detalle);
            }


            impresion.montoSubTotal = dato.resumenFactura.totalVenta;
            impresion.montoDescuento = dato.resumenFactura.totalDescuentos;
            impresion.montoImpuestoVenta = dato.resumenFactura.totalImpuesto;
            impresion.montoTotal = dato.resumenFactura.totalComprobante;

            impresion.Normativa = "Autorizada mediante resolución No DGT-R-48-2016 del 7 de Octubre de 2016";

            return impresion;
        }




    }
}
