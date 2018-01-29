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

                    <dx:LayoutItem Caption="Clave">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txtClave" runat="server" Width="400px" Enabled="false">
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Número Cédula Emisor">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txtNumCedEmisor" runat="server" Width="400px" Enabled="false">
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Fecha del Emisor">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txtFechaEmisor" runat="server" Width="400px" Enabled="false">
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Mensaje">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox ID="cmbMensaje" runat="server" SelectedIndex="0" Width="400px">
                                    <Items>
                                        <dx:ListEditItem Selected="True" Text="Aceptado" Value="0" />
                                        <dx:ListEditItem Selected="True" Text="Rechazado Parcial" Value="1" />
                                        <dx:ListEditItem Text="Rechazado" Value="2" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Detalle del Mensaje">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo ID="txtDetalleMensaje" runat="server" Width="400px" Height="100px">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Monto Total Impuesto">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txtMontoTotalImpuesto" runat="server" Width="400px" Enabled="false" PropertiesTextEdit-DisplayFormatString="c2">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Total Factura">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txtTotalFactura" runat="server" Width="400px" Enabled="false" PropertiesTextEdit-DisplayFormatString="c2">
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Número Cédula del Receptor">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txtNumCedReceptor" runat="server" Width="400px" Enabled="false">
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Número Consecutivo Receptor">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txtNumConsecReceptor" runat="server" Width="400px" Enabled="false">
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>


                </Items>

            </dx:LayoutGroup>


        </Items>

    </dx:ASPxFormLayout>

</asp:Content>
