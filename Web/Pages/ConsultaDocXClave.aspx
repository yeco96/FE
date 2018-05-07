<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="ConsultaDocXClave.aspx.cs" Inherits="Web.Pages.ConsultaDocXClave" %>

<%@ Register Assembly="DevExpress.XtraReports.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <section class="featured">
        <div class="content-wrapper">
            Documento Electrónico
        </div>
    </section>
    <dx:ASPxFormLayout runat="server" AlignItemCaptionsInAllGroups="true">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Caption="Datos de Consulta" ColCount="2" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                <Items>

                    <dx:LayoutItem Caption="Clave del documento">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>

                                <dx:ASPxTextBox ID="txtClave" runat="server" Width="100%" MaxLength="50" ToolTip="Favor ingresar la clave numérica">
                                    <MaskSettings Mask="00000000000000000000000000000000000000000000000000" />
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Requerido">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton ID="txtConsultar" runat="server" Text="Consultar" Width="80px" Image-Url="~/Content/Images/search1.png" Image-Height="20px"
                                    CausesValidation="true" OnClick="btnConsultar_Click">
<Image Height="20px" Url="~/Content/Images/search1.png"></Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>



                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <div id="alertMessages" role="alert" runat="server" />
    <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server"></dx:ASPxWebDocumentViewer>
</asp:Content>
