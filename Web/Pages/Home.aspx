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
            <h1>¿Qué es la Factura Electrónica?</h1>
            <p class="mainText">Una factura electrónica es un documento de uso comercial para trabajar en línea con tributación nacional, dicho documento está en formato electrónico y podrá sustituir la factura funcional en papel; la factura será en enviada vía correo electrónico al cliente sin perder validez o bien puede consultarla en nuestro portal.</p>
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
                    <li>Dodumentos Electrónicos ilimitados</li>
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
                    <li> 100 Dodumentos Electrónicos</li>
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
                    <li>Dodumentos Electrónicos ilimitados</li>
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
                    <li> 100 Dodumentos Electrónicos</li>
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
                    <li>Dodumentos Electrónicos ilimitados</li>
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
                    <li> 1000 Dodumentos Electrónicos</li>
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
                <h3>¿Cuáles beneficios obteniene adquiriendo Factura Electrónica MSA?</h3>
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
                        <h4 class="media-heading">Reducción de Costos</h4>
                        <p><small>Utilice la factura electrónica para ahorrar gastos de papelería, espacio para almacenar.  Si el cliente desea puede eliminar por completo el comprobante impreso, ya que la factura es un archivo digital, los datos y transacciones de su facturación se guardan en un archivo digital, y finalmente, el cliente recibe su factura por medio de un correo electrónico facilitando el envío de la misma. También tiene la posibilidad de consultar dichos documentos en nuestro portal.</small></p>
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
                        <h4 class="media-heading">Rapidez en emisión de los comprobantes</h4>
                        <p><small>Optimiza el tiempo de emisión de tu facturación electrónica, MSAFactura está creada bajo estándares de calidad y seguridad para timbrar tus documentos fiscales.</small></p>
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
                        <h4 class="media-heading">Mejora la atención a sus clientes</h4>
                        <p><small>La herramienta cuenta con portal al cliente, estos podrán tramitar sus facturas directamente en el sistema y desde la comodidad de preferencia. Asi mismo, ahorra tiempo y dinero.</small></p>
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
                        <p><small>La factura electrónica MSA facilita los procesos contables y auditoría fiscal mediante consultas rápidas, de fácil acceso por medio del portal o el computador del cliente.</small></p>
                    </div>
                </div>
            </div>
        </div>
    </section>
</div>
</asp:Content>