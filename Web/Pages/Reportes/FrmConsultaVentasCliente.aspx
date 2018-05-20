<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmConsultaVentasCliente.aspx.cs" Inherits="Web.Pages.Reportes.FrmConsultaVentasCliente" %>

<%@ Register Assembly="DevExpress.XtraReports.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Src="~/UserControls/AddAuditoriaForm.ascx" TagPrefix="user" TagName="AddAuditoriaForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

      <div class="text-box-title">
        <div class="text-box-heading-title"> Consulta de venta por clientes</div>
        <div class="arrow-down-title" style="margin-bottom: 5px;"></div>                        
     </div>  
     
    <div class="borde_redondo_tabla">

        <dx:ASPxFormLayout runat="server" AlignItemCaptionsInAllGroups="true">
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
            <Items>
                <dx:LayoutGroup Caption="Datos de Consulta" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                    <Items>

                        <dx:LayoutItem Caption="Fecha Inicio">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxDateEdit ID="txtFechaInicio" runat="server" Width="100%"  ValidationSettings-RequiredField-IsRequired="true"  DisplayFormatString="yyyy/MM/dd" EditFormatString="yyyy/MM/dd">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Requerido">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <dx:LayoutItem Caption="Fecha Fin">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxDateEdit ID="txtFechaFin" runat="server" Width="100%" ValidationSettings-RequiredField-IsRequired="true"  DisplayFormatString="yyyy/MM/dd" EditFormatString="yyyy/MM/dd">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Requerido">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxButton ID="txtConsultar" runat="server" Text="Consultar" Width="80px" Image-Url="~/Content/Images/search1.png" CausesValidation="true"  Image-Height="20px" OnClick="txtConsultar_Click" >
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

    </div>
    

</asp:Content>
