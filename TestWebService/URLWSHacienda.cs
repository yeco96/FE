using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebService
{
    class URLWSHacienda
    {  
        public static string enviroment = "DES";
          
        /// <summary>
        /// 
        /// </summary>
        /// <returns>URL</returns>
        public static String RECEPCION_POST()
        { 
            if (enviroment.Equals("PRD"))
            {
                return "https://api.comprobanteselectronicos.go.cr/recepcion/v1/recepcion";
            }else if (enviroment.Equals("DES"))
            { 
                return "https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/recepcion";
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>URL</returns>
        public static String RECEPCION_GET_CLAVE(string clave)
        { 
            if (enviroment.Equals("PRD"))
            {
                return  String.Format("https://api.comprobanteselectronicos.go.cr/recepcion/v1/recepcion/{0}", clave);
            }
            else if (enviroment.Equals("DES"))
            {
                return String.Format("https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/recepcion/{0}", clave);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>URL</returns>
        public static String COMPROBANTE_GET(String parametros)
        { 
            if (enviroment.Equals("PRD"))
            {
                return String.Format("https://api.comprobanteselectronicos.go.cr/recepcion/v1/comprobantes/{0}", parametros);
            }
            else if (enviroment.Equals("DES"))
            {
                return String.Format("https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/comprobantes/{0}", parametros);
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>URL</returns>
        public static String COMPROBANTE_GET_CLAVE(string clave)
        {
            if (enviroment.Equals("PRD"))
            {
                return String.Format("https://api.comprobanteselectronicos.go.cr/recepcion/v1/comprobantes/{0}", clave);
            }
            else if (enviroment.Equals("DES"))
            {
                return String.Format("https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/comprobantes/{0}", clave);
            }
            return null;
        }


    }
}
