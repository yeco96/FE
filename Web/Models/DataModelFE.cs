using Class.Seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Models.Catalogos;
using Web.Models.Facturacion;

namespace Web.Models
{
    public class DataModelFE : DbContext
    {


        public DataModelFE() : base("name=fe_db")
        { 
        }

        //public virtual DbSet<Canton> Canton { get; set; }
        public virtual DbSet<CondicionVenta> CondicionVenta { get; set; }
        public virtual DbSet<CodigoPais> CodigoPais { get; set; }
        public virtual DbSet<CodigoReferencia> CodigoReferencia { get; set; }
        public virtual DbSet<ConfiguracionCorreo> ConfiguracionCorreo { get; set; }
        
        //public virtual DbSet<Distrito> Distrito { get; set; }
        public virtual DbSet<ConsecutivoDocElectronico> ConsecutivoDocElectronico { get; set; } 
        public virtual DbSet<Exoneracion> Exoneracion { get; set; }
        public virtual DbSet<MedioPago> MedioPago { get; set; }
        //public virtual DbSet<Provincia> Provincia { get; set; }

        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<ProductoImpuesto> ProductoImpuesto { get; set; }
        

        public virtual DbSet<TipoProductoServicio> TipoProductoServicio { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificacion { get; set; }
        public virtual DbSet<TipoImpuesto> TipoImpuesto { get; set; }
        public virtual DbSet<TipoMoneda> TipoMoneda { get; set; }

        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }


        public virtual DbSet<EmisorReceptorIMEC> EmisorReceptorIMEC { get; set; }


         

        #region singletonInstance
        /// <summary>
        /// Al crear una sola instancia de los objetos se evita tener en memoria una gran cantidad objetos, con lo cual se reduce el consumo de recursos. Además se puede mantener un mayor control sobre el número de objetos creados.
        /// Ofrece una solución concreta a un problema, favoreciendo la reutilización de código y la comunicación entre los procesos de la aplicación.
        /// </summary>
        private static DataModelFE singletonInstance = null;
        public static DataModelFE GetInstance()
        {
            if (singletonInstance == null)
                singletonInstance = new DataModelFE();

            return singletonInstance;
        }
        #endregion

    }
}