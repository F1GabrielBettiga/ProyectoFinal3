<%@ Page Title="Home" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CatalogoWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Css/Default.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="updTarjetas" runat="server">
        <ContentTemplate>

            <h1 class="titulo-lista-productos">Lista de Productos</h1>

            <div class="row row-cols-1 row-cols-sm-2 row-cols-md-4 g-4 productos-grid">
                <asp:Repeater ID="repetidorDeTarjetas" runat="server" OnItemDataBound="repetidorDeTarjetas_ItemDataBound">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card tarjeta-producto h-100">
                                <asp:Image ID="imgProducto"
                                    runat="server"
                                    CssClass="card-img-top img-producto"
                                    ImageUrl='<%# Eval("imagenUrl") %>' />
                                <div class="card-body">
                                    <h5 class="card-title titulo-producto"><%# Eval("nombre") %></h5>
                                    <p class="card-text descripcion-producto"><%# Eval("descripcion") %></p>
                                    <asp:Button
                                        ID="btnVerDetalle"
                                        runat="server"
                                        Text="Ver Detalle"
                                        CssClass="btn btn-primary btn-sm"
                                        CommandArgument='<%# Eval("Id") %>'
                                        OnClick="btnVerDetalle_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="text-center mt-3 mb-4">
                <asp:Button ID="btnCargarMas" runat="server"
                    Text="Ver Más"
                    CssClass="btn btn-outline-light"
                    OnClick="btnCargarMas_Click" />
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
