<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="DefaultRedirectErrorPage.aspx.cs" Inherits="Web.Pages.Error.DefaultRedirectErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    
<div>
    <h3>
      Se presento un error, si desconoce la fuente contacte al administrador  del sistema Tel.88729065.</h3>
    <asp:Panel ID="InnerErrorPanel" runat="server" Visible="false">
      <p>
        Inner Error Message:<br />
        <asp:Label ID="innerMessage" runat="server" Font-Bold="true" 
          Font-Size="Large" /><br />
      </p>
      <pre>
        <asp:Label ID="innerTrace" runat="server" />
      </pre>
    </asp:Panel>
    <p>
      Error Message:<br />
      <asp:Label ID="exMessage" runat="server" Font-Bold="true" 
        Font-Size="Large" />
    </p>
    <pre>
      <asp:Label ID="exTrace" runat="server" Visible="false" />
    </pre>
    <br />
    Volver a <a href='/Pages/Home.aspx'>Inicio</a>
  </div>


</asp:Content>
