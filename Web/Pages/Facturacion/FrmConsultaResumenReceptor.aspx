<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmConsultaResumenReceptor.aspx.cs" Inherits="Web.Pages.Facturacion.FrmConsultaResumenReceptor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            Resumen Documentos Electrónicos
        </div>
    </section>

    <dx:ASPxFormLayout runat="server" AlignItemCaptionsInAllGroups="true">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Caption="Datos de Consulta" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                <Items>

                    <dx:LayoutItem Caption="Fecha Inicio">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit ID="txtFechaInicio" runat="server" Width="100%"  ValidationSettings-RequiredField-IsRequired="true"  DisplayFormatString="yyyy/MM/dd" EditFormatString="yyyy/MM/dd">
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
                                <dx:ASPxDateEdit ID="txtFechaFin" runat="server" Width="100%" ValidationSettings-RequiredField-IsRequired="true"  DisplayFormatString="yyyy/MM/dd" EditFormatString="yyyy/MM/dd">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="Requerido">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                      <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                 <dx:ASPxCheckBox ID="chkCambioMoneda" runat="server" Text="Activar cambio moneda USD a CRC" >
                                    </dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                 <dx:ASPxButton ID="txtConsultar" runat="server" Text="Consultar" Width="80px" Image-Url="~/Content/Images/search1.png" Image-Height="20px"
                                      CausesValidation="true" OnClick="btnConsultar_Click" >
                                    </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption=" " ClientVisible="false">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                 <dx:ASPxButton ID="btnReporte" runat="server" Text="Reporte" Width="80px" Image-Url="~/Content/Images/search1.png" Image-Height="20px"
                                      CausesValidation="true"  OnClick="btnReporte_Click" />  
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>

    <div class="borde_redondo_tabla">
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
            Width="100%" EnableTheming="True" KeyFieldName="clave" Theme="Moderno">
            <Columns>
                <dx:GridViewDataComboBoxColumn  Caption="Tipo Documento" FieldName="tipoDocumento" VisibleIndex="1" >
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Caption="Consecutivo" FieldName="consecutivo" VisibleIndex="2"  >
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Moneda" FieldName="codigoMoneda" VisibleIndex="3"  >
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tipo Cambio" FieldName="tipoCambio" VisibleIndex="4"   PropertiesTextEdit-DisplayFormatString="n2">
               </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Serv. Gravados" FieldName="totalServGravados" VisibleIndex="5" Visible="false"  PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Serv. Exentos" FieldName="totalServExentos" VisibleIndex="6" Visible="false"  PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Merc. Gravados" FieldName="totalMercanciasGravadas" VisibleIndex="7" Visible="false"   PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Merc. Exentas" FieldName="totalMercanciasExentas" VisibleIndex="8" Visible="false"  PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Gravado" FieldName="totalGravado" VisibleIndex="9"  PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Exento" FieldName="totalExento" VisibleIndex="10"  PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Venta" FieldName="totalVenta" VisibleIndex="11"  PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Descuentos" FieldName="totalDescuentos" VisibleIndex="12"  PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Venta Neta" FieldName="totalVentaNeta" VisibleIndex="13"  PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Impuesto" FieldName="totalImpuesto" VisibleIndex="14"  PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Comprobante" FieldName="totalComprobante" VisibleIndex="15"  PropertiesTextEdit-DisplayFormatString="n2">
                    
                </dx:GridViewDataTextColumn>
            </Columns>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="totalServGravados" SummaryType="Sum"  DisplayFormat="{0:n2}"/>
                <dx:ASPxSummaryItem FieldName="totalServExentos" SummaryType="Sum"  DisplayFormat="{0:n2}"/>

                <dx:ASPxSummaryItem FieldName="totalMercanciasGravadas" SummaryType="Sum"  DisplayFormat="{0:n2}"/>
                <dx:ASPxSummaryItem FieldName="totalMercanciasExentas" SummaryType="Sum" DisplayFormat="{0:n2}"/>

                <dx:ASPxSummaryItem FieldName="totalGravado" SummaryType="Sum" DisplayFormat="{0:n2}"/>
                <dx:ASPxSummaryItem FieldName="totalExento" SummaryType="Sum" DisplayFormat="{0:n2}"/>
                <dx:ASPxSummaryItem FieldName="totalVenta" SummaryType="Sum" DisplayFormat="{0:n2}"/>
                
                <dx:ASPxSummaryItem FieldName="totalDescuentos" SummaryType="Sum"  DisplayFormat="{0:n2}"/>
                <dx:ASPxSummaryItem FieldName="totalVentaNeta" SummaryType="Sum" DisplayFormat="{0:n2}"/>

                <dx:ASPxSummaryItem FieldName="totalImpuesto" SummaryType="Sum"  DisplayFormat="{0:n2}"/>
                <dx:ASPxSummaryItem FieldName="totalComprobante" SummaryType="Sum"  DisplayFormat="{0:n2}"/>
            </TotalSummary>

            
            <SettingsBehavior ColumnResizeMode="NextColumn" />
            <Settings ShowFooter="True" ShowFilterBar="Visible" ShowFilterRow="true" />
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
            <SettingsPager PageSize="10" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Items="10, 20, 30, 100" />
     
            <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="True" UseFixedTableLayout="True" />
            <SettingsContextMenu EnableColumnMenu="True" Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="True" EnableRowMenu="True" />
            <SettingsDetail ShowDetailRow="false" AllowOnlyOneMasterRowExpanded="true" />
            <SettingsDataSecurity AllowDelete="false" AllowEdit="false" AllowInsert="false" />

            <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800" AllowOnlyOneAdaptiveDetailExpanded="true" AdaptiveDetailColumnCount="1"></SettingsAdaptivity>
            <EditFormLayoutProperties>
                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
            </EditFormLayoutProperties>
            <Styles>
                <Cell Wrap="False"></Cell>
                <AlternatingRow Enabled="true" />
            </Styles>

        </dx:ASPxGridView>

        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1" FileName="Resumen Documentos">
            <Styles>
                <Default Font-Names="Arial" Font-Size="Small" />
            </Styles>
            <PageHeader Center="Facturación Web - Resumen Documentos">
                <Font Bold="True" Names="Arial" Size="Large" />
            </PageHeader>
            <PageFooter Left="[Page # of Pages #]" Right="[Date Printed][Time Printed]">
                <Font Names="Arial" Size="Small" />
            </PageFooter>
        </dx:ASPxGridViewExporter>
    </div>
    <div id="alertMessages" role="alert" runat="server" />
</asp:Content>
