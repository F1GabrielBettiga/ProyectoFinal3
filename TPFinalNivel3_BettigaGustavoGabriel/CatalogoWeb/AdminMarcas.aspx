<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="AdminMarcas.aspx.cs" Inherits="CatalogoWeb.AdminMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/AdminMarcas.css?v=1" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header-admin-marcas">
        <h1 class="titulo-admin-marcas">Administración de Marcas</h1>

        <asp:Button
            ID="btnNuevaMarca"
            runat="server"
            Text="Crear marca"
            CssClass="btn-crear-marca"
            OnClick="btnNuevaMarca_Click" />
    </div>

    <!-- ===========================
         BUSCADOR DE MARCAS
         =========================== -->
    <asp:Panel runat="server" DefaultButton="btnBuscarMarca">
        <div class="buscador-admin-marcas">
            <asp:TextBox
                ID="txtBuscarMarcas"
                runat="server"
                CssClass="input-buscar-marcas"
                placeholder="Buscar marca..." />

            <asp:Button
                ID="btnBuscarMarca"
                runat="server"
                Text="Buscar"
                CssClass="btn-buscar-marca"
                OnClick="btnBuscarMarca_Click" />

            <asp:Button
                ID="btnBorrarMarca"
                runat="server"
                Text="Borrar"
                CssClass="btn-limpiar-busqueda-marcas"
                OnClick="btnBorrarMarca_Click" />
        </div>
    </asp:Panel>

    <!-- MENSAJE CUANDO NO HAY REGISTROS -->
    <asp:Label
        ID="lblSinArticulos"
        runat="server"
        Visible="false"
        CssClass="mensaje-sin-articulos"
        Text="No hay marcas cargadas todavía.">
    </asp:Label>

    <asp:GridView
        ID="dgvMarcas"
        runat="server"
        CssClass="tabla-admin-marcas"
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
                        CommandArgument='<%# Eval("Id") %>'
                        OnClientClick="return confirm('¿Seguro que querés eliminar este registro? Esta acción no se puede deshacer.');" />

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <PagerStyle CssClass="pager-admin" HorizontalAlign="Center" />
    </asp:GridView>

</asp:Content>
