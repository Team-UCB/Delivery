using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using Pedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnitarios
{
    [TestClass]
    public class TestDetalleFactura
    {
        private static long idFoto = -1;
        [TestMethod]
        public async Task Add99DetallesFactura()
        {
            PedidosPollomonContext pedidosContext = new PedidosPollomonContext();
            DetalleFacturasController detalleFacturasController = new DetalleFacturasController(pedidosContext);
            for (int i = 0; i < 10; i++)
            {
                var result = await detalleFacturasController.PostDetalleFactura(new DetalleFactura()
                {
                    Cantidad = 100,
                    Monto = 100,
                    Descripcion="test"+i,
                    IdDetallePedidoNavigation= new DetallePedido 
                    {
                        IdPedidoNavigation= new Pedido 
                        {
                            FechaIngreso = DateTime.Now,
                            FechaAtencion = DateTime.Now,
                            FechaSalida= DateTime.Now,
                            FechaEntrega = DateTime.Now,
                            Estado ="test"+1,
                            CodigoQrFactura="fg/gfg23*23",
                            MontoEnvio=10,
                            TipoPago="contado",
                            MontoCliente=50,
                            IdClienteNavigation = new Cliente 
                            {
                                NombresApellidos="Leonardo Perez",
                                Celular="63785633",
                                Telefono="466-23673"
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
                            },
                            IdTransporteNavigation = new Transportador 
                            {
                                NombreCompleto="Pedro Fuentes",
                                Celular="72945372",
                                DescripcionVehiculo="es un cuadriciclo",
                                TipoVehiculo="Motocicleta",
                                Estado="test"+i,
                                Latitud=-21233434,
                                Longitud=232336343,
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
                        },
                        Cantidad=24,
                        SubMonto=34
                    },
                    IdFacturaNavigation = new Factura 
                    {
                        NroFactura=34234,
                        FechaEmision= DateTime.Now,
                        Estado="test"+i,
                        CodigoControl="7kje5dgon8",
                        Observaciones="test"+i,
                        IdDosificacionNavigation = new Dosificacion 
                        {
                            NroAutorizacion = 23443436763,
                            NroFacturaActual= 1+i,
                            Llave="f3-63d-j6-4f",
                            FechaLimiteEmision=DateTime.Now,
                            Leyenda="test"+i,
                            FechaActivacion= DateTime.Now,
                            ActividadPrincipal="test"+1,
                            ActividadSecundaria="test"+1,
                        },
                        IdPedidoNavigation = new Pedido
                        {
                            FechaIngreso = DateTime.Now,
                            FechaAtencion = DateTime.Now,
                            FechaSalida = DateTime.Now,
                            FechaEntrega = DateTime.Now,
                            Estado = "test" + 1,
                            CodigoQrFactura = "fg/gfg23*23",
                            MontoEnvio = 10,
                            TipoPago = "contado",
                            MontoCliente = 50,
                            IdClienteNavigation = new Cliente
                            {
                                NombresApellidos = "Leonardo Perez",
                                Celular = "63785633",
                                Telefono = "466-23673"
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
                            },
                            IdTransporteNavigation = new Transportador
                            {
                                NombreCompleto = "Pedro Fuentes",
                                Celular = "72945372",
                                DescripcionVehiculo = "es un cuadriciclo",
                                TipoVehiculo = "Motocicleta",
                                Estado = "test" + i,
                                Latitud = -21233434,
                                Longitud = 232336343,
                            }
                        }
                    }
                });

                Assert.IsNotNull(result.Result);
            }
        }
        [TestMethod]
        public async Task AddDetalleFactura()
        {
            PedidosPollomonContext pedidosContext = new PedidosPollomonContext();
            DetalleFacturasController detalleFacturasController = new DetalleFacturasController(pedidosContext);
            var result = await detalleFacturasController.PostDetalleFactura(new DetalleFactura()
            {
                Cantidad = 100,
                Monto = 100,
                Descripcion = "test",
                IdDetallePedidoNavigation = new DetallePedido
                {
                    IdPedidoNavigation = new Pedido
                    {
                        FechaIngreso = DateTime.Now,
                        FechaAtencion = DateTime.Now,
                        FechaSalida = DateTime.Now,
                        FechaEntrega = DateTime.Now,
                        Estado = "test",
                        CodigoQrFactura = "fg/gfg23*23",
                        MontoEnvio = 10,
                        TipoPago = "contado",
                        MontoCliente = 50,
                        IdClienteNavigation = new Cliente
                        {
                            NombresApellidos = "Leonardo Perez",
                            Celular = "63785633",
                            Telefono = "466-23673"
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
                        },
                        IdTransporteNavigation = new Transportador
                        {
                            NombreCompleto = "Pedro Fuentes",
                            Celular = "72945372",
                            DescripcionVehiculo = "es un cuadriciclo",
                            TipoVehiculo = "Motocicleta",
                            Estado = "test",
                            Latitud = -21233434,
                            Longitud = 232336343,
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
                    },
                    Cantidad = 24,
                    SubMonto = 34
                },
                IdFacturaNavigation = new Factura
                {
                    NroFactura = 34234,
                    FechaEmision = DateTime.Now,
                    Estado = "test",
                    CodigoControl = "7kje5dgon8",
                    Observaciones = "test",
                    IdDosificacionNavigation = new Dosificacion
                    {
                        NroAutorizacion = 23443436763,
                        NroFacturaActual = 1,
                        Llave = "f3-63d-j6-4f",
                        FechaLimiteEmision = DateTime.Now,
                        Leyenda = "test",
                        FechaActivacion = DateTime.Now,
                        ActividadPrincipal = "test" + 1,
                        ActividadSecundaria = "test" + 1,
                    },
                    IdPedidoNavigation = new Pedido
                    {
                        FechaIngreso = DateTime.Now,
                        FechaAtencion = DateTime.Now,
                        FechaSalida = DateTime.Now,
                        FechaEntrega = DateTime.Now,
                        Estado = "test" + 1,
                        CodigoQrFactura = "fg/gfg23*23",
                        MontoEnvio = 10,
                        TipoPago = "contado",
                        MontoCliente = 50,
                        IdClienteNavigation = new Cliente
                        {
                            NombresApellidos = "Leonardo Perez",
                            Celular = "63785633",
                            Telefono = "466-23673"
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
                        },
                        IdTransporteNavigation = new Transportador
                        {
                            NombreCompleto = "Pedro Fuentes",
                            Celular = "72945372",
                            DescripcionVehiculo = "es un cuadriciclo",
                            TipoVehiculo = "Motocicleta",
                            Estado = "test",
                            Latitud = -21233434,
                            Longitud = 232336343,
                        }
                    }
                }
            });

            Assert.IsNotNull(result.Result);
            DetalleFactura detalleFactura = (DetalleFactura)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(detalleFactura);
            Assert.IsTrue(detalleFactura.Id > 0);
            idFoto = detalleFactura.Id;
        }
        [TestMethod]
        public async Task UpdDetalleFactura()
        {
            PedidosPollomonContext pedidosContext = new PedidosPollomonContext();
            DetalleFacturasController detalleFacturasController = new DetalleFacturasController(pedidosContext);
            var result = await detalleFacturasController.PutDetalleFactura(idFoto, new DetalleFactura()
            {
                Cantidad = 100,
                Monto = 100,
                Descripcion = "test",
                IdDetallePedidoNavigation = new DetallePedido
                {
                    IdPedidoNavigation = new Pedido
                    {
                        FechaIngreso = DateTime.Now,
                        FechaAtencion = DateTime.Now,
                        FechaSalida = DateTime.Now,
                        FechaEntrega = DateTime.Now,
                        Estado = "test",
                        CodigoQrFactura = "fg/gfg23*23",
                        MontoEnvio = 10,
                        TipoPago = "contado",
                        MontoCliente = 50,
                        IdClienteNavigation = new Cliente
                        {
                            NombresApellidos = "Leonardo Perez",
                            Celular = "63785633",
                            Telefono = "466-23673"
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
                        },
                        IdTransporteNavigation = new Transportador
                        {
                            NombreCompleto = "Pedro Fuentes",
                            Celular = "72945372",
                            DescripcionVehiculo = "es un cuadriciclo",
                            TipoVehiculo = "Motocicleta",
                            Estado = "test",
                            Latitud = -21233434,
                            Longitud = 232336343,
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
                    },
                    Cantidad = 24,
                    SubMonto = 34
                },
                IdFacturaNavigation = new Factura
                {
                    NroFactura = 34234,
                    FechaEmision = DateTime.Now,
                    Estado = "test",
                    CodigoControl = "7kje5dgon8",
                    Observaciones = "test",
                    IdDosificacionNavigation = new Dosificacion
                    {
                        NroAutorizacion = 23443436763,
                        NroFacturaActual = 1,
                        Llave = "f3-63d-j6-4f",
                        FechaLimiteEmision = DateTime.Now,
                        Leyenda = "test",
                        FechaActivacion = DateTime.Now,
                        ActividadPrincipal = "test" + 1,
                        ActividadSecundaria = "test" + 1,
                    },
                    IdPedidoNavigation = new Pedido
                    {
                        FechaIngreso = DateTime.Now,
                        FechaAtencion = DateTime.Now,
                        FechaSalida = DateTime.Now,
                        FechaEntrega = DateTime.Now,
                        Estado = "test" + 1,
                        CodigoQrFactura = "fg/gfg23*23",
                        MontoEnvio = 10,
                        TipoPago = "contado",
                        MontoCliente = 50,
                        IdClienteNavigation = new Cliente
                        {
                            NombresApellidos = "Leonardo Perez",
                            Celular = "63785633",
                            Telefono = "466-23673"
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
                        },
                        IdTransporteNavigation = new Transportador
                        {
                            NombreCompleto = "Pedro Fuentes",
                            Celular = "72945372",
                            DescripcionVehiculo = "es un cuadriciclo",
                            TipoVehiculo = "Motocicleta",
                            Estado = "test",
                            Latitud = -21233434,
                            Longitud = 232336343,
                        }
                    }
                }
            });

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task DelDetalleFactura()
        {
            PedidosPollomonContext pedidosContext = new PedidosPollomonContext();
            DetalleFacturasController detalleFacturasController = new DetalleFacturasController(pedidosContext);
            var result = await detalleFacturasController.DeleteDetalleFactura(idFoto);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task GetDetalleFactura()
        {
            PedidosPollomonContext pedidosContext = new PedidosPollomonContext();
            DetalleFacturasController detalleFacturasController = new DetalleFacturasController(pedidosContext);
            var result = await detalleFacturasController.GetDetalleFactura(new PageAndSortRequest() { Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);
        }
    }
}
