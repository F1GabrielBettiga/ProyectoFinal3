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



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}