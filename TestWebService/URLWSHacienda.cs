using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebService
{
    class URLWSHacienda
    {

        private static string enviroment = "DES";


        public URLWSHacienda()
        {
            enviroment = "DES";
        }

        public URLWSHacienda(string pEnviroment)
        {
            enviroment = pEnviroment;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <returns>URL</returns>
        public static string RECEPCION_POST()
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
        public static string RECEPCION_GET_CLAVE(string clave)
        { 
            if (enviroment.Equals("PRD"))
            {
                return  string.Format("https://api.comprobanteselectronicos.go.cr/recepcion/v1/recepcion/{0}", clave);
            }
            else if (enviroment.Equals("DES"))
            {
                return string.Format("https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/recepcion/{0}", clave);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>URL</returns>
        public static string COMPROBANTE_GET(string parametros)
        { 
            if (enviroment.Equals("PRD"))
            {
                return string.Format("https://api.comprobanteselectronicos.go.cr/recepcion/v1/comprobantes/{0}", parametros);
            }
            else if (enviroment.Equals("DES"))
            {
                return string.Format("https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/comprobantes/{0}", parametros);
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>URL</returns>
        public static string COMPROBANTE_GET_CLAVE(string clave)
        {
            if (enviroment.Equals("PRD"))
            {
                return string.Format("https://api.comprobanteselectronicos.go.cr/recepcion/v1/comprobantes/{0}", clave);
            }
            else if (enviroment.Equals("DES"))
            {
                return string.Format("https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/comprobantes/{0}", clave);
            }
            return null;
        }


    }
}
