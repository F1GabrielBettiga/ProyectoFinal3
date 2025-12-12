using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace CatalogoWeb
{
    public partial class AdminCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrid();
            }
        }

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABM_Categorias.aspx",false);
        }

        protected void dgvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCategorias.PageIndex = e.NewPageIndex;
            List<Categoria> listaAMostrar;

            listaAMostrar = (List<Categoria>)Session["listaCategorias"];

            dgvCategorias.DataSource = listaAMostrar;
            dgvCategorias.DataBind();

        }

        protected void dgvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            // Obtenemos el ID del artículo
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Redirigir a la pantalla de edición
                Response.Redirect("ABM_Categorias.aspx?id=" + id);
            }
            else if (e.CommandName == "Borrar")
            {
                eliminarCategoria(id);
                txtBuscarCategoria.Text = string.Empty;
                cargarGrid();
            }


        }



        private void cargarGrid()
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();

                List<Categoria> lista = negocio.listarCategorias() ?? new List<Categoria>();
                Session["listaCategorias"] = lista;

                bool hayRegistros = lista.Count > 0;

          
                lblSinArticulos.Visible = !hayRegistros;

          
                dgvCategorias.Visible = hayRegistros;
                dgvCategorias.DataSource = lista;
                dgvCategorias.DataBind();
            }
            catch (Exception ex)
            {
                RedirigirConError("No se pudieron cargar las categorías.", ex);
            }
        }

        private void eliminarCategoria(int id)
        {
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();
            ArticuloNegocio negocioArticulo = new ArticuloNegocio();
            try
            {
                negocioArticulo.ActualizarCategoriaEnArticulos(id);
                negocioCategoria.eliminarCategoria(id);
            }
            catch (Exception ex)
            {
                RedirigirConError("No se pudo eliminar la categoría.", ex);
            }
        }

        private void BuscarCategoria()
        {
            string texto = txtBuscarCategoria.Text.Trim();


            if (string.IsNullOrWhiteSpace(texto))
            {
                cargarGrid();
            }
            else
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                List<Categoria> listaCategoriasAdminFiltrada = negocio.BuscarCategoriasPorTexto(texto);
                dgvCategorias.DataSource = listaCategoriasAdminFiltrada;
                dgvCategorias.DataBind();
            }


        }

        protected void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            BuscarCategoria();
        }

        protected void btnLimpiarCategoria_Click(object sender, EventArgs e)
        {
            txtBuscarCategoria.Text = string.Empty;
            cargarGrid();
        }

        private void RedirigirConError(string mensajeUsuario, Exception ex = null)
        {
            Session["ErrorUsuario"] = mensajeUsuario;
            Session["ErrorTecnico"] = ex != null ? ex.ToString() : null;
            Response.Redirect("Error.aspx", false);
        }
    }
}