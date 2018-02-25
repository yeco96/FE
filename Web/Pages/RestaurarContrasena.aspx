<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutError.master"  CodeBehind="RestaurarContrasena.aspx.cs" Inherits="Web.Pages.RestaurarContrasena" %>

 
<asp:Content ID="ClientArea" ContentPlaceHolderID="Content" runat="server">
   
  
    <style>
        .borde {
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
            padding: 10px;
            width: 500px;
            margin: 0 auto;
        }

        @media only screen and (max-width: 800px) { 
            .borde { 
                width: 100%;
            }  
        }

    </style>


    <div class="borde">
        <dx:ASPxFormLayout runat="server" >
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
            <Items>
                <dx:LayoutGroup Caption="Formulario de reinicio de contraseña" ColCount="1" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <p>Por favor digite su usuario y correo, si no los recuerda <a href="/Pages/Contacts.aspx">Contáctenos</a></p>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <dx:LayoutItem Caption="Usuario">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txtUserName" runat="server" HelpText="Favor, ingrese su usuario" MaxLength="12">
                                        <ValidationSettings ValidationGroup="LoginUserValidationGroup" ErrorDisplayMode="ImageWithTooltip">
                                            <RequiredField ErrorText="El usuario es requerido" IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Correo">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="txtCorreo" runat="server" MaxLength="50" HelpText="Favor, ingrese su correo" >
                                        <ValidationSettings ValidationGroup="LoginUserValidationGroup" ErrorDisplayMode="ImageWithTooltip">
                                            <RequiredField ErrorText="El correo es requerido" IsRequired="true" />
                                            <RegularExpression ErrorText="El correo no es valido" ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem> 
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxCaptcha ID="Captcha" runat="server" Theme="MetropolisBlue">
                                        <ValidationSettings ErrorText="El código ingresado es incorrecto." SetFocusOnError="true" ErrorDisplayMode="Text">
                                        </ValidationSettings>
                                        <RefreshButton Text="Mostrar otro código.">
                                        </RefreshButton>
                                        <TextBox LabelText="Digite el código mostrado:" NullText="Ingrese el código mostrado" Position="Top" />
                                        <ChallengeImage ForegroundColor="#000000" AlternateText="Codigo"></ChallengeImage>
                                    </dx:ASPxCaptcha>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                          <dx:LayoutItem Caption="" >
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <table><tr><td>
                                    <dx:ASPxButton ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" /></td>
                                        <td>
                                     <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" Visible="false" OnClick="btnRegresar_Click" /></td><tr>
                                        </table>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                    </Items>
                </dx:LayoutGroup>

            </Items>
        </dx:ASPxFormLayout>

        <div id="alertMessages" role="alert" runat="server" />
    </div>
</asp:Content>


