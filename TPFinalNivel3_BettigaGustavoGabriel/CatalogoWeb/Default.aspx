<%@ Page Title="Home" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CatalogoWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/Default.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="updTarjetas" runat="server">
        <ContentTemplate>

            <div class="container-fluid home-layout">

                <!-- ROW GENERAL -->
                <div class="row">

                    <!-- ===========================
                         COLUMNA IZQUIERDA: FILTRO SECUNDARIO
                         =========================== -->
                    <div class="col-12 col-md-4 col-lg-3 mb-4">
                        <div class="filtro-secundario p-3">
                            <h2 class="titulo-filtro-secundario text-center">
                                Filtro
                            </h2>

                            <!-- Filtro por Categoría -->
                            <div class="mb-3">
                                <label for="ddlFiltroCategoria" class="form-label">Categoría</label>
                                <asp:DropDownList ID="ddlFiltroCategoria" runat="server"
                                                  CssClass="form-select">
                                </asp:DropDownList>
                            </div>

                            <!-- Filtro por Marca -->
                            <div class="mb-3">
                                <label for="ddlFiltroMarca" class="form-label">Marca</label>
                                <asp:DropDownList ID="ddlFiltroMarca" runat="server"
                                                  CssClass="form-select">
                                </asp:DropDownList>
                            </div>

                            <!-- Rango de precios -->
                            <div class="mb-3">
                                <label class="form-label">Rango de precios</label>
                                <div class="d-flex gap-2">
                                    <asp:TextBox ID="txtPrecioMin" runat="server"
                                                 CssClass="form-control"
                                                 placeholder="Mín." />
                                    <asp:TextBox ID="txtPrecioMax" runat="server"
                                                 CssClass="form-control"
                                                 placeholder="Máx." />
                                </div>
                            </div>

                            <!-- Botones de acción -->
                            <div class="d-flex justify-content-between mt-3">
                                <asp:Button ID="btnBuscarFiltros" runat="server"
                                            Text="Buscar"
                                            CssClass="btn btn-primary btn-sm"
                                            OnClick="btnBuscarFiltros_Click" />
                                <asp:Button ID="btnLimpiarFiltros" runat="server"
                                            Text="Borrar"
                                            CssClass="btn btn-outline-light btn-sm"
                                            OnClick="btnLimpiarFiltros_Click" />
                            </div>
                        </div>
                    </div>

                    <!-- ===========================
                         COLUMNA DERECHA: GRID DE PRODUCTOS
                         =========================== -->
                    <div class="col-12 col-md-8 col-lg-9">
                        <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-xl-4 g-4 productos-grid">
                            <asp:Repeater ID="repetidorDeTarjetas" runat="server" OnItemDataBound="repetidorDeTarjetas_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col">
                                        <div class="card tarjeta-producto h-100">
                                            <asp:Image ID="imgProducto"
                                                       runat="server"
                                                       CssClass="card-img-top img-producto"
                                                       ImageUrl='<%# Eval("imagenUrl") %>' />
                                            <div class="card-body">
                                                <h5 class="card-title titulo-producto"><%# Eval("nombre") %></h5>
                                                <p class="card-text descripcion-producto"><%# Eval("descripcion") %></p>
                                                <asp:Button
                                                    ID="btnVerDetalle"
                                                    runat="server"
                                                    Text="Ver Detalle"
                                                    CssClass="btn btn-primary btn-sm"
                                                    CommandArgument='<%# Eval("Id") %>'
                                                    OnClick="btnVerDetalle_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                        <div class="text-center mt-3 mb-4">
                            <asp:Button ID="btnCargarMas" runat="server"
                                        Text="Ver Más"
                                        CssClass="btn btn-outline-light"
                                        OnClick="btnCargarMas_Click" />
                        </div>
                    </div>

                </div> <!-- /row -->
            </div> <!-- /container-fluid -->

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>