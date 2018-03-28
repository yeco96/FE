<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmPagoComision.aspx.cs" Inherits="Web.Pages.Administracion.FrmPagoComision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <div class="text-box-title">
        <div class="text-box-heading-title">Pago de Comisiones MSASOFT</div>
        <div class="arrow-down-title" style="margin-bottom: 5px;"></div>
    </div>


    <dx:ASPxFormLayout runat="server" AlignItemCaptionsInAllGroups="true">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Caption="Datos de Consulta" ColCount="3" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                <Items>

                    <dx:LayoutItem Caption="Fecha Inicio">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit ID="txtFechaInicio" runat="server" Width="100%" ValidationSettings-RequiredField-IsRequired="true" DisplayFormatString="yyyy/MM/dd" EditFormatString="yyyy/MM/dd">
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
                                <dx:ASPxDateEdit ID="txtFechaFin" runat="server" Width="100%" ValidationSettings-RequiredField-IsRequired="true" DisplayFormatString="yyyy/MM/dd" EditFormatString="yyyy/MM/dd">
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
                                <dx:ASPxButton ID="btnConsultar" runat="server" Text="Consultar" Width="80px" Image-Url="~/Content/Images/search1.png" Image-Height="20px"
                                    CausesValidation="true" OnClick="btnConsultar_Click">
                                </dx:ASPxButton>

                                <dx:ASPxButton ID="btnReporte" runat="server" Text="Reporte" Width="80px" Image-Url="~/Content/Images/pdf.png" Image-Height="20px"
                                    CausesValidation="true" OnClick="btnReporte_Click">
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>


                </Items>
            </dx:LayoutGroup>

        </Items>
    </dx:ASPxFormLayout>

    <div class="borde_redondo_tabla">
        <dx:ASPxGridView ID="dgvDatos" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
            Width="100%" EnableTheming="True" KeyFieldName="consecutivo" Theme="Moderno">
            <Columns>
                <dx:GridViewDataTextColumn Caption="Cliente" FieldName="cliente" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Plan" FieldName="plan" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Fecha de Pago" FieldName="fechaPago" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Monto Pago" FieldName="montoPago" VisibleIndex="4" PropertiesTextEdit-DisplayFormatString="n2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Comisión Anchía" FieldName="comision1" VisibleIndex="5" PropertiesTextEdit-DisplayFormatString="n2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Comisión Ale" FieldName="comision2" VisibleIndex="6" PropertiesTextEdit-DisplayFormatString="n2">
                </dx:GridViewDataTextColumn>

            </Columns>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="montoPago" SummaryType="Sum" DisplayFormat="{0:n2}" />
                <dx:ASPxSummaryItem FieldName="comision1" SummaryType="Sum" DisplayFormat="{0:n2}" />
                <dx:ASPxSummaryItem FieldName="comision2" SummaryType="Sum" DisplayFormat="{0:n2}" />
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

        <dx:ASPxFormLayout runat="server" AlignItemCaptionsInAllGroups="true">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Caption="Pago de Comisiones" ColCount="2" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                <Items>

                    <dx:LayoutItem Caption="Pago Comisión Anchia">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txtPagoComision1" Width="100%" AutoResizeWithContainer="true" runat="server" DisplayFormatString="N2" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" PropertiesTextEdit-DisplayFormatString="n2" RightToLeft="True">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                    </ValidationSettings>
                                 </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Pago Comisión Ale">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txtPagoComision2" Width="100%" AutoResizeWithContainer="true" runat="server" DisplayFormatString="N2" ValidationSettings-ErrorDisplayMode="ImageWithTooltip" PropertiesTextEdit-DisplayFormatString="n2" RightToLeft="True">
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

    <div id="alertMessages" role="alert" runat="server" />
</asp:Content>


