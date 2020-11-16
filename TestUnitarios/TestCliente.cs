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
    public class TestCliente
    {
        private static long idCliente = -1;
        static PedidosPollomonContext pedidos = new PedidosPollomonContext();
        ClientesController clientesController = new ClientesController(pedidos);

        [TestMethod]
        public async Task PostClienteTest()
        {
            var resultCliente = await clientesController.PostCliente(new Cliente()
            {
                Celular = "75137903",
                NombresApellidos = "Prueba ",
                Telefono = "6663843"
            });


            Assert.IsNotNull(resultCliente.Result);
            Cliente cliente = (Cliente)(resultCliente.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(cliente);
            Assert.IsTrue(cliente.Id > 0);
            idCliente = cliente.Id;
        }

        [TestMethod]
        public async Task UpdateClienteTest()
        {

            var resultCliente = await clientesController.PutCliente(
                idCliente, new Cliente()
                {
                    Celular = "75137903",
                    NombresApellidos = "Prueba ",
                    Telefono = "6663843",
                });
            Assert.IsNotNull(resultCliente);
        }

        [TestMethod]
        public async Task DeleteClienteTest()
        {

            var result = await clientesController.DeleteCliente(idCliente);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Add99ClienteTest()
        {

            for (int i = 0; i < 99; i++)
            {
                var result = await clientesController.PostCliente(
                    new Cliente()
                    {
                        Celular = "0" + i,
                        NombresApellidos = "Prueba " + i,
                        Telefono = "0000" + i,
                    });
                Assert.IsNotNull(result);
            }
        }
        [TestMethod]
        public async Task GetClienteTest()
        {
            var result = await clientesController.GetCliente
                (new PageAndSortRequest()
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
