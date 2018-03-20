namespace Desktop.Configuración.Catalogos
{
    partial class FrmCatalogoCodigoPais
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridControl1dgvDatos = new DevExpress.XtraGrid.GridControl();
            this.dgvDatos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.codigo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.descripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.estado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1dgvDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(867, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.salirToolStripMenuItem.Text = "&Salir";
            // 
            // gridControl1dgvDatos
            // 
            this.gridControl1dgvDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1dgvDatos.Location = new System.Drawing.Point(0, 24);
            this.gridControl1dgvDatos.MainView = this.dgvDatos;
            this.gridControl1dgvDatos.Name = "gridControl1dgvDatos";
            this.gridControl1dgvDatos.Size = new System.Drawing.Size(867, 385);
            this.gridControl1dgvDatos.TabIndex = 1;
            this.gridControl1dgvDatos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvDatos});
            // 
            // dgvDatos
            // 
            this.dgvDatos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.codigo,
            this.descripcion,
            this.estado});
            this.dgvDatos.GridControl = this.gridControl1dgvDatos;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.OptionsView.ShowFooter = true;
            // 
            // codigo
            // 
            this.codigo.Caption = "Código";
            this.codigo.Name = "codigo";
            this.codigo.Visible = true;
            this.codigo.VisibleIndex = 0;
            this.codigo.Width = 151;
            // 
            // descripcion
            // 
            this.descripcion.Caption = "Descripción";
            this.descripcion.Name = "descripcion";
            this.descripcion.Visible = true;
            this.descripcion.VisibleIndex = 1;
            this.descripcion.Width = 454;
            // 
            // estado
            // 
            this.estado.Caption = "Estado";
            this.estado.Name = "estado";
            this.estado.Visible = true;
            this.estado.VisibleIndex = 2;
            this.estado.Width = 244;
            // 
            // FrmCatalogoCodigoPais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 409);
            this.Controls.Add(this.gridControl1dgvDatos);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmCatalogoCodigoPais";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo Código de País";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1dgvDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private DevExpress.XtraGrid.GridControl gridControl1dgvDatos;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvDatos;
        private DevExpress.XtraGrid.Columns.GridColumn codigo;
        private DevExpress.XtraGrid.Columns.GridColumn descripcion;
        private DevExpress.XtraGrid.Columns.GridColumn estado;
    }
}