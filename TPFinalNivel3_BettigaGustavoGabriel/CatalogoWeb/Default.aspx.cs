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
        // Cuántos artículos mostramos por “página”
        private const int TamanioPagina = 4;

        // Lista completa de artículos, guardada en Session
        public List<Articulo> listaArticulos
        {
            get { return (List<Articulo>)Session["listaArticulos"]; }
            set { Session["listaArticulos"] = value; }
        }

        // Cuántos artículos ya mostramos hasta ahora
        private int cantidadMostrada
        {
            get { return ViewState["cantidadMostrada"] != null ? (int)ViewState["cantidadMostrada"] : 0; }
            set { ViewState["cantidadMostrada"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1) Cargo TODOS los artículos UNA sola vez desde negocio
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulos = negocio.listarArticulos();

                // 2) Arranco mostrando desde cero
                cantidadMostrada = 0;

                // 3) Llamo a cargarTarjetas para mostrar los primeros 8
                cargarTarjetas();
            }
        }



        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.CommandArgument;
            Response.Redirect("DetalleProducto.aspx?id=" + id);

        }

        protected void btnCargarMas_Click(object sender, EventArgs e)
        {
            // Cada vez que tocan el botón, muestro 8 más
            cargarTarjetas();
        }

        private void cargarTarjetas()
        {
            // Si la lista está vacía o nula, oculto el botón y no hago nada
            if (listaArticulos == null || listaArticulos.Count == 0)
            {
                btnCargarMas.Visible = false;
                return;
            }

            // Cuántos faltan por mostrar
            int restantes = listaArticulos.Count - cantidadMostrada;

            // De lo que falta, tomo hasta 8
            int aTomar = Math.Min(TamanioPagina, restantes);

            // Actualizo cuántos llevo mostrados
            cantidadMostrada += aTomar;

            // Tomo desde el inicio hasta "cantidadMostrada"
            var listaParcial = listaArticulos
                                .Take(cantidadMostrada)
                                .ToList();

            // Bindeo al Repeater SOLO esa porción
            repetidorDeTarjetas.DataSource = listaParcial;
            repetidorDeTarjetas.DataBind();

            // Si ya mostré todos, oculto el botón “Cargar más”
            btnCargarMas.Visible = cantidadMostrada < listaArticulos.Count;
        }

        protected void repetidorDeTarjetas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var articulo = (Articulo)e.Item.DataItem;
                var img = (Image)e.Item.FindControl("imgProducto");

                string fallback = ResolveUrl("~/Images/no-image.png");

                // ✅ Asigna la imagen (o fallback si viene null/vacía)
                img.ImageUrl = string.IsNullOrWhiteSpace(articulo.imagenUrl)
                    ? fallback
                    : ResolveUrl(articulo.imagenUrl);

                // ✅ Si la imagen falla en el navegador → usa fallback automáticamente
                img.Attributes["onerror"] =
                    $"this.onerror=null; this.src='{fallback}';";
            }
        }
    }
}