using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class Identificacion
    {
        
        
        
        
        /// <summary>
        /// tamaño 2  DGTEs un campo fijo de dos posiciones.Este campo será de condición obligatoria, cuando se posea
        /// información en el nodo “Número de cédula física/jurídica/NITE/DIMEX emis
        /// </summary>
        [XmlElement(ElementName = "Tipo", Order = 1)]
        public string tipo { set; get; } 




        [XmlElement(ElementName = "Numero", Order = 2)]
        /// <summary>
        ///       tamaño 12  DGT
        /// Número de cédula física/ jurídica/NITE/DIMEX del emisor
        ///El contribuyente debe estar inscrito ante la Administración Tributaria.
        /// campo será de condición obligatoria, cuando se posea información en el nodo “Tipo de identificación del
        ///emisor. Formato: La “Cédula física” debe de contener 9 dígitos, sin cero al inicio y sin guiones La “cédula de personas 
        ///Jurídicas ” debe contener 10 dígitos y sin guiones El “Documento de Identificación Migratorio para 
        ///       Extranjeros(DIMEX)” debe contener 11 o 12 dígitos, sin ceros al inicio y sin guiones El “Documento de Identificación de la 
        ///       DGT(NITE)” debe contener 10 dígitos y sin guiones
        /// </summary>
        public string numero { set; get; }

       




        public Identificacion() {
    }
    }
   
}
