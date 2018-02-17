using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    public class LineaDetalle
    {
        [XmlElement(ElementName = "NumeroLinea", Order = 1)]
        public int numeroLinea { set; get; }
        [XmlElement(ElementName = "Codigo", Order = 2)]
        public Codigo codigo { set; get; }
        [XmlElement(ElementName = "Cantidad", Order = 3)]
        public decimal cantidad { set; get; }//tamaño 16,3 DGT
        [XmlElement(ElementName = "UnidadMedida", Order = 4)]
        public string unidadMedida { set; get; }//tamaño 15 DGT
        [XmlElement(ElementName = "UnidadMedidaComercial", Order = 5)]
        public string unidadMedidaComercial { set; get; }//tamaño 15 DGT
        [XmlElement(ElementName = "Detalle", Order = 6)]
        public string detalle { set; get; }//tamaño 160 DGT
        [XmlElement(ElementName = "PrecioUnitario", Order = 7)]
        public decimal precioUnitario { set; get; }//tamaño 18,3 DGT

        /// <summary>
        /// Se obtiene de multiplicar el campo cantidad por el campo precio unitario tamaño 18,3 DGT 
        /// </summary>
        [XmlElement(ElementName = "MontoTotal", Order = 8)]
        public decimal montoTotal { set; get; }

        /// <summary>
        /// Monto de descuento concedido, el cual es obligatorio si existe descuento
        /// </summary>
        [XmlElement(ElementName = "MontoDescuento", Order = 9)]
        public decimal montoDescuento { set; get; }//tamaño 18,3 DGT

        /// <summary>
        ///  Monto de descuento concedido, el cual es obligatorio si existe descuento //tamaño 80 DGT
        /// </summary>
        [XmlElement(ElementName = "NaturalezaDescuento", Order = 10)]
        public string naturalezaDescuento { set; get; }

        /// <summary>
        /// Se obtiene de la resta del campo monto total menos monto de descuento concedido //tamaño 18,3 DGT
        /// </summary>
        [XmlElement(ElementName = "SubTotal", Order = 11)]
        public decimal subTotal { set; get; }

        /// <summary>
        /// Cuando el producto o servicio este gravado con algúnimpuesto se debe indicar cada uno de ellos.  //tamaño 18,3 DGT
        /// </summary>
        [XmlElement(ElementName = "Impuesto", Order = 12)]
        public List<Impuesto> impuestos { set; get; }


        /// <summary>
        /// Se obtiene de la suma de los campos subtotal más monto de los impuestos //tamaño 18,3 DGT
        /// </summary>
        [XmlElement(ElementName = "MontoTotalLinea", Order = 13)]
        public decimal montoTotalLinea { set; get; }

        [XmlIgnoreAttribute]
        public string producto { set; get; }

        [XmlIgnoreAttribute]
        public string tipoServMerc { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public LineaDetalle()
        {
            this.codigo = new Codigo();
            this.impuestos = new List<Impuesto>();
            this.montoTotal = 0;
            this.montoDescuento = 0;
            this.precioUnitario = 0;
            this.montoTotalLinea = 0;
        }

        /// <summary>
        /// Este metono realziar los calculos de los atributos montoTotal, subTotal, montoTotalLinea, requiere de que se asignen: precioUnitario y cantidad
        /// </summary>
        public void calcularMontos()
        {
            this.montoTotal = this.precioUnitario * this.cantidad;
            this.subTotal = this.montoTotal - this.montoDescuento;

            if (this.impuestos != null)
            {
                this.montoTotalLinea = this.subTotal + this.impuestos.Sum(x => x.monto);
            }
            else
            {
                this.montoTotalLinea = this.subTotal;
            }

            if (this.montoDescuento < 0)
            {
                this.naturalezaDescuento = null;
            }

        }


        /// <summary>
        /// Este método determina los valores que no tienen datos y los asigna NULL para que no se generen los notos
        /// </summary>
        public void verificaDatosParaXML()
        {
            if (this.impuestos != null)
            {
                if (this.impuestos.Count == 0)
                {
                    this.impuestos = null;
                }
                else
                {
                    foreach (var item in this.impuestos)
                    {
                        item.verificaDatosParaXML();
                    }
                }
            }
        }
    }
}
