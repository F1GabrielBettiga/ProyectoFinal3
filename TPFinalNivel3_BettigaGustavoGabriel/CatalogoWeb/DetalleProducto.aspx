<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="CatalogoWeb.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/DetalleProducto.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="detalle-container">

        <!-- Título principal -->
        <h1 class="titulo-detalle">
            <asp:Label ID="lblNombre" runat="server" Text="Nombre del producto"></asp:Label>
        </h1>

        <!-- Subtítulo con código, marca y categoría -->
        <p class="subtitulo-detalle">
            Código:
            <asp:Label ID="lblCodigo" runat="server"></asp:Label>
            |
            Marca:
            <asp:Label ID="lblMarca" runat="server"></asp:Label>
            |
            Categoría:
            <asp:Label ID="lblCategoria" runat="server"></asp:Label>
        </p>

        <!-- Contenido principal dividido en dos columnas -->
        <div class="detalle-producto">

            <!-- Columna izquierda: Imagen -->
            <div class="detalle-imagen">
                <asp:Image ID="imgProducto" runat="server"
                    CssClass="img-fluid rounded shadow"
                    AlternateText="Imagen del producto" />
            </div>

            <!-- Columna derecha: Detalles -->
            <div class="detalle-info">
                <h3>Precio:</h3>
                <p class="precio">
                    <asp:Label ID="lblPrecio" runat="server"></asp:Label>
                </p>

                <h3>Descripción:</h3>
                <p class="descripcion">
                    <asp:Label ID="lblDescripcion" runat="server"></asp:Label>
                </p>

                <!-- Botones -->
                <div class="acciones-detalle">
                    <asp:Button ID="btnVolver" runat="server" Text="Volver al inicio"
                        CssClass="btn btn-outline-light"
                        PostBackUrl="~/Default.aspx" />

                    <asp:ImageButton ID="btnFavorito" runat="server"
                        ImageUrl="~/Images/fav-empty.png"
                        AlternateText="Agregar a favoritos"
                        CssClass="btn-favorito"
                        ToolTip="Agregar a favoritos" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
