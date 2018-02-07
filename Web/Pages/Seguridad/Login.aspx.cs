using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Bo.Seguridad;
using Class.Seguridad;
using Class.Utilidades;
using Bo.Catalogos;
using Class.Catalogos; 

namespace MatriculaWeb {
    public partial class Login : System.Web.UI.Page {

        UsuarioBo usuarioBo;    
        PeriodoBo periodoBo;
        ProfesorBo profesorBo;

        public Login(){
            this.usuarioBo = new UsuarioBo();
            this.periodoBo = PeriodoBo.GetInstance();
            this.profesorBo = ProfesorBo.GetInstance();
        }
        protected void Page_Load(object sender, EventArgs e) {
            this.lblError.Text = ""; 
        }

        protected void btnLogin_Click(object sender, EventArgs e) {
            try
            {  
                bool esUsuario = usuarioBo.existe(new Usuario(tbUserName.Text));

                Usuario usuario = this.usuarioBo.login(new Usuario(tbUserName.Text, tbPassword.Text));
                if (usuario != null)
                {
                    usuarioBo.aumentarIntentos(new Usuario(tbUserName.Text), "Correcto");
                    if (usuario.estado.ToUpper().Equals(Usuario.INACTIVO))
                    {
                        
                         this.lblError.Text = "El usuario se encuentra inactivo, contacte al administrador.";
                         return;
                    }
                    if (usuario.rol.estado.ToUpper().Equals(Rol.INACTIVO))
                    {
                        
                        this.lblError.Text = "El perfil al que pertenece el usuario, se encuentra inactivo, contacte al administrador.";
                        return;
                    }

                    Session["usuarioObjeto"] = usuario;
                    Session["usuario"] = usuario.idUsuario;

                    //Traemos el periodo Activo junto con los rangos de fecha estipulados
                    foreach (Periodo dato in this.periodoBo.consultarTodos())
                    {
                        //Colocamos los periodos activos
                        if (dato.estado.ToUpper() == "ACTIVO")
                        {
                            //COLOCAMOS LOS VALORES DE LOS PERIODOS ACTIVOS Y FECHAS DE INICIO Y FINAL DE REGISTRO DE NOTAS EN LAS VARIABLES SESION
                            Session["periodoActual"] = dato.ToString();
                            Session["fechaInicialRegistroNotas"] = dato.fechaInicialRegistroNotas.ToShortDateString();
                            Session["fechaFinalRegistroNotas"] = dato.fechaFinalRegistroNotas.ToShortDateString();
                        }
                    }
                    //En caso de ser profesor la información se guardará en una variable session
                    if (usuario.rol.descripcion.ToUpper() == "PROFESOR") { 
                        Session["IdProfesor"] = profesorBo.numIdProfesor(usuario.idUsuario);
                    }

                    this.usuarioBo.modificarUltimoIngreso(usuario);

                    if (String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    {
                        FormsAuthentication.SetAuthCookie(usuario.ToStringLogin(), false);

                        if (String.Compare(tbUserName.Text, tbPassword.Text) == 0)
                        {
                            Response.Redirect("~/Account/ChangePassword.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/Default.aspx");
                        }
                    }
                    else
                    {
                        //FormsAuthentication.RedirectFromLoginPage(usuario.ToStringLogin(), false);
                        FormsAuthentication.SetAuthCookie(usuario.ToStringLogin(), false);
                        Response.Redirect("~/Default.aspx");
                    }
                }
                else
                {
                    //Usuario no existe
                    if (esUsuario == true)
                    {
                        //Contador de usuario aumentando
                        usuarioBo.aumentarIntentos(new Usuario(tbUserName.Text), "Fallo");
                        //Indica la cantidad de intentos fallidos
                        int cantidad = usuarioBo.saberIntentos(new Usuario(tbUserName.Text));
                        if (cantidad >= 3)
                        {
                            this.lblError.Text = "Ha superado la mayor cantidad de intentos posibles, su cuenta ha sido bloqueada, contacte al administrador";
                            
                        }
                        else {
                            this.lblError.Text = "Intento fallidos : " + cantidad;
                            
                        }

                    }
                    tbUserName.ErrorText = "Usuario invalido";
                    tbUserName.IsValid = false;
                }
            }
            catch (Exception ex)
            { 
                lblError.Text  = Utilidades.validarExepcionSQL ( ex.Message);
                Session["errorMessage"] = null;
            }
        }
         
    }
}