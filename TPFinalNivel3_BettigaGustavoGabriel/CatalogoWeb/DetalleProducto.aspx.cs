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
            }



        }




        private void CargarImagen(Articulo articulo)
        {
            string fallback = ResolveUrl("~/Images/no-image.png");

            //Si no hay imagen o vino algo inválido
            if (string.IsNullOrWhiteSpace(articulo.imagenUrl))
            {
                imgProducto.ImageUrl = fallback;
                return;
            }

            //Normalizo removiendo el ?v=123 si existiera
            string imagenUrl = articulo.imagenUrl.Split('?')[0].Trim();

            //Si es URL completa (http/https)
            if (Uri.IsWellFormedUriString(imagenUrl, UriKind.Absolute))
            {
                imgProducto.ImageUrl = imagenUrl;
            }
            else
            {
                //ruta interna del proyecto
                imgProducto.ImageUrl = ResolveUrl(imagenUrl);
            }

            //Si falla al cargar en el navegador → fallback automático
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
                lblMarca.Text = (articulo.marca != null && articulo.marca.id != 0)? articulo.marca.descripcion: "Sin marca";
                lblCategoria.Text = (articulo.categoria != null && articulo.categoria.id != 0)? articulo.categoria.descripcion: "Sin categoría";
                lblPrecio.Text = "$" + articulo.precio.ToString("N2");
                lblDescripcion.Text = articulo.descripcion;

                
                CargarImagen(articulo);

                chequearFavorito(articulo.id);


            }
            catch (Exception ex)
            {
               
                throw ex;
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

            }
            else
            {

                favNegocio.InsertarFavorito(userLogueado.id, int.Parse(idArticulo));

            }
            Response.Redirect("DetalleProducto.aspx?id=" + idArticulo, false);
        }
    }
}