
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.Models.Facturacion
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    [Table("fact_emisor_receptor")]
    public class EmisorReceptor
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String identificacionTipo { set; get; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String identificacion { set; get; }

        [MaxLength(80, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String nombre { set; get; } 


        [MaxLength(80, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String nombreComercial { set; get; }



        [MaxLength(1, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String provincia { set; get; }

        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String canton { set; get; }

        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String distrito { set; get; }

        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String barrio { set; get; }

        [MaxLength(160, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String otraSena { set; get; }

        [MaxLength(16), Column(TypeName = "Binary")]
        public byte[] llaveCriptografica { set; get; }


        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String telefonoCodigoPais { set; get; }

        [MaxLength(20, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String telefono { set; get; }

        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String faxCodigoPais { set; get; }

        [MaxLength(20, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public String fax { set; get; }

        [MaxLength(60, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public string correoElectronico { set; get; }


        /// <summary>
        /// AUDIOTORIA
        /// </summary>
        public String estado { set; get; }

        public String usuarioCreacion { set; get; }

        public DateTime? fechaCreacion { set; get; }

        public String usuarioModificacion { set; get; }

        public DateTime? fechaModificacion { set; get; }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
    }
}