using FirmaXadesNet;
using FirmaXadesNet.Crypto;
using FirmaXadesNet.Signature.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FirmaXadesNet
{
    public class FirmaXML
    {
        public X509Certificate2 certificado;

        public FirmaXML()
        {

        }

        public static string getXMLFirmado(string xml)
        {
            XadesService xadesService = new XadesService();
            SignatureParameters parametros = new SignatureParameters();
              
            // Política de firma de factura-e 3.1
            parametros.SignaturePolicyInfo = new SignaturePolicyInfo();
            parametros.SignaturePolicyInfo.PolicyIdentifier = "https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4.2/ResolucionComprobantesElectronicosDGT-R-48-2016_4.2.pdf";
            parametros.SignaturePolicyInfo.PolicyHash = "Ohixl6upD6av8N7pEvDABhEL6hM=";
            parametros.SignaturePackaging = SignaturePackaging.ENVELOPED;
            parametros.DataFormat = new DataFormat();
            parametros.DataFormat.MimeType = "text/xml";
            parametros.SignerRole = new SignerRole();
            parametros.SignerRole.ClaimedRoles.Add("emisor"); 

            //selecciona el certificado del archivop12
            var selectedCertificate = GetSelectedCertificate(null, "6891"); 
  
            using (parametros.Signer = new Signer(selectedCertificate))
            { 
                // convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(xml);
                //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
                MemoryStream stream = new MemoryStream(byteArray);
                 
                var docFirmado = xadesService.Sign(stream, parametros);
                return docFirmado.Document.InnerXml; 
                
            } 
        }

        /// <summary>
        /// Firma un documento XML
        /// </summary>
        /// <param name="xml">documenot XML</param>
        /// <param name="data_p12">llave criptografica del usuario</param>
        /// <param name="clave">clave de la llave criptografica</param>
        /// <returns></returns>
        public static string getXMLFirmadoWeb(string xml, byte[] data_p12, string clave )
        {
            XadesService xadesService = new XadesService();
            SignatureParameters parametros = new SignatureParameters();

            // Política de firma de factura-e 3.1
            parametros.SignaturePolicyInfo = new SignaturePolicyInfo();
            parametros.SignaturePolicyInfo.PolicyIdentifier = "https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4.2/ResolucionComprobantesElectronicosDGT-R-48-2016_4.2.pdf";                                                   
            parametros.SignaturePolicyInfo.PolicyHash = "Ohixl6upD6av8N7pEvDABhEL6hM=";
            parametros.SignaturePackaging = SignaturePackaging.ENVELOPED;
            parametros.DataFormat = new DataFormat();
            parametros.DataFormat.MimeType = "text/xml";
            parametros.SignerRole = new SignerRole();
            parametros.SignerRole.ClaimedRoles.Add("emisor");

            //selecciona el certificado del archivop12
            var selectedCertificate = GetSelectedCertificate(data_p12, clave);

            using (parametros.Signer = new Signer(selectedCertificate))
            {
                // convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(xml);
                //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
                MemoryStream stream = new MemoryStream(byteArray);

                var docFirmado = xadesService.Sign(stream, parametros);
                return docFirmado.Document.InnerXml;

            }
        }


        public static X509Certificate2 GetSelectedCertificate(byte[] data, string password) 
        {
            if(data != null)
            {
                return (new X509Certificate2(data, password));
            }
            else
            {
                return (new X509Certificate2("060354097414.p12", "6891"));
            }
            
        }
    }
}
