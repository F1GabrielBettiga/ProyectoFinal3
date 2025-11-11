<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CatalogoWeb.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/Login.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container login-container">
        <div class="login-card ">
            <!-- Título -->
            <h2 class="login-title text-center mb-2">Iniciá sesión</h2>
            <p class="login-subtitle text-center mb-4">Hola 👋 Por favor, ingresá tus datos para continuar</p>

            <!-- Campo Email -->
            <div class="mb-3">
                <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico" CssClass="form-label text-light" />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" Placeholder="ejemplo@email.com" />
            </div>

            <!-- Campo Contraseña -->
            <div class="mb-3">
                <asp:Label ID="lblPassword" runat="server" Text="Contraseña" CssClass="form-label text-light" />
                <div class="input-group">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="••••••••" />
                    <span class="input-group-text bg-dark text-light">
                        <asp:CheckBox ID="chkMostrarPass" runat="server" AutoPostBack="true" OnCheckedChanged="chkMostrarPass_CheckedChanged" />
                        <span class="ms-1">👁️</span>
                    </span>
                </div>
            </div>

            <!-- Mensaje de error -->
            <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block text-center mb-3" Visible="false" />

            <!-- Botones -->
            <div class="d-grid mb-3">
                <asp:Button ID="btnLogin" runat="server" Text="Iniciar sesión" CssClass="btn btn-primary btn-lg" OnClick="btnLogin_Click" />
            </div>

            <div class="text-center">
                <span class="text-light">¿No tenés cuenta?</span>
                <asp:Button ID="btnCrearCuenta" runat="server" Text="Crear cuenta" CssClass="btn btn-outline-light btn-sm ms-2" OnClick="btnCrearCuenta_Click" />
            </div>
        </div>
    </div>


</asp:Content>
