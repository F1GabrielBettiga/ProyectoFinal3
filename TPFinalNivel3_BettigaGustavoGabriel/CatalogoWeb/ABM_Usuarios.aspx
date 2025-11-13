<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="ABM_Usuarios.aspx.cs" Inherits="CatalogoWeb.ABM_Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/ABM_Usuarios.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- CONTENEDOR GENERAL -->
    <div class="abm-usuario-container">

        <!-- TÍTULO -->
        <asp:Label ID="lblTituloABMUsuario" runat="server"
                   Text="Nuevo usuario"
                   CssClass="titulo-abm-usuario" />

        <!-- TARJETA -->
        <div class="abm-usuario-card">

            <!-- FILA ID -->
            <asp:Panel ID="pnlIdUsuario" runat="server" CssClass="fila-id-usuario">
                <div class="row g-2 align-items-center">
                    <div class="col-auto">
                        <asp:Label ID="lblIdUsuario" runat="server"
                                   Text="ID"
                                   CssClass="form-label" />
                    </div>
                    <div class="col-auto">
                        <asp:TextBox ID="txtIdUsuario" runat="server"
                                     CssClass="form-control txt-id-usuario"
                                     ReadOnly="true" />
                    </div>
                </div>
            </asp:Panel>

            <!-- LAYOUT PRINCIPAL: DOS COLUMNAS -->
            <div class="row g-3">

                <!-- COLUMNA IZQUIERDA: DATOS PERSONALES -->
                <div class="col-md-7">

                    <!-- Nombre -->
                    <div class="mb-3">
                        <asp:Label ID="lblNombre" runat="server"
                                   Text="Nombre"
                                   CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server"
                                     CssClass="form-control" />
                    </div>

                    <!-- Apellido -->
                    <div class="mb-3">
                        <asp:Label ID="lblApellido" runat="server"
                                   Text="Apellido"
                                   CssClass="form-label" />
                        <asp:TextBox ID="txtApellido" runat="server"
                                     CssClass="form-control" />
                    </div>

                    <!-- Email -->
                    <div class="mb-3">
                        <asp:Label ID="lblEmail" runat="server"
                                   Text="Correo electrónico"
                                   CssClass="form-label" />
                        <asp:TextBox ID="txtEmail" runat="server"
                                     CssClass="form-control"
                                     TextMode="Email" />
                    </div>

                    <!-- Contraseña -->
                    <div class="mb-3">
                        <asp:Label ID="lblPassword" runat="server"
                                   Text="Contraseña"
                                   CssClass="form-label" />
                        <div class="input-group">
                            <asp:TextBox ID="txtPassword" runat="server"
                                         CssClass="form-control"
                                         TextMode="Password" />
                            <span class="input-group-text bg-dark text-light">
                                <asp:CheckBox ID="chkMostrarPass" runat="server"
                                              AutoPostBack="true"
                                              OnCheckedChanged="chkMostrarPass_CheckedChanged" />
                                <span class="ms-1">👁️</span>
                            </span>
                        </div>
                    </div>

                </div>

                <!-- COLUMNA DERECHA: ROL + IMAGEN -->
                <div class="col-md-5">

                    <!-- Rol -->
                    <div class="mb-3">
                        <asp:Label ID="lblRol" runat="server"
                                   Text="Rol de usuario"
                                   CssClass="form-label" />
                        <asp:DropDownList ID="ddlRol" runat="server"
                                          CssClass="form-select">
                            <asp:ListItem Text="Estándar" Value="false"></asp:ListItem>
                            <asp:ListItem Text="Administrador" Value="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <!-- Imagen actual -->
                    <div class="mb-3">
                        <asp:Label ID="lblImagenActual" runat="server"
                                   Text="Imagen de perfil"
                                   CssClass="form-label" />
                        <div class="abm-usuario-imagen-wrapper">
                            <asp:Image ID="imgUsuario" runat="server"
                                       CssClass="img-fluid abm-img-usuario"
                                       AlternateText="Imagen de usuario" />
                        </div>
                    </div>

                    <!-- Subir nueva imagen -->
                    <div class="mb-3">
                        <asp:Label ID="lblNuevaImagen" runat="server"
                                   Text="Nueva imagen de perfil"
                                   CssClass="form-label" />
                        <input type="file" id="fileImagenUsuario" runat="server"
                               class="form-control" />
                    </div>

                </div>
            </div>

            <!-- BOTONES DE ACCIÓN -->
            <div class="abm-usuario-botones text-center mt-4">
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
