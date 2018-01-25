using DevExpress.Utils.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using OAuth2;
using System.Configuration;

namespace Web.WebServices
{
    public class Services
    {
          
        public static void AuthorizeUser(OAuth2Config authorization)
        {
            string Url = GetAuthorizationUrl(authorization);
            HttpContext.Current.Response.Redirect(Url, false);
        }
        /// <summary>    
        ///     
        /// </summary>    
        /// <param name="data"></param>    
        /// <returns></returns>    
        private static string GetAuthorizationUrl(OAuth2Config authorization)
        {
            //get this value by opening your web app in browser.    
            string RedirectUrl = "http://localhost:54762/Pages/facturacion/FrmCargarXML.aspx";
            string Url = authorization.server;
            StringBuilder UrlBuilder = new StringBuilder(Url);
            UrlBuilder.Append("client_id=" + authorization.clientId);
            UrlBuilder.Append("&username=" + authorization.username);
            UrlBuilder.Append("&password=" + authorization.password);
            UrlBuilder.Append("&client_secret=" + authorization.clientSecret);
            UrlBuilder.Append("&scope=" + authorization.scope);
            UrlBuilder.Append("&access_type=" + "password");
            UrlBuilder.Append("&redirect_uri=" + RedirectUrl);

            return UrlBuilder.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="clave"></param>
        /// <returns>response ws json</returns>
        public static async Task<string> getRecepcion(OAuth2Token token, string clave)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URLServices.RECEPCION_GET_CLAVE(clave));
                responseMessage = await httpClient.SendAsync(request);
            }

            return await responseMessage.Content.ReadAsStringAsync();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static async Task  postRecepcion(OAuth2Token token, string jsonData, string result)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                 
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                responseMessage = await httpClient.PostAsync(URLServices.RECEPCION_POST(), content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    result = "Success";
                }
                else
                {
                    result = "Error";
                } 
            }

             string x =  await responseMessage.Content.ReadAsStringAsync();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public static async Task<string> getComprobante(OAuth2Token token, string clave)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URLServices.COMPROBANTE_GET_CLAVE(clave));
                responseMessage = await httpClient.SendAsync(request);
            }

            return await responseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="parametro"></param>
        /// <returns></returns>
        public static async Task<string> getComprobantes(OAuth2Token token, string parametro)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URLServices.COMPROBANTE_GET(parametro));
                responseMessage = await httpClient.SendAsync(request);
            }

            return await responseMessage.Content.ReadAsStringAsync();
        }


    }
}