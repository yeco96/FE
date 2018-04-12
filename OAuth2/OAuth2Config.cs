using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2
{
    [Table("conf_oauth2")]
    public class OAuth2Config
    {
        [Key]
        [Required]
        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Ambiente")]
        public string enviroment { set; get; }

        [Display(Name = "Servidor")]
        public string server { set; get; }

        [Display(Name = "Cliente Id")]
        public string clientId { set; get; }

        [Display(Name = "Cliente Clave")]
        public string clientSecret { set; get; }

        [Display(Name = "Scope")]
        public string scope { set; get; }

        [NotMapped]
        [Display(Name = "Token")]
        public OAuth2Token token { set; get; }


        /// <summary>
        /// VALORES PARA PRUEBAS
        /// </summary>

        [NotMapped]
        [Display(Name = "Nombre Usuario")]
        public string username { set; get; }

        [NotMapped]
        [Display(Name = "Contraseña")]
        public string password { set; get; }

        [NotMapped]
        [Display(Name = "Servidor URL")]
        public Uri urlServer { set; get; }



        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public OAuth2Config()
        {
            this.token = new OAuth2Token();
            this.urlServer = new Uri("https://idp.comprobanteselectronicos.go.cr");
        }


        // <summary>
        /// optiene el TOKEN de autorizacion
        /// </summary>
        /// <param name="authorization"> objeto de tipo OAuth2Config con la configuracion para la connexion</param>
        /// <returns>un objeto de tpo OAuth2Config.Token dentro del mismo parametro de entrada authorization</returns> 
        public static async Task getTokenWeb(OAuth2Config authorization)
        {
            try {
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

                using (HttpClient httpClient = new HttpClient())
                {
                    //HttpClient httpClient = new HttpClient();
                    HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, authorization.server);

                    tokenRequest.Content = httpContent;

                    HttpResponseMessage response = await httpClient.SendAsync(tokenRequest);
                    string tokenResult = await response.Content.ReadAsStringAsync();

                    authorization.token = JsonConvert.DeserializeObject<OAuth2Token>(tokenResult);
                }
            }
            catch (Exception e)
            {
                authorization = null;
            } 

        }



        // <summary>
        /// optiene el TOKEN de autorizacion
        /// </summary>
        /// <param name="authorization"> objeto de tipo OAuth2Config con la configuracion para la connexion</param>
        /// <returns>un objeto de tpo OAuth2Config.Token dentro del mismo parametro de entrada authorization</returns> 
        public static async Task getTokenWeb(OAuth2Config authorization, string username, string password)
        {
            try
            {
                HttpContent httpContent = new FormUrlEncodedContent(
                new[]
                {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("client_id", authorization.clientId),
                new KeyValuePair<string, string>("scope", authorization.scope),
                new KeyValuePair<string, string>("client_secret", authorization.clientSecret)
                });

                using (HttpClient httpClient = new HttpClient())
                {
                    //HttpClient httpClient = new HttpClient();
                    HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, authorization.server);

                    tokenRequest.Content = httpContent;

                    HttpResponseMessage response = await httpClient.SendAsync(tokenRequest);
                    string tokenResult = await response.Content.ReadAsStringAsync();

                    authorization.token = JsonConvert.DeserializeObject<OAuth2Token>(tokenResult);
                }
            }
            catch (Exception e)
            {
                authorization = null;
            }

        }


        /// <summary>
        /// optiene el TOKEN de autorizacion
        /// </summary>
        /// <returns>un objeto de tpo OAuth2Config.Token</returns>
        public static OAuth2Token getToken()
        {
            //authorization server parameters owned from the client
            OAuth2Config authorization = new OAuth2Config();
            authorization.loadParameter();

            //access token request
            string tokenStr = requestTokenToAuthorizationServer(authorization)
                .GetAwaiter()
                .GetResult();

            // authorizationServer token 
            OAuth2Token token = JsonConvert.DeserializeObject<OAuth2Token>(tokenStr);

            return token;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns>token authorization json</returns>
        private static async Task<string> requestTokenToAuthorizationServer(OAuth2Config authorization)
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
        /// datos de pruebas 
        /// </summary>
        public void loadParameter()
        {
            enviroment = "DES";

            if (enviroment.Equals("PRD")) { 
                server = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut/protocol/openid-connect/token";
                clientId = "api-prod";
                clientSecret = "";
                scope = "";
                username = "cpf-06-0354-0974@stag.comprobanteselectronicos.go.cr";
                password = "]2ty.[S*-SGQJ&*#]sh#";
            } else if (enviroment.Equals("DES"))
            {
                server = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token";
                clientId = "api-stag";
                clientSecret = "";
                scope = "";
                username = "cpf-06-0354-0974@stag.comprobanteselectronicos.go.cr";
                password = "]2ty.[S*-SGQJ&*#]sh#";
            }
        }
    }
}
