<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" CodeBehind="Login.aspx.cs" Inherits="WebServiceFE.Seguridad.Login" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="Content" runat="server">

    <style> 

        .borde_redondo { 
            border: 1px solid #3E5496;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
            padding: 10px; 
            width:300px;
            margin: 0 auto; 
        }
 

        .content-wrapper { 
            border: 1px solid #3E5496;
            background-color:#0072C6 ;   /* 3E5496  0072C6 */
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
            padding: 10px; 
            width:300px;
            margin: 0 auto;
            color:white;
            font-weight:bold;
        }

         .borde_redondo_interior { 
            border: 1px solid #3E5496;
            background-color:#0072C6 ;   /* 3E5496  0072C6 */
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
            padding: 10px; 
            width:170px;
            margin: 0 auto;
            color:white;
            font-weight:bold;
        }


    </style>

     <br />
    <div  class ="content-wrapper">
        Inicio de sesi&oacute;n
    </div>
    <br />
    <div  class ="borde_redondo">

        <div class="accountHeader" align="center" > 
            <p> Por favor digite su usuario y contrase&ntilde;a.  <a href="Register.aspx">Registrese</a> si no tiene una cuenta.</p>
        </div>

        <div  class ="borde_redondo_interior">
                Usuario:
                <div class="form-field">
                    <dx:ASPxTextBox ID="tbUserName" runat="server"  >
                        <ValidationSettings ValidationGroup="LoginUserValidationGroup" ErrorDisplayMode="ImageWithTooltip">
                            <RequiredField ErrorText="El usuario es requerido." IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
                Contrase&ntilde;a:
                <div class="form-field">
                    <dx:ASPxTextBox ID="tbPassword" runat="server" Password="true" >
                        <ValidationSettings ValidationGroup="LoginUserValidationGroup" ErrorDisplayMode="ImageWithTooltip">
                            <RequiredField ErrorText="La contrase&ntilde;a es requerido." IsRequired="true" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
                <br />
                <div align="center">
                    <dx:ASPxButton ID="btnLogin" runat="server" Text="Entrar" ValidationGroup="LoginUserValidationGroup"
                        OnClick="btnLogin_Click">
                    </dx:ASPxButton>
                </div>
            </div> 
                <div class="accountHeader" align="center"> 
                    <a href="FrmRestaurarContrasena.aspx">¿ Olvid&oacute; su contrase&ntilde;a ?</a>
                    <br />
                    <dx:ASPxLabel ID="lblError" runat="server" ForeColor="Red" Text="Error" />
                </div>
    
        </div> 
</asp:Content>