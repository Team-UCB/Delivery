using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using Pedidos.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TestUnitarios.Controllers
{
    [TestClass]
    public class TestParametro
    {
        private static long idParametro = -1;
        static PedidosPollomonContext pedidos = new PedidosPollomonContext();
        ParametrosController parametrosController = new ParametrosController(pedidos);
        [TestMethod]
        public async Task PostParametroTest()
        {

            var result = await parametrosController.PostParametro(new Parametro() { Nombre = "testPrubea", Valor = "1" });


            Assert.IsNotNull(result.Result);
            Parametro parametro = (Parametro)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(parametro);
            Assert.IsTrue(parametro.Id > 0);
            idParametro = parametro.Id;
        }
        [TestMethod]
        public async Task UpdateParametroTest()
        {

            var result = await parametrosController.PutParametro(idParametro, new Parametro() { Nombre = "test 2", Valor = "2" });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DeleteParametroTest()
        {

            var result = await parametrosController.DeleteParametro(idParametro);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task AddParametrosTest()
        {

            for (int i = 0; i < 99; i++)
            {
                var result = await parametrosController.PostParametro(new Parametro() { Nombre = "test =" + i, Valor = "123" + i });
                Assert.IsNotNull(result);
            }
        }
        [TestMethod]
        public async Task GetParametroTest()
        {
            var result = await parametrosController.GetParametros(new PageAndSortRequest() { Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);

        }
    }
}

