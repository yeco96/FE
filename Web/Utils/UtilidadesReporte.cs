using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
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
            using (var conexion = new DataModelFE())
            {
                WSRecepcionPOST dato = conexion.WSRecepcionPOST.Where(x => x.clave == clave).FirstOrDefault();
                string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);

                DocumentoElectronico documento = (DocumentoElectronico)EncodeXML.EncondeXML.getObjetcFromXML(xml);
                Empresa empresa = conexion.Empresa.Find(documento.emisor.identificacion.numero);

                if (empresa != null && "EN".Equals(empresa.idioma))
                {
                    using (RptComprobanteEN report = new RptComprobanteEN())
                    {
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(documento, dato.mensaje, empresa);
                        report.objectDataSource1.DataSource = dataSource;
                        string enviroment_url = ConfigurationManager.AppSettings["ENVIROMENT_URL"].ToString();
                        report.xrBarCode1.Text = (enviroment_url + documento.clave).ToUpper();
                        if (empresa != null && empresa.logo != null)
                        {
                            report.pbLogo.Image = byteArrayToImage(empresa.logo);
                        }
                        report.CreateDocument();
                        report.ExportToPdf(reportStream);
                    }
                }
                else
                {
                    using (RptComprobante report = new RptComprobante())
                    {
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(documento, dato.mensaje, empresa);
                        report.objectDataSource1.DataSource = dataSource;
                        string enviroment_url = ConfigurationManager.AppSettings["ENVIROMENT_URL"].ToString();
                        report.xrBarCode1.Text = (enviroment_url + documento.clave).ToUpper();
                        if (empresa != null && empresa.logo != null)
                        {
                            report.pbLogo.Image = byteArrayToImage(empresa.logo);
                        }
                        report.CreateDocument();
                        report.ExportToPdf(reportStream);
                    }
                }
            }
            return reportStream;
        }

        public static Image byteArrayToImage(byte[] imgBytes)
        {
            using (MemoryStream imgStream = new MemoryStream(imgBytes))
            {
                return Image.FromStream(imgStream);
            }
        }


        public static Impresion cargarObjetoImpresion(DocumentoElectronico dato, string mensaje, Empresa empresa)
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


            if (empresa != null)
            {
                mensaje += String.Format(" {0}", empresa.leyenda);
            }
            mensaje = mensaje.Replace("Este comprobante fue procesado en el ambiente de pruebas, por lo cual no tiene validez para fines tributarios.", "");
            impresion.leyenda = mensaje;

            using (var conexion = new DataModelFE())
            {
                impresion.CondicionVenta = conexion.CondicionVenta.Find(dato.condicionVenta).descripcion;
                impresion.MedioPago = conexion.MedioPago.Find(dato.medioPago).descripcion;

                if (empresa != null && "EN".Equals(empresa.idioma))
                {
                    mensaje = empresa.leyenda;
                    impresion.tipoDocumento = conexion.TipoDocumento.Find(dato.tipoDocumento).descripcionEN;
                }
                else
                {
                    impresion.tipoDocumento = conexion.TipoDocumento.Find(dato.tipoDocumento).descripcion;
                }
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


            return impresion;
        }

    }
}
