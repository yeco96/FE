using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Class.Seguridad;
using System.Threading;
using Class.Utilidades;
using Web.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Web.Pages
{
    public partial class RestaurarContrasena : System.Web.UI.Page
    {
        const int MaxCharacterSetLength = 50;
        const string CaptchaCssPostfixSessionKey = "6aad54c0-25ef-11df-8a39-0800200c9a66";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
                this.alertMessages.Attributes["class"] = "";
                this.alertMessages.InnerText = "";

                Session[CaptchaCssPostfixSessionKey] = Captcha.CssPostfix;
            }
            if (Captcha.CssPostfix != (string)Session[CaptchaCssPostfixSessionKey])
            {
                Captcha.IsValid = true;
                Session[CaptchaCssPostfixSessionKey] = Captcha.CssPostfix;
            }
        }

        //Colocar el metodo del botón
        protected void btnEnviar_Click(object sender, EventArgs e)
        { 
            try { 
                //Si el captcha es correcto realizar lo del envio de datos
                if (Captcha.IsValid == true && txtUserName.Text != "" )
                {
                    //Genera posible contraseña
                    Usuario usuario = new Usuario();
                   
                    usuario.codigo = this.txtUserName.Text;
                    usuario.correo = this.txtCorreo.Text;

                    using (var conexion = new DataModelFE())
                    {
                        usuario = conexion.Usuario.Where(x=>x.codigo == usuario.codigo && x.correo == usuario.correo).FirstOrDefault();
                        if (usuario != null)
                        {
                            string password = Utilidades.generarContrasena(10);
                            usuario.contrasena = MD5Util.getMd5Hash(password);
                            usuario.intentos = 0;
                            usuario.estado = Estado.ACTIVO.ToString();
                            conexion.Entry(usuario).State = EntityState.Modified;
                            conexion.SaveChanges();

                            usuario.contrasena = password;
                            if (enviaCorreoCambioContrasena(usuario)) {  
                                this.alertMessages.Attributes["class"] = "alert alert-info";
                                this.alertMessages.InnerText = "La nueva contraseña ha sido enviada a la cuenta registrada";
                            }
                            else
                            {
                                this.alertMessages.Attributes["class"] = "alert alert-danger";
                                this.alertMessages.InnerText = "En este momento tenemos problemas en enviar el correo, favor intente dentro de unos minutos";
                            }
                        }
                        else
                        {
                            this.alertMessages.Attributes["class"] = "alert alert-danger";
                            this.alertMessages.InnerText = "No existe un usuario con la información suministrada";
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

    
        private bool enviaCorreoCambioContrasena(Usuario usuario)
        {
            try
            {
                String mensaje = "";
                mensaje += "<table width=100% border=1 cellpadding=0 cellspacing=0>";
                mensaje += "<tr><td bgcolor=#004080 style='color:white; padding:4px;' width=40%><strong><div align=center>MSA SOFT</div></strong></td>";
                mensaje += "<td style='padding:4px;'>Le enviamos este correo por que se recibio una solicitud para el cambio de clave para su usuario en el sitio web.</td></tr>";
                mensaje += "</table><br>";

                mensaje += "<table width=100% border=1 cellpadding=0 cellspacing=0>";
                mensaje += "<tr><th colspan=6 style='padding:4px;'>ESTOS SON SUS DATOS</th></tr>";
                mensaje += "<tr>";
                mensaje += "<th bgcolor=#004080 style='color:white; padding:4px;'><div align=center>USUARIO</div></th>";
                mensaje += "<th bgcolor=#004080 style='color:white; padding:4px;'><div align=center>CONTRASEÑA</div></th>";
                mensaje += "<tr>";
                mensaje += String.Format("<td style='padding:4px;'><div align=center>{0}</div></td>", usuario.codigo);
                mensaje += String.Format("<td style='padding:4px;'><div align=center>{0}</div></td>", usuario.contrasena);
                mensaje += "</tr>";
                mensaje += "</table>";
                 
                return Utilidades.sendMail(Usuario.USUARIO_AUTOMATICO, usuario.correo,
                  "MSA SOFT Facturación  | Olvido de Contraseña",
                    mensaje, "MSA SOFT");

            }
            catch (Exception ex)
            {
                this.alertMessages.InnerText = Utilidades.validarExepcionSQL(ex);
                this.alertMessages.Attributes["class"] = "alert alert-danger";
            }
            return false;
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Login.aspx");
        }
    }
}