using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebServices.Controllers
{
    public class URLServices
    {
        private static string enviroment = ConfigurationManager.AppSettings["ENVIROMENT"].ToString();


        public URLServices()
        {
            enviroment = "DES";
        }

        public URLServices(string pEnviroment)
        {
            enviroment = pEnviroment;
        } 

        /// <summary>
        /// PRD = https://api.comprobanteselectronicos.go.cr/recepcion/v1/recepcio
        /// DES = https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/recepcion
        /// </summary>
        /// <returns>URL</returns>
        public static string RECEPCION_POST()
        {
            if (enviroment.Equals("PRD"))
            {
                return "https://api.comprobanteselectronicos.go.cr/recepcion/v1/recepcion";
            }
            else if (enviroment.Equals("DES"))
            {
                return "https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/recepcion";
            }
            return null;
        }

        /// <summary>
        /// PRD = https://api.comprobanteselectronicos.go.cr/recepcion/v1/recepcion/{0}
        /// DES = https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/recepcion/{0}
        /// </summary>
        /// <returns>URL</returns>
        public static string RECEPCION_GET_CLAVE(string clave)
        {
            if (enviroment.Equals("PRD"))
            {
                return string.Format("https://api.comprobanteselectronicos.go.cr/recepcion/v1/recepcion/{0}", clave);
            }
            else if (enviroment.Equals("DES"))
            {
                return string.Format("https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/recepcion/{0}", clave);
            }
            return null;
        }

        /// <summary>
        /// PRD = https://api.comprobanteselectronicos.go.cr/recepcion/v1/comprobantes/{0}
        /// DES = https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/comprobantes/{0}
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
        /// PRD = https://api.comprobanteselectronicos.go.cr/recepcion/v1/comprobantes/{0}
        /// DES = https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1/comprobantes/{0}
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