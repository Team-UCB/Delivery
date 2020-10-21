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
    public class TestRubro
    {
        //private static long idRubro = -1;
        //[TestMethod]
        //public async Task Add()
        //{
        //    PedidosPollomonContext pedidospollomonContext = new PedidosPollomonContext();
        //    RubrosController rubrosController = new RubrosController(pedidospollomonContext, "");
        //    var result = await rubrosController.PostRubro(new Rubro() { Nombre = "test11", Descripcion = "test11" });


        //    Assert.IsNotNull(result.Result);
        //    Rubro rubro = (Rubro)(result.Result as CreatedAtActionResult).Value;
        //    Assert.IsNotNull(rubro);
        //    Assert.IsTrue(rubro.Id > 0);
        //    idRubro = rubro.Id;
        //}

        //[TestMethod]
        //public async Task Upd()
        //{
        //    //Console.WriteLine(idRubro);
        //    PedidosPollomonContext pedidospollomonContext = new PedidosPollomonContext();
        //    RubrosController rubrosController = new RubrosController(pedidospollomonContext, "");
        //    var result = await rubrosController.PutRubro(4, new Rubro() { Nombre = "text", Descripcion = "text" });

        //    Assert.IsNotNull(result);
        //}
        //[TestMethod]
        //public async Task Del()
        //{
        //    PedidosPollomonContext pedidospollomonContext = new PedidosPollomonContext();
        //    RubrosController rubrosController = new RubrosController(pedidospollomonContext, "");
        //    var result = await rubrosController.DeleteRubro(6);


        //    Assert.IsNotNull(result);
        //}
        //[TestMethod]
        //public async Task Get()
        //{
        //    PedidosPollomonContext pedidospollomonContext = new PedidosPollomonContext();
        //    RubrosController rubrosController = new RubrosController(pedidospollomonContext, "");
        //    var result = await rubrosController.GetAllRubros();

        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.Count() > 0);
        //}
        //[TestMethod]
        //public async Task Add99()
        //{
        //    PedidosPollomonContext pedidospollomonContext = new PedidosPollomonContext();
        //    RubrosController rubrosController = new RubrosController(pedidospollomonContext, "");
        //    for (int i = 0; i < 10; i++)
        //    {
        //        var result = await rubrosController.PostRubro(new Rubro() { Nombre = "test" + i, Descripcion = "test" + i });
        //        Assert.IsNotNull(result.Result);
        //    }
        //}
    }
}
