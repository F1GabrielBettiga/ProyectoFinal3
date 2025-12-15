<%@ Page Title="ABM Usuarios" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="ABM_Usuarios.aspx.cs" Inherits="CatalogoWeb.ABM_Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%: ResolveUrl("~/Scripts/perfil.js") %>"></script>
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
            <asp:Panel ID="pnlIdUsuario" runat="server" Visible="false" CssClass="fila-id-usuario">
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

            <!-- LAYOUT PRINCIPAL -->
            <div class="row g-3">

                <!-- COLUMNA IZQUIERDA -->
                <div class="col-md-7">

                    <!-- Nombre -->
                    <div class="mb-3">
                        <asp:Label ID="lblNombre" runat="server"
                            Text="Nombre"
                            CssClass="form-label" />
                        <asp:TextBox ID="txtNombre" runat="server"
                            CssClass="form-control"
                            MaxLength="50" />
                        <asp:RegularExpressionValidator
                            ID="revNombreSoloLetras"
                            runat="server"
                            ControlToValidate="txtNombre"
                            CssClass="text-danger"
                            ErrorMessage="Solo se permiten letras."
                            Display="Dynamic"
                            ValidationExpression="^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]+$">
                        </asp:RegularExpressionValidator>
                    </div>

                    <!-- Apellido -->
                    <div class="mb-3">
                        <asp:Label ID="lblApellido" runat="server"
                            Text="Apellido"
                            CssClass="form-label" />
                        <asp:TextBox ID="txtApellido" runat="server"
                            CssClass="form-control"
                            MaxLength="50" />
                        <asp:RegularExpressionValidator
                            ID="revApellidoSoloLetras"
                            runat="server"
                            ControlToValidate="txtApellido"
                            CssClass="text-danger"
                            ErrorMessage="Solo se permiten letras."
                            Display="Dynamic"
                            ValidationExpression="^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]+$">
                        </asp:RegularExpressionValidator>
                    </div>

                    <!-- Email -->
                    <div class="mb-3">
                        <asp:Label ID="lblEmail" runat="server"
                            Text="Correo electrónico"
                            CssClass="form-label" />
                        <asp:TextBox ID="txtEmail" runat="server"
                            CssClass="form-control"
                            TextMode="Email"
                            MaxLength="100" />


                        <asp:RequiredFieldValidator
                            ID="rfvEmail"
                            runat="server"
                            ControlToValidate="txtEmail"
                            CssClass="text-danger"
                            ErrorMessage="El correo electrónico es obligatorio."
                            Display="Dynamic"
                            ValidationGroup="UsuarioABM">
                        </asp:RequiredFieldValidator>


                        <asp:RegularExpressionValidator
                            ID="revEmail"
                            runat="server"
                            ControlToValidate="txtEmail"
                            CssClass="text-danger"
                            ErrorMessage="Ingrese un correo electrónico válido."
                            Display="Dynamic"
                            ValidationGroup="UsuarioABM"
                            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$">
                        </asp:RegularExpressionValidator>
                    </div>

                    <!-- Contraseña -->
                    <div class="mb-3">
                        <asp:Label ID="lblPassword" runat="server"
                            Text="Contraseña"
                            CssClass="form-label" />

                        <div class="input-group">
                            <asp:TextBox ID="txtPassword" runat="server"
                                CssClass="form-control"
                                TextMode="Password"
                                ClientIDMode="Static"
                                MaxLength="20" />

                            <span class="input-group-text bg-dark text-light">
                                <asp:CheckBox ID="chkMostrarPass" runat="server"
                                    onclick="togglePassword('txtPassword', this)" />
                                <span class="ms-1">👁️</span>
                            </span>
                        </div>


                        <asp:RequiredFieldValidator
                            ID="rfvPassword"
                            runat="server"
                            ControlToValidate="txtPassword"
                            CssClass="text-danger"
                            ErrorMessage="La contraseña es obligatoria."
                            Display="Dynamic"
                            ValidationGroup="UsuarioABM">
                        </asp:RequiredFieldValidator>


                        <asp:RegularExpressionValidator
                            ID="revPassStrong"
                            runat="server"
                            ControlToValidate="txtPassword"
                            CssClass="text-danger"
                            Display="Dynamic"
                            ValidationGroup="UsuarioABM"
                            ErrorMessage="La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número."
                            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$">
                        </asp:RegularExpressionValidator>
                    </div>

                </div>

                <!-- COLUMNA DERECHA -->
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
                                ClientIDMode="Static"
                                Display="Dynamic"
                                ImageUrl="~/Images/no-user.jpg"
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
                            class="form-control"
                            onchange="mostrarVistaPrevia(this, 'imgUsuario')" />
                    </div>

                </div>
            </div>
            <asp:Label ID="lblError" runat="server"
                Text=""
                Visible="false"
                CssClass="registro-mensaje-error" />
            <!-- BOTONES -->
            <div class="abm-usuario-botones text-center mt-4">

                <asp:Button ID="btnGuardar" runat="server"
                    Text="Crear"
                    CssClass="btn btn-primary me-2"
                    ValidationGroup="UsuarioABM"
                    OnClick="btnGuardar_Click"
                    OnClientClick="if (typeof(Page_ClientValidate) === 'function' && !Page_ClientValidate('UsuarioABM')) return false; return confirm('¿Seguro que querés guardar el usuario?');" />

                <asp:Button ID="btnCancelar" runat="server"
                    Text="Cancelar"
                    CssClass="btn btn-outline-light"
                    OnClick="btnCancelar_Click"
                    OnClientClick="return confirm('¿Seguro que querés cancelar? Se perderán los cambios.');" />

            </div>

        </div>

    </div>

</asp:Content>
