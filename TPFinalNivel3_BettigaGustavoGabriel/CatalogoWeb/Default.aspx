<%@ Page Title="Home" Language="C#" MasterPageFile="~/NavBar_Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CatalogoWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="Css/Default.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h1 class="titulo-lista-productos">Lista de Productos</h1>

    <div class="row row-cols-1 row-cols-sm-2 row-cols-md-4 g-4 productos-grid">
        <asp:Repeater ID="repetidorDeTarjetas" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card tarjeta-producto h-100">
                        <img src="<%# Eval("imagenUrl") %>" class="card-img-top img-producto" alt="Imagen producto">
                        <div class="card-body">
                            <h5 class="card-title titulo-producto"><%# Eval("nombre") %></h5>
                            <p class="card-text descripcion-producto"><%# Eval("descripcion") %></p>
                            <a href='DetalleProducto.aspx?id=<%# Eval("id") %>' class="btn btn-primary btn-sm">Ver Detalle</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
</div>


</asp:Content>
