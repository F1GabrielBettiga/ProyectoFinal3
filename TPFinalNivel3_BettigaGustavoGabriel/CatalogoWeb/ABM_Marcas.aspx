<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="ABM_Marcas.aspx.cs" Inherits="CatalogoWeb.ABM_Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/ABM_Marcas.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="abm-marca-container">

        <!-- TÍTULO (se puede cambiar desde el code-behind: "Nueva marca" / "Editar marca") -->
        <asp:Label ID="lblTituloABMMarca" runat="server"
            Text="Nueva marca"
            CssClass="titulo-abm-marca" />

        <!-- TARJETA PRINCIPAL -->
        <div class="abm-marca-card">

            <!-- FILA ID (solo para editar; podés ocultarla en Page_Load si es alta) -->
            <asp:Panel ID="pnlIdMarca" runat="server" CssClass="fila-id-marca">
                <div class="mb-3">
                    <asp:Label ID="lblIdMarca" runat="server"
                        Text="ID"
                        CssClass="form-label" />
                    <asp:TextBox ID="txtIdMarca" runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />
                </div>
            </asp:Panel>

            <!-- CAMPO NOMBRE DE LA MARCA -->
            <div class="mb-3">
                <asp:Label ID="lblNombreMarca" runat="server"
                    Text="Nombre de la marca"
                    CssClass="form-label" />
                <asp:TextBox ID="txtNombreMarca" runat="server"
                    CssClass="form-control"
                    Placeholder="Ej: Samsung, Apple, Logitech..." />
            </div>

            <!-- BOTONES -->
            <div class="abm-marca-botones text-center mt-3">
                <asp:Button ID="btnGuardarMarca" runat="server"
                    Text="Guardar"
                    CssClass="btn btn-primary me-2"
                    OnClick="btnGuardarMarca_Click" />

                <asp:Button ID="btnCancelarMarca" runat="server"
                    Text="Cancelar"
                    CssClass="btn btn-outline-light"
                    PostBackUrl="~/AdminMarcas.aspx" />
            </div>

            <!-- MENSAJE DE ERROR -->
            <asp:Label ID="lblErrorMarca" runat="server"
                CssClass="abm-marca-mensaje-error"
                Visible="false" />

        </div>

    </div>




</asp:Content>
