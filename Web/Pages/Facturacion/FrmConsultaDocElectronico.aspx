﻿<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmConsultaDocElectronico.aspx.cs" Inherits="Web.Pages.Facturacion.FrmConsultaDocElectronico" %>

<%@ Register Src="~/UserControls/AddAuditoriaForm.ascx" TagPrefix="user" TagName="AddAuditoriaForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <section class="featured">
        <div class="content-wrapper">
            Documentos Electrónicos
        </div>
    </section>
    <div class="borde_redondo_tabla">

        <dx:ASPxFormLayout runat="server" AlignItemCaptionsInAllGroups="true">
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
            <Items>
                <dx:LayoutGroup Caption="Datos de Consulta" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                    <Items>

                        <dx:LayoutItem Caption="Fecha Inicio">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxDateEdit ID="txtFechaInicio" runat="server" Width="100%">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Requerido">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <dx:LayoutItem Caption="Fecha Fin">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxDateEdit ID="txtFechaFin" runat="server" Width="100%" ValidationSettings-RequiredField-IsRequired="true">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Requerido">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>

                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxButton ID="txtCorreoReceptor" runat="server" Text="Consultar" Width="80px" Image-Url="~/Content/Images/search1.png" CausesValidation="true">
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>

            </Items>
        </dx:ASPxFormLayout>

        <div id="alertMessages" role="alert" runat="server" />
        
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
            Width="100%" EnableTheming="True" KeyFieldName="clave" Theme="Moderno"
            OnDetailRowExpandedChanged="ASPxGridView1_DetailRowExpandedChanged"
            OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize">
            <Columns>

                <dx:GridViewDataTextColumn Caption="Clave" FieldName="clave" VisibleIndex="2" Visible="false">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Consecutivo" FieldName="numeroConsecutivo" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Fecha" FieldName="fecha" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Emisor" FieldName="emisorIdentificacion" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Receptor" FieldName="Receptor.nombreCompleto" VisibleIndex="5" Width="25%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Mensaje" FieldName="mensaje" VisibleIndex="6" Visible="false">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataComboBoxColumn Caption="Estado" FieldName="indEstado" VisibleIndex="7">
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Caption="Monto Impuesto" FieldName="montoTotalImpuesto" VisibleIndex="8" PropertiesTextEdit-DisplayFormatString="c2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Monto Factura" FieldName="montoTotalFactura" VisibleIndex="9" PropertiesTextEdit-DisplayFormatString="c2">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataComboBoxColumn Caption="Tipo" FieldName="tipoDocumento" VisibleIndex="10">
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataTextColumn Visible="false" Caption="Usuario Creación" FieldName="usuarioCreacion" VisibleIndex="11">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Visible="false" Caption="Fecha Creación" FieldName="fechaCreacion" VisibleIndex="12">
                    <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="" EditFormatString="dd/MM/yyyy hh:mm:ss"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Visible="false" Caption="Usuario Modificación" FieldName="usuarioModificacion" VisibleIndex="13">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Visible="false" Caption="Fecha Modificación" FieldName="fechaModificacion" VisibleIndex="14">
                    <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="" EditFormatString="dd/MM/yyyy hh:mm:ss"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
            </Columns>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="montoTotalImpuesto" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="montoTotalFactura" SummaryType="Sum" />
            </TotalSummary>

            <SettingsBehavior ColumnResizeMode="NextColumn" />
            <Settings ShowFooter="True" ShowFilterBar="Visible" ShowFilterRow="true" />
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
            <SettingsPager PageSize="10" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Items="10, 20, 30, 100" />
            <SettingsEditing Mode="EditFormAndDisplayRow" />
            <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="True" UseFixedTableLayout="True" />
            <SettingsContextMenu EnableColumnMenu="True" Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="True" EnableRowMenu="True" />
            <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="true" />

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
                            <dx:LayoutGroup Caption=" " ColCount="4" GroupBoxDecoration="None" UseDefaultPaddings="false">

                                <Items>
                                    <dx:LayoutItem Caption="Mensaje" ColSpan="4" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxMemo runat="server" Value='<%# Eval("mensaje") %>' AutoResizeWithContainer="true" Width="100%" ReadOnly="true" />

                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnDescargarXML" runat="server" Text="Descargar XML" OnClick="btnDescargarXML_Click" Image-Url="~/Content/Images/xml.png"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>


                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnDescargarPDF" runat="server" Text="Descargar PDF" OnClick="btnDescargarPDF_Click" Image-Url="~/Content/Images/pdf2.png"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnReenvioCorreo" runat="server" Text="Reenviar Correo" OnClick="btnReenvioCorreo_Click" Image-Url="~/Content/Images/send.png"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnActualizar" runat="server" Text="Actualizar Información" OnClick="btnActualizar_Click" Image-Url="~/Content/Images/refresh2.png"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnNotaCredito" runat="server" Text="Crear Nota Crédito" OnClick="btnNotaCredito_Click" Image-Url="~/Content/Images/nota_credito.png"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnNotaDebito" runat="server" Text="Crear Nota Débito" OnClick="btnNotaDebito_Click" Image-Url="~/Content/Images/nota_debito.png"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btnEnvioManual" runat="server" Text="Reenvio Hacienda" OnClick="btnEnvioManual_Click" Image-Url="~/Content/Images/send2.png"></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    
                                    <dx:LayoutItem Caption="">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" ></dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                </Items>
                            </dx:LayoutGroup>

                        </Items>
                    </dx:ASPxFormLayout>


                </DetailRow>
                <TitlePanel>
                    <div style="text-align: right;">
                        <asp:ImageButton ID="exportarPDF" runat="server" ImageUrl="~/Content/Images/pdf.png" Text="Exportar a PDF" OnClick="exportarPDF_Click" />
                        <asp:ImageButton ID="exportarXLSX" runat="server" ImageUrl="~/Content/Images/xlsx.png" Text="Exportar a MS-Excel 2007 o superior" OnClick="exportarXLSX_Click" />
                        <asp:ImageButton ID="exportarCSV" runat="server" ImageUrl="~/Content/Images/csv.png" Text="Exportar a MS-Excel delimitado con punto y coma" OnClick="exportarCSV_Click" />
                    </div>
                </TitlePanel>
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
