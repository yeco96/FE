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
        public Int32 codigo { set; get; }

       
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
        [Display(Name = "Rol")]
        [ForeignKey("Rol")]
        public int rol { set; get; }


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
        public Usuario()
        {
        }

        /// <summary>
        /// OBJETOS COMPUESTOS x
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
