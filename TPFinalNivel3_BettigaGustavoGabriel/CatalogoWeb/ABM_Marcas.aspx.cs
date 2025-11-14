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
                    string id = Request.QueryString["id"];
                    CargarDetalles(int.Parse(id));
                }
            }


        }

        protected void btnGuardarMarca_Click(object sender, EventArgs e)
        {

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