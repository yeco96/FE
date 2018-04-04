using Desktop.Configuración.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Desktop
{
    public partial class frmFacturaElectronica : DevExpress.XtraEditors.XtraForm
    {
        public frmFacturaElectronica()
        {
            InitializeComponent();
        }

        private void frmFacturaElectronica_Load(object sender, EventArgs e)
        {

        }

        private void msbSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void consuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ssmCodigoPais_Click(object sender, EventArgs e)
        {
            FrmCatalogoCodigoPais oFormulario = new FrmCatalogoCodigoPais();
            oFormulario.MdiParent = this;
            oFormulario.Show();
        }

        private void smiCorreoElectronico_Click(object sender, EventArgs e)
        {
          
        }
    }
}
