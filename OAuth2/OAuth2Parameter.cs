using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2
{
    public class OAuth2Parameter
    {

        public static string enviroment = "DES";

        public Uri server;
        public string clientId;
        public string clientSecret;
        public string scope;
        public string username;
        public string password;

        public OAuth2Parameter()
        {
            this.server = new Uri("https://idp.comprobanteselectronicos.go.cr");
        }


        public void loadParameter()
        {
            if (enviroment.Equals("PRD")) { 
                server = new Uri("https://idp.comprobanteselectronicos.go.cr/auth/realms/rut/protocol/openid-connect/token");
                clientId = "api-prod";
                clientSecret = "";
                scope = "";
                username = "cpf-06-0354-0974@stag.comprobanteselectronicos.go.cr";
                password = "]2ty.[S*-SGQJ&*#]sh#";
            } else if (enviroment.Equals("DES"))
            {
                server = new Uri("https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token");
                clientId = "api-stag";
                clientSecret = "";
                scope = "";
                username = "cpf-06-0354-0974@stag.comprobanteselectronicos.go.cr";
                password = "]2ty.[S*-SGQJ&*#]sh#";
            }
        }
    }
}
