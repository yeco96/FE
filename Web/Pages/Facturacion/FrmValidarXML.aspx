<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmValidarXML.aspx.cs" Inherits="Web.Pages.Facturacion.FrmValidarXML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">



    <script type="text/javascript">
        function onFileUploadComplete(s, e) {
            var boton = document.getElementById('<%=btnCargarDatos.ClientID%>');
            boton.click();
        }

    </script>


    <dx:ASPxFormLayout runat="server">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Caption="" ColCount="2" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                <Items>

                    <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxUploadControl runat="server" ID="xmlUploadControl1" ClientInstanceName="DocumentsUploadControl" Width="100%"
                                    AutoStartUpload="true" ShowProgressPanel="True" ShowTextBox="true" NullText="Seleccione un archivo XML ..."
                                    BrowseButton-Text="Cargar XML" FileUploadMode="OnPageLoad"
                                    OnFileUploadComplete="xmlUploadControl_FileUploadComplete">
                                    <AdvancedModeSettings EnableMultiSelect="false" EnableDragAndDrop="true" ExternalDropZoneID="dropZone" EnableFileList="True" />
                                    <ValidationSettings
                                        AllowedFileExtensions=".xml"
                                        MaxFileSize="4194304">
                                    </ValidationSettings>
                                    <ClientSideEvents FileUploadComplete="onFileUploadComplete" />
                                </dx:ASPxUploadControl>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton ID="btnCargarDatos" Visible="true" runat="server" Width="50px" Text="Cargar Valores del XML" OnClick="btnCargarDatos_Click"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo ID="txtXML" runat="server" Width="170px"></dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    
                </Items>

            </dx:LayoutGroup>


        </Items>

    </dx:ASPxFormLayout>
    <table style="width: 100%;">
        <tr>
            <td>
                <dx:ASPxLabel ID="lblClave" runat="server" Text="Clave : "></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtClave" runat="server" Width="170px" Enabled="false">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td>
                <dx:ASPxLabel ID="lblNumCedEmisor" runat="server" Text="Número Cédula Emisor : "></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtNumCedEmisor" runat="server" Width="170px" Enabled="false">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td>
                <dx:ASPxLabel ID="lblFechaEmisor" runat="server" Text="Fecha Emisor : "></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtFechaEmisor" runat="server" Width="170px" Enabled="false">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td>
                <dx:ASPxLabel ID="lblmensaje" runat="server" Text="Mensaje : "></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbMensaje" runat="server" SelectedIndex="0">
                    <Items>
                        <dx:ListEditItem Selected="True" Text="Aceptado" Value="0" />
                        <dx:ListEditItem Selected="True" Text="Rechazado Parcial" Value="1" />
                        <dx:ListEditItem Text="Rechazado" Value="2" />
                    </Items>
                </dx:ASPxComboBox>
            </td>
        </tr>

        <tr>
            <td>
                <dx:ASPxLabel ID="lblDetalleMensaje" runat="server" Text="Detalle Mensaje : "></dx:ASPxLabel>
            </td>
            <td>
                <%--<dx:ASPxMemo ID="txtDetalleMensaje" runat="server" Height="71px" Width="170px">
                </dx:ASPxMemo>--%>
                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px" Enabled="false">
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="lblMontotalImpuesto" runat="server" Text="Monto Total Impuesto : "></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtMontoTotalImpuesto" runat="server" Width="170px" Enabled="false">
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td>
                <dx:ASPxLabel ID="lblTotalFactura" runat="server" Text="Total Factura : "></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtTotalFactura" runat="server" Width="170px" Enabled="false">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td>
                <dx:ASPxLabel ID="lblNumCedReceptor" runat="server" Text="Número Cédula Receptor : "></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtNumCedReceptor" runat="server" Width="170px" Enabled="false">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>

        <tr>
            <td>
                <dx:ASPxLabel ID="lblNumConsecReceptor" runat="server" Text="Número Consecutivo Receptor : "></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtNumConsecReceptor" runat="server" Width="170px" Enabled="false">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>

    </table>
</asp:Content>
