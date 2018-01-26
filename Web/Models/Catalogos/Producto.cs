﻿using System;
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
        [Key]
        [Required]
        [MaxLength(20, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }
         
        [Required]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Tipo")]
        public string tipo { set; get; }

        
        [Required]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
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
        public string emisor { set; get; }

        public double precio { set; get; }


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
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }
        public virtual EmisorReceptor Emisor { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }
        


    }
}