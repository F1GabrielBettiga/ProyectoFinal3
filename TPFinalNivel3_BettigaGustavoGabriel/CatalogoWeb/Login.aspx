<%@ Page Title="Login" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CatalogoWeb.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/Login.css" />
    <script src="<%: ResolveUrl("~/Scripts/perfil.js?v=" + DateTime.Now.Ticks) %>"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container login-container">
        <div class="login-card ">
            <!-- Título -->
            <h2 class="login-title text-center mb-2">Iniciá sesión</h2>
            <p class="login-subtitle text-center mb-4">Hola 👋 Por favor, ingresá tus datos para continuar</p>

            <asp:Panel runat="server" DefaultButton="btnLogin">

                <!-- Campo Email -->
                <div class="mb-3">
                    <asp:Label ID="lblEmail" runat="server"
                        Text="Correo electrónico"
                        CssClass="form-label text-light" />

                    <asp:TextBox ID="txtEmail" runat="server"
                        CssClass="form-control"
                        TextMode="Email"
                        MaxLength="100"
                        Placeholder="ejemplo@email.com" />

                    <asp:RequiredFieldValidator
                        ID="rfvEmail"
                        runat="server"
                        ControlToValidate="txtEmail"
                        CssClass="text-danger"
                        ErrorMessage="El correo es obligatorio."
                        Display="Dynamic"
                        ValidationGroup="LoginGroup" />

                    <asp:RegularExpressionValidator
                        ID="revEmail"
                        runat="server"
                        ControlToValidate="txtEmail"
                        CssClass="text-danger"
                        ErrorMessage="Ingrese un correo electrónico válido."
                        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                        Display="Dynamic"
                        ValidationGroup="LoginGroup" />
                </div>

                <!-- Campo Contraseña -->
                <div class="mb-3">
                    <asp:Label ID="lblPassword" runat="server"
                        Text="Contraseña"
                        CssClass="form-label text-light" />

                    <div class="input-group">
                        <asp:TextBox ID="txtPassword" runat="server"
                            CssClass="form-control"
                            TextMode="Password"
                            ClientIDMode="Static"
                            MaxLength="20"
                            Placeholder="••••••••" />

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
                        ValidationGroup="LoginGroup" />
                </div>
                <!-- Mensaje de error --> <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block text-center mb-3" Visible="false" />
                <!-- Botón -->
                <div class="d-grid mb-3">
                    <asp:Button ID="btnLogin" runat="server"
                        Text="Iniciar sesión"
                        CssClass="btn btn-primary btn-lg"
                        OnClick="btnLogin_Click"
                        ValidationGroup="LoginGroup" />
                </div>

            </asp:Panel>

            <div class="text-center">
                <span class="text-light">¿No tenés cuenta?</span>
                <asp:Button ID="btnCrearCuenta" runat="server"
                    Text="Crear cuenta"
                    CssClass="btn btn-outline-light btn-sm ms-2"
                    OnClick="btnCrearCuenta_Click" />
            </div>
        </div>
    </div>

</asp:Content>
