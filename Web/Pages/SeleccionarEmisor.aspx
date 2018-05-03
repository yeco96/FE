<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="SeleccionarEmisor.aspx.cs" Inherits="Web.Pages.SeleccionarEmisor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

     <div class="text-box-title">
        <div class="text-box-heading-title">Emisores</div>
        <div class="arrow-down-title" style="margin-bottom: 5px;"></div>
    </div>
    <div class="borde_redondo_tabla">
                                <dx:ASPxGridView ID="ASPxGridViewEmisores" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
                                     EnableTheming="True" KeyFieldName="identificacion" Theme="Moderno"    EnableCallBacks="false"
                                    OnRowDeleting="ASPxGridViewEmisores_RowDeleting"
                                         Width="100%"   OnSelectionChanged="ASPxGridViewClientes_SelectionChanged" > 
                                    <Columns>
                                        <dx:GridViewCommandColumn Width="50px" ButtonType="Image" ShowSelectButton="true" ShowDeleteButton="true" ShowEditButton="false" ShowNewButtonInHeader="false" VisibleIndex="0" ShowClearFilterButton="True" Caption=" ">
                                        </dx:GridViewCommandColumn>

                                        <dx:GridViewDataComboBoxColumn Caption="Tipo" Visible="false" FieldName="identificacionTipo" VisibleIndex="2"
                                            PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                                           
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn Caption="Identificación" FieldName="identificacion" VisibleIndex="3" PropertiesTextEdit-MaxLength="12"
                                            Settings-AutoFilterCondition="Contains"
                                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                           
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="Nombre" FieldName="nombre" VisibleIndex="4" PropertiesTextEdit-MaxLength="30" Width="25%"
                                            Settings-AutoFilterCondition="Contains"
                                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                           
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn Caption="Nombre Comercial" FieldName="nombreComercial" VisibleIndex="6" PropertiesTextEdit-MaxLength="80" Visible="true" EditFormSettings-Visible="True"
                                            Settings-AutoFilterCondition="Contains"
                                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="false" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            
                                            <Settings AutoFilterCondition="Contains" /> 
                                        </dx:GridViewDataTextColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="Teléfono" FieldName="telefono" VisibleIndex="7" PropertiesSpinEdit-MaxLength="20"
                                            PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                            <PropertiesSpinEdit DisplayFormatString="g" MaxLength="20">
                                              
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                 
                                        <dx:GridViewDataTextColumn Caption="Correo" FieldName="correoElectronicoPrincipal" VisibleIndex="8" PropertiesTextEdit-MaxLength="80" Width="25%"
                                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                                          
                                        </dx:GridViewDataTextColumn>
                  
                                    </Columns>

                                      <SettingsSearchPanel Visible="true"  />
                                    <SettingsBehavior ColumnResizeMode="NextColumn" />
                                    <Settings  ShowFooter="false" ShowFilterBar="Hidden" ShowFilterRow="true"  />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="false" ConfirmDelete="True" ProcessSelectionChangedOnServer="True" AllowSelectSingleRowOnly="true" />
                                    <SettingsPager PageSize="10" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Items="10, 20, 50, 100" >
                                   
                                        <PageSizeItemSettings Items="10, 20, 50, 100" Visible="True">
                                        </PageSizeItemSettings>
                                    </SettingsPager>
                                   
                                    <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="false" ShowFilterRow="false" ShowTitlePanel="True" UseFixedTableLayout="True" />
                                    <SettingsContextMenu EnableColumnMenu="True" Enabled="True" EnableFooterMenu="True" EnableGroupPanelMenu="True" EnableRowMenu="True" />
                                    <SettingsDataSecurity AllowDelete="true" AllowInsert="false" AllowEdit="false" />
                                    <SettingsCommandButton>
                                        <SelectButton Image-ToolTip="Seleccionar" Image-Url="~/Content/Images/search1.png" > 
                                            <Image ToolTip="Seleccionar" Url="~/Content/Images/search1.png">
                                            </Image>
                                        </SelectButton>
                                         <DeleteButton Image-ToolTip="Eliminar" Image-Url="~/Content/Images/delete.png" />
                                        <ClearFilterButton Image-ToolTip="Quitar filtros" Image-Url="~/Content/Images/refresh.png" > 
                                            <Image ToolTip="Quitar filtros" Url="~/Content/Images/refresh.png">
                                            </Image>
                                        </ClearFilterButton>
                                    </SettingsCommandButton>

                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" HideDataCellsAtWindowInnerWidth="800" AllowOnlyOneAdaptiveDetailExpanded="true" AdaptiveDetailColumnCount="1"></SettingsAdaptivity>
                                    <EditFormLayoutProperties>
                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="600" />
                                    </EditFormLayoutProperties>
                                    <Styles>
                                        <Cell Wrap="True"></Cell>
                                        <AlternatingRow Enabled="true" />
                                    </Styles>
             
                                    <Border BorderWidth="0px" />
                                    <BorderBottom BorderWidth="1px" />

                                </dx:ASPxGridView>
</div>
</asp:Content>
