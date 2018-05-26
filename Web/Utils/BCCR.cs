using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Web.Utils
{
    public class BCCR
    {
        public static Double tipoCambioDOLAR(DateTime fecha)
        {
            try {
                if(fecha==null){
                    fecha = DateTime.Now;
                }

                ServiceReferenceBCCR.wsIndicadoresEconomicosSoapClient serviceBCCR = new ServiceReferenceBCCR.wsIndicadoresEconomicosSoapClient();
                string xml = serviceBCCR.ObtenerIndicadoresEconomicosXML("318", fecha.ToString("dd/MM/yyyy"), fecha.ToString("dd/MM/yyyy"), "MSASoft", "N");
                 
                XmlDocument xm = new XmlDocument();
                xm.LoadXml(xml);
                XmlNodeList dato = xm.GetElementsByTagName("NUM_VALOR"); 
                return Math.Round(Double.Parse(dato[0].InnerXml),2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public static Double tipoCambioEURO(DateTime fecha)
        {
            try
            {
                if (fecha == null)
                {
                    fecha = DateTime.Now;
                }

                ServiceReferenceBCCR.wsIndicadoresEconomicosSoapClient serviceBCCR = new ServiceReferenceBCCR.wsIndicadoresEconomicosSoapClient();
                string xml = serviceBCCR.ObtenerIndicadoresEconomicosXML("318", fecha.ToString("dd/MM/yyyy"), fecha.ToString("dd/MM/yyyy"), "MSASoft", "N");

                XmlDocument xm = new XmlDocument();
                xm.LoadXml(xml);
                XmlNodeList dato = xm.GetElementsByTagName("NUM_VALOR");
                return Math.Round(Double.Parse(dato[0].InnerXml), 2);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}