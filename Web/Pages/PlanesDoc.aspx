<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeBehind="PlanesDoc.aspx.cs" Inherits="Web.PlanesDoc" %>

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
                <h1>Información de nuestros Planes </h1>
                <a class="btn btn-primary btn-lg" href="Contacts"><i class="glyphicon glyphicon-info-sign"></i> Cotizar Planes</a>
            </div>
                <p class="mainText">Si su empresa factura una cantidad superior indicada en los planes, por favor comunicarse con MSASOFT + (506) 8872 9065 o escribamos al correo msalamanca@msasoft.net</p>
        </section>
        <section class="row features">
            <div class="col-md-4 media">
                
                <div class="span4">
                    <div class="text-box">
                        <div style="text-align: center"; class="text-box-heading">PLAN PREPAGO 1</div>
                        <div style="text-align: center"; class="text-box-heading">¢10,000.00 ANUAL</div>
                        <div class="arrow-down" style="margin-bottom: 10px;"></div>
                           <p><ul>  
                            <li>100 Documentos electrónicos anuales</li>                         
                            <li>Facturas, NC y ND</li> 
                            <li>1 emisor</li>
                            <p>¿Para quién es?</p>
                            <p>Enfocado en profesionales independientes y PYMES</p>
                            <li>Válido por año desde la compra o hasta agotar la cantidad de documentos</li> 
                        </ul></p>
                        <p style="text-align: right; margin-bottom: 0;"><a class="btn btn-primary" href="#" style="pull-right">Comprar</a></p>              
                    </div>
                </div>

            </div>
            <div class="col-md-4 media">
                
                <div class="span4">
                    <div class="text-box">
                        <div style="text-align: center"; class="text-box-heading">PLAN PREPAGO 2</div>
                        <div style="text-align: center"; class="text-box-heading">¢20,000.00 ANUAL</div>
                        <div class="arrow-down" style="margin-bottom: 10px;"></div>
                           <p><ul>  
                            <li>215 Documentos electrónicos anuales</li>                         
                            <li>Facturas, NC y ND</li> 
                            <li>1 emisor</li>
                            <p>¿Para quién es?</p>
                            <p>Enfocado en profesionales independientes y PYMES</p>
                            <li>Válido por año desde la compra o hasta agotar la cantidad de documentos</li> 
                        </ul></p>
                        <p style="text-align: right; margin-bottom: 0;"><a class="btn btn-primary" href="#" style="pull-right">Comprar</a></p>              
                    </div>
                </div>
            </div>

             <div class="col-md-4 media">
                
                <div class="span4">
                    <div class="text-box">
                        <div style="text-align: center"; class="text-box-heading">PLAN PREPAGO 3</div>
                        <div style="text-align: center"; class="text-box-heading">¢30,000.00 ANUAL</div>
                        <div class="arrow-down" style="margin-bottom: 10px;"></div>
                           <p><ul>  
                            <li>330 Documentos electrónicos anuales</li>                         
                            <li>Facturas, NC y ND</li> 
                            <li>1 emisor</li>
                            <p>¿Para quién es?</p>
                            <p>Enfocado en profesionales independientes y PYMES</p>
                            <li>Válido por año desde la compra o hasta agotar la cantidad de documentos</li> 
                        </ul></p>
                        <p style="text-align: right; margin-bottom: 0;"><a class="btn btn-primary" href=#" style="pull-right">Comprar</a></p>              
                    </div>
                </div>
            </div>
            <div class="col-md-4 media">                
                <div class="span4">
                    <div class="text-box">
                        <div style="text-align: center"; class="text-box-heading">PLAN PROFESIONAL</div>
                        <div style="text-align: center"; class="text-box-heading">¢5,000.00 MES ¢50,000.00 ANUAL</div>
                        <div class="arrow-down" style="margin-bottom: 10px;"></div>
                           <p><ul>  
                            <li>Documentos electrónicos sin Límite</li>                         
                            <li>Facturas, NC y ND</li> 
                            <li>1 emisor</li>
                            <p>¿Para quién es?</p>
                            <p>Enfocado para todos los Profesionales independientes que facturen gran cantidad de documentos</p>
                            <li>Válido por año desde la compra</li> 
                        </ul></p>
                        <p style="text-align: right; margin-bottom: 0;"><a class="btn btn-primary" href="#" style="pull-right">Comprar</a></p>              
                    </div>
                </div>
            </div>
            <div class="col-md-4 media">                
                <div class="span4">
                    <div class="text-box">
                        <div style="text-align: center"; class="text-box-heading">PLAN PYMES</div>
                        <div style="text-align: center"; class="text-box-heading">¢10,000.00 MES ¢100,000.00 ANUAL</div>
                        <div class="arrow-down" style="margin-bottom: 10px;"></div>
                           <p><ul>  
                            <li>Entre 1 y 250 documentos mensuales</li>                         
                            <li>Facturas, NC y ND</li> 
                            <li>1 emisor</li>
                            <p>¿Para quién es?</p>
                            <p>Para empresas que facturan aproximadamente 250 documentos mensuales</p>
                            <li>Válido por año desde la compra</li> 
                        </ul></p>
                        <p style="text-align: right; margin-bottom: 0;"><a class="btn btn-primary" href="#" style="pull-right">Comprar</a></p>              
                    </div>
                </div>
            </div>
            <div class="col-md-4 media">                
                <div class="span4">
                    <div class="text-box">
                        <div style="text-align: center"; class="text-box-heading">PLAN EMPRESARIAL</div>
                        <div style="text-align: center"; class="text-box-heading">¢20,000.00 MES ¢200,000.00 ANUAL</div>
                        <div class="arrow-down" style="margin-bottom: 10px;"></div>
                           <p><ul>  
                            <li>Entre 1 y 500 documentos mensuales</li>                         
                            <li>Facturas, NC y ND</li> 
                            <li>1 emisor</li>
                            <p>¿Para quién es?</p>
                            <p>Para empresas que facturan aproximadamente 500 documentos mensuales</p>
                            <li>Válido por año desde la compra</li> 
                        </ul></p>
                        <p style="text-align: right; margin-bottom: 0;"><a class="btn btn-primary" href="#" style="pull-right">Comprar</a></p>              
                    </div>
                </div>
            </div>
             
        </section>               
        <section>
           

 <br/>
            <div class="span4">
                <div class="text-box">
                    <div class="text-box-heading-blue" style="text-align: left;">Para más información</div>
                    <div class="arrow-down-blue" style="margin-bottom: 10px;"></div>
                    <p>
                        <ul>
                            <li>Maikol Salamanca Arias / +(506) 8872-9065 / msalamanca@msasoft.net</li>
                            <li>Manuel Anchia Torrentes / +(506) 6181-8738 / manchia@msasoft.net</li>
                            <li>Alejandro Reyes Pérez / +(506) 6343-1651 / areyes@msasoft.net</li>
                        </ul>
                    </p>

                </div>
            </div> 

        </section>
    </div>
</asp:Content>
