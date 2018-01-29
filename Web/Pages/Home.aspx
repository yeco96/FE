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
            <p class="mainText">Una factura electr�nica es un documento de uso comercial para trabajar en l�nea con tributaci�n nacional, dicho documento est� en formato electr�nico y podr� sustituir la factura funcional en papel; la factura ser� en enviada v�a correo electr�nico al cliente sin perder validez o bien puede consultarla en nuestro portal.</p>
        </div>
        <div class="col-md-12 marginTop20">
            <a class="btn btn-primary btn-lg" href="#"><i class="glyphicon glyphicon-info-sign"></i> Learn more</a>
        </div>
    </section>
    <section class="row features">
        <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-wrench featureIcon text-primary"></i>
                </div>
            </div>
            <div class="media-body">
                <h4 class="media-heading">Plan Persona Fisica A</h4>
                <ul>
                    <li>Dodumentos Electr�nicos ilimitados</li>
                    <li>Precio Mensual</li>
                    <li>Precio Anual</li> 
                    <li>Precio Adicional por documento</li>
                </ul> 
            </div>
        </div>
                <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-wrench featureIcon text-primary"></i>
                </div>
            </div>
            <div class="media-body">
                <h4 class="media-heading">Plan Persona Fisica B</h4>
                <ul>
                    <li> 100 Dodumentos Electr�nicos</li>
                    <li>Precio Mensual</li>
                    <li>Precio Anual</li> 
                    <li>Precio Adicional por documento</li>
                </ul> 
            </div>
        </div>
                <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-wrench featureIcon text-primary"></i>
                </div>
            </div>
            <div class="media-body">
                <h4 class="media-heading">Plan Pymes A</h4>
                <ul>
                    <li>Dodumentos Electr�nicos ilimitados</li>
                    <li>Precio Mensual</li>
                    <li>Precio Anual</li> 
                    <li>Precio Adicional por documento</li>
                </ul> 
            </div>
        </div>
                <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-wrench featureIcon text-primary"></i>
                </div>
            </div>
            <div class="media-body">
                <h4 class="media-heading">Plan Pymes B</h4>
                <ul>
                    <li> 100 Dodumentos Electr�nicos</li>
                    <li>Precio Mensual</li>
                    <li>Precio Anual</li> 
                    <li>Precio Adicional por documento</li>
                </ul> 
            </div>
        </div>
                <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-wrench featureIcon text-primary"></i>
                </div>
            </div>
            <div class="media-body">
                <h4 class="media-heading">Plan Empresarial A</h4>
                <ul>
                    <li>Dodumentos Electr�nicos ilimitados</li>
                    <li>Precio Mensual</li>
                    <li>Precio Anual</li> 
                    <li>Precio Adicional por documento</li>
                </ul> 
            </div>
        </div>
                <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-wrench featureIcon text-primary"></i>
                </div>
            </div>
            <div class="media-body">
                <h4 class="media-heading">Plan Empresarial  B</h4>
                <ul>
                    <li> 1000 Dodumentos Electr�nicos</li>
                    <li>Precio Mensual</li>
                    <li>Precio Anual</li> 
                    <li>Precio Adicional por documento</li>
                </ul> 
            </div>
        </div>
    </section>
    <section>
        <div class="row marginTop20">
            <div class="col-md-12">
                <h3>�Cu�les beneficios obteniene adquiriendo Factura Electr�nica MSA?</h3>
                <p></p>
            </div>
        </div>
        <div class="row customers">
            <div class="col-md-6">
                <div class="media">
                    <div class="media-left media-middle">
                        <div class="media-object">
                            <img src="<%:Page.ResolveClientUrl("~/Content/Images/Customers/RafaelRaje.jpg")%>" width="100" alt="customer" class="img-circle">
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
                            <img src="<%:Page.ResolveClientUrl("~/Content/Images/Customers/NathanBryant.jpg")%>" width="100" alt="customer" class="img-circle">
                        </div>
                    </div>
                    <div class="media-body">
                        <h4 class="media-heading">Rapidez en emisi�n de los comprobantes</h4>
                        <p><small>Optimiza el tiempo de emisi�n de tu facturaci�n electr�nica, MSAFactura est� creada bajo est�ndares de calidad y seguridad para timbrar tus documentos fiscales.</small></p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row customers">
            <div class="col-md-6">
                <div class="media">
                    <div class="media-left media-middle">
                        <div class="media-object">
                            <img src="<%:Page.ResolveClientUrl("~/Content/Images/Customers/HeidiLopez.jpg")%>" width="100" alt="customer" class="img-circle">
                        </div>
                    </div>
                    <div class="media-body">
                        <h4 class="media-heading">Mejora la atenci�n a sus clientes</h4>
                        <p><small>La herramienta cuenta con portal al cliente, estos podr�n tramitar sus facturas directamente en el sistema y desde la comodidad de preferencia. Asi mismo, ahorra tiempo y dinero.</small></p>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="media">
                    <div class="media-left media-middle">
                        <div class="media-object">
                            <img src="<%:Page.ResolveClientUrl("~/Content/Images/Customers/GaryRubio.jpg")%>" width="100" alt="customer" class="img-circle">
                        </div>
                    </div>
                    <div class="media-body">
                        <h4 class="media-heading">Seguridad y trasparencia fiscal</h4>
                        <p><small>La factura electr�nica MSA facilita los procesos contables y auditor�a fiscal mediante consultas r�pidas, de f�cil acceso por medio del portal o el computador del cliente.</small></p>
                    </div>
                </div>
            </div>
        </div>
    </section>
</div>
</asp:Content>