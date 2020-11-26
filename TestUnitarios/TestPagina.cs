using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using Pedidos.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TestUnitarios
{
    [TestClass]
    public class TestPagina
    {
        private static long idPagina = -1;
        static PedidosPollomonContext pedidos = new PedidosPollomonContext();
        PaginasController paginasController = new PaginasController(pedidos);
        [TestMethod]
        public async Task PostPaginaTest()
        {

            var result = await paginasController.PostPagina(new Pagina()
            {
                Nombre = "testPrubea",
                Contenido = "pruebapagina",
                Publicado = true,
                Tipo = 1,
                FechaActualizacion = DateTime.Now,
                FechaCreacion = DateTime.Now,
                IdUsuario = 1
            });


            Assert.IsNotNull(result.Result);
            Pagina pagina = (Pagina)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(pagina);
            Assert.IsTrue(pagina.Id > 0);
            idPagina = pagina.Id;
        }
        [TestMethod]
        public async Task UpdatePaginaTest()
        {

            var result = await paginasController.PutPagina(idPagina, new Pagina()
            {
                Nombre = "testPrubea",
                Contenido = "pruebapagina",
                Publicado = true,
                Tipo = 1,
                FechaActualizacion = DateTime.Now,
                FechaCreacion = DateTime.Now,
                IdUsuario = 1
            });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DeletePaginaTest()
        {

            var result = await paginasController.DeletePagina(idPagina);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Add99RubrosTest()
        {

            for (int i = 0; i < 99; i++)
            {
                var result = await paginasController.PostPagina(new Pagina()
                {
                    Nombre = "testPrubea" + i,
                    Contenido = "pruebapagina" + i,
                    Publicado = true,
                    Tipo = 1,
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now,
                    IdUsuario = 1
                });
                Assert.IsNotNull(result);
            }
        }
        [TestMethod]
        public async Task GetPaginaTest()
        {
            var result = await paginasController.GetPaginas(new PageAndSortRequest() { Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);

        }
    }
}

