<%@ Page Title="Administracion de Usuarios" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="AdminUsuarios.aspx.cs" Inherits="CatalogoWeb.AdminUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" type="text/css" href="Css/AdminUsuarios.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header-admin-usuarios">
        <h1 class="titulo-admin-usuarios">Administración de Usuarios</h1>

        <asp:Button
            ID="btnNuevoUsuario"
            runat="server"
            Text="Crear usuario"
            CssClass="btn-crear-usuario"
            OnClick="btnNuevoUsuario_Click" />
    </div>

    <!-- ===========================
         BUSCADOR DE USUARIOS
         =========================== -->
    <div class="buscador-admin-usuarios">
        <asp:TextBox
            ID="txtBuscarUsuario"
            runat="server"
            CssClass="input-buscar-usuarios"
            Placeholder="Buscar por nombre, apellido, email..." />

        <asp:Button
            ID="btnBuscarUsuario"
            runat="server"
            Text="Buscar"
            CssClass="btn-buscar-usuario"
            OnClick="btnBuscarUsuario_Click" />

        <asp:Button
            ID="btnLimpiarBusquedaUsuario"
            runat="server"
            Text="Borrar"
            CssClass="btn-borrar-usuario"
            OnClick="btnLimpiarBusquedaUsuario_Click" />
    </div>

    <!-- ===========================
         TABLA ADMINISTRAR USUARIOS
         =========================== -->

    <asp:GridView
        ID="dgvUsuarios"
        runat="server"
        CssClass="tabla-admin-usuarios"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="4"
        OnPageIndexChanging="dgvUsuarios_PageIndexChanging"
        OnRowCommand="dgvUsuarios_RowCommand">

        <Columns>
            <asp:BoundField HeaderText="Id" DataField="id" />

            <asp:BoundField HeaderText="Nombre" DataField="nombre" />

            <asp:BoundField HeaderText="Apellido" DataField="apellido" />

            <asp:BoundField HeaderText="Email" DataField="email" />

            <asp:TemplateField HeaderText="Usuario">
                <ItemTemplate>
                    <%# (bool)Eval("esAdmin") ? "Admin" : "Estándar" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acción">
                <ItemTemplate>
                    <asp:Button runat="server"
                        Text="Editar"
                        CssClass="btn btn-primary btn-sm"
                        CommandName="Editar"
                        CommandArgument='<%# Eval("Id") %>' />

                    <asp:Button runat="server"
                        Text="Borrar"
                        CssClass="btn btn-danger btn-sm"
                        CommandName="Borrar"
                        CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <PagerStyle CssClass="pager-admin" HorizontalAlign="Center" />
    </asp:GridView>

</asp:Content>
