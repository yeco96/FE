<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddUbicacionForm.ascx.cs" Inherits="Web.UserControls.AddUbicacionForm" %>

<dx:ASPxFormLayout runat="server" ID="layoutAddUbicacionForm" >
    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
    <Items>
        <dx:LayoutGroup Caption=" " ColCount="3" GroupBoxDecoration="None" UseDefaultPaddings="false">

            <Items>
                <dx:LayoutItem Caption="Provincia">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxGridViewTemplateReplacement   ReplacementType="EditFormCellEditor" ColumnID="provincia" runat="server"/>  
                        </dx:LayoutItemNestedControlContainer> 
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Canton">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxGridViewTemplateReplacement  ReplacementType="EditFormCellEditor" ColumnID="canton" runat="server"/>   
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Distrito">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                            <dx:ASPxGridViewTemplateReplacement  ReplacementType="EditFormCellEditor" ColumnID="distrito" runat="server"/>   
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Barrio">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                             <dx:ASPxGridViewTemplateReplacement  ReplacementType="EditFormCellEditor" ColumnID="barrio" runat="server"/>   
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                 <dx:LayoutItem Caption="Otras Señas">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer>
                             <dx:ASPxGridViewTemplateReplacement  ReplacementType="EditFormCellEditor" ColumnID="otraSena" runat="server"/>   
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>

    </Items>
</dx:ASPxFormLayout>
