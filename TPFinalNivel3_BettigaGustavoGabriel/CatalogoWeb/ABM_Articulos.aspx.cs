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
    public partial class ABM_Articulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDdlCategorias();
                CargarDdlMarcas();

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                        string id = Request.QueryString["id"];
                        CargarDetalles(int.Parse(id));
                }
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminArticulos.aspx");

        }

        private void CargarDetalles(int id)
        {
            try
            {

                ArticuloNegocio negocio = new ArticuloNegocio();

                
                Articulo articulo = negocio.listarArticulos().FirstOrDefault(a => a.id == id);

                
                if (articulo == null)
                    return;

                
                txtId.Text = articulo.id.ToString();
                txtCodigo.Text = articulo.codigo;
                txtNombre.Text = articulo.nombre;
                txtDescripcion.Text = articulo.descripcion;
                txtPrecio.Text = articulo.precio.ToString("0.00");

                // Marca
                if (articulo.marca != null)
                {   
                    ddlMarca.SelectedValue = articulo.marca.id.ToString();
                }

                // Categoría
                if (articulo.categoria != null)
                {
                    ddlCategoria.SelectedValue = articulo.categoria.id.ToString();
                }

                CargarImagen(articulo);
                
            }
            catch (Exception ex)
            {

                throw ex;
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
                imgArticulo.ImageUrl = fallback;
                return;
            }

            // Normalizo la url que viene de la BD
            string imagenUrl = articulo.imagenUrl.Trim();

            // Si es una URL completa (http o https)
            if (Uri.IsWellFormedUriString(imagenUrl, UriKind.Absolute))
            {
                imgArticulo.ImageUrl = imagenUrl;
            }
            else
            {
                // Si es una ruta interna del proyecto (Images/loquesea.png, etc.)
                imgArticulo.ImageUrl = ResolveUrl("~/") + imagenUrl.TrimStart('/');
            }

            // Si la imagen falla al cargar en el navegador, usar la de respaldo
            imgArticulo.Attributes["onerror"] =
                $"this.onerror=null; this.src='{fallback}';";
        }

        void CargarDdlMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> lista = negocio.listarMarcas();

            // Enlazo la lista directamente al DropDownList
            ddlMarca.DataSource = lista;
            ddlMarca.DataTextField = "descripcion";   // lo que ve el usuario
            ddlMarca.DataValueField = "id";          // el valor que guardás (FK)
            ddlMarca.DataBind();


        }

        void CargarDdlCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> lista = negocio.listarCategorias();

            
            ddlCategoria.DataSource = lista;
            ddlCategoria.DataTextField = "descripcion";   
            ddlCategoria.DataValueField = "id";          
            ddlCategoria.DataBind();


        }





    }
}