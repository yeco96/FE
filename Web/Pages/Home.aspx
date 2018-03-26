<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeBehind="Home.aspx.cs" Inherits="Web.Home" %>

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
                <p class="mainText">Una factura electrónica es un documento de uso comercial para trabajar en línea con tributación directa, este documento electrónico podrá sustituir la factura tradicional; la factura será enviada vía correo electrónico al cliente sin perder validez ,el usuario después podrá consultarla desde un sitio web donde estarán todos los documentos emitidos.</p>
            </div>
            <div class="col-md-12 marginTop20">
                <a class="btn btn-primary btn-lg" href="Contacts"><i class="glyphicon glyphicon-info-sign"></i> Contáctenos para más información</a>
            </div>
        </section>
        <section class="row features">
            <div class="col-md-4 media">

                <div class="span4">
                    <div class="text-box">
                        <div class="text-box-heading">Facturas Electrónicas </div>
                        <div class="arrow-down" style="margin-bottom: 10px;"></div>
                        <p>Realice su facturación desde internet. </p>
                        <p>¿Si eres trabajador independiente? ó ¿Tiene una PYME y no cuenta con un sistema de facturación o ERP?</p>
                        <p style="text-align: right; margin-bottom: 0;"><a class="btn btn-primary" href="#" style="pull-right" id="yui_3_17_2_1_1517459886390_165">Más Detalles</a></p>
                    </div>
                </div>

            </div>
            <div class="col-md-4 media">

                <div class="span4">
                    <div class="text-box">
                        <div class="text-box-heading">Nuestros Planes</div>
                        <div class="arrow-down" style="margin-bottom: 10px;"></div>
                        <p>
                            <ul>
                                <li>Planes Prepago</li>
                                <li>Plan Profesional Ilimitado</li>
                                <li>Plan Pymes</li>
                                <li>Plan Cliente Jurídico I y II</li>
                                <li>Planes Empresariales</li>
                            </ul>
                        </p>
                        <p style="text-align: right; margin-bottom: 0;"><a href="/Pages/PlanesDoc.aspx" class="btn btn-primary">Más Detalles</a></p>
                    </div>
                </div>
            </div>

            <div class="col-md-4 media">
                <div class="span4">
                    <div class="text-box">
                        <div class="text-box-heading">Requisitos</div>
                        <div class="arrow-down" style="margin-bottom: 10px;"></div>
                        <p>
                            <ul>
                                <li><a href="https://tribunet.hacienda.go.cr/principal.html">Inscribirse en Hacienda</a></li>
                                <li><a href="https://www.hacienda.go.cr/atv/login.aspx">ATV Hacienda </a></li>
                                <li><a href="https://www.youtube.com/watch?v=7kZcx3dZIHc">Crear llave Criptográfica</a></li>
                                <li>Llenar nuestro formulario</li>
                                <li>Preguntas frecuentes</li>
                            </ul>
                        </p>
                        <p style="text-align: right; margin-bottom: 0;"><a class="btn btn-primary" href="#" style="pull-right">Más Detalles</a></p>
                    </div>
                </div>
            </div>

        </section>
        <section>
            <div class="row marginTop20">
                <div class="col-md-12">
                    <h3>¿Cuáles beneficios obtiene adquiriendo MSAFactura?</h3>
                    <p></p>
                </div>
            </div>
            <div class="row customers">
                <div class="col-md-6">
                    <div class="media">
                        <div class="media-left media-middle">
                            <div class="media-object">
                                <img src="<%:Page.ResolveClientUrl("~/Content/Images/Customers/costos5.jpg")%>" width="100" alt="customer" class="img-circle">
                            </div>
                        </div>
                        <div class="media-body">
                            <h4 class="media-heading">Reducción de Costos</h4>
                            <p><small>Utilice la factura electrónica para ahorrar gastos de papelería y espacio para almacenar. Si el cliente desea puede continuar utilizando el comprobante impreso sin embargo es suficiente con emitir o enviar por correo electrónico el documento digital que guardará los datos y transacciones de su facturación, posteriormente tiene la posibilidad de consultar todos los documentos emitidos desde un sitio web.</small></p>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="media">
                        <div class="media-left media-middle">
                            <div class="media-object">
                                <img src="<%:Page.ResolveClientUrl("~/Content/Images/Customers/Fast2.png")%>" width="100" alt="customer" class="img-circle">
                            </div>
                        </div>
                        <div class="media-body">
                            <h4 class="media-heading">Rapidez en emisión de los comprobantes</h4>
                            <p><small>Optimiza el tiempo de emisión de su facturación, MSAFactura está creada bajo estándares de calidad y seguridad para timbrar sus documentos fiscales.</small></p>
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
                            <h4 class="media-heading">Mejora la atención a sus clientes</h4>
                            <p><small>La herramienta cuenta con sitio web para el cliente, estos podrán tramitar sus facturas directamente en el sistema desde internet.Así mismo, ahorra tiempo y dinero.</small></p>
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
                            <h4 class="media-heading">Seguridad y transparencia fiscal</h4>
                            <p><small>MSAFactura facilita los procesos contables y fiscales mediante consultas rápidas de todos los documentos electrónicos emitidos.</small></p>
                        </div>
                    </div>
                </div>
            </div>
            <footer>
                <div class="footerUp">
                    <div class="container">
                        <div class="row">


                            <div class="col-md-4 media">
                                <div class="span4">
                                    <div class="text-box">
                                        <div class="text-box-heading-blue">¿Quienes Somos?</div>
                                        <div class="arrow-down-blue" style="margin-bottom: 10px;"></div>
                                        <p>MSA SOFT es una empresa dedicada a la implementación de soluciones a la medida, somos la mejor opción para el éxito de su proyecto.</p>

                                    </div>
                                </div>
                            </div>



                            <div class="col-md-4 media">
                                <div class="span4">
                                    <div class="text-box">
                                        <div class="text-box-heading-blue">Nuestros Productos</div>
                                        <div class="arrow-down-blue" style="margin-bottom: 10px;"></div>
                                        <p>
                                            <ul class="list-unstyled latestPosts">
                                                <li>Factura Electrónica</li>
                                                <li>Sistema de Matrícula</li>
                                                <li>Sistema de Planillas</li>
                                                <li>Soporte Técnico y Consultorías</li>
                                            </ul>
                                        </p>

                                    </div>
                                </div>
                            </div>



                            <div class="col-md-4 media">
                                <div class="span4">
                                    <div class="text-box">
                                        <div class="text-box-heading-blue">Contacto</div>
                                        <div class="arrow-down-blue" style="margin-bottom: 10px;"></div>
                                        <p>
                                            <ul class="list-unstyled">
                                                <li>Heredia</li>
                                                <li>Costa Rica</li>
                                                <li>Teléfono: +(506) 8872 9065</li>
                                                <li>Correo: msalamanca@msasoft.com</li>
                                            </ul>
                                        </p>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </footer>
        </section>
    </div>
</asp:Content>
