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
    public partial class ABM_Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                if (Session["UsuarioEditar"] != null)
                {
                    int id = (int)Session["UsuarioEditar"];

                    CargarDetalles(id);
                    
                }
                
            }


        }

        protected void chkMostrarPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarPass.Checked)
            {
                txtPassword.TextMode = TextBoxMode.SingleLine;
            }
            else
            {
                txtPassword.TextMode = TextBoxMode.Password;
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminUsuarios.aspx");

        }


        private void CargarDetalles(int id)
        {
            try
            {

                UsuarioNegocio negocio = new UsuarioNegocio();


                Usuario usuario= negocio.listarUsuarios().FirstOrDefault(a => a.id == id);


                if (usuario == null)
                    return;


                txtIdUsuario.Text = usuario.id.ToString();
                txtNombre.Text = usuario.nombre;
                txtApellido.Text = usuario.apellido;
                txtEmail.Text = usuario.email;
                txtPassword.Attributes["value"] = usuario.password;
                ddlRol.SelectedValue = usuario.esAdmin.ToString().ToLower();
                CargarImagen(usuario);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CargarImagen(Usuario usuario)
        {
            // Imagen de respaldo (por defecto)
            string fallback = ResolveUrl("~/Images/no-user.jpg");

            // 1) Si el usuario vino nulo (no hay nada en Session, por ejemplo)
            if (usuario == null)
            {
                imgUsuario.ImageUrl = fallback;
                return;
            }

            // 2) Si el campo urlImagenPerfil está vacío o nulo
            if (string.IsNullOrEmpty(usuario.urlImagenPerfil))
            {
                imgUsuario.ImageUrl = fallback;
            }
            else
            {
                // 3) Si tiene algo, usamos lo que vino
                imgUsuario.ImageUrl = usuario.urlImagenPerfil;
            }


    }   }
}