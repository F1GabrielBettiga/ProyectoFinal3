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
        // Tamaño de página (cuántos favoritos se agregan por cada "Ver más")
        private const int TamanioPagina = 2;

        private List<Favorito> listaFavoritos
        {
            get { return (List<Favorito>)Session["listaFavoritos"]; }
            set { Session["listaFavoritos"] = value; }
        }

        // Cuántos favoritos llevo mostrados
        private int cantidadMostrada
        {
            get
            {
                if (ViewState["cantidadMostradaFav"] == null)
                    return 0;
                return (int)ViewState["cantidadMostradaFav"];
            }
            set { ViewState["cantidadMostradaFav"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {           
                Usuario userLogueado = (Usuario)Session["UsuarioLogueado"];

                FavoritoNegocio favNegocio = new FavoritoNegocio();
                listaFavoritos = favNegocio.TraerFavoritosPorUsuario(userLogueado.id);

                // Reseteo el contador y cargo la primera "página"
                cantidadMostrada = 0;
                cargarTarjetasFavoritos(listaFavoritos);
            }
        }

        protected void btnVerDetalleFav_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idArticulo = btn.CommandArgument;
            Response.Redirect("DetalleProducto.aspx?id=" + idArticulo, false);
        }

        private void cargarTarjetasFavoritos(List<Favorito> fuente)
        {
            if (fuente == null || fuente.Count == 0)
            {
                repFavoritos.Visible = false;
                lblSinFavoritos.Visible = true;
                btnCargarMasFav.Visible = false;
                return;
            }

            repFavoritos.Visible = true;
            lblSinFavoritos.Visible = false;

            // Cuántos faltan por mostrar
            int restantes = fuente.Count - cantidadMostrada;

            // De lo que falta, tomo hasta TamanioPagina
            int aTomar = Math.Min(TamanioPagina, restantes);

            // Actualizo cuántos llevo mostrados
            cantidadMostrada += aTomar;

            // Tomo desde el inicio hasta "cantidadMostrada"
            var listaParcial = fuente
                .Take(cantidadMostrada)
                .ToList();

            repFavoritos.DataSource = listaParcial;
            repFavoritos.DataBind();

            // Si ya mostré todos, oculto el botón “Ver más”
            btnCargarMasFav.Visible = cantidadMostrada < fuente.Count;
        }

        protected void btnCargarMasFav_Click(object sender, EventArgs e)
        {
            cargarTarjetasFavoritos(listaFavoritos);
        }

        protected void repFavoritos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    {
               
                string fallback = ResolveUrl("~/Images/no-image.png");

                // Obtengo el objeto Favorito del ítem
                Favorito fav = (Favorito)e.Item.DataItem;

                // Busco el control de imagen
                Image img = (Image)e.Item.FindControl("imgProductoFav");

                if (img != null)
                {
                    // Si articulo o imagenUrl vienen vacíos → usar fallback
                    string url = (fav?.articulo?.imagenUrl);

                    img.ImageUrl = string.IsNullOrWhiteSpace(url)
                        ? fallback
                        : ResolveUrl(url);

                    // Si falla la carga en el navegador
                    img.Attributes["onerror"] =
                        $"this.onerror=null; this.src='{fallback}';";
                }
            }
        }
    }
}