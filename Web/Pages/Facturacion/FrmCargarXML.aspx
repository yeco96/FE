<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmCargarXML.aspx.cs" Inherits="Web.Pages.Facturacion.FrmCargarXML" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout runat="server">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Caption="Datos Personales" ColCount="4" GroupBoxDecoration="Box" UseDefaultPaddings="false">
                <Items>
                    <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                               
                                 <dx:ASPxUploadControl runat="server" ID="xmlUploadControl" ClientInstanceName="DocumentsUploadControl" Width="100%"
                                                AutoStartUpload="true" ShowProgressPanel="True" ShowTextBox="false" BrowseButton-Text="Subir XML" FileUploadMode="OnPageLoad"
                                                OnFileUploadComplete="DocumentsUploadControl_FileUploadComplete">
                                                <AdvancedModeSettings EnableMultiSelect="false" EnableDragAndDrop="true" ExternalDropZoneID="dropZone" />
                                                <ValidationSettings
                                                    AllowedFileExtensions=".xml"
                                                    MaxFileSize="4194304">
                                                </ValidationSettings>
                                                 
                                            </dx:ASPxUploadControl>

                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                      <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                               <dx:ASPxButton ID="btnMostrarXML" runat="server" Width="50px" Text="Mostrar XML" OnClick="btnMostrarXML_Click"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                     <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                               <dx:ASPxButton ID="btnFirmar" runat="server" Width="50px" Text="Firmar XML" OnClick="btnFirmar_Click"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
 
                    <dx:LayoutItem Caption=" ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                               <dx:ASPxButton ID="btnEnviar" runat="server"  Width="50px" Text="Enviar XML" OnClick="btnEnviar_Click"></dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                </Items>
            </dx:LayoutGroup>


        </Items>
    </dx:ASPxFormLayout>
    
     <dx:ASPxHtmlEditor ID="HtmlEditor" runat="server" Height="370px" ClientEnabled="true" Width="100%" OnHtmlCorrecting="HTMLEditorXMLData_HtmlCorrecting">
          <Settings AllowHtmlView="true" AllowDesignView="false" AllowPreview="false" />    
    </dx:ASPxHtmlEditor>



</asp:Content>
