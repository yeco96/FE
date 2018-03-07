<%@ Page Async="true" Title="" Culture="es-CR" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmGenerarDocumento.aspx.cs" Inherits="Web.Pages.Facturacion.FrmGenerarDocumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <section class="featured">
        <div class="content-wrapper">
            Documento Electrónico
        </div>
    </section>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always" OnUnload="UpdatePanel_Unload">
        <ContentTemplate>
            <div id="alertMessages" role="alert" runat="server" />
            <dx:ASPxPageControl ID="documento" Width="100%" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                <TabPages> 
                    <dx:TabPage Text="Receptor">
                        <ContentCollection>
                            <dx:ContentControl runat="server">

                                <dx:ASPxFormLayout runat="server"  AlignItemCaptionsInAllGroups="true" >
                                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                    <Items>
                                        <dx:LayoutGroup Caption="Datos Personales" ColCount="4" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                            <Items>
                                                <dx:LayoutItem Caption="Tipo">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbReceptorTipo" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                                ValidationSettings-RequiredField-IsRequired="false" ValidationSettings-RequiredField-ErrorText="Requerido" >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" />
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Identificación">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>

                                                            <table>
                                                                <tr>
                                                                    <td style="width: 90%;">
                                                                       <dx:ASPxTextBox ID="txtReceptorIdentificacion" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                                ValidationSettings-RequiredField-IsRequired="false" ValidationSettings-RequiredField-ErrorText="Requerido" MaxLength="12" 
                                                                           onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);">
                                                                           <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                               <RequiredField ErrorText="Requerido" />
                                                                           </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td style="width: 10%;"> 
                                                                       <dx:ASPxButton runat="server" ToolTip="Búscar" Image-AlternateText="Búscar" ID="btnBuscarReceptor" CssClass="imagen" CausesValidation="false" OnClick="btnBuscarReceptor_Click"  Image-Url="~/Content/Images/loadUser.png"   Image-Height="20px">
                                                                           <Image AlternateText="Búscar" Height="20px" Url="~/Content/Images/loadUser.png">
                                                                           </Image>
                                                                        </dx:ASPxButton> 
                                                                    </td>
                                                                </tr>
                                                            </table> 
                                                           
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Nombre">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="txtReceptorNombre" Width="100%" AutoResizeWithContainer="true" runat="server" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                                ValidationSettings-RequiredField-IsRequired="false" ValidationSettings-RequiredField-ErrorText="Requerido" MaxLength="80" >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" />
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Nombre Comercial" >
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="txtReceptorNombreComercial" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" MaxLength="80" >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
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
                                                                        <dx:ASPxComboBox ID="cmbReceptorTelefonoCod" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" >
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td style="width: 70%;">
                                                                        <dx:ASPxSpinEdit  AllowMouseWheel="false" ID="txtReceptorTelefono" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"  MaxLength="20"  >
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxSpinEdit>
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
                                                                        <dx:ASPxComboBox ID="cmbReceptorFaxCod" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" >
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td style="width: 70%;">
                                                                        <dx:ASPxSpinEdit  AllowMouseWheel="false" ID="txtReceptorFax" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"  MaxLength="20">
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Correo">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="txtReceptorCorreo" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                                ValidationSettings-RegularExpression-ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*">
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RegularExpression ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*" />
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
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
                                        <dx:LayoutGroup Caption="Ubicación" ColCount="4" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                            <Items>
                                                <dx:LayoutItem Caption="Provincia">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbReceptorProvincia" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbReceptorProvincia_ValueChanged" AutoPostBack="true"
                                                                ValidationSettings-ErrorDisplayMode="ImageWithTooltip" >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Cantón">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbReceptorCanton" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbReceptorCanton_ValueChanged" AutoPostBack="true"
                                                                ValidationSettings-ErrorDisplayMode="ImageWithTooltip" >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Distrito">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbReceptorDistrito" Width="100%" AutoResizeWithContainer="true" runat="server" OnValueChanged="cmbReceptorDistrito_ValueChanged" AutoPostBack="true"
                                                                ValidationSettings-ErrorDisplayMode="ImageWithTooltip" >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Barrio">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbReceptorBarrio" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Otras Señas" ColSpan="4" Width="100%">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxMemo ID="txtReceptorOtraSenas" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                </ValidationSettings>
                                                            </dx:ASPxMemo>
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

                     <dx:TabPage Text="Referencias">
                        <ContentCollection>
                            <dx:ContentControl runat="server">

                                  <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView2" KeyboardSupport="True"
                                    Width="100%" EnableTheming="True" KeyFieldName="numero" Theme="Moderno" 
                                      OnCellEditorInitialize="ASPxGridView2_CellEditorInitialize"
                                    OnRowDeleting="ASPxGridView2_RowDeleting" 
                                    OnRowInserting="ASPxGridView2_RowInserting"
                                    OnRowUpdating="ASPxGridView2_RowUpdating">
                                    <Columns>
                                        <dx:GridViewCommandColumn Width="100px" ButtonType="Image" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                                        </dx:GridViewCommandColumn>

                                        <dx:GridViewDataComboBoxColumn Caption="Tipo Documento" FieldName="tipoDocumento" VisibleIndex="1" PropertiesComboBox-ClientSideEvents-ValueChanged="function(s,e){cambioPrecio(s,e);}"
                                            PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesComboBox>
                                                <ClientSideEvents ValueChanged="function(s,e){cambioPrecio(s,e);}" />
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>

                                        <dx:GridViewDataTextColumn Caption="Número" FieldName="numero" VisibleIndex="2" PropertiesTextEdit-MaxLength="50"  PropertiesTextEdit-MaskSettings-Mask="##################################################"
                                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesTextEdit MaxLength="50">
                                                <MaskSettings Mask="##################################################" />
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                         
                                       <dx:GridViewDataTextColumn Caption="Fecha Emisión" FieldName="fechaEmision" VisibleIndex="3"  PropertiesTextEdit-MaskSettings-Mask="####-##-##"
                                             PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                           <PropertiesTextEdit>
                                               <MaskSettings Mask="####-##-##" />
                                               <ValidationSettings>
                                                   <RequiredField ErrorText="Requerido" IsRequired="True" />
                                               </ValidationSettings>
                                           </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                         
                                         <dx:GridViewDataComboBoxColumn Caption="Código" FieldName="codigo" VisibleIndex="4" 
                                            PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                                             <PropertiesComboBox>
                                                 <ValidationSettings>
                                                     <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                 </ValidationSettings>
                                             </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>

                                        <dx:GridViewDataTextColumn Caption="Razón" FieldName="razon" VisibleIndex="5" PropertiesTextEdit-MaxLength="80"
                                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesTextEdit MaxLength="80">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                         
                                         
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
                                        <NewButton Image-ToolTip="Nuevo" Image-Url="~/Content/Images/add.png" >
                                            <Image ToolTip="Nuevo" Url="~/Content/Images/add.png">
                                            </Image>
                                        </NewButton>
                                        <EditButton Image-ToolTip="Modificar" Image-Url="~/Content/Images/edit.png" >
                                            <Image ToolTip="Modificar" Url="~/Content/Images/edit.png">
                                            </Image>
                                        </EditButton>
                                        <DeleteButton Image-ToolTip="Eliminar" Image-Url="~/Content/Images/delete.png" >
                                            <Image ToolTip="Eliminar" Url="~/Content/Images/delete.png">
                                            </Image>
                                        </DeleteButton>
                                        <ClearFilterButton Image-ToolTip="Quitar filtros" Image-Url="~/Content/Images/refresh.png" >
                                            <Image ToolTip="Quitar filtros" Url="~/Content/Images/refresh.png">
                                            </Image>
                                        </ClearFilterButton>
                                        <UpdateButton ButtonType="Link" Image-ToolTip="Guardar cambios y cerrar formulario de edición" Image-Url="~/Content/Images/acept.png" >
                                            <Image ToolTip="Guardar cambios y cerrar formulario de edición" Url="~/Content/Images/acept.png">
                                            </Image>
                                        </UpdateButton>
                                        <CancelButton ButtonType="Link" Image-ToolTip="Cerrar el formulario de edición sin guardar los cambios" Image-Url="~/Content/Images/cancel.png" >
                                            <Image ToolTip="Cerrar el formulario de edición sin guardar los cambios" Url="~/Content/Images/cancel.png">
                                            </Image>
                                        </CancelButton>
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

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>

                    <dx:TabPage Text="Factura">
                        <ContentCollection>
                            <dx:ContentControl runat="server">


                                <dx:ASPxFormLayout runat="server"  AlignItemCaptionsInAllGroups="true" >
                                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                    <Items>
                                        <dx:LayoutGroup Caption="Encabezado" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                            <Items>

                                                 <dx:LayoutItem Caption="Tipo Documento">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbTipoDocumento" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                                ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido">
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                 <dx:LayoutItem Caption="Sucursal y Caja" >
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbSucursalCaja" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                                ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido">
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                  <dx:LayoutItem Caption="" >
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                           
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>


                                                <dx:LayoutItem Caption="Medio Pago">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbMedioPago" Width="100%" AutoResizeWithContainer="true" runat="server"
                                                                ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Condición Venta">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbCondicionVenta" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbCondicionVenta_ValueChanged" AutoPostBack="true"
                                                                ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Plazo Crédito">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxSpinEdit  AllowMouseWheel="false" ID="txtPlazoCredito" runat="server" Width="100%" AutoResizeWithContainer="true"  MaxLength="2" Enabled="false"
                                                                ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                </ValidationSettings>
                                                            </dx:ASPxSpinEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Fecha de Emision">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxDateEdit ID="txtFechaEmision" Width="100%" AutoResizeWithContainer="true" runat="server" EditFormatString="yyyy-MM-dd HH:mm:ss"
                                                                ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </dx:ASPxDateEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Moneda">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbTipoMoneda" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" OnValueChanged="cmbMoneda_ValueChanged" AutoPostBack="true"
                                                                ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido">
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tipo Cambio">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="txtTipoCambio" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" Enabled="false" 
                                                                ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido"  >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Detalle" ColSpan="3" Width="100%">
                                                    <LayoutItemNestedControlCollection >
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox ID="txtOtros" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"  MaxLength="200" > 
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
                                        var monto = s.GetText().split("-")[1];
                                        monto = monto.replace(",", "").replace(".", "");
                                        if (isNaN(monto)) {
                                            ASPxGridView1.SetEditValue("precioUnitario", 0);
                                        } else {
                                            ASPxGridView1.SetEditValue("precioUnitario", monto / 100);
                                        }
                                    }
                                </script>

                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
                                    Width="100%" EnableTheming="True" KeyFieldName="producto" Theme="Moderno"
                                    OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize"
                                    OnRowDeleting="ASPxGridView1_RowDeleting"
                                    OnRowInserting="ASPxGridView1_RowInserting"
                                    OnRowUpdating="ASPxGridView1_RowUpdating">
                                    <Columns>
                                        <dx:GridViewCommandColumn Width="100px" ButtonType="Image" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                                        </dx:GridViewCommandColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="Cantidad" FieldName="cantidad" VisibleIndex="2" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesSpinEdit DisplayFormatString="g" DecimalPlaces="2" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Producto" FieldName="producto" VisibleIndex="3" PropertiesComboBox-ClientSideEvents-ValueChanged="function(s,e){cambioPrecio(s,e);}"
                                            PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesComboBox>
                                                <ClientSideEvents ValueChanged="function(s,e){cambioPrecio(s,e);}" />
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="Precio U" FieldName="precioUnitario" VisibleIndex="4" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                                            PropertiesSpinEdit-AllowMouseWheel="false"
                                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="SubTotal" FieldName="montoTotal" VisibleIndex="5" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            EditFormSettings-Visible="False"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="Descuento" FieldName="montoDescuento" VisibleIndex="6" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                                            PropertiesSpinEdit-AllowMouseWheel="false"
                                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataTextColumn Caption="Naturaleza Descuento" FieldName="naturalezaDescuento" VisibleIndex="7" PropertiesTextEdit-MaxLength="80"
                                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesTextEdit MaxLength="80">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>


                                        <dx:GridViewDataSpinEditColumn Caption="Total" FieldName="subTotal" VisibleIndex="8" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                                            EditFormSettings-Visible="False"
                                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>


                                          <dx:GridViewDataSpinEditColumn Caption="Total + Impuestos" FieldName="montoTotalLinea" VisibleIndex="8" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                              EditFormSettings-Visible="False"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                         
                                              <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                  <ValidationSettings>
                                                      <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                  </ValidationSettings>
                                            </PropertiesSpinEdit>

                                        </dx:GridViewDataSpinEditColumn>
                                         
                                    </Columns>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="subTotal" SummaryType="Sum" />
                                        <dx:ASPxSummaryItem FieldName="montoDescuento" SummaryType="Sum" />
                                        <dx:ASPxSummaryItem FieldName="montoTotal" SummaryType="Sum" />
                                        <dx:ASPxSummaryItem FieldName="montoTotalLinea" SummaryType="Sum" />
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
                                        <NewButton Image-ToolTip="Nuevo" Image-Url="~/Content/Images/add.png" >
                                            <Image ToolTip="Nuevo" Url="~/Content/Images/add.png">
                                            </Image>
                                        </NewButton>
                                        <EditButton Image-ToolTip="Modificar" Image-Url="~/Content/Images/edit.png" >
                                            <Image ToolTip="Modificar" Url="~/Content/Images/edit.png">
                                            </Image>
                                        </EditButton>
                                        <DeleteButton Image-ToolTip="Eliminar" Image-Url="~/Content/Images/delete.png" >
                                            <Image ToolTip="Eliminar" Url="~/Content/Images/delete.png">
                                            </Image>
                                        </DeleteButton>
                                        <ClearFilterButton Image-ToolTip="Quitar filtros" Image-Url="~/Content/Images/refresh.png" >
                                            <Image ToolTip="Quitar filtros" Url="~/Content/Images/refresh.png">
                                            </Image>
                                        </ClearFilterButton>
                                        <UpdateButton ButtonType="Link" Image-ToolTip="Guardar cambios y cerrar formulario de edición" Image-Url="~/Content/Images/acept.png" >
                                            <Image ToolTip="Guardar cambios y cerrar formulario de edición" Url="~/Content/Images/acept.png">
                                            </Image>
                                        </UpdateButton>
                                        <CancelButton ButtonType="Link" Image-ToolTip="Cerrar el formulario de edición sin guardar los cambios" Image-Url="~/Content/Images/cancel.png" >
                                            <Image ToolTip="Cerrar el formulario de edición sin guardar los cambios" Url="~/Content/Images/cancel.png">
                                            </Image>
                                        </CancelButton>
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


                                                        <dx:TabPage Text="Exoneración" Visible="true">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                   
                                                                      <dx:ASPxFormLayout runat="server" ID="formLayoutExoneracion">
                                                                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                                                            <Items>
                                                                                <dx:LayoutGroup Caption="Exoneración" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                                                                    <Items>
                                                                                        <dx:LayoutItem Caption="Tipo Documento">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer>
                                                                                                    <dx:ASPxComboBox ID="cmbTipoDocumento" runat="server" Width="100%" AutoResizeWithContainer="true" 
                                                                                                        ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                        <dx:LayoutItem Caption="Número Documento">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer>
                                                                                                     <dx:ASPxTextBox ID="numeroDocumento" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                        <dx:LayoutItem Caption="Nombre Institución">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer>
                                                                                                     <dx:ASPxTextBox ID="nombreInstitucion" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                        <dx:LayoutItem Caption="Fecha Emisión">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer>
                                                                                                     <dx:ASPxDateEdit ID="fechaEmision" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" EditFormatString="yyyy-MM-dd" />
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem> 
                                                                                         <dx:LayoutItem Caption="Porcentaje Compra">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer>
                                                                                                     <dx:ASPxSpinEdit  AllowMouseWheel="false" ID="porcentajeCompra" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"  MaxValue="100" />
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                        <dx:LayoutItem Caption="Monto Impuesto" >
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer>
                                                                                                     <dx:ASPxSpinEdit  AllowMouseWheel="false" ID="montoImpuesto" Enabled="false" BackColor="LightGray" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" DecimalPlaces="2" />
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



                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>


            <dx:ASPxButton runat="server" ID="btnFacturar" Text="Facturar" OnClick="btnFacturar_Click" CausesValidation="true"></dx:ASPxButton>
            

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

