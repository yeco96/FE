using Class.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;

namespace Web.UserControls {
    public partial class AddUbicacionForm : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            try
            { 
                 
            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
        }
    }
}