﻿using Class.Utilidades;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Catalogos;
using WSDomain;
using XMLDomain;

namespace Web.Pages
{
    public partial class ConsultaDocXClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
        }

        XtraReport CreateReport(string clave)
        {

            XtraReport report = null;
            using (var conexion = new DataModelFE())
            {
                string mensaje = "";
                string xml = "";
                if (clave.Substring(29, 2) == TipoDocumento.PROFORMA)
                {
                    WSRecepcionPOSTProforma dato = conexion.WSRecepcionPOSTProforma.Find(clave);
                    if (dato != null)
                    {
                        xml = dato.comprobanteXml;
                    }
                }
                else
                {
                    WSRecepcionPOST dato = conexion.WSRecepcionPOST.Find(clave);
                    if (dato != null)
                    {
                        xml = dato.comprobanteXml;
                    }
                }

                //WSRecepcionPOST dato = conexion.WSRecepcionPOST.Where(x => x.clave == clave).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(xml))
                {
                    //string xml = EncodeXML.XMLUtils.base64Decode(xml);

                    RptComprobante reportES = new RptComprobante();
                    RptComprobanteEN reportEN = new RptComprobanteEN();

                    DocumentoElectronico documento = (DocumentoElectronico)EncodeXML.XMLUtils.getObjetcFromXML(xml);

                    Empresa empresa = conexion.Empresa.Find(documento.emisor.identificacion.numero);

                    if (empresa != null && "EN".Equals(empresa.idioma))
                    {
                        object dataSource = UtilidadesReporte.cargarObjetoImpresion(documento, mensaje, empresa);
                        reportEN.objectDataSource1.DataSource = dataSource;
                        string enviroment_url = ConfigurationManager.AppSettings["ENVIROMENT_URL"].ToString();
                        reportEN.xrBarCode1.Text = (enviroment_url + documento.clave).ToUpper();
                        if (empresa != null && empresa.logo != null)
                        {
                            reportEN.pbLogo.Image = UtilidadesReporte.byteArrayToImage(empresa.logo);
                        }
                        reportEN.CreateDocument();
                        report = reportEN;
                    }
                    else
                    {
                        if (empresa != null && "ES".Equals(empresa.idioma))
                        {
                            object dataSource = UtilidadesReporte.cargarObjetoImpresion(documento, mensaje, empresa);
                            reportES.objectDataSource1.DataSource = dataSource;
                            string enviroment_url = ConfigurationManager.AppSettings["ENVIROMENT_URL"].ToString();
                            reportES.xrBarCode1.Text = (enviroment_url + documento.clave).ToUpper();
                            if (empresa != null && empresa.logo != null)
                            {
                                reportES.pbLogo.Image = UtilidadesReporte.byteArrayToImage(empresa.logo);
                            }
                            reportES.CreateDocument();
                            report = reportES;
                        }
                        else
                        {
                            this.alertMessages.Attributes["class"] = "alert alert-danger";
                            this.alertMessages.InnerText = "El documento ingresado no se encuentra en el repositorio de datos del emisor!!!";
                        }

                    }
                    this.alertMessages.Attributes["class"] = "alert alert-info";
                    this.alertMessages.InnerText = "Los datos fueron enviados correctamente!!!";
                }
                else {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "El documento ingresado no se encuentra en el repositorio de datos del emisor!!!";
                }
            }

            return report;

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClave.Text.Length == 50)
                {
                    ASPxWebDocumentViewer1.OpenReport(CreateReport(txtClave.Text));
                }
                else
                {
                    this.alertMessages.Attributes["class"] = "alert alert-danger";
                    this.alertMessages.InnerText = "La clave ingresada no posee el formato correcto, favor verificar el tamaño del mismo!!!";
                }
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
        }
    }
}