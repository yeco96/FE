<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmCatalogoEmpresa.aspx.cs" Inherits="Web.Pages.Catalogos.FrmCatalogoEmpresa" %>

<%@ Register Src="~/UserControls/AddAuditoriaForm.ascx" TagPrefix="user" TagName="AddAuditoriaForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

     <div class="text-box-title">
        <div class="text-box-heading-title"> Mantenimiento Empresa</div>
        <div class="arrow-down-title" style="margin-bottom: 5px;"></div>                        
     </div>  
     
    <div class="borde_redondo_tabla">

        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
            Width="100%" EnableTheming="True" KeyFieldName="codigo" Theme="Moderno"
            OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize"
             
            OnRowDeleting="ASPxGridView1_RowDeleting"
            OnRowInserting="ASPxGridView1_RowInserting"
            OnRowUpdating="ASPxGridView1_RowUpdating">
            <Columns>
                <dx:GridViewCommandColumn Width="100px" ButtonType="Image" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="Código" FieldName="codigo" VisibleIndex="2" PropertiesTextEdit-MaxLength="10"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Nombre" FieldName="descripcion" VisibleIndex="3" PropertiesTextEdit-MaxLength="80" 
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataBinaryImageColumn Caption="Logo" FieldName="logo" VisibleIndex="10"  
                    PropertiesBinaryImage-ValidationSettings-RequiredField-IsRequired="true" PropertiesBinaryImage-ValidationSettings-RequiredField-ErrorText="Requerido">
                    <EditItemTemplate>
                        <dx:ASPxBinaryImage ID="imgPreview" runat="server" Value='<%# Bind("logo") %>' Width="100px" Height="100px">
                        </dx:ASPxBinaryImage>
                        <dx:ASPxUploadControl ID="fileUpload" OnFileUploadComplete="fileUpload_FileUploadComplete" ShowUploadButton="true" runat="server">
                              <ValidationSettings
                                    AllowedFileExtensions=".jpg,.jpeg,.png"
                                    MaxFileSize="512304">
                                </ValidationSettings>
                        </dx:ASPxUploadControl>
                    </EditItemTemplate>
                </dx:GridViewDataBinaryImageColumn>

                
                  <dx:GridViewDataComboBoxColumn Caption="Idioma" FieldName="idioma" VisibleIndex="4"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataComboBoxColumn Caption="Tipo Impresión" FieldName="tipoImpresion" VisibleIndex="5"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>
                 
                  <dx:GridViewDataMemoColumn Caption="Leyenda" FieldName="leyenda" VisibleIndex="4" Visible="false" EditFormSettings-Visible="True"  
                    PropertiesMemoEdit-ValidationSettings-RequiredField-IsRequired="false" PropertiesMemoEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataMemoColumn>



                  <dx:GridViewDataComboBoxColumn Caption="Medio Pago" FieldName="medioPago" VisibleIndex="6" EditFormSettings-Visible="True"  Visible="false"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>

                  <dx:GridViewDataComboBoxColumn Caption="Condición Venta" FieldName="condicionVenta" VisibleIndex="6"  EditFormSettings-Visible="True"  Visible="false"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn> 

                  <dx:GridViewDataSpinEditColumn Caption="Plazo Crédito" FieldName="plazoCredito" VisibleIndex="6"  EditFormSettings-Visible="True"  Visible="false"
                      PropertiesSpinEdit-AllowMouseWheel="false"
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataSpinEditColumn> 

                <dx:GridViewDataComboBoxColumn Caption="Tipo Moneda" FieldName="moneda" VisibleIndex="6"  EditFormSettings-Visible="True"  Visible="false"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>

                
                <dx:GridViewDataComboBoxColumn Caption="Estado" FieldName="estado" VisibleIndex="6"  EditFormSettings-Visible="True"  Visible="false"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataTextColumn Visible="false" Caption="Usuario Creación" FieldName="usuarioCreacion" VisibleIndex="17">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Visible="false" Caption="Fecha Creación" FieldName="fechaCreacion" VisibleIndex="18">
                    <PropertiesDateEdit EditFormat="DateTime" DisplayFormatstring="" EditFormatstring="dd/MM/yyyy hh:mm:ss"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Visible="false" Caption="Usuario Modificación" FieldName="usuarioModificacion" VisibleIndex="19">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Visible="false" Caption="Fecha Modificación" FieldName="fechaModificacion" VisibleIndex="20">
                    <PropertiesDateEdit EditFormat="DateTime" DisplayFormatstring="" EditFormatstring="dd/MM/yyyy hh:mm:ss"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
            </Columns>
            
             <SettingsBehavior ColumnResizeMode="NextColumn" />
            <Settings ShowFooter="True" ShowFilterBar="Visible" ShowFilterRow="true" />
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
            <SettingsPager PageSize="10" PageSizeItemSettings-Visible="true"  PageSizeItemSettings-Items="10, 20, 50, 100" />
            <SettingsEditing Mode="EditFormAndDisplayRow" />
            <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="True" UseFixedTableLayout="True" />
            <SettingsContextMenu EnableColumnMenu="True" Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="True" EnableRowMenu="True" />
            
            <SettingsCommandButton>
                <NewButton Image-ToolTip="Nuevo" Image-Url="~/Content/Images/add.png"/> 
                <EditButton  Image-ToolTip="Modificar" Image-Url="~/Content/Images/edit.png" /> 
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
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Auditoría" Visible="true">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <user:AddAuditoriaForm runat="server" />
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
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1" FileName="Catálogo Empresa">
            <Styles>
                <Default Font-Names="Arial" Font-Size="Small" />
            </Styles>
            <PageHeader Center="Facturación Web - Catálogo Empresa">
                <Font Bold="True" Names="Arial" Size="Large" />
            </PageHeader>
            <PageFooter Left="[Page # of Pages #]" Right="[Date Printed][Time Printed]">
                <Font Names="Arial" Size="Small" />
            </PageFooter>
        </dx:ASPxGridViewExporter> 
    </div>

</asp:Content>
