using Class.Seguridad;
using Class.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Configuracion;

namespace Web.UserControls {
    public partial class AddCommentForm : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    this.alertMessages.Attributes["class"] = "";
                    this.alertMessages.InnerText = "";
            }
            catch (Exception ex)
            {
                this.alertMessages.Attributes["class"] = "alert alert-danger";
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                String mensajeConsulta = "";
                mensajeConsulta += "<b><p>Nombre: </p></b>" + txtNombre.Text.ToString().ToUpper();
                mensajeConsulta += "<b><p>Correo electrónico : </p></b>" + txtCorreo.Text.ToString().ToLower();
                mensajeConsulta += "<b><p>Consulta: </p></b>";
                mensajeConsulta += "<p></p>";
                mensajeConsulta += "<p></p>" + txtConsulta.Text;
                mensajeConsulta += "<p></p>";
                mensajeConsulta += "<p>Gracias</p>";

                using (var conexion = new DataModelFE())
                {
                    ConfiguracionGlobal dato = new ConfiguracionGlobal();

                    //llena el objeto con los valores de la pantalla
                    dato.codigo = "RECEPTOR_CONSULTA";
                    
                    //busca el objeto 
                    dato = conexion.ConfiguracionGlobal.Find(dato.codigo);
                    //string pCorreo = "roswel030@gmail.com";
                    //string a = dato.descripcion;
                    Utilidades.sendMail(Usuario.USUARIO_AUTOMATICO,dato.descripcion, "Consulta de : " + txtNombre.Text.ToUpper(), mensajeConsulta, "", null, "", "");

                }

                this.alertMessages.Attributes["class"] = "alert alert-warning";
                this.alertMessages.InnerText = String.Format("Consulta enviada satisfactoriamente.");
                this.txtConsulta.Text = "";
                this.txtNombre.Text = "";
                this.txtCorreo.Text = "";

            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
            finally
            {
                //refescar los datos
            }

        }
    }
}