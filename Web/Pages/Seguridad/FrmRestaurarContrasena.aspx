<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master"  CodeBehind="FrmRestaurarContrasena.aspx.cs" Inherits="WebServiceFE.Seguridad.FrmRestaurarContrasena" %>

 
<asp:Content ID="ClientArea" ContentPlaceHolderID="Content" runat="server">
    <style>
        .dxgvEditFormTable_MetropolisBlue {
            padding: 4px 4px 4px 10px;
            white-space: nowrap;
        }

        .borde_redondo {
            border: 1px solid #3E5496;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
            padding: 10px;
            width: 98%;
            margin: 0 auto;
        }

        .content-wrapper {
            border: 1px solid #3E5496;
            background-color: #0072C6; /* 3E5496  0072C6 */
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
            padding: 10px;
            width: 98%;
            margin: 0 auto;
            color: white;
            font-weight: bold;
        }

        .borde_redondo_interior {
            border: 1px solid #3E5496;
            background-color: #0072C6; /* 3E5496  0072C6 */
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
            padding: 10px;
            width: 170px;
            margin: 0 auto;
            color: white;
            font-weight: bold;
        }

        .errorMessage {
            color: red;
        }

        .successMessage {
            color: blue;
            font: medium;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always" OnUnload="UpdatePanel_Unload">
        <ContentTemplate>

            <div class="content-wrapper">
                Restaurar contrase&ntilde;a
            </div>
            <br />
            <div class="borde_redondo">
                <p>Favor ingresar los valores registrados en el repositorio de datos</p>
            </div>
            <br />
            <div class="borde_redondo">

                <div>
                    <table>
                        <tr>
                            <td>
                                <table class="dxgvEditFormTable_MetropolisBlue">
                                    <tr>
                                        <td class="dxgvEditFormCaption_MetropolisBlue">
                                            <dx:ASPxLabel ID="lblUserName" runat="server" AssociatedControlID="tbUserName" Text="Usuario" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dxgvEditFormCaption_MetropolisBlue">
                                            <dx:ASPxTextBox ID="tbUserName" runat="server">
                                                <ValidationSettings ValidationGroup="RegisterUserValidationGroup" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField ErrorText="Usuario requerido." IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="dxgvEditFormCaption_MetropolisBlue">
                                            <dx:ASPxButton ID="btnContinuar" runat="server" Text="Verificar usuario" ValidationGroup="RegisterUserValidationGroup" OnClick="btnContinuar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="TablaCorreo" class="dxgvEditFormCaption_MetropolisBlue">
                                    <tr>
                                        <td class="dxgvEditFormCaption_MetropolisBlue">
                                            <dx:ASPxLabel ID="lblConfirmarCorreo" runat="server" Text="Confirmar correo electrónico" />
                                            <br />
                                            <dx:ASPxLabel ID="lblCorreo" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="dxgvEditFormCaption_MetropolisBlue">
                                            <dx:ASPxTextBox ID="tbCorreo" runat="server">
                                                <ValidationSettings ValidationGroup="RegisterUserValidationGroup" ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField ErrorText="Usuario requerido." IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td class="dxgvEditFormCaption_MetropolisBlue">
                                            <dx:ASPxButton ID="btnContinuar2" runat="server" Text="Verificar Correo" ValidationGroup="RegisterUserValidationGroup" OnClick="btnContinuar2_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td class="dxgvEditFormCaption_MetropolisBlue" colspan="2">
                                <dx:ASPxCaptcha ID="Captcha" runat="server" Theme="MetropolisBlue" Visible="False">
                                    <ValidationSettings ErrorText="El código ingresado es incorrecto." SetFocusOnError="true" ErrorDisplayMode="Text">
                                    </ValidationSettings>
                                    <RefreshButton Text="Mostrar otro código.">
                                    </RefreshButton>
                                    <TextBox LabelText="Digite el código mostrado:" NullText="Ingrese el código mostrado" Position="Top" />
                                    <ChallengeImage ForegroundColor="#000000" AlternateText="Codigo"></ChallengeImage>
                                </dx:ASPxCaptcha>
                            </td>
                        </tr>
                    </table>
                </div>

                <br />

                <dx:ASPxButton ID="btnEnviar" runat="server" Text="Enviar" ValidationGroup="RegisterUserValidationGroup" OnClick="btnEnviar_Click" />
                <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                <br />
                <br />
                <dx:ASPxLabel ID="lblError" runat="server" Text="Error" />

                <dx:ASPxTextBox ID="tbUserHide" runat="server" Visible="false"></dx:ASPxTextBox>
                <dx:ASPxLabel ID="lblCorreoOculto" runat="server" Visible="False" />
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
