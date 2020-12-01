using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pedidos.Controllers;
using Pedidos.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace TestUnitarios
{
    [TestClass]
    public class TestDosificacion
    {
        private static long idDosificacion = -1;
        [TestMethod]
        public async Task Add()
        {
            deliveryContext dosificacionesContext = new deliveryContext();
            DosificacionesController dosificacionesController = new DosificacionesController(dosificacionesContext);
            var result = await dosificacionesController.PostDosificacion(new Dosificacion()
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
            });


            Assert.IsNotNull(result.Result);
            Dosificacion dosificacion = (Dosificacion)(result.Result as CreatedAtActionResult).Value;
            Assert.IsNotNull(dosificacion);
            Assert.IsTrue(dosificacion.Id > 0);
            idDosificacion = dosificacion.Id;
        }
        [TestMethod]
        public async Task Upd()
        {

            deliveryContext dosificacionesContext = new deliveryContext();
            DosificacionesController dosificacionesController = new DosificacionesController(dosificacionesContext);
            var result = await dosificacionesController.PutDosificacion(idDosificacion, new Dosificacion()
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
            });



            Assert.IsNotNull(result);


        }
        [TestMethod]
        public async Task Del()
        {
            deliveryContext dosificacionesContext = new deliveryContext();
            DosificacionesController dosificacionesController = new DosificacionesController(dosificacionesContext);
            var result = await dosificacionesController.DeleteDosificacion(idDosificacion);


            Assert.IsNotNull(result);



        }

        [TestMethod]
        public async Task Add99()
        {
            deliveryContext dosificacionesContext = new deliveryContext();
            DosificacionesController dosificacionesController = new DosificacionesController(dosificacionesContext);
            for (int i = 0; i < 10; i++)
            {
                var result = await dosificacionesController.PostDosificacion(new Dosificacion()
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
                });

                Assert.IsNotNull(result.Result);

            }

        }
        [TestMethod]
        public async Task Get()
        {
            deliveryContext dosificacionesContext = new deliveryContext();
            DosificacionesController dosificacionesController = new DosificacionesController(dosificacionesContext);
            var result = await dosificacionesController.GetDosificaciones(new PageAndSortRequest() { Pagina = 1, TamPagina = 10, Columna = "Id", Direccion = "asc", Filtro = "" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Value.Datos.Count() > 0);
        }
    }
}

