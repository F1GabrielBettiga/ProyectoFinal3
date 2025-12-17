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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool emailVacio = string.IsNullOrWhiteSpace(txtEmail.Text);
            bool passVacia = string.IsNullOrWhiteSpace(txtPassword.Text);

            if (!emailVacio && !passVacia)
            {
                // Si ambos tienen algo → seguimos con la validación de usuario
                validarUsuario();
            }
            else
            {
                if (emailVacio && passVacia)
                    lblError.Text = "Los campos Email y Contraseña son obligatorios.";
                else if (emailVacio)
                    lblError.Text = "El campo Email es obligatorio.";
                else
                    lblError.Text = "El campo Contraseña es obligatorio.";

                lblError.Visible = true;
            }
        }
        protected void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx",false);

        }
        private void validarUsuario()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();

            bool usuarioValido = negocio.obtenerUsuarioPorEmail(txtEmail.Text, txtPassword.Text, out usuario);

            lblError.Text = "";
            lblError.Visible = false;

            if (usuarioValido)
            {   
                Session["UsuarioLogueado"] = usuario;
                Response.Redirect("MiPerfil.aspx",false);
            }
            else
            {
                lblError.Text = "Email o contraseña incorrectos.";
                lblError.Visible = true;
            }




        }
        private void RedirigirConError(string mensajeUsuario, Exception ex = null)
        {
            Session["ErrorUsuario"] = mensajeUsuario;
            Session["ErrorTecnico"] = ex != null ? ex.ToString() : null;
            Response.Redirect("Error.aspx", false);
        }



    }
}