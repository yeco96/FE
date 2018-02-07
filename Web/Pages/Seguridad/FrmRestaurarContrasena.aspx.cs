using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Class.Seguridad;
using Bo.Seguridad;
using Class.Utilidades;
using Bo.Catalogos;


namespace WebServiceFE
{
    public partial class FrmRestaurarContrasena : System.Web.UI.Page
    {
        const int MaxCharacterSetLength = 50;
        const string CaptchaCssPostfixSessionKey = "6aad54c0-25ef-11df-8a39-0800200c9a66";
        UsuarioBo usuarioBo = new UsuarioBo();
        MailConfigBo mailConfigBo = new MailConfigBo();
        Usuario usuario = new Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session[CaptchaCssPostfixSessionKey] = Captcha.CssPostfix;
                this.ocultaTablaCorreo();
                this.lblError.Text = "";
                this.btnEnviar.Visible = false;
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
                if (Captcha.IsValid == true && tbUserName.Text != "" )
                {
                    //Genera posible contraseña
                    usuario.contrasena = usuarioBo.generaContrasena(8);
                    usuario.idUsuario = tbUserHide.Text;
                    //Se cambia la contraseña
                    usuarioBo.cambioContrasena(usuario);
                    //Enviar Confirmación por correo
                    enviaCorreoCambioContrasena();
                    this.ocultaTablaCorreo();
                    this.Captcha.Visible = false;
                    this.btnEnviar.Visible = false;
                    this.btnContinuar.Visible = false;
                    this.tbUserName.Visible = false;
                    this.lblUserName.Visible = false;
                    //Se muestra el mensaje y se da formato al mensaje más grande
                    lblError.Text = "La nueva contraseña ha sido enviada a la cuenta registrada";
                    lblError.CssClass = "successMessage";
                }
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;
                this.lblError.CssClass = "errorMessage";
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            bool siExiste = false;
            this.ocultaTablaCorreo();
            if (this.tbUserName.Text != "")
            {
                //Validar si existe
                usuario.idUsuario = this.tbUserName.Text.ToString();
                siExiste = this.usuarioBo.existe(usuario);
                if (siExiste)
                {
                    //Traer Correo Electrónico
                    string correo = usuarioBo.consultarId(usuario).correo;
                    string valor = "";
                    bool cambio = false;
                    if (correo != null)
                    {
                        string primerLetra = correo.Substring(0, 1);
                        char[] a = correo.ToCharArray();
                        this.lblCorreoOculto.Text = correo;
                        //Recorremos la cadena para sustituir los valores
                        for (int i = 1; i < a.Length; i++)
                        {
                            if (!a[i].Equals('@') && cambio == false)
                            {
                                a[i] = '*';
                                valor += (a[i]);
                                cambio = false;
                            }
                            else
                            {
                                valor += (a[i]);
                                cambio = true;
                            }
                        }
                        this.lblCorreo.Text = primerLetra + valor;
                        this.muestraTablaCorreo();
                    }
                    else
                    {
                        this.lblError.Text = "El usuario no tiene un correo asociado";
                        this.lblError.CssClass = "errorMessage";
                    }
                    this.tbUserHide.Text = tbUserName.Text.ToString();
                }
                else {
                    this.ocultaTablaCorreo();
                    this.lblCorreoOculto.Text = "";
                    this.tbCorreo.Text = "";
                    this.tbUserHide.Text = "";
                    this.lblError.Text = "El usuario no existe";
                    this.lblError.CssClass = "errorMessage";
                }
            }
        }

        private void muestraTablaCorreo()
        {
            this.tbCorreo.Visible = true;
            this.btnContinuar2.Visible = true;
            this.lblConfirmarCorreo.Visible = true;
            this.lblCorreo.Visible = true;
        }
        private void ocultaTablaCorreo()
        {
            this.tbCorreo.Visible = false;
            this.btnContinuar2.Visible = false;
            this.lblConfirmarCorreo.Visible = false;
            this.lblCorreo.Visible = false;
        }

        protected void btnContinuar2_Click(object sender, EventArgs e)
        {
            if (tbCorreo.Text == lblCorreoOculto.Text)
            {
                this.Captcha.Visible = true;
                this.btnEnviar.Visible = true;
            }
            else {
                this.Captcha.Visible = false;
                this.btnEnviar.Visible = false;
                this.lblError.Text = "El correo no coincide con el ingresado en el repositorio de datos";
                lblError.CssClass = "errorMessage";
            }
        }

        private void enviaCorreoCambioContrasena()
        {
            try
            {
                String mensaje = "";
                mensaje += "<table width=100% border=1 cellpadding=0 cellspacing=0>";
                mensaje += "<tr><td bgcolor=#004080 style='color:white; padding:4px;' width=40%><strong><div align=center>UNIVERSIDAD SANTA LUCIA</div></strong></td>";
                mensaje += "<td style='padding:4px;'>Le enviamos este correo por que se recibio una solicitud para el cambio de clave para su usuario en el sitio web.</td></tr>";
                mensaje += "</table><br>";

                mensaje += "<table width=100% border=1 cellpadding=0 cellspacing=0>";
                mensaje += "<tr><th colspan=6 style='padding:4px;'>ESTOS SON SUS DATOS</th></tr>";
                mensaje += "<tr>";
                mensaje += "<th bgcolor=#004080 style='color:white; padding:4px;'><div align=center>USUARIO</div></th>";
                mensaje += "<th bgcolor=#004080 style='color:white; padding:4px;'><div align=center>CONTRASEÑA</div></th>";
                mensaje += "<tr>";
                mensaje += String.Format("<td style='padding:4px;'><div align=center>{0}</div></td>", usuario.idUsuario);
                mensaje += String.Format("<td style='padding:4px;'><div align=center>{0}</div></td>", usuario.contrasena);
                mensaje += "</tr>";
                mensaje += "</table>";

                this.mailConfigBo.enviar(this.lblCorreoOculto.Text, "Universidad Santa Lucia | Olvido de Contraseña", mensaje);

            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;
                this.lblError.CssClass = "errorMessage"; 
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            this.tbUserName.Text = " ";
            Response.Redirect("~/Account/Login.aspx");
        }
    }
}