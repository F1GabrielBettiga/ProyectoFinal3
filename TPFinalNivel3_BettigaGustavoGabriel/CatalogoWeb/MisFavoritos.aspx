<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="MisFavoritos.aspx.cs" Inherits="CatalogoWeb.MisFavoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/MisFavoritos.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- ===========================
         CUERPO PRINCIPAL
         =========================== -->

    <div class="container favoritos-layout">

        <!-- Título principal -->
        <h1 class="titulo-favoritos text-center">MIS FAVORITOS</h1>

        <!-- PANEL AJAX para la lista + botón Ver más -->
        <asp:UpdatePanel ID="updFavoritos" runat="server">
            <ContentTemplate>

                <!-- LISTA DE FAVORITOS -->
                <asp:Repeater ID="repFavoritos" runat="server"
                    OnItemDataBound="repFavoritos_ItemDataBound">
                    <ItemTemplate>

                        <!-- Ítem tipo fila (estilo Mercado Libre) -->
                        <div class="card favorito-item mb-3">
                            <div class="row g-0 align-items-center">

                                <!-- ===========================
                                     Columna 1: Imagen
                                     =========================== -->
                                <div class="col-12 col-md-3 col-lg-2 text-center">
                                    <asp:Image ID="imgProductoFav"
                                        runat="server"
                                        CssClass="img-favorito"
                                        ImageUrl='<%# Eval("articulo.imagenUrl") %>' />
                                </div>

                                <!-- ===========================
                                     Columna 2: Título + descripción + botón Ver
                                     =========================== -->
                                <div class="col-12 col-md-6 col-lg-7">
                                    <div class="favorito-texto">
                                        <h5 class="favorito-titulo">
                                            <%# Eval("articulo.nombre") %>
                                        </h5>

                                        <p class="favorito-descripcion">
                                            <%# Eval("articulo.descripcion") %>
                                        </p>

                                        <asp:Button ID="btnVerDetalleFav"
                                            runat="server"
                                            Text="Ver detalle"
                                            CssClass="btn btn-primary btn-sm"
                                            CommandArgument='<%# Eval("articulo.Id") %>'
                                            OnClick="btnVerDetalleFav_Click" />
                                    </div>
                                </div>

                                <!-- ===========================
                                     Columna 3: Precio + ícono favorito
                                     =========================== -->
                                <div class="col-12 col-md-3 col-lg-3 d-flex flex-column 
                                            align-items-md-end align-items-start 
                                            justify-content-between favorito-lateral">

                                    <!-- Precio -->
                                    <span class="favorito-precio">
                                        <%# ((decimal)Eval("articulo.precio"))
                                                .ToString("C", new System.Globalization.CultureInfo("es-AR")) %>
                                    </span>

                                    <!-- Ícono de corazón (por ahora solo visual) -->


                                    <asp:ImageButton ID="imgFavorito" runat="server"
                                        ImageUrl="~/Images/fav-full.png"
                                        AlternateText="Quitar de favoritos"
                                        CssClass="icono-favorito"
                                        ToolTip="Quitar de favoritos"
                                        CommandArgument='<%# Eval("articulo.Id") %>'
                                        OnClick="imgFavorito_Click" />
                                </div>

                            </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>

                <!-- Botón Ver más (paginado) -->
                <div class="text-center mt-3 mb-4">
                    <asp:Button ID="btnCargarMasFav" runat="server"
                        Text="Ver Más"
                        CssClass="btn btn-outline-light"
                        OnClick="btnCargarMasFav_Click" />
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Mensaje cuando no haya favoritos -->
        <asp:Label ID="lblSinFavoritos" runat="server"
            CssClass="texto-sin-favoritos text-center"
            Visible="false"
            Text="Todavía no tenés productos en favoritos." />
    </div>

</asp:Content>
