using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            
            string codigoIngresado = (txtCodigo.Text ?? string.Empty).Trim().Replace(" ", "").ToUpper();

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                
                if (validarCamposObligatorios())
                {
                    string id = Request.QueryString["id"];
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo articulo = negocio.listarArticulos().FirstOrDefault(a => a.id == int.Parse(id));

                    if (articulo != null)
                    {
                        
                        string codigoActualArticulo = (articulo.codigo ?? string.Empty).Trim().Replace(" ", "").ToUpper();

                        
                        if (codigoActualArticulo == codigoIngresado)
                        {
                            actualizarArticulo();
                        }
                        
                        else if (ExisteCodigo(codigoIngresado))
                        {
                            lblError.Text = "El código ya está registrado.";
                            lblError.Visible = true;
                            return;
                        }
                        else
                        {
                            actualizarArticulo();
                        }
                    }
                }
            }
            else
            {
                
                if (validarCamposObligatorios())
                {
                    if (ExisteCodigo(codigoIngresado))
                    {
                        lblError.Text = "El código ya está registrado.";
                        lblError.Visible = true;
                        return;
                    }
                    else
                    {
                        agregarArticulo();
                    }
                }
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
                ViewState["ImagenOriginal"] = articulo.imagenUrl;
                pnlIdArticulo.Visible = true;

            }
            catch (Exception ex)
            {

                RedirigirConError("Error al cargar los detalles del artículo.", ex);
            }
        }

        private void agregarArticulo()
        {
            Articulo articulo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
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

                guardarImagenDeArticulo(articulo);



                // Ejecuta
                bool exito = negocio.agregarArticulo(articulo);

                if (exito)
                    Response.Redirect("AdminArticulos.aspx",false);
                else
                {
                    lblError.Text = "Error al actualizar el artículo.";
                    lblError.Visible = true;
                }
            }
            catch (FormatException)
            {
                
            }
            catch (Exception ex)
            {
                RedirigirConError("Error al agregar el artículo.", ex);
            }
        }









        private void CargarImagen(Articulo articulo)
        {
            // Imagen de respaldo
            string fallback = ResolveUrl("/Images/no-image.png");

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

        private void guardarImagenDeArticulo(Articulo art)
        {
            try
            {
                // 1) ¿HAY ARCHIVO NUEVO?
                if (txtImagen.PostedFile != null &&
                    txtImagen.PostedFile.ContentLength > 0 &&
                    !string.IsNullOrEmpty(txtImagen.PostedFile.FileName))
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();

                    // 👉 EXISTE → USAR ID
                    // 👉 NUEVO → USAR PRÓXIMO ID
                    int idParaNombre = art.id > 0
                        ? art.id
                        : negocio.obtenerProximoId();

                    // ✔ SOLO ACÁ SE USA ~
                    string rutaFisica = Server.MapPath("~/Images/");
                    string nombreArchivo = $"art-{idParaNombre}.jpg";

                    txtImagen.PostedFile.SaveAs(rutaFisica + nombreArchivo);

                    // ❌ SIN ~ EN LA URL
                    art.imagenUrl = $"/Images/{nombreArchivo}";
                    imgArticulo.ImageUrl = art.imagenUrl + "?v=" + DateTime.Now.Ticks;

                    return;
                }

                // 2) NO SE SUBIÓ IMAGEN → MANTENER LA ACTUAL
                string urlActual = imgArticulo.ImageUrl?.Split('?')[0];

                if (string.IsNullOrWhiteSpace(urlActual) ||
                    urlActual.EndsWith("no-image.png", StringComparison.OrdinalIgnoreCase))
                {
                    art.imagenUrl = ViewState["ImagenOriginal"] as string;
                }
                else
                {
                    art.imagenUrl = urlActual;
                }
            }
            catch (Exception ex)
            {
                RedirigirConError("Error al guardar la imagen del artículo.", ex);
            }
        }


        void CargarDdlMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> lista = negocio.listarMarcas();

           
            lista.Insert(0, new Marca{ id = 0, descripcion = "Sin Marca"});

            ddlMarca.DataSource = lista;
            ddlMarca.DataTextField = "descripcion";
            ddlMarca.DataValueField = "id";
            ddlMarca.DataBind();
        }

        void CargarDdlCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> lista = negocio.listarCategorias();

            
            lista.Insert(0, new Categoria { id = 0, descripcion = "Sin Categoría"});

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
                int idMarca = int.Parse(ddlMarca.SelectedValue);
                articulo.marca.id = idMarca;   // 0 = Sin Marca

                // --- Categoría ---
                articulo.categoria = new Categoria();
                int idCategoria = int.Parse(ddlCategoria.SelectedValue);
                articulo.categoria.id = idCategoria; // 0 = Sin Categoría

                // --- Imagen ---
                guardarImagenDeArticulo(articulo);

                // Ejecutar actualización en BD
                bool exito = negocio.actualizarArticulo(articulo);

                if (exito)
                    Response.Redirect("AdminArticulos.aspx", false);
                    
                else
                {
                    lblError.Text = "Error al actualizar el artículo.";
                    lblError.Visible = true;
                }
            }
            catch (FormatException)
            {
                lblError.Text = "El precio debe ser un número válido.";
                lblError.Visible = true;
            }
            catch (Exception ex)
            {
                RedirigirConError("Error al actualizar el artículo.", ex);
            }
        }

        private bool validarCamposObligatorios()
        {
            lblError.Visible = false;

            
            // 1) CÓDIGO
            
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                lblError.Text = "El campo Código es obligatorio.";
                lblError.Visible = true;
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtCodigo.Text, @"^[a-zA-Z0-9]+$"))
            {
                lblError.Text = "El código solo puede contener letras y números.";
                lblError.Visible = true;
                return false;
            }

          
            // 2) NOMBRE 
            
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblError.Text = "El campo Nombre es obligatorio.";
                lblError.Visible = true;
                return false;
            }

            

          
            // 3) PRECIO 
          
            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                lblError.Text = "El campo Precio es obligatorio.";
                lblError.Visible = true;
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPrecio.Text, @"^[0-9]+([.,][0-9]+)?$"))
            {
                lblError.Text = "El precio debe ser un número válido.";
                lblError.Visible = true;
                return false;
            }

            return true; 
        }

        bool ExisteCodigo(string codigo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            if (negocio.ExisteArticulo(codigo))
            {

                return true;
            }

            return false;
        }

        private void RedirigirConError(string mensajeUsuario, Exception ex = null)
        {
            Session["ErrorUsuario"] = mensajeUsuario;
            Session["ErrorTecnico"] = ex != null ? ex.ToString() : null;
            Response.Redirect("Error.aspx", false);
        }



    }
}