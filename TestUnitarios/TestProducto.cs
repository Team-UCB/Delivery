using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using Pedidos.Models;
using System.Threading.Tasks;
using System.Linq;

namespace TestUnitarios
{
    [TestClass]
    public class TestProducto
    {
        [TestMethod]
        public async Task Add99()
        {
            deliveryContext productosContext = new deliveryContext();
            ProductosController productosController = new ProductosController(productosContext);
            for (int i = 0; i < 2; i++)
            {
                var result = await productosController.PostProducto(
                new Producto()
                {
                    Nombre = "Pie maracuya",
                    PrecioUnitario = 50,
                    Cantidad = 10,
                    PrecioMayor = 45,
                    Marca = "Jenny",
                    Modelo = "Mediana",
                    Especificaciones = "12 personas",
                    IdCategoriaNavigation = new CategoriaProducto
                    {
                        Nombre = "Gaeosas",
                        Descripcion = "liquidos en base a componentes gaseoss",
                        Lugar = "Sección refrigerantes"
                    },

                    IdVendedorNavigation = new Vendedor
                    {
                        PersonaContacto = "Pedro Fuentes Lazarraga",
                        Celular = "72967356",
                        Telefono = "466-23058",
                        Correo = "carlos.fuentes@hotmail.com",
                        NombreEmpresa = "Arcor",
                        Direccion = "Zona mercader Av. Ballivian",
                        PathLogo = "",

                        IdRubroNavigation = new Rubro
                        {
                            Nombre = "Comida Rápida",
                            Descripcion = "encargados del área de comida rapida y derivados"
                        }
                    }
                });

                Assert.IsNotNull(result.Result);
            }
        }

        private static long idProducto = -1;
        [TestMethod]
        public async Task Add()
        {
            deliveryContext productosContext = new deliveryContext();
            ProductosController productosController = new ProductosController(productosContext);
            //var result = await productosController.PostProducto(new Productos() { Descripcion = "testOr", PathImg = "testOr", IdProducto = 1 });
            var result = await productosController.PostProducto(
                new Producto()
                {
                    Nombre = "Pie maracuya",
                    PrecioUnitario = 50,
                    Cantidad = 10,
                    PrecioMayor = 45,
                    Marca = "Jenny",
                    Modelo = "Mediana",
                    Especificaciones = "12 personas",
                    IdCategoriaNavigation = new CategoriaProducto
                    {
                        Nombre = "Gaeosas",
                        Descripcion = "liquidos en base a componentes gaseoss",
                        Lugar = "Sección refrigerantes"
                    },

                    IdVendedorNavigation = new Vendedor
                    {
                        PersonaContacto = "Pedro Fuentes Lazarraga",
                        Celular = "72967356",
                        Telefono = "466-23058",
                        Correo = "carlos.fuentes@hotmail.com",
                        NombreEmpresa = "Arcor",
                        Direccion = "Zona mercader Av. Ballivian",
                        PathLogo = "",

                        IdRubroNavigation = new Rubro
                        {
                            Nombre = "Comida Rápida",
                            Descripcion = "encargados del área de comida rapida y derivados"
                        }
                    }
                });

            Assert.IsNotNull(result.Result);
            Producto producto = (Producto)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(producto);
            Assert.IsTrue(producto.Id > 0);
            idProducto = producto.Id;
        }

        [TestMethod]
        public async Task Upd()
        {
            deliveryContext productosContext = new deliveryContext();
            ProductosController productosController = new ProductosController(productosContext);

            var result = await productosController.PostProducto(
                new Producto()
                {
                    Nombre = "Pie maracuya",
                    PrecioUnitario = 50,
                    Cantidad = 10,
                    PrecioMayor = 45,
                    Marca = "Jenny",
                    Modelo = "Mediana",
                    Especificaciones = "12 personas",
                    IdCategoriaNavigation = new CategoriaProducto
                    {
                        Nombre = "Gaeosas",
                        Descripcion = "liquidos en base a componentes gaseoss",
                        Lugar = "Sección refrigerantes"
                    },

                    IdVendedorNavigation = new Vendedor
                    {
                        PersonaContacto = "Pedro Fuentes Lazarraga",
                        Celular = "72967356",
                        Telefono = "466-23058",
                        Correo = "carlos.fuentes@hotmail.com",
                        NombreEmpresa = "Arcor",
                        Direccion = "Zona mercader Av. Ballivian",
                        PathLogo = "",

                        IdRubroNavigation = new Rubro
                        {
                            Nombre = "Comida Rápida",
                            Descripcion = "encargados del área de comida rapida y derivados"
                        }
                    }
                });

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Del()
        {
            deliveryContext productosContext = new deliveryContext();
            ProductosController productosController = new ProductosController(productosContext);
            var result = await productosController.DeleteProducto(idProducto);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Get()
        {
            deliveryContext productosContext = new deliveryContext();
            ProductosController productosController = new ProductosController(productosContext);
            var result = await productosController.GetProducto(new PageAndSortRequest() { Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);
        }


    }
}
