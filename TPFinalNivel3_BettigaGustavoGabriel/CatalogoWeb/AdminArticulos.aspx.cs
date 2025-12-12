using Dominio;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoWeb
{
    public partial class AdminArticulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                cargarGrid();
            }


        }

        protected void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABM_Articulos.aspx",false);
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            List<Articulo> listaAMostrar;

            listaAMostrar = (List<Articulo>)Session["listaArticulos"];

            dgvArticulos.DataSource = listaAMostrar;
            dgvArticulos.DataBind();

        }

        protected void dgvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtenemos el ID del artículo
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Redirigir a la pantalla de edición
                Response.Redirect("ABM_Articulos.aspx?id=" + id, false);
            }
            else if (e.CommandName == "Borrar")
            {
                
                eliminarArticulo(id);
                txtBuscarArticulos.Text = string.Empty;
                cargarGrid();
            }

        }
        private void eliminarArticulo(int id)
        {
            ArticuloNegocio negocioart = new ArticuloNegocio();
            FavoritoNegocio negociofav = new FavoritoNegocio();
            try
            {
                negociofav.EliminarFavoritoPorArticulo(id);
                negocioart.eliminarArticulo(id);
            }
            catch (Exception ex)
            {
                RedirigirConError("No se pudo eliminar el artículo.", ex);
            }
        }

        private void cargarGrid()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                List<Articulo> lista = negocio.listarArticulos() ?? new List<Articulo>();
                Session["listaArticulos"] = lista;

                bool hayArticulos = lista.Count > 0;

              
                lblSinArticulos.Visible = !hayArticulos;

               
                dgvArticulos.Visible = hayArticulos;

                dgvArticulos.DataSource = lista;
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                RedirigirConError("No se pudieron cargar los artículos.", ex);
            }
        }

        private void BuscarArticulo()
        {
            string texto = txtBuscarArticulos.Text.Trim();
            

            if (string.IsNullOrWhiteSpace(texto))
            {
                cargarGrid();
            }
            else
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> listaArticulosAdminFiltrada = negocio.BuscarArticulosPorTexto(texto);
                dgvArticulos.DataSource = listaArticulosAdminFiltrada;
                dgvArticulos.DataBind();
            }

           
        }

        protected void btnBuscarArticulos_Click(object sender, EventArgs e)
        {
            BuscarArticulo();
        }

        protected void btnLimpiarBusquedaArticulos_Click(object sender, EventArgs e)
        {
            txtBuscarArticulos.Text = string.Empty;
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