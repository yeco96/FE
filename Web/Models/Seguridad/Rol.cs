using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Seguridad
{
    [Table("security_rol")]
    public class Rol 
    {  
        public static String PROFESOR = "PROFESOR";
        public static String ASISTENTE = "ASISTENTE";
        public static String ADMINISTRADOR = "ADMINISTRADOR";
        public static String ESTUDIANTE = "ESTUDIANTE";
        public static String MATRICULA = "MATRICULA";
        public static String CONSULTA = "CONSULTA";
        public static String REGISTRO = "REGISTRO";
        public static String CAJERO = "CAJERO";
        public static String COORDINADOR = "COORDINADOR";
     
        [Key]
        [Required]
        [MaxLength(5, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }
         
        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Descripcion")]
        public String descripcion { set; get; }

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
        public Rol()
        {
        }

        /// <summary>
        /// OBJETOS COMPUESTOS x
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }
    }
}
