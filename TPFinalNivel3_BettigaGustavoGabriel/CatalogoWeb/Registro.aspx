<%@ Page Title="Registrarse" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="CatalogoWeb.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/Registro.css" />
    <script src="<%: ResolveUrl("~/Scripts/perfil.js?v=" + DateTime.Now.Ticks) %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- CONTENEDOR GENERAL -->
    <div class="registro-container">

        <!-- TARJETA REGISTRO -->
        <div class="registro-card">

            <!-- TÍTULO Y SUBTÍTULO -->
            <h2 class="registro-title">Crear cuenta</h2>
            <p class="registro-subtitle">
                Completá tus datos para registrarte en el catálogo.
            </p>
             <asp:Panel runat="server" DefaultButton="btnCrearCuenta">
            <!-- NOMBRE -->
            <div class="mb-3">
                <asp:Label ID="lblNombre" runat="server"
                    Text="Nombre"
                    CssClass="form-label" />

                <asp:TextBox ID="txtNombre" runat="server"
                    CssClass="form-control"
                    Placeholder="Escribí tu nombre"
                    MaxLength="50" />

                <asp:RequiredFieldValidator
                    ID="rfvNombre"
                    runat="server"
                    ControlToValidate="txtNombre"
                    CssClass="text-danger"
                    ErrorMessage="El nombre es obligatorio."
                    Display="Dynamic"
                    ValidationGroup="Registro">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator
                    ID="revNombreSoloLetrasMin4"
                    runat="server"
                    ControlToValidate="txtNombre"
                    CssClass="text-danger"
                    ErrorMessage="El nombre debe tener solo letras y al menos 4 caracteres."
                    ValidationExpression="^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]{4,}$"
                    Display="Dynamic"
                    ValidationGroup="Registro">
                </asp:RegularExpressionValidator>
            </div>

            <!-- APELLIDO -->
            <div class="mb-3">
                <asp:Label ID="lblApellido" runat="server"
                    Text="Apellido"
                    CssClass="form-label" />

                <asp:TextBox ID="txtApellido" runat="server"
                    CssClass="form-control"
                    Placeholder="Escribí tu apellido"
                    MaxLength="50" />

                <asp:RequiredFieldValidator
                    ID="rfvApellido"
                    runat="server"
                    ControlToValidate="txtApellido"
                    CssClass="text-danger"
                    ErrorMessage="El apellido es obligatorio."
                    Display="Dynamic"
                    ValidationGroup="Registro">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator
                    ID="revApellidoSoloLetrasMin3"
                    runat="server"
                    ControlToValidate="txtApellido"
                    CssClass="text-danger"
                    ErrorMessage="El apellido debe tener solo letras y al menos 3 caracteres."
                    ValidationExpression="^[a-zA-ZÁÉÍÓÚáéíóúñÑ ]{3,}$"
                    Display="Dynamic"
                    ValidationGroup="Registro">
                </asp:RegularExpressionValidator>
            </div>

            <!-- EMAIL -->
            <div class="mb-3">
                <asp:Label ID="lblEmail" runat="server"
                    Text="Correo electrónico"
                    CssClass="form-label" />

                <asp:TextBox ID="txtEmail" runat="server"
                    CssClass="form-control"
                    TextMode="Email"
                    MaxLength="100"
                    Placeholder="tucorreo@ejemplo.com" />

                <asp:RequiredFieldValidator
                    ID="rfvEmail"
                    runat="server"
                    ControlToValidate="txtEmail"
                    CssClass="text-danger"
                    ErrorMessage="El correo electrónico es obligatorio."
                    Display="Dynamic"
                    ValidationGroup="Registro">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator
                    ID="revEmailFormato"
                    runat="server"
                    ControlToValidate="txtEmail"
                    CssClass="text-danger"
                    ErrorMessage="Ingresá un correo electrónico válido."
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                    Display="Dynamic"
                    ValidationGroup="Registro">
                </asp:RegularExpressionValidator>
            </div>

            <!-- CONTRASEÑA -->
            <div class="mb-3">
                <asp:Label ID="lblPassword" runat="server"
                    Text="Contraseña"
                    CssClass="form-label" />

                <div class="input-group">
                    <asp:TextBox ID="txtPassword" runat="server"
                        CssClass="form-control"
                        TextMode="Password"
                        ClientIDMode="Static"
                        Placeholder="Escribí una contraseña"
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
                    ValidationGroup="Registro">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator
                    ID="revPassStrong"
                    runat="server"
                    ControlToValidate="txtPassword"
                    CssClass="text-danger"
                    Display="Dynamic"
                    ErrorMessage="Debe tener al menos 8 caracteres, una mayúscula, una minúscula y un número."
                    ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"
                    ValidationGroup="Registro" />
            </div>

            <!-- REPETIR CONTRASEÑA -->
            <div class="mb-3">
                <asp:Label ID="lblRepetirPassword" runat="server"
                    Text="Repetir contraseña"
                    CssClass="form-label" />

                <div class="input-group">
                    <asp:TextBox ID="txtRepetirPassword" runat="server"
                        CssClass="form-control"
                        TextMode="Password"
                        ClientIDMode="Static"
                        Placeholder="Repetí la contraseña"
                        MaxLength="20" />

                    <span class="input-group-text bg-dark text-light">
                        <asp:CheckBox ID="chkMostrarRepetirPass" runat="server"
                            onclick="togglePassword('txtRepetirPassword', this)" />
                        <span class="ms-1">👁️</span>
                    </span>
                </div>

                <asp:RequiredFieldValidator
                    ID="rfvRepetirPassword"
                    runat="server"
                    ControlToValidate="txtRepetirPassword"
                    CssClass="text-danger"
                    ErrorMessage="Debés repetir la contraseña."
                    Display="Dynamic"
                    ValidationGroup="Registro">
                </asp:RequiredFieldValidator>

                <asp:CompareValidator
                    ID="cvRepetirPassword"
                    runat="server"
                    ControlToValidate="txtRepetirPassword"
                    ControlToCompare="txtPassword"
                    ErrorMessage="Las contraseñas no coinciden."
                    CssClass="text-danger"
                    Display="Dynamic"
                    ValidationGroup="Registro">
                </asp:CompareValidator>
            </div>

            <!-- BOTONES -->
            <div class="registro-botones d-flex gap-2 mt-2">
                <asp:Button ID="btnCrearCuenta" runat="server"
                    Text="Crear cuenta"
                    CssClass="btn btn-primary flex-fill"
                    OnClick="btnCrearCuenta_Click"
                    ValidationGroup="Registro" />
                <asp:Button ID="btnCancelar" runat="server"
                    Text="Cancelar"
                    CssClass="btn btn-outline-light flex-fill"
                    PostBackUrl="~/Default.aspx" />
            </div>



            <!-- MENSAJE DE ERROR  -->
            <asp:Label ID="lblErrorRegistro" runat="server"
                CssClass="registro-mensaje-error"
                Visible="false" />
</asp:Panel>
        </div>
         

        <!-- FOOTER -->
        <div class="registro-footer">
            ¿Ya tenés una cuenta?
            <asp:HyperLink ID="lnkIrLogin" runat="server"
                NavigateUrl="~/Login.aspx"
                CssClass="link-ir-login">
                Iniciá sesión
            </asp:HyperLink>
        </div>

    </div>

</asp:Content>
