using Class.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.Facturacion
{
    public partial class FrmConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Utilidades.getCulture();
            this.AsyncMode = true;
            //Se ingresa el valor en la caja de texto
            this.txtDocumentoNum.Text = "50608011800060354097400100001010000000010188888888";
            //Se llama el método del botón
            this.btnBuscar_Click(null, null);
        }

        protected void UpdatePanel_Unload(object sender, EventArgs e)
        {
            RegisterUpdatePanel((UpdatePanel)sender);
        }
        protected void RegisterUpdatePanel(UpdatePanel panel)
        {
            var sType = typeof(ScriptManager);
            var mInfo = sType.GetMethod("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel", BindingFlags.NonPublic | BindingFlags.Instance);
            if (mInfo != null)
                mInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { panel });
        }

        /// <summary>
        /// Método que Genera la vista de la factura electrónica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //ASPxWebDocumentViewer1.OpenReport(UtilidadesReporte.generarDocumento(txtDocumentoNum.Text));
            UtilidadesReporte.generarDocumento(txtDocumentoNum.Text);
        }

    }
}