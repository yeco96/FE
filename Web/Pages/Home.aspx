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
            <p class="mainText">Es un documento comercial con efectos tributarios, generado, expresado y transmitido en formato electrónico, ésta sustituye por igual a la ya tradicional factura en papel y la puede utilizar cualquier tipo de empresa. Los documentos con los que es permitido trabajar son los siguientes: facturas, notas de crédito, notas de débito, tiquetes electrónicos y acuses de aceptación o rechazo de documentos.</p>
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
                <h4 class="media-heading">Basico</h4>
                <ul>
                    <li>Facturas ilimitadas</li>
                    <li>Notas de crédito ilimitadas</li>
                    <li>Notas de debito ilimitadas</li> 
                    <li>Soporte 8am a 5pm</li>
                </ul> 
            </div>
        </div>
        <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-cog featureIcon text-primary"></i>
                </div>
            </div>
            <div class="media-body">
                <h4 class="media-heading">Profesional</h4>
                <ul>
                    <li>Facturas ilimitadas</li>
                    <li>Notas de crédito ilimitadas</li>
                    <li>Notas de debito ilimitadas</li>
                    <li>Logo en la factura</li>
                    <li>Soporte 24/7</li>
                </ul> 
            </div>
        </div>
        <div class="col-md-4 media">
            <div class="media-left">
                <div class="media-object">
                    <i class="glyphicon glyphicon-phone featureIcon text-primary"></i>
                </div>
            </div>
            <div class="media-body">
                <h4 class="media-heading">Empresarial</h4>
                <ul>
                    <li>Facturas ilimitadas</li>
                    <li>Notas de crédito ilimitadas</li>
                    <li>Notas de debito ilimitadas</li>
                    <li>Logo en la factura</li>
                    <li>API de interconexión</li>
                    <li>Soporte 24/7</li>
                </ul> 
            </div>
        </div>
    </section>
    <section>
        <div class="row marginTop20">
            <div class="col-md-12">
                <h3>¿Qué beneficios puedo obtener si implemento Factura Electrónica?</h3>
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
                        <h4 class="media-heading">Reducción del costo de tus gastos de facturación</h4>
                        <p><small>La factura electrónica permite ahorrar en gastos de papelería, almacenaje y en envíos. Ya que el comprobante se entrega al cliente en un archivo digital, la impresión de éste se evita; el almacenaje de la información sobre transacciones se guarda en un archivo digital, y finalmente, el cliente recibe su factura por medio de un correo electrónico facilitando el envío de la misma.</small></p>
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
                        <h4 class="media-heading">Seguridad y rapidez en la emisión de comprobantes</h4>
                        <p><small>Al realizar tu facturación electrónica con un PAC autorizado, estás contratando los servicios de una empresa que cumple con todos los requisitos impuestos por el SAT en materia de seguridad. En cuanto al tiempo de emisión de tu factura, ioFacturo tarda menos de un segundo en timbrar tus documentos fiscales.</small></p>
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
                        <h4 class="media-heading">Mejorar el servicio al cliente</h4>
                        <p><small>Al contar con un portal al cliente, estos podrán tramitar sus facturas directamente en el sistema y desde la comodidad de sus casas u oficinas. Así mismo el ahorro de tiempo y dinero te permitirá una mejor reinversión del mismo en actividades de servicio al cliente.</small></p>
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
                        <h4 class="media-heading">Facilita los procesos de auditoría</h4>
                        <p><small>La factura electrónica facilita los procesos de auditoría ya que permite la búsqueda y localización rápida de la información fiscal dentro del archivero digital que el prestador de servicios mantiene en su computadora.</small></p>
                    </div>
                </div>
            </div>
        </div>
    </section>
</div>
</asp:Content>