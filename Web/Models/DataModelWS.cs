using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Models.Catalogos;
using Web.Models.Facturacion;

namespace Web.Models
{
    public class DataModelWS : DbContext
    {


        public DataModelWS() : base("name=fe_db")
        { 
        }
        
        public virtual DbSet<WSDomain.WSRecepcionPOST> WSRecepcionPOST { get; set; }
    }
}