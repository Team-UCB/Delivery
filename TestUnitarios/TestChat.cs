using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using Pedidos.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TestUnitarios
{
    [TestClass]
    public class TestChat
    {


        private static long idChat = -1;
        static PedidosPollomonContext pedidos = new PedidosPollomonContext();
        ChatsController chatsController = new ChatsController(pedidos);
        [TestMethod]
        public async Task PostChatTest()
        {

            var result = await chatsController.PostChat(new Chat() { IdDestino = 1, IdOrigen = 1, Estado = "pasibo" });


            Assert.IsNotNull(result.Result);
            Chat chat = (Chat)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(chat);
            Assert.IsTrue(chat.Id > 0);
            idChat = chat.Id;
        }
        [TestMethod]
        public async Task UpdateChat()
        {

            var result = await chatsController.PutChat(idChat, new Chat() { IdDestino = 1, IdOrigen = 1, Estado = "activo" });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DeleteRubroTest()
        {

            var result = await chatsController.DeleteChat(idChat);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Add99ChatTest()
        {

            for (int i = 0; i < 99; i++)
            {
                var result = await chatsController.PostChat(new Chat() { IdDestino = i, IdOrigen = i, Estado = "activo" + i });
                Assert.IsNotNull(result);
            }
        }
        [TestMethod]
        public async Task GetRubroTest()
        {
            var result = await chatsController.GetChat(new PageAndSortRequest() { Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);

        }
    }
}