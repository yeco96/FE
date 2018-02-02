<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCommentForm.ascx.cs" Inherits="Web.UserControls.AddCommentForm" %>
<dx:ASPxFormLayout ID="AddCommentFormLayout" runat="server" Width="100%" UseDefaultPaddings="false">
    <SettingsItems Width="100%" />
    <SettingsItemCaptions  Location="Top" />
    <Items>
        <dx:LayoutItem Caption="Nombre">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxTextBox ID="Name" runat="server" Width="100%">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="true" ErrorText="Name is required" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Correo electrónico">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxTextBox ID="Email" runat="server" Width="100%">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                            <RegularExpression ErrorText="Invalid e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                            <RequiredField IsRequired="true" ErrorText="Email is required" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Consulta">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxMemo ID="Notes" runat="server" Width="100%" MaxLength="500" Rows="6">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="true" ErrorText="Notes is required" />
                        </ValidationSettings>
                    </dx:ASPxMemo>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:EmptyLayoutItem />
        <dx:LayoutItem ShowCaption="False">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer>
                    <dx:ASPxButton ID="Submit" runat="server" Text="Enviar" Width="100px" UseSubmitBehavior="true" />
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>