<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="ABM_Categorias.aspx.cs" Inherits="CatalogoWeb.ABM_Categorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/ABM_Categorias.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="abm-categoria-container">

        <!-- Título dinámico: "Nueva categoría" / "Editar categoría" -->
        <asp:Label ID="lblTituloABMCategoria" runat="server"
                   Text="Nueva categoría"
                   CssClass="titulo-abm-categoria" />

        <!-- Tarjeta principal -->
        <div class="abm-categoria-card">

            <!-- Fila ID -->
            <asp:Panel ID="pnlIdCategoria" runat="server" CssClass="fila-id-categoria">
                <div class="mb-3">
                    <asp:Label ID="lblIdCategoria" runat="server"
                               Text="ID"
                               CssClass="form-label" />
                    <asp:TextBox ID="txtIdCategoria" runat="server"
                                 CssClass="form-control"
                                 ReadOnly="true" />
                </div>
            </asp:Panel>

            <!-- Nombre / descripción de la categoría -->
            <div class="mb-3">
                <asp:Label ID="lblNombreCategoria" runat="server"
                           Text="Nombre de la categoría"
                           CssClass="form-label" />
                <asp:TextBox ID="txtNombreCategoria" runat="server"
                             CssClass="form-control"
                             Placeholder="Ej: Periféricos, Monitores, Notebooks..." />
            </div>

            <!-- Botones -->
            <div class="abm-categoria-botones d-flex justify-content-center gap-3 mt-3">
                <asp:Button ID="btnGuardarCategoria" runat="server"
                            Text="Guardar"
                            CssClass="btn btn-primary" 
                            OnClick="btnGuardarCategoria_Click"/>

                <asp:Button ID="btnCancelarCategoria" runat="server"
                            Text="Cancelar"
                            CssClass="btn btn-outline-light"
                            PostBackUrl="~/AdminCategorias.aspx"/>
            </div>

            <!-- Mensaje de error -->
            <asp:Label ID="lblErrorCategoria" runat="server"
                       CssClass="abm-categoria-mensaje-error"
                       Visible="false" />
        </div>
    </div>
</asp:Content>
