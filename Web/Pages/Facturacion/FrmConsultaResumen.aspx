<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmConsultaResumen.aspx.cs" Inherits="Web.Pages.Facturacion.FrmConsultaResumen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
            <section class="featured">
        <div class="content-wrapper">
            Facturas Resumen 
        </div>
    </section>

    <div class="borde_redondo_tabla">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
            Width="100%" EnableTheming="True"  Theme="Moderno">
        <Columns>
                <dx:GridViewCommandColumn Width="100px" ButtonType="Image" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="Clave" FieldName="clave" VisibleIndex="1" PropertiesTextEdit-MaxLength="12"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Codigo Moneda" FieldName="codigoMoneda" VisibleIndex="2" PropertiesTextEdit-MaxLength="3" ></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tipo Cambio" FieldName="tipoCambio" VisibleIndex="3" PropertiesTextEdit-MaxLength="10"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Servicios Gravados" FieldName="totalServGravados" VisibleIndex="4" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Servicios Exentos" FieldName="totalServExentos" VisibleIndex="5" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Mercancías Gravados" FieldName="totalMercanciasGravados" VisibleIndex="6" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Mercancías Exentas" FieldName="totalMercanciasExentas" VisibleIndex="7" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Gravado" FieldName="totalGravado" VisibleIndex="8" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Exento" FieldName="totalExento" VisibleIndex="9" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Venta" FieldName="totalVenta" VisibleIndex="10" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Descuentos" FieldName="totalDescuentos" VisibleIndex="11" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Venta Neta" FieldName="totalVentaNeta" VisibleIndex="12" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Impuesto" FieldName="totalImpuesto" VisibleIndex="13" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Comprobante" FieldName="totalComprobante" VisibleIndex="12" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
         </Columns>
        <TotalSummary>
                <dx:ASPxSummaryItem FieldName="totalDescuentos" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="totalVentaNeta" SummaryType="Sum" />
            </TotalSummary>
                </dx:ASPxGridView>

        <%--<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1" FileName="Catálogo Consecutivo Documento Electrónico">
            <Styles>
                <Default Font-Names="Arial" Font-Size="Small" />
            </Styles>
            <PageHeader Center="Facturación Web - Catálogo Consecutivo Documento Electrónico">
                <Font Bold="True" Names="Arial" Size="Large" />
            </PageHeader>
            <PageFooter Left="[Page # of Pages #]" Right="[Date Printed][Time Printed]">
                <Font Names="Arial" Size="Small" />
            </PageFooter>
        </dx:ASPxGridViewExporter> --%>
    </div>
    <div id="alertMessages" role="alert" runat="server" />
</asp:Content>
