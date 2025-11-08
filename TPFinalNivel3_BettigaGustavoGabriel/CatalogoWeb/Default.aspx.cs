using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace CatalogoWeb
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarTarjetas();
            }


        }





        private void cargarTarjetas()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            listaArticulos = negocio.listarArticulos();

            repetidorDeTarjetas.DataSource = listaArticulos;
            repetidorDeTarjetas.DataBind();

        }

    }
}