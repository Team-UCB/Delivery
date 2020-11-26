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
    public class TestFactura
    {
        private static long idFactura = -1;

        [TestMethod]
        public async Task AddFactura()
        {
            PedidosPollomonContext facturasContext = new PedidosPollomonContext();
            FacturasController facturasController = new FacturasController(facturasContext);


            var result = await facturasController.PostFactura(new Factura()
            {
                NroFactura = 0,
                FechaEmision = DateTime.Now,
                Estado = "activo",
                CodigoControl = "text",
                Observaciones = "ninguno",
                IdDosificacionNavigation = new Dosificacion()
                {
                    NroAutorizacion = 0,
                    NroFacturaActual = 0,
                    Llave = "text",
                    FechaLimiteEmision = DateTime.Now,
                    Leyenda = "text",
                    FechaActivacion = DateTime.Now,
                    Activa = 0,
                    ActividadPrincipal = "text",
                    ActividadSecundaria = "text"
                },
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
                        NombresApellidos = "Galleta Mana",
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

                }

            });

            Assert.IsNotNull(result.Result);
            Factura factura = (Factura)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(factura);
            Assert.IsTrue(factura.Id > 0);
            idFactura = factura.Id;
        }
        [TestMethod]
        public async Task UpdFactura()
        {
            PedidosPollomonContext facturasContext = new PedidosPollomonContext();
            FacturasController facturasController = new FacturasController(facturasContext);

            var result = await facturasController.PutFactura(idFactura, new Factura()
            {
                NroFactura = 0,
                FechaEmision = DateTime.Now,
                Estado = "activo",
                CodigoControl = "text",
                Observaciones = "ninguno",
                IdDosificacionNavigation = new Dosificacion()
                {
                    NroAutorizacion = 0,
                    NroFacturaActual = 0,
                    Llave = "text",
                    FechaLimiteEmision = DateTime.Now,
                    Leyenda = "text",
                    FechaActivacion = DateTime.Now,
                    Activa = 0,
                    ActividadPrincipal = "text",
                    ActividadSecundaria = "text"
                },
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
                        NombresApellidos = "Galleta Mana",
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

                }

            });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DelFactura()
        {
            PedidosPollomonContext facturasContext = new PedidosPollomonContext();
            FacturasController facturasController = new FacturasController(facturasContext);

            var result = await facturasController.DeleteFactura(idFactura);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task Add99Factura()
        {
            PedidosPollomonContext facturasContext = new PedidosPollomonContext();
            FacturasController facturasController = new FacturasController(facturasContext);

            for (int i = 0; i < 10; i++)
            {

                var result = await facturasController.PostFactura(new Factura()
                {
                    NroFactura = 0,
                    FechaEmision = DateTime.Now,
                    Estado = "activo",
                    CodigoControl = "text",
                    Observaciones = "ninguno",
                    IdDosificacionNavigation = new Dosificacion()
                    {
                        NroAutorizacion = 0,
                        NroFacturaActual = 0,
                        Llave = "text",
                        FechaLimiteEmision = DateTime.Now,
                        Leyenda = "text",
                        FechaActivacion = DateTime.Now,
                        Activa = 0,
                        ActividadPrincipal = "text",
                        ActividadSecundaria = "text"
                    },
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
                            NombresApellidos = "Galleta Mana",
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

                    }

                });
                Assert.IsNotNull(result.Result);
            }
        }
        [TestMethod]
        public async Task GetFacturas()
        {
            PedidosPollomonContext facturasContext = new PedidosPollomonContext();
            FacturasController facturasController = new FacturasController(facturasContext);

            var result = await facturasController.GetFacturas(new PageAndSortRequest()
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
