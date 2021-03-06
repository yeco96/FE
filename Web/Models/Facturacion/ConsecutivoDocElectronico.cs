﻿using Class.Seguridad;
using Class.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models.Facturacion
{
    [Table("fact_cosecutivo_doc_electronico")]
    public class ConsecutivoDocElectronico
    {
        public static String DEFAULT_SUCURSAL = "001";
        public static String DEFAULT_CAJA = "00001";
        public static String DEFAULT_DIGITO_VERIFICADOR = "00000000";

        [Key]
        [Required]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(9, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [Display(Name = "Emisor")]
        [Column (Order =1)]
        public string emisor { set; get; }

        [Key]
        [Required]
        [Column(Order = 2)]
        [Display(Name = "Sucursal")]
        [MaxLength(3, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(3, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [RegularExpression("\\d{3}", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")] 
        public string sucursal { set; get; }

        [Key]
        [Required]
        [Column(Order = 3)]
        [MaxLength(5, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(5, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [RegularExpression("\\d{5}", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        [Display(Name = "Caja")]
        public string caja { set; get; }
        
        [Key]
        [Required]
        [Column(Order = 4)]
        [MaxLength(2, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(2, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [RegularExpression("\\d{2}", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        [Display(Name = "Tipo Documento")]
        public string tipoDocumento { set; get; }

        [Required]
        [Display(Name = "Consecutivo")]
        [RegularExpression("^[0-9]{1,10}?$", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        public long consecutivo { set; get; }

        [Required]
        [Display(Name = "Digito Verificador")]
        [MaxLength(8, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [MinLength(8, ErrorMessage = "La propiedad {0} no puede tener menos de {1} elementos")]
        [RegularExpression("\\d{8}", ErrorMessage = "La propiedad {0} solo debe tener valores numéricos")]
        public string digitoVerificador { set; get; }

          
        [MaxLength(10, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Estado")]
        public string estado { set; get; }

        /// <summary>
        /// AUDIOTORIA
        /// </summary>
        [ForeignKey("UsuarioCreacion")]
        public string usuarioCreacion { set; get; }

        public DateTime? fechaCreacion { set; get; }

        [ForeignKey("UsuarioModificacion")]
        public string usuarioModificacion { set; get; }

        public DateTime? fechaModificacion { set; get; }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public ConsecutivoDocElectronico()
        {
        }

        /// <summary>
        /// OBJETOS COMPUESTOS
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }


        public string getClave(string fechaDocumento)
        {
            string tipoEnvio = "";
            if ( fechaDocumento.CompareTo( Date.DateTimeNow().ToString("yyyyMMdd") ) < 0 )
            {
                tipoEnvio = "3";
            }else
            {
                tipoEnvio = "1";
            }
           
            string fecha = Date.DateTimeNow().ToString("ddMMyy");
            //506 080118 000603540974 001 00001 01 0000000018 1 88888888
            return String.Format("506{0}{1}{2}{3}{4}{5}{6}{7}", fecha, this.emisor.PadLeft(12, '0'), this.sucursal, this.caja, this.tipoDocumento, this.consecutivo.ToString().PadLeft(10, '0'), tipoEnvio, this.digitoVerificador);
        }

        /// <summary>
        /// Número de 20 digitos que representa el consecutivo
        /// </summary>
        /// <param name="tipoDocumento">factura, nota credito, nota debito, tiquete</param>
        /// <returns></returns>
        public string getConsecutivo()
        {
            //001 00001 01 0000000018 
            return String.Format("{0}{1}{2}{3}",  this.sucursal, this.caja, this.tipoDocumento, this.consecutivo.ToString().PadLeft(10, '0'));
        }


        public override string ToString()
        {
            return string.Format("Sucursal: {0} - Caja: {1} - Consecutivo: {2}",this.sucursal, caja, consecutivo);
        }


    }
}