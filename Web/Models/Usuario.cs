using Class.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models
{
    [Table("security_user")]
    public class Usuario 
    {
        [Key]
        [Required]
        [MaxLength(15, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Id")]
        public string id { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Nombre")]
        public string nombre { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Apellidos")]
        public string apellido { set; get; }

        public string nombreCompleto { get { return ToString(); } }


        /// <summary>
        /// AUDIOTORIA
        /// </summary>
        public string estado { set; get; }
         
        public string usuarioCreacion { set; get; }
         
        public DateTime? fechaCreacion { set; get; }
         
        public string usuarioModificacion { set; get; }
         
        public DateTime? fechaModificacion { set; get; }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Usuario()
        { 
        }


        /// <summary>
        /// SOBRE ESCRIEBE EL METODO STRING
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1} - {2}",this.apellido,this.nombre,this.id);
        }


        // EJEMPLO DE ANOTACION DE VALIDACION http://www.puntonetalpunto.net/2013/10/validaciones-con-dataannotations.html
    }


}