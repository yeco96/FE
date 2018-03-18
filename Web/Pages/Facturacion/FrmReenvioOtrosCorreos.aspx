<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmReenvioOtrosCorreos.aspx.cs" Inherits="Web.Pages.Facturacion.FrmReenvioOtrosCorreos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

     <div class="text-box-title">
        <div class="text-box-heading-title"> Reenvio Documento Electrónico</div>
        <div class="arrow-down-title" style="margin-bottom: 5px;"></div>   
     </div>  
      
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always" OnUnload="UpdatePanel_Unload">
        <ContentTemplate>
            <div id="alertMessages" role="alert" runat="server" />
            <dx:ASPxFormLayout runat="server" AlignItemCaptionsInAllGroups="true">
                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                <Items>
                    <%-- Datos del docuemento de referencia --%>

                    <dx:LayoutGroup Caption="Encabezado" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                        <Items>

                            <dx:LayoutItem Caption="Tipo Documento">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cmbTipoDocumento" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false">
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Moneda">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cmbTipoMoneda" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Tipo Cambio">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtTipoCambio" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>


                            <dx:LayoutItem Caption="Medio Pago">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cmbMedioPago" Width="100%" AutoResizeWithContainer="true" runat="server" Enabled="false" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Condición Venta">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cmbCondicionVenta" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Plazo Crédito">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxSpinEdit AllowMouseWheel="false" ID="txtPlazoCredito" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Fecha de Emision">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="txtFechaEmision" runat="server" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Detalle" ColSpan="3" Width="100%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="txtOtrosTextos" runat="server" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>


                            <dx:LayoutItem Caption="Emisor" ColSpan="3" Width="100%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="txtNombreEmisor" runat="server" Width="100%">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>


                            <dx:LayoutItem Caption="Receptor" ColSpan="3" Width="100%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="txtNombreReceptor" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Correos" ColSpan="3" Width="100%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTokenBox ID="txtCorreos" runat="server" Width="100%" AutoResizeWithContainer="true"
                                            HelpText="Puede agregar más direcciones de correo separadas por ',' " />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>

                    </dx:LayoutGroup>

                </Items>

            </dx:ASPxFormLayout>


            <div class="borde_redondo_tabla">
                <%-- Grid para datos de la factura --%>
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
                    Width="100%" EnableTheming="True" KeyFieldName="codigo.codigo"
                    OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize">
                    <Columns>
                        <dx:GridViewDataSpinEditColumn Caption="Cantidad" FieldName="cantidad" VisibleIndex="2" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                            <PropertiesSpinEdit DisplayFormatString="g" DecimalPlaces="2" MaxLength="10">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Producto" FieldName="codigo.codigo" VisibleIndex="3" PropertiesComboBox-ClientSideEvents-ValueChanged="function(s,e){cambioPrecio(s,e);}"
                            PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                        </dx:GridViewDataComboBoxColumn>

                        <dx:GridViewDataSpinEditColumn Caption="Precio U" FieldName="precioUnitario" VisibleIndex="4" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>

                        <dx:GridViewDataSpinEditColumn Caption="SubTotal" FieldName="montoTotal" VisibleIndex="5" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>


                        <dx:GridViewDataSpinEditColumn Caption="Descuento" FieldName="montoDescuento" VisibleIndex="6" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>

                        <dx:GridViewDataTextColumn Caption="Naturaleza Descuento" FieldName="naturalezaDescuento" VisibleIndex="7" PropertiesTextEdit-MaxLength="80"
                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataSpinEditColumn Caption="Total" FieldName="subTotal" VisibleIndex="8" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>

                    </Columns>
                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="subTotal" SummaryType="Sum" />
                        <dx:ASPxSummaryItem FieldName="montoDescuento" SummaryType="Sum" />
                        <dx:ASPxSummaryItem FieldName="montoTotal" SummaryType="Sum" />
                    </TotalSummary>
                    <Settings ShowFooter="true" />
                </dx:ASPxGridView>
                <br />
                <dx:ASPxButton runat="server" ID="btnEnviarCorreo" Text="Enviar" OnClick="btnEnviarCorreo_Click" ></dx:ASPxButton>
                <dx:ASPxButton runat="server" ID="btnRecresar" Text="Regresar" OnClick="btnRecresar_Click" ></dx:ASPxButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div style="text-align: center;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
            <ProgressTemplate>
                <div class="modalBlock">
                    <div class="centerBlock">
                        <img src="../../Content/Images/cargando.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

</asp:Content>
