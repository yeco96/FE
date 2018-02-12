using Class.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserControls {
    public partial class AddCommentForm : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {


            String mensajeConsulta = "";
            mensajeConsulta += "<b><p>Nombre: </p></b>" + txtNombre.Text;
            mensajeConsulta += "<b><p>Correo electrónico : </p></b>" + txtCorreo.Text;
            mensajeConsulta += "<b><p>Consulta: </p></b>";
            mensajeConsulta += "<p></p>";
            mensajeConsulta += "<p></p>" +txtConsulta.Text;
            mensajeConsulta += "<p></p>";
            mensajeConsulta += "<p>Gracias</p>";


            Utilidades.sendMail("roswel030@gmail.com","Consulta de : " + txtNombre.Text.ToUpper(), mensajeConsulta, "",null, "", "");
        }
    }
}