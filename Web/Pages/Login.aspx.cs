using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Class.Seguridad;
using Class.Utilidades;
using System.Threading;
using Web.Models;
using System.Data.Entity;

namespace Web.Pages
{
    public partial class Login : System.Web.UI.Page {


        public Login()
        {
        }

        protected void Page_Load(object sender, EventArgs e) {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.alertMessages.Attributes["class"] = "";
            this.alertMessages.InnerText = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e) {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    Usuario usuario = conexion.Usuario.Find(tbUserName.Text);
                    
                    if (usuario != null)
                    {
                        if (usuario.contrasena.Equals(MD5Util.getMd5Hash(this.tbPassword.Text)))
                        {

                            if (usuario.estado.Equals(Estado.INACTIVO.ToString()))
                            {
                                this.alertMessages.InnerText = "El usuario se encuentra inactivo, contacte al administrador";
                                return;
                            }
                            if (usuario.Rol.estado.Equals(Estado.INACTIVO.ToString()))
                            { 
                                this.alertMessages.InnerText = "El perfil al que pertenece el usuario, se encuentra inactivo, contacte al administrador";
                                this.alertMessages.Attributes["class"] = "alert alert-danger";
                                return;
                            }

                            Session["usuario"] = usuario.codigo;
                            Session["emisor"] = conexion.EmisorReceptorIMEC.Find(usuario.codigo);
                             
                            FormsAuthentication.SetAuthCookie(usuario.nombre, false);
                            Response.Redirect("~/"); 
                        }
                        else
                        {

                            usuario.intentos += 1;
                            conexion.Entry(usuario).State = EntityState.Modified;
                            conexion.SaveChanges();

                            if (usuario.intentos > 3)
                            {
                                this.alertMessages.InnerText = "Ha superado la mayor cantidad de intentos posibles, su cuenta ha sido bloqueada, contacte al administrador";
                                this.alertMessages.Attributes["class"] = "alert alert-danger";

                            }
                            else
                            {
                                this.alertMessages.InnerText = "Intento fallidos : " + usuario.intentos;
                                this.alertMessages.Attributes["class"] = "alert alert-danger";

                            }
                            this.tbUserName.ErrorText = "Usuario invalido";
                        }

                    }
                    
                }
            }
            catch (Exception ex)
            {
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL (ex.Message);
                this.alertMessages.Attributes["class"] = "alert alert-danger";
            }
        }
         
    }
}