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
    public partial class AdminCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrid();
            }
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
                // Confirmar o eliminar el artículo (ejemplo simple)
                ArticuloNegocio negocio = new ArticuloNegocio();
                // negocio.EliminarArticulo(id);

                // Recargamos la grilla
                cargarGrid();
            }


        }



        void cargarGrid()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                List<Categoria> lista = negocio.listarCategorias();
                Session["listaCategorias"] = lista;

                if (lista == null || lista.Count == 0)
                {
                    // Si por alguna razón no trajo nada, vuelve a intentar
                    lista = negocio.listarCategorias();
                    Session["listaCategorias"] = lista;
                }

                dgvCategorias.DataSource = lista;
                dgvCategorias.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
}