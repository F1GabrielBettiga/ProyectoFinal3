<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="CatalogoWeb.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/MiPerfil.css" />
    <script src="<%: ResolveUrl("~/Scripts/perfil.js?v=" + DateTime.Now.Ticks) %>"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="titulo-perfil">Mi Perfil</h1>

    <div class="perfil-container">
        <div class="perfil-card">
            <div class="row">

                <!-- ===========================
                     COLUMNA IZQUIERDA
                ============================ -->
                <div class="col-md-4 col-sm-12 perfil-col-izquierda">

                    <!-- Imagen de perfil -->
                    <asp:Image ID="imgPerfil" runat="server"
                        ClientIDMode="Static"
                        CssClass="img-perfil"
                        AlternateText="Imagen de perfil" />

                    <!-- Cargar nueva imagen -->
                    <% if (btnGuardar.Visible == true)
                        { %>
                    <div class="mt-3">
                        <input type="file" id="fileImagenUsuario" runat="server"
                            class="input-imagen-perfil"
                            onchange="mostrarVistaPrevia(this, 'imgPerfil')" />
                    </div>
                    <% } %>

                    <!-- Tipo de usuario -->
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
                ============================ -->
                <div class="col-md-8 col-sm-12 perfil-col-derecha">

                    <!-- Nombre -->
                    <div class="form-group">
                        <label>Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server"
                            CssClass="form-control"
                            MaxLength="50"></asp:TextBox>

                        <asp:RegularExpressionValidator
                            ID="revNombrePerfil"
                            runat="server"
                            ControlToValidate="txtNombre"
                            CssClass="text-danger"
                            ErrorMessage="El nombre debe tener solo letras y al menos 4 caracteres."
                            ValidationExpression="^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]{4,}$"
                            Display="Dynamic">
                        </asp:RegularExpressionValidator>
                    </div>

                    <!-- Apellido -->
                    <div class="form-group">
                        <label>Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server"
                            CssClass="form-control"
                            MaxLength="50"></asp:TextBox>

                        <asp:RegularExpressionValidator
                            ID="revApellidoPerfil"
                            runat="server"
                            ControlToValidate="txtApellido"
                            CssClass="text-danger"
                            ErrorMessage="El apellido debe tener solo letras y al menos 3 caracteres."
                            ValidationExpression="^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]{3,}$"
                            Display="Dynamic">
                        </asp:RegularExpressionValidator>
                    </div>

                    <!-- Email -->
                    <div class="form-group">
                        <label>Email</label>

                        <asp:TextBox ID="txtEmail" runat="server"
                            TextMode="Email"
                            CssClass="form-control"
                            MaxLength="100"></asp:TextBox>

                        <asp:RequiredFieldValidator
                            ID="rfvEmailPerfil"
                            runat="server"
                            ControlToValidate="txtEmail"
                            CssClass="text-danger"
                            ErrorMessage="El email es obligatorio."
                            Display="Dynamic"
                            ValidationGroup="Perfil">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator
                            ID="revEmailPerfil"
                            runat="server"
                            ControlToValidate="txtEmail"
                            CssClass="text-danger"
                            ErrorMessage="Ingresá un correo electrónico válido."
                            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                            Display="Dynamic"
                            ValidationGroup="Perfil">
                        </asp:RegularExpressionValidator>
                    </div>

                    <!-- Contraseña actual -->
                    <div class="form-group">
                        <label>Contraseña</label>

                        <div class="input-group">
                            <asp:TextBox ID="txtPassword" runat="server"
                                CssClass="form-control"
                                TextMode="Password"
                                ClientIDMode="Static"
                                MaxLength="20"></asp:TextBox>

                            <span class="input-group-text bg-dark text-light">
                                <asp:CheckBox ID="chkMostrarPassword" runat="server"
                                    onclick="togglePassword('txtPassword', this)" />
                                <span class="ms-1">👁️</span>
                            </span>
                        </div>

                        <asp:Button ID="btnCambiarPassword" runat="server"
                            Text="Cambiar contraseña"
                            CssClass="btn btn-outline-light btn-sm mt-2"
                            OnClick="btnCambiarPassword_Click" />
                    </div>

                    <!-- Nueva contraseña + repetir -->
                    <div id="bloqueCambioPassword">

                        <!-- Nueva contraseña -->
                        <div class="form-group">
                            <asp:Label ID="lblNuevaPass" runat="server"
                                Text="Nueva contraseña"
                                Visible="false"
                                CssClass="form-label" />

                            <div class="input-group">
                                <asp:TextBox ID="txtNuevaPassword" runat="server"
                                    CssClass="form-control"
                                    TextMode="Password"
                                    ClientIDMode="Static"
                                    Visible="false"
                                    MaxLength="20"></asp:TextBox>

                                <span id="spanMostrarNuevaPass" runat="server"
                                    class="input-group-text bg-dark text-light"
                                    visible="false">
                                    <asp:CheckBox ID="chkMostrarNuevaPass" runat="server"
                                        onclick="togglePassword('txtNuevaPassword', this)" />
                                    <span class="ms-1">👁️</span>
                                </span>
                            </div>

                            <asp:RegularExpressionValidator
                                ID="revNuevaPassStrong"
                                runat="server"
                                ControlToValidate="txtNuevaPassword"
                                CssClass="text-danger"
                                Display="Dynamic"
                                Visible="false"
                                ErrorMessage="La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número."
                                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$">
                            </asp:RegularExpressionValidator>
                        </div>

                        <!-- Repetir contraseña -->
                        <div class="form-group">
                            <asp:Label ID="lblRepetirPass" runat="server"
                                Text="Repetir contraseña"
                                Visible="false"
                                CssClass="form-label" />

                            <div class="input-group">
                                <asp:TextBox ID="txtRepetirPassword" runat="server"
                                    CssClass="form-control"
                                    TextMode="Password"
                                    ClientIDMode="Static"
                                    Visible="false"
                                    MaxLength="20"></asp:TextBox>

                                <span id="spanMostrarRepetirPass" runat="server"
                                    class="input-group-text bg-dark text-light"
                                    visible="false">
                                    <asp:CheckBox ID="chkMostrarRepetirPass" runat="server"
                                        onclick="togglePassword('txtRepetirPassword', this)" />
                                    <span class="ms-1">👁️</span>
                                </span>
                            </div>

                            <asp:CompareValidator
                                ID="cvRepetirPassword"
                                runat="server"
                                ControlToValidate="txtRepetirPassword"
                                ControlToCompare="txtNuevaPassword"
                                CssClass="text-danger"
                                Display="Dynamic"
                                Visible="false"
                                ErrorMessage="Las contraseñas no coinciden.">
                            </asp:CompareValidator>
                        </div>

                    </div>

                    <asp:Label ID="lblMensajeError" runat="server"
                        Text=""
                        Visible="false"
                        CssClass="registro-mensaje-error" />

                    <!-- Botones -->
                    <div class="acciones-perfil">

                        <asp:Button ID="btnEditar" runat="server"
                            Text="Editar perfil"
                            CssClass="btn btn-outline-primary"
                            OnClick="btnEditar_Click" />

                        <asp:Button ID="btnGuardar" runat="server"
                            Text="Guardar cambios"
                            Visible="false"
                            CssClass="btn btn-primary me-2"
                            ValidationGroup="Perfil"
                            OnClick="btnGuardar_Click"
                            OnClientClick="if (typeof(Page_ClientValidate) === 'function' && !Page_ClientValidate('Perfil')) return false; return confirm('¿Seguro que querés guardar los cambios del perfil?');" />

                        <asp:Button ID="btnCancelar" runat="server"
                            Text="Cancelar"
                            Visible="false"
                            CssClass="btn btn-outline-light"
                            OnClick="btnCancelar_Click"
                            OnClientClick="return confirm('¿Seguro que querés cancelar? Se perderán los cambios realizados.');" />

                    </div>

                </div>
            </div>

        </div>
    </div>

</asp:Content>
