<%@ Page Title="Administracion de Categorias" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="AdminCategorias.aspx.cs" Inherits="CatalogoWeb.AdminCategorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="Css/AdminCategorias.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="titulo-admin-categorias">Administración de Categorias</h1>

<asp:GridView
    ID="dgvCategorias"
    runat="server"
    CssClass=" tabla-admin-categorias"
    DataKeyNames="Id"
    AutoGenerateColumns="false"
    AllowPaging="true"
    PageSize="4"
    OnPageIndexChanging="dgvCategorias_PageIndexChanging"       
    OnRowCommand="dgvCategorias_RowCommand">

    <Columns>
        <%-- Id --%>
        <asp:BoundField HeaderText="ID" DataField="id" />

        <%-- Descripción --%>
        <asp:BoundField HeaderText="Categorias" DataField="descripcion" />

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
