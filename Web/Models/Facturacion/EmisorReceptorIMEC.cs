
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
 
    [Table("fact_emisor_receptor")]
    public class EmisorReceptorIMEC
    {
      
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public string identificacionTipo { set; get; }

        [Key]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(9, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        public string identificacion { set; get; }

        [MaxLength(80, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public string nombre { set; get; } 
         

        [MaxLength(80, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public string nombreComercial { set; get; }



        [MaxLength(1, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public string provincia { set; get; }

        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(2, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [RegularExpression("\\d{2}", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        public string canton { set; get; }

        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(2, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [RegularExpression("\\d{2}", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        public string distrito { set; get; }

        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(2, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [RegularExpression("\\d{2}", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        public string barrio { set; get; }

        [MaxLength(160, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public string otraSena { set; get; }
         

        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [RegularExpression("^[0-9]{1,3}?$", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        public string telefonoCodigoPais { set; get; }

        [MaxLength(20, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [RegularExpression("^[0-9]{1,20}?$", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        public string telefono { set; get; }

        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [RegularExpression("^[0-9]{1,3}?$", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        public string faxCodigoPais { set; get; }

        [MaxLength(20, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [RegularExpression("^[0-9]{1,20}?$", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        public string fax { set; get; }

        [MaxLength(60, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [RegularExpression(@"\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*", ErrorMessage = "La propiedad {0} debe tener una dirección de correo valida, ejemplo: cuenta@dominio.com")]
        public string correoElectronico { set; get; }


        [Column(TypeName = "Binary")]
        public byte[] llaveCriptografica { set; get; }

        [MaxLength(4, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(4, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [RegularExpression("\\d{4}", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")] 
        public string claveLlaveCriptografica { set; get; }

        [MaxLength(100, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public string usernameOAuth2 { set; get; }

        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        public string passwordOAuth2 { set; get; }
        

        /// <summary>
        /// AUDIOTORIA
        /// </summary>
        public string estado { set; get; }

        [ForeignKey("UsuarioCreacion")]
        public string usuarioCreacion { set; get; }

        public DateTime? fechaCreacion { set; get; }

        [ForeignKey("UsuarioModificacion")]
        public string usuarioModificacion { set; get; }

        public DateTime? fechaModificacion { set; get; }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public EmisorReceptorIMEC()
        {
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }



    }
}