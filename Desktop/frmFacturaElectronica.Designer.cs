namespace Desktop
{
    partial class frmFacturaElectronica
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacturaElectronica));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.msbLogin = new System.Windows.Forms.ToolStripDropDownButton();
            this.msbDocumentos = new System.Windows.Forms.ToolStripDropDownButton();
            this.smiConfirmarXML = new System.Windows.Forms.ToolStripMenuItem();
            this.msiConsulta = new System.Windows.Forms.ToolStripMenuItem();
            this.msiHistorico = new System.Windows.Forms.ToolStripMenuItem();
            this.msiFacturar = new System.Windows.Forms.ToolStripMenuItem();
            this.msbConfiguracion = new System.Windows.Forms.ToolStripDropDownButton();
            this.smiNumeroConsecutivo = new System.Windows.Forms.ToolStripMenuItem();
            this.smiCorreoElectronico = new System.Windows.Forms.ToolStripMenuItem();
            this.smiCatalogos = new System.Windows.Forms.ToolStripMenuItem();
            this.ssmEmisor = new System.Windows.Forms.ToolStripMenuItem();
            this.ssmClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.ssmEmpresa = new System.Windows.Forms.ToolStripMenuItem();
            this.ssmProductosServicios = new System.Windows.Forms.ToolStripMenuItem();
            this.ssmProductoImpuesto = new System.Windows.Forms.ToolStripMenuItem();
            this.smiPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.smiSeguridad = new System.Windows.Forms.ToolStripMenuItem();
            this.ssmUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.msbAyuda = new System.Windows.Forms.ToolStripDropDownButton();
            this.smiContacto = new System.Windows.Forms.ToolStripMenuItem();
            this.smiManual = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msbSalir = new System.Windows.Forms.ToolStripDropDownButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.consuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msbLogin,
            this.msbDocumentos,
            this.msbConfiguracion,
            this.msbAyuda,
            this.msbSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(751, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // msbLogin
            // 
            this.msbLogin.Image = ((System.Drawing.Image)(resources.GetObject("msbLogin.Image")));
            this.msbLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msbLogin.Name = "msbLogin";
            this.msbLogin.Size = new System.Drawing.Size(66, 22);
            this.msbLogin.Text = "&Login";
            // 
            // msbDocumentos
            // 
            this.msbDocumentos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.msbDocumentos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiConfirmarXML,
            this.msiConsulta,
            this.msiHistorico,
            this.msiFacturar,
            this.consuToolStripMenuItem});
            this.msbDocumentos.Image = ((System.Drawing.Image)(resources.GetObject("msbDocumentos.Image")));
            this.msbDocumentos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msbDocumentos.Name = "msbDocumentos";
            this.msbDocumentos.Size = new System.Drawing.Size(88, 22);
            this.msbDocumentos.Text = "&Documentos";
            // 
            // smiConfirmarXML
            // 
            this.smiConfirmarXML.Name = "smiConfirmarXML";
            this.smiConfirmarXML.Size = new System.Drawing.Size(155, 22);
            this.smiConfirmarXML.Text = "Confirmar &XML";
            // 
            // msiConsulta
            // 
            this.msiConsulta.Name = "msiConsulta";
            this.msiConsulta.Size = new System.Drawing.Size(155, 22);
            this.msiConsulta.Text = "&Consulta";
            // 
            // msiHistorico
            // 
            this.msiHistorico.Name = "msiHistorico";
            this.msiHistorico.Size = new System.Drawing.Size(155, 22);
            this.msiHistorico.Text = "&Histórico";
            // 
            // msiFacturar
            // 
            this.msiFacturar.Name = "msiFacturar";
            this.msiFacturar.Size = new System.Drawing.Size(155, 22);
            this.msiFacturar.Text = "&Facturar";
            // 
            // msbConfiguracion
            // 
            this.msbConfiguracion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiNumeroConsecutivo,
            this.smiCorreoElectronico,
            this.smiCatalogos,
            this.smiPlan,
            this.smiSeguridad});
            this.msbConfiguracion.Image = ((System.Drawing.Image)(resources.GetObject("msbConfiguracion.Image")));
            this.msbConfiguracion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msbConfiguracion.Name = "msbConfiguracion";
            this.msbConfiguracion.Size = new System.Drawing.Size(112, 22);
            this.msbConfiguracion.Text = "&Configuración";
            // 
            // smiNumeroConsecutivo
            // 
            this.smiNumeroConsecutivo.Name = "smiNumeroConsecutivo";
            this.smiNumeroConsecutivo.Size = new System.Drawing.Size(203, 22);
            this.smiNumeroConsecutivo.Text = "&Número de Consecutivo";
            // 
            // smiCorreoElectronico
            // 
            this.smiCorreoElectronico.Name = "smiCorreoElectronico";
            this.smiCorreoElectronico.Size = new System.Drawing.Size(203, 22);
            this.smiCorreoElectronico.Text = "Correo &Electrónico";
            // 
            // smiCatalogos
            // 
            this.smiCatalogos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssmEmisor,
            this.ssmClientes,
            this.ssmEmpresa,
            this.ssmProductosServicios,
            this.ssmProductoImpuesto});
            this.smiCatalogos.Name = "smiCatalogos";
            this.smiCatalogos.Size = new System.Drawing.Size(203, 22);
            this.smiCatalogos.Text = "&Catálogos";
            // 
            // ssmEmisor
            // 
            this.ssmEmisor.Name = "ssmEmisor";
            this.ssmEmisor.Size = new System.Drawing.Size(180, 22);
            this.ssmEmisor.Text = "&Emisor";
            // 
            // ssmClientes
            // 
            this.ssmClientes.Name = "ssmClientes";
            this.ssmClientes.Size = new System.Drawing.Size(180, 22);
            this.ssmClientes.Text = "&Clientes";
            // 
            // ssmEmpresa
            // 
            this.ssmEmpresa.Name = "ssmEmpresa";
            this.ssmEmpresa.Size = new System.Drawing.Size(180, 22);
            this.ssmEmpresa.Text = "E&mpresa";
            // 
            // ssmProductosServicios
            // 
            this.ssmProductosServicios.Name = "ssmProductosServicios";
            this.ssmProductosServicios.Size = new System.Drawing.Size(180, 22);
            this.ssmProductosServicios.Text = "Producto / &Servicios";
            // 
            // ssmProductoImpuesto
            // 
            this.ssmProductoImpuesto.Name = "ssmProductoImpuesto";
            this.ssmProductoImpuesto.Size = new System.Drawing.Size(180, 22);
            this.ssmProductoImpuesto.Text = "&Producto Impuesto";
            // 
            // smiPlan
            // 
            this.smiPlan.Name = "smiPlan";
            this.smiPlan.Size = new System.Drawing.Size(203, 22);
            this.smiPlan.Text = "&Plan";
            // 
            // smiSeguridad
            // 
            this.smiSeguridad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssmUsuario});
            this.smiSeguridad.Name = "smiSeguridad";
            this.smiSeguridad.Size = new System.Drawing.Size(203, 22);
            this.smiSeguridad.Text = "&Seguridad";
            // 
            // ssmUsuario
            // 
            this.ssmUsuario.Name = "ssmUsuario";
            this.ssmUsuario.Size = new System.Drawing.Size(114, 22);
            this.ssmUsuario.Text = "&Usuario";
            // 
            // msbAyuda
            // 
            this.msbAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiContacto,
            this.smiManual,
            this.acercaDeToolStripMenuItem});
            this.msbAyuda.Image = ((System.Drawing.Image)(resources.GetObject("msbAyuda.Image")));
            this.msbAyuda.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msbAyuda.Name = "msbAyuda";
            this.msbAyuda.Size = new System.Drawing.Size(70, 22);
            this.msbAyuda.Text = "&Ayuda";
            // 
            // smiContacto
            // 
            this.smiContacto.Name = "smiContacto";
            this.smiContacto.Size = new System.Drawing.Size(127, 22);
            this.smiContacto.Text = "&Contacto";
            // 
            // smiManual
            // 
            this.smiManual.Name = "smiManual";
            this.smiManual.Size = new System.Drawing.Size(127, 22);
            this.smiManual.Text = "&Manual";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.acercaDeToolStripMenuItem.Text = "&Acerca De";
            // 
            // msbSalir
            // 
            this.msbSalir.Image = ((System.Drawing.Image)(resources.GetObject("msbSalir.Image")));
            this.msbSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msbSalir.Name = "msbSalir";
            this.msbSalir.Size = new System.Drawing.Size(58, 22);
            this.msbSalir.Text = "&Salir";
            this.msbSalir.Click += new System.EventHandler(this.msbSalir_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 376);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(751, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // consuToolStripMenuItem
            // 
            this.consuToolStripMenuItem.Name = "consuToolStripMenuItem";
            this.consuToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.consuToolStripMenuItem.Text = "Consultar XML &Recibidos";
            this.consuToolStripMenuItem.Click += new System.EventHandler(this.consuToolStripMenuItem_Click);
            // 
            // frmFacturaElectronica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 398);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "frmFacturaElectronica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSAFactura";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmFacturaElectronica_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton msbSalir;
        private System.Windows.Forms.ToolStripDropDownButton msbDocumentos;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton msbAyuda;
        private System.Windows.Forms.ToolStripDropDownButton msbConfiguracion;
        private System.Windows.Forms.ToolStripDropDownButton msbLogin;
        private System.Windows.Forms.ToolStripMenuItem smiConfirmarXML;
        private System.Windows.Forms.ToolStripMenuItem msiConsulta;
        private System.Windows.Forms.ToolStripMenuItem msiHistorico;
        private System.Windows.Forms.ToolStripMenuItem msiFacturar;
        private System.Windows.Forms.ToolStripMenuItem smiNumeroConsecutivo;
        private System.Windows.Forms.ToolStripMenuItem smiCorreoElectronico;
        private System.Windows.Forms.ToolStripMenuItem smiCatalogos;
        private System.Windows.Forms.ToolStripMenuItem ssmEmisor;
        private System.Windows.Forms.ToolStripMenuItem ssmClientes;
        private System.Windows.Forms.ToolStripMenuItem ssmEmpresa;
        private System.Windows.Forms.ToolStripMenuItem ssmProductosServicios;
        private System.Windows.Forms.ToolStripMenuItem ssmProductoImpuesto;
        private System.Windows.Forms.ToolStripMenuItem smiPlan;
        private System.Windows.Forms.ToolStripMenuItem smiSeguridad;
        private System.Windows.Forms.ToolStripMenuItem ssmUsuario;
        private System.Windows.Forms.ToolStripMenuItem smiContacto;
        private System.Windows.Forms.ToolStripMenuItem smiManual;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consuToolStripMenuItem;
    }
}

