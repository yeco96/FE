
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebServices.Seguridad;

namespace WebServices.Models.Catalogos
{
    [Table("xml_tipo_documento")]
    public class TipoDocumento
    {

        [NotMapped]
        public static int ENVIADO = 0;
        [NotMapped]
        public static int ACEPTADO = 1;
        [NotMapped]
        public static int RECHAZADO = 3;
        [NotMapped]
        public static int PENDIENTE = 9;


        [NotMapped]
        public static string FACTURA_ELECTRONICA = "01";
        [NotMapped]
        public static string NOTA_DEBITO = "02";
        [NotMapped]
        public static string NOTA_CREDITO = "03";
        [NotMapped]
        public static string TIQUETE_ELECTRONICO = "04";


        [Key]
        [Required]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Descripción")]
        public string descripcion { set; get; }

        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Descripción Inglés")]
        public string descripcionEN { set; get; }

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
        public TipoDocumento()
        {
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }


    }
}