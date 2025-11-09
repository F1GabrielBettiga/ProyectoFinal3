<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="CatalogoWeb.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/MiPerfil.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="titulo-perfil">Mi Perfil</h1>

    <div class="perfil-container">
        <div class="perfil-card">

            <div class="row">
                <!-- ===========================
                     COLUMNA IZQUIERDA
                     =========================== -->
                <div class="col-md-4 col-sm-12 perfil-col-izquierda">

                    <!-- Imagen de perfil -->
                    <asp:Image ID="imgPerfil" runat="server"
                        CssClass="img-perfil"
                        AlternateText="Imagen de perfil" />

                    <!-- Cargar nueva imagen -->
                    <div class="mt-3">
                        <asp:FileUpload ID="fuImagenPerfil" runat="server" CssClass="input-imagen-perfil" />
                    </div>

                    <!-- Tipo de usuario (solo lectura) -->
                    <div class="mt-3">
                        <asp:Label ID="lblTipoUsuarioTitulo" runat="server"
                            Text="Tipo de usuario:"
                            CssClass="etiqueta-tipo-usuario"></asp:Label>
                        <asp:Label ID="lblTipoUsuario" runat="server"
                            CssClass="valor-tipo-usuario"></asp:Label>
                    </div>

                    <!-- ID de usuario (muestyro solo si es admin) -->
                    <div class="mt-2">
                        <asp:Label ID="lblIdUsuarioTitulo" runat="server"
                            Text="ID de usuario:"
                            CssClass="etiqueta-id-usuario"></asp:Label>
                        <asp:Label ID="lblIdUsuario" runat="server"
                            CssClass="valor-id-usuario"></asp:Label>
                    </div>

                    <!-- Combo para cambiar tipo de usuario (solo admins lo ´puede ver) -->
                    <div class="mt-3">
                        <asp:Label ID="lblEditarTipoUsuario" runat="server"
                            Text="Cambiar tipo de usuario:"
                            CssClass="etiqueta-editar-tipo"></asp:Label>
                        <asp:DropDownList ID="ddlTipoUsuario" runat="server"
                            CssClass="ddl-tipo-usuario">
                            <asp:ListItem Text="Estándar" Value="Estándar"></asp:ListItem>
                            <asp:ListItem Text="Administrador" Value="Administrador"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>

                <!-- ===========================
                     COLUMNA DERECHA
                     =========================== -->
                <div class="col-md-8 col-sm-12 perfil-col-derecha">

                    <!-- Nombre -->
                    <div class="form-group">
                        <label>Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server"
                            CssClass="form-control"></asp:TextBox>
                    </div>

                    <!-- Apellido -->
                    <div class="form-group">
                        <label>Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server"
                            CssClass="form-control"></asp:TextBox>
                    </div>

                    <!-- Email -->
                    <div class="form-group">
                        <label>Email</label>
                        <asp:TextBox ID="txtEmail" runat="server"
                            CssClass="form-control"></asp:TextBox>
                    </div>

                    <!-- Contraseña actual + checkbox + botón Cambiar -->
                    <div class="form-group">
                        <label>Contraseña</label>

                        <div class="fila-password">
                            <asp:TextBox ID="txtPassword" runat="server"
                                CssClass="form-control campo-password"
                                TextMode="Password"></asp:TextBox>

                            <asp:CheckBox ID="chkMostrarPassword" runat="server"
                                Text="Mostrar"
                                CssClass="chk-mostrar-password" />

                            <asp:Button ID="btnCambiarPassword" runat="server"
                                Text="Cambiar contraseña"
                                CssClass="btn btn-outline-light btn-sm btn-cambiar-password" />
                        </div>
                    </div>

                    <!-- Nueva contraseña + repetir  -->
                    <div id="bloqueCambioPassword">
                        <div class="form-group">
                            <label>Nueva contraseña</label>
                            <asp:TextBox ID="txtNuevaPassword" runat="server"
                                CssClass="form-control"
                                TextMode="Password"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Repetir contraseña</label>
                            <asp:TextBox ID="txtRepetirPassword" runat="server"
                                CssClass="form-control"
                                TextMode="Password"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Botones de acción -->
                    <div class="acciones-perfil">
                        <asp:Button ID="btnGuardar" runat="server"
                            Text="Guardar cambios"
                            CssClass="btn btn-primary" />

                        <asp:Button ID="btnVolverInicio" runat="server"
                            Text="Volver al inicio"
                            CssClass="btn btn-outline-light"
                            PostBackUrl="~/Default.aspx" />
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>
