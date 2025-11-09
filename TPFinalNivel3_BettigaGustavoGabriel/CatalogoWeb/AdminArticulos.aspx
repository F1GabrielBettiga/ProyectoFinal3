<%@ Page Title="Administracion de Articulos" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="AdminArticulos.aspx.cs" Inherits="CatalogoWeb.AdminArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/AdminArticulos.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="titulo-admin-articulos">Administración de Artículos</h1>

    <asp:GridView
        ID="dgvArticulos"
        runat="server"
        CssClass=" tabla-admin-articulos"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="4"
        OnPageIndexChanging="dgvArticulos_PageIndexChanging"       
        OnRowCommand="dgvArticulos_RowCommand">

        <Columns>
            <%-- Código --%>
            <asp:BoundField HeaderText="Código" DataField="codigo" />

            <%-- Nombre --%>
            <asp:BoundField HeaderText="Nombre" DataField="nombre" />

            <%-- Descripción --%>
            <asp:BoundField HeaderText="Descripción" DataField="descripcion" />

            <%-- Marca --%>
            <asp:TemplateField HeaderText="Marca">
                <ItemTemplate>
                    <%# Eval("marca.descripcion") %>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- Categoría  --%>
            <asp:TemplateField HeaderText="Categoría">
                <ItemTemplate>
                    <%# Eval("categoria.descripcion") %>
                </ItemTemplate>
            </asp:TemplateField>

            <%-- URL imagen --%>
            <asp:BoundField HeaderText="URL Imagen" DataField="imagenUrl" />

            <%-- Precio --%>
            <asp:BoundField HeaderText="Precio"
                DataField="precio"
                DataFormatString="${0:N2}"
                HtmlEncode="false" />

            <%-- Acción --%>
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
