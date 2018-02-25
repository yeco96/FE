using Class.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Web.Models.Facturacion;

namespace Web.Models.Catalogos
{
    [Table("fact_producto")]
    public class Producto
    {
        [NotMapped]
        public static string TIPO_SERVICIO = "SE";
        [NotMapped]
        public static string TIPO_MERCANCIA = "ME";

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public virtual int id { set; get; }

        [Required]
        [Column("codigo", Order = 1)]
        [MaxLength(20, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }
         
        [Required]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Tipo")]
        public string tipo { set; get; }

        [Required]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Carga en Factura")]
        public string cargaAutFactura { set; get; }


        [Required]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Tipo Servicio / Mercancias")]
        public string tipoServMerc { set; get; }
        
             
        [Required]
        [MaxLength(10, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Unidad Medida")]
        [ForeignKey("UnidadMedida")]
        public string unidadMedida { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Descripción")]
        public string descripcion { set; get; }

        [Required]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Emisor")]

        [ForeignKey("Emisor")]
        [Column("emisor", Order = 2)]
        public string emisor { set; get; }

        public decimal precio { set; get; }

        


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
        public Producto()
        {
            this.id = 0;
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }
        public virtual EmisorReceptorIMEC Emisor { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }


        public override string ToString()
        {
            return String.Format("{0} - {1}",this.descripcion,this.precio.ToString("n2"));
        }

    }
}