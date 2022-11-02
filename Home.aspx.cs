using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace tp_webforms_gottig_ramirez
{
    public partial class Home : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos = new List<Articulo>();
        public Carrito carrito;
        public ArticuloNegocio negocio = new ArticuloNegocio();
        MarcaNegocio marcaNegocio = new MarcaNegocio();
        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //funciones al cargar la pagina, para listar articulos y cargar la session
                listarArticulos();
                crearSessionCarrito();
                crearSessionFavoritos();
                inicializarDropwDownMarca();
                inicializarDropwDownCategoria();

                lblImporteTotal.Text = "Total: $ " + ((Carrito)Session["Carrito"]).ImporteTotal.ToString();
                lblCantProd.Text = "Cant. Productos: " + ((Carrito)Session["Carrito"]).cantProductos.ToString();

            }
        }

        private void listarArticulos()
        {

            listaArticulos = negocio.ListarArticulos();
            repeaterArticulos.DataSource = listaArticulos;
            repeaterArticulos.DataBind();
        }

        private void crearSessionCarrito()
        {
            if (Session["Carrito"] == null)
            {
                carrito = new Carrito();
                Session.Add("Carrito", carrito);
            }

        }

        private void crearSessionFavoritos()
        {
            if (Session["Favoritos"] == null)
            {
                List<Articulo> articulosFavoritosList = new List<Articulo>();
                Session.Add("Favoritos", articulosFavoritosList);
            }
            else
            {

                repeaterFavoritos.DataSource = ((List<Articulo>)Session["Favoritos"]);
                repeaterFavoritos.DataBind();
            }
        }

        private void inicializarDropwDownMarca()
        {
            List<Marca> marcas = new List<Marca>();
            marcas.Add(new Marca() { Descripcion = "-- Seleccionar --" });
            marcas.AddRange(marcaNegocio.listar());

            DropDownListMarca.DataSource = marcas;
            DropDownListMarca.DataBind();
        }

        private void inicializarDropwDownCategoria()
        {
            List<Categoria> categorias = new List<Categoria>();
            categorias.Add(new Categoria() { Descripcion = "-- Seleccionar --" });
            categorias.AddRange(categoriaNegocio.listar());

            DropDownListCategoria.DataSource = categorias;
            DropDownListCategoria.DataBind();
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //si el carrito está nulo, lo agregi a la Session
            crearSessionCarrito();

            //busco id del Articulo seleccionado
            int idArticuloSeleccionado = Convert.ToInt32(((Button)sender).CommandArgument);

            List<CarritoDetalle> detalleCarritoList = ((Carrito)Session["Carrito"]).CarritoDetalleList;

            //si la session del carrito no está vacia, busco el Articulo y sumo cantidad, si no lo agrego a la lista de la Session
            if (detalleCarritoList != null && detalleCarritoList.Any(x => x.IdArticulo == idArticuloSeleccionado))
            {

                CarritoDetalle aux = new CarritoDetalle();
                aux = detalleCarritoList.Find(x => x.IdArticulo == idArticuloSeleccionado);

                detalleCarritoList.Find(x => x.IdArticulo == idArticuloSeleccionado).Cantidad++;
                aux.PrecioTotal += aux.PrecioUnitario;

                ((Carrito)Session["Carrito"]).ImporteTotal += aux.PrecioUnitario;
            }
            else
            {
                listaArticulos = negocio.ListarArticulos();
                Articulo articuloSeleccionado = listaArticulos.Find(x => x.Id == idArticuloSeleccionado);

                CarritoDetalle nuevoDetalle = new CarritoDetalle()
                {
                    IdArticulo = articuloSeleccionado.Id,
                    Nombre = articuloSeleccionado.Nombre,
                    UrlImagen = articuloSeleccionado.ImagenUrl,
                    Codigo = articuloSeleccionado.Codigo,
                    Descripcion = articuloSeleccionado.Descripcion,
                    Cantidad = 1,
                    PrecioUnitario = articuloSeleccionado.Precio,
                    PrecioTotal = articuloSeleccionado.Precio
                };

                ((Carrito)Session["Carrito"]).ImporteTotal += nuevoDetalle.PrecioUnitario;
                detalleCarritoList.Add(nuevoDetalle);
            }

            ((Carrito)Session["Carrito"]).cantProductos++;
            repeaterCarrito.DataSource = ((Carrito)Session["Carrito"]).CarritoDetalleList;
            repeaterCarrito.DataBind();

            lblImporteTotal.Text = "Total: $ " + ((Carrito)Session["Carrito"]).ImporteTotal.ToString();
            lblCantProd.Text = "Cant Productos: " + ((Carrito)Session["Carrito"]).cantProductos.ToString();
        }

        protected void btnFavorito_Click(object sender, EventArgs e)
        {
            crearSessionFavoritos();

            int idArticuloSeleccionado = Convert.ToInt32(((Button)sender).CommandArgument);
            Articulo articuloSeleccionado = negocio.ListarArticulos().Find(x => x.Id == idArticuloSeleccionado);

            List<Articulo> articulosFavoritosList = ((List<Articulo>)Session["Favoritos"]);

            if (!articulosFavoritosList.Any(x => x.Id == idArticuloSeleccionado))
            {
                articulosFavoritosList.Add(articuloSeleccionado);
            }

            repeaterFavoritos.DataSource = ((List<Articulo>)Session["Favoritos"]);
            repeaterFavoritos.DataBind();

        }

        protected void btnBuscarNom_Click(object sender, EventArgs e)
        {

            string filtroNombre = txtBuscarNom.Text;
            filtroNombre = filtroNombre.ToLower();

            List<Articulo> listaFiltrada = new List<Articulo>();

            listaFiltrada = negocio.ListarArticulos().Where(x => x.Nombre.ToLower().Contains(filtroNombre)).ToList();

            repeaterArticulos.DataSource = null;

            repeaterArticulos.DataSource = listaFiltrada;
            repeaterArticulos.DataBind();
        }




        protected void DropDownListMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        protected void DropDownListCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            listaArticulos = negocio.ListarArticulos();
            var listaMarcas = marcaNegocio.listar();
            var listaCategorias = categoriaNegocio.listar();

            var filtroMarcas = DropDownListMarca.SelectedItem.Text;

            var filtroCategorias = DropDownListCategoria.SelectedItem.Text;

            //chequea que la marca exista en la DB

            if (listaMarcas.Any(x => x.Descripcion == filtroMarcas))
            {
                //chequea que la propiedad Marca del Objeto no este nula

                listaArticulos = listaArticulos.Where(x => x.Marca?.Descripcion == filtroMarcas).ToList();

            }


            if (listaCategorias.Any(x => x.Descripcion == filtroCategorias))
            {

                listaArticulos = listaArticulos.Where(x => x.Categoria?.Descripcion == filtroCategorias).ToList();

            }


            repeaterArticulos.DataSource = listaArticulos;
            repeaterArticulos.DataBind();
        }

        protected void btnEliminarCar_Click(object sender, EventArgs e)
        {
            int idArticuloSeleccionado = Convert.ToInt32(((Button)sender).CommandArgument);

            List<CarritoDetalle> detalleCarritoList = ((Carrito)Session["Carrito"]).CarritoDetalleList;

            // Busca el precio unitario y la cantidad del articulo a eliminar
            float importe = detalleCarritoList.Find(x => x.IdArticulo == idArticuloSeleccionado).PrecioUnitario;
            int cantidad = detalleCarritoList.Find(x => x.IdArticulo == idArticuloSeleccionado).Cantidad;

            // Elimina el precio por la cantidad que habia en el carrito
            ((Carrito)Session["Carrito"]).ImporteTotal -= (importe * cantidad);

            // Elimina el articulo de la lista detalleCarrito
            detalleCarritoList.RemoveAll(x => x.IdArticulo == idArticuloSeleccionado);

            // Resta la cantidad de ese articulo a la cantidad de productos en la session (para cambiar el label del offcanvas)
            ((Carrito)Session["Carrito"]).cantProductos -= cantidad;

            lblImporteTotal.Text = "Total: $ " + ((Carrito)Session["Carrito"]).ImporteTotal.ToString();
            lblCantProd.Text = "Cant Productos: " + ((Carrito)Session["Carrito"]).cantProductos.ToString();

            repeaterCarrito.DataSource = ((Carrito)Session["Carrito"]).CarritoDetalleList;
            repeaterCarrito.DataBind();
        }

        protected void btnEliminarFavorito_Click(object sender, EventArgs e)
        {
            List<Articulo> articulosFavoritosList = ((List<Articulo>)Session["Favoritos"]);

            int idArticuloSeleccionado = Convert.ToInt32(((Button)sender).CommandArgument);
            var articuloSeleccionado = articulosFavoritosList.Find(x => x.Id == idArticuloSeleccionado);

            articulosFavoritosList.Remove(articuloSeleccionado);

            repeaterFavoritos.DataSource = articulosFavoritosList;
            repeaterFavoritos.DataBind();
        }
    }
}