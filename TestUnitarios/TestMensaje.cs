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
    public class TestMensaje
    {
        private static long idMensaje = -1;
        PedidosPollomonContext pedidosContext = new PedidosPollomonContext();

        [TestMethod]
        public async Task Add()
        {

            MensajesController mensajesController = new MensajesController(pedidosContext);
            
         
            var result = await mensajesController.PostMensaje(new Mensaje() {
                FechaHora=DateTime.Now,
                Text = "test", 
                IdChatNavigation=new Chat { 
                    Estado="vivo",
                    IdDestino=124,
                    IdOrigen=456 
                } 
            });
            Mensaje mensajes = (Mensaje)(result.Result as CreatedAtActionResult).Value;

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(mensajes);
            Assert.IsTrue(mensajes.Id > 0);
            idMensaje = mensajes.Id;
        }

        [TestMethod]
        public async Task Upd()
        {
            Console.WriteLine(idMensaje);
            MensajesController mensajesController = new MensajesController(pedidosContext);
            var result = await mensajesController.PutMensaje(idMensaje, new Mensaje() {
                FechaHora = DateTime.Now,
                Text = "cambiando",
                IdChatNavigation = new Chat
                {
                    Estado = "vivo",
                    IdDestino = 124,
                    IdOrigen = 456
                }
            });

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Del()
        {
            MensajesController mensajesController = new MensajesController(pedidosContext);
            var result = await mensajesController.DeleteMensaje(idMensaje);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Add99()
        {
            MensajesController mensajesController = new MensajesController(pedidosContext);

            for (int i = 0; i < 99; i++)
            {
                var result = await mensajesController.PostMensaje(new Mensaje() {
                    FechaHora = DateTime.Now,
                    Text = "test " + i,
                    IdChatNavigation = new Chat
                    {
                        Estado = "vivo",
                        IdDestino = 124,
                        IdOrigen = 456
                    }
                });
                Assert.IsNotNull(result.Result);
            }
        }
        [TestMethod]
        public async Task Get()
        {

            MensajesController mensajesController = new MensajesController(pedidosContext);
            var result = await mensajesController.GetMensaje(new PageAndSortRequest() { Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);
        }

    }
}
