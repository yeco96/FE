using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Seguridad
{
    [Table("security_user")]
    public class Usuario 
    {
        [Key]
        [Required]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }

       
        [Required]
        [MaxLength(100, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Nombre")]
        public String nombre { set; get; }

        [Required]
        [MaxLength(100, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Contraseña")]
        public String contrasena { set; get; }

        [Required] 
        [Display(Name = "Intentos")]
        public int intentos { set; get; }

        [Required]
        [MaxLength(5, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Rol")]
        [ForeignKey("Rol")]
        public string rol { set; get; }


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

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.nombre, this.codigo);
        }

        /// <summary>
        /// OBJETOS COMPUESTOS x
        /// </summary>
        public virtual Rol Rol { get; set; }
    }
}
