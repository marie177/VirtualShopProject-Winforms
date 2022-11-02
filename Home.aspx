<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="tp_webforms_gottig_ramirez.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-2" style="margin-left: 50px">
        <div class="row pt-4 pb-4">
            <div class="col-2 text-center">
                <asp:TextBox CssClass="form-control me-2" ID="txtBuscarNom" PlaceHolder="Buscar por nombre" runat="server"></asp:TextBox>
            </div>
            <div class="col-2">
                <asp:Button CssClass="btn btn-info" ID="btnBuscarNom" OnClick="btnBuscarNom_Click" runat="server" Text="Buscar" />
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div class="row">
                    <div class="col">
                        <label class="w-100">Marca </label>
                        <div class="btn-group">
                            <asp:DropDownList ID="DropDownListMarca" runat="server" CssClass="btn btn-secondary dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="DropDownListMarca_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <label class="w-100">Categoria </label>
                        <div class="btn-group">
                            <asp:DropDownList f ID="DropDownListCategoria" runat="server" CssClass="btn btn-secondary dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCategoria_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-10">
                <div class="row row-cols-1 row-cols-md-2 g-4 justify-content-center">
                    <asp:Repeater runat="server" ID="repeaterArticulos">
                        <ItemTemplate>
                            <div class="col">
                                <div class="card m-auto p-2" style="max-width: 750px;">
                                    <div class="row g-0">
                                        <div class="col-md-3">
                                            <img src="<%#Eval("ImagenUrl")%>" class="img-fluid rounded-start mt-4" alt="...">
                                        </div>
                                        <div class="col-md-8">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col">
                                                        <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col">
                                                        <p class="card-text"><small class="text-muted">Cod: <%#Eval("Codigo") %></small></p>
                                                        <p class="card-text"><%#Eval("Descripcion")%></p>
                                                        <p class="card-text mt-2">$ <%#Eval("Precio") %></p>
                                                    </div>
                                                    <div class="col-md-3 m-auto">
                                                        <asp:Button ID="btnAgregar" OnClick="btnAgregar_Click" CommandArgument='<%#Eval("Id") %>' CommandName="IdArticulo" class="btn btn-outline-success" runat="server" Text="Agregar" />
                                                        <asp:Button ID="btnFavorito" class="btn btn-outline-danger mt-3" OnClick="btnFavorito_Click" CommandArgument='<%#Eval("Id") %>' CommandName="IdArticulo" runat="server" Text="Favorito" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasCarrito" aria-labelledby="offcanvasCarritoLabel" style="">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title fw-bold btn btn-success disabled m-auto" id="offcanvasExampleLabel" style="width: 100%"><i class="uil uil-shopping-cart">Su Carrito</i></h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Cerrar"></button>
        </div>
        <div class="offcanvas-body pt-0">
            <p class="placeholder-wave">
                <span class="placeholder col-12 bg-warning"></span>
            </p>
            <div class="row mx-2">
                <div class="alert alert-warning ms-4 text-center fw-bolder" style="width: 40%;">
                    <asp:Label ID="lblImporteTotal" runat="server" Text=""></asp:Label>
                </div>
                <div class="alert alert-warning mx-2 text-center fw-bolder" style="width: 45%">
                    <small>
                        <asp:Label ID="lblCantProd" runat="server" Text=""></asp:Label>
                    </small>
                </div>
            </div>
            <p class="placeholder-wave">
                <span class="placeholder col-12 bg-warning"></span>
            </p>
            <div>
                <div class="row">
                    <asp:Repeater runat="server" ID="repeaterCarrito">
                        <ItemTemplate>
                            <li class="dropdown-item">
                                <div class="col m-4" style="width: 85%;">
                                    <div class="card mx-3 my-1" style="max-width: 540px;">
                                        <div class="row g-0">
                                            <div class="col-md-3">
                                                <img src="<%#Eval("UrlImagen")%>" class="img-fluid rounded-start mt-4 text-center" alt="...">
                                            </div>
                                            <div class="col-md-8">
                                                <div class="card-body" style="word-break: break-all; white-space: normal;">
                                                    <div class="row">
                                                        <div class="col-10">
                                                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col">
                                                            <p class="card-text mb-0"><small class="text-muted">Cod: <%#Eval("Codigo") %></small></p>
                                                            <p class="card-text mt-2">$ <%#Eval("PrecioUnitario") %></p>
                                                            <div class="col-md-4 m-auto">
                                                                <small>
                                                                    <asp:Button ID="btnEliminarCar" CssClass=" btn btn-close btn-danger" OnClick="btnEliminarCar_Click" CommandArgument='<%#Eval("IdArticulo") %>' CommandName="IdArticulo" runat="server" Text="" />
                                                                </small>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <div class="offcanvas offcanvas-start" tabindex="-1" id="offcanvasFavoritos" aria-labelledby="offcanvasFavoritosLabel">
        <div class="offcanvas-header">
            <h5 class="offcanvas-title" id="offcanvasFavoritosLabel"><i class="uil uil-heart-alt"></i>Favoritos</h5>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Cerrar"></button>
        </div>
        <div class="offcanvas-body">
            <asp:Repeater runat="server" ID="repeaterFavoritos">
                <ItemTemplate>
                    <div class="row" style="width: 100%">
                        <div class="col m-2 card p-1" style="max-width: 540px;">
                            <div class="row g-0">
                                <div class="col-md-3 m-auto">
                                    <img src="<%#Eval("ImagenUrl")%>" class="img-fluid rounded-start mt-2" alt="...">
                                </div>
                                <div class="col-md-8">
                                    <div class="card-body p-2" style="word-break: break-all; white-space: normal;">
                                        <div class="row p-0 m-0">
                                            <div class="col">
                                                <h5 class="card-title p-0 m-1"><%#Eval("Nombre") %></h5>
                                                <p class="card-text"><small class="text-muted">Cod: <%#Eval("Codigo") %></small></p>
                                            </div>
                                        </div>
                                        <div class="row p-0 m-0">
                                            <div class="col">
                                                <p class="card-text">$ <%#Eval("Precio") %></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <asp:Button runat="server" CssClass="btn btn-warning" CommandArgument='<%#Eval("Id") %>' CommandName="IdArticulo" ID="btnEliminarFavorito" OnClick="btnEliminarFavorito_Click" Text="Eliminar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
