using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models
{
    [Table("xml_condicion_venta")]
    public class CondicionVenta
    {
        [Key]
        [Required]
        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public String codigo { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Descripción")]
        public String descripcion { set; get; }


        [Display(Name = "Usuario Creación")]
        public String usuarioCreacion { set; get; }

        [Display(Name = "Fecha Creación")]
        public String fechaCreacion { set; get; }

        [Display(Name = "Usuario Modificación")]
        public String usuarioModificacion { set; get; }

        [Display(Name = "Fecha Modificación")]
        public String fechaModificacion { set; get; }

        [Display(Name = "Estado")]
        public String estado { set; get; }

    }
}