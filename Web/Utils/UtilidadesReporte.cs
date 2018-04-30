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

        public static MemoryStream generarPDFProforma(string clave)
        {
            var reportStream = new MemoryStream();
            using (var conexion = new DataModelFE())
            {
                WSRecepcionPOSTProforma dato = conexion.WSRecepcionPOSTProforma.Where(x => x.clave == clave).FirstOrDefault();
                string xml = EncodeXML.EncondeXML.base64Decode(dato.comprobanteXml);

                //Cambiar el formato a Proforma Electrónica
                xml = xml.Replace("FacturaElectronica", "ProformaElectronica");// esto es solo para que no se reemplace por el de abajo


                ProformaElectronico documento = (ProformaElectronico)EncodeXML.EncondeXML.getObjetcFromXML(xml);
                Empresa empresa = conexion.Empresa.Find(documento.emisor.identificacion.numero);

                if (empresa != null && "EN".Equals(empresa.idioma))
                {
                    using (RptComprobanteProformasEN report = new RptComprobanteProformasEN())
                    {
                        object dataSource = UtilidadesReporte.cargarObjetoImpresionProforma(documento, dato.mensaje, empresa);
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
                    using (RptComprobanteProformas report = new RptComprobanteProformas())
                    {
                        object dataSource = UtilidadesReporte.cargarObjetoImpresionProforma(documento, dato.mensaje, empresa);
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


        public static Impresion cargarObjetoImpresion(DocumentoElectronico dato, string mensajeHacienda, Empresa empresa)
        {
            Impresion impresion = new Impresion();

            impresion.emisorNombre = dato.emisor.nombre;
            impresion.emisorIdentificacion = dato.emisor.identificacion.numero;
            impresion.emisorNombreComercial = dato.emisor.nombreComercial;
            impresion.emisorIdentificacionCorreo = dato.emisor.correoElectronico;
            impresion.emisorTelefonos = dato.emisor.telefono.numTelefono;

            
            using (var conexion = new DataModelFE())
            {
                Web.Models.Catalogos.Ubicacion oDato = conexion.Ubicacion.Where(x => x.codProvincia == dato.emisor.ubicacion.provincia && x.codCanton == dato.emisor.ubicacion.canton && x.codDistrito == dato.emisor.ubicacion.distrito && x.codBarrio == dato.emisor.ubicacion.barrio).FirstOrDefault();
                if (oDato != null)
                {
                    impresion.emisorDireccion = ProperCase.ToTitleCase(oDato.nombreProvincia) + " , " + ProperCase.ToTitleCase(oDato.nombreCanton) + " , " + ProperCase.ToTitleCase(oDato.nombreDistrito) + " , " + ProperCase.ToTitleCase(oDato.nombreBarrio) + " , " + ProperCase.ToTitleCase(dato.emisor.ubicacion.otrassenas);
                }

                else {
                    impresion.emisorDireccion = ProperCase.ToTitleCase(dato.emisor.ubicacion.otrassenas);
                }
            } 
            //dato.emisor.ubicacion.provincia.ToUpper().ToString() + ", " + dato.emisor.ubicacion.canton.ToUpper().ToString() + ", " + dato.emisor.ubicacion.distrito.ToUpper().ToString() + ", " +
             
            impresion.receptorNombre = dato.receptor.nombre;
            impresion.receptorIdentificacion = dato.receptor.identificacion.numero;
            impresion.receptorIdentificacionCorreo = dato.receptor.correoElectronico;

            impresion.clave = dato.clave;
            impresion.consecutivo = dato.clave.Substring(21,20);
            impresion.fecha = Convert.ToDateTime(dato.fechaEmision);
            impresion.moneda = dato.resumenFactura.codigoMoneda;
            impresion.tipoCambio = dato.resumenFactura.tipoCambio.ToString();


            /*
            if (empresa != null)
            {
                mensaje += String.Format(" {0}", empresa.leyenda);
            }
            */
            foreach (var otros in dato.otros.otrosTextos)
            {
                impresion.leyenda += string.Format("{0}\n", otros);
            }
           
            using (var conexion = new DataModelFE())
            {
                if (empresa != null && "EN".Equals(empresa.idioma))
                {
                    if (string.IsNullOrWhiteSpace(impresion.leyenda))
                    {
                        impresion.leyenda = empresa.leyenda;
                    }

                    impresion.tipoDocumento = conexion.TipoDocumento.Find(dato.tipoDocumento).descripcionEN;
                    impresion.CondicionVenta = conexion.CondicionVenta.Find(dato.condicionVenta).descripcionEN;
                    impresion.MedioPago = conexion.MedioPago.Find(dato.medioPago).descripcionEN;

                    if (impresion.CondicionVenta.Equals(CondicionVenta.CREDIT))
                    {
                        impresion.CondicionVenta += string.Format(" / {0} DAYS", dato.plazoCredito);
                    }
                }
                else
                {
                    impresion.tipoDocumento = conexion.TipoDocumento.Find(dato.tipoDocumento).descripcion;
                    impresion.CondicionVenta = conexion.CondicionVenta.Find(dato.condicionVenta).descripcion;
                    impresion.MedioPago = conexion.MedioPago.Find(dato.medioPago).descripcion;
                   
                    if (impresion.CondicionVenta.Equals(CondicionVenta.CREDITO))
                    {
                        impresion.CondicionVenta += string.Format(" / {0} DÍAS", dato.plazoCredito);
                    }
                }
            }


            impresion.detalles = new List<ImpresionDetalle>();

            foreach (var item in dato.detalleServicio.lineaDetalle)
            {
                ImpresionDetalle detalle = new ImpresionDetalle();
                detalle.cantidad = int.Parse(item.cantidad.ToString());
                detalle.codigo = item.codigo.codigo;
                if (empresa != null && "EN".Equals(empresa.idioma)) { 
                    detalle.descripcion = string.Format("{0} - {1}",item.detalle, impresion.fecha.ToString("yyyy-MM"));
                }else
                {
                    if(item.cantidad == 1)
                        detalle.descripcion =  item.detalle;
                    else
                        detalle.descripcion = string.Format("{0} p/u={1}" ,item.detalle, item.precioUnitario) ;
                }
                detalle.monto = item.montoTotal;

                impresion.detalles.Add(detalle);
            }

            impresion.montoSubTotal = dato.resumenFactura.totalVenta;
            impresion.montoDescuento = dato.resumenFactura.totalDescuentos;
            impresion.montoImpuestoVenta = dato.resumenFactura.totalImpuesto;
            impresion.montoTotal = dato.resumenFactura.totalComprobante;

            impresion.fechaImpresion = Date.DateTimeNow();
            return impresion;
        }

        public static Impresion cargarObjetoImpresionProforma(ProformaElectronico dato, string mensajeHacienda, Empresa empresa)
        {
            Impresion impresion = new Impresion();

            impresion.emisorNombre = dato.emisor.nombre;
            impresion.emisorIdentificacion = dato.emisor.identificacion.numero;
            impresion.emisorNombreComercial = dato.emisor.nombreComercial;
            impresion.emisorIdentificacionCorreo = dato.emisor.correoElectronico;
            impresion.emisorTelefonos = dato.emisor.telefono.numTelefono;


            using (var conexion = new DataModelFE())
            {
                Web.Models.Catalogos.Ubicacion oDato = conexion.Ubicacion.Where(x => x.codProvincia == dato.emisor.ubicacion.provincia && x.codCanton == dato.emisor.ubicacion.canton && x.codDistrito == dato.emisor.ubicacion.distrito && x.codBarrio == dato.emisor.ubicacion.barrio).FirstOrDefault();
                if (oDato != null)
                {
                    impresion.emisorDireccion = ProperCase.ToTitleCase(oDato.nombreProvincia) + " , " + ProperCase.ToTitleCase(oDato.nombreCanton) + " , " + ProperCase.ToTitleCase(oDato.nombreDistrito) + " , " + ProperCase.ToTitleCase(oDato.nombreBarrio) + " , " + ProperCase.ToTitleCase(dato.emisor.ubicacion.otrassenas);
                }

                else
                {
                    impresion.emisorDireccion = ProperCase.ToTitleCase(dato.emisor.ubicacion.otrassenas);
                }
            }
            //dato.emisor.ubicacion.provincia.ToUpper().ToString() + ", " + dato.emisor.ubicacion.canton.ToUpper().ToString() + ", " + dato.emisor.ubicacion.distrito.ToUpper().ToString() + ", " +

            impresion.receptorNombre = dato.receptor.nombre;
            impresion.receptorIdentificacion = dato.receptor.identificacion.numero;
            impresion.receptorIdentificacionCorreo = dato.receptor.correoElectronico;

            impresion.clave = dato.clave;
            impresion.consecutivo = dato.clave.Substring(21, 20);
            impresion.fecha = Convert.ToDateTime(dato.fechaEmision);
            impresion.moneda = dato.resumenProforma.codigoMoneda;
            impresion.tipoCambio = dato.resumenProforma.tipoCambio.ToString();


            /*
            if (empresa != null)
            {
                mensaje += String.Format(" {0}", empresa.leyenda);
            }
            */
            foreach (var otros in dato.otros.otrosTextos)
            {
                impresion.leyenda += string.Format("{0}\n", otros);
            }

            using (var conexion = new DataModelFE())
            {
                if (empresa != null && "EN".Equals(empresa.idioma))
                {
                    if (string.IsNullOrWhiteSpace(impresion.leyenda))
                    {
                        impresion.leyenda = empresa.leyenda;
                    }

                    impresion.tipoDocumento = conexion.TipoDocumento.Find(dato.tipoDocumento).descripcionEN;
                    impresion.CondicionVenta = conexion.CondicionVenta.Find(dato.condicionVenta).descripcionEN;
                    impresion.MedioPago = conexion.MedioPago.Find(dato.medioPago).descripcionEN;

                    if (impresion.CondicionVenta.Equals(CondicionVenta.CREDIT))
                    {
                        impresion.CondicionVenta += string.Format(" / {0} DAYS", dato.plazoCredito);
                    }
                }
                else
                {
                    impresion.tipoDocumento = conexion.TipoDocumento.Find(dato.tipoDocumento).descripcion;
                    impresion.CondicionVenta = conexion.CondicionVenta.Find(dato.condicionVenta).descripcion;
                    impresion.MedioPago = conexion.MedioPago.Find(dato.medioPago).descripcion;

                    if (impresion.CondicionVenta.Equals(CondicionVenta.CREDITO))
                    {
                        impresion.CondicionVenta += string.Format(" / {0} DÍAS", dato.plazoCredito);
                    }
                }
            }


            impresion.detalles = new List<ImpresionDetalle>();

            foreach (var item in dato.detalleServicio.lineaDetalle)
            {
                ImpresionDetalle detalle = new ImpresionDetalle();
                detalle.cantidad = int.Parse(item.cantidad.ToString());
                detalle.codigo = item.codigo.codigo;
                if (empresa != null && "EN".Equals(empresa.idioma))
                {
                    detalle.descripcion = string.Format("{0} - {1}", item.detalle, impresion.fecha.ToString("yyyy-MM"));
                }
                else
                {
                    if (item.cantidad == 1)
                        detalle.descripcion = item.detalle;
                    else
                        detalle.descripcion = string.Format("{0} p/u={1}", item.detalle, item.precioUnitario);
                }
                detalle.monto = item.montoTotal;

                impresion.detalles.Add(detalle);
            }

            impresion.montoSubTotal = dato.resumenProforma.totalVenta;
            impresion.montoDescuento = dato.resumenProforma.totalDescuentos;
            impresion.montoImpuestoVenta = dato.resumenProforma.totalImpuesto;
            impresion.montoTotal = dato.resumenProforma.totalComprobante;

            impresion.fechaImpresion = Date.DateTimeNow();
            return impresion;
        }
    }
}
