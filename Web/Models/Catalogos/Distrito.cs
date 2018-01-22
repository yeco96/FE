using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.Catalogos
{
    [Table("xml_distrito")]
    public class Distrito
    {
        [Key]
        [Required]
        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Descripción")]
        public String descripcion { set; get; }


        [Required]
        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Canton")]
        public string canton { set; get; }

        [Required]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "provincia")]
        public string provincia { set; get; }

         

    }
}