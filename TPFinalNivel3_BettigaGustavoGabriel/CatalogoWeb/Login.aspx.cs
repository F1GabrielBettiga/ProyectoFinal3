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
            validarUsuario();
        }

        protected void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");

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

        private void validarUsuario()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();

            bool usuarioValido = negocio.obtenerUsuarioPorEmail(txtEmail.Text, txtPassword.Text, out usuario);

            if (usuarioValido)
            {   
                Session["Usuario"] = usuario.id;
                Response.Redirect("MiPerfil.aspx");
            }
            else
            {
                lblError.Text = "Email o contraseña incorrectos.";
                lblError.Visible = true;
            }




        }



    }
}