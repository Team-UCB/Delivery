using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using Pedidos.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestUnitarios
{
    [TestClass]
    public class TestPedido
    {
        private static long idPedido = -1;

        [TestMethod]
        public async Task AddPedido()
        {
            deliveryContext pedidosContext = new deliveryContext();
            PedidosController pedidosController = new PedidosController(pedidosContext);
            var result = await pedidosController.PostPedido(new Pedido()
            {
                FechaIngreso = DateTime.Now,
                FechaAtencion = DateTime.Now,
                FechaSalida = DateTime.Now,
                FechaEntrega = DateTime.Now,
                Estado = "activo",
                CodigoQrFactura = "codigo qr de prueba",
                MontoEnvio = 123,
                TipoPago = "efectivo",
                MontoCliente = 12,
                IdClienteNavigation = new Cliente()
                {
                    NombresApellidos = "Pepe Galleta",
                    Celular = "78547845",
                    Telefono = "46657812"
                },
                IdTransporteNavigation = new Transportador()
                {
                    Celular = "78547845",
                    DescripcionVehiculo = "Moto",
                    Estado = "activo",
                    Latitud = 0M,
                    Longitud = 0M,
                    NombreCompleto = "Pedro Flores",
                    TipoVehiculo = "Moto",
                },
                IdVendedorNavigation = new Vendedor()
                {
                    NombreEmpresa = "Micro mercado la terminal",
                    Celular = "78547845",
                    Telefono = "46657812",
                    Direccion = "Av. La Paz esq. Av. Victor Paz",
                    Correo = "laterminalmm@gmail.com",
                    PersonaContacto = "Juan Perez",
                    PathLogo = "",
                    IdRubroNavigation = new Rubro()
                    {
                        Nombre = "Super Mercados",
                        Descripcion = "Nuevo Rubro"
                    }
                }

            });

            Assert.IsNotNull(result.Result);
            Pedido pedido = (Pedido)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(pedido);
            Assert.IsTrue(pedido.Id > 0);
            idPedido = pedido.Id;
        }
        [TestMethod]
        public async Task UpdPedido()
        {
            deliveryContext pedidosContext = new deliveryContext();
            PedidosController pedidosController = new PedidosController(pedidosContext);

            var result = await pedidosController.PutPedido(idPedido, new Pedido()
            {
                FechaIngreso = DateTime.Now,
                FechaAtencion = DateTime.Now,
                FechaSalida = DateTime.Now,
                FechaEntrega = DateTime.Now,
                Estado = "activo",
                CodigoQrFactura = "codigo qr de prueba",
                MontoEnvio = 123,
                TipoPago = "efectivo actualizado",
                MontoCliente = 12,
                IdClienteNavigation = new Cliente()
                {
                    NombresApellidos = "Pepe Galleta",
                    Celular = "78547845",
                    Telefono = "46657812"
                },
                IdTransporteNavigation = new Transportador()
                {
                    Celular = "78547845",
                    DescripcionVehiculo = "Moto",
                    Estado = "activo",
                    Latitud = 0M,
                    Longitud = 0M,
                    NombreCompleto = "Pedro Flores",
                    TipoVehiculo = "Moto",
                },
                IdVendedorNavigation = new Vendedor()
                {
                    NombreEmpresa = "Micro mercado la terminal",
                    Celular = "78547845",
                    Telefono = "46657812",
                    Direccion = "Av. La Paz esq. Av. Victor Paz",
                    Correo = "laterminalmm@gmail.com",
                    PersonaContacto = "Juan Perez",
                    PathLogo = "",
                    IdRubroNavigation = new Rubro()
                    {
                        Nombre = "Super Mercados",
                        Descripcion = "Nuevo Rubro"
                    }
                }

            });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DelPedido()
        {
            deliveryContext pedidosContext = new deliveryContext();
            PedidosController pedidosController = new PedidosController(pedidosContext);

            var result = await pedidosController.DeletePedido(idPedido);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task Add99Pedido()
        {
            deliveryContext pedidosContext = new deliveryContext();
            PedidosController pedidosController = new PedidosController(pedidosContext);

            for (int i = 0; i < 99; i++)
            {

                var result = await pedidosController.PostPedido(new Pedido()
                {
                    FechaIngreso = DateTime.Now,
                    FechaAtencion = DateTime.Now,
                    FechaSalida = DateTime.Now,
                    FechaEntrega = DateTime.Now,
                    Estado = "activo",
                    CodigoQrFactura = "codigo qr de prueba",
                    MontoEnvio = 123,
                    TipoPago = "efectivo",
                    MontoCliente = 12,
                    IdClienteNavigation = new Cliente()
                    {
                        NombresApellidos = "Pepe Galleta",
                        Celular = "78547845",
                        Telefono = "46657812"
                    },
                    IdTransporteNavigation = new Transportador()
                    {
                        Celular = "78547845",
                        DescripcionVehiculo = "Moto",
                        Estado = "activo",
                        Latitud = 0M,
                        Longitud = 0M,
                        NombreCompleto = "Pedro Flores",
                        TipoVehiculo = "Moto",
                    },
                    IdVendedorNavigation = new Vendedor()
                    {
                        NombreEmpresa = "Micro mercado la terminal",
                        Celular = "78547845",
                        Telefono = "46657812",
                        Direccion = "Av. La Paz esq. Av. Victor Paz",
                        Correo = "laterminalmm@gmail.com",
                        PersonaContacto = "Juan Perez",
                        PathLogo = "",
                        IdRubroNavigation = new Rubro()
                        {
                            Nombre = "Super Mercados",
                            Descripcion = "Nuevo Rubro"
                        }
                    }

                });
                Assert.IsNotNull(result.Result);
            }
        }
        [TestMethod]
        public async Task GetPedidos()
        {
            deliveryContext pedidosContext = new deliveryContext();
            PedidosController pedidosController = new PedidosController(pedidosContext);

            var result = await pedidosController.GetPedidos(new PageAndSortRequest()
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
