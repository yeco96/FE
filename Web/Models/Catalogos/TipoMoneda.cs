using Class.Seguridad;
using Class.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.Catalogos
{
    [Table("xml_tipo_moneda")]
    public class TipoMoneda 
    {
        public static string CRC = "CRC";

        [Key]
        [Required]
        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Descripción")]
        public string descripcion { set; get; }

         
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
        public TipoMoneda()
        { 
        }
        
        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }


        // EJEMPLO DE ANOTACION DE VALIDACION http://www.puntonetalpunto.net/2013/10/validaciones-con-dataannotations.html
    }


}