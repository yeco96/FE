using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    [XmlRoot(ElementName = "InformacionReferencia")]
    public  class InformacionReferencia

    {

        /// <summary>
        /// Tipo de documento de referencia. 01 Factura electrónica, 02 Nota de débito electrónica, 03 nota de crédito electrónica, 04
        /// Tiquete electrónico, 05 Nota de despacho, 06 Contrato, 07 Procedimiento, 08 Comprobante emitido en contigencia, 99 Otros
        /// </summary>
        [XmlElement(ElementName = "TipoDoc", Order = 1)]
        public string tipoDocumento { set; get; }//tamaño 2 DGT

        /// <summary>
        /// Número de documento de referencia
        /// </summary>
        [XmlElement(ElementName = "Numero", Order = 2)]
        public string numero { set; get; }//tamaño 50 DGT

        [XmlElement(ElementName = "FechaEmision", Order = 3)]
        public string fechaEmision { set; get; }

        [XmlIgnore]
        public string fechaEmisionTotal { set; get; }
        

        /// <summary>
        /// Código de referencia. 01 Anula documento de referencia, 02 Corrige texto de documento de referencia, 03 Corrige monto, 04
        /// Referencia a otro documento, 05 Sustituye comprobante provisional por contigencia, 99 Otros
        /// </summary>
        [XmlElement(ElementName = "Codigo", Order = 4)]
        public string codigo { set; get; }//tamaño 2 DGT

        [XmlElement(ElementName = "Razon", Order = 5)]
        public string razon { set; get; }//tamaño 180 DGT


        public InformacionReferencia()
        {
        }
    }
}
