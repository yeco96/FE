<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmGenerarNota.aspx.cs" Inherits="Web.Pages.Facturacion.FrmGenerarNota" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     
     <div class="text-box-title">
        <div class="text-box-heading-title">Nota Electrónica</div>
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
                    <dx:LayoutGroup Caption="Datos del Documento de Referencia" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                        <Items>

                            <dx:LayoutItem Caption="Tipo Documento">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cmbTipoDocumento" runat="server" Width="100%" AutoResizeWithContainer="true" ReadOnly="true">
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Sucursal y Caja">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cmbSucursalCaja" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Consecutivo">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtConsecutivo" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Clave" Visible="false">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtClave" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Fecha">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtFechaEmisor" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>


                            <dx:LayoutItem Caption="Referencia">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cmbCodigoReferencia" AutoPostBack="true" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Razón">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtRazón" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" />
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>

                    <%-- Datos del Emisor --%>
                    <dx:LayoutGroup Caption="Datos del Documento" ColCount="1" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                        <Items>

                            <dx:LayoutItem Caption="Emisor">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="txtNombreEmisor" runat="server" Width="100%">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>


                            <dx:LayoutItem Caption="Receptor">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel ID="txtNombreReceptor" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Correo electrónico Receptor">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtCorreoReceptor" runat="server" Width="100%" AutoResizeWithContainer="true" MaxLength="80">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Requerido">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>

                    </dx:LayoutGroup>

                </Items>

            </dx:ASPxFormLayout>

            <script type="text/javascript">
                function cambioPrecio(s, e) {
                    var monto = s.GetText().split("₵")[1];
                    monto = monto.replace(",", "").replace(".", "");
                    if (isNaN(monto)) {
                        ASPxGridView1.SetEditValue("precioUnitario", 0);
                    } else {
                        ASPxGridView1.SetEditValue("precioUnitario", monto / 100);
                    }
                }
            </script>

            <%-- Grid para datos de la factura --%>
            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
                Width="100%" EnableTheming="True" KeyFieldName="codigo.codigo" Theme="Moderno"
                OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize"
                OnRowDeleting="ASPxGridView1_RowDeleting"
                OnRowUpdating="ASPxGridView1_RowUpdating">
                <Columns>
                    <dx:GridViewCommandColumn Width="100px" ButtonType="Image" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="false" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                    </dx:GridViewCommandColumn>

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

                <SettingsBehavior ColumnResizeMode="NextColumn" />
                <Settings ShowFooter="True" ShowFilterBar="Hidden" ShowFilterRow="false" />
                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                <SettingsPager PageSize="10" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Items="10, 20, 50, 100">
                    <PageSizeItemSettings Items="10, 20, 50, 100" Visible="True"></PageSizeItemSettings>
                </SettingsPager>
                <SettingsEditing Mode="EditFormAndDisplayRow" />
                <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="false" ShowFilterRow="false" ShowTitlePanel="True" UseFixedTableLayout="True" />
                <SettingsContextMenu EnableColumnMenu="True" Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="false" EnableRowMenu="True" />

                <SettingsCommandButton>
                    <NewButton Image-ToolTip="Nuevo" Image-Url="~/Content/Images/add.png" />
                    <EditButton Image-ToolTip="Modificar" Image-Url="~/Content/Images/edit.png" />
                    <DeleteButton Image-ToolTip="Eliminar" Image-Url="~/Content/Images/delete.png" />
                    <ClearFilterButton Image-ToolTip="Quitar filtros" Image-Url="~/Content/Images/refresh.png" />
                    <UpdateButton ButtonType="Link" Image-ToolTip="Guardar cambios y cerrar formulario de edición" Image-Url="~/Content/Images/acept.png" />
                    <CancelButton ButtonType="Link" Image-ToolTip="Cerrar el formulario de edición sin guardar los cambios" Image-Url="~/Content/Images/cancel.png" />
                </SettingsCommandButton>

                <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800" AllowOnlyOneAdaptiveDetailExpanded="true" AdaptiveDetailColumnCount="1"></SettingsAdaptivity>
                <EditFormLayoutProperties>
                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                </EditFormLayoutProperties>
                <Styles>
                    <Cell Wrap="False"></Cell>
                    <AlternatingRow Enabled="true" />
                </Styles>
                <Templates>
                    <EditForm>
                        <div style="padding: 4px 4px 3px 4px">
                            <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" Theme="Moderno">
                                <TabPages>
                                    <dx:TabPage Text="Datos" Visible="true">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server">
                                                <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server" />
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                        </div>
                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server" />
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server" />
                        </div>
                    </EditForm>
                </Templates>
                <Border BorderWidth="0px" />
                <BorderBottom BorderWidth="1px" />

            </dx:ASPxGridView>



            <dx:ASPxButton runat="server" ID="btnCrearNota" Text="Crear Nota" OnClick="btnCrearNota_Click" CausesValidation="true"></dx:ASPxButton>


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
