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
    public partial class ABM_Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    
                    string id = Request.QueryString["id"];
                    CargarDetalles(int.Parse(id));
                }
            }



        }

        protected void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            // Normalizar el texto ingresado
            string textoIngresado = txtNombreCategoria.Text.Trim().Replace(" ", "").ToUpper();

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (validacionesCamposObligatorios())
                {
                    string id = Request.QueryString["id"];
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    Categoria categoria = negocio.listarCategorias()
                                                .FirstOrDefault(a => a.id == int.Parse(id));

                    // Normalizar descripción actual
                    string descripcionCategoria = categoria.descripcion.Trim().Replace(" ", "").ToUpper();

                    // Si NO cambió la descripción → actualizar sin validar duplicados
                    if (descripcionCategoria == textoIngresado)
                    {
                        actualizarCategoria();
                    }
                    else if (ExisteDescripcion(txtNombreCategoria.Text))
                    {
                        lblErrorCategoria.Text = "Categoría ya registrada.";
                        lblErrorCategoria.Visible = true;
                        return;
                    }
                    else
                    {
                        actualizarCategoria();
                    }
                }
            }
            else
            {
                if (validacionesCamposObligatorios())
                {
                    if (ExisteDescripcion(txtNombreCategoria.Text))
                    {
                        lblErrorCategoria.Text = "Categoría ya registrada.";
                        lblErrorCategoria.Visible = true;
                        return;
                    }
                    else
                    {
                        agregarCategoria();
                    }
                }
            }


        }
        private void actualizarCategoria()
        {
            try
            {
                Categoria categoria = new Categoria();
                CategoriaNegocio negocio = new CategoriaNegocio();

                categoria.id = int.Parse(txtIdCategoria.Text);
                categoria.descripcion = txtNombreCategoria.Text;

                bool exito = negocio.actualizarCategoria(categoria);

                if (exito)
                {
                    
                    Response.Redirect("AdminCategorias.aspx",false);
                }
                else
                {
                    lblErrorCategoria.Text = "Error al actualizar el artículo.";
                    lblErrorCategoria.Visible = true;
                }
            }
            catch (Exception ex)
            {
                RedirigirConError("Error al actualizar la categoría.", ex);
            }
        }
        private void CargarDetalles(int id)
        {
            try
            {

                CategoriaNegocio negocio = new CategoriaNegocio();


                Categoria categoria = negocio.listarCategorias().FirstOrDefault(a => a.id == id);


                if (categoria == null)
                    return;



                txtIdCategoria.Text = categoria.id.ToString();
                txtNombreCategoria.Text = categoria.descripcion;
                pnlIdCategoria.Visible = true;
                lblTituloABMCategoria.Text = "Editar categoría";
                btnGuardarCategoria.Text = "Modificar";


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void agregarCategoria()
        {
            try
            {
                Categoria categoria = new Categoria();
                CategoriaNegocio negocio = new CategoriaNegocio();

                
                categoria.descripcion = txtNombreCategoria.Text;

                bool exito = negocio.agregarCategoria(categoria); 

                if (exito)
                {
                    Response.Redirect("AdminCategorias.aspx", false);
                }
                else
                {
                    lblErrorCategoria.Text = "Error al actualizar el artículo.";
                    lblErrorCategoria.Visible = true;
                }
            }
            catch (Exception ex)
            {
                RedirigirConError("Error al agregar la categoría.", ex);
            }

        }
        protected void btnCancelarCategoria_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminCategorias.aspx", false);
        }
        private bool validacionesCamposObligatorios()
        {
            lblErrorCategoria.Visible = false;

            //Nombre obligatorio
            if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
            {
                lblErrorCategoria.Text = "El nombre de la categoría es obligatorio.";
                lblErrorCategoria.Visible = true;
                return false;
            }

            // Solo letras
            if (!System.Text.RegularExpressions.Regex.IsMatch(
                    txtNombreCategoria.Text, @"^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]+$"))
            {
                lblErrorCategoria.Text = "El nombre de la categoría solo puede contener letras.";
                lblErrorCategoria.Visible = true;
                return false;
            }

            return true; 
        }
        bool ExisteDescripcion(string descripcion)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            if (negocio.ExisteDescripcion(descripcion))
            {

                return true;
            }

            return false;
        }
        private void RedirigirConError(string mensajeUsuario, Exception ex = null)
        {
            Session["ErrorUsuario"] = mensajeUsuario;
            Session["ErrorTecnico"] = ex != null ? ex.ToString() : null;
            Response.Redirect("Error.aspx", false);
        }
    }
}