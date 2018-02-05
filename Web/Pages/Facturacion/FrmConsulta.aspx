<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmConsulta.aspx.cs" Inherits="Web.Pages.Facturacion.FrmConsulta" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraReports.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <section class="featured">
        <div class="content-wrapper">
            Ingrese el número del documento a consultar
        </div>
    </section>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always" OnUnload="UpdatePanel_Unload">
        <ContentTemplate>

            <dx:ASPxFormLayout runat="server">
                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                <Items>
                    <dx:LayoutGroup Caption=" " ColCount="2" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                    
                            <Items>
                            <dx:LayoutItem Caption=" ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxCheckBox ID="chkGenerarDocumentos" runat="server" Text="Generar Documentos " Theme="Moderno"></dx:ASPxCheckBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption=" ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Desde :">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit ID="dtnDesde" runat="server"></dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Hasta :">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxDateEdit ID="dtnHasta" runat="server"></dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Documentos">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cmbDocumentos" runat="server" ValueType="System.String"></dx:ASPxComboBox>
                                        
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                        </Items>

                    </dx:LayoutGroup>
                    
                    <dx:LayoutGroup Caption="" ColCount="2" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                        <Items>

                            <dx:LayoutItem Caption="Documento número">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtDocumentoNum" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="true">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption=" ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButton ID="btnBuscar" text="Buscar" runat="server" Width="50px" CausesValidation="true" Style="visibility:visible"></dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                        </Items>

                    </dx:LayoutGroup>


                </Items>

            </dx:ASPxFormLayout>
            <div id="alertMessages" role="alert" runat="server" />
             
        </ContentTemplate>
    </asp:UpdatePanel>

    <div style="text-align: center;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
            <ProgressTemplate>
                <div class="modalBlock">
                    <div class="centerBlock">
                        <img src="../../Content/Images/cargando.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server"></dx:ASPxWebDocumentViewer>
</asp:Content>
