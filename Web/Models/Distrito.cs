using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models
{
    [Table("xml_distrito")]
    public class Distrito
    {
        [Key]
        [Required]
        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "IdDistrito")]
        public string idDistrito { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Descripción")]
        public String descripcion { set; get; }


        [Required]
        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "IdCanton")]
        public string idCanton { set; get; }

        [Required]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "IdProvincia")]
        public string idProvincia { set; get; }



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