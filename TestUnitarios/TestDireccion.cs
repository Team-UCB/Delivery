using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using System;
using Pedidos.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using System.Linq;
using Pedidos.Data;

namespace TestUnitarios
{
    [TestClass]
    public class TestDireccion
    {
        private static long idDireccion = -1;
        static deliveryContext pedidosContext = new deliveryContext();
        DireccionesController direccionesController = new DireccionesController(pedidosContext);

        [TestMethod]
        public async Task Add99()
        {

            for (int i = 0; i < 10; i++)
            {
                var result = await direccionesController.PostDireccion(new Direccion()
                {
                    Descripcion = "prueba",
                    Latitud = 0M,
                    Longitud = 0M,
                    Referencia = "text",
                    Predeterminada = false,
                    IdClienteNavigation = new Cliente
                    {
                        NombresApellidos = "prueba",
                        Celular = "100",
                        Telefono = "100"
                    }
                });
            }
        }
        [TestMethod]
        public async Task Add()
        {

            var result = await direccionesController.PostDireccion(new Direccion()
            {
                Descripcion = "prueba",
                Latitud = 0M,
                Longitud = 0M,
                Referencia = "text",
                Predeterminada = false,
                IdClienteNavigation = new Cliente
                {
                    NombresApellidos = "prueba",
                    Celular = "100",
                    Telefono = "100"
                }
            });

            Assert.IsNotNull(result.Result);
            Direccion direccion = (Direccion)(result.Result as CreatedAtActionResult).Value;
            Assert.IsTrue(direccion.Id > 0);
            idDireccion = direccion.Id;
        }
        [TestMethod]
        public async Task PutDireccionTest()
        {

            var result = await direccionesController.PutDireccion(idDireccion, new Direccion()
            {
                Descripcion = "prueba2",
                Latitud = 0M,
                Longitud = 0M,
                Referencia = "text",
                Predeterminada = false,
                IdClienteNavigation = new Cliente
                {
                    NombresApellidos = "prueba",
                    Celular = "100",
                    Telefono = "100"
                }
            });

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task DeleteDireccionTest()
        {

            var result = await direccionesController.DeleteDireccion(idDireccion);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task GetDireccionTest()
        {
            var result = await direccionesController.GetDireccion(new PageAndSortRequest()
            {
                Columna = "Id",
                Direccion = "asc",
                Pagina = 1,
                TamPagina = 10,
                Filtro = ""
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);
        }
    }
}
