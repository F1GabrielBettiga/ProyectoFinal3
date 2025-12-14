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
    public partial class DetalleProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    CargarDetalles(int.Parse(id));
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }



        }




        private void CargarImagen(Articulo articulo)
        {
            // Imagen de respaldo
            string fallback = ResolveUrl("~/Images/no-image.png");

            // Si el campo está vacío o tiene texto inválido
            if (string.IsNullOrEmpty(articulo.imagenUrl) ||
                articulo.imagenUrl.Length < 5 ||
                articulo.imagenUrl.IndexOf("sin_imagen_para_que_de_error", StringComparison.OrdinalIgnoreCase) >= 0 ||
                articulo.imagenUrl.IndexOf("noimage", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                imgProducto.ImageUrl = fallback;
                return;
            }

            // Normalizo la url que viene de la BD
            string imagenUrl = articulo.imagenUrl.Trim();

            // Si es una URL completa (http o https)
            if (Uri.IsWellFormedUriString(imagenUrl, UriKind.Absolute))
            {
                imgProducto.ImageUrl = imagenUrl;
            }
            else
            {
                //  Si el campo urlImagenPerfil está vacío o nulo
                if (string.IsNullOrEmpty(imagenUrl))
                {
                    imgProducto.ImageUrl = fallback;
                }
                else
                {
                    //  Si tiene algo, usamos lo que vino
                    imgProducto.ImageUrl = imagenUrl;
                }
            }

            // Si la imagen falla al cargar en el navegador, usar la de respaldo
            imgProducto.Attributes["onerror"] =
                $"this.onerror=null; this.src='{fallback}';";
        }

        private void CargarDetalles(int id)
        {
            try
            {
                
                ArticuloNegocio negocio = new ArticuloNegocio();

                // Buscamos el artículo con el ID recibido
                Articulo articulo = negocio.listarArticulos()
                                           .FirstOrDefault(a => a.id == id);

                // Si no se encuentra, salimos
                if (articulo == null)
                    return;

                
                lblNombre.Text = articulo.nombre;
                lblCodigo.Text = articulo.codigo;
                lblMarca.Text = (articulo.marca == null || string.IsNullOrWhiteSpace(articulo.marca.descripcion)) ? "Sin marca": articulo.marca.descripcion;
                lblCategoria.Text = (articulo.categoria == null || string.IsNullOrWhiteSpace(articulo.categoria.descripcion)) ? "Sin categoría": articulo.categoria.descripcion;
                lblPrecio.Text = "$" + articulo.precio.ToString("N2");
                lblDescripcion.Text = articulo.descripcion;

                
                CargarImagen(articulo);

                chequearFavorito(articulo.id);


            }
            catch (Exception ex)
            {
               
                RedirigirConError("Ocurrió un error al cargar los detalles del producto.", ex);
            }
        }

        private void chequearFavorito(int artId)
        {
            if (Session["UsuarioLogueado"] != null)
            {
                Usuario user = (Usuario)Session["UsuarioLogueado"];
                FavoritoNegocio favNegocio = new FavoritoNegocio();

                bool esFavorito = favNegocio.EsFavorito(user.id, artId);

                btnFavorito.ImageUrl = esFavorito
                    ? "~/Images/fav-full.png"   // ❤️ está en favoritos
                    : "~/Images/fav-empty.png"; // 🤍 no está en favoritos
            }
            else
            {
                // Si no está logueado, siempre gris
                btnFavorito.ImageUrl = "~/Images/fav-empty.png";
            }

        }

        protected void btnAgregarFavorito_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }
            else
            {
                agregarQuitarFavorito();

            }
        }

        private void agregarQuitarFavorito()
        {
            Usuario userLogueado = (Usuario)Session["UsuarioLogueado"];
            string idArticulo = Request.QueryString["id"];
            FavoritoNegocio favNegocio = new FavoritoNegocio();

            if (favNegocio.EsFavorito(userLogueado.id, int.Parse(idArticulo)))
            {
                favNegocio.EliminarFavorito(userLogueado.id, int.Parse(idArticulo));
                chequearFavorito(int.Parse(idArticulo));
            }
            else
            {

                favNegocio.InsertarFavorito(userLogueado.id, int.Parse(idArticulo));
                chequearFavorito(int.Parse(idArticulo));

            }
           
        }
        private void RedirigirConError(string mensajeUsuario, Exception ex = null)
        {
            Session["ErrorUsuario"] = mensajeUsuario;
            Session["ErrorTecnico"] = ex != null ? ex.ToString() : null;
            Response.Redirect("Error.aspx", false);
        }
    }
}