<%@ Page Title="Home" Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeBehind="Home.aspx.cs" Inherits="Web.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<div class="container-fluid">
    <div class="row">
        <dx:ASPxImageSlider ID="ImageSlider" runat="server" Width="100%" Height="400px" ImageSourceFolder="~/Content/Images/Components">
            <SettingsImageArea ImageSizeMode="FillAndCrop" NavigationButtonVisibility="Always" EnableLoopNavigation="true" />
            <SettingsNavigationBar Mode="Dots" />
            <SettingsSlideShow AutoPlay="true" PlayPauseButtonVisibility="OnMouseOver" />
        </dx:ASPxImageSlider>
    </div>
</div>
<div class="container">
    <section class="row text-center">
        <div class="col-md-12">
            <h1>�Qu� es la Factura Electr�nica?</h1>
            <p class="mainText">Una factura electr�nica es un documento de uso comercial para trabajar en l�nea con tributaci�n directa, dicho documento est� en formato electr�nico y podr� sustituir la factura tradicional en papel; la factura ser� en enviada v�a correo electr�nico al cliente sin perder validez o bien puede consultarla en nuestro portal.</p>
        </div>
        <div class="col-md-12 marginTop20">
            <a class="btn btn-primary btn-lg" href="#"><i class="glyphicon glyphicon-info-sign"></i> Desplace la p�gina para m�s informaci�n</a>
        </div>
    </section>
    <section class="row features">
         <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-wrench featureIcon text-primary"></i>
                </div>
            </div>
          <div style="position: relative; border:1px solid #E6E6E6; width: 250px;">
            <div class="media-body">
                <h4 class="media-heading">Facturas Electr�nicas </h4>
                <ul> 
                    <li><u> Realice su facturacion desde la NUBE</u></li>
                    <p>�Trabajas como profesional independiente? �Tiene una PYME y no cuenta con un sistema de facturaci�n o ERP?</p>
                    <asp:Button ID="Button2" runat="server"  Text="M�s Informaci�n" />
                </ul> 
            </div>
           </div>
        </div>
        <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-wrench featureIcon text-primary"></i>
                </div>
          </div>
            <div style="position: relative; border:1px solid #E6E6E6; width: 250px;">
            <div class="media-body">
                <h4 class="media-heading">Nuestros Planes</h4>
                <ul>
                    <li>Profesional Independiente</>  
                    <li>Pymes</li>
                    <li>Empresarial</li> 
                    <li>Planes Prepago</li>
                    <li>Planes Ilimitados</li>
                    <asp:Button ID="Button1" runat="server" Text="Ver Planes" />
                </ul> 
            </div>
           </div>
        </div>
      <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-wrench featureIcon text-primary"></i>
                </div>
            </div>
          <div style="position: relative; border:1px solid #E6E6E6; width: 250px;">
            <div class="media-body">
                <h4 class="media-heading">Requisitos</h4>
                <ul>
                    <li><a href="https://tribunet.hacienda.go.cr/principal.htm">Inscribirse en Hacienda</a></li>     
                    <li>ATV Hacienda</li> 
                    <li><a href="https://www.youtube.com/watch?v=7kZcx3dZIHc">Crear llave Criptogr�fica</a></li>
                    <li>Llenar nuestro formulario</li>
                    <li>Preguntas frecuentes</li> 
                    <asp:Button ID="Button3" runat="server" Text="Ver informaci�n"/>
                </ul> 
            </div>
           </div>
        </div>

    </section>
    <section>
        <div class="row marginTop20">
            <div class="col-md-12">
                <h3>�Cu�les beneficios obtiene adquiriendo MSA Factura Electr�nica?</h3>
                <p></p>
            </div>
        </div>
        <div class="row customers">
            <div class="col-md-6">
                <div class="media">
                    <div class="media-left media-middle">
                        <div class="media-object">
                            <img src="<%:Page.ResolveClientUrl("~/Content/Images/Customers/costos01.png")%>" width="100" alt="customer" class="img-circle">
                        </div>
                    </div>
                    <div class="media-body">
                        <h4 class="media-heading">Reducci�n de Costos</h4>
                        <p><small>Utilice la factura electr�nica para ahorrar gastos de papeler�a, espacio para almacenar.  Si el cliente desea puede eliminar por completo el comprobante impreso, ya que la factura es un archivo digital, los datos y transacciones de su facturaci�n se guardan en un archivo digital, y finalmente, el cliente recibe su factura por medio de un correo electr�nico facilitando el env�o de la misma. Tambi�n tiene la posibilidad de consultar dichos documentos en nuestro portal.</small></p>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="media">
                    <div class="media-left media-middle">
                        <div class="media-object">
                            <img src="<%:Page.ResolveClientUrl("~/Content/Images/Customers/rapidez.jpg")%>" width="100" alt="customer" class="img-circle">
                        </div>
                    </div>
                    <div class="media-body">
                        <h4 class="media-heading">Rapidez en emisi�n de los comprobantes</h4>
                        <p><small>Optimiza el tiempo de emisi�n de su facturaci�n, MSAFactura est� creada bajo est�ndares de calidad y seguridad para timbrar sus documentos fiscales.</small></p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row customers">
            <div class="col-md-6">
                <div class="media">
                    <div class="media-left media-middle">
                        <div class="media-object">
                            <img src="<%:Page.ResolveClientUrl("~/Content/Images/Customers/cliente.png")%>" width="100" alt="customer" class="img-circle">
                        </div>
                    </div>
                    <div class="media-body">
                        <h4 class="media-heading">Mejora la atenci�n a sus clientes</h4>
                        <p><small>La herramienta cuenta con portal para el cliente, estos podr�n tramitar sus facturas directamente en el sistema y desde la comodidad de preferencia. Asi mismo, ahorra tiempo y dinero.</small></p>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="media">
                    <div class="media-left media-middle">
                        <div class="media-object">
                            <img src="<%:Page.ResolveClientUrl("~/Content/Images/Customers/auditoria.jpg")%>" width="100" alt="customer" class="img-circle">
                        </div>
                    </div>
                    <div class="media-body">
                        <h4 class="media-heading">Seguridad y trasparencia fiscal</h4>
                        <p><small>La factura electr�nica MSA facilita los procesos contables y fiscales mediante consultas r�pidas, de f�cil acceso por medio del portal o el computador del cliente.</small></p>
                    </div>
                </div>
            </div>
        </div>
    </section>
</div>
</asp:Content>