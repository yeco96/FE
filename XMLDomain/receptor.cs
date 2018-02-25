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

        [XmlElement(ElementName = "IdentificacionExtranjero", Order = 3)]
        public string identificacionExtranjero { set; get; }
         

        [XmlElement(ElementName = "NombreComercial", Order = 4)]
        public string nombreComercial { set; get; }//tamaño 80 DGT

        [XmlElement(ElementName = "Ubicacion", Order = 5)]
        public Ubicacion ubicacion { set; get; }

        [XmlElement(ElementName = "Telefono", Order = 6)]
        public Telefono telefono { set; get; }


        [XmlElement(ElementName = "Fax", Order = 7)]
        public Fax fax { set; get; }

        [XmlElement(ElementName = "CorreoElectronico", Order = 8)]
        public string correoElectronico { set; get; }//tamaño 60  DGT

        public Receptor() {
            this.identificacion = new Identificacion();
            this.ubicacion = new Ubicacion();
            this.telefono = new Telefono();
            this.fax = new Fax();
        }
        
        public void verificar()
        {

            if (identificacion != null) {
                if (string.IsNullOrWhiteSpace(identificacion.tipo))
                {
                    identificacion.tipo = null;
                }
                if (string.IsNullOrWhiteSpace(identificacion.numero))
                {
                    identificacion.numero = null;
                }
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                nombre = null;
            }


            if (string.IsNullOrWhiteSpace(nombreComercial))
            {
                nombreComercial = null;
            }

            if (telefono != null)
            {
                if (string.IsNullOrWhiteSpace(telefono.codigoPais))
                {
                    telefono.codigoPais = null;
                }

                if (string.IsNullOrWhiteSpace(telefono.numTelefono))
                {
                    telefono.numTelefono = null;
                }
            }


            if (fax != null)
            {
                if (string.IsNullOrWhiteSpace(fax.codigoPais))
                {
                    fax.codigoPais = null;
                }

                if (string.IsNullOrWhiteSpace(fax.numTelefono))
                {
                    fax.numTelefono = null;
                }
            }

            if (string.IsNullOrWhiteSpace(correoElectronico))
            {
                correoElectronico = null;
            }




            if (ubicacion != null)
            {
                if (string.IsNullOrWhiteSpace(ubicacion.provincia))
                {
                    ubicacion.provincia = null;
                }

                if (string.IsNullOrWhiteSpace(ubicacion.canton))
                {
                    ubicacion.canton = null;
                }
                if (string.IsNullOrWhiteSpace(ubicacion.distrito))
                {
                    ubicacion.distrito = null;
                }

                if (string.IsNullOrWhiteSpace(ubicacion.barrio))
                {
                    ubicacion.barrio = null;
                }

                if (string.IsNullOrWhiteSpace(ubicacion.otrassenas))
                {
                    ubicacion.otrassenas = null;
                }

            } 

        }

    }
}
