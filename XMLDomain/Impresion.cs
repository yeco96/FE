using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLDomain
{
    public class Impresion
    {
        public string emisorIdentificacion { set; get; }
        public string emisorNombre { set; get; }

          
        public string receptorIdentificacion { set; get; } 
        public string receptorNombre { set; get; }
        public string receptorIdentificacionCorreo { set; get; }

          
        public string clave { set; get; }
        public string consecutivo { set; get; }
        public DateTime fecha { set; get; }
        public string leyenda { set; get; }
        public string moneda { set; get; }
        public string tipoCambio { set; get; }
        public string tipoDocumento { set; get; }


        public List<ImpresionDetalle> detalles;


        public decimal montoSubTotal { set; get; }
        public decimal montoDescuento { set; get; }
        public decimal montoImpuestoVenta { set; get; }
        public decimal montoTotal { set; get; }

        public Impresion()
        {
            this.detalles = new List<ImpresionDetalle>();
        }
         


    }
}
