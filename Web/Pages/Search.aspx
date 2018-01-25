<%@ Page Title="Search" Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeBehind="Search.aspx.cs" Inherits="Web.Search" %>
<%@ Register Src="~/UserControls/BlogNavigation.ascx" TagPrefix="dx" TagName="BlogNavigation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<div class="container">
    <div class="row">
        <section class="col-md-9">
            <div class="page-header">
                <h1>Search in blog</h1>
            </div>
            <dx:ASPxGridView ID="SearchGridView" runat="server" Width="100%" KeyFieldName="Id" PreviewFieldName="Body">
                <Styles>
                    <Cell BorderBottom-BorderWidth="0px"/>
                </Styles>
                <Columns>
                    <dx:GridViewDataHyperLinkColumn FieldName="Id">
                        <CellStyle HorizontalAlign="Left" />
                        <PropertiesHyperLinkEdit TextField="Subject" NavigateUrlFormatstring="~/Pages/BlogPost.aspx?id={0}"/>
                    </dx:GridViewDataHyperLinkColumn>
                </Columns>
                <Settings ShowPreview="true" ShowColumnHeaders="false"/>
                <SettingsSearchPanel Visible="true" SearchInPreview="true" />
                <SettingsBehavior MaxPreviewTextLength="250" />
                <SettingsPager PageSize="5" EnableAdaptivity="true" />
                <SettingsText EmptyDataRow="Nothing matches the specified criteria" />
            </dx:ASPxGridView>
        </section>
        <div class="col-md-3">
            <dx:BlogNavigation runat="server"/>
        </div>
    </div>
</div>
</asp:Content>