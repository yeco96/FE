<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="FrmPlan.aspx.cs" Inherits="Web.Pages.Administracion.FrmPlan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

     
    <div class="text-box-title">
        <div class="text-box-heading-title">Configuración Plan</div>
        <div class="arrow-down-title" style="margin-bottom: 5px;"></div>                        
     </div>   
        <div class="borde_redondo_tabla">

        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="ASPxGridView1" KeyboardSupport="True"
            Width="100%" EnableTheming="True" KeyFieldName="emisor" Theme="Moderno"  
            OnDetailRowExpandedChanged="ASPxGridView2_DetailRowExpandedChanged" > 
            <Columns>
                 

                <dx:GridViewDataTextColumn Caption="Emisor" FieldName="emisor" VisibleIndex="1"
                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Plan" FieldName="plan" VisibleIndex="1" Width="25%"
                    PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" PropertiesComboBox-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataComboBoxColumn>
                  <dx:GridViewDataSpinEditColumn Caption="Cantidad Doc Plan" FieldName="cantidadDocPlan" VisibleIndex="1" 
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn Caption="Cantidad Doc Emitidos" FieldName="cantidadDocEmitido" VisibleIndex="2" 
                    PropertiesSpinEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesSpinEdit-ValidationSettings-RequiredField-ErrorText="Requerido">
                </dx:GridViewDataSpinEditColumn>
               
                <dx:GridViewDataDateColumn  Caption="Fecha Inicio" FieldName="fechaInicio" VisibleIndex="6" PropertiesDateEdit-ValidationSettings-RequiredField-IsRequired="true"
                   >
                    <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn  Caption="Fecha Fin" FieldName="fechaFin" VisibleIndex="7" PropertiesDateEdit-ValidationSettings-RequiredField-IsRequired="true">
                    <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>


                <dx:GridViewDataComboBoxColumn Caption="Estado" FieldName="estado" VisibleIndex="4"
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

            <SettingsBehavior ColumnResizeMode="NextColumn" />
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
             
            <SettingsEditing Mode="EditFormAndDisplayRow" />
            <Settings VerticalScrollBarMode="Hidden" GridLines="Both" VerticalScrollableHeight="350" VerticalScrollBarStyle="Standard" ShowGroupPanel="false" ShowFilterRow="false" ShowTitlePanel="True" UseFixedTableLayout="True" />
           
            <SettingsDetail ShowDetailRow="false" />
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
                <Cell Wrap="False"></Cell>
                <AlternatingRow Enabled="true" />
            </Styles>
            <Templates>
               
                <FooterRow>
                    <asp:ImageButton ID="exportarPDF" runat="server" ImageUrl="~/Content/Images/pdf.png" ToolTip="Exportar a PDF" OnClick="exportarPDF_Click" />
                    <asp:ImageButton ID="exportarXLSX" runat="server" ImageUrl="~/Content/Images/xlsx.png" ToolTip="Exportar a MS-Excel 2007 o superior" OnClick="exportarXLSX_Click" />
                    <asp:ImageButton ID="exportarCSV" runat="server" ImageUrl="~/Content/Images/csv.png" ToolTip="Exportar a MS-Excel delimitado con punto y coma" OnClick="exportarCSV_Click" />
                </FooterRow>
                
            </Templates>
            <Border BorderWidth="0px" />
            <BorderBottom BorderWidth="1px" />

        </dx:ASPxGridView>
       
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1" FileName="Catálogo Plan">
            <Styles>
                <Default Font-Names="Arial" Font-Size="Small" />
            </Styles>
            <PageHeader Center="Facturación Web - Catálogo Plan">
                <Font Bold="True" Names="Arial" Size="Large" />
            </PageHeader>
            <PageFooter Left="[Page # of Pages #]" Right="[Date Printed][Time Printed]">
                <Font Names="Arial" Size="Small" />
            </PageFooter>
        </dx:ASPxGridViewExporter>
    </div>

</asp:Content>
