﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLDomain
{
    //[XmlRoot(ElementName = "NotaDebitoElectronica", Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4.2/notaDebitoElectronica")]
    public class NotaDebitoElectronica : DocumentoElectronico
    {
        

        public NotaDebitoElectronica()
        {
            this.emisor = new Emisor();
            this.receptor = new Receptor();
            this.detalleServicio = new DetalleServicio();
            this.resumenFactura = new ResumenFactura();
            this.informacionReferencia = new List<InformacionReferencia>();
            this.normativa = new Normativa();
            this.otros = new Otros();
        }

         
    }
}
