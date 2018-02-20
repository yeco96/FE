<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmEmisor.aspx.cs" Inherits="Web.Pages.Catalogos.FrmEmisor" %>

<%@ Register Src="~/UserControls/AddAuditoriaForm.ascx" TagPrefix="user1" TagName="AddAuditoriaForm" %>
<%@ Register Src="~/UserControls/AddUbicacionForm.ascx" TagPrefix="user2" TagName="AddUbicacionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <section class="featured">
        <div class="content-wrapper">
            Mantenimiento Emisor
        </div>
    </section>
    <div class="borde_redondo_tabla">



        <dx:ASPxPageControl ID="documento" Width="100%" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"  EnableTabScrolling="true" TabAlign="Justify" >
            <TabPages>
                <dx:TabPage Text="Datos Personales">
                    <ContentCollection>
                        <dx:ContentControl runat="server">

                            <dx:ASPxFormLayout runat="server">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                <Items>
                                    <dx:LayoutGroup Caption="Datos Personales" ColCount="3" GroupBoxDecoration="HeadingLine" UseDefaultPaddings="false">
                                        <Items>
                                            <dx:LayoutItem Caption="Tipo">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cmbEmisorTipo" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" />
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Identificación">
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
                                                        <dx:ASPxTextBox ID="txtEmisorNombreComercial" runat="server" Width="100%" AutoResizeWithContainer="true" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" MaxLength="80" />
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

                <dx:TabPage Text="Contácto">
                    <ContentCollection>
                        <dx:ContentControl runat="server">

                            <dx:ASPxFormLayout runat="server">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                <Items>
                                    <dx:LayoutGroup Caption="Contácto" ColCount="3" GroupBoxDecoration="HeadingLine" UseDefaultPaddings="false">
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
                                                            ValidationSettings-RegularExpression-ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*">
                                                        </dx:ASPxTextBox>
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

                <dx:TabPage Text="Ubicación">
                    <ContentCollection>
                        <dx:ContentControl runat="server">

                            <dx:ASPxFormLayout runat="server">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                <Items>
                                    <dx:LayoutGroup Caption="Ubicación" ColCount="4" GroupBoxDecoration="HeadingLine" UseDefaultPaddings="false">
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
                                            <dx:LayoutItem Caption="Otras Señas" ColSpan="4" Width="100%">
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

                <dx:TabPage Text="Hacienda">
                    <ContentCollection>
                        <dx:ContentControl runat="server">

                            <dx:ASPxFormLayout runat="server">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                <Items>
                                    <dx:LayoutGroup Caption="Hacienda" ColCount="2" GroupBoxDecoration="HeadingLine" UseDefaultPaddings="false">
                                        <Items>
                                            <dx:LayoutItem Caption="Llave Criptográfica">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxUploadControl runat="server" ID="p12UploadControl" ClientInstanceName="DocumentsUploadControl" Width="100%"
                                                            AutoStartUpload="true" ShowProgressPanel="True" ShowTextBox="false" BrowseButton-Text="Cargar" FileUploadMode="OnPageLoad"
                                                            OnFileUploadComplete="DocumentsUploadControl_FileUploadComplete">
                                                            <AdvancedModeSettings EnableMultiSelect="false" EnableDragAndDrop="true" ExternalDropZoneID="dropZone" />
                                                            <ValidationSettings
                                                                AllowedFileExtensions=".p12"
                                                                MaxFileSize="4194304">
                                                            </ValidationSettings>
                                                        </dx:ASPxUploadControl>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Clave">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxSpinEdit ID="txtClaveLlaveCriptografica" runat="server" Width="100%" AutoResizeWithContainer="true" MaxLength="4" MinValue="0" MaxValue="9999"
                                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Usuario Comprobantes Electrónicos">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="txtUsernameOAuth2" runat="server" Width="100%" AutoResizeWithContainer="true" MaxLength="100"
                                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="Requerido" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" />
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Contraseña Comprobantes Electrónicos">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTextBox ID="txtPasswordOAuth2" runat="server" Width="100%" AutoResizeWithContainer="true" MaxLength="50"
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

            </TabPages>
        </dx:ASPxPageControl>

         
        <dx:ASPxButton runat="server" ID="btnActualizar" Enabled="false" Text="Actualizar" OnClick="btnActualizar_Click" CausesValidation="true" />

         <div id="alertMessages" role="alert"  runat="server" /> 
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
            Width="100%" EnableTheming="True" KeyFieldName="identificacionTipo;identificacion" Theme="Moderno" EnableCallBacks="false"
            OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize"
            
            OnSelectionChanged="ASPxGridView1_SelectionChanged"
            OnRowDeleting="ASPxGridView1_RowDeleting"
            OnRowInserting="ASPxGridView1_RowInserting"
            OnRowUpdating="ASPxGridView1_RowUpdating">

            <Columns>
                <dx:GridViewCommandColumn Width="50px" ButtonType="Image" ShowSelectButton="true" ShowDeleteButton="false" ShowEditButton="false" ShowNewButtonInHeader="false" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                </dx:GridViewCommandColumn>

                <dx:GridViewDataComboBoxColumn Caption="Tipo" FieldName="identificacionTipo" VisibleIndex="2"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Caption="Identificación" FieldName="identificacion" VisibleIndex="3" PropertiesTextEdit-MaxLength="12"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Nombre" FieldName="nombre" VisibleIndex="4" PropertiesTextEdit-MaxLength="30" Width="20%"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Nombre Comercial" FieldName="nombreComercial" VisibleIndex="6" PropertiesTextEdit-MaxLength="80"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Teléfono" FieldName="telefono" VisibleIndex="7" PropertiesTextEdit-MaxLength="80"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Correo" FieldName="correoElectronico" VisibleIndex="8" PropertiesTextEdit-MaxLength="80" Width="20%"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>


                <dx:GridViewDataComboBoxColumn FieldName="provincia" Visible="false" />
                <dx:GridViewDataComboBoxColumn FieldName="canton" Visible="false" />
                <dx:GridViewDataComboBoxColumn FieldName="distrito" Visible="false" />
                <dx:GridViewDataComboBoxColumn FieldName="barrio" Visible="false" />
                <dx:GridViewDataMemoColumn FieldName="otraSena" Visible="false" />

                <dx:GridViewDataComboBoxColumn Caption="Estado" FieldName="estado" VisibleIndex="50"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>
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

    </div>

</asp:Content>
