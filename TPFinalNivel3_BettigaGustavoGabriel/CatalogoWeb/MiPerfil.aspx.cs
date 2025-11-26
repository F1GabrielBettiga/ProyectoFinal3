using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Negocio;

namespace CatalogoWeb
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (Session["UsuarioLogueado"] != null)
                {
                    Usuario user = (Usuario)Session["UsuarioLogueado"];

                    CargarDetalles(user.id);
                    ModoSoloLectura();

                }
            }



        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["UsuarioLogueado"] != null)
            {
                
                actualizarPerfil();


            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            ModoEdicion();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, false);
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


        void actualizarPerfil()
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                usuario = (Usuario)Session["UsuarioLogueado"];

                // --- Email ---
                usuario.email = string.IsNullOrWhiteSpace(txtEmail.Text)
                    ? null
                    : txtEmail.Text.Trim();

                // --- Password ---
                if (txtNuevaPassword.Visible)
                {
                    if (txtNuevaPassword.Text == txtRepetirPassword.Text)
                    {
                        usuario.password = txtNuevaPassword.Text.Trim();
                    }
                    else
                    {
                        lblMensajeError.ForeColor = Color.Red;
                        lblMensajeError.Text = "Las nuevas contraseñas no coinciden.";
                        lblMensajeError.Visible = true;
                        return;
                    }
                }
                else
                {
                   
                    usuario.password = string.IsNullOrWhiteSpace(txtPassword.Text)
                        ? null
                        : txtPassword.Text.Trim();
                }

                // --- Nombre ---
                usuario.nombre = string.IsNullOrWhiteSpace(txtNombre.Text)
                    ? null
                    : txtNombre.Text.Trim();

                // --- Apellido ---
                usuario.apellido = string.IsNullOrWhiteSpace(txtApellido.Text)
                    ? null
                    : txtApellido.Text.Trim();

                // --- esAdmin ---
                usuario.esAdmin = lblTipoUsuario.Text.Equals("administrador",
                                   StringComparison.OrdinalIgnoreCase);

                // --- UrlImagen ---
                guardarImagenDePerfil(usuario);

                // Ejecutar actualización
                bool exito = negocio.actualizarUsuario(usuario);

                if (exito)
                    Response.Redirect(Request.RawUrl, false);
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
                    imgPerfil.ImageUrl = user.urlImagenPerfil + "?v=" + DateTime.Now.Ticks;
                }
                else
                {
                    // NO SE SUBIÓ NADA NUEVO → CONSERVAR LA QUE YA TENÍA

                    // Si en la página hay alguna imagen cargada, la reutilizo
                    if (!string.IsNullOrWhiteSpace(imgPerfil.ImageUrl))
                    {
                        // Por si tiene el "?v=12345" de cache-busting, me quedo solo con la ruta
                        string urlActual = imgPerfil.ImageUrl.Split('?')[0];

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





        private void ModoSoloLectura()
        {
            
            txtNombre.ReadOnly = true;
            txtApellido.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtPassword.ReadOnly = true;



            
            txtNuevaPassword.Visible = false;
            txtRepetirPassword.Visible = false;

            // Botones
            btnCambiarPassword.Visible = false;
            btnEditar.Visible = true;
            btnGuardar.Visible = false;  
            btnCancelar.Visible = false;
        }

        private void ModoEdicion()
        {
           
            txtNombre.ReadOnly = false;
            txtApellido.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtPassword.ReadOnly = false;
            



            // Botones
            btnCambiarPassword.Visible = true;
            btnEditar.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
        }


    }
}