using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using XMLDomain;

namespace EncodeXML
{
    public class EncondeXML
    {
        /// <summary>
        /// decodifica un string de base 64
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string base64Decode(string data)
        {
            try 
            {

                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding(); 
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                 
                byte[] todecode_byte = Convert.FromBase64String(data);

                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);

                char[] decoded_char = new char[charCount];

                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);

                string result = new string(decoded_char);

                return result;

            } 
            catch (Exception e) 
            { 
                throw new Exception("Error en Descodificar a base 64" + e.Message); 
            }

        }

        
        /// <summary>
        /// Codifica un string a base 64
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string base64Encode(string data)

        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error en Codificar a base 64" + e.Message);
            }

        }


        public static T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        /// <summary>
        /// tranforma un XML a su objeto correspondiente mapeado
        /// </summary>
        /// <param name="xml">XML con la data</param>
        /// <param name="objectType">tipo de objeto</param>
        /// <returns></returns>
        public static Object objectToXML(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception exp)
            {
                //Handle Exception Code
                return exp.Message;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return obj;
        }



        /// <summary>
        /// Optiene un XML del objeto enviado
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string getXMLFromObject(object o)
        { 
            stringWriterUtf8 sw = new stringWriterUtf8(); 
            XmlTextWriter tw = null;
            try
            {  
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);

                if(typeof(FacturaElectronica) == o.GetType()) { 
                    tw.WriteStartElement("FacturaElectronica");
                    tw.WriteAttributeString("xmlns", null, "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica");
                    tw.WriteAttributeString("xmlns", "xsd", null, "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica");
                    tw.WriteAttributeString("xmlns", "xs", null, "http://www.w3.org/2001/XMLSchema");
                    tw.WriteAttributeString("xmlns", "vc", null, "http://www.w3.org/2007/XMLSchema-versioning");
                    tw.WriteAttributeString("xmlns", "ds", null, "http://www.w3.org/2000/09/xmldsig#");
                    tw.WriteEndElement();
                    tw.Flush();
                }

                serializer.Serialize(tw, o); 
                tw.Indentation = (Int32)Formatting.Indented;
            
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }


        class stringWriterUtf8 : System.IO.StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;

        }

        /// <summary>
        /// Valida el formato XML contra su XDS
        /// </summary>
        /// <param name="xmlFirmado"></param>
        public static void validadXMLXSD(string xmlFirmado)
        {
            XmlReaderSettings booksSettings = new XmlReaderSettings();
            booksSettings.Schemas.Add("https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4.2/facturaElectronica", "facturaElectronica");
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            XmlReader books = XmlReader.Create(generateStreamFromstring(xmlFirmado), booksSettings);

            while (books.Read()) { }
        }



        /// <summary>
        /// Objetiene un Stream de datos de un objeto tipo string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static Stream generateStreamFromstring(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }



        private static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Busca el valor de una etiqueta dentro de un XML
        /// </summary>
        /// <param name="tagPadre">nombre de la etiqueta padre Ej: emisor receptor MensajeHacienda FacturaElectronica NotaCreditoElectronica NotaDebitoElectronica</param>
        /// <param name="label">nombre de la etiqueta</param>
        /// <param name="xml">XML donde se busca la etiqueta</param>
        /// <returns></returns>
        public static string buscarValorEtiquetaXML(string tagPadre,string label, string xml)
        {
            try {  
                string dato = "";
                XmlDocument xm = new XmlDocument();
                xm.LoadXml(xml);
                XmlNodeList xmlTag = xm.GetElementsByTagName(tagPadre);
                XmlNodeList lista = ((XmlElement)xmlTag[0]).GetElementsByTagName(label);
                foreach (XmlElement nodo in lista)
                {
                    XmlNodeList nNombre = nodo.GetElementsByTagName(label);
                    dato = nodo.InnerText;
                }
                return dato;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public static string tipoDocumentoXML(string xml)
        {
            if (xml.Contains("MensajeHacienda"))
                return "MensajeHacienda";
          
            if (xml.Contains("FacturaElectronica"))
                return "FacturaElectronica";

            if (xml.Contains("NotaCreditoElectronica"))
                return "NotaCreditoElectronica";

            if (xml.Contains("NotaDebitoElectronica"))
                return "NotaDebitoElectronica";

            if (xml.Contains("TiqueteElectronico"))
                return "TiqueteElectronico";

            return null;
        }



    }
}
