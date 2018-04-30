<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmReporteProformaResumen.aspx.cs" Inherits="Web.Pages.Reportes.FrmReporteProformaResumen" %>

<%@ Register Assembly="DevExpress.XtraReports.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server"></dx:ASPxWebDocumentViewer>
     <BorderLeft BorderStyle="Solid" />
        <BorderRight BorderStyle="Solid" />
        <DisabledStyle>
            <BorderRight BorderStyle="Solid" />
        </DisabledStyle>
</asp:Content>

