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
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            { 
                actualizarArticulo();
            }
            else
            {
                // Código para agregar un nuevo artículo (no implementado en este snippet)
            }


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

                lblTituloABM.Text = "Modificar Artículo";
                btnGuardar.Text = "Modificar";



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
                //  Si el campo urlImagenPerfil está vacío o nulo
                if (string.IsNullOrEmpty(imagenUrl))
                {
                    imgArticulo.ImageUrl = fallback;
                }
                else
                {
                    //  Si tiene algo, usamos lo que vino
                    imgArticulo.ImageUrl = imagenUrl;
                }
            }

            // Si la imagen falla al cargar en el navegador, usar la de respaldo
            imgArticulo.Attributes["onerror"] =
                $"this.onerror=null; this.src='{fallback}';";
        }

        private void guardarImagenDePerfil(Articulo art)
        {
            try
            {
                // ¿HAY ARCHIVO NUEVO?
                if (txtImagen.PostedFile != null && txtImagen.PostedFile.ContentLength > 0 && !string.IsNullOrEmpty(txtImagen.PostedFile.FileName))
                {
                    string rutaFisica = Server.MapPath("~/Images/"); // carpeta interna
                    string nombreArchivo = "art-" + art.id + ".jpg";

                    // Guarda físicamente en la carpeta del proyecto
                    txtImagen.PostedFile.SaveAs(rutaFisica + nombreArchivo);

                    // Guarda la ruta virtual (accesible desde la web y portable entre equipos)
                    art.imagenUrl = "~/Images/" + nombreArchivo;

                    // Actualiza el <asp:Image> para la vista previa
                    imgArticulo.ImageUrl = art.imagenUrl + "?v=" + DateTime.Now.Ticks;
                }
                else
                {
                    // NO SE SUBIÓ NADA NUEVO → CONSERVAR LA QUE YA TENÍA

                    // Si en la página hay alguna imagen cargada, la reutilizo
                    if (!string.IsNullOrWhiteSpace(imgArticulo.ImageUrl))
                    {
                        // Por si tiene el "?v=12345" de cache-busting, me quedo solo con la ruta
                        string urlActual = imgArticulo.ImageUrl.Split('?')[0];

                        // Si no es la imagen por defecto, la conservo
                        if (!urlActual.EndsWith("no-image.png", StringComparison.OrdinalIgnoreCase))
                        {
                            art.imagenUrl = urlActual;
                        }
                        else
                        {
                            // Imagen por defecto → guardo null en la entidad
                            art.imagenUrl = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

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

        void actualizarArticulo()
        {
            Articulo articulo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                articulo.id = int.Parse(txtId.Text);

                // --- Código ---
                articulo.codigo = string.IsNullOrWhiteSpace(txtCodigo.Text)
                    ? null
                    : txtCodigo.Text.Trim();

                // --- Nombre ---
                articulo.nombre = string.IsNullOrWhiteSpace(txtNombre.Text)
                    ? null
                    : txtNombre.Text.Trim();

                // --- Descripción ---
                articulo.descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text)
                    ? null
                    : txtDescripcion.Text.Trim();

                // --- Precio ---
                if (string.IsNullOrWhiteSpace(txtPrecio.Text))
                    articulo.precio = 0; 
                else
                    articulo.precio = decimal.Parse(txtPrecio.Text);

                // --- Marca ---
                articulo.marca = new Marca();
                articulo.marca.id = int.Parse(ddlMarca.SelectedValue);

                // --- Categoría ---
                articulo.categoria = new Categoria();
                articulo.categoria.id = int.Parse(ddlCategoria.SelectedValue);

                // --- Imagen (por ahora ignorada) ---

                guardarImagenDePerfil(articulo);

                

                // Ejecutar actualización
                bool exito = negocio.actualizarArticulo(articulo);

                if (exito)
                    Response.Redirect("AdminArticulos.aspx");
                else
                {
                    //lblMensajeError.Text = "Error al actualizar el artículo.";
                    //lblMensajeError.Visible = true;
                }
            }
            catch (FormatException)
            {
                //lblMensajeError.Text = "El precio debe ser un número válido.";
                //lblMensajeError.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}