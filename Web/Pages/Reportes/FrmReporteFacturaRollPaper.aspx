﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmReporteFacturaRollPaper.aspx.cs" Inherits="Web.Pages.Reportes.FrmReporteFacturaRollPaper" %>

<%@ Register Assembly="DevExpress.XtraReports.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server">
        <BorderLeft BorderStyle="Solid" />
        <BorderRight BorderStyle="Solid" />
        <DisabledStyle>
            <BorderRight BorderStyle="Solid" />
        </DisabledStyle>
    </dx:ASPxWebDocumentViewer>     
</asp:Content>

