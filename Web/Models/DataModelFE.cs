using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class DataModelFE : DbContext
    {


        public DataModelFE() : base("name=fe_db")
        {

        }

        public virtual DbSet<TipoMoneda> Moneda { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }




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