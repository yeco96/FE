<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="WebServices.aspx.cs" Inherits="Web.Pages.WebServices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">




       

      <div class="">
        <div class="span4">
            <div class="text-box">
                <div class="text-box-heading-title">Desarrollo</div>
                <div class="arrow-down-blue" style="margin-bottom: 10px;"></div>
                <p>
                    <ul class="list-unstyled">
                        <li><strong>POST</strong></li>
                        <li><strong>Body Type: </strong>application/xml</li>
                        <li><strong>URL: </strong>http://des.fe.msasoft.net/api/services/recepcionmesajehacienda/</li>
                        <li><strong>GET</strong></li>
                        <li><strong>URL: </strong>http://des.fe.msasoft.net/api/services/respuestamesajehacienda/{clave}<li> 
                        <li><br/></li>
                        <li><strong>SECURITY</strong> (Basic Auth)</li>
                        <li><strong>user: </strong>msasoft<strong> password: </strong>msaoft.01</li>
                    </ul>
                </p>

            </div>
        </div>
    </div>

     <div class="">
        <div class="span4">
            <div class="text-box">
                <div class="text-box-heading-title">Producción</div>
                <div class="arrow-down-blue" style="margin-bottom: 10px;"></div>
                <p>
                    <ul class="list-unstyled">
                        <li><strong>POST</strong></li>
                        <li><strong>Body Type: </strong>application/xml</li>
                        <li><strong>URL: </strong>https://fe.msasoft.net/api/services/recepcionmesajehacienda/</li>
                        <li><strong>GET</strong></li>
                        <li><strong>URL: </strong>https://fe.msasoft.net/api/services/respuestamesajehacienda/{clave}<li> 
                        <li><br/></li>
                        <li><strong>SECURITY</strong> (Basic Auth)</li>
                        <li><strong>user: </strong>[required]<strong> password: </strong>[required]</li>
                    </ul>
                </p>

            </div>
        </div>
    </div>

</asp:Content>
