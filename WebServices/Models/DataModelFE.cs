using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using WebServices.Models.Administracion;
using WebServices.Models.Catalogos;
using WebServices.Models.Configuracion;
using WebServices.Models.Facturacion;
using WebServices.Seguridad;
using XMLDomain;

namespace WebServices.Models
{
    public class DataModelFE : DbContext
    {


        public DataModelFE() : base("name=fe_db")
        {
        }

        public virtual DbSet<EmisorReceptorIMEC> EmisorReceptorIMEC { get; set; }
        public virtual DbSet<OAuth2.OAuth2Config> OAuth2Config { get; set; }
        public virtual DbSet<EmisorReceptor> EmisorReceptor { get; set; }
        public virtual DbSet<WebServices.Models.WS.WSRecepcionPOST> WSRecepcionPOST { get; set; }
        public virtual DbSet<ResumenFactura> ResumenFactura { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }


        /*

        //public virtual DbSet<Cantón> Cantón { get; set; }
        public virtual DbSet<CondicionVenta> CondicionVenta { get; set; }
        public virtual DbSet<CodigoPais> CodigoPais { get; set; }
        public virtual DbSet<CodigoReferencia> CodigoReferencia { get; set; }
        public virtual DbSet<ConfiguracionCorreo> ConfiguracionCorreo { get; set; }
        public virtual DbSet<ConfiguracionGlobal> ConfiguracionGlobal { get; set; }

       
        public virtual DbSet<ConsecutivoDocElectronico> ConsecutivoDocElectronico { get; set; }
        public virtual DbSet<Exoneracion> Exoneracion { get; set; }
        public virtual DbSet<MedioPago> MedioPago { get; set; }
        

        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<ProductoImpuesto> ProductoImpuesto { get; set; }

        public virtual DbSet<TipoPlan> TipoPlan { get; set; }
        public virtual DbSet<TipoProductoServicio> TipoProductoServicio { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificacion { get; set; }
        public virtual DbSet<TipoImpuesto> TipoImpuesto { get; set; }
        public virtual DbSet<TipoMoneda> TipoMoneda { get; set; }

        
       
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }


       
        
        
        public virtual DbSet<XMLDomain.ResumenFactura> ResumenFactura { get; set; }

        
        //Para los datos del Receptor que suba informacion
        
        public virtual DbSet<WSDomain.WSRecepcionPOSTReceptor> WSRecepcionPOSTReceptor { get; set; }

        
        public virtual DbSet<Empresa> Empresa { get; set; }
        */

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