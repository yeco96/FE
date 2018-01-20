using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2
{
    public class Token
    {
        public Token()
        {
        }
  
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string refresh_token { get; set; } 
        public string token_type { get; set; }
        public string id_token { get; set; } 
        public int not_before_policy { get; set; }
        public string session_state { get; set; }

        public override string ToString()
        {
            String result = "";
            result += "access_token : " + access_token; 
            result += "expires_in : " + expires_in;
            result += "refresh_expires_in : " + refresh_expires_in;
            result += "token_type : " + token_type;
            result += "id_token : " + id_token;
            result += "not_before_policy : " + not_before_policy;
            result += "session_state : " + session_state;
            return result;
        }

    }
}
