using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServices.Models.Facturacion
{
    [Table("emisor_receptor")]
    public class EmisorReceptor
    {

        [Column("identificacionTipo")]
        [JsonProperty("tipoIdentificacion", Order = 1)]
        public string tipoIdentificacion { set; get; }

        [Key]
        [Column ("identificacion")]
        [JsonProperty("numeroIdentificacion", Order = 2)]
        public string numeroIdentificacion { set; get; }

        // [JsonProperty("nombre")]
        [JsonIgnore]
        [Column("nombre")]
        public string nombre { set; get; }

        // [JsonProperty("nombreComercial")]
        [JsonIgnore]
        [Column("nombreComercial")]
        public string nombreComercial { set; get; }


        [JsonIgnore]
        [NotMapped]
        [Column("nombreCompleto")]
        public string nombreCompleto { get { return this.ToString(); } }


        public EmisorReceptor() { }

        public override string ToString()
        {
            return String.Format("{0} - {1} - {2}", tipoIdentificacion, numeroIdentificacion, nombre);
        }

    }
}
