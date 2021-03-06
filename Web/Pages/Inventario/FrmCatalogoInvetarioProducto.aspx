﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmCatalogoInvetarioProducto.aspx.cs" Inherits="Web.Pages.Inventario.FrmCatalogoInvetarioProducto" %>

<%@ Register Src="~/UserControls/AddAuditoriaForm.ascx" TagPrefix="user" TagName="AddAuditoriaForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

     <div class="text-box-title">
        <div class="text-box-heading-title">Mantenimiento Producto</div>
        <div class="arrow-down-title" style="margin-bottom: 5px;"></div>                        
     </div>  
    <div class="borde_redondo_tabla">

         <script type="text/javascript">
            function calculaPrecio(s, e) {
                //var porcentaje = s.GetText(); 
                
                var porcentaje = ASPxGridView1.GetEditValue("porcentajeGanancia"); 
                var aplicaIV = ASPxGridView1.GetEditValue("aplicaIV");
                var aplicaIS = ASPxGridView1.GetEditValue("aplicaIS");
                var precioCompra = ASPxGridView1.GetEditValue("precioCompra"); 
                //alert(precio);
                if (isNaN(porcentaje)) {
                    ASPxGridView1.SetEditValue("precioVenta1", 0);
                    ASPxGridView1.SetEditValue("precioVenta2", 0);
                    ASPxGridView1.SetEditValue("precioVenta3", 0);
                } else {
                    ASPxGridView1.SetEditValue("precioVenta1", ((porcentaje / 100) + 1) * precioCompra);
                    ASPxGridView1.SetEditValue("precioVenta2", ((porcentaje / 100) + 1) * precioCompra);
                    ASPxGridView1.SetEditValue("precioVenta3", ((porcentaje / 100) + 1) * precioCompra);
                }
            } 

        </script>

        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
            Width="100%" EnableTheming="True" KeyFieldName="id" Theme="MetropolisBlue" 
            OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize" OnDetailRowExpandedChanged="ASPxGridView2_DetailRowExpandedChanged"
            OnRowDeleting="ASPxGridView1_RowDeleting" 
            OnRowInserting="ASPxGridView1_RowInserting" 
            OnRowUpdating="ASPxGridView1_RowUpdating">
            <ClientSideEvents EndCallback="function(s, e) {if (s.cpUpdatedMessage) { alert(s.cpUpdatedMessage);  delete s.cpUpdatedMessage;  }}" />
            <Columns>

                <dx:GridViewCommandColumn Width="100px" ButtonType="Image" ShowDeleteButton="True" ShowEditButton="True"
                     ShowNewButtonInHeader="True" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                </dx:GridViewCommandColumn>

                <dx:GridViewDataTextColumn Caption="Id" FieldName="id" VisibleIndex="2" Visible="false" EditFormSettings-Visible="True"  EditFormSettings-VisibleIndex="1"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Tipo Código" FieldName="tipo" VisibleIndex="2"  Visible="false" EditFormSettings-Visible="True"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>
                  <dx:GridViewDataComboBoxColumn Caption="Tipo" FieldName="tipoServMerc" VisibleIndex="3" 
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn Caption="Unidad Medida" FieldName="unidadMedida" VisibleIndex="4"   Visible="false" EditFormSettings-Visible="True"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Caption="Código" FieldName="codigo" VisibleIndex="5"  PropertiesTextEdit-MaxLength="20" EditFormSettings-VisibleIndex="2"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Descripción" FieldName="descripcion" VisibleIndex="6" EditFormSettings-VisibleIndex="5" PropertiesTextEdit-MaxLength="50" Width="20%"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataSpinEditColumn Caption="P.Compra" FieldName="precioCompra" VisibleIndex="7" PropertiesSpinEdit-DecimalPlaces="2"
                     PropertiesSpinEdit-AllowMouseWheel="false"
                    PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="{0:n2}"
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                    <PropertiesSpinEdit>
                          <ClientSideEvents ValueChanged="function(s,e){calculaPrecio(s,e);}" />
                      </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
                 
                  <dx:GridViewDataSpinEditColumn Caption="% Ganancia" FieldName="porcentajeGanancia" VisibleIndex="8" PropertiesSpinEdit-DecimalPlaces="2"
                     PropertiesSpinEdit-AllowMouseWheel="false"
                    PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="{0:n2}"
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                      <PropertiesSpinEdit>
                          <ClientSideEvents ValueChanged="function(s,e){calculaPrecio(s,e);}" />
                      </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>


                  <dx:GridViewDataSpinEditColumn Caption="P.Venta 1" FieldName="precioVenta1" VisibleIndex="9" PropertiesSpinEdit-DecimalPlaces="2"
                     PropertiesSpinEdit-AllowMouseWheel="false"
                    PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="{0:n2}"
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataSpinEditColumn>

                  <dx:GridViewDataSpinEditColumn Caption="P.Venta 2" FieldName="precioVenta2" VisibleIndex="9" PropertiesSpinEdit-DecimalPlaces="2"
                     PropertiesSpinEdit-AllowMouseWheel="false"
                    PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="{0:n2}"
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataSpinEditColumn>

                  <dx:GridViewDataSpinEditColumn Caption="P.Venta 3" FieldName="precioVenta3" VisibleIndex="9" PropertiesSpinEdit-DecimalPlaces="2"
                     PropertiesSpinEdit-AllowMouseWheel="false"
                    PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="999999999999" PropertiesSpinEdit-DisplayFormatString="{0:n2}"
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataSpinEditColumn>

                 
                 <dx:GridViewDataComboBoxColumn Caption="Imp. Venta" FieldName="aplicaIV" VisibleIndex="100" 
                     Visible="false" EditFormSettings-Visible="True"  CellStyle-HorizontalAlign="Center"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>
                 <dx:GridViewDataComboBoxColumn Caption="Imp. Servicio" FieldName="aplicaIS" VisibleIndex="101"
                     Visible="false" EditFormSettings-Visible="True"  CellStyle-HorizontalAlign="Center"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>

                  <dx:GridViewDataSpinEditColumn  Caption="Cant. Mínima" FieldName="cantidadMinima" VisibleIndex="12"   PropertiesSpinEdit-DisplayFormatString="{0:n2}"
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataSpinEditColumn>
                 <dx:GridViewDataSpinEditColumn Caption="Cant. Máxima" FieldName="cantidadMaxima" VisibleIndex="13"   PropertiesSpinEdit-DisplayFormatString="{0:n2}"
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataSpinEditColumn>
                 <dx:GridViewDataSpinEditColumn Caption="Cant. Disponible" FieldName="cantidadDisponible" VisibleIndex="13"   PropertiesSpinEdit-DisplayFormatString="{0:n2}"
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataSpinEditColumn>

                <dx:GridViewDataComboBoxColumn Caption="Estado" FieldName="estado" VisibleIndex="14" EditFormSettings-VisibleIndex="7"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Visible="false" Caption="Usuario Creación" FieldName="usuarioCreacion" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Visible="false" Caption="Fecha Creación" FieldName="fechaCreacion" VisibleIndex="6">
                    <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="" EditFormatString="dd/MM/yyyy hh:mm:ss"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Visible="false" Caption="Usuario Modificación" FieldName="usuarioModificacion" VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Visible="false" Caption="Fecha Modificación" FieldName="fechaModificacion" VisibleIndex="8">
                    <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="" EditFormatString="dd/MM/yyyy hh:mm:ss"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
            </Columns>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="cantidadDisponible" SummaryType="Sum" DisplayFormat="{0:n0}" />
                <dx:ASPxSummaryItem FieldName="precioCompra" SummaryType="Sum" DisplayFormat="{0:n2}"/>
                <dx:ASPxSummaryItem FieldName="subTotal1" ShowInColumn="precioVenta1" SummaryType="Sum" DisplayFormat="{0:n2}"/>
                <dx:ASPxSummaryItem FieldName="subTotal2" ShowInColumn="precioVenta2" SummaryType="Sum" DisplayFormat="{0:n2}"/>
                <dx:ASPxSummaryItem FieldName="subTotal3" ShowInColumn="precioVenta3" SummaryType="Sum" DisplayFormat="{0:n2}"/>
            </TotalSummary>
            <SettingsBehavior ColumnResizeMode="NextColumn" />
            <Settings ShowFooter="True" ShowFilterBar="Visible" ShowFilterRow="true"   />
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
            <SettingsPager PageSize="10" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Items="10, 20, 50, 100" />
            <SettingsEditing Mode="EditFormAndDisplayRow"  EditFormColumnCount="3"  />
            <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="True" UseFixedTableLayout="True" />
            <SettingsContextMenu EnableColumnMenu="True" Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="True" EnableRowMenu="True" />
            <SettingsDetail ShowDetailRow="true" />
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
                <TitlePanel>
                    <div style="text-align: right;" >
                        <asp:ImageButton ID="exportarPDF" runat="server" ImageUrl="~/Content/Images/pdf.png" ToolTip="Exportar a PDF" OnClick="exportarPDF_Click" />
                        <asp:ImageButton ID="exportarXLSX" runat="server" ImageUrl="~/Content/Images/xlsx.png" ToolTip="Exportar a MS-Excel 2007 o superior" OnClick="exportarXLSX_Click" />
                        <asp:ImageButton ID="exportarCSV" runat="server" ImageUrl="~/Content/Images/csv.png" ToolTip="Exportar a MS-Excel delimitado con punto y coma" OnClick="exportarCSV_Click" />
                    </div>
                </TitlePanel>
                <DetailRow>

                    <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView2" KeyboardSupport="True"
                       EnableTheming="True" KeyFieldName="idProducto" Theme="Moderno" 
                         >
                        <Columns>
                            <dx:GridViewDataComboBoxColumn Caption="Tipo Impuesto" FieldName="TipoImpuesto.descripcion" VisibleIndex="2"
                                PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataSpinEditColumn Caption="Porcentaje" FieldName="porcentaje" VisibleIndex="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-MaxValue="9999" PropertiesSpinEdit-DecimalPlaces="2"
                                PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                            </dx:GridViewDataSpinEditColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Estado" FieldName="estado" VisibleIndex="4"
                                PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                            </dx:GridViewDataComboBoxColumn>

                        </Columns>
                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800" AllowOnlyOneAdaptiveDetailExpanded="true" AdaptiveDetailColumnCount="1"></SettingsAdaptivity>
                        <Styles>
                            <Cell Wrap="True"></Cell>
                            <AlternatingRow Enabled="true" />
                        </Styles>
                        <Border BorderWidth="0px" />
                        <BorderBottom BorderWidth="1px" />

                    </dx:ASPxGridView>

                </DetailRow>
            </Templates>
            <Border BorderWidth="0px" />
            <BorderBottom BorderWidth="1px" />

        </dx:ASPxGridView>
       
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1" FileName="Catálogo Producto">
            <Styles>
                <Default Font-Names="Arial" Font-Size="Small" />
            </Styles>
            <PageHeader Center="Facturación Web - Catálogo Producto">
                <Font Bold="True" Names="Arial" Size="Large" />
            </PageHeader>
            <PageFooter Left="[Page # of Pages #]" Right="[Date Printed][Time Printed]">
                <Font Names="Arial" Size="Small" />
            </PageFooter>
        </dx:ASPxGridViewExporter>
    </div>

</asp:Content>
