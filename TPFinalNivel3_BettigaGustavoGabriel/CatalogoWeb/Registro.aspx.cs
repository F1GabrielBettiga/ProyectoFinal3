using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoWeb
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            crearCuenta();

        }

        private void crearCuenta()
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                if (txtPassword.Text != txtRepetirPassword.Text)
                {
                    lblErrorRegistro.Text = "Las contraseñas no coinciden.";
                    lblErrorRegistro.Visible = true;

                }
                else
                {
                    usuario.nombre = txtNombre.Text;
                    usuario.apellido = txtApellido.Text;
                    usuario.email = txtEmail.Text;
                    usuario.password = txtPassword.Text;
                    usuario.esAdmin = false;

                    if (!negocio.insertarUsuario(usuario))
                    {
                        lblErrorRegistro.Text = "No se pudo crear la cuenta";
                        lblErrorRegistro.Visible = true;
                        return;
                    }

                    // Si llegamos acá, se insertó OK → intento loguear
                    validarUsuario(usuario.email, usuario.password);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private void validarUsuario(string mail, string password)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();

            bool usuarioValido = negocio.obtenerUsuarioPorEmail(mail, password, out usuario);

            if (usuarioValido)
            {
                Session["Usuario"] = usuario.id;
                Response.Redirect("MiPerfil.aspx");
            }
            else
            {
                lblErrorRegistro.Text = "No se pudo iniciar sesión luego de crear la cuenta.";
                lblErrorRegistro.Visible = true;
            }


        }


    }
}