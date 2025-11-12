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
    public partial class AdminMarcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrid();
            }

        }

        protected void dgvMarcas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMarcas.PageIndex = e.NewPageIndex;
            List<Marca> listaAMostrar;

            listaAMostrar = (List<Marca>)Session["listaMarcas"];

            dgvMarcas.DataSource = listaAMostrar;
            dgvMarcas.DataBind();

        }

        protected void dgvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtenemos el ID del artículo
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Redirigir a la pantalla de edición
                Response.Redirect("ABM_Marcas.aspx?id=" + id);
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
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                List<Marca> lista = negocio.listarMarcas();
                Session["listaMarcas"] = lista;

                if (lista == null || lista.Count == 0)
                {
                    // Si por alguna razón no trajo nada, vuelve a intentar
                    lista = negocio.listarMarcas();
                    Session["listaMarcas"] = lista;
                }

                dgvMarcas.DataSource = lista;
                dgvMarcas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}