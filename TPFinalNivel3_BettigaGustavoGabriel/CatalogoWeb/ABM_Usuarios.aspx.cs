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
                    lblTituloABMUsuario.Text = "Modificar Usuario";
                    int id = (int)Session["UsuarioEditar"];

                    CargarDetalles(id);

                }

            }


        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["UsuarioEditar"] != null)
            {
                if (validacionesCamposObligatorios())
                {
                    int id = (int)Session["UsuarioEditar"];
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    Usuario usuario = negocio.listarUsuarios().FirstOrDefault(a => a.id == id);

                   
                    string emailGuardado = (usuario.email ?? string.Empty).Trim().Replace(" ", "").ToLower();

                    string emailPantalla = (txtEmail.Text ?? string.Empty).Trim().Replace(" ", "").ToLower();

                    
                    if (emailGuardado == emailPantalla)
                    {
                        actualizarUsuario();
                    }
                    
                    else if (ExisteMail(txtEmail.Text))
                    {
                        lblError.Text = "El correo electrónico ya está registrado.";
                        lblError.Visible = true;
                        return;
                    }
                    else
                    {
                        actualizarUsuario();
                    }
                }
            }
            else
            {
                if (validacionesCamposObligatorios())
                {
                    
                    if (ExisteMail(txtEmail.Text))
                    {
                        lblError.Text = "El correo electrónico ya está registrado.";
                        lblError.Visible = true;
                        return;
                    }
                    else
                    {
                        agregarUsuario();
                    }
                }
            }



        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminUsuarios.aspx");

        }

        void actualizarUsuario()
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                usuario.id = int.Parse(txtIdUsuario.Text);

                // --- Email ---
                usuario.email = string.IsNullOrWhiteSpace(txtEmail.Text)
                    ? null
                    : txtEmail.Text.Trim();

                // --- Password ---
                usuario.password = string.IsNullOrWhiteSpace(txtPassword.Text)
                    ? null
                    : txtPassword.Text.Trim();

                // --- Nombre ---
                usuario.nombre = string.IsNullOrWhiteSpace(txtNombre.Text)
                    ? null
                    : txtNombre.Text.Trim();

                // --- apellido ---
                usuario.apellido = string.IsNullOrWhiteSpace(txtApellido.Text)
                    ? null
                    : txtApellido.Text.Trim();
                // --- esAdmin ---
                usuario.esAdmin = ddlRol.SelectedValue.ToLower() == "true" ? true : false;
                // --- UrlImagen ---
                guardarImagenDePerfil(usuario);
                
                // Ejecutar actualización
                bool exito = negocio.actualizarUsuario(usuario);

                if (exito)
                    Response.Redirect("AdminUsuarios.aspx");
                else
                {
                    //lblMensajeError.Text = "Error al actualizar el artículo.";
                    //lblMensajeError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void agregarUsuario()
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
   
                // --- Email ---
                usuario.email = string.IsNullOrWhiteSpace(txtEmail.Text)
                    ? null
                    : txtEmail.Text.Trim();

                // --- Password ---
                usuario.password = string.IsNullOrWhiteSpace(txtPassword.Text)
                    ? null
                    : txtPassword.Text.Trim();

                // --- Nombre ---
                usuario.nombre = string.IsNullOrWhiteSpace(txtNombre.Text)
                    ? null
                    : txtNombre.Text.Trim();

                // --- apellido ---
                usuario.apellido = string.IsNullOrWhiteSpace(txtApellido.Text)
                    ? null
                    : txtApellido.Text.Trim();
                // --- esAdmin ---
                usuario.esAdmin = ddlRol.SelectedValue.ToLower() == "true" ? true : false;
                // --- UrlImagen ---
                guardarImagenDePerfil(usuario);

                // Ejecutar actualización
                bool exito = negocio.insertarUsuario(usuario);

                if (exito)
                    Response.Redirect("AdminUsuarios.aspx");
                else
                {
                    //lblMensajeError.Text = "Error al actualizar el artículo.";
                    //lblMensajeError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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


                txtIdUsuario.Text = usuario.id.ToString();
                txtNombre.Text = usuario.nombre;
                txtApellido.Text = usuario.apellido;
                txtEmail.Text = usuario.email;
                txtPassword.Attributes["value"] = usuario.password;
                ddlRol.SelectedValue = usuario.esAdmin.ToString().ToLower();
                CargarImagen(usuario);
                pnlIdUsuario.Visible = true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void guardarImagenDePerfil(Usuario user)
        {
            try
            {
                //  ¿HAY ARCHIVO NUEVO?
                if (fileImagenUsuario.PostedFile != null &&
                    fileImagenUsuario.PostedFile.ContentLength > 0 &&
                    !string.IsNullOrEmpty(fileImagenUsuario.PostedFile.FileName))
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();

                    //  Si el usuario todavía NO existe → usamos próximo ID
                    int idParaNombre = user.id > 0
                        ? user.id
                        : negocio.obtenerProximoId();

                    string rutaFisica = Server.MapPath("~/Images/");
                    string nombreArchivo = $"perfil-{idParaNombre}.jpg";

                    fileImagenUsuario.PostedFile.SaveAs(rutaFisica + nombreArchivo);

                    user.urlImagenPerfil = $"~/Images/{nombreArchivo}";
                    imgUsuario.ImageUrl = user.urlImagenPerfil + "?v=" + DateTime.Now.Ticks;
                }
                else
                {
                    // NO SE SUBIÓ NUEVA → DECIDIR QUÉ GUARDAR

                    string urlActual = imgUsuario.ImageUrl?.Split('?')[0];

                    //  Si sigue la default → NO guardar en BD
                    if (string.IsNullOrWhiteSpace(urlActual) ||
                        urlActual.EndsWith("no-user.jpg", StringComparison.OrdinalIgnoreCase) ||
                        urlActual.EndsWith("no-image.png", StringComparison.OrdinalIgnoreCase))
                    {
                        user.urlImagenPerfil = null;
                    }
                    else
                    {
                        //  Conservar la imagen existente
                        user.urlImagenPerfil = urlActual;
                    }
                }
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


        }

        private bool validacionesCamposObligatorios()
        {
            lblError.Visible = false;

            
            // 1) EMAIL 
            
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblError.Text = "El campo Email es obligatorio.";
                lblError.Visible = true;
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                    txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblError.Text = "Ingrese un correo electrónico válido.";
                lblError.Visible = true;
                return false;
            }

           
            // 2) CONTRASEÑA 
          
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "El campo Contraseña es obligatorio.";
                lblError.Visible = true;
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                    txtPassword.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
            {
                lblError.Text = "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número.";
                lblError.Visible = true;
                return false;
            }

            
            // 3) NOMBRE 
           
            if (!string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(
                        txtNombre.Text, @"^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]+$"))
                {
                    lblError.Text = "El nombre solo puede contener letras.";
                    lblError.Visible = true;
                    return false;
                }
            }

           
            // 4) APELLIDO 
           
            if (!string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(
                        txtApellido.Text, @"^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]+$"))
                {
                    lblError.Text = "El apellido solo puede contener letras.";
                    lblError.Visible = true;
                    return false;
                }
            }

            return true; // Todo OK
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