<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmGenerarDocumento.aspx.cs" Inherits="Web.Pages.Facturacion.FrmGenerarDocumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">


    <dx:ASPxPageControl ID="documento" Width="100%" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True">
        <TabPages>
            <dx:TabPage Text="Emisor">
                <ContentCollection>
                    <dx:ContentControl runat="server">

                        <dx:ASPxFormLayout runat="server" >
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                            <Items>
                                <dx:LayoutGroup Caption="Datos Personales" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                    <Items>
                                        <dx:LayoutItem Caption="Tipo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbEmisorTipo" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Identficación">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxSpinEdit ID="txtEmisorIdentificacion" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" MaxLength="12" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Nombre">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txtEmisorNombre" Width="100%" AutoResizeWithContainer="true" runat="server" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" MaxLength="80" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Nombre Comercial" ColSpan="3" Width="97.5%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txtEmisorNombreComercial" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" MaxLength="80"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>

                        <dx:ASPxFormLayout runat="server">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                            <Items>
                                <dx:LayoutGroup Caption="Contácto" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                    <Items>

                                        <dx:LayoutItem Caption="Teléfono">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                <dx:ASPxComboBox ID="cmbEmisorTelefonoCod" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <dx:ASPxSpinEdit ID="txtEmisorTelefono" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Fax">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                <dx:ASPxComboBox ID="cmbEmisorFaxCod" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <dx:ASPxSpinEdit ID="txtEmisorFax" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Correo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txtEmisorCorreo" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                        ValidationSettings-RegularExpression-ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>

                        <dx:ASPxFormLayout runat="server">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                            <Items>
                                <dx:LayoutGroup Caption="Ubicación" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                    <Items>
                                        <dx:LayoutItem Caption="Provincia">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbEmisorProvincia" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbEmisorProvincia_ValueChanged" AutoPostBack="true"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Canton">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbEmisorCanton" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbEmisorCanton_ValueChanged" AutoPostBack="true"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Distrito">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbEmisorDistrito" Width="100%" AutoResizeWithContainer="true" runat="server" OnValueChanged="cmbEmisorDistrito_ValueChanged" AutoPostBack="true"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Barrio">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbEmisorBarrio" runat="server" Width="100%" AutoResizeWithContainer="true"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Otras Señas" ColSpan="2" Width="100%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxMemo ID="txtEmisorOtraSenas" runat="server" Width="100%" AutoResizeWithContainer="true"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>

                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Receptor">
                <ContentCollection>
                    <dx:ContentControl runat="server">


                        <dx:ASPxFormLayout runat="server" >
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                            <Items>
                                <dx:LayoutGroup Caption="Datos Personales" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                    <Items>
                                        <dx:LayoutItem Caption="Tipo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbReceptorTipo" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Identficación">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxSpinEdit ID="txtReceptorIdentificacion" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" MaxLength="12" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Nombre">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txtReceptorNombre" Width="100%" AutoResizeWithContainer="true" runat="server" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" MaxLength="80" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Nombre Comercial" ColSpan="3" Width="97.5%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txtReceptorNombreComercial" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" MaxLength="80" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>

                        <dx:ASPxFormLayout runat="server">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                            <Items>
                                <dx:LayoutGroup Caption="Contácto" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                    <Items>
                                        <dx:LayoutItem Caption="Teléfono">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                <dx:ASPxComboBox ID="cmbReceptorTelefonoCod" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <dx:ASPxSpinEdit ID="txtReceptorTelefono" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Fax">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 30%;">
                                                                <dx:ASPxComboBox ID="cmbReceptorFaxCod" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                            </td>
                                                            <td style="width: 70%;">
                                                                <dx:ASPxSpinEdit ID="txtReceptorFax" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Correo" >
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txtReceptorCorreo" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                        ValidationSettings-RegularExpression-ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>

                        <dx:ASPxFormLayout runat="server">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                            <Items>
                                <dx:LayoutGroup Caption="Ubicación" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                    <Items>
                                        <dx:LayoutItem Caption="Provincia">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbReceptorProvincia" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbReceptorProvincia_ValueChanged" AutoPostBack="true"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Canton">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbReceptorCanton" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbReceptorCanton_ValueChanged" AutoPostBack="true"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Distrito">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbReceptorDistrito" Width="100%" AutoResizeWithContainer="true" runat="server" OnValueChanged="cmbReceptorDistrito_ValueChanged" AutoPostBack="true"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Barrio">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbReceptorBarrio" runat="server" Width="100%" AutoResizeWithContainer="true" 
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Otras Señas" ColSpan="2" Width="100%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxMemo ID="txtReceptorOtraSenas" runat="server" Width="100%" AutoResizeWithContainer="true"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                       
                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>



                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Factura">
                <ContentCollection>
                    <dx:ContentControl runat="server">


                        <dx:ASPxFormLayout runat="server">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                            <Items>
                                <dx:LayoutGroup Caption="Encabezado" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                    <Items>
                                        <dx:LayoutItem Caption="Condición Venta">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbCondicionVenta" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbCondicionVenta_ValueChanged" AutoPostBack="true"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Plazo Crédito">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxSpinEdit ID="cmbPlazoCredito" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbEmisorCanton_ValueChanged" AutoPostBack="true" MaxLength="2" Enabled="false"
                                                        ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Medio Pago">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="cmbMedioPago" Width="100%" AutoResizeWithContainer="true" runat="server"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Fecha de Emision">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxDateEdit ID="txtFechaEmision" Width="100%" AutoResizeWithContainer="true" runat="server"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                         <dx:LayoutItem Caption="Moneda">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                       <dx:ASPxComboBox ID="cmbTipoMoneda" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" OnValueChanged="cmbMoneda_ValueChanged" AutoPostBack="true"
                                                                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tipo Cambio">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxSpinEdit ID="txtTipoCambio" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"  MinValue="0" MaxValue="999999" Enabled="false" DecimalPlaces="2"
                                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                 
                            </Items>
                        </dx:ASPxFormLayout>


                           <dx:ASPxFormLayout runat="server">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                            <Items>
                                <dx:LayoutGroup Caption="Detalle" ColCount="6" GroupBoxDecoration="Box" UseDefaultPaddings="false" SettingsItemCaptions-Location="Top">
                                    <Items>
                                        <dx:LayoutItem Caption="Cantidad">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxSpinEdit ID="txtCantidad" runat="server" Width="100%" AutoResizeWithContainer="true" MinValue="0" 
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Producto" >
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="ASPxSpinEdit1" runat="server" Width="100%" AutoResizeWithContainer="true" 
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido"  ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Monto">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxSpinEdit ID="txtMonto" Width="100%" AutoResizeWithContainer="true" runat="server" DecimalPlaces="2"  MinValue="0"
                                                        ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Decuento">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxSpinEdit ID="txtDescuento" Width="100%" AutoResizeWithContainer="true" runat="server" DecimalPlaces="2" MinValue="0"
                                                            ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                         <dx:LayoutItem Caption="Naturaleza Descuento">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                       <dx:ASPxTextBox ID="txtDescuentoDescripcion" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"  MaxLength="80"/>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                         <dx:LayoutItem Caption=" ">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                       <dx:ASPxButton ID="bntAgregarLineaDetalle"  Image-Url="~/Content/Images/acept.png" runat="server" Width="50px" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                 
                            </Items>
                        </dx:ASPxFormLayout>


                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>

    <dx:ASPxButton runat="server" ID="btnFacturar" Text="Facturr" OnClick="btnFacturar_Click" CausesValidation="true"></dx:ASPxButton>


</asp:Content>
