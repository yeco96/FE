using Class.Utilidades;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Models;
using Web.Models.Facturacion;

namespace Web.Pages
{
    [PrincipalPermission(SecurityAction.Demand, Role = "SUPER")]
    public partial class SeleccionarEmisor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var conexion = new DataModelFE())
            {
                /* EMISORES POR SUPERVISOR */
                string supervisorUser = Session["usuario"].ToString();
                this.ASPxGridViewEmisores.DataSource = (from Emisor in conexion.EmisorReceptorIMEC
                                                        from supervisor in conexion.Supervisor
                                                        where Emisor.identificacion == supervisor.emisor && supervisor.supervisor == supervisorUser
                                                        select Emisor
                                             ).ToList();
                conexion.EmisorReceptorIMEC.ToList();
                this.ASPxGridViewEmisores.DataBind();
            }
        }

        protected void ASPxGridViewClientes_SelectionChanged(object sender, EventArgs e)
        {
            if ((sender as ASPxGridView).GetSelectedFieldValues("identificacion").Count > 0)
            {
                string identificacion = (sender as ASPxGridView).GetSelectedFieldValues("identificacion")[0].ToString();
                using (var conexion = new DataModelFE())
                { 
                    EmisorReceptorIMEC emisor = conexion.EmisorReceptorIMEC.Find(identificacion);
                    //Session["usuario"] = emisor.identificacion;
                    //Session["elUsuario"] = conexion.Usuario.Find(emisor.identificacion);
                    Session["emisor"] = emisor.identificacion;
                    Session["elEmisor"] = emisor;
                    Response.Redirect("~/");
                }
                 
            }
        }

        protected void ASPxGridViewEmisores_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    string supervisor = Session["usuario"].ToString();
                    string emisor = e.Values["identificacion"].ToString();

                    //busca objeto
                    object[] key = new object[] { supervisor, emisor };
                    var itemToRemove = conexion.Supervisor.Find(key);
                    conexion.Supervisor.Remove(itemToRemove);
                    conexion.SaveChanges();

                    //esto es para el manero del devexpress
                    e.Cancel = true;
                    this.ASPxGridViewEmisores.CancelEdit();
                }

            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(fullErrorMessage, ex.EntityValidationErrors);

            }
            catch (Exception ex)
            {
                throw new Exception(Utilidades.validarExepcionSQL(ex), ex.InnerException);
            }
            finally
            {
                Page_Load(sender, e);
            }
        }
    }
}