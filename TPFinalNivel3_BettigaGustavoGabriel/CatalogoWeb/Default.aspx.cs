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
        private int cantidadMostrada
        {
            get { return ViewState["cantidadMostrada"] != null ? (int)ViewState["cantidadMostrada"] : 0; }
            set { ViewState["cantidadMostrada"] = value; }
        }
        // Cuántos artículos mostramos por “página”
        private const int TamanioPagina = 4;
        private  List<Articulo> listaArticulos
        {
            get { return (List<Articulo>)Session["listaArticulos"]; }
            set { Session["listaArticulos"] = value; }
        }
        private List<Articulo> listaArticulosFiltrada
        {
            get { return (List<Articulo>)Session["listaArticulosFiltrada"]; }
            set { Session["listaArticulosFiltrada"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                CargarDdlCategorias();
                CargarDdlMarcas(0);


                listaArticulos = negocio.listarArticulos();

                // 2) Arranco mostrando desde cero
                cantidadMostrada = 0;

                // 3) Llamo a cargarTarjetas para mostrar los primeros 8
                cargarTarjetas(listaArticulos);
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
            if (listaArticulosFiltrada == null)
            {
                cargarTarjetas(listaArticulos);
            }
            else
            {
                cargarTarjetas(listaArticulosFiltrada);
            }
        }
        protected void btnBuscarFiltros_Click(object sender, EventArgs e)
        {
            {
                int? idCategoria = int.Parse(ddlFiltroCategoria.SelectedValue);
                int? idMarca = int.Parse(ddlFiltroMarca.SelectedValue);

                decimal? precioMin = null;
                decimal? precioMax = null;
                decimal tempMin;
                decimal tempMax;

                ArticuloNegocio negocio = new ArticuloNegocio();



                // Convertir valor 0 "sin filtro"
                if (idCategoria == 0)
                    idCategoria = null;

                if (idMarca == 0)
                    idMarca = null;

                // Precio mínimo
                if (decimal.TryParse(txtPrecioMin.Text, out tempMin))
                { precioMin = tempMin; }

                // Precio máximo
                if (decimal.TryParse(txtPrecioMax.Text, out tempMax))
                { precioMax = tempMax; }

                listaArticulosFiltrada = negocio.FiltrarArticulos(idCategoria, idMarca, precioMin, precioMax);

                cantidadMostrada = 0;

                cargarTarjetas(listaArticulosFiltrada);
            }

        }
        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            // Limpio los controles visuales
            ddlFiltroCategoria.SelectedIndex = 0;
            ddlFiltroMarca.SelectedIndex = 0;
            txtPrecioMin.Text = "";
            txtPrecioMax.Text = "";

            //Limpio también la lista filtrada
            listaArticulosFiltrada = null;

            //Reseteo el paginado
            cantidadMostrada = 0;

            // Vuelvo a cargar la lista COMPLETA
            cargarTarjetas(listaArticulos);
        }
        protected void ddlFiltroCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int valor = int.Parse(ddlFiltroCategoria.SelectedValue);

            CargarDdlMarcas(valor);

        }
        protected void ddlFiltroMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int valor = int.Parse(ddlFiltroMarca.SelectedValue);

            if (valor != 0)
            {
                btnBuscarFiltros.Enabled = true;


            }
        }
        protected void repetidorDeTarjetas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var articulo = (Articulo)e.Item.DataItem;
                var img = (Image)e.Item.FindControl("imgProducto");

                string fallback = ResolveUrl("~/Images/no-image.png");

                // Asigna la imagen (o fallback si viene null/vacía)
                img.ImageUrl = string.IsNullOrWhiteSpace(articulo.imagenUrl)
                    ? fallback
                    : ResolveUrl(articulo.imagenUrl);

                //Si la imagen falla en el navegador → usa fallback automáticamente
                img.Attributes["onerror"] =
                    $"this.onerror=null; this.src='{fallback}';";
            }
        }
        private void cargarTarjetas(List<Articulo> origen)
        {
            // Si la lista está vacía o nula, oculto el botón y limpio el repeater
            if (origen == null || origen.Count == 0)
            {
                btnCargarMas.Visible = false;
                repetidorDeTarjetas.DataSource = null;
                repetidorDeTarjetas.DataBind();
                return;
            }

            // Cuántos faltan por mostrar
            int restantes = origen.Count - cantidadMostrada;

            // De lo que falta, tomo hasta TamanioPagina
            int aTomar = Math.Min(TamanioPagina, restantes);

            // Actualizo cuántos llevo mostrados
            cantidadMostrada += aTomar;

            // Tomo desde el inicio hasta "cantidadMostrada"
            var listaParcial = origen
                                .Take(cantidadMostrada)
                                .ToList();

            // Bindeo al Repeater SOLO esa porción
            repetidorDeTarjetas.DataSource = listaParcial;
            repetidorDeTarjetas.DataBind();

            // Si ya mostré todos, oculto el botón Cargar más
            btnCargarMas.Visible = (cantidadMostrada < origen.Count);
        }      
        private void CargarDdlCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> lista = negocio.listarCategorias();


            ddlFiltroCategoria.DataSource = lista;
            ddlFiltroCategoria.DataTextField = "descripcion";
            ddlFiltroCategoria.DataValueField = "id";
            ddlFiltroCategoria.DataBind();
            
            ddlFiltroCategoria.Items.Insert(0, new ListItem("Todos", "0"));


        }
        private void CargarDdlMarcas(int id)
        {
            if (id == 0)
            {
                ddlFiltroMarca.Items.Clear();
                MarcaNegocio negocio = new MarcaNegocio();
                List<Marca> lista = negocio.listarMarcasPorCategoria(id);
                ddlFiltroMarca.DataSource = lista;
                ddlFiltroMarca.DataTextField = "descripcion";
                ddlFiltroMarca.DataValueField = "id";
                ddlFiltroMarca.DataBind();
                ddlFiltroMarca.Items.Insert(0, new ListItem("Todos", "0"));

            }
            else
            {

                MarcaNegocio negocio = new MarcaNegocio();
                List<Marca> lista = negocio.listarMarcasPorCategoria(id);
                ddlFiltroMarca.DataSource = lista;
                ddlFiltroMarca.DataTextField = "descripcion";
                ddlFiltroMarca.DataValueField = "id";
                ddlFiltroMarca.DataBind();
                ddlFiltroMarca.Items.Insert(0, new ListItem("Todos", "0"));
            }
        }
      
    }
}