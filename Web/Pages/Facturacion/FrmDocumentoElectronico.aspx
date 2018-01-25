<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmDocumentoElectronico.aspx.cs" Inherits="Web.Pages.Facturacion.FrmDocumentoElectronico" %>

<%@ Register Src="~/UserControls/AddAuditoriaForm.ascx" TagPrefix="user" TagName="AddAuditoriaForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <section class="featured">
        <div class="content-wrapper">
            Documentos Electrónicos
        </div>
    </section>
    <div class="borde_redondo_tabla">

        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
            Width="100%" EnableTheming="True" KeyFieldName="clave" Theme="Moderno"
            OnDetailRowExpandedChanged="ASPxGridView1_DetailRowExpandedChanged"
            OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize"
            OnCustomErrorText="ASPxGridView1_CustomErrorText">
            <Columns>

                <dx:GridViewDataTextColumn Caption="Clave" FieldName="clave" VisibleIndex="2" Visible="false">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Código" FieldName="factura" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Emisor" FieldName="emisorIdentificacion" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Receptor" FieldName="receptorIdentificacion" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Mensaje" FieldName="mensaje" VisibleIndex="5" Visible="false">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Monto Impuesto" FieldName="montoTotalImpuesto" VisibleIndex="5" PropertiesTextEdit-DisplayFormatString="c2">
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn Caption="Monto Factura" FieldName="montoTotalFactura" VisibleIndex="5" PropertiesTextEdit-DisplayFormatString="c2">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataComboBoxColumn Caption="Estado" FieldName="indEstado" VisibleIndex="6">
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

            <SettingsBehavior ColumnResizeMode="NextColumn" />
            <Settings ShowFooter="True" ShowFilterBar="Visible" ShowFilterRow="true" />
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
            <SettingsPager PageSize="10" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Items="10, 20, 50, 100" />
            <SettingsEditing Mode="EditFormAndDisplayRow" />
            <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="True" UseFixedTableLayout="True" />
            <SettingsContextMenu EnableColumnMenu="True" Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="True" EnableRowMenu="True" />
            <SettingsDetail ShowDetailRow="True" />

            <SettingsCommandButton>
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
                <DetailRow>
                     
                    <dx:ASPxFormLayout runat="server" ID="layoutAddAuditoriaForm">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
                        <Items>
                            <dx:LayoutGroup Caption=" " ColCount="3" GroupBoxDecoration="None" UseDefaultPaddings="false">

                                <Items>
                                    <dx:LayoutItem Caption="Mensaje" ColSpan="3" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxMemo runat="server" Value='<%# Eval("mensaje") %>' AutoResizeWithContainer="true" Width="100%" BackColor="LightGray" ReadOnly="true" />

                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnNotaCredito" runat="server" Text="Crear Nota Crédito" OnClick="btnActualizar_Click"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnNotaDebito" runat="server" Text="Crear Nota Débito" OnClick="btnActualizar_Click"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnActualizar_Click"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>

                        </Items>
                    </dx:ASPxFormLayout>


                </DetailRow>
                <FooterRow>
                    <asp:ImageButton ID="exportarPDF" runat="server" ImageUrl="~/Content/Images/pdf.png" ToolTip="Exportar a PDF" OnClick="exportarPDF_Click" />
                    <asp:ImageButton ID="exportarXLSX" runat="server" ImageUrl="~/Content/Images/xlsx.png" ToolTip="Exportar a MS-Excel 2007 o superior" OnClick="exportarXLSX_Click" />
                    <asp:ImageButton ID="exportarCSV" runat="server" ImageUrl="~/Content/Images/csv.png" ToolTip="Exportar a MS-Excel delimitado con punto y coma" OnClick="exportarCSV_Click" />
                </FooterRow>
            </Templates>
            <Border BorderWidth="0px" />
            <BorderBottom BorderWidth="1px" />

        </dx:ASPxGridView>
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1" FileName="Documentos Electrónicos">
            <Styles>
                <Default Font-Names="Arial" Font-Size="Small" />
            </Styles>
            <PageHeader Center="Facturación Web - Documentos Electrónicos">
                <Font Bold="True" Names="Arial" Size="Large" />
            </PageHeader>
            <PageFooter Left="[Page # of Pages #]" Right="[Date Printed][Time Printed]">
                <Font Names="Arial" Size="Small" />
            </PageFooter>
        </dx:ASPxGridViewExporter>

    </div>

</asp:Content>
