using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Web.Models.Facturacion;

namespace Web.Models.Catalogos
{
    [Table("config_mail")]
    public class ConfiguracionCorreo
    { 
        
        [Key]
        [Required]
        [Column("codigo", Order = 1)]
        [MaxLength(20, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }
         
        [Required]
        [MaxLength(25, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Servidor")]
        public string host { set; get; }

        
        [Required]
        [MaxLength(4, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Puerto")] 
        public string port { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Usuario")]
        public string user { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Contraseña")]  
        public string password { set; get; }

        
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
        public ConfiguracionCorreo()
        { 
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }
     
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.codigo;
        }

    }
}