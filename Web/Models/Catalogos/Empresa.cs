using Class.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.Catalogos
{
    [Table("conf_empresa")]
    public class Empresa
    {
        public static string IDIOMA_ESPANOL = "ES";
        public static string IDIOMA_INGLES = "EN";

        [Key]
        [Required]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }

        [Required]
        [MaxLength(250, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Descripción")]
        public string descripcion { set; get; }


        [Column(TypeName = "Binary")]
        public byte[] logo { set; get; }
         
        [Required]
        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Idioma")]
        public string idioma { set; get; }

        
        [MaxLength(200, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Leyenda")]
        public string leyenda { set; get; }

        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Condición Venta")]
        public string condicionVenta { set; get; }

        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Medio Pago")]
        public string medioPago { set; get; }
         
        [Display(Name = "Plazo Crédito")]
        public int plazoCredito { set; get; }

        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Moneda")]
        public string moneda { set; get; }


        [MaxLength(15, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Tipo de Impresión")]
        public string tipoImpresion { set; get; }
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
        public Empresa()
        {
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}", this.descripcion, this.codigo);
        }
    }
}