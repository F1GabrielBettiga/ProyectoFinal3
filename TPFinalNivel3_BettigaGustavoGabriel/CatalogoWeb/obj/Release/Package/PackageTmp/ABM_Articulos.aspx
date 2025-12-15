<%@ Page Title="ABM Articulos" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="ABM_Articulos.aspx.cs" Inherits="CatalogoWeb.ABM_Articulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" type="text/css" href="Css/ABM_Articulos.css" />
    <script src="<%: ResolveUrl("~/Scripts/perfil.js") %>"></script>
    <script src="<%: ResolveUrl("~/Scripts/ABM_Articulos.js") %>"></script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="abm-articulo-container">

        <asp:Label ID="lblTituloABM" runat="server"
            Text="Nuevo artículo"
            CssClass="titulo-abm-articulo" />

        <div class="abm-articulo-card">

            <!-- Fila ID  -->
            <asp:Panel ID="pnlIdArticulo" runat="server" Visible="false" CssClass="fila-formulario fila-id">
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

                    <!-- Código  -->
                    <div class="mb-3">
                        <asp:Label ID="lblCodigo" runat="server" Text="Código" CssClass="form-label" />
                        <asp:TextBox ID="txtCodigo" runat="server"
                            CssClass="form-control"
                            MaxLength="50" />

                        <asp:RequiredFieldValidator
                            ID="rfvCodigo"
                            runat="server"
                            ControlToValidate="txtCodigo"
                            CssClass="text-danger"
                            ErrorMessage="El código es obligatorio."
                            Display="Dynamic"
                            ValidationGroup="Articulo">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator
                            ID="revCodigo"
                            runat="server"
                            ControlToValidate="txtCodigo"
                            ValidationExpression="^[a-zA-Z0-9]*$"
                            ErrorMessage="Solo se permiten letras y números."
                            CssClass="text-danger"
                            Display="Dynamic"
                            ValidationGroup="Articulo" />
                    </div>

                    <!-- Nombre -->
                    <div class="mb-3">
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server"
                            CssClass="form-control"
                            MaxLength="50" />

                        <asp:RequiredFieldValidator
                            ID="rfvNombre"
                            runat="server"
                            ControlToValidate="txtNombre"
                            CssClass="text-danger"
                            ErrorMessage="El nombre es obligatorio."
                            Display="Dynamic"
                            ValidationGroup="Articulo">
                        </asp:RequiredFieldValidator>
                    </div>

                    <!-- Descripción -->
                    <div class="mb-3">
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" CssClass="form-label" />

                        <asp:TextBox ID="txtDescripcion" runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine" Rows="3"
                            MaxLength="150"
                            ClientIDMode="Static"
                            oninput="contarYLimitarTexto('txtDescripcion', 'contadorDescripcion', 150)"
                             />

                        <small id="contadorDescripcion" class="contador-caracteres" data-max="150">0/150</small>
                    </div>

                    <!-- Precio -->
                    <div class="mb-3">
                        <asp:Label ID="lblPrecio" runat="server" Text="Precio" CssClass="form-label" />
                        <asp:TextBox ID="txtPrecio" runat="server"
                            CssClass="form-control"
                            MaxLength="15" />

                        <asp:RequiredFieldValidator
                            ID="rfvPrecio"
                            runat="server"
                            ControlToValidate="txtPrecio"
                            CssClass="text-danger"
                            ErrorMessage="El precio es obligatorio."
                            Display="Dynamic"
                            ValidationGroup="Articulo">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator
                            ID="revPrecio"
                            runat="server"
                            ControlToValidate="txtPrecio"
                            ValidationExpression="^[0-9]+([.,][0-9]+)?$"
                            ErrorMessage="Ingrese un número válido"
                            CssClass="text-danger"
                            Display="Dynamic"
                            ValidationGroup="Articulo" />
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
                                ImageUrl="/Images/no-image.png"
                                ClientIDMode="Static"
                                CssClass="img-fluid abm-img-articulo"
                                AlternateText="Imagen del artículo" />
                        </div>
                    </div>

                    <!-- Campo para cambiar la imagen (archivo) -->
                    <div class="mb-3">
                        <asp:Label ID="lblUrlImagen" runat="server" Text="Cargar imagen" CssClass="form-label" />
                        <input type="file" id="txtImagen" runat="server"
                            class="form-control"
                            onchange="mostrarVistaPrevia(this, 'imgArticulo')" />
                    </div>

                </div>
            </div>

            <asp:Label ID="lblError" runat="server"
                Text=""
                Visible="false"
                CssClass="mensaje-error" />

            <!-- Botones de acción -->
            <div class="abm-botones text-center mt-4">
                <asp:Button ID="btnGuardar" runat="server"
                    Text="Crear"
                    CssClass="btn btn-primary me-2"
                    OnClick="btnGuardar_Click"
                    ValidationGroup="Articulo"
                    OnClientClick="if (typeof(Page_ClientValidate) === 'function' && !Page_ClientValidate('Articulo')) return false; return confirm('¿Seguro que querés guardar este artículo?');" />

                <asp:Button ID="btnCancelar" runat="server"
                    Text="Cancelar"
                    CssClass="btn btn-outline-light"
                    OnClick="btnCancelar_Click"
                    OnClientClick="return confirm('¿Seguro que querés cancelar? Se perderán los cambios.');" />
            </div>

        </div>

    </div>   

</asp:Content>
