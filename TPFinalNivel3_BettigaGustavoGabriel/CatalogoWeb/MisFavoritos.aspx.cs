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
    public partial class MisFavoritos : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos
        {
            get { return (List<Articulo>)Session["listaArticulos"]; }
            set { Session["listaArticulos"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listarArticulos();
            repFavoritos.DataSource = listaArticulos;
            repFavoritos.DataBind();

        }

        protected void btnVerDetalleFav_Click(object sender, EventArgs e)
        {

        }
    }
}