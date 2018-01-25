using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDomain
{
    [Table("fact_emisor_receptor")]
    public class Emisor
    {
        [Key]
        [Column("identificacionTipo", Order = 1)]
        [JsonProperty("tipoIdentificacion", Order = 1)]
        public string tipoIdentificacion { set; get; }

        [Key]
        [Column("identificacion", Order = 2)]
        [JsonProperty("numeroIdentificacion", Order = 2)]
        public string numeroIdentificacion { set; get; }

       // [JsonProperty("nombre")]
       // public string nombre { set; get; }

        public Emisor() { }
    }
}
