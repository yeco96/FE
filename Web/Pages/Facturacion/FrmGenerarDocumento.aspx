﻿<%@ Page Async="true" Title="" Culture="es-CR" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmGenerarDocumento.aspx.cs" Inherits="Web.Pages.Facturacion.FrmGenerarDocumento" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <div class="text-box-title">
        <div class="text-box-heading-title">Documento Electrónico</div>
        <div class="arrow-down-title" style="margin-bottom: 5px;"></div>
    </div>


    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always" OnUnload="UpdatePanel_Unload">
        <ContentTemplate>

            <div id="alertMessages" role="alert" runat="server" />

            <dx:ASPxPageControl ID="documento" Width="100%" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True" Theme="MetropolisBlue" >
                <TabPages>
                  

                      <dx:TabPage Text="Favoritos" > 
                        <ContentCollection>
                            <dx:ContentControl runat="server">


                                <dx:ASPxGridView ID="ASPxGridViewClientes" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
                                     EnableTheming="True" KeyFieldName="identificacion" Theme="Moderno"    EnableCallBacks="false"
                                         Width="100%"   OnSelectionChanged="ASPxGridViewClientes_SelectionChanged"
                                    OnRowDeleting="ASPxGridViewClientes_RowDeleting" > 
                                    <Columns>
                                        <dx:GridViewCommandColumn Width="50px" ButtonType="Image" ShowSelectButton="true" ShowDeleteButton="true" ShowEditButton="false" ShowNewButtonInHeader="false" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                                        </dx:GridViewCommandColumn>

                                        <dx:GridViewDataComboBoxColumn Caption="Tipo" Visible="false" FieldName="identificacionTipo" VisibleIndex="2">
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn Caption="Identificación" FieldName="identificacion" VisibleIndex="3" PropertiesTextEdit-MaxLength="12"  >
                                            
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="Nombre" FieldName="nombre" VisibleIndex="4" PropertiesTextEdit-MaxLength="30" Width="25%">
                                            
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="Nombre Comercial" FieldName="nombreComercial" VisibleIndex="6" PropertiesTextEdit-MaxLength="80" >
                                            <Settings AutoFilterCondition="Contains" /> 
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="Teléfono" FieldName="telefono" VisibleIndex="7" >
                                        </dx:GridViewDataSpinEditColumn>
                 
                                        <dx:GridViewDataTextColumn Caption="Correo" FieldName="correoElectronicoPrincipal" VisibleIndex="8" PropertiesTextEdit-MaxLength="80" Width="100px">
                                        </dx:GridViewDataTextColumn>
                  
                                    </Columns>

                                      <SettingsSearchPanel Visible="true"  />
                                    <SettingsBehavior ColumnResizeMode="NextColumn" />
                                    <Settings  ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="true"  />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="false" ConfirmDelete="True" ProcessSelectionChangedOnServer="True" AllowSelectSingleRowOnly="true" />
                                    <SettingsPager PageSize="10" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Items="10, 20, 50, 100" >
                                   
                                        <PageSizeItemSettings Items="10, 20, 50, 100" Visible="True">
                                        </PageSizeItemSettings>
                                    </SettingsPager>
                                   
                                    <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="false" ShowFilterRow="false" ShowTitlePanel="True" UseFixedTableLayout="True" />
                                    <SettingsContextMenu EnableColumnMenu="True" Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="True" EnableRowMenu="True" />
                                    <SettingsDataSecurity AllowDelete="true" AllowInsert="false" AllowEdit="false" />
                                    <SettingsCommandButton>
                                        <SelectButton Image-ToolTip="Seleccionar" Image-Url="~/Content/Images/search1.png" > 
                                            <Image ToolTip="Seleccionar" Url="~/Content/Images/search1.png">
                                            </Image>
                                        </SelectButton>
                                        <DeleteButton Image-ToolTip="Eliminar" Image-Url="~/Content/Images/delete.png" />
                                        <ClearFilterButton Image-ToolTip="Quitar filtros" Image-Url="~/Content/Images/refresh.png" > 
                                            <Image ToolTip="Quitar filtros" Url="~/Content/Images/refresh.png">
                                            </Image>
                                        </ClearFilterButton>
                                    </SettingsCommandButton>

                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800" AllowOnlyOneAdaptiveDetailExpanded="true" AdaptiveDetailColumnCount="1"></SettingsAdaptivity>
                                    <EditFormLayoutProperties>
                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                    </EditFormLayoutProperties>
                                    <Styles>
                                        <Cell Wrap="True"></Cell>
                                        <AlternatingRow Enabled="true" />
                                    </Styles>
             
                                    <Border BorderWidth="0px" />
                                    <BorderBottom BorderWidth="1px" />

                                </dx:ASPxGridView>

                                  </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>

                      <dx:TabPage Text="Cliente"> 
                        <ContentCollection>
                            <dx:ContentControl runat="server">

                                 
                                     <dx:BootstrapAccordion ID="acordionReceptor"  runat="server" AutoCollapse="true"   >
                                          
                                              <Groups>
                                                <dx:BootstrapAccordionGroup Text="1. Datos Personales" >

                                                     <ContentTemplate> 
                                                         
                                                    <dx:ASPxFormLayout runat="server" AlignItemCaptionsInAllGroups="true" ID="ASPxFormLayout">
                                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Datos Personales" ColCount="2" GroupBoxDecoration="Box" UseDefaultPaddings="false" SettingsItemCaptions-Location="Top">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Tipo">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxComboBox ID="cmbReceptorTipo" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                                                    ValidationSettings-RequiredField-IsRequired="false" ValidationSettings-RequiredField-ErrorText="Requerido">
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
                                                                                            <dx:ASPxSpinEdit ID="txtReceptorIdentificacion" runat="server" Width="100%" AutoResizeWithContainer="true" MaxLength="25"
                                                                                                AllowMouseWheel="false">
                                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                    <RequiredField ErrorText="Requerido" />
                                                                                                </ValidationSettings>
                                                                                            </dx:ASPxSpinEdit>
                                                                                        </td>
                                                                                        <td style="width: 10%;">
                                                                                            <dx:ASPxButton runat="server" ToolTip="Buscar" Image-AlternateText="Buscar" ID="btnBuscarReceptor" CssClass="imagen" CausesValidation="false" OnClick="btnBuscarReceptor_Click" Image-Url="~/Content/Images/loadUser.png" Image-Height="20px" Height="16px" Width="33px">
                                                                                                <Image AlternateText="Buscar" Height="30px" Url="~/Content/Images/search1.png">
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
                                                                                     OnValueChanged="txtReceptorNombre_ValueChanged"  AutoPostBack="true" MaxLength="80">
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                        <RequiredField ErrorText="Requerido" />
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Nombre Comercial">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxTextBox ID="txtReceptorNombreComercial" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" MaxLength="80">
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                </Items>
                                                                <SettingsItemCaptions Location="Top" />
                                                            </dx:LayoutGroup>

                                                        </Items>
                                                    </dx:ASPxFormLayout>

                                                    </ContentTemplate>
                                                </dx:BootstrapAccordionGroup>
                                                <dx:BootstrapAccordionGroup Text="2.Contacto"  >
                                                    <ContentTemplate>
                                                        
                                                    <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout">
                                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Contacto" ColCount="2" GroupBoxDecoration="Box" UseDefaultPaddings="false" SettingsItemCaptions-Location="Top">
                                                                <Items>

                                                                    <dx:LayoutItem Caption="Código Teléfono">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                
                                                                                            <dx:ASPxComboBox ID="cmbReceptorTelefonoCod" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                </ValidationSettings>
                                                                                            </dx:ASPxComboBox>
                                                                               
                                                                                           
                                                                                       
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    <dx:LayoutItem Caption="Número Teléfono">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                
                                                                                            <dx:ASPxSpinEdit AllowMouseWheel="false" ID="txtReceptorTelefono" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" MaxLength="20">
                                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                </ValidationSettings>
                                                                                            </dx:ASPxSpinEdit> 
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Código Fax">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                
                                                                                            <dx:ASPxComboBox ID="cmbReceptorFaxCod" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                </ValidationSettings>
                                                                                            </dx:ASPxComboBox>
                                                                                       
                                                                                       
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    
                                                                    
                                                                     <dx:LayoutItem Caption="Número Fax">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                
                                                                                           <dx:ASPxSpinEdit AllowMouseWheel="false" ID="txtReceptorFax" runat="server" Width="90%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" MaxLength="20">
                                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                </ValidationSettings>
                                                                                            </dx:ASPxSpinEdit>
                                                                                       
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                    
                                                                    <dx:LayoutItem Caption="Correo" ColSpan="2" Width="100%"> 
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxTokenBox ID="txtReceptorCorreo" runat="server" Width="100%" AutoResizeWithContainer="true" AutoPostBack="true"
                                                                                    OnTokensChanged="txtReceptorCorreo_TokensChanged" HelpText="Máximo cinco correos electrónicos" >
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                        <RegularExpression ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*" ErrorText="La dirección no cumple con el formato correo@dominio.com" />
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxTokenBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                </Items>
                                                                <SettingsItemCaptions Location="Top" />
                                                            </dx:LayoutGroup>

                                                        </Items>
                                                    </dx:ASPxFormLayout>

                                                    </ContentTemplate>
                                                </dx:BootstrapAccordionGroup>
                                                   <dx:BootstrapAccordionGroup Text="3. Dirección" >
                                                    <ContentTemplate>
                                                        
                                                    <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout">
                                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Dirección" ColCount="2" GroupBoxDecoration="Box" UseDefaultPaddings="false" SettingsItemCaptions-Location="Top">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Provincia">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxComboBox ID="cmbReceptorProvincia" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbProvincia_ValueChanged" AutoPostBack="true"
                                                                                    ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Cantón">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxComboBox ID="cmbReceptorCanton" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbCanton_ValueChanged" AutoPostBack="true"
                                                                                    ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Distrito">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxComboBox ID="cmbReceptorDistrito" Width="100%" AutoResizeWithContainer="true" runat="server" OnValueChanged="cmbDistrito_ValueChanged" AutoPostBack="true"
                                                                                    ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Barrio">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxComboBox ID="cmbReceptorBarrio" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Otras Señas" ColSpan="2" Width="100%">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxMemo ID="txtReceptorOtraSenas" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxMemo>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>

                                                                </Items>
                                                                <SettingsItemCaptions Location="Top" />
                                                            </dx:LayoutGroup>

                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                    </ContentTemplate>
                                                </dx:BootstrapAccordionGroup>
                                            </Groups>
                                          

                                     </dx:BootstrapAccordion>     
                                <p><strong>Note:</strong> Haga clic en el texto del encabezado vinculado para expandir o colapsar los paneles.</p>
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

                                        <dx:GridViewDataTextColumn Caption="Número" FieldName="numero" VisibleIndex="2" PropertiesTextEdit-MaxLength="50" >
                                            <PropertiesTextEdit MaxLength="50"> 
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="Fecha Emisión" FieldName="fechaEmision" VisibleIndex="3" >
                                            <PropertiesTextEdit>
                                                <MaskSettings Mask="####-##-##" />
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataComboBoxColumn Caption="Código" FieldName="codigo" VisibleIndex="4">
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
                                        <NewButton Image-ToolTip="Nuevo" Image-Url="~/Content/Images/add.png">
                                            <Image ToolTip="Nuevo" Url="~/Content/Images/add.png">
                                            </Image>
                                        </NewButton>
                                        <EditButton Image-ToolTip="Modificar" Image-Url="~/Content/Images/edit.png">
                                            <Image ToolTip="Modificar" Url="~/Content/Images/edit.png">
                                            </Image>
                                        </EditButton>
                                        <DeleteButton Image-ToolTip="Eliminar" Image-Url="~/Content/Images/delete.png">
                                            <Image ToolTip="Eliminar" Url="~/Content/Images/delete.png">
                                            </Image>
                                        </DeleteButton>
                                        <ClearFilterButton Image-ToolTip="Quitar filtros" Image-Url="~/Content/Images/refresh.png">
                                            <Image ToolTip="Quitar filtros" Url="~/Content/Images/refresh.png">
                                            </Image>
                                        </ClearFilterButton>
                                        <UpdateButton ButtonType="Link" Image-ToolTip="Guardar cambios y cerrar formulario de edición" Image-Url="~/Content/Images/acept.png">
                                            <Image ToolTip="Guardar cambios y cerrar formulario de edición" Url="~/Content/Images/acept.png">
                                            </Image>
                                        </UpdateButton>
                                        <CancelButton ButtonType="Link" Image-ToolTip="Cerrar el formulario de edición sin guardar los cambios" Image-Url="~/Content/Images/cancel.png">
                                            <Image ToolTip="Cerrar el formulario de edición sin guardar los cambios" Url="~/Content/Images/cancel.png">
                                            </Image>
                                        </CancelButton>
                                    </SettingsCommandButton>

                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800" AllowOnlyOneAdaptiveDetailExpanded="true" AdaptiveDetailColumnCount="1"></SettingsAdaptivity>
                                    <EditFormLayoutProperties>
                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                    </EditFormLayoutProperties>
                                    <Styles>
                                        <Cell Wrap="True"></Cell>
                                        <AlternatingRow Enabled="true" />
                                    </Styles>
                                    <Templates>
                                        <EditForm>
                                            <div style="padding: 4px 4px 3px 4px">
                                                <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" Theme="MetropolisBlue">
                                                    <TabPages>
                                                        <dx:TabPage Text="Documento" Visible="true">
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


                                <dx:ASPxFormLayout runat="server" AlignItemCaptionsInAllGroups="true">
                                    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                    <Items>
                                        <dx:LayoutGroup Caption="Encabezado" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                                            <Items>

                                                <dx:LayoutItem Caption="Tipo Documento">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxComboBox ID="cmbTipoDocumento" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                                ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido"
                                                                OnValueChanged="cmbTipoDocumento_ValueChanged" AutoPostBack="true" >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Sucursal y Caja">
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

                                                 <dx:LayoutItem Caption="Cliente">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxTextBox  runat="server" ID="txtReceptor" ReadOnly="true" BackColor="#d9edf7" Width="100%"></dx:ASPxTextBox>
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
                                                            <dx:ASPxSpinEdit AllowMouseWheel="false" ID="txtPlazoCredito" runat="server" Width="100%" AutoResizeWithContainer="true" MaxLength="5" MinValue="0" Enabled="false"
                                                                ValidationSettings-ErrorDisplayMode="ImageWithTooltip" MaxValue="99999">
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
                                                                ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ReadOnly="true"  ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
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
                                                            <dx:ASPxTextBox ID="txtTipoCambio" runat="server" Width="100%" AutoResizeWithContainer="true" 
                                                                  Enabled="false"  >
                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                                    <RegularExpression ValidationExpression="^(\d{1}\.)?(\d+\.?)+(\d{2})?$" />
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                            
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>

                                                <dx:LayoutItem Caption="Detalle" ColSpan="3" Width="100%" >
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <dx:ASPxMemo ID="txtOtros" Rows="4" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" MaxLength="2000">
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
                                            PropertiesSpinEdit-AllowMouseWheel="false">
                                            <PropertiesSpinEdit DisplayFormatString="g" DecimalPlaces="2" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Producto" FieldName="producto" VisibleIndex="3" PropertiesComboBox-ClientSideEvents-ValueChanged="function(s,e){cambioPrecio(s,e);}">
                                            <PropertiesComboBox>
                                                <ClientSideEvents ValueChanged="function(s,e){cambioPrecio(s,e);}" />
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="Precio" FieldName="precioUnitario" VisibleIndex="4" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                                            PropertiesSpinEdit-AllowMouseWheel="false">
                                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="SubTotal" FieldName="montoTotal" VisibleIndex="5" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            EditFormSettings-Visible="False"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2">
                                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="Descuento" FieldName="montoDescuento" VisibleIndex="6" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                                            PropertiesSpinEdit-AllowMouseWheel="false">
                                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>

                                        <dx:GridViewDataTextColumn Caption="Naturaleza Descuento" FieldName="naturalezaDescuento" VisibleIndex="7" PropertiesTextEdit-MaxLength="80"
                                            Visible="false" EditFormSettings-Visible="True" >
                                            <PropertiesTextEdit MaxLength="80">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>


                                        <dx:GridViewDataSpinEditColumn Caption="Total" FieldName="subTotal" VisibleIndex="8" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2"
                                            EditFormSettings-Visible="False">
                                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataSpinEditColumn>


                                        <dx:GridViewDataSpinEditColumn Caption="Impuestos" FieldName="montoImpuesto" VisibleIndex="8" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            EditFormSettings-Visible="False"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2">

                                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                             

                                        </dx:GridViewDataSpinEditColumn>

                                          <dx:GridViewDataSpinEditColumn Caption="Total Linea" FieldName="montoTotalLinea" VisibleIndex="8" PropertiesSpinEdit-MaxLength="10" PropertiesSpinEdit-DecimalPlaces="2"
                                            EditFormSettings-Visible="False"
                                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="n2">

                                            <PropertiesSpinEdit DisplayFormatString="n2" NumberFormat="Custom" DecimalPlaces="2" MaxValue="999999999999" MaxLength="10">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="Requerido" IsRequired="True" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit> 
                                        </dx:GridViewDataSpinEditColumn>

                                    </Columns>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="subTotal" SummaryType="Sum" DisplayFormat="{0:n2}" />
                                        <dx:ASPxSummaryItem FieldName="montoDescuento" SummaryType="Sum" DisplayFormat="{0:n2}" />
                                        <dx:ASPxSummaryItem FieldName="montoTotal" SummaryType="Sum" DisplayFormat="{0:n2}" />
                                        <dx:ASPxSummaryItem FieldName="montoImpuesto" SummaryType="Sum" DisplayFormat="{0:n2}" /> 
                                        <dx:ASPxSummaryItem FieldName="montoTotalLinea" SummaryType="Sum" DisplayFormat="{0:n2}" />
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
                                        <NewButton Image-ToolTip="Nuevo" Image-Url="~/Content/Images/add.png">
                                            <Image ToolTip="Nuevo" Url="~/Content/Images/add.png">
                                            </Image>
                                        </NewButton>
                                        <EditButton Image-ToolTip="Modificar" Image-Url="~/Content/Images/edit.png">
                                            <Image ToolTip="Modificar" Url="~/Content/Images/edit.png">
                                            </Image>
                                        </EditButton>
                                        <DeleteButton Image-ToolTip="Eliminar" Image-Url="~/Content/Images/delete.png">
                                            <Image ToolTip="Eliminar" Url="~/Content/Images/delete.png">
                                            </Image>
                                        </DeleteButton>
                                        <ClearFilterButton Image-ToolTip="Quitar filtros" Image-Url="~/Content/Images/refresh.png">
                                            <Image ToolTip="Quitar filtros" Url="~/Content/Images/refresh.png">
                                            </Image>
                                        </ClearFilterButton>
                                        <UpdateButton ButtonType="Link" Image-ToolTip="Guardar cambios y cerrar formulario de edición" Image-Url="~/Content/Images/acept.png">
                                            <Image ToolTip="Guardar cambios y cerrar formulario de edición" Url="~/Content/Images/acept.png">
                                            </Image>
                                        </UpdateButton>
                                        <CancelButton ButtonType="Link" Image-ToolTip="Cerrar el formulario de edición sin guardar los cambios" Image-Url="~/Content/Images/cancel.png">
                                            <Image ToolTip="Cerrar el formulario de edición sin guardar los cambios" Url="~/Content/Images/cancel.png">
                                            </Image>
                                        </CancelButton>
                                    </SettingsCommandButton>

                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800" AllowOnlyOneAdaptiveDetailExpanded="true" AdaptiveDetailColumnCount="1"></SettingsAdaptivity>
                                    <EditFormLayoutProperties>
                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                    </EditFormLayoutProperties>
                                    <Styles>
                                        <Cell Wrap="True"></Cell>
                                        <AlternatingRow Enabled="true" />
                                    </Styles>
                                    <Templates>
                                        <EditForm>
                                            <div style="padding: 4px 4px 3px 4px">
                                                <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" Theme="MetropolisBlue">
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
                                                                                                <dx:ASPxSpinEdit AllowMouseWheel="false" ID="porcentajeCompra" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" MaxValue="100" />
                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                        </LayoutItemNestedControlCollection>
                                                                                    </dx:LayoutItem>
                                                                                    <dx:LayoutItem Caption="Monto Impuesto">
                                                                                        <LayoutItemNestedControlCollection>
                                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                                <dx:ASPxSpinEdit AllowMouseWheel="false" ID="montoImpuesto" Enabled="false" BackColor="LightGray" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" DecimalPlaces="2" />
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

               <div class="text-center">
            <dx:ASPxButton runat="server" ID="btnFacturar" Text="Facturar" OnClick="btnFacturar_Click" CausesValidation="true" Image-Url="~/Content/Images/check.png"></dx:ASPxButton>
                   </div>
            <div id="alertMessages1" role="alert" runat="server" />
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

