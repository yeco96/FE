﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Models.Catalogos;

namespace Web.Models
{
    public class DataModelFE : DbContext
    {


        public DataModelFE() : base("name=fe_db")
        {
        }

        public virtual DbSet<Canton> Canton { get; set; }
        public virtual DbSet<CondicionVenta> CondicionVenta { get; set; }
        public virtual DbSet<CodigoReferencia> CodigoReferencia { get; set; }
        public virtual DbSet<Distrito> Distrito { get; set; }
        public virtual DbSet<Exoneracion> Exoneracion { get; set; }
        public virtual DbSet<MedioPago> MedioPago { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<ProductoServicio> ProductoServicio { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificacion { get; set; }
        public virtual DbSet<TipoMoneda> TipoMoneda { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

 



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