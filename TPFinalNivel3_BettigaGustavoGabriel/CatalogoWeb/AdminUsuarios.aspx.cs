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
                Response.Redirect("ABM_Usuarios.aspx",false);
            }
            else if (e.CommandName == "Borrar")
            {
                eliminarUsuario(id);
                txtBuscarUsuario.Text = string.Empty;
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

        private void cargarGrid()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                List<Usuario> lista = negocio.listarUsuarios() ?? new List<Usuario>();
                Session["listaUsuarios"] = lista;

                bool hayRegistros = lista.Count > 0;

                // Label “estado vacío” (unificado)
                lblSinArticulos.Visible = !hayRegistros;

                // Grid
                dgvUsuarios.Visible = hayRegistros;
                dgvUsuarios.DataSource = lista;
                dgvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                RedirigirConError("No se pudieron cargar los usuarios.", ex);
            }
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABM_Usuarios.aspx", false);

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
                RedirigirConError("No se pudo eliminar el usuario.", ex);
            }
        }

        private void BuscarUsuario()
        {
            string texto = txtBuscarUsuario.Text.Trim();


            if (string.IsNullOrWhiteSpace(texto))
            {
                cargarGrid();
            }
            else
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                List<Usuario> listaUsuariosAdminFiltrada = negocio.BuscarUsuariosPorTexto(texto);
                dgvUsuarios.DataSource = listaUsuariosAdminFiltrada;              
                dgvUsuarios.DataBind();
            }


        }

        protected void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            BuscarUsuario();
        }

        protected void btnLimpiarBusquedaUsuario_Click(object sender, EventArgs e)
        {
            txtBuscarUsuario.Text = string.Empty;
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