using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Web.Models.Facturacion;

namespace Web.Models.Catalogos
{
    [Table("fact_producto_impuesto")]
    public class ProductoImpuesto
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        [Column(Order = 1)]
        [ForeignKey("Producto")]
        public long idProducto { set; get; }

        [Key]
        [Required]
        [Column(Order = 2)]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(2, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [Display(Name = "Tipo Impuesto")]
        [ForeignKey("TipoImpuesto")]
        public string tipoImpuesto { set; get; }
         
           
        [Required] 
        [Display(Name = "Procentaje")]
        public decimal porcentaje { set; get; }

        [Required]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Emisor")]
        [ForeignKey("Emisor")]
        public string emisor { set; get; }
         
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
        public ProductoImpuesto()
        {
            this.idProducto = 0;
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }
        public virtual EmisorReceptorIMEC Emisor { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual TipoImpuesto TipoImpuesto { get; set; }


        public override string ToString()
        {
            return String.Format("{0} - {1}", this.TipoImpuesto.descripcion, this.porcentaje);
        }

    }
}