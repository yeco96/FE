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
        public string emisorNombreComercial { set; get; }
        public string emisorTelefonos { set; get; }
        public string emisorDireccion { set; get; }
        public string emisorIdentificacionCorreo { set; get; }

        public string receptorIdentificacion { set; get; } 
        public string receptorNombre { set; get; }
        public string receptorIdentificacionCorreo { set; get; }
                

        public string clave { set; get; }
        public string consecutivo { set; get; }

        public string CondicionVenta { set; get; }
        public string MedioPago { set; get; }
        public DateTime fecha { set; get; }
        public string leyenda { set; get; }
        public string moneda { set; get; }
        public string tipoCambio { set; get; }
        public string tipoDocumento { set; get; }

        public List<ImpresionDetalle> detalles { set; get; }


        public decimal montoSubTotal { set; get; }
        public decimal montoDescuento { set; get; }
        public decimal montoImpuestoVenta { set; get; }
        public decimal montoTotal { set; get; }
        public string Normativa { set; get; }

        public Impresion()
        {
            this.detalles = new List<ImpresionDetalle>();
            iniciarParametros();
        }
         

        public void iniciarParametros()
        {
            this.emisorNombre = "MAIKOL JESUS SALAMANCA ARIAS";
            this.emisorIdentificacion = "603540974";
            this.emisorNombreComercial = "MSA SOFT";
            this.emisorIdentificacionCorreo= "jupmasalamanca@gmail.com";
            this.emisorTelefonos = "506 24402090";
            this.emisorDireccion = "HEREDIA, HEREDIA, HEREDIA, 125 norte dela biblioteca publica";
            
            this.receptorNombre = "Andrea Santamaria Quesada";
            this.receptorIdentificacion= "207550498";
            this.receptorIdentificacionCorreo= "jandreasantamariaquesada@gmail.com";

            this.clave = "50608011800060354097400100001010000000038188888888";
            this.consecutivo = "00100001010000000038";
            this.fecha = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "-06:00");
            this.leyenda = "Acá va una leyenda";
            this.moneda = "CRC";
            this.tipoCambio = "1.0000";
            this.tipoDocumento = "Factura Electrónica";
            this.CondicionVenta = "Crédito";
            this.MedioPago = "Efectivo";
            
            ImpresionDetalle lineaDetalle = new ImpresionDetalle();
            lineaDetalle.codigo = "1";
            lineaDetalle.descripcion= "Arti,culo 1";
            lineaDetalle.cantidad = 1;
            lineaDetalle.monto = 100000;
            detalles.Add(lineaDetalle);

            ImpresionDetalle lineaDetalle1 = new ImpresionDetalle();
            lineaDetalle1.codigo = "2";
            lineaDetalle1.descripcion = "Articulo 2";
            lineaDetalle1.cantidad = 2;
            lineaDetalle1.monto = 200000;
            detalles.Add(lineaDetalle1);

            ImpresionDetalle lineaDetalle2 = new ImpresionDetalle();
            lineaDetalle2.codigo = "3";
            lineaDetalle2.descripcion = "Articulo 3";
            lineaDetalle2.cantidad = 3;
            lineaDetalle2.monto = 300000;

            detalles.Add(lineaDetalle2);


            this.montoSubTotal = 600000;
            this.montoDescuento = 0;
            this.montoImpuestoVenta = 78000;
            this.montoTotal = 522000;

            this.Normativa = "Autorizada mediante resolución No DGT-R-48-2016 del 7 de Octubre de 2016";
        }
    }
}
