using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
  public  class Normativa
    {
        [XmlElement(ElementName = "NumeroResolucion", Order = 1)]
        public string numeroResolucion { set; get; }//tamaño 13 DGT
        [XmlElement(ElementName = "FechaResolucion", Order = 2)]
        public string fechaResolucion { set; get; }//tamaño 20 DGT
        /// <summary>
        /// La fecha con formato (DD-MM-AAAA) La hora con formato (HH:MM:SS) 
        /// </summary>
        public Normativa(){
            this.numeroResolucion = "DGT-R-48-2016";
            this.fechaResolucion = "07-10-2016 08:00:00";
        }
    }
}
