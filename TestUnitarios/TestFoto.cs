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
    public class TestFoto
    {
        private static long idFoto = -1;

        [TestMethod]
        public async Task Add99()
        {
            deliveryContext pedidospollomonContext = new deliveryContext();
            FotosController fotosController = new FotosController(pedidospollomonContext);
            for (int i = 0; i < 10; i++)
            {
                var result = await fotosController.PostFoto(new Foto()
                {
                    Descripcion = "testFoto" + i,
                    PathImg = "testFoto" + i,
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
            }
        }

        [TestMethod]
        public async Task Add()
        {
            deliveryContext pedidospollomonContext = new deliveryContext();
            FotosController fotosController = new FotosController(pedidospollomonContext);
            var result = await fotosController.PostFoto(new Foto()
            {
                Descripcion = "testFa",
                PathImg = "testFa",
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
            Foto foto = (Foto)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(foto);
            Assert.IsTrue(foto.Id > 0);
            idFoto = foto.Id;
        }

        [TestMethod]
        public async Task Upd()
        {
            //Console.WriteLine(idRubro);
            deliveryContext pedidospollomonContext = new deliveryContext();
            FotosController fotosController = new FotosController(pedidospollomonContext);
            var result = await fotosController.PutFoto(idFoto, new Foto()
            {
                Descripcion = "texta",
                PathImg = "texta",
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
        public async Task Del()
        {
            deliveryContext pedidospollomonContext = new deliveryContext();
            FotosController fotosController = new FotosController(pedidospollomonContext);
            var result = await fotosController.DeleteFoto(idFoto);


            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Get()
        {
            deliveryContext pedidosContext = new deliveryContext();
            FotosController fotosController = new FotosController(pedidosContext);
            var result = await fotosController.GetFoto(new PageAndSortRequest() { Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);
        }

    }
}
