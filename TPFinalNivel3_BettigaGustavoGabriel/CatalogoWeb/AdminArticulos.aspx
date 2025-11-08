<%@ Page Title="Administracion de Articulos" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="AdminArticulos.aspx.cs" Inherits="CatalogoWeb.AdminArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/AdminArticulos.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="titulo-admin-articulos">Administración de Artículos</h1>

    <asp:GridView
    ID="dgvArticulos"
    runat="server"
    CssClass="table table-dark table-striped table-hover tabla-admin-articulos"
    DataKeyNames="Id"
    AutoGenerateColumns="false"
    AllowPaging="true"
    PageSize="2"    
    OnPageIndexChanging="dgvArticulos_PageIndexChanging"
    OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged">

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
        <asp:CommandField HeaderText="Acción"
                          ShowSelectButton="true"
                          SelectText="✏️" />
    </Columns>

    <PagerStyle CssClass="pager-admin" HorizontalAlign="Center" />
</asp:GridView>

</asp:Content>
