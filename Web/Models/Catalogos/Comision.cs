using Class.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Web.Models.Catalogos
{
    [Table("comision")]
    public class Comision
    {
        public Comision()
        {
        }
        [Key]
        public int consecutivo { set; get; }
        public string cliente { set; get; }
        public string plan { set; get; }
        public DateTime fechaPago { set; get; }
        public double montoPago { set; get; }
        [NotMapped]
        public double comision1 { get { return montoPago * 0.25; } }
        [NotMapped]
        public double comision2 { get { return montoPago * 0.25; } }
    }
}