<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="AdminMarcas.aspx.cs" Inherits="CatalogoWeb.AdminMarcas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/AdminMarcas.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h1 class="titulo-admin-marcas">Administración de marcas</h1>

<asp:GridView
    ID="dgvMarcas"
    runat="server"
    CssClass=" tabla-admin-marcas"
    DataKeyNames="Id"
    AutoGenerateColumns="false"
    AllowPaging="true"
    PageSize="4"
    OnPageIndexChanging="dgvMarcas_PageIndexChanging"       
    OnRowCommand="dgvMarcas_RowCommand">

    <Columns>
        <%-- Id --%>
        <asp:BoundField HeaderText="ID" DataField="id" />

        <%-- Descripción --%>
        <asp:BoundField HeaderText="Descripción" DataField="descripcion" />

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
