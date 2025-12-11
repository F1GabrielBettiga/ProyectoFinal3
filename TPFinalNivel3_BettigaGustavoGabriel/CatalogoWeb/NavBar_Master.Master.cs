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
            validarSesionActiva();

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

        private void validarSesionActiva()
        {
            //Con este if hacemos que pueda ingresar a cualquiera de esas 4 paginas sin logearse, pero no  a las demas 
            if (!(Page is Login || Page is Default || Page is Registro ||  Page is DetalleProducto || Page is Error))
            {
                //entra si es admin o si no es admin
                if (!Seguridad.ValidarSesionActiva(Session["UsuarioLogueado"]))
                {
                    Response.Redirect("Login.aspx");
                    
                }

                // si no es admin y esta logeado solo puede entrar a InformacionUsuario y  las demas paginas que no necesita logearse
                if (!(Page is MiPerfil || Page is MisFavoritos))
                {

                    if (!Seguridad.esAdmin(Session["UsuarioLogueado"]))
                    {
                        Response.Redirect("Login.aspx");

                    }

                }


            }

        }


    }
}