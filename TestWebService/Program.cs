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
           // enviarDocumento(getToken());
            consultarDocumento(getToken());

          //  consultarComprobantes(getToken());

            Console.ReadKey();
        }


        public static Token getToken()
        {
            //authorization server parameters owned from the client
            OAuth2Parameter authorization = new OAuth2Parameter();
            authorization.loadParameter();

            //access token request
            string tokenStr = requestTokenToAuthorizationServer(authorization)
                .GetAwaiter()
                .GetResult();

            // authorizationServer token 
            Token token = JsonConvert.DeserializeObject<Token>(tokenStr);

            return token;
        }


        /// <summary>
        /// consulta un documento de hacienda, requiere numero de de 50 digitos
        /// </summary>
        public static void consultarDocumento(Token token)
        { 
            string clave = "50608011800060354097400100001010000000015188888888";
             
            //secured web api request
            string response = getRecepcion(token, clave)
                .GetAwaiter()
                .GetResult();

            RecepcionGET trama = JsonConvert.DeserializeObject<RecepcionGET>(response);

            Console.WriteLine("");
            Console.WriteLine("Response received from WebAPI:");
            Console.WriteLine("clave -> " + trama.clave);
            Console.WriteLine("fecha -> " + trama.fecha);
            Console.WriteLine("indEstado -> " + trama.indEstado);
            //Console.WriteLine(response);

            string xmlDecode = EncodeXML.EncondeXML.base64Decode(trama.respuestaXml);

            string data = ObtenerValores("DetalleMensaje", xmlDecode);
            Console.WriteLine(data);

        }

        /// <summary>
        /// envia documento a hacienda, requiere de una factura en xml y firmada , codebase64
        /// </summary>
        public static void enviarDocumento(Token token)
        {
            
            RecepcionPOST post = new RecepcionPOST();
            post.clave = "5060801180006035409740010000104000000000188888888";
            post.emisor.tipoIdentificacion = "01";
            post.emisor.numeroIdentificacion = "603540974";
            post.receptor.tipoIdentificacion = "01";
            post.receptor.numeroIdentificacion = "207550498";


            FacturaElectronica fact = new FacturaElectronica();
            fact.iniciarParametros();

            String path = Path.Combine(Path.GetFullPath("fact.xml"));
            //String xmlData = File.ReadAllText(path);
            String xmlData = EncondeXML.GetXMLFromObject(fact);    
            String xmlDataSigned = FirmaXML.getXMLFirmado(xmlData);

           // EncondeXML.validadXMLXSD(xmlDataSigned);

            // guarda xml firmado para pruebas
            File.WriteAllText(Path.GetFullPath("fact_firma.xml"), xmlDataSigned); 

            post.comprobanteXml = EncodeXML.EncondeXML.base64Encode(xmlDataSigned);

            String jsonTrama = JsonConvert.SerializeObject(post);

            string responsePost = postRecepcion(token, jsonTrama)
                .GetAwaiter()
                .GetResult();

        }


        /// <summary>
        /// lista de comprobantes de hacienda
        /// </summary>
        /// <param name="token"></param>
        /// <param name="parametro"></param>
        public static void consultarComprobantes(Token token)
        {
            ComprobanteParametro parametro = new ComprobanteParametro();
            parametro.limit = 2;
            parametro.offset = 1;


             //secured web api request
            string response = getComprobantes(token, parametro.ToString())
                .GetAwaiter()
                .GetResult();

            RecepcionGET trama = JsonConvert.DeserializeObject<RecepcionGET>(response);

            Console.WriteLine("");
            Console.WriteLine("Response received from WebAPI:");
            Console.WriteLine("clave -> " + trama.clave);
            Console.WriteLine("fecha -> " + trama.fecha);
            Console.WriteLine("indEstado -> " + trama.indEstado);
            //Console.WriteLine(response);

            string xmlDecode = EncodeXML.EncondeXML.base64Decode(trama.respuestaXml);

            string data = ObtenerValores("DetalleMensaje", xmlDecode);
            Console.WriteLine(data);

        }



        private static string ObtenerValores(string label, string xml)
        {
            string dato = ""; 
            XmlDocument xm = new XmlDocument(); 
            xm.LoadXml(xml); 
            XmlNodeList personas = xm.GetElementsByTagName("MensajeHacienda"); 
            XmlNodeList lista = ((XmlElement)personas[0]).GetElementsByTagName(label);
            foreach (XmlElement nodo in lista)
            {
                XmlNodeList nNombre = nodo.GetElementsByTagName(label); 
                dato = nodo.InnerText;
            }
            return dato;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns>token authorization json</returns>
        private static async Task<string> requestTokenToAuthorizationServer(OAuth2Parameter authorization)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, authorization.server);
                HttpContent httpContent = new FormUrlEncodedContent(
                    new[]
                    {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", authorization.username),
                    new KeyValuePair<string, string>("password", authorization.password),
                    new KeyValuePair<string, string>("client_id", authorization.clientId),
                    new KeyValuePair<string, string>("scope", authorization.scope),
                    new KeyValuePair<string, string>("client_secret", authorization.clientSecret)
                    });
                tokenRequest.Content = httpContent;
                responseMessage = await client.SendAsync(tokenRequest);
            }
            return await responseMessage.Content.ReadAsStringAsync();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="clave"></param>
        /// <returns>response ws json</returns>
        private static async Task<string> getRecepcion(Token token, String clave)
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


        private static async Task<string> postRecepcion(Token token, String jsonData)
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



        private static async Task<string> getComprobante(Token token, String clave)
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

        private static async Task<string> getComprobantes(Token token, String parametro)
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
