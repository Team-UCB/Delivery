using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using Pedidos.Models;
using System.Threading.Tasks;
using System.Linq;


namespace TestUnitarios
{
    [TestClass]
    public class TestCategoriaProducto
    {
        [TestMethod]
        public async Task Add99()
        {
            PedidosPollomonContext categoriaProductosContext = new PedidosPollomonContext();
            CategoriaProductosController categoriaProductosController = new CategoriaProductosController(categoriaProductosContext);

            for (int i = 0; i < 10; i++)
            {
                var result = await categoriaProductosController.PostCategoriaProducto(new CategoriaProducto() 
                {
                    Nombre = "Libros",
                    Descripcion = "el Principito",
                    Lugar = "Bolivar" 
                });
                Assert.IsNotNull(result.Result);
            }

        }
        private static long idCategoriaProducto = -1;
        [TestMethod]
        public async Task Add()
        {
            PedidosPollomonContext categoriaProductosContext = new PedidosPollomonContext();
            CategoriaProductosController categoriaProductosController = new CategoriaProductosController(categoriaProductosContext);
            var result = await categoriaProductosController.PostCategoriaProducto(new CategoriaProducto()
            { 
                Nombre = "Libros",
                Descripcion = "el Principito", 
                Lugar = "Bolivar" 
            });


            Assert.IsNotNull(result.Result);
            CategoriaProducto categoriaProducto = (CategoriaProducto)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(categoriaProducto);
            Assert.IsTrue(categoriaProducto.Id > 0);
            idCategoriaProducto = categoriaProducto.Id;
        }
        [TestMethod]
        public async Task Upd()
        {

            PedidosPollomonContext categoriaProductosContext = new PedidosPollomonContext();
            CategoriaProductosController categoriaProductosController = new CategoriaProductosController(categoriaProductosContext);
            var result = await categoriaProductosController.PutCategoriaProducto(idCategoriaProducto, new CategoriaProducto()
            { 
                Nombre = "Libros",
                Descripcion = "el Principito", 
                Lugar = "Bolivar" 
            });

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Del()
        {
            PedidosPollomonContext categoriaProductosContext = new PedidosPollomonContext();
            CategoriaProductosController categoriaProductosController = new CategoriaProductosController(categoriaProductosContext);
            var result = await categoriaProductosController.DeleteCategoriaProducto(idCategoriaProducto);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Get()
        {
            PedidosPollomonContext categoriaProductosContext = new PedidosPollomonContext();
            CategoriaProductosController categoriaProductosController = new CategoriaProductosController(categoriaProductosContext);
            var result = await categoriaProductosController.GetCategoriaProductos(new PageAndSortRequest()
            { 
                Pagina = 1, 
                TamPagina = 10,
                Columna = "Id",
                Direccion = "asc", 
                Filtro = ""
            });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);
        }

    }
}
