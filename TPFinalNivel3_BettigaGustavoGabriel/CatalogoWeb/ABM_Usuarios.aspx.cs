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
            if (Session["UsuarioEditar"] != null)
            {
                actualizarUsuario();

            }
            else
            {
                // Código para agregar un nuevo artículo (no implementado en este snippet)
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
                // ¿HAY ARCHIVO NUEVO?
                if (fileImagenUsuario.PostedFile != null && fileImagenUsuario.PostedFile.ContentLength > 0 && !string.IsNullOrEmpty(fileImagenUsuario.PostedFile.FileName))
                {
                    string rutaFisica = Server.MapPath("~/Images/"); // carpeta interna
                    string nombreArchivo = "perfil-" + user.id + ".jpg";

                    // Guarda físicamente en la carpeta del proyecto
                    fileImagenUsuario.PostedFile.SaveAs(rutaFisica + nombreArchivo);

                    // Guarda la ruta virtual (accesible desde la web y portable entre equipos)
                    user.urlImagenPerfil = "~/Images/" + nombreArchivo;

                    // Actualiza el <asp:Image> para la vista previa
                    imgUsuario.ImageUrl = user.urlImagenPerfil + "?v=" + DateTime.Now.Ticks;
                }
                else
                {
                    // NO SE SUBIÓ NADA NUEVO → CONSERVAR LA QUE YA TENÍA

                    // Si en la página hay alguna imagen cargada, la reutilizo
                    if (!string.IsNullOrWhiteSpace(imgUsuario.ImageUrl))
                    {
                        // Por si tiene el "?v=12345" de cache-busting, me quedo solo con la ruta
                        string urlActual = imgUsuario.ImageUrl.Split('?')[0];

                        // Si no es la imagen por defecto, la conservo
                        if (!urlActual.EndsWith("no-image.png", StringComparison.OrdinalIgnoreCase))
                        {
                            user.urlImagenPerfil = urlActual;
                        }
                        else
                        {
                            // Imagen por defecto → guardo null en la entidad
                            user.urlImagenPerfil = null;
                        }
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
    }
}