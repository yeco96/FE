<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmEmisorReceptor.aspx.cs" Inherits="Web.Pages.Catalogos.FrmEmisorReceptor" %>

<%@ Register Src="~/UserControls/AddAuditoriaForm.ascx" TagPrefix="user1" TagName="AddAuditoriaForm" %>
<%@ Register Src="~/UserControls/AddUbicacionForm.ascx" TagPrefix="user2" TagName="AddUbicacionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <section class="featured">
        <div class="content-wrapper">
            Mantenimiento Moneda
        </div>
    </section>
    <div class="borde_redondo_tabla">

        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
            Width="100%" EnableTheming="True" KeyFieldName="identificacionTipo;identificacion" Theme="Moderno"
            OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize"
            OnCustomErrorText="ASPxGridView1_CustomErrorText"
            OnRowDeleting="ASPxGridView1_RowDeleting"
            OnRowInserting="ASPxGridView1_RowInserting"
            OnRowUpdating="ASPxGridView1_RowUpdating">

            <Columns>
                <dx:GridViewCommandColumn Width="100px" ButtonType="Image" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                </dx:GridViewCommandColumn>

                <dx:GridViewDataComboBoxColumn Caption="Tipo" FieldName="identificacionTipo" VisibleIndex="2"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Caption="Identificación" FieldName="identificacion" VisibleIndex="3" PropertiesTextEdit-MaxLength="12"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Nombre" FieldName="nombre" VisibleIndex="4" PropertiesTextEdit-MaxLength="30"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Nombre Comercial" FieldName="nombreComercial" VisibleIndex="6" PropertiesTextEdit-MaxLength="80"
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
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
            <SettingsPager PageSize="10" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Items="10, 20, 50, 100" />
            <SettingsEditing Mode="EditFormAndDisplayRow" />
            <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="True" UseFixedTableLayout="True" />
            <SettingsContextMenu EnableColumnMenu="True" Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="True" EnableRowMenu="True" />

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
                                <dx:TabPage Text="Ubicación" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">


                                            <dx:ASPxFormLayout runat="server" ID="layoutAddUbicacionForm">
                                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                                                <Items>
                                                    <dx:LayoutGroup Caption=" " ColCount="3" GroupBoxDecoration="None" UseDefaultPaddings="false">

                                                        <Items>
                                                            <dx:LayoutItem Caption="Provincia">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxGridViewTemplateReplacement ReplacementType="EditFormCellEditor" ColumnID="provincia" runat="server" />
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Canton">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxGridViewTemplateReplacement ReplacementType="EditFormCellEditor" ColumnID="canton" runat="server" />
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Distrito">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxGridViewTemplateReplacement ReplacementType="EditFormCellEditor" ColumnID="distrito" runat="server" />
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Barrio">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxGridViewTemplateReplacement ReplacementType="EditFormCellEditor" ColumnID="barrio" runat="server" />
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Otras Señas">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxGridViewTemplateReplacement ReplacementType="EditFormCellEditor" ColumnID="otraSena" runat="server" />
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
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1" FileName="Catálogo Moneda">
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
