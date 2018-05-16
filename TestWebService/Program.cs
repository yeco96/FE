using EncodeXML;
using FirmaXadesNet;
using Newtonsoft.Json;
using OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WSDomain;
using XMLDomain;

namespace TestWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            OAuth2Token token = OAuth2.OAuth2Config.getToken();

            
            //enviarDocumento(token);
            //Console.ReadKey();
            consultarDocumento(token);

            //  consultarComprobantes(OAuth2.OAuth2Config.getToken());

            Console.ReadKey();
        }


       

        /// <summary>
        /// consulta un documento de hacienda, requiere numero de de 50 digitos
        /// </summary>
        public static void consultarDocumento(OAuth2Token token)
        { 
            string clave = "50613021800060354097400100002010000000003188888888";
             
            //secured web api request
            string response = getRecepcion(token, clave)
                .GetAwaiter()
                .GetResult();

            if (string.IsNullOrWhiteSpace(response))
            { 
                Console.WriteLine("NOT Response received from WebAPI:");
            }
            else{ 
                WSRecepcionGET trama = JsonConvert.DeserializeObject<WSRecepcionGET>(response);

                Console.WriteLine("");
                Console.WriteLine("Response received from WebAPI:");
                Console.WriteLine("clave -> " + trama.clave);
                Console.WriteLine("fecha -> " + trama.fecha);
                Console.WriteLine("indEstado -> " + trama.indEstado);
                //Console.WriteLine(response);

                string xmlDecode = EncodeXML.XMLUtils.base64Decode(trama.respuestaXml);

                string data = EncodeXML.XMLUtils.buscarValorEtiquetaXML("MensajeHacienda", "DetalleMensaje", xmlDecode);
                Console.WriteLine(data);
            }

        }

        /// <summary>
        /// envia documento a hacienda, requiere de una factura en xml y firmada , codebase64
        /// </summary>
        public static void enviarDocumento(OAuth2Token token)
        {
            
            WSRecepcionPOST post = new WSRecepcionPOST();
            post.clave = "50613021800060354097400100002010000000003188888888";
            post.emisor.tipoIdentificacion = "01";
            post.emisor.numeroIdentificacion = "603540974";
            post.receptor.tipoIdentificacion = "01";
            post.receptor.numeroIdentificacion = "601230863";
            
            FacturaElectronica fact = new FacturaElectronica();
            fact.iniciarParametros();

            string path = Path.Combine(Path.GetFullPath("fact.xml"));

            string xmlData = File.ReadAllText(path);
            //string xmlData = XMLUtilsgetXMLFromObject(fact);   

            //XMLUtilsvalidadXMLXSD(xmlData);

            string xmlDataSigned = FirmaXML.getXMLFirmado(xmlData);

          

            // guarda xml firmado para pruebas
            File.WriteAllText(Path.GetFullPath("fact_firma.xml"), xmlDataSigned); 

            post.comprobanteXml = EncodeXML.XMLUtils.base64Encode(xmlDataSigned);

            string jsonTrama = JsonConvert.SerializeObject(post);

            string responsePost = postRecepcion(token, jsonTrama)
                .GetAwaiter()
                .GetResult();

        }


        /// <summary>
        /// lista de comprobantes de hacienda
        /// </summary>
        /// <param name="token"></param>
        /// <param name="parametro"></param>
        public static void consultarComprobantes(OAuth2Token token)
        {
            ComprobanteParametro parametro = new ComprobanteParametro();
            parametro.limit = 2;
            parametro.offset = 1;


             //secured web api request
            string response = getComprobantes(token, parametro.ToString())
                .GetAwaiter()
                .GetResult();

            WSRecepcionGET trama = JsonConvert.DeserializeObject<WSRecepcionGET>(response);

            Console.WriteLine("");
            Console.WriteLine("Response received from WebAPI:");
            Console.WriteLine("clave -> " + trama.clave);
            Console.WriteLine("fecha -> " + trama.fecha);
            Console.WriteLine("indEstado -> " + trama.indEstado);
            //Console.WriteLine(response);

            string xmlDecode = EncodeXML.XMLUtils.base64Decode(trama.respuestaXml);

            string data = EncodeXML.XMLUtils.buscarValorEtiquetaXML("MensajeHacienda", "DetalleMensaje", xmlDecode);
            Console.WriteLine(data);

        }



       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="clave"></param>
        /// <returns>response ws json</returns>
        private static async Task<string> getRecepcion(OAuth2Token token, string clave)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URLWSHacienda.RECEPCION_GET_CLAVE(clave));
                responseMessage = await httpClient.SendAsync(request);
            }

            return await responseMessage.Content.ReadAsStringAsync();
        }


        private static async Task<string> postRecepcion(OAuth2Token token, string jsonData)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                // HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URLWSHacienda.COMPROBANTE_GET());

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                responseMessage = await httpClient.PostAsync(URLWSHacienda.RECEPCION_POST(), content);
                 
                if (responseMessage.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                { 
                    Console.Write("Error");
                }

            }

            return await responseMessage.Content.ReadAsStringAsync();
        }



        private static async Task<string> getComprobante(OAuth2Token token, string clave)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URLWSHacienda.COMPROBANTE_GET_CLAVE(clave));
                responseMessage = await httpClient.SendAsync(request);
            }

            return await responseMessage.Content.ReadAsStringAsync();
        }

        private static async Task<string> getComprobantes(OAuth2Token token, string parametro)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URLWSHacienda.COMPROBANTE_GET(parametro));
                responseMessage = await httpClient.SendAsync(request);
            }

            return await responseMessage.Content.ReadAsStringAsync();
        }


    }
}
