using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace CatalogoWeb
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    int id = (int)Session["Usuario"];

                    CargarDetalles(id);

                }
            }



        }


        private void CargarDetalles(int id)
        {
            try
            {

                UsuarioNegocio negocio = new UsuarioNegocio();


                Usuario usuario = negocio.listarUsuarios().FirstOrDefault(a => a.id == id);


                if (usuario == null)
                    return;


               
                txtNombre.Text = usuario.nombre;
                txtApellido.Text = usuario.apellido;
                txtEmail.Text = usuario.email;
                txtPassword.Attributes["value"] = usuario.password;
                lblTipoUsuario.Text = usuario.esAdmin ? "Administrador" : "Estándar";          
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
                imgPerfil.ImageUrl = fallback;
                return;
            }

            // 2) Si el campo urlImagenPerfil está vacío o nulo
            if (string.IsNullOrEmpty(usuario.urlImagenPerfil))
            {
                imgPerfil.ImageUrl = fallback;
            }
            else
            {
                // 3) Si tiene algo, usamos lo que vino
                imgPerfil.ImageUrl = usuario.urlImagenPerfil;
            }


        }

        protected void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            if (txtNuevaPassword.Visible == true)
            {
                lblNuevaPass.Visible = false;
                txtNuevaPassword.Visible = false;

                lblRepetirPass.Visible = false;
                txtRepetirPassword.Visible = false;

            }
            else
            {

                lblNuevaPass.Visible = true;
                txtNuevaPassword.Visible = true;

                lblRepetirPass.Visible = true;
                txtRepetirPassword.Visible = true;
            }
        }

        protected void chkMostrarPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarPassword.Checked)
            {
                txtPassword.TextMode = TextBoxMode.SingleLine;
            }
            else
            {
                txtPassword.TextMode = TextBoxMode.Password;
            }
        }
    }
}