<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddAuditoriaForm.ascx.cs" Inherits="Web.UserControls.AddAuditoriaForm" %>

<dx:ASPxFormLayout runat="server" ID="layoutAddAuditoriaForm" >
    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
    <Items>
        <dx:LayoutGroup Caption=" " ColCount="2" GroupBoxDecoration="None" UseDefaultPaddings="false">

            <Items>
                <dx:LayoutItem Caption="Usuario Creación">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxTextBox runat="server" Value='<%# Eval("usuarioCreacion") %>' AutoResizeWithContainer="true" Width="100%" BackColor="LightGray" ReadOnly="true" />

                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Fecha Creación">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxTextBox runat="server" Value='<%# Eval("fechaCreacion") %>' AutoResizeWithContainer="true" Width="100%" BackColor="LightGray" ReadOnly="true"/>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Usuario Modificación">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxTextBox runat="server" Value='<%# Eval("usuarioModificacion") %>' AutoResizeWithContainer="true" Width="100%" BackColor="LightGray" ReadOnly="true"/>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Fecha Modificación">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxTextBox runat="server" Value='<%# Eval("fechaModificacion") %>' AutoResizeWithContainer="true" Width="100%" BackColor="LightGray" ReadOnly="true"/>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>

    </Items>
</dx:ASPxFormLayout>
