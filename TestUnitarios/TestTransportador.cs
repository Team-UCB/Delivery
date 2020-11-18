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
    public class TestTransportador
    {
        private static long idTransportador = -1;
        static PedidosPollomonContext pedidos = new PedidosPollomonContext();
        TransportadoresController transportadoresController = new TransportadoresController(pedidos);

        [TestMethod]
        public async Task PostTransportadorTest()
        {
          

            var resultTransportador = await transportadoresController.PostTransportador(new Transportador()
            {
                Celular = "75137903",
                DescripcionVehiculo = "pruebasa",
                Estado = "Activo",
                Latitud = 0,
                Longitud = 1,
                NombreCompleto = "testPrubea",
                TipoVehiculo = "Moto"                               
            });


            Assert.IsNotNull(resultTransportador.Result);
            Transportador transportador = (Transportador)(resultTransportador.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(transportador);
            Assert.IsTrue(transportador.Id > 0);
            idTransportador = transportador.Id;
        }

        [TestMethod]
        public async Task UpdateTransportadorTest()
        {

            var resultTransportador = await transportadoresController.PutTransportador(
                idTransportador, new Transportador()
                {
                    Celular = "75137903",
                    DescripcionVehiculo = "pruebasa 2",
                    Estado = "Activo",
                    Latitud = 0,
                    Longitud = 1,
                    NombreCompleto = "testPrubea 2",
                    TipoVehiculo = "Moto 2",                    
                });
            Assert.IsNotNull(resultTransportador);
        }

        [TestMethod]
        public async Task DeleteTransportadorTest()
        {

            var result = await transportadoresController.DeleteTransportador(idTransportador);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Add99TransportadorTest()
        {

            for (int i = 0; i < 99; i++)
            {
                var result = await transportadoresController.PostTransportador(
                    new Transportador()
                    {
                        Celular = "0" + i,
                        DescripcionVehiculo = "pruebas "+ i,
                        Estado = "Activo",
                        Latitud = 0,
                        Longitud = 1,
                        NombreCompleto = "testPrueba "+ i,
                        TipoVehiculo = "Moto 2",                        
                    });
                Assert.IsNotNull(result);
            }
        }
        [TestMethod]
        public async Task GetTransportadorTest()
        {
            var result = await transportadoresController.GetTransportador
                (new PageAndSortRequest() 
                { 
                    Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" 
                });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);

        }
    }
}
