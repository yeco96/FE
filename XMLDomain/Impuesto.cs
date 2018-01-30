using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
   public  class Impuesto
    {
        /// <summary>
        /// Código del impuesto: 01 Impuesto General sobre las
        /// ventas, 02 Impuesto Selectivo de Consumo, 03 Impuesto único a los combustivos,
        /// 04 Impuesto específico de bebidas alcohólicas, 05 Impuesto específico sobre las
        /// bebidas envasadas sin contenido alcohólico y jabones de tocador, 06 Impuesto a
        /// los productos de tabaco, 07 Servicios, 99 Otros, 08 Impuesto General a las
        /// Ventas Diplomáticos, 09 Impuesto General sobre Ventas compras autorizadas, 10
        /// Impuesto General sobre las ventas instituciones públicas y otros organismos, 11
        /// Impuesto Selectivo de consumo compras autorizadas, 12 Impuesto Especifico al
        /// cemento, 98 Otros
        /// </summary>
        [XmlElement(ElementName = "Codigo", Order = 1)]
        public string codigo { set; get; }//tamaño 2  DGT

        /// <summary>
        /// Tarifa del impuesto
        /// </summary>
        [XmlElement(ElementName = "Tarifa", Order = 2)]
        public decimal tarifa { set; get; }//tamaño 4,2  DGT
         
        /// <summary>
        /// Se obtiene de la multiplicación del campo subtotal por la tarifa del impuesto
        /// </summary>
        [XmlElement(ElementName = "Monto", Order = 3)]
        public decimal monto { set; get; }//tamaño 18,5  DGT

        [XmlElement(ElementName = "Exoneracion", Order = 4)]
        public Exoneracion exoneracion;


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Impuesto(){
            this.exoneracion = new Exoneracion();
        }


        /// <summary>
        /// Este método determina los valores que no tienen datos y los asigna NULL para que no se generen los notos
        /// </summary>
        public void verificaDatosParaXML()
        {
            if (string.IsNullOrWhiteSpace(this.exoneracion.tipoDocumento) &&
                string.IsNullOrWhiteSpace(this.exoneracion.numeroDocumento))
            {
                this.exoneracion = null;
            }

        }


    }
}
