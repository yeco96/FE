using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace Desktop.Configuración.Catalogos
{
    public partial class FrmCatalogoEmisor : DevExpress.XtraEditors.XtraForm
    {
        public FrmCatalogoEmisor()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}