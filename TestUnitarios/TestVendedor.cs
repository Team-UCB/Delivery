using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using System;
using Pedidos.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Pedidos.Data;

namespace TestUnitarios
{
    [TestClass]
    public class TestVendedor
    {
        private static long idVendedor = -1;
        static deliveryContext pedidosContext = new deliveryContext();
        VendedoresController vendedoresController = new VendedoresController(pedidosContext);

        [TestMethod]
        public async Task Add99VendedorTest()
        {
            for (int i = 0; i < 10; i++)
            {
                var result = await vendedoresController.PostVendedor(new Vendedor()
                {
                    PersonaContacto = "Prueba2",
                    Celular = "100",
                    Telefono = "100",
                    Correo = "prueba",
                    NombreEmpresa = "prueba2",
                    Direccion = "prueba",
                    PathLogo = "",
                    IdRubroNavigation = new Rubro
                    {
                        Nombre = "Prueba",
                        Descripcion = "Prueba",
                        IdPadre = 1
                    }
                });
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public async Task Add()
        {

            var result = await vendedoresController.PostVendedor(new Vendedor()
            {
                PersonaContacto = "Prueba",
                Celular = "100",
                Telefono = "100",
                Correo = "prueba",
                NombreEmpresa = "prueba2",
                Direccion = "prueba",
                PathLogo = "",
                IdRubroNavigation = new Rubro
                {
                    Nombre = "Prueba",
                    Descripcion = "Prueba",
                    IdPadre = 1
                }
            });
            Assert.IsNotNull(result.Result);
            Vendedor vendedor = (Vendedor)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(vendedor);
            Assert.IsTrue(vendedor.Id > 0);
            idVendedor = vendedor.Id;
        }
        [TestMethod]
        public async Task PutVendedorTest()
        {

            var result = await vendedoresController.PutVendedor(idVendedor, new Vendedor()
            {
                PersonaContacto = "Prueba2",
                Celular = "100",
                Telefono = "100",
                Correo = "prueba",
                NombreEmpresa = "prueba2",
                Direccion = "prueba",
                PathLogo = "",
                IdRubroNavigation = new Rubro
                {
                    Nombre = "Prueba",
                    Descripcion = "Prueba",
                    IdPadre = 1
                }
            });
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task DeleteVendedorTest()
        {

            var result = await vendedoresController.DeleteVendedor(idVendedor);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task GetVendedorTest()
        {

            var result = await vendedoresController.GetVendedor(new PageAndSortRequest()
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
