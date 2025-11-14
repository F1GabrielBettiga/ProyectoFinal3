<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="CatalogoWeb.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/Registro.css" />
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

            <!-- NOMBRE -->
            <div class="mb-3">
                <asp:Label ID="lblNombre" runat="server"
                    Text="Nombre"
                    CssClass="form-label" />
                <asp:TextBox ID="txtNombre" runat="server"
                    CssClass="form-control"
                    Placeholder="Escribí tu nombre" />
            </div>

            <!-- APELLIDO -->
            <div class="mb-3">
                <asp:Label ID="lblApellido" runat="server"
                    Text="Apellido"
                    CssClass="form-label" />
                <asp:TextBox ID="txtApellido" runat="server"
                    CssClass="form-control"
                    Placeholder="Escribí tu apellido" />
            </div>

            <!-- EMAIL -->
            <div class="mb-3">
                <asp:Label ID="lblEmail" runat="server"
                    Text="Correo electrónico"
                    CssClass="form-label" />
                <asp:TextBox ID="txtEmail" runat="server"
                    CssClass="form-control"
                    TextMode="Email"
                    Placeholder="tucorreo@ejemplo.com" />
            </div>

            <!-- CONTRASEÑA -->
            <div class="mb-3">
                <asp:Label ID="lblPassword" runat="server"
                    Text="Contraseña"
                    CssClass="form-label" />
                <asp:TextBox ID="txtPassword" runat="server"
                    CssClass="form-control"
                    TextMode="Password"
                    Placeholder="Escribí una contraseña" />
            </div>

            <!-- REPETIR CONTRASEÑA -->
            <div class="mb-3">
                <asp:Label ID="lblRepetirPassword" runat="server"
                    Text="Repetir contraseña"
                    CssClass="form-label" />
                <asp:TextBox ID="txtRepetirPassword" runat="server"
                    CssClass="form-control"
                    TextMode="Password"
                    Placeholder="Repetí la contraseña" />
            </div>

            <!-- BOTONES -->
            <div class="registro-botones d-flex gap-2 mt-2">
                <asp:Button ID="btnCrearCuenta" runat="server"
                    Text="Crear cuenta"
                    CssClass="btn btn-primary flex-fill"
                    OnClick="btnCrearCuenta_Click" />
                <asp:Button ID="btnCancelar" runat="server"
                    Text="Cancelar"
                    CssClass="btn btn-outline-light flex-fill"
                    PostBackUrl="~/Default.aspx" />
            </div>

            <!-- MENSAJE DE ERROR (opcional) -->
            <asp:Label ID="lblErrorRegistro" runat="server"
                CssClass="registro-mensaje-error"
                Visible="false" />

        </div>

        <!-- FOOTER: YA TENÉS CUENTA -->
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
