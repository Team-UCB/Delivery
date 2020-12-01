using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using Pedidos.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestUnitarios
{
    [TestClass]
    public class TestDetallePedido
    {
        private static long idDetallePedido = -1;
        static deliveryContext pedidos = new deliveryContext();
        DetallePedidosController detallepedidosController = new DetallePedidosController(pedidos);
        [TestMethod]
        public async Task PostDetallePedidoTest()
        {

            var result = await detallepedidosController.PostDetallePedido(new DetallePedido()
            {
                Cantidad = 1,
                SubMonto = 1,
                IdPedidoNavigation = new Pedido()
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
                },
                IdProductoNavigation = new Producto
                {
                    Nombre = "Coca cola 2lts.",
                    PrecioUnitario = 15,
                    Cantidad = 2,
                    PrecioMayor = 10,
                    Marca = "Embol S.A",
                    Modelo = "Coca-Cola",
                    Especificaciones = "Gaseosa negra de 2 lts",
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
                }
            });


            Assert.IsNotNull(result.Result);
            DetallePedido detallepedido = (DetallePedido)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(detallepedido);
            Assert.IsTrue(detallepedido.Id > 0);
            idDetallePedido = detallepedido.Id;
        }
        [TestMethod]
        public async Task UpdateDetallePedidoTest()
        {

            var result = await detallepedidosController.PutDetallePedido(idDetallePedido, new DetallePedido()
            {
                Cantidad = 2000,
                SubMonto = 1,
                IdPedidoNavigation = new Pedido()
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
                },
                IdProductoNavigation = new Producto
                {
                    Nombre = "Coca cola 2lts.",
                    PrecioUnitario = 15,
                    Cantidad = 2,
                    PrecioMayor = 10,
                    Marca = "Embol S.A",
                    Modelo = "Coca-Cola",
                    Especificaciones = "Gaseosa negra de 2 lts",
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
                }
            });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DeleteDetallePedidoTest()
        {

            var result = await detallepedidosController.DeleteDetallePedido(idDetallePedido);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Add99DetallePedidoTest()
        {

            for (int i = 0; i < 99; i++)
            {
                var result = await detallepedidosController.PostDetallePedido(new DetallePedido()
                {
                    Cantidad = i,
                    SubMonto = i,
                    IdPedidoNavigation = new Pedido()
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
                    },
                    IdProductoNavigation = new Producto
                    {
                        Nombre = "Coca cola 2lts.",
                        PrecioUnitario = 15,
                        Cantidad = 2,
                        PrecioMayor = 10,
                        Marca = "Embol S.A",
                        Modelo = "Coca-Cola",
                        Especificaciones = "Gaseosa negra de 2 lts",
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
                    }
                });
                Assert.IsNotNull(result);
            }
        }
        [TestMethod]
        public async Task GetDetallePedidosTest()
        {
            var result = await detallepedidosController.GetRubro(new PageAndSortRequest() { Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);

        }
    }
}
