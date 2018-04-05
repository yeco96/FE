using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.Facturacion
{
    [Table("fact_supervisor")]
    public class Supervisor
    {

        [Key]
        [Column(Order = 1)]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(1, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        public string supervisor { set; get; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(1, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        public string emisor { set; get; }
    }
}