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
        public static Object getObjetcFromXML(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null; 
            
            try
            {
                string nodoInicial = "";
                string nodoFinal = "";

                if (typeof(FacturaElectronica) == objectType)
                { 
                    nodoInicial = "<FacturaElectronica>";
                    nodoFinal = "</FacturaElectronica>";
                }
                if (typeof(NotaCreditoElectronica) == objectType)
                { 
                    nodoInicial = "<NotaCreditoElectronica>";
                    nodoFinal = "</NotaCreditoElectronica>";
                }
                if (typeof(NotaDebitoElectronica) == objectType)
                {  
                    nodoInicial = "<NotaDebitoElectronica>";
                    nodoFinal = "</NotaDebitoElectronica>";
                }
                if (typeof(TiqueteElectronico) == objectType)
                {  
                    nodoInicial = "<TiqueteElectronico>";
                    nodoFinal = "</TiqueteElectronico>";
                }
                if (typeof(MensajeReceptor) == objectType)
                { 
                    nodoInicial = "<MensajeReceptor>";
                    nodoFinal = "</MensajeReceptor>";
                }

                int start = xml.IndexOf("<Clave>");
                int end = xml.IndexOf("<ds:Signature xmlns");
                xml = xml.Substring(0, end) + nodoFinal;
                xml = nodoInicial + xml.Substring(start);

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
        /// tranforma un XML a su objeto correspondiente mapeado
        /// </summary>
        /// <param name="xml">XML con la data</param> 
        /// <returns></returns>
        public static Object getObjetcFromXML(string xml)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            Type objectType = null;

            try
            {
                string nodoInicial = "";
                string nodoFinal = "";

                if (xml.Contains("FacturaElectronica"))
                {
                    nodoInicial = "<FacturaElectronica>";
                    nodoFinal = "</FacturaElectronica>";
                    objectType = typeof(FacturaElectronica);
                }
                if (xml.Contains("NotaCreditoElectronica"))
                {
                    nodoInicial = "<NotaCreditoElectronica>";
                    nodoFinal = "</NotaCreditoElectronica>";
                    objectType = typeof(NotaCreditoElectronica);
                }
                if (xml.Contains("NotaDebitoElectronica"))
                {
                    nodoInicial = "<NotaDebitoElectronica>";
                    nodoFinal = "</NotaDebitoElectronica>";
                    objectType = typeof(NotaDebitoElectronica);
                }
                if (xml.Contains("TiqueteElectronico"))
                {
                    nodoInicial = "<TiqueteElectronico>";
                    nodoFinal = "</TiqueteElectronico>";
                    objectType = typeof(TiqueteElectronico);
                }
                if (xml.Contains("MensajeReceptor"))
                {
                    nodoInicial = "<MensajeReceptor>";
                    nodoFinal = "</MensajeReceptor>";
                    objectType = typeof(MensajeReceptor);
                }

                if (xml.Contains("ProformaElectronica"))
                {
                    nodoInicial = "<ProformaElectronica>";
                    nodoFinal = "</ProformaElectronica>";
                    objectType = typeof(ProformaElectronica);
                }

                int start = xml.IndexOf("<Clave>");
                int end = xml.IndexOf("<ds:Signature xmlns");

                if (start > 0 && end > 0)
                {
                    xml = xml.Substring(0, end) + nodoFinal;
                    xml = nodoInicial + xml.Substring(start);
                }
                else
                {
                    xml = nodoInicial + xml.Substring(start);
                }

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
            string xml = "";
            string tipo_documento = "";
            string nodo_inicial = "";
            stringWriterUtf8 sw = new stringWriterUtf8(); 
            XmlTextWriter tw = null;
            try
            {  
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                
                serializer.Serialize(tw, o); 
                tw.Indentation = (Int32)Formatting.Indented;

                if (typeof(FacturaElectronica) == o.GetType())
                {
                    tipo_documento = "facturaElectronica";
                    nodo_inicial = "FacturaElectronica ";
                } 
                if (typeof(NotaCreditoElectronica) == o.GetType())
                {
                    tipo_documento = "notaCreditoElectronica";
                    nodo_inicial = "NotaCreditoElectronica ";
                }
                if (typeof(NotaDebitoElectronica) == o.GetType())
                {
                    tipo_documento = "notaDebitoElectronica";
                    nodo_inicial = "NotaDebitoElectronica ";
                }
                if (typeof(TiqueteElectronico) == o.GetType())
                {
                    tipo_documento = "tiqueteElectronico";
                    nodo_inicial = "TiqueteElectronico ";
                }
                if (typeof(MensajeReceptor) == o.GetType())
                {
                    tipo_documento = "mensajeReceptor";
                    nodo_inicial = "MensajeReceptor ";
                }
                //Se agrega POroforma Electronica
                if (typeof(ProformaElectronica) == o.GetType())
                {
                    tipo_documento = "facturaElectronica";
                    nodo_inicial = "FacturaElectronica ";
                }


                xml = sw.ToString();
                //FacturaElectronica xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
                //xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                //xmlns ="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica"

                xml = xml.Replace("http://www.w3.org/2001/XMLSchema-instance", "XSI");// esto es solo para que no se reemplace por el de abajo
                xml = xml.Replace("http://www.w3.org/2001/XMLSchema", "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/" + tipo_documento );

                xml = xml.Replace("XSI","http://www.w3.org/2001/XMLSchema-instance");

                if (typeof(ProformaElectronica) == o.GetType())
                {
                    xml = xml.Replace("ProformaElectronica", "FacturaElectronica");// esto es solo para que no se reemplace por el de abajo
                }

                string new_atribute = nodo_inicial + " ";
               // string new_atribute = nodo_inicial + "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ";
                new_atribute += "xmlns=\"https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/" + tipo_documento + "\" ";
                ///new_atribute += "xmlns:xsd=\"https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/" + tipo_documento + "\" ";
                new_atribute += "xmlns:xs=\"http://www.w3.org/2001/XMLSchema" + "\" ";
               // new_atribute += "xmlns:vc=\"http://www.w3.org/2007/XMLSchema-versioning" + "\" ";
                //new_atribute += "xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#a" + "\" "; 

                xml = xml.Replace(nodo_inicial, new_atribute);
                 
                //tw.WriteAttributeString("xmlns", null, "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica");
                //tw.WriteAttributeString("xmlns", "xsd", null, "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica");
                //tw.WriteAttributeString("xmlns", "xs", null, "http://www.w3.org/2001/XMLSchema");
                //tw.WriteAttributeString("xmlns", "vc", null, "http://www.w3.org/2007/XMLSchema-versioning");
                //tw.WriteAttributeString("xmlns", "ds", null, "http://www.w3.org/2000/09/xmldsig#");
             


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
            return xml;
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

                    if (!string.IsNullOrWhiteSpace(dato))
                    {
                        break;
                    }
                }
                return dato;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }


        /// <summary>
        /// tipo de documento presente en el XML
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string tipoDocumentoXML(string xml)
        {
            if (xml.Contains("FacturaElectronica"))
                return "FacturaElectronica";

            if (xml.Contains("MensajeReceptor"))
                return "MensajeReceptor";

            if (xml.Contains("MensajeHacienda"))
                return "MensajeHacienda";

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
