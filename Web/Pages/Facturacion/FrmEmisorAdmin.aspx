<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmEmisorAdmin.aspx.cs" Inherits="Web.Pages.Catalogos.FrmEmisorAdmin" %>

<%@ Register Src="~/UserControls/AddAuditoriaForm.ascx" TagPrefix="user1" TagName="AddAuditoriaForm" %>
<%@ Register Src="~/UserControls/AddUbicacionForm.ascx" TagPrefix="user2" TagName="AddUbicacionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <div class="text-box-title">
        <div class="text-box-heading-title">Mantenimiento Emisor</div>
        <div class="arrow-down-title" style="margin-bottom: 5px;"></div>
    </div>

    <div class="borde_redondo_tabla">

        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always" OnUnload="UpdatePanel_Unload">
            <ContentTemplate>
                <div id="alertMessages" role="alert" runat="server" />
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
                    Width="100%" EnableTheming="True" KeyFieldName="identificacion" Theme="Moderno"
                    OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize"
                    OnRowValidating="ASPxGridView1_RowValidating"
                    OnCancelRowEditing="ASPxGridView1_CancelRowEditing"
                    OnRowDeleting="ASPxGridView1_RowDeleting"
                    OnRowInserting="ASPxGridView1_RowInserting"
                    OnRowUpdating="ASPxGridView1_RowUpdating">
                    <ClientSideEvents EndCallback="function(s, e) {if (s.cpUpdatedMessage) { alert(s.cpUpdatedMessage);  delete s.cpUpdatedMessage;  }}" />
                    <Columns>
                        <dx:GridViewCommandColumn Width="50px" ButtonType="Image" ShowSelectButton="false" ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                        </dx:GridViewCommandColumn>

                        <dx:GridViewDataComboBoxColumn Caption="Tipo" FieldName="identificacionTipo" VisibleIndex="2"
                            PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn Caption="Identificación" FieldName="identificacion" VisibleIndex="3" PropertiesTextEdit-MaxLength="12"
                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Caption="Nombre" FieldName="nombre" VisibleIndex="4" PropertiesTextEdit-MaxLength="80" Width="20%"
                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Caption="Nombre Comercial" FieldName="nombreComercial" VisibleIndex="6" PropertiesTextEdit-MaxLength="80" EditFormSettings-Visible="True" Visible="false"
                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataSpinEditColumn Caption="Teléfono" FieldName="telefono" VisibleIndex="7" PropertiesSpinEdit-MaxLength="20"
                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                        </dx:GridViewDataSpinEditColumn>

                        <dx:GridViewDataSpinEditColumn Caption="Fax" FieldName="fax" VisibleIndex="7" PropertiesSpinEdit-MaxLength="20" Visible="false" EditFormSettings-Visible="true">
                        </dx:GridViewDataSpinEditColumn>

                        <dx:GridViewDataComboBoxColumn Caption="Cod Teléfono" FieldName="telefonoCodigoPais" Visible="false" />
                        <dx:GridViewDataComboBoxColumn Caption="Cod Fax" FieldName="faxCodigoPais" Visible="false" />

                         <dx:GridViewDataTokenBoxColumn Caption="Correo" FieldName="correoElectronicoPrincipal" VisibleIndex="8"  Visible="true" EditFormSettings-Visible="False"
                    PropertiesTokenBox-MaxLength="250"
                    PropertiesTokenBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesTokenBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTokenBoxColumn>

                <dx:GridViewDataTokenBoxColumn Caption="Correo" FieldName="correoElectronico" VisibleIndex="8"     Visible="false" EditFormSettings-Visible="true"
                    PropertiesTokenBox-MaxLength="250"
                    PropertiesTokenBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesTokenBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTokenBoxColumn>

                        <dx:GridViewDataComboBoxColumn Caption="Estado" FieldName="estado" VisibleIndex="9" Visible="false" EditFormSettings-Visible="True"
                            PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Clave Llave Criptográfica" FieldName="claveLlaveCriptografica" VisibleIndex="20" Visible="false" EditFormSettings-Visible="True" PropertiesSpinEdit-MaxLength="4">
                        </dx:GridViewDataSpinEditColumn>

                        <dx:GridViewDataBinaryImageColumn Caption="Llave Criptográfica" FieldName="llaveCriptografica" VisibleIndex="20" Visible="false" EditFormSettings-Visible="True"
                            PropertiesBinaryImage-ValidationSettings-RequiredField-IsRequired="true" PropertiesBinaryImage-ValidationSettings-RequiredField-ErrorText="Requerido">
                            <EditItemTemplate>
                                <dx:ASPxUploadControl ID="fileUpload" OnFileUploadComplete="DocumentsUploadControl_FileUploadComplete" ShowUploadButton="true" runat="server">
                                    <ValidationSettings
                                        AllowedFileExtensions=".p12"
                                        MaxFileSize="512304">
                                    </ValidationSettings>
                                </dx:ASPxUploadControl>
                            </EditItemTemplate>
                        </dx:GridViewDataBinaryImageColumn>

                        <dx:GridViewDataTextColumn Caption="Usuario Comprobantes" FieldName="usernameOAuth2" VisibleIndex="19" Visible="false" EditFormSettings-Visible="True" PropertiesTextEdit-MaxLength="100">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Contraseña Comprobantes" FieldName="passwordOAuth2" VisibleIndex="19" Visible="false" EditFormSettings-Visible="True" PropertiesTextEdit-MaxLength="50">
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Visible="false" Caption="Usuario Creación" FieldName="usuarioCreacion" VisibleIndex="51">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Visible="false" Caption="Fecha Creación" FieldName="fechaCreacion" VisibleIndex="52">
                            <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="" EditFormatString="dd/MM/yyyy hh:mm:ss"></PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Visible="false" Caption="Usuario Modificación" FieldName="usuarioModificacion" VisibleIndex="53">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Visible="false" Caption="Fecha Modificación" FieldName="fechaModificacion" VisibleIndex="54">
                            <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="" EditFormatString="dd/MM/yyyy hh:mm:ss"></PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                    </Columns>

                    <SettingsBehavior ColumnResizeMode="NextColumn" />
                    <Settings ShowFooter="True" ShowFilterBar="Visible" ShowFilterRow="true" />
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" ProcessSelectionChangedOnServer="True" />
                    <SettingsPager PageSize="10" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Items="10, 20, 50, 100" />
                    <SettingsEditing Mode="EditFormAndDisplayRow" />
                    <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="True" UseFixedTableLayout="True" />
                    <SettingsContextMenu EnableColumnMenu="True" Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="True" EnableRowMenu="True" />
                    <SettingsDataSecurity AllowDelete="true" AllowInsert="true" AllowEdit="true" />
                    <SettingsCommandButton>
                        <SelectButton Image-ToolTip="Seleccionar" Image-Url="~/Content/Images/search1.png" />
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
                                <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" Theme="MetropolisBlue">
                                    <TabPages>
                                        <dx:TabPage Text="Datos" Visible="true">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server" />

                                                    <dx:ASPxFormLayout runat="server" ID="formLayoutUbicacion">
                                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Ubicación" ColCount="4" GroupBoxDecoration="HeadingLine" UseDefaultPaddings="false">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Provincia">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxComboBox ID="cmbProvincia" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbProvincia_ValueChanged" AutoPostBack="true"
                                                                                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Cantón">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxComboBox ID="cmbCanton" runat="server" Width="100%" AutoResizeWithContainer="true" OnValueChanged="cmbCanton_ValueChanged" AutoPostBack="true"
                                                                                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Distrito">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxComboBox ID="cmbDistrito" Width="100%" AutoResizeWithContainer="true" runat="server" OnValueChanged="cmbDistrito_ValueChanged" AutoPostBack="true"
                                                                                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Barrio">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxComboBox ID="cmbBarrio" runat="server" Width="100%" AutoResizeWithContainer="true"
                                                                                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Otras Señas" ColSpan="4" Width="100%">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer>
                                                                                <dx:ASPxMemo ID="txtOtraSenas" runat="server" Width="100%" AutoResizeWithContainer="true"
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
                                        <dx:TabPage Text="Auditoría" Visible="true">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <user1:AddAuditoriaForm runat="server" />
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
                        <FooterRow>
                            <asp:ImageButton ID="exportarPDF" runat="server" ImageUrl="~/Content/Images/pdf.png" ToolTip="Exportar a PDF" OnClick="exportarPDF_Click" />
                            <asp:ImageButton ID="exportarXLSX" runat="server" ImageUrl="~/Content/Images/xlsx.png" ToolTip="Exportar a MS-Excel 2007 o superior" OnClick="exportarXLSX_Click" />
                            <asp:ImageButton ID="exportarCSV" runat="server" ImageUrl="~/Content/Images/csv.png" ToolTip="Exportar a MS-Excel delimitado con punto y coma" OnClick="exportarCSV_Click" />
                        </FooterRow>
                    </Templates>
                    <Border BorderWidth="0px" />
                    <BorderBottom BorderWidth="1px" />

                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1" FileName="Catálogo Emisor">
                    <Styles>
                        <Default Font-Names="Arial" Font-Size="Small" />
                    </Styles>
                    <PageHeader Center="Facturación Web - Catálogo Moneda">
                        <Font Bold="True" Names="Arial" Size="Large" />
                    </PageHeader>
                    <PageFooter Left="[Page # of Pages #]" Right="[Date Printed][Time Printed]">
                        <Font Names="Arial" Size="Small" />
                    </PageFooter>
                </dx:ASPxGridViewExporter>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
