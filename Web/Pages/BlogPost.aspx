<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeBehind="BlogPost.aspx.cs" Inherits="Web.BlogPost" %>
<%@ Register Src="~/UserControls/BlogNavigation.ascx" TagPrefix="dx" TagName="BlogNavigation" %>
<%@ Register Src="~/UserControls/AddCommentForm.ascx" TagPrefix="dx" TagName="AddCommentForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<div class="container">
    <div class="row">
        <article class="col-sm-9">
            <div class="page-header">
                <h1 id="Subject" runat="server"></h1>
                <div class="text-muted">
                    <span>Posted on: <i class="glyphicon glyphicon-calendar"></i> <asp:Literal id="Date" runat=server mode="PassThrough"/></span>
                    <span>by <i class="glyphicon glyphicon-user"></i> <a href="#">Admin</a></span>
                </div>
            </div>
            <img Id="BlogImage" runat="server" class="blogImage" alt="">
            <p id="Body" runat="server" class="marginTop20"></p>
            <hr />
        </article>
        <nav class="col-sm-3">
            <dx:BlogNavigation runat="server"/>
        </nav>
    </div>
    <div class="row">
        <div class="col-md-9">
            <h4>Leave a Comment:</h4>
            <dx:AddCommentForm runat="server"/>
            <hr />
            <div class="media">
                <div class="media-left">
                    <i class="glyphicon glyphicon-user user text-muted"></i>
                </div>
                <div class="media-body">
                    <h4 class="media-heading">Nick<small> <%:DateTime.Today.ToString("ddd, MMM dd yyyy")%></small></h4>
                    <p>Lorem ipsum dolor sit amet, nec te errem ridens aeterno. Ei mea utinam incorrupte intellegebat, te sonet aliquando intellegebat nam, offendit theophrastus ea per.</p>
                </div>
            </div>
            <hr />
            <div class="media">
                <div class="media-left">
                    <i class="glyphicon glyphicon-user user text-muted"></i>
                </div>
                <div class="media-body">
                    <h4 class="media-heading">Geert<small> <%:DateTime.Today.ToString("ddd, MMM dd yyyy")%></small></h4>
                    <p>Ut per alia constituto, vis amet adhuc quidam no, ea amet aeterno ceteros mea. Nec scriptorem adversarium id, dolorum elaboraret id sit. Novum ignota per ne, mel mollis verear ut. Id per integre periculis gubergren, eam aperiam apeirian menandri et.</p>
                    <div class="media">
                        <div class="media-left">
                            <i class="glyphicon glyphicon-user user text-muted"></i>
                        </div>
                        <div class="media-body">
                            <h4 class="media-heading">John<small> <%:DateTime.Today.ToString("ddd, MMM dd yyyy")%></small></h4>
                            <p>Mollis vivendum in pro. Partem tritani cu duo, te cum antiopam consequuntur.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3"></div>
    </div>
</div>
</asp:Content>