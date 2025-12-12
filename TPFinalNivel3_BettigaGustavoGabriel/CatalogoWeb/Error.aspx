<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CatalogoWeb.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/Error.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="error-page-container">
        <div class="error-card">

            <!-- Icono de error -->
            <div class="error-icon">
                ❌
            </div>

            <!-- Título técnico -->
            <h1 class="error-title">
                ERROR
            </h1>          

            <!-- Mensaje dinámico más amigable  -->
            <asp:Label
                ID="lblMensajeError"
                runat="server"
                CssClass="error-message-main">
            </asp:Label>

            <!-- Bloque de detalle técnico -->
            <asp:Panel ID="pnlDetalleTecnico" runat="server" CssClass="error-technical-panel">
                <h2 class="error-technical-title">
                    Detalle técnico (para soporte)
                </h2>
                <asp:Label
                    ID="lblDetalleError"
                    runat="server"
                    CssClass="error-technical-text">
                </asp:Label>
            </asp:Panel>
            
            <!-- Botón de acción -->
            <div class="error-actions">
                <asp:Button
                    ID="btnVolverInicio"
                    runat="server"
                    Text="Volver al inicio"
                    CssClass="btn btn-primary btn-error-home"
                    PostBackUrl="~/Default.aspx" />
            </div>

        </div>
    </div>
</asp:Content>
