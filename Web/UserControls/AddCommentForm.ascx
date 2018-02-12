<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCommentForm.ascx.cs" Inherits="Web.UserControls.AddCommentForm" %>
<dx:ASPxFormLayout ID="AddCommentFormLayout" runat="server" Width="100%" UseDefaultPaddings="false">
    <SettingsItems Width="100%" />
    <SettingsItemCaptions  Location="Top" />
    <Items>
        <dx:LayoutItem Caption="Nombre">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxTextBox ID="txtNombre" runat="server" Width="100%">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="true" ErrorText="El nombre es requerido" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Correo electrónico">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxTextBox ID="txtCorreo" runat="server" Width="100%">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                            <RegularExpression ErrorText="Correo electrónico incorrecto" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                            <RequiredField IsRequired="true" ErrorText="El correo electrónico es requerido" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Consulta">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxMemo ID="txtConsulta" runat="server" Width="100%" MaxLength="500" Rows="6">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="true" ErrorText="La consulta es requerida" />
                        </ValidationSettings>
                    </dx:ASPxMemo>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:EmptyLayoutItem />
        <dx:LayoutItem ShowCaption="False">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxButton ID="btnEnviar" runat="server" Text="Enviar" Width="100px" UseSubmitBehavior="true" OnClick="btnEnviar_Click"/>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
         
</dx:ASPxFormLayout>
<div id="alertMessages" role="alert" runat="server" />