using Class.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Web.Models.Facturacion;

namespace Web.Models.Administracion
{
    [Table("conf_plan")]
    public class Plan
    {
          
        [Key]
        [MinLength(9, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "emisor")]
        public string emisor { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Plan")]
        public string plan { set; get; }
         
        [Required]
        [Display(Name = "Cantidad Documentos Plan")]
        public int cantidadDocPlan { set; get; }
         
       
        [Display(Name = "Cantidad Documentos Emitidos")]
        public int cantidadDocEmitido { set; get; }
        
             
       
        public DateTime? fechaInicio { set; get; }
        public DateTime? fechaFin { set; get; }


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
        public Plan()
        {
            this.cantidadDocPlan = 0;
            this.cantidadDocEmitido = 0;
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}",this.emisor, this.plan);
        }

    }
}