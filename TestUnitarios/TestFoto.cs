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
        public async Task Add()
        {
            PedidosPollomonContext pedidospollomonContext = new PedidosPollomonContext();
            FotosController fotosController = new FotosController(pedidospollomonContext, "");
            var result = await fotosController.PostFoto(new Foto() { Descripcion = "testF", PathImg = "testF", IdProducto=1 });


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
            PedidosPollomonContext pedidospollomonContext = new PedidosPollomonContext();
            FotosController fotosController = new FotosController(pedidospollomonContext, "");
            var result = await fotosController.PutFoto(idFoto, new Foto() { Descripcion = "text", PathImg = "text", IdProducto=1 });

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Del()
        {
            PedidosPollomonContext pedidospollomonContext = new PedidosPollomonContext();
            FotosController fotosController = new FotosController(pedidospollomonContext, "");
            var result = await fotosController.DeleteFoto(idFoto);


            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Get()
        {
            PedidosPollomonContext pedidospollomonContext = new PedidosPollomonContext();
            FotosController fotosController = new FotosController(pedidospollomonContext, "");
            var result = await fotosController.GetAllFotos();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }
        [TestMethod]
        public async Task Add99()
        {
            PedidosPollomonContext pedidospollomonContext = new PedidosPollomonContext();
            FotosController fotosController = new FotosController(pedidospollomonContext, "");
            for (int i = 0; i < 10; i++)
            {
                var result = await fotosController.PostFoto(new Foto() { Descripcion = "test" + i, PathImg = "test" + i, IdProducto=1 });
                Assert.IsNotNull(result.Result);
            }
        }

    }
}
