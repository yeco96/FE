<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutError.master" AutoEventWireup="true" CodeBehind="Http404ErrorPage.aspx.cs" Inherits="Web.Pages.Error.Http404ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

 <div>
    <h2>
      DefaultRedirect Error Page</h2>
    Standard error message suitable for all unhandled errors. 
    The original exception object is not available.<br />
    <br />
    Return to the <a href='Default.aspx'> Default Page</a>
  </div>

</asp:Content>
