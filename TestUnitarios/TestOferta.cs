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
    public class TestOferta
    {
        private static long idOferta = -1;
       
        [TestMethod]
        public async Task AddOferta()
        {
            PedidosPollomonContext pedidosContext = new PedidosPollomonContext();
            OfertasController ofertasController = new OfertasController(pedidosContext);
            var result = await ofertasController.PostOfertum(new Ofertum() {
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                PrecioOferta = 100,
               
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now,
                IdUsuario = 1,
                IdProductoNavigation=new Producto() {
                    Nombre = "Pie maracuya",
                    PrecioUnitario = 50,
                    Cantidad = 10,
                    PrecioMayor = 45,
                    Marca = "Jenny",
                    Modelo = "Mediana",
                    Especificaciones = "12 personas",
                    IdCategoriaNavigation = new CategoriaProducto() {
                        Nombre = "Bebida",
                        Descripcion = "Coca-Cola",
                        Lugar = "Tarija"
                    },
                    IdVendedorNavigation =new Vendedor() {
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
            Ofertum oferta = (Ofertum)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(oferta);
            Assert.IsTrue(oferta.Id > 0);
            idOferta = oferta.Id;
        }
        
       
        [TestMethod]
        public async Task UpdOferta()
        {
            PedidosPollomonContext pedidosContext = new PedidosPollomonContext();
            OfertasController ofertasController = new OfertasController(pedidosContext);
           
            var result = await ofertasController.PutOfertum(idOferta, new Ofertum() {
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                PrecioOferta = 200,
                IdProducto = 1,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now,
                IdUsuario = 1
            });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DelOferta()
        {
            PedidosPollomonContext pedidosContext = new PedidosPollomonContext();
            OfertasController ofertasController = new OfertasController(pedidosContext);
            
            var result = await ofertasController.DeleteOfertum(idOferta);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Add99Oferta()
        {
            PedidosPollomonContext pedidosContext = new PedidosPollomonContext();
            OfertasController ofertasController = new OfertasController(pedidosContext);
           
           
            for (int i = 0; i < 99; i++)
            {
                var result = await ofertasController.PostOfertum(new Ofertum() {
                    FechaInicio = DateTime.Now,
                    FechaFin = DateTime.Now,
                    PrecioOferta = 100,

                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                    IdUsuario = 1,
                    IdProductoNavigation = new Producto()
                    {
                        Nombre = "Pie maracuya",
                        PrecioUnitario = 50,
                        Cantidad = 10,
                        PrecioMayor = 45,
                        Marca = "Jenny",
                        Modelo = "Mediana",
                        Especificaciones = "12 personas",
                        IdCategoriaNavigation = new CategoriaProducto()
                        {
                            Nombre = "Bebida",
                            Descripcion = "Coca-Cola",
                            Lugar = "Tarija"
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
        public async Task GetOfertas()
        {
            PedidosPollomonContext pedidosContext = new PedidosPollomonContext();
            OfertasController ofertasController = new OfertasController(pedidosContext);
          
            var result = await ofertasController.GetOfertas(new PageAndSortRequest() { 
                Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);
        }

       
    }
}
