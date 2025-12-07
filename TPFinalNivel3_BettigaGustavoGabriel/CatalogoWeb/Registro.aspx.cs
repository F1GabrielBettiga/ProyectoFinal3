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
            if (validarCamposObligatorios())
            { 
                if (ExisteMail(txtEmail.Text))
                {
                    lblErrorRegistro.Text = "El correo electrónico ya está registrado.";
                    lblErrorRegistro.Visible = true;
                    return;
                }
                else 
                {
                    crearCuenta();
                }
                    
            }
            

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
                Session["UsuarioLogueado"] = usuario;
                Response.Redirect("MiPerfil.aspx");
            }
            else
            {
                lblErrorRegistro.Text = "No se pudo iniciar sesión luego de crear la cuenta.";
                lblErrorRegistro.Visible = true;
            }


        }

        private bool validarCamposObligatorios()
        {
            lblErrorRegistro.Visible = false;

            // --------------------
            // VALIDACIONES
            // --------------------

            // 1) Campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtRepetirPassword.Text))
            {
                lblErrorRegistro.Text = "Todos los campos son obligatorios.";
                lblErrorRegistro.Visible = true;
                return false;
            }

            // 2) Nombre: mínimo 3 letras, solo letras
            if (txtNombre.Text.Length < 3 ||
                !System.Text.RegularExpressions.Regex.IsMatch(txtNombre.Text, @"^[a-zA-ZÁÉÍÓÚáéíóúñÑ]+$"))
            {
                lblErrorRegistro.Text = "El nombre debe tener al menos 3 caracteres y solo letras.";
                lblErrorRegistro.Visible = true;
                return false;
            }

            // 3) Apellido: mínimo 3 letras, solo letras
            if (txtApellido.Text.Length < 3 ||
                !System.Text.RegularExpressions.Regex.IsMatch(txtApellido.Text, @"^[a-zA-ZÁÉÍÓÚáéíóúñÑ]+$"))
            {
                lblErrorRegistro.Text = "El apellido debe tener al menos 3 caracteres y solo letras.";
                lblErrorRegistro.Visible = true;
                return false;
            }

            // 4) Email en formato válido
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblErrorRegistro.Text = "Ingresá un correo electrónico válido.";
                lblErrorRegistro.Visible = true;
                return false;
            }

            // 5) Contraseña: mínimo 8 caracteres, 1 mayus, 1 minus, 1 número
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPassword.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
            {
                lblErrorRegistro.Text = "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número.";
                lblErrorRegistro.Visible = true;
                return false;
            }

            // 6) Las contraseñas deben coincidir
            if (txtPassword.Text != txtRepetirPassword.Text)
            {
                lblErrorRegistro.Text = "Las contraseñas no coinciden.";
                lblErrorRegistro.Visible = true;
                return false;
            }

            return true;
        }

        bool ExisteMail(string email)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            if (negocio.ExisteEmail(email))
            {
                
                return true;
            }

            return false;
        }


    }
    
}