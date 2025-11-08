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
                // Si es una ruta interna del proyecto (Images/loquesea.png, etc.)
                imgProducto.ImageUrl = ResolveUrl("~/") + imagenUrl.TrimStart('/');
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
                lblMarca.Text = articulo.marca != null ? articulo.marca.descripcion : "Sin marca";
                lblCategoria.Text = articulo.categoria != null ? articulo.categoria.descripcion : "Sin categoría";
                lblPrecio.Text = "$" + articulo.precio.ToString("N2");
                lblDescripcion.Text = articulo.descripcion;

                
                CargarImagen(articulo);
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }


    }
}