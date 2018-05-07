<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmDashboard.aspx.cs" Inherits="Web.Pages.Administracion.FrmDashboard" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    

     <dx:ASPxFormLayout runat="server" ID="formLayout" CssClass="formLayout">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                <Items>
                                    <dx:LayoutGroup Caption=" " ColCount="3" GroupBoxDecoration="None" UseDefaultPaddings="false">

                                        <Items>
                                            <dx:LayoutItem Caption="Fecha Inicio">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="txtFechaInicio" runat="server" 
                                                            DisplayFormatString="dd/MM/yyyy"  EditFormatString="dd/MM/yyyy"> 
                                                            <ValidationSettings ValidationGroup="ReportValidationGroup" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField ErrorText="Valor requerido" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Fecha Fin">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="txtFechaFin" runat="server" 
                                                            DisplayFormatString="dd/MM/yyyy"  EditFormatString="dd/MM/yyyy"> 
                                                            <ValidationSettings ValidationGroup="ReportValidationGroup" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField ErrorText="Valor requerido" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButton ID="btnGenerar" runat="server"   Text="Generar" ValidationGroup="ReportValidationGroup" Width="80px" Image-Url="~/Content/Images/search1.png" Image-Height="20px" />
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
    <div id="alertMessages" role="alert" runat="server" />

     <div class="center_grafico">
         
        <dx:WebChartControl ID="wbDocumentos" runat="server" Width="1000px" Height="500px" CssClass="AlignCenter TopLargeMargin"/>
    </div>





</asp:Content>
