<%@ Page Title="Blog" Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeBehind="BlogTimeline.aspx.cs" Inherits="Web.BlogTimeline" %>
<%@ Register Src="~/UserControls/BlogNavigation.ascx" TagPrefix="dx" TagName="BlogNavigation" %>
<script runat=server>
protected String GetPreviewText() {
    var previewTextLength = 335;
    var blogBody = Eval("Body").ToString();
    return blogBody.Length > previewTextLength ? blogBody.Substring(0, previewTextLength) : blogBody;
}
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<div class="container">
    <div class="row">
        <div class="col-md-9">
            <div class="page-header">
                <h1>Blog</h1>
            </div>
            <dx:ASPxDataView ID="BlogPostsDataView" runat="server" Width="100%" CssClass="dataViewPosts" PagerAlign="Left">
                <ContentStyle Paddings-Padding="0px" Border-BorderWidth="0px" />
                <ItemStyle Width="100%" Paddings-Padding="0px" />
                <SettingsTableLayout ColumnCount="1" RowsPerPage="3" />
                <PagerSettings Position="Bottom" SEOFriendly="Enabled" EnableAdaptivity="True" />
                <ItemTemplate>
                    <section class="row">
                        <div class="col-sm-5"><a href="BlogPost.aspx?id=<%# Eval("ID")%>"><img class="blogImage" src='<%# Page.ResolveClientUrl(Eval("ImageUrl").ToString()) %>' alt=""></a></div>
                        <article class="col-sm-7">
                            <h3><a href="BlogPost.aspx?id=<%# Eval("ID")%>"><%# Eval("Subject") %></a></h3>
                            <p><%# GetPreviewText() %>...</p>
                            <a href="BlogPost.aspx?id=<%# Eval("ID")%>"><big>Read more</big></a>
                        </article>
                    </section>
                    <hr />
                </ItemTemplate>
            </dx:ASPxDataView>
        </div>                
        <nav class="col-md-3">
            <dx:BlogNavigation runat="server"/>
        </nav>
    </div>
</div>
</asp:Content>