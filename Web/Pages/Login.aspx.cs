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
using Web.Models.Facturacion;
using System.Data.Entity.Validation;

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

                            EmisorReceptorIMEC emisor = conexion.EmisorReceptorIMEC.Find(usuario.emisor);
                            Session["usuario"] = usuario.codigo;
                            Session["elUsuario"] = usuario;
                            Session["emisor"] = emisor.identificacion;
                            Session["elEmisor"] = emisor;



                            usuario.intentos = 0;
                            conexion.Entry(usuario).State = EntityState.Modified;
                            conexion.SaveChanges();

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
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);
                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(fullErrorMessage, ex.EntityValidationErrors);

            }
            catch (Exception ex)
            {
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
                this.alertMessages.Attributes["class"] = "alert alert-danger";
            }
        }
         
    }
}