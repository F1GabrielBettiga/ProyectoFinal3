<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="ABM_Articulos.aspx.cs" Inherits="CatalogoWeb.ABM_Articulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" type="text/css" href="Css/ABM_Articulos.css" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="abm-articulo-container">

        <asp:Label ID="lblTituloABM" runat="server"
            Text="Nuevo artículo"
            CssClass="titulo-abm-articulo" />

        <div class="abm-articulo-card">

            <!-- Fila ID (alineada a la izquierda, label arriba del campo) -->
            <asp:Panel ID="pnlIdArticulo" runat="server" CssClass="fila-formulario fila-id">
                <div class="mb-3">
                    <asp:Label ID="lblId" runat="server" Text="ID" CssClass="form-label lbl-id" />
                    <asp:TextBox ID="txtId" runat="server"
                        CssClass="form-control input-id"
                        ReadOnly="true" />
                </div>
            </asp:Panel>

            <!-- Layout principal: dos columnas -->
            <div class="row g-3">

                <!-- Columna izquierda-->
                <div class="col-md-7">

                    <!-- Código -->
                    <div class="mb-3">
                        <asp:Label ID="lblCodigo" runat="server" Text="Código" CssClass="form-label" />
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
                    </div>

                    <!-- Nombre -->
                    <div class="mb-3">
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>

                    <!-- Descripción -->
                    <div class="mb-3">
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" CssClass="form-label" />
                        <asp:TextBox ID="txtDescripcion" runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine" Rows="3" />
                    </div>

                    <!-- Precio -->
                    <div class="mb-3">
                        <asp:Label ID="lblPrecio" runat="server" Text="Precio" CssClass="form-label" />
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                    </div>

                </div>

                <!-- Columna derecha-->
                <div class="col-md-5">

                    <!-- Marca -->
                    <div class="mb-3">
                        <asp:Label ID="lblMarca" runat="server" Text="Marca" CssClass="form-label" />
                        <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>

                    <!-- Categoría -->
                    <div class="mb-3">
                        <asp:Label ID="lblCategoria" runat="server" Text="Categoría" CssClass="form-label" />
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>

                    <!-- Imagen actual del artículo -->
                    <div class="mb-3">
                        <asp:Label ID="lblImagenActual" runat="server" Text="Imagen actual" CssClass="form-label" />
                        <div class="abm-imagen-wrapper">
                            <asp:Image ID="imgArticulo" runat="server"
                                CssClass="img-fluid abm-img-articulo"
                                AlternateText="Imagen del artículo" />
                        </div>
                    </div>

                    <!-- Campo para cambiar la imagen (URL) -->
                    <div class="mb-3">
                        <asp:Label ID="lblUrlImagen" runat="server" Text="Cargar imagen" CssClass="form-label" />
                        <input type="file" id="txtImagen" runat="server" class="form-control" />
                    </div>

                </div>
            </div>

            <!-- Botones de acción -->
            <div class="abm-botones text-center mt-4">
                <asp:Button ID="btnGuardar" runat="server"
                    Text="Guardar"
                    CssClass="btn btn-primary me-2"
                    OnClick="btnGuardar_Click" />

                <asp:Button ID="btnCancelar" runat="server"
                    Text="Cancelar"
                    CssClass="btn btn-outline-light"
                    OnClick="btnCancelar_Click" />
            </div>

        </div>

    </div>

</asp:Content>
