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
        <h1 class="titulo-favoritos text-center">Mis favoritos
        </h1>

        <!-- PANEL AJAX (opcional) para futuros clicks en corazón, etc. -->
        <asp:UpdatePanel ID="updFavoritos" runat="server">
            <ContentTemplate>

                <!-- LISTA DE FAVORITOS -->
                <asp:Repeater ID="repFavoritos" runat="server">
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
                                        ImageUrl='<%# Eval("imagenUrl") %>' />
                                </div>

                                <!-- ===========================
                                     Columna 2: Título + descripción + botón Ver
                                     =========================== -->
                                <div class="col-12 col-md-6 col-lg-7">
                                    <div class="favorito-texto">
                                        <h5 class="favorito-titulo">
                                            <%# Eval("nombre") %>
                                        </h5>

                                        <p class="favorito-descripcion">
                                            <%# Eval("descripcion") %>
                                        </p>

                                        <asp:Button ID="btnVerDetalleFav"
                                            runat="server"
                                            Text="Ver detalle"
                                            CssClass="btn btn-primary btn-sm"
                                            CommandArgument='<%# Eval("Id") %>'
                                            OnClick="btnVerDetalleFav_Click"/>
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
                                        <%# ((decimal)Eval("precio")).ToString("C", new System.Globalization.CultureInfo("es-AR")) %>
                                    </span>

                                    <!-- Ícono de corazón (por ahora solo visual) -->
                                    <asp:Image ID="imgFavorito"
                                        runat="server"
                                        CssClass="icono-favorito"
                                        ImageUrl="~/Images/fav-empty.png"
                                        AlternateText="Quitar de favoritos" />
                                </div>

                            </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>

            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Opcional: mensaje cuando no haya favoritos (lo manejás desde el code-behind) -->
        <asp:Label ID="lblSinFavoritos" runat="server"
            CssClass="texto-sin-favoritos text-center"
            Visible="false"
            Text="Todavía no tenés productos en favoritos." />
    </div>

</asp:Content>
