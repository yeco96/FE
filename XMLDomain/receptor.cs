using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class Receptor
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlElement(ElementName = "Nombre", Order = 1)]
        public string nombre { set; get; }//tamaño 2  DGT

        [XmlElement(ElementName = "Identificacion", Order = 2)]
        public Identificacion identificacion { set; get; }

        [XmlElement(ElementName = "NombreComercial", Order = 3)]
        public string nombreComercial { set; get; }//tamaño 80 DGT

        [XmlElement(ElementName = "Ubicacion", Order = 4)]
        public Ubicacion ubicacion { set; get; }

        [XmlElement(ElementName = "Telefono", Order = 5)]
        public Telefono telefono { set; get; }


        [XmlElement(ElementName = "Fax", Order = 6)]
        public Fax fax { set; get; }

        [XmlElement(ElementName = "CorreoElectronico", Order = 7)]
        public string correoElectronico { set; get; }//tamaño 60  DGT

        public Receptor() {
            this.identificacion = new Identificacion();
            this.ubicacion = new Ubicacion();
            this.telefono = new Telefono();
            this.fax = new Fax();
        }
    }
}
