using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XMLDomain
{


    [Table("ws_resumen_factura")]
    public class ResumenFactura
    {

        [Key]
        [XmlIgnore]
        public string clave { set; get; }

        [XmlIgnore]
        public string consecutivo { get { return clave.Substring(21, 20); } }
        [XmlIgnore]
        public string tipoDocumento { get { return clave.Substring(29, 2); } }


        [XmlElement(ElementName = "CodigoMoneda", Order = 1)]
        public string codigoMoneda { set; get; }//tamaño 3  DGT
        [XmlElement(ElementName = "TipoCambio", Order = 2)]
        public decimal   tipoCambio { set; get; }//tamaño 18,5  DGT


        /// <summary>
        /// Total de los servicios gravados con IV
        /// </summary>
        [XmlElement(ElementName = "TotalServGravados", Order = 3)]
        public decimal   totalServGravados { set; get; }//tamaño 18,5  DGT

        /// <summary>
        /// Total de los servicios exentos de IV
        /// </summary>
        [XmlElement(ElementName = "TotalServExentos", Order = 4)]
        public decimal   totalServExentos { set; get; }//tamaño 18,5  DGT

        /// <summary>
        /// Total mercancias gravadas con IV
        /// </summary>
        [XmlElement(ElementName = "TotalMercanciasGravadas", Order = 5)]
        public decimal   totalMercanciasGravadas { set; get; }//tamaño 18,5  DGT

        /// <summary>
        /// Total mercancias exentas de IV
        /// </summary>
        [XmlElement(ElementName = "TotalMercanciasExentas", Order = 6)]
        public decimal   totalMercanciasExentas { set; get; }//tamaño 18,5  DGT

        /// <summary>
        /// Total gravado. se obtiene de la suma del total servicios gravados con IV + total mercancias gravadas con IV
        /// </summary>
        [XmlElement(ElementName = "TotalGravado", Order = 7)]
        public decimal   totalGravado { set; get; }//tamaño 18,5  DGT


        /// <summary>
        /// Total Exento, se obtiene de la suma de los campos total servicios exentos IV mas total mercancias exentas IV
        /// </summary>
        [XmlElement(ElementName = "TotalExento", Order = 8)]
        public decimal   totalExento { set; get; }//tamaño 18,5  DGT


        /// <summary>
        ///Se obtiene de la suma de los campos total gravado más total exento
        /// </summary>
        [XmlElement(ElementName = "TotalVenta", Order = 9)]
        public decimal   totalVenta { set; get; }//tamaño 18,5  DGT

        /// <summary>
        /// Se obtiene de la suma de todos los campo de monto de descuento concedido
        /// </summary>
        [XmlElement(ElementName = "TotalDescuentos", Order = 10)]
        public decimal   totalDescuentos { set; get; }//tamaño 18,5  DGT

        /// <summary>
        /// Se obtiene de la resta de los campos total venta menos total descuento
        /// </summary>
        [XmlElement(ElementName = "TotalVentaNeta", Order = 11)]
        public decimal   totalVentaNeta { set; get; }//tamaño 18,5  DGT

        /// <summary>
        /// Se obtiene de la suma de todos campos monto del impuesto
        /// </summary>
        [XmlElement(ElementName = "TotalImpuesto", Order = 12)]
        public decimal   totalImpuesto { set; get; }//tamaño 18,5  DGT

        /// <summary>
        /// Se obtiene de la suma de los campos total venta neta más monto total de los impuestos
        /// </summary>
        [XmlElement(ElementName = "TotalComprobante", Order = 13)]
        public decimal   totalComprobante { set; get; }//tamaño 18,5  DGT


        public ResumenFactura() {
            this.tipoCambio = 1;
        }


        /// <summary>
        /// Realiza el calculo de todos los montos que resumen la factura
        /// </summary>
        /// <param name="lineaDetalle"></param>
        public void calcularResumenFactura(List<LineaDetalle> lineaDetalle)
        {
            totalImpuesto = 0;
            totalServExentos = 0;
            totalServGravados = 0;
            totalMercanciasGravadas = 0;
            totalMercanciasExentas = 0;

            foreach (var linea in lineaDetalle)
            {
                 
                if (linea.impuestos != null)
                {
                    foreach (var impuesto in linea.impuestos)
                    {
                        totalImpuesto += impuesto.monto;
                    }
                    //con IV
                    if(linea.tipoServMerc.Equals("SE"))
                        totalServGravados += linea.montoTotal;
                    else
                        totalMercanciasGravadas += linea.montoTotal;
                }
                else//sin IV
                {
                    if (linea.tipoServMerc.Equals("SE"))
                        totalServExentos += linea.montoTotal;
                    else
                        totalMercanciasGravadas += linea.montoTotal;
                }
                totalDescuentos += linea.montoDescuento;

            }
            totalGravado = totalServGravados + totalMercanciasGravadas;
            totalExento = totalServExentos + totalMercanciasExentas;

            totalVenta = totalGravado + totalExento;
            totalVentaNeta = totalVenta - totalDescuentos;
            totalComprobante = totalVentaNeta + totalImpuesto;
        }
    }
}
