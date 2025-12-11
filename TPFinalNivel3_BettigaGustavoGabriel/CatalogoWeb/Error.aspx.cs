using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoWeb
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarMensajesDeError();
        }

        private void CargarMensajesDeError()
        {
            // Mensaje amigable para el usuario
            if (Session["ErrorUsuario"] != null)
            {
                lblMensajeError.Text = Session["ErrorUsuario"].ToString();
            }
            else
            {
                lblMensajeError.Text = "Ocurrió un error inesperado.";
            }

            // Mensaje técnico para soporte
            if (Session["ErrorTecnico"] != null)
            {
                lblDetalleError.Text = Session["ErrorTecnico"].ToString();
            }
            else
            {
                lblDetalleError.Text = "No se ha recibido información técnica del error.";
            }

            // Limpieza de sesión
            Session.Remove("ErrorUsuario");
            Session.Remove("ErrorTecnico");
        }
    }
}