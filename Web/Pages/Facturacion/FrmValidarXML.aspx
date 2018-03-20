<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmValidarXML.aspx.cs" Inherits="Web.Pages.Facturacion.FrmValidarXML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">


    <script type="text/javascript">
        function onFileUploadComplete(s, e) {
            var boton = document.getElementById('<%=btnCargarDatos.ClientID%>');
             boton.click();
         }

    </script>
     
     <div class="text-box-title">
        <div class="text-box-heading-title"> Confirmar XML</div>
        <div class="arrow-down-title" style="margin-bottom: 5px;"></div>   
     </div>  
     
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always" OnUnload="UpdatePanel_Unload">
        <ContentTemplate>

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
                                        <dx:ASPxButton ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click" Style="visibility: hidden"></dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Clave">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtClave" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Número Cédula Emisor">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtNumCedEmisor" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false">
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
                                        <dx:ASPxTextBox ID="txtFechaEmisor" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false">
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
                                        <dx:ASPxComboBox ID="cmbMensaje" runat="server" SelectedIndex="0" Width="100%" AutoResizeWithContainer="true">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="Aceptado" Value="1" />
                                                <dx:ListEditItem Text="Rechazado Parcial" Value="2" />
                                                <dx:ListEditItem Text="Rechazado" Value="3" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>


                            <dx:LayoutItem Caption="Detalle del Mensaje" ColSpan="2" Width="100%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxMemo ID="txtDetalleMensaje" runat="server" Width="100%" AutoResizeWithContainer="true" MaxLength="80">
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Monto Total Impuesto">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtMontoTotalImpuesto" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false" PropertiesTextEdit-DisplayFormatString="n2">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="Total Factura">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txtTotalFactura" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false" PropertiesTextEdit-DisplayFormatString="n2">
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
                                        <dx:ASPxTextBox ID="txtNumCedReceptor" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="false">
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
                                        <dx:ASPxTextBox ID="txtNumConsecutivoReceptor" runat="server" Width="100%" AutoResizeWithContainer="true" Enabled="true">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>


                            <dx:LayoutItem Caption="" ColSpan="2">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxButton ID="btnEnviar" runat="server" Text="Enviar" Width="50px" CausesValidation="true" OnClick="btnEnviar_Click"></dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>


                        </Items>

                    </dx:LayoutGroup>


                </Items>

            </dx:ASPxFormLayout>
            <div id="alertMessages" role="alert" runat="server" />
             
        </ContentTemplate>
    </asp:UpdatePanel>

    <div style="text-align: center;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
            <ProgressTemplate>
                <div class="modalBlock">
                    <div class="centerBlock">
                        <img src="../../Content/Images/cargando.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

</asp:Content>