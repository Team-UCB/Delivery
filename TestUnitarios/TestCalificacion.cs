using Pedidos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Pedidos.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TestUnitarios
{
    class TestCalificacion
    {
        private static long idCalificacion = -1;
        static deliveryContext calificacion = new deliveryContext();
        CalificacionesController calificacionController = new CalificacionesController(calificacion);
        [TestMethod]
        public async Task PostCalificacionTest()
        {

            var result = await calificacionController.PostCalificacion(new Calificacion()
            {
                IdOrigen = 1,
                IdDestino = 1,
                Puntaje = 1,
                Observaciones = "good",
                Tipo = "grande",

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
                }
            });




            Assert.IsNotNull(result.Result);
            Calificacion calificacion = (Calificacion)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(calificacion);
            Assert.IsTrue(calificacion.Id > 0);
            idCalificacion = calificacion.Id;
        }
        [TestMethod]
        public async Task UpdateCalificacionTest()
        {

            var result = await calificacionController.PutCalificacion(idCalificacion, new Calificacion()
            {
                IdOrigen = 1,
                IdDestino = 1,
                Puntaje = 1,
                Observaciones = "good",
                Tipo = "grande",

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
                }
            });
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public async Task DeleteCalificacionTest()
        {

            var result = await calificacionController.DeleteCalificacion(idCalificacion);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Add99CalificacionTest()
        {

            for (int i = 0; i < 99; i++)
            {
                var result = await calificacionController.PostCalificacion(new Calificacion()
                {

                    IdOrigen = i,
                    IdDestino = i,
                    Puntaje = i,
                    Observaciones = "good" + i,
                    Tipo = "grande" + i,

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
                    }
                });
                Assert.IsNotNull(result);
            }
        }
        [TestMethod]
        public async Task GetCalificacionTest()
        {
            var result = await calificacionController.GetCalificacion(new PageAndSortRequest() { Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);

        }
    }  }