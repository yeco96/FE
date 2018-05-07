using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace XMLDomain
{
    
    public class ProformaElectronica : DocumentoElectronico
    {  
        
        public ProformaElectronica()
        {
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            this.detalleServicio = new DetalleServicio();
            this.resumenFactura= new ResumenFactura();
            this.normativa = new Normativa();
            this.informacionReferencia = new List<InformacionReferencia>();
            this.otros = new Otros();
        }

     

    }
}
