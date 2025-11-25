<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="CatalogoWeb.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/MiPerfil.css" />
    <script src="<%: ResolveUrl("~/Scripts/perfil.js") %>"></script>
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
                        ClientIDMode="Static"
                        CssClass="img-perfil"
                        AlternateText="Imagen de perfil" />

                    <!-- Cargar nueva imagen -->
                    <% if (btnGuardar.Visible == true)
                       { 
                    %>
                    <div class="mt-3">
                        <input type="file" id="fileImagenUsuario" runat="server"
                            class="input-imagen-perfil"
                            onchange="mostrarVistaPrevia(this, 'imgPerfil')" />

                    </div>
                    <%
                       } 
                    %>

                    <!-- Tipo de usuario (solo lectura) -->
                    <div class="mt-3">
                        <asp:Label ID="lblTipoUsuarioTitulo" runat="server"
                            Text="Tipo de usuario:"
                            CssClass="etiqueta-tipo-usuario"></asp:Label>
                        <asp:Label ID="lblTipoUsuario" runat="server"
                            CssClass="valor-tipo-usuario"></asp:Label>
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
                                CssClass="chk-mostrar-password"
                                AutoPostBack="true"
                                OnCheckedChanged="chkMostrarPassword_CheckedChanged" />

                            <asp:Button ID="btnCambiarPassword" runat="server"
                                Text="Cambiar contraseña"
                                CssClass="btn btn-outline-light btn-sm btn-cambiar-password"
                                OnClick="btnCambiarPassword_Click" />
                        </div>
                    </div>

                    <!-- Nueva contraseña + repetir  -->
                    <div id="bloqueCambioPassword">
                        <div class="form-group">
                            <asp:Label ID="lblNuevaPass" runat="server"
                                Text="Nueva contraseña"
                                Visible="false"
                                CssClass="form-label" />
                            <asp:TextBox ID="txtNuevaPassword" Visible="false" runat="server"
                                CssClass="form-control"
                                TextMode="Password"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblRepetirPass" runat="server"
                                Text="Repetir contraseña"
                                Visible="false"
                                CssClass="form-label" />
                            <asp:TextBox ID="txtRepetirPassword" Visible="false" runat="server"
                                CssClass="form-control"
                                TextMode="Password"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Botones de acción -->
                    <div class="acciones-perfil">
                        <asp:Button ID="btnEditar" runat="server"
                            Text="Editar perfil"
                            CssClass="btn btn-outline-primary"
                            OnClick="btnEditar_Click" />
                        <asp:Button ID="btnGuardar" runat="server"
                            Text="Guardar cambios"
                            Visible="false"
                            CssClass="btn btn-primary me-2"
                            OnClick="btnGuardar_Click" />

                        <asp:Button ID="btnCancelar" runat="server"
                            Text="Cancelar"
                            Visible="false"
                            CssClass="btn btn-outline-light"
                            OnClick="btnCancelar_Click" />
                    </div>
                    <asp:Label ID="lblMensajeError" runat="server"
                        Text=""
                        Visible="false"
                        CssClass="form-label" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
