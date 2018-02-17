<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" CodeBehind="Login.aspx.cs" Inherits="Web.Pages.Login" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">

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
                <dx:LayoutGroup Caption="Formulario de ingreso" ColCount="1" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <p>Por favor digite su usuario y contrase&ntilde;a.  <a href="/Pages/Contacts.aspx">Contáctenos</a> si no tiene una cuenta.</p>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <dx:LayoutItem Caption="Usuario">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="tbUserName" runat="server" HelpText="Favor, ingrese el usuario">
                                        <ValidationSettings ValidationGroup="LoginUserValidationGroup" ErrorDisplayMode="ImageWithTooltip">
                                            <RequiredField ErrorText="El usuario es requerido" IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Contraseña">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="tbPassword" runat="server" Password="true" HelpText="Favor, ingrese la contraseña">
                                        <ValidationSettings ValidationGroup="LoginUserValidationGroup" ErrorDisplayMode="ImageWithTooltip">
                                            <RequiredField ErrorText="La contrase&ntilde;a es requerida" IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="" >
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxButton ID="btnLogin" runat="server" Text="Ingresar" Width="100px"  ValidationGroup="LoginUserValidationGroup" OnClick="btnLogin_Click">
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <a href="FrmRestaurarContrasena.aspx">¿ Olvid&oacute; su contrase&ntilde;a ?</a>
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
