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
            // Intento obtener el usuario desde la sesión
            Usuario usuarioLogueado = Session["UsuarioLogueado"] as Usuario;

            // Si NO hay usuario logueado, pongo la imagen por defecto y salgo
            if (usuarioLogueado == null)
            {
                imgBtnUsuario.ImageUrl = fallback;
                return;
            }

            // Si hay usuario y tiene imagen de perfil, la uso
            if (!string.IsNullOrWhiteSpace(usuarioLogueado.urlImagenPerfil))
            {
                imgBtnUsuario.ImageUrl = usuarioLogueado.urlImagenPerfil + "?v=" + DateTime.Now.Ticks;
                // el ?v=... fuerza al navegador a recargar la imagen
            }
            else
            {
                // Usuario logueado pero sin imagen → uso una por defecto
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
            // Tomo el texto del buscador
            string texto = txtBuscarGlobal.Text.Trim();

            // Si está vacío → limpio la lista filtrada
            if (string.IsNullOrWhiteSpace(texto))
            {
                Session["listaArticulosFiltrada"] = null;
            }
            else
            {
                // Busco en la BD
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> resultado = negocio.BuscarArticulosPorTexto(texto);

                // Guardo la lista en sesión 
                Session["listaArticulosFiltrada"] = resultado;
            }

            // Voy al Home (Default) para que pinte el grid con lo que haya en sesión
            Response.Redirect("~/Default.aspx");
        }


    }
}