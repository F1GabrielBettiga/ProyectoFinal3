using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoWeb
{
    public partial class NavBar_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            CargarCampos();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear(); //BORRAMOS TODO PORQUE NO QUEREMOS NADA QUE CORRESPONDA A ESE USUARIO
            Response.Redirect("Login.aspx");
        }

        private void CargarCampos()
        {
            // Imagen de respaldo (por defecto)
            string fallback = ResolveUrl("~/Images/no-user.jpg");

            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            if (usuarioLogueado == null)
            {
                imgBtnUsuario.ImageUrl = fallback;
                return;
            }

            if (!string.IsNullOrWhiteSpace(usuarioLogueado.urlImagenPerfil))
            {
                imgBtnUsuario.ImageUrl = usuarioLogueado.urlImagenPerfil + "?v=" + DateTime.Now.Ticks;
            }
            else
            {
                imgBtnUsuario.ImageUrl = fallback;
            }
        }

        protected void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscarGlobal_Click(object sender, EventArgs e)
        {
            BuscarDesdeNav();
        }

        private void BuscarDesdeNav()
        {
            string texto = txtBuscarGlobal.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                Session["listaArticulosFiltrada"] = null;
            }
            else
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> resultado = negocio.BuscarArticulosPorTexto(texto);

                Session["listaArticulosFiltrada"] = resultado;
            }

            Response.Redirect("~/Default.aspx");
        }
    }
}