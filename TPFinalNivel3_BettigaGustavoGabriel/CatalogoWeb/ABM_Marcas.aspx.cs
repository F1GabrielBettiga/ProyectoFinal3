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
    public partial class ABM_Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    lblTituloABMMarca.Text = "Editar marca";
                    string id = Request.QueryString["id"];
                    CargarDetalles(int.Parse(id));
                }
            }


        }

        protected void btnGuardarMarca_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                actualizarMarca();
            }
            else
            {
                // Código para agregar un nuevo artículo (no implementado en este snippet)
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
                    Response.Redirect("AdminMarcas.aspx");
                }
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

                MarcaNegocio negocio = new MarcaNegocio();


                Marca marca = negocio.listarMarcas().FirstOrDefault(a => a.id == id);


                if (marca == null)
                    return;



                txtIdMarca.Text = marca.id.ToString();
                txtNombreMarca.Text = marca.descripcion;

               

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}