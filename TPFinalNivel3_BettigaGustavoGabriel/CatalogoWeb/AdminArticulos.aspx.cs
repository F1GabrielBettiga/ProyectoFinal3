using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

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
                Response.Redirect("ABM_Articulos.aspx?id=" + id);
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
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                List<Articulo> lista = negocio.listarArticulos();
                Session["listaArticulos"] = lista;

                if (lista == null || lista.Count == 0)
                {
                    // Si por alguna razón no trajo nada, vuelve a intentar
                    lista = negocio.listarArticulos();
                    Session["listaArticulos"] = lista;
                }

                dgvArticulos.DataSource = lista;
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        
    }
}