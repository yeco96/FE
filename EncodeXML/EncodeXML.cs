using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

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

                string result = new String(decoded_char);

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




        public static Object ObjectToXML(string xml, Type objectType)
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




        public static string GetXMLFromObject(object o)
        {
            StringWriterUtf8 sw = new StringWriterUtf8(); 
            XmlTextWriter tw = null;
            try
            {  
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
               
              
                //xmlns: vc = "http://www.w3.org/2007/XMLSchema-versioning" 
                //xmlns: ds = "http://www.w3.org/2000/09/xmldsig#"


                serializer.Serialize(tw, o); 
                tw.Indentation = (Int32)Formatting.Indented;
                 
                //tw.WriteAttributeString("xmlns", "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica");
                //tw.WriteAttributeString("xmlns:xsd", "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica");

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


        class StringWriterUtf8 : System.IO.StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;

        }



        private static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        public static void validadXMLXSD(String xmlFirmado)
        {
             

            XmlReaderSettings booksSettings = new XmlReaderSettings();
            booksSettings.Schemas.Add("https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/tiqueteElectronico", "TiqueteElectronico_V4.2.xsd");
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            XmlReader books = XmlReader.Create(GenerateStreamFromString(xmlFirmado), booksSettings);

            while (books.Read()) { }
        }

        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
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

    }
}
