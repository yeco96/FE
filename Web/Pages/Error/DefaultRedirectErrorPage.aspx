<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutError.master" AutoEventWireup="true" CodeBehind="DefaultRedirectErrorPage.aspx.cs" Inherits="Web.Pages.Error.DefaultRedirectErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    
<div>
    <h5>
      Se presento un error, si desconoce la fuente contacte al administrador  del sistema.</h5>
    <ul>
        <li>Si pasaron más de 5 minutos sin utilizar el sistema, su sesión venció, ingrese nuevamente.</li>
        <li>Verifique su conexión a internet.</li>
    </ul>

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
