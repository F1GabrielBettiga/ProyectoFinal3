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
    public partial class AdminUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrid();
            }
        }

        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtenemos el ID del artículo
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Redirigir a la pantalla de edición

                Session["UsuarioEditar"] = id;
                Response.Redirect("ABM_Usuarios.aspx");
            }
            else if (e.CommandName == "Borrar")
            {
                eliminarUsuario(id);

                // Recargamos la grilla
                cargarGrid();
            }

        }

        protected void dgvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuarios.PageIndex = e.NewPageIndex;
            List<Usuario> listaAMostrar;

            listaAMostrar = (List<Usuario>)Session["listaUsuarios"];

            dgvUsuarios.DataSource = listaAMostrar;
            dgvUsuarios.DataBind();

        }

        void cargarGrid()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                List<Usuario> lista = negocio.listarUsuarios();
                Session["listaUsuarios"] = lista;

                if (lista == null || lista.Count == 0)
                {
                    // Si por alguna razón no trajo nada, vuelve a intentar
                    lista = negocio.listarUsuarios();
                    Session["listaUsuarios"] = lista;
                }

                dgvUsuarios.DataSource = lista;
                dgvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABM_Usuarios.aspx");

        }

        private void eliminarUsuario(int id)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                negocio.eliminarUsuario(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}