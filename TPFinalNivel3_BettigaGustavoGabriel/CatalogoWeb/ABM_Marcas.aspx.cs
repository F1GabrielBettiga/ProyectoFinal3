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
    public partial class ABM_Marcas : System.Web.UI.Page
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

        protected void btnGuardarMarca_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (validacionesCamposObligatorios())
                {
                    string id = Request.QueryString["id"];
                    MarcaNegocio negocio = new MarcaNegocio();
                    Marca marca = negocio.listarMarcas().FirstOrDefault(a => a.id == int.Parse(id));

                   
                    string descMarcaBD = (marca.descripcion ?? string.Empty).Trim().Replace(" ", "").ToUpper();

                    string descTextbox = (txtNombreMarca.Text ?? string.Empty).Trim().Replace(" ", "").ToUpper();

                    
                    if (descMarcaBD == descTextbox)
                    {
                        actualizarMarca();
                    }
                    else if (ExisteDescripcion(txtNombreMarca.Text))
                    {
                        lblErrorMarca.Text = "Marca ya registrada.";
                        lblErrorMarca.Visible = true;
                        return;
                    }
                    else
                    {
                        actualizarMarca();
                    }
                }
            }
            else
            {
                if (validacionesCamposObligatorios())
                {
                    if (ExisteDescripcion(txtNombreMarca.Text))
                    {
                        lblErrorMarca.Text = "Marca ya registrada.";
                        lblErrorMarca.Visible = true;
                        return;
                    }
                    else
                    {
                        agregarMarca();
                    }
                }
            }



        }
        private void actualizarMarca()
        {
            try
            {
                Marca marca = new Marca();
                MarcaNegocio negocio = new MarcaNegocio();

                marca.id = int.Parse(txtIdMarca.Text);
                marca.descripcion = txtNombreMarca.Text;

                bool exito = negocio.actualizarMarca(marca);

                if (exito)
                {
                    Response.Redirect("AdminMarcas.aspx",false);
                }
                else
                {
                    lblErrorMarca.Text = "Error al actualizar el artículo.";
                    lblErrorMarca.Visible = true;
                }
            }
            catch (Exception ex)
            {
                RedirigirConError("Error al actualizar la marca.", ex);
            }
        }
        private void CargarDetalles(int id)
        {
            try
            {

                MarcaNegocio negocio = new MarcaNegocio();


                Marca marca = negocio.listarMarcas().FirstOrDefault(a => a.id == id);


                if (marca == null)
                    return;



                txtIdMarca.Text = marca.id.ToString();
                txtNombreMarca.Text = marca.descripcion;
                pnlIdMarca.Visible = true;
                lblTituloABMMarca.Text = "Editar marca";
                btnGuardarMarca.Text = "Modificar";



            }
            catch (Exception ex)
            {

                RedirigirConError("Error al cargar los detalles de la marca.", ex);
            }
        }
        private void agregarMarca()
        {

            try
            {
                Marca marca = new Marca();
                MarcaNegocio negocio = new MarcaNegocio();

                
                marca.descripcion = txtNombreMarca.Text;

                bool exito = negocio.agregarMarca(marca);

                if (exito)
                {
                    Response.Redirect("AdminMarcas.aspx", false);
                }
                else
                {
                    lblErrorMarca.Text = "Error al actualizar el artículo.";
                    lblErrorMarca.Visible = true;
                }
            }
            catch (Exception ex)
            {
                RedirigirConError("Error al agregar la marca.", ex);
            }

        }
        protected void btnCancelarMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminMarcas.aspx", false);
        }
        private bool validacionesCamposObligatorios()
        {
            lblErrorMarca.Visible = false;

            // Nombre obligatorio
            if (string.IsNullOrWhiteSpace(txtNombreMarca.Text))
            {
                lblErrorMarca.Text = "El nombre de la marca es obligatorio.";
                lblErrorMarca.Visible = true;
                return false;
            }

            // Solo letras
            if (!System.Text.RegularExpressions.Regex.IsMatch(
                    txtNombreMarca.Text, @"^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]+$"))
            {
                lblErrorMarca.Text = "El nombre de la marca solo puede contener letras.";
                lblErrorMarca.Visible = true;
                return false;
            }

            return true; 
        }
        bool ExisteDescripcion(string descripcion)
        {
            MarcaNegocio negocio = new MarcaNegocio();

            if (negocio.ExisteMarca(descripcion))
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