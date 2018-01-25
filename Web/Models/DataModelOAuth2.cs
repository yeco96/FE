using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Models.Catalogos;
using Web.Models.Facturacion; 

namespace Web.Models
{
    public class DataModelOAuth2 : DbContext
    {


        public DataModelOAuth2() : base("name=fe_db")
        { 
        }

        public virtual DbSet<OAuth2.OAuth2Config> OAuth2Config { get; set; }
        
    }
}