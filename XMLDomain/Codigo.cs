using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class Codigo  
    {   /*Tipo complejo para el código de un producto o servicio.  Se puede incluir un máximo de 5 repeticiones de códigos de producto/servicio. */
        /// <summary>
        /// Esta lista contiene todos los detalles de la factura
        /// </summary>
        [XmlElement(ElementName = "Tipo", Order = 1)]
        public string tipo { set; get; }//tamaño 2  DGT
        [XmlElement(ElementName = "Codigo", Order = 2)]
        public string codigo { set; get; }//tamaño 20  DGT
        public  Codigo() {}

    }
}
